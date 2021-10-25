using System;
using System.Collections.Generic;
using System.Text;

namespace SCR.Models
{
    public class StudentEntity:BaseEntity
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsEnabled { get; set; }
        
        public override void Create()
        {
            this.StudentId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
        }


    }
}
