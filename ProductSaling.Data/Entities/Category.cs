namespace ProductSaling.Data.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public virtual IList<Product> Products { get; set; }
    }
}
