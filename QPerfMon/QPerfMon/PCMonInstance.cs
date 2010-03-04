using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace QPerfMon
{
    public class PCMonInstance
    {
        public PCMonInstance(string name)
        {
            Name = name;
            Scale = 1;
        }
        public string Name { get; set; }
        public string Machine { get; set; }
        public string Category { get; set; }
        public string Counter { get; set; }
        public string Instance { get; set; }
        public double Scale { get; set; }
        public PerformanceCounter PCInstance { get; set; }
    }
}
