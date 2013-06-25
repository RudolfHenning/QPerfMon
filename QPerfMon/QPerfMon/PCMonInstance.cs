using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.Xml;

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
            if (key.StartsWith("<"))
            {
                XmlDocument config = new XmlDocument();
                config.LoadXml(key);
                XmlElement root = config.DocumentElement;
                XmlNode machineNode = root.SelectSingleNode("machine");
                if (machineNode == null)
                    throw new Exception(string.Format("Invalid config specified! Machine name not specified\r\n{0}", key));
                else 
                    machine = machineNode.InnerText;
                XmlNode categoryNode = root.SelectSingleNode("category");
                if (categoryNode == null)
                    throw new Exception(string.Format("Invalid config specified! Category not specified\r\n{0}", key));
                else
                    category = categoryNode.InnerText;
                XmlNode counterNode = root.SelectSingleNode("counter");
                if (counterNode == null)
                    throw new Exception(string.Format("Invalid config specified! Counter not specified\r\n{0}", key));
                else
                    counter = counterNode.InnerText;
                XmlNode instanceNode = root.SelectSingleNode("instance");
                if (instanceNode == null)
                    instance = "";
                else
                    instance = instanceNode.InnerText;
                XmlNode scaleNode = root.SelectSingleNode("scale");
                if (scaleNode == null)
                    scale = 1;
                else if (!double.TryParse(scaleNode.InnerText, out scale))
                    scale = 1;
                XmlNode styleNode = root.SelectSingleNode("style");
                if (styleNode == null)
                    plotStyle = 0;
                else if (!int.TryParse(styleNode.InnerText, out plotStyle))
                    plotStyle = 0;
                XmlNode dashNode = root.SelectSingleNode("dash");
                if (dashNode == null)
                    dashStyle = 0;
                else if (!int.TryParse(dashNode.InnerText, out dashStyle))
                    dashStyle = 0;
                LoadColorError = false;
                XmlNode colorNode = root.SelectSingleNode("color");
                if (colorNode == null)
                    LoadColorError = true;
                else
                {
                    try
                    {
                        if (colorNode.InnerText.StartsWith("#"))
                        {
                            plotColor = Color.FromArgb(int.Parse(colorNode.InnerText.Substring(1)));
                        }
                        else
                            plotColor = Color.FromName(colorNode.InnerText);
                        if (plotColor.R == 0 && plotColor.B == 0 && plotColor.G == 0)
                            LoadColorError = true;
                    }
                    catch
                    {
                        LoadColorError = true;
                    }
                }
                

                //Set name
                name = string.Format("{0}\\{1}\\{2}\\", machine, category, counter);
                if (instance.Contains("\\"))
                    name += "\"" + instance + "\"";
                else
                    name += instance;
            }
            else
            {
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
                    string colorStr = GetParsedElement(6, parts);
                    if (colorStr.Length > 0)
                    {
                        try
                        {
                            plotColor = Color.FromName(colorStr);
                            LoadColorError = false;
                        }
                        catch { LoadColorError = true; }
                    }
                    else
                        LoadColorError = true;
                }
                else
                {
                    throw new Exception("Invalid name/key specified for PCMonInstance");
                }
            }
            Selected = false;
            LastError = "";
        }
        public void SetName()
        {
            name = string.Format("{0}\\{1}\\{2}\\", machine, category, counter);
            if (instance.Contains("\\"))
                name += "\"" + instance + "\"";
            else
                name += instance;
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
        private int dashStyle;
        public int DashStyle
        {
            get { return dashStyle; }
            set { dashStyle = value; }
        }
        [System.Xml.Serialization.XmlIgnore]
        public bool LoadColorError { get; private set; }
        private Color plotColor;
        public Color PlotColor
        {
            get { return plotColor; }
            set { plotColor = value; }
        }

        private PerformanceCounter pcInstance = null;
        [System.Xml.Serialization.XmlIgnore]
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

        public string KeyToXml()
        {
            XmlDocument config = new XmlDocument();
            config.LoadXml("<xml />");
            XmlElement root = config.DocumentElement;
            root.AppendChild(root.CreateElementWithText("machine", machine));
            root.AppendChild(root.CreateElementWithText("category", category));
            root.AppendChild(root.CreateElementWithText("counter", counter));
            root.AppendChild(root.CreateElementWithText("instance", instance));
            if (scale != 1)
                root.AppendChild(root.CreateElementWithText("scale", scale.ToString()));
            if (plotStyle != 0)
                root.AppendChild(root.CreateElementWithText("style", plotStyle.ToString()));
            if (dashStyle != 0)
                root.AppendChild(root.CreateElementWithText("dash", dashStyle.ToString()));

            root.AppendChild(root.CreateElementWithText("color", plotColor.IsKnownColor ? plotColor.Name : "#" + plotColor.ToArgb().ToString()));
            return config.OuterXml;
        }

        internal string GetCounterDefinitionXml()
        {
            string plotColorStr = plotColor.IsKnownColor ? plotColor.Name : "#" + plotColor.ToArgb().ToString();
            string scaleStr = "1";
            string plotStyleStr = "";
            string dashStyleStr = "";
            if (scale != 1)
                scaleStr = scale.ToString();
            if (plotStyle != 0)
                plotStyleStr = plotStyle.ToString();
            if (dashStyle != 0)
                dashStyleStr = dashStyle.ToString();
            return string.Format("<machine>{0}</machine>" + 
                "<category>{1}</category>" +
                "<counter>{2}</counter>" + 
                "<instance>{3}</instance>" +
                "<scale>{4}</scale>" +
                "<color>{5}</color>" +
                "<style>{6}</style>" +
                "<dash>{7}</dash>",
                this.machine, this.category, this.counter, this.instance, this.scale, plotColorStr, plotStyleStr, dashStyleStr);
        }
        public static List<PCMonInstance> GetCountersFromCounterDefinitionList(string counterDefinitionList)
        {
            List<PCMonInstance> counters = new List<PCMonInstance>();
            XmlDocument config = new XmlDocument();
            config.LoadXml(counterDefinitionList);
            XmlElement root = config.DocumentElement;
            foreach (XmlNode n in root.GetElementsByTagName("string"))
            {
                PCMonInstance newCounter = new PCMonInstance(string.Format("<xml>{0}</xml>", n.InnerText));
                counters.Add(newCounter);
            }
            return counters;
        }
    }
}
