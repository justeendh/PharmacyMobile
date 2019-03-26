using CommonLibs;
using SocketCommunicate;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;

namespace SocketCommunicate
{
    // State object for reading client data asynchronously
    public class StateObject
    {
        // Client  socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 10240;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
    }

    public class AsynchronousSocketListener
    {
        public const int BUFFER_SIZE = 10240;
        public const int MAX_CLIENTS = 100;

        private Socket listener;
        private int _portListen;
        private IPEndPoint localEndPoint;
        private Action<ItemData, AsynchronousSocketListener, Socket> _actionProccesing;

        private ManualResetEvent acceptConnection = new ManualResetEvent(false);
        private ManualResetEvent sendClientDone = new ManualResetEvent(false);
        private ManualResetEvent receiveClientDone = new ManualResetEvent(false);
        
        public AsynchronousSocketListener(int PORT_LISTEN, Action<ItemData, AsynchronousSocketListener, Socket> actionProccesing)
        {
            this._portListen = PORT_LISTEN;
            this._actionProccesing = actionProccesing;
            this.Init();
        }

        private void Init()
        {
            Random rnd = new Random();
            byte[] bytes = new Byte[BUFFER_SIZE];
            localEndPoint = new IPEndPoint(IPAddress.Any, _portListen);
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        
        public void Start()
        {
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(MAX_CLIENTS);
                while (true)
                {
                    acceptConnection.Reset();
                    Console.WriteLine(string.Format("{0}: Waiting for a connection on port {1}", DateTime.Now, _portListen));
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                    acceptConnection.WaitOne();
                }
            }
            catch (Exception ex)
            {
                LoggingData.WriteLog(ex);
                this.Restart();
            }
        }

        private void Restart()
        {
            this.Init();
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);
            IPEndPoint localIpEndPoint = handler.LocalEndPoint as IPEndPoint;
            Console.WriteLine(string.Format("{0}: Client connected: {1}", DateTime.Now, handler.RemoteEndPoint.ToString()));
            LoggingData.WriteLog(string.Format(string.Format("{0}: Client connected: {1}", DateTime.Now, handler.RemoteEndPoint.ToString())));

            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
            acceptConnection.Set();
        }
        
        public void Disconnect(Socket client)
        {
            try
            {
                // Release the socket. 
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
                Socket client = ar.AsyncState as Socket;
                if (client != null) client.EndDisconnect(ar);
            }
            catch { }
        }

        private void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            int bytesRead = 0;
            try
            {
                if (handler != null)
                {
                    bytesRead = handler.EndReceive(ar);
                    if (bytesRead > 0)
                    {
                        byte[] byteRecv = new byte[bytesRead];
                        Array.Copy(state.buffer, 0, byteRecv, 0, bytesRead);
                        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                        byte[] decryptedData = CommonFunctions.Decryption(byteRecv, RSA.ExportParameters(true), false);
                        ThreadPool.QueueUserWorkItem(new WaitCallback(stateQueue => {
                            if (_actionProccesing != null) _actionProccesing(ItemData.Parse(decryptedData), this, handler);
                        }));
                        StateObject new_state = new StateObject();
                        new_state.workSocket = handler;
                        handler.BeginReceive(new_state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), new_state);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingData.WriteLog(ex);
                try
                {
                    if (handler != null)
                    {
                        Console.WriteLine("Disconected to {0} | ReadCallback", handler.RemoteEndPoint.ToString());
                        handler.Disconnect(false);
                    }
                } catch { }
                return;
            }
        }

        public bool SendData(Socket client, byte[] data)
        {
            try
            {
                if (client != null && client.IsConnected())
                {
                    RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                    byte[] dataEncrypt = CommonFunctions.Encryption(data, RSA.ExportParameters(false), false);
                    client.BeginSend(dataEncrypt, 0, dataEncrypt.Length, 0, new AsyncCallback(SendCallback), client);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LoggingData.WriteLog(ex);
                return false;
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                int bytesSent = handler.EndSend(ar);
            }
            catch (Exception ex)
            {
                LoggingData.WriteLog(ex);
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
