namespace OptimizeMatchData
{
    public class SpotSummary
    {
        public int SpotNumber { get; set; }
        public Spot BasicSpot { get; set; }
        public SpotProgramme SpotProgramme { get; set; }

        public SpotSummary(Spot basicSpot, SpotProgramme spotProgramme)
        {
            this.SpotNumber = basicSpot.SpotNumber;
            this.BasicSpot = basicSpot;
            this.SpotProgramme = spotProgramme;
        }

        public override string ToString()
        {
            return string.Format("\r\nSpotSummary-> StpotNumber: {0}\r\nBasicSpot-> {1}\r\nSpotProgramme-> {2}",this.SpotNumber, this.BasicSpot, this.SpotProgramme);
        }
    }
}