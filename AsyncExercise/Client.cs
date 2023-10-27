using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AsyncExercise
{
    public class Client
    {
        private readonly Server mServer;

        public Client(Server server){
            mServer = server;
        }       

        public void run() {
            List<Task> mTasks = new();

            while (true)
            {
                int number = GetNumber("Enter a number (0 for stop): ");

                if (number == 0) break;

                Task t = new Task(() =>
                                    WriteToFile(mServer.GetNumbers(number, 1, 6)));

                t.Start();

                mTasks.Add(t);
            }
            // vent på at alt er færdigt
            foreach (var t in mTasks)
                t.Wait();

            //fortæl brugeren vi er færdige
            Console.WriteLine("Done...");
            Console.ReadKey();

        }

        private int GetNumber(String text)
        {
            Console.WriteLine(text);
            return int.Parse(Console.ReadLine());
        }

        private async void WriteToFileAsync(int[] numbers) {
            var t = new Task(() => WriteToFile(numbers));
            await t;
        }

        private void WriteToFile(int[] numbers)
        {
            string path = @"fromclient.txt";

            StringBuilder sb = new StringBuilder();

            foreach (var x in numbers)
                sb.Append(x.ToString() + ", ");

            sb.Remove(sb.Length - 2, 2);

            StreamWriter sw = File.AppendText(path);

            sw.WriteLine(sb.ToString());

            sw.Flush();
        }

    }
}
