using System;
using System.Threading;

namespace AsyncExercise
{
    public class Server
    {

        /* Return [amount] random numbers between [min] and [max] inclusive */
        public void GetNumbers(int amount, int min, int max, ICallback cb)
        {
            var t = new Thread(() => DoTheRealStuff(amount, min, max, cb) );
            t.Start();
        }

        private void DoTheRealStuff(int amount, int min, int max, ICallback cb) {
            Thread.Sleep(10000);
            int[] res = new int[amount];
            Random r = new Random();
            int count = 0;
            while (count < amount)
            {
                int x = r.Next(max - min + 1) + min;
                res[count++] = x;
            }
            cb.WhenResultReceived(res);
        }



    }
}
