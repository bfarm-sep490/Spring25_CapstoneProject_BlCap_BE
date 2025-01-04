namespace Spring25.BlCapstone.BE.APIs.RequestModels.Fertilizer
{
    public class UpdatedFertilizer
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string NutrientContent { get; set; }
        public string StorageConditions { get; set; }
        public int Quantity { get; set; }
        public int Unit { get; set; }
        public DateOnly ExpriredDate { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public int FarmOrnerId { get; set; }
    }
}
