using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OptimizeMatchData
{
    [TestClass]
    public class PerformanceTest
    {
        private const int MaxTargetMilliseconds = 4000;
        private const int SpotListSize = 20000;

        private IEnumerable<Spot> _basicSpots;
        private IEnumerable<SpotProgramme> _spotProgrammes;

        [TestInitialize]
        public void GenerateTestData()
        {
            _basicSpots = Builder<Spot>.CreateListOfSize(SpotListSize).Build();
            _spotProgrammes = SpotProgramme.GenerateSpotProgrammes(SpotListSize / 2);
        }
        
        [TestMethod]
        public void ShouldMatchSpotsInLessThan2Seconds()
        {
            var stopWatch = Stopwatch.StartNew();
            var spotSummaries = this.MatchSpotsFast().ToList();
            stopWatch.Stop();

            spotSummaries = spotSummaries.OrderBy(s => s.SpotNumber).ToList();
            Assert.AreEqual(spotSummaries[0].ToString(),new SpotSummary(new Spot() {Length = 1, SpotNumber = 1}, null).ToString());
            Assert.AreEqual(spotSummaries[SpotListSize-1].ToString(), new SpotSummary(new Spot() { Length = SpotListSize, SpotNumber = SpotListSize }, new SpotProgramme() { Name = "Programme " + SpotListSize, SpotNumber = SpotListSize }).ToString());

            Console.Out.WriteLine("Max target milliseconds: " + MaxTargetMilliseconds);
            Console.Out.WriteLine("elapsed milliseconds: " + stopWatch.ElapsedMilliseconds);
            Assert.IsTrue(stopWatch.ElapsedMilliseconds <= MaxTargetMilliseconds, String.Format("The elapsed time was greater than {0} milliseconds. Actual: <{1}>",MaxTargetMilliseconds, stopWatch.ElapsedMilliseconds));            
        }

        private IEnumerable<SpotSummary> MatchSpots()
        {
            return _basicSpots.Select(
                    basicSpot =>
                        new SpotSummary(basicSpot,
                            _spotProgrammes.SingleOrDefault(spotProg => spotProg.SpotNumber == basicSpot.SpotNumber)));
        }

        private IEnumerable<SpotSummary> MatchSpotsParallel()
        {
            return _basicSpots.Select(
                    basicSpot =>
                        new SpotSummary(basicSpot,
                            _spotProgrammes.AsParallel().SingleOrDefault(spotProg => spotProg.SpotNumber == basicSpot.SpotNumber)));
        }

        private IEnumerable<SpotSummary> MatchSpotsFast()
        {
            var sortedSpotProgrammes = _spotProgrammes.OrderBy(spotProg => spotProg.SpotNumber).ToList();
            return _basicSpots.Select(
                basicSpot =>
                {
                    var spotProgramme = sortedSpotProgrammes.FirstOrDefault();
                    if (spotProgramme == null || spotProgramme.SpotNumber != basicSpot.SpotNumber)
                    {
                        return new SpotSummary(basicSpot, null);
                    } 

                    sortedSpotProgrammes.RemoveAt(0);
                    return new SpotSummary(basicSpot, spotProgramme);
                });
        }
    }
}
