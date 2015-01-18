using System;
using System.Collections.Generic;
using FizzWare.NBuilder;

namespace OptimizeMatchData
{
    public class SpotProgramme
    {    
        public String Name { get; set; }
        public int SpotNumber { get; set; }

        public override string ToString()
        {
            return string.Format("SpotNumber: {0}, Name: {1}", this.SpotNumber, this.Name);
        }

        public static IEnumerable<SpotProgramme> GenerateSpotProgrammes(int size)
        {
            var idGenerator = new SequentialGenerator<int>
            {
                Direction = GeneratorDirection.Descending,
                Increment = 2
            };
            idGenerator.StartingWith(size * 2);
            IList<SpotProgramme> spotProgrammes = Builder<SpotProgramme>.CreateListOfSize(size)
                .WhereAll().Have(sp =>
                {
                    sp.SpotNumber = idGenerator.Generate();
                    sp.Name = "Programme " + sp.SpotNumber;
                    return sp;
                }
                )
                
                .Build();
            return spotProgrammes;
        }
    }
}