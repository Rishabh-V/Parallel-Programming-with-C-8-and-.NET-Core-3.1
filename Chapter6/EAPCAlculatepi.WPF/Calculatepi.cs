using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAPCAlculatepi.WPF
{
    public delegate void CalculatepiCompletedEventHandler(object sender, CalculatepiCompletedEventArgs e);
    public class Calculatepi
    {
        //Completion event handler
        public event CalculatepiCompletedEventHandler CalculatepiCompleted;

        //delegate to call the synchronous operation in async way (in this case we are using task.run)
        private delegate void CalculationEventHandler(int numSteps, object sender, AsyncOperation asyncOp);

        //Delegate to handle callback and raise completion event
        private SendOrPostCallback onCompletedDelegate;

        public Calculatepi()
        {
            onCompletedDelegate = new SendOrPostCallback(CalculationCompleted);
        }

        /// <summary>
        /// callback method
        /// </summary>
        private void CalculationCompleted(object operationState)
        {
            CalculatepiCompletedEventArgs e = operationState as CalculatepiCompletedEventArgs;

            if (CalculatepiCompleted != null)
            {
                CalculatepiCompleted(this, e);
            }
        }

        //Dicstionary to handle multiple tasks innovcation, uniquely identify each opearation as one dictionary element
        private HybridDictionary parallelTasks = new HybridDictionary();

        /// <summary>
        /// Calculate pi async
        /// </summary>
        public void CalculatepiAsync(int numsteps, object sender, object operationState)
        {
            AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(operationState);

            //Locking for thread safety
            lock (parallelTasks.SyncRoot)
            {
                if (parallelTasks.Contains(operationState))
                {
                    throw new ArgumentException("User state parameter must be unique", "userState");
                }

                parallelTasks[operationState] = asyncOp;
            }

            CalculationEventHandler worker = new CalculationEventHandler(Calculatepivalue);

            //Execute process Asynchronously
            Task.Run(() => worker(numsteps, sender, asyncOp));
        }

        /// <summary>
        /// Synchronous method to calculate pi
        /// </summary>
        void Calculatepivalue(int numsteps, object sender, AsyncOperation asyncOp)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            numsteps++;

            uint[] value = new uint[numsteps * 10 / 3 + 2];
            uint[] rem = new uint[numsteps * 10 / 3 + 2];

            uint[] pi = new uint[numsteps];

            for (int j = 0; j < value.Length; j++)
                value[j] = 20;

            for (int i = 0; i < numsteps; i++)
            {
                Thread.Sleep(5);
                if (TaskCanceled(asyncOp.UserSuppliedState))
                {
                    break;
                }
                uint carryForward = 0;
                for (int j = 0; j < value.Length; j++)
                {
                    uint number = (uint)(value.Length - j - 1);
                    uint pow = number * 2 + 1;

                    value[j] += carryForward;

                    uint quotient = value[j] / pow;
                    rem[j] = value[j] % pow;

                    carryForward = quotient * number;
                }


                pi[i] = (value[value.Length - 1] / 10);


                rem[value.Length - 1] = value[value.Length - 1] % 10; ;

                for (int j = 0; j < value.Length; j++)
                    value[j] = rem[j] * 10;
            }

            var result = "";

            uint c = 0;

            for (int i = pi.Length - 1; i >= 0; i--)
            {
                pi[i] += c;
                c = pi[i] / 10;

                result = (pi[i] % 10).ToString() + result;
            }
            result = result.Substring(0, 1) + "." + result.Substring(1, result.Length - 1);


            //rasie callback
            CalculatepiCompletedEventArgs e = new CalculatepiCompletedEventArgs(result, timer.ElapsedMilliseconds, sender, null, TaskCanceled(asyncOp.UserSuppliedState), asyncOp.UserSuppliedState);
            asyncOp.PostOperationCompleted(onCompletedDelegate, e);

            lock (parallelTasks.SyncRoot)
            {
                parallelTasks.Remove(asyncOp.UserSuppliedState);
            }

            timer.Stop();
        }

        // This method cancels a pending asynchronous operation.
        public void CancelAsync(object operationState)
        {
            AsyncOperation asyncOp = parallelTasks[operationState] as AsyncOperation;
            if (asyncOp != null)
            {
                lock (parallelTasks.SyncRoot)
                {
                    parallelTasks.Remove(operationState);
                }
            }
        }

        //Utility method to check the task status
        private bool TaskCanceled(object operationState)
        {
            return (parallelTasks[operationState] == null);
        }
    }

    public class CalculatepiCompletedEventArgs : AsyncCompletedEventArgs
    {
        public string Result { get; private set; }
        public long TimeTaken { get; private set; }

        public Object Sender { get; private set; }

        public CalculatepiCompletedEventArgs(string value, long TimeTaken, object sender, Exception e, bool canceled, object state) : base(e, canceled, state)
        {
            this.Result = value;
            this.TimeTaken = TimeTaken;
            this.Sender = sender;
        }
    }

}
