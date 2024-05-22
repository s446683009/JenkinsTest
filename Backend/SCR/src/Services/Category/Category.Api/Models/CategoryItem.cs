namespace Category.Api.Models;

public class CategoryItem
{
    public string Name { get; set; }
    public long Id { get; set; }
    public CategoryType Type { get; set; }
    public long TypeId { get; set; }
    public decimal Price { get; set; }
    public string[] ImgUrl { get; set; }
    
}