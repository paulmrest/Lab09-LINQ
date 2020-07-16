using System;
using System.Collections.Generic;
using System.Text;

namespace Lab09_LINQinManhattan.Classes
{
    public class Property
    {
        public string Zip { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string Borough { get; set; }
        public string Neighborhood { get; set; }
        public string County { get; set; }

        public override string ToString()
        {
            return Neighborhood;
        }
    }
}
