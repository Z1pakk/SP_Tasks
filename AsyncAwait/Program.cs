using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {
        static async Task Main(string[] args)
        {
            double a = Counter(1, 2);
            double b = await CounterAsync(1, 2);
        }
        static Task<double> CounterAsync(int a, int b)
        {
            return Task.Run(() => Counter(a, b));
        }

        static double Counter(int a, int b)
        {
            return Math.Pow(a, b);
        }
    }
}
