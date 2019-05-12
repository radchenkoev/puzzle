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
            //list.Add(new Call { Id = 1, BeginCall = new DateTime(2015, 03, 31, 08, 05, 00), EndCall = new DateTime(2015, 03, 31, 08, 25, 00) });
            //list.Add(new Call { Id = 2, BeginCall = new DateTime(2015, 03, 31, 08, 10, 00), EndCall = new DateTime(2015, 03, 31, 09, 25, 00) });
            //list.Add(new Call { Id = 3, BeginCall = new DateTime(2015, 03, 31, 08, 20, 00), EndCall = new DateTime(2015, 03, 31, 09, 00, 00) });
            //list.Add(new Call { Id = 4, BeginCall = new DateTime(2015, 03, 31, 08, 35, 00), EndCall = new DateTime(2015, 03, 31, 10, 35, 00) });
            //list.Add(new Call { Id = 5, BeginCall = new DateTime(2015, 03, 31, 08, 40, 00), EndCall = new DateTime(2015, 03, 31, 09, 30, 00) });
            //list.Add(new Call { Id = 6, BeginCall = new DateTime(2015, 03, 31, 08, 45, 00), EndCall = new DateTime(2015, 03, 31, 09, 10, 00) });
            //list.Add(new Call { Id = 7, BeginCall = new DateTime(2015, 03, 31, 09, 35, 00), EndCall = new DateTime(2015, 03, 31, 10, 15, 00) });
            //list.Add(new Call { Id = 8, BeginCall = new DateTime(2015, 03, 31, 10, 20, 00), EndCall = new DateTime(2015, 03, 31, 10, 25, 00) });
            //list.Add(new Call { Id = 9, BeginCall = new DateTime(2015, 03, 31, 10, 30, 00), EndCall = new DateTime(2015, 03, 31, 10, 40, 00) });
            //list.Add(new Call { Id = 10, BeginCall = new DateTime(2015, 03, 31, 10, 35, 00), EndCall = new DateTime(2015, 03, 31, 10, 45, 00) });

            list.Add(new Call { Id = 1, BeginCall = new DateTime(2015, 03, 31, 08, 25, 00), EndCall = new DateTime(2015, 03, 31, 08, 50, 00) });
            list.Add(new Call { Id = 2, BeginCall = new DateTime(2015, 03, 31, 08, 05, 00), EndCall = new DateTime(2015, 03, 31, 08, 25, 00) });
            list.Add(new Call { Id = 3, BeginCall = new DateTime(2015, 03, 31, 08, 50, 00), EndCall = new DateTime(2015, 03, 31, 10, 00, 00) });
            list.Add(new Call { Id = 4, BeginCall = new DateTime(2015, 03, 31, 08, 40, 00), EndCall = new DateTime(2015, 03, 31, 09, 00, 00) });
            list.Add(new Call { Id = 5, BeginCall = new DateTime(2015, 03, 31, 09, 10, 00), EndCall = new DateTime(2015, 03, 31, 09, 30, 00) });
            list.Add(new Call { Id = 6, BeginCall = new DateTime(2015, 03, 31, 09, 45, 00), EndCall = new DateTime(2015, 03, 31, 11, 00, 00) });
            list.Add(new Call { Id = 7, BeginCall = new DateTime(2015, 03, 31, 09, 20, 00), EndCall = new DateTime(2015, 03, 31, 09, 50, 00) });
            

            var dic = new Dictionary<int, List<Call>>();

            list.ForEach(x => {
                dic.Add(x.Id, list.Where(y => y.BeginCall <= x.EndCall && y.EndCall >= x.EndCall).OrderBy(z => z.Id).ToList());
            });

            var maxCalls = dic
                .Select(x => x.Value.Count)
                .Max();

            var callsInMaxChains = dic
                .Where(x => x.Value.Count == maxCalls)
                .Select(x => x.Value)
                .ToList();

            callsInMaxChains.ForEach(x => {
                Console.WriteLine(string.Join("-", x.Select(y => y.Id)));
                Console.WriteLine("BeginPeriod: {0} - EndPeriod: {1}", x.Max(y => y.BeginCall), x.Min(y => y.EndCall));
            });
        }
    }
}
