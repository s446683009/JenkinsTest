
namespace Services.Common;

public class Tree
{

    public string Name { get; set; }
    public string Id { get; set; }
    public string ParentId { get; set; }
    public bool HasChildren { get; set; }
    public IEnumerable<Tree> Children { get; set; }

    public static List<Tree> GetTrees(List<Tree> data, string parentId)
    {
        var list = data.Where(t => t.ParentId == parentId).ToList();
        foreach (var sub in list)
     
        {
            sub.HasChildren = data.Any(t=>t.ParentId==sub.Id);
            if (sub.HasChildren)
            {
                sub.Children=GetTrees(data,sub.Id);
            }
        }

        return list;

    }
}