using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Models
{
    public class PlannerList
    {
        public string Heading { get; set; }
        public string Description { get; set; }
        public PlannerList(string heading, string description) { Heading = heading; Description = description; }
    }
}
