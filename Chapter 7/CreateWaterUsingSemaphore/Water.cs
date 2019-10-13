using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CreateWaterUsingSemaphore
{
    public class Water
    {
        Semaphore semaphoreH = new Semaphore(2, 2);
        Semaphore semaphoreO = new Semaphore(0, 1);

        void ReleaseHydrogen()
        {
            Console.WriteLine("H");
        }

        void ReleaseOxygen()
        {
            Console.WriteLine("O");
        }

        int hCount = 0;

        public async Task HThread(Action releaseH)
        {
            await Task.Delay(1);
            //Wait on Hydrogen thread, code after this will be blocked after processing two Hydrogen threads until one Oxygen thread is processed
            semaphoreH.WaitOne();

            releaseH();

            hCount++;
            if (hCount % 2 == 0) //For every two Hydrogen threads releasing Oxygen semaphore to process Oxygen method.
            {
                semaphoreO.Release();
            }
        }

        public async Task OThread(Action releaseO)
        {
            await Task.Delay(1);
            //Locking on Oxygen semaphore, this will allow to be processed only when 2 Hydrogen threads are processed or else will wait. 
            //Code after this is blocked until two Hydrogen threads are processed as initial concurrent threads for Oxygen semaphore is 0 (first parameter)
            semaphoreO.WaitOne();

            releaseO();

            semaphoreH.Release(2); //Exiting Hydrogen semaphore twice, allowing two Hydrogen to be processed
        }

        public async Task BuildWaterAsync(string input)
        {
            List<Task> tasks = new List<Task>();
            foreach (char c in input)
            {

                switch (c)
                {
                    case 'O':
                        tasks.Add(OThread(ReleaseOxygen));
                        break;
                    case 'H':
                        tasks.Add(HThread(ReleaseHydrogen));
                        break;
                    default:
                        break;
                }
            }
            await Task.WhenAll(tasks);
        }

    }
}
