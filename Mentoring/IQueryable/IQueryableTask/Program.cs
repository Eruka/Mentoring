using IQueryableTask.Client;
using IQueryableTask.Provider;
using System;
using System.Linq;

namespace IQueryableTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var entities = new LinqToYahooDataSet<Quote>();

            foreach (var entity in entities.Where(e => e.Symbol == "EPAM" && e.StartDate == "2015-09-22" && e.EndDate == "2015-09-25"))
            {
                Console.WriteLine(entity);
            }

            Console.ReadKey();
        }
    }
}
