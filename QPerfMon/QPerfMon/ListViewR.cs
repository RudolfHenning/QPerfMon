using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace QPerfMon
{
    /// <summary>
    /// Enhancement class to avoid the listview from flickering during updates
    /// </summary>
    public class ListViewR : ListView
    {
        public ListViewR() : base()
        {
            DoubleBuffered = true;
        }
    }
}
