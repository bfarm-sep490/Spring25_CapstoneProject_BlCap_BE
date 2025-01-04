﻿namespace Spring25.BlCapstone.BE.APIs.RequestModels.Pesticide
{
    public class UpdatedPesticide
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Purpose { get; set; }
        public string SafetyInstructions { get; set; }
        public string ReEntryPeriod { get; set; }
        public int Quantity { get; set; }
        public int Unit { get; set; }
        public DateOnly ExpriredDate { get; set; }
        public string Status {  get; set; }
        public string CreatedBy { get; set; }
        public int FarmOrnerId { get; set; }
    }
}
