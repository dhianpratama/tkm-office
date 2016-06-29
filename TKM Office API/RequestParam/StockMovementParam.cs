namespace TKM_Office_API.RequestParam
{
    public class StockMovementParam
    {
        public long BinId { get; set; } 
        public int MovementQty { get; set; }
        public int CurrentQty { get; set; }
    }
}