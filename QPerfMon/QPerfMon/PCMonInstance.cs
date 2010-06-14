using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace QPerfMon
{
    [Serializable]
    public class PCMonInstance
    {
        public PCMonInstance()
        {
            Scale = 1;
            LastError = "";
        }
        public PCMonInstance(string name)
        {
            Name = name;
            Scale = 1;
            LastError = "";
        }
        private string name;
        public string Name 
        {
            get { return name; }
            set { name = value; }
        }
        private string machine;
        public string Machine
        {
            get { return machine; }
            set { machine = value; }
        }
        private string category;
        public string Category
        {
            get { return category; }
            set { category = value; }
        }
        private string counter;
        public string Counter
        {
            get { return counter; }
            set { counter = value; }
        }
        private string instance;
        public string Instance
        {
            get { return instance; }
            set { instance = value; }
        }
        private double scale;
        public double Scale 
        {
            get { return scale; }
            set { scale = value; }
        }
     
        private PerformanceCounter pcInstance = null;
        public PerformanceCounter PCInstance 
        {
            get { return pcInstance; }
            set { pcInstance = value; }
        }
      
        private string lastError;
        public string LastError
        {
            get { return lastError; }
            set { lastError = value; }
        }

        public float LastValue { get; set; }
        public bool Selected { get; set; }
    }
}
