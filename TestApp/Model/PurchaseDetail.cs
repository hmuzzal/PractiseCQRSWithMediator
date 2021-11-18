namespace TestApp.Model
{
    public class PurchaseDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public int TotalAmount { get; set; }
        public int PurchaseId { get; set; }
        public virtual Purchase Purchase { get; set; }
    }
}
