using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entity
{
    public class Group : IEntity
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public List<int> StudentsId { get; set; }
    }
}
