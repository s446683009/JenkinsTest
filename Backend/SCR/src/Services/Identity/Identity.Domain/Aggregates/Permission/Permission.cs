using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Aggregates.Permission
{
    public class Permission:BaseEntity,IAggregateRoot
    {
        public  Permission(string name,int parentId)
        {
            this.Name = name;
            this.ParentId = parentId;
            this.Actions = new List<PermissionAction>();
        }

        public int ParentId { get;private set; }
        public int PermissionId { get;  set; }
        public string Name { get; private set; }
 
        public virtual ICollection<PermissionAction> Actions { get; private set; }
        public virtual Permission Parent { get;private set; }

        public void ChangeName(string name)
        {
            this.Name = name;
  
           
        }

        public void ChangeParentId(int parentId)
        {
            this.ParentId = parentId;
          
        }

        public void ChangePermission(IEnumerable<string> actions)
        {  
            this.Actions.Clear();
            this.Actions = actions.Select(t=>new PermissionAction()
            {
                Action = t
            }).ToList();
            
        }
    }
    public class PermissionAction {
       public int PermissionId { get; set; }
       public string Action { get; set; }
    }
}
