using Spring25.BlCapstone.BE.Repositories.Models;

namespace Spring25.BlCapstone.BE.APIs.RequestModels.Pesticide
{
    public class CreatedPesticide
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Purpose { get; set; }
        public string SafetyInstructions { get; set; }
        public string ReEntryPeriod { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string CreatedBy { get; set; }
        public int FarmOwnerId { get; set; }
    }
}
