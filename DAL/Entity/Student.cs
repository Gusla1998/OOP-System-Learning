using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entity
{
    public class Student : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double AvgMark { get; set; }
        public int Group { get; set; }
    }
}
