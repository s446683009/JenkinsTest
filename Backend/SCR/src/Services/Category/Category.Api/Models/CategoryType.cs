namespace Category.Api.Models;

public class CategoryType
{
    public string Name { get; set; }
    public long Id { get; set; }
    public long? ParentId { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Description { get; set; }
    public virtual CategoryType Parent { get; set; }
    public virtual ICollection<CategoryType> Children { get; set; }
}