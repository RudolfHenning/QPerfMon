using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Drawing;

namespace QPerfMon
{
    [Serializable]
    public class PCMonInstance : IComparable
    {
        #region Constructors
        public PCMonInstance()
        {
            Scale = 1;
            LastError = "";
        }
        public PCMonInstance(string key)
        {
            Name = name;
            string[] parts = key.Split('\\');
            if (parts.Length >= 4)
            {
                machine = GetParsedElement(0, parts);
                category = GetParsedElement(1, parts);
                counter = GetParsedElement(2, parts);
                instance = GetParsedElement(3, parts);
                string scaleStr = GetParsedElement(4, parts);
                if (scaleStr.Length > 0)
                    scale = double.Parse(scaleStr);
                else
                    scale = 1;

                name = string.Format("{0}\\{1}\\{2}\\", machine, category, counter);
                if (instance.Contains("\\"))
                    name += "\"" + instance + "\"";
                else
                    name += instance;
                string styleStr = GetParsedElement(5, parts);
                if (styleStr.Length > 0)
                {
                    if (!int.TryParse(styleStr, out plotStyle))
                        plotStyle = 0;
                }
            }                
            else
            {
                throw new Exception("Invalid name/key specified for PCMonInstance");
            }
            Selected = false;
            LastError = "";
        }
        #endregion

        #region GetParsedElement
        /// <summary>
        /// This function returns the logical element numbered 'elementNo' 
        /// This is to cater for when an element spans two array elements because it contained a '\' inside the name
        /// To indicate spanning double quotes ("...") are used around the logical element
        /// </summary>
        /// <param name="elementNo">Logical element number to return</param>
        /// <param name="element">Array of split strings</param>
        /// <returns>The logical element for that position</returns>
        private string GetParsedElement(int elementNo, string[] element)
        {
            int pos = 0;
            bool spanning = false;
            string output = "";
            for (int i = 0; i < element.Length; i++)
            {
                output += (spanning ? "\\" : "") + element[i].Replace("\"", "");

                if (!spanning && element[i].StartsWith("\""))
                    spanning = true;
                if (element[i].EndsWith("\""))
                    spanning = false;
                if (!spanning)
                {
                    if (pos == elementNo)
                        break;
                    pos++;
                    output = "";
                }
            }
            return output;
        } 
        #endregion

        #region Properties
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
        private int plotStyle;
        public int PlotStyle
        {
            get { return plotStyle; }
            set { plotStyle = value; }
        }
        private Color plotColor;
        public Color PlotColor
        {
            get { return plotColor; }
            set { plotColor = value; }
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
        #endregion

        public void CreatePCInstance()
        {
            pcInstance = new PerformanceCounter(category, counter, instance, machine);
        }

        #region IComparable Members
        public string CounterDefinition()
        {
            return this.machine + "\\" + this.category + "\\" + this.counter + "\\" + this.instance;
        }
        public int CompareTo(object obj)
        {
            PCMonInstance otherInstance = (PCMonInstance)obj;
            return CounterDefinition().CompareTo(otherInstance.CounterDefinition());
        }
        public override bool Equals(object obj)
        {
            PCMonInstance otherInstance = (PCMonInstance)obj;
            return CompareTo(otherInstance) == 0;// CounterDefinition().Equals(otherInstance.CounterDefinition()); 
        }
        public override int GetHashCode()
        {
            return CounterDefinition().GetHashCode();
        }
        #endregion
    }
}
