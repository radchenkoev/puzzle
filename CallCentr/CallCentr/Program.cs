using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCentr
{
    //Есть логи звонков — время начала и завершения,
    //и нужно составить алгоритм, 
    //который выводил бы на экран максимальное число одновременных звонков.
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<Call>();
            list.Add(new Call { Id = 1, BeginCall = new DateTime(2015, 03, 31, 08, 05, 00), EndCall = new DateTime(2015, 03, 31, 08, 25, 00) });
            list.Add(new Call { Id = 2, BeginCall = new DateTime(2015, 03, 31, 08, 10, 00), EndCall = new DateTime(2015, 03, 31, 09, 25, 00) });
            list.Add(new Call { Id = 3, BeginCall = new DateTime(2015, 03, 31, 08, 20, 00), EndCall = new DateTime(2015, 03, 31, 09, 00, 00) });
            list.Add(new Call { Id = 4, BeginCall = new DateTime(2015, 03, 31, 08, 35, 00), EndCall = new DateTime(2015, 03, 31, 10, 35, 00) });
            list.Add(new Call { Id = 5, BeginCall = new DateTime(2015, 03, 31, 08, 40, 00), EndCall = new DateTime(2015, 03, 31, 09, 30, 00) });
            list.Add(new Call { Id = 6, BeginCall = new DateTime(2015, 03, 31, 08, 45, 00), EndCall = new DateTime(2015, 03, 31, 09, 10, 00) });
            list.Add(new Call { Id = 7, BeginCall = new DateTime(2015, 03, 31, 09, 35, 00), EndCall = new DateTime(2015, 03, 31, 10, 15, 00) });
            list.Add(new Call { Id = 8, BeginCall = new DateTime(2015, 03, 31, 10, 20, 00), EndCall = new DateTime(2015, 03, 31, 10, 25, 00) });
            list.Add(new Call { Id = 9, BeginCall = new DateTime(2015, 03, 31, 10, 30, 00), EndCall = new DateTime(2015, 03, 31, 10, 40, 00) });
            list.Add(new Call { Id = 10, BeginCall = new DateTime(2015, 03, 31, 10, 35, 00), EndCall = new DateTime(2015, 03, 31, 10, 45, 00) });

            var dic1 = new Dictionary<int, List<Call>>();
            //var dic2 = new Dictionary<int, List<Call>>();

            list.ForEach(x =>
            {
                dic1.Add(x.Id, list.Where(y => y.BeginCall <= x.EndCall && y.EndCall >= x.EndCall).OrderBy(z => z.Id).ToList());
                //dic2.Add(x.Id, list.Where(y => y.BeginCall <= x.BeginCall && y.EndCall >= x.BeginCall).OrderBy(z => z.Id).ToList());
            });

            var max1 = dic1
                .Select(x => x.Value.Count)
                .Max();

            //var max2 = dic2
            //    .Select(x => x.Value.Count)
            //    .Max();

            var maxChains1 = dic1
                .Where(x => x.Value.Count == max1)
                .Select(x => x.Value)
                .ToList();

            //var maxChains2 = dic2
            //    .Where(x => x.Value.Count == max2)
            //    .Select(x => x.Value)
            //    .ToList();


            maxChains1.ForEach(x => 
                {
                    x.ForEach(y => Console.Write("-{0}", y.Id));
                    Console.WriteLine();
                    Console.WriteLine("{0} - {1}", x.Max(y => y.BeginCall), x.Min(y => y.EndCall));
                });
                
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
