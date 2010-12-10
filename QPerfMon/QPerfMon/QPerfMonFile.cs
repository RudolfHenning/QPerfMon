using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace QPerfMon
{
    [Serializable]
    public class QPerfMonFile
    {
        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private int initialMaxValue = 100;
        public int InitialMaxValue
        {
            get { return initialMaxValue; }
            set { initialMaxValue = value; }
        }
        private List<string> counterDefinitionList = new List<string>();
        public List<string> CounterDefinitionList
        {
            get { return counterDefinitionList; }
            set { counterDefinitionList = value; }
        }

        [OptionalField]
        private string version = "Pre 1.4.9";
        public string Version
        {
            get { return version; }
            set { version = value; }
        }
        [OnDeserializing]
        private void Setversion(StreamingContext sc)
        {
            version = "1.4.8";
        }

    }
}
