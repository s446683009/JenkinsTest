using System;
using System.Collections.Generic;
using System.Text;

namespace SCR.Models
{
    public class BaseEntity
    {
        public DateTime? CreateDate { get; set; }
        public virtual void Create() {

        }
        public virtual void Modify() { 
        
        }
    }
}
