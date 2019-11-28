using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAPCalculatepi
{
    public delegate void CalculatepiCompletedEventHandler(object sender, CalculatepiCompletedEventArgs e);
    public class Calculatepi
    {
        //Completion event handler
        public event CalculatepiCompletedEventHandler CalculatepiCompleted;

        //delegate to call the synchronous operation in async way (in this case we are using task.run)
        private delegate void CalculationEventHandler(int numSteps, AsyncOperation asyncOp);

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

        //Dictionary to handle multiple tasks invocation, uniquely identify each operation as one dictionary element
        private HybridDictionary parallelTasks = new HybridDictionary();

        /// <summary>
        /// Calculate pi async
        /// </summary>
        public void CalculatepiAsync(int numsteps, object operationState)
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
            Task.Run(() => worker(numsteps, asyncOp));
        }

        /// <summary>
        /// Synchronous method to calculate pi
        /// </summary>
        void Calculatepivalue(int numsteps, AsyncOperation asyncOp)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            numsteps++;

            //Variables used in calculation of Pi
            uint[] value = new uint[numsteps * 10 / 3 + 2];
            uint[] rem = new uint[numsteps * 10 / 3 + 2];

            uint[] pi = new uint[numsteps];

            for (int j = 0; j < value.Length; j++)
                value[j] = 20;

            //Simple looping logic to calculate Pi till the number of characters passed as input
            for (int i = 0; i < numsteps; i++)
            {
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

                Thread.Sleep(10);
            }
            result = result.Substring(0, 1) + "." + result.Substring(1, result.Length - 1);

            lock (parallelTasks.SyncRoot)
            {
                parallelTasks.Remove(asyncOp.UserSuppliedState);
            }

            //raise callback
            CalculatepiCompletedEventArgs e = new CalculatepiCompletedEventArgs(result, timer.ElapsedMilliseconds, null, null, false, asyncOp.UserSuppliedState);
            asyncOp.PostOperationCompleted(onCompletedDelegate, e);
            timer.Stop();
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
