using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Drawing;

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

        [OptionalField]
        private bool rememberMainWindowSizeLocation = false;
        public bool RememberMainWindowSizeLocation
        {
            get { return rememberMainWindowSizeLocation; }
            set { rememberMainWindowSizeLocation = value; }
        }

        [OptionalField]
        private Size mainWindowSize = new Size(0, 0);
        public Size MainWindowSize
        {
            get { return mainWindowSize; }
            set { mainWindowSize = value; }
        }

        [OptionalField]
        private Point mainWindowLocation = new Point(0, 0);
        public Point MainWindowLocation
        {
            get { return mainWindowLocation; }
            set { mainWindowLocation = value; }
        }
    }
}
