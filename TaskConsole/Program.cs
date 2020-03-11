using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskConsole
{
    class Program
    {
        static CancellationTokenSource tokenSource;
        static CancellationToken cancellationToken;

        static void Main(string[] args)
        {
            tokenSource = new CancellationTokenSource();
            cancellationToken = tokenSource.Token;

            Action action = new Action(EasyWork);

            Task ts = new Task(action,cancellationToken);
            ts.Start();
            ts.Wait();

            var taskResult = Task.Run(action);
            Thread.Sleep(500);
            tokenSource.Cancel();

            var taskResult2 = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("Hello world from Run Task");
                }
            });

            taskResult2.Wait();

            var taskResult3 = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("Hello world from Run Task");
                }
                return true;
            });

            taskResult3.Wait();
            Console.WriteLine(taskResult3.Result);

            var taskresult4 = Task.Run(() => Counter(4, 5));
            taskresult4.Wait();
            Console.WriteLine(taskresult4.Result);
        }

        static void EasyWork()
        {
            for (int i = 0; i < 10; i++)
            {
                if(cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                Console.WriteLine($"Easy {i} Hello World!!!");
                Thread.Sleep(100);
                Console.WriteLine($"Current Th={Thread.CurrentThread.ManagedThreadId}");
            }
        }

        static double Counter(double a, double b) => a + b;
    }
}
