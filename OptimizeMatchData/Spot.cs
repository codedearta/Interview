namespace OptimizeMatchData
{
    public class Spot
    {
        public int SpotNumber { get; set; }
        public int Length { get; set; }

        public override string ToString()
        {
            return string.Format("SpotNumber: {0}, Length: {1}", this.SpotNumber, this.Length);
        }
    }
}