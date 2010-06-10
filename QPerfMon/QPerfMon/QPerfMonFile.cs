using System;
using System.Collections.Generic;
using System.Text;

namespace QPerfMon
{
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
    }
}
