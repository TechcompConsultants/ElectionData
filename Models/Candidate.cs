namespace ElectionData.Models
{
    public class Candidate
    {
        public string CandidateName { get; set; }
        public string PartyOrAlliance { get; set; }
        public int Votes { get; set; }
        public decimal Percentage { get; set; }  // This holds the raw percentage value
        public string FormattedPercentage { get; set; }  // Add this line for the formatted value
    }
}
