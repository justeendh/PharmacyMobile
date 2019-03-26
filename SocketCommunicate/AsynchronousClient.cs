using CommonLibs;
using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace SocketCommunicate
{
    public class AsynchronousClient
    {
        private string _Address;
        private int _Port;
        private Socket client;
        private byte[] responseServerData;
        private Action<ItemData, AsynchronousClient> _actionProccesing;
        private const int TIME_OUT = 5000;

        // ManualResetEvent instances signal completion.  
        private ManualResetEvent connectDone = new ManualResetEvent(false);
        private ManualResetEvent sendDone = new ManualResetEvent(false);
        private ManualResetEvent receiveDone = new ManualResetEvent(false);

        // The response from the remote device.  
        //private byte[] response;
        private bool _autoReconect;
        private int _reconectDelay;

        private CancellationTokenSource tokenSource;
        private CancellationToken cancelToken;
        private Task task;

        private void startConnect()
        {
            try
            {
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.BeginConnect(this._Address, this._Port, new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne(TIME_OUT);
            }
            catch (Exception ex)
            {
                LoggingData.WriteLog(ex);
            }
        }

        public AsynchronousClient(string Address, int Port, Action<ItemData, AsynchronousClient> actionProccesing, bool autoReconect = true, int reconectDelay = 5000)
        {
            this._Address = Address;
            this._Port = Port;
            this._actionProccesing = actionProccesing;
            this._autoReconect = autoReconect;
            this._reconectDelay = reconectDelay;

            Console.WriteLine("{0}: Connect to server {1} on port {2} ...", DateTime.Now, _Address, _Port);
            this.startConnect();

            if (_autoReconect && (task == null || (task.IsCompleted || task.Status != TaskStatus.Running || task.Status != TaskStatus.WaitingToRun || task.Status != TaskStatus.WaitingForActivation)))
            {
                tokenSource = new CancellationTokenSource();
                cancelToken = tokenSource.Token;
                task = Task.Factory.StartNew(() =>
                {
                    // Were we already canceled?
                    bool moreToDo = true;
                    cancelToken.ThrowIfCancellationRequested();
                    Thread.Sleep(_reconectDelay);
                    while (moreToDo)
                    {
                        // Poll on this property if you have to do
                        // other cleanup before throwing.
                        if (cancelToken.IsCancellationRequested)
                        {
                            cancelToken.ThrowIfCancellationRequested();
                        }

                        bool isConnected = client.IsConnected();
                        if (!isConnected)
                        {
                            Console.WriteLine("{0}: Reconnect to server {1} on port {2} ...", DateTime.Now, _Address, _Port);
                            this.Reconect();
                        }
                        Thread.Sleep(_reconectDelay);
                    }
                }, tokenSource.Token); // Pass same token to StartNew
            }
        }

        public void Reconect()
        {
            this.startConnect();
        }

        public bool IsConnected()
        {
            try
            {
                if (client != null) return client.IsConnected();
                else return false;
            }
            catch
            {
                return false;
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                if (client != null)
                {
                    Socket client = (Socket)ar.AsyncState;
                    client.EndConnect(ar);
                    Console.WriteLine("{0:dd-MM-yyyy HH:mm:ss}: Connected to {1}", DateTime.Now, client.RemoteEndPoint.ToString());
                }
                connectDone.Set();
            }
            catch (Exception ex)
            {
                connectDone.Set();
                LoggingData.WriteLog(ex);
            }
        }


        public byte[] SendData(byte[] data, int timeoutSend = 60000, int timeoutRecv = 60000)
        {
            try
            {
                if (client != null && client.IsConnected())
                {
                    RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                    byte[] dataEncrypt = CommonFunctions.Encryption(data, RSA.ExportParameters(false), false);
                    sendDone.Reset();
                    receiveDone.Reset();
                    client.BeginSend(dataEncrypt, 0, dataEncrypt.Length, 0, new AsyncCallback(SendCallback), client);
                    sendDone.WaitOne(timeoutSend);
                    StateObject state = new StateObject();
                    state.workSocket = client;
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                    receiveDone.WaitOne(timeoutRecv);

                    return responseServerData;
                }
                return null;
            }
            catch (Exception ex)
            {
                LoggingData.WriteLog(ex);
                Console.WriteLine(ex.ToString());
                return null;
            }
        }


        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
                sendDone.Set();
            }
            catch (Exception ex)
            {
                sendDone.Set();
                LoggingData.WriteLog(ex);
                Console.WriteLine(ex.ToString());
            }
        }


        private void ReceiveCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket client = state.workSocket;
            responseServerData = null;
            try
            {
                if(client != null)
                {
                    int bytesRead = client.EndReceive(ar);
                    if (bytesRead > 0)
                    {
                        byte[] byteRecv = new byte[bytesRead];
                        Array.Copy(state.buffer, 0, byteRecv, 0, bytesRead);
                        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                        byte[] decryptedData = CommonFunctions.Decryption(byteRecv, RSA.ExportParameters(true), false);
                        responseServerData = decryptedData;
                        ThreadPool.QueueUserWorkItem(new WaitCallback(stateQueue => {
                            if (_actionProccesing != null) _actionProccesing(ItemData.Parse(decryptedData), this);
                        }));
                        receiveDone.Set();
                        state = new StateObject();
                        state.workSocket = client;
                        client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                    }
                }
            }
            catch (Exception ex)
            {
                receiveDone.Set();
                LoggingData.WriteLog(ex);
                try
                {
                    if (client != null) {

                        Console.WriteLine("Disconected to {0} | ReadCallback", client.RemoteEndPoint.ToString());
                        client.Disconnect(true);
                    }
                } catch { }
                Console.WriteLine(ex.ToString());
            }
        }

        public void Disconnect()
        {
            try
            {
                // Release the socket. 
                tokenSource.Cancel();
                client.BeginDisconnect(false, DisconnectCallback, client);
            }
            catch (Exception ex)
            {
                LoggingData.WriteLog(ex);
                Console.WriteLine(ex.ToString());
            }
        }

        private void DisconnectCallback(IAsyncResult ar)
        {
            try
            {
                client.EndDisconnect(ar);
                client.Close();
            }
            catch { }
        }

    }
}
