using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FizzWare.NBuilder;

namespace Interview
{
    class Program
    {
        static void Main(string[] args)
        {
            var basicSpots = Builder<Spot>.CreateListOfSize(20000).Build();
            var spotProgrammes = GenerateSpotProgrammes();

            var stopWatch = Stopwatch.StartNew();
            var spotSummaries = 
                basicSpots.Select(basicSpot => new SpotSummary(basicSpot,spotProgrammes.SingleOrDefault(spotProg =>spotProg.SpotNumber == basicSpot.SpotNumber))).ToList();


            
            //spotProgrammes.ToList().ForEach(spot => Console.Out.WriteLine(spot));
            stopWatch.Stop();
            Console.Out.WriteLine(spotSummaries.Last());
            Console.Out.WriteLine(stopWatch.Elapsed);

            Console.ReadLine();
        }

        private static IEnumerable<SpotProgramme> GenerateSpotProgrammes()
        {
            var idGenerator = new SequentialGenerator<int>
            {
                Direction = GeneratorDirection.Descending,
                Increment = 20000
            };
            idGenerator.StartingWith(0);
            IList<SpotProgramme> spotProgrammes = Builder<SpotProgramme>.CreateListOfSize(10000)
                .All().With(sp => sp.SpotNumber = idGenerator.Generate())
                .Build();
            return spotProgrammes;
        }
    }
}
