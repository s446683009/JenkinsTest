using System;
using System.Collections.Generic;
using System.Text;

namespace SCR.Models.Models
{
    public class CourseEntity:BaseEntity
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public override void Create()
        {
            this.CourseId = Guid.NewGuid().ToString();
        }
    }
}
