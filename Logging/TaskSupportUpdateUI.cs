using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonLibs
{
    public class TaskSupportUpdateUI
    {
        CancellationTokenSource source;
        private readonly SynchronizationContext synchronizationContext;

        public TaskSupportUpdateUI()
        {
            source = new CancellationTokenSource();
            synchronizationContext = SynchronizationContext.Current;
        }

        public Task TaskWorking(Action<CancellationTokenSource, SynchronizationContext> TaskRun)
        {
            return Task.Run(() =>
            {
                TaskRun(source, synchronizationContext);
            });
        }

        public void UpdateUI(Action funcUpdate)
        {
            synchronizationContext.Post(new SendOrPostCallback(o => {
                funcUpdate();
            }), null);
        }


    }
}
