using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Domain
{
    public class BaseEntity
    {
        public DateTime CreatedTime { get; protected set; }
        public DateTime? ModifiedTime { get; protected set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsDeleted { get; protected set; }
        public virtual bool SetDelete() {
            this.IsDeleted = true;
            return true;
        }
    }
}
