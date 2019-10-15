using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeLoanVerificationSimulatorUsingBarrier
{
    public class HomeLoan
    {
        const int minScore = 150;
        const int maxScore = 1000;
        Barrier barrier;
        public bool HomeLoanStatus { get; set; }
        bool creditScoreStatus, socialScoreStatus;

        public HomeLoan(int numberOfParticipants)
        {
            barrier = new Barrier(numberOfParticipants, (myBarrier) =>
            {
                Console.WriteLine($"========================================================");
                Console.WriteLine($"Phase {barrier.CurrentPhaseNumber} finished for all sources");
                Console.WriteLine($"========================================================");
            });
            this.HomeLoanStatus = creditScoreStatus = socialScoreStatus = true;
        }

        /// <summary>
        /// Method to simulate random credit score
        /// </summary>
        /// <returns></returns>
        int GetCreditScore()
        {
            Random rnd = new Random();
            return rnd.Next(minScore, maxScore);
        }

        /// <summary>
        /// Method to simulate random credit score
        /// </summary>
        /// <returns></returns>
        int GetSocialScore()
        {
            Random rnd = new Random();
            return rnd.Next(minScore, maxScore);
        }

        public async Task HomeanAnalyzerAsync(string sourceName)
        {
            await Task.Factory.StartNew(() =>
            {
                // Start of phase 0
                Console.WriteLine($"Credit score evaluation, phase {barrier.CurrentPhaseNumber}, from source {sourceName} started");
                int creditScore;
                creditScore = GetCreditScore();
                if (creditScore < 200 && creditScoreStatus)
                {
                    creditScoreStatus = false;
                }
                // Signal the barrier
                barrier.SignalAndWait();
                // start of phase 1
                Console.WriteLine($"Social score evaluation, phase {barrier.CurrentPhaseNumber}, from source {sourceName} started");
                if (!creditScoreStatus)
                {
                    Console.WriteLine($"Bad credit score from source {sourceName}");
                    this.HomeLoanStatus = false;
                }
                else
                {
                    int socialScore;
                    socialScore = GetSocialScore();
                    if (socialScore < 200 && socialScoreStatus)
                    {
                        Console.WriteLine($"Bad social score from source {sourceName}");
                        socialScoreStatus = false;
                        this.HomeLoanStatus = false;
                    }
                }
                //signal the barrier
                barrier.SignalAndWait();
            });
        }


    }
}
