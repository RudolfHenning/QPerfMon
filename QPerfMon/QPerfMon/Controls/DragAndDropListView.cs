using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace HenIT.Windows.Controls
{
    public partial class DragAndDropListView :  ListView
    {
        public DragAndDropListView()
        {
            DoubleBuffered = true;
            InitializeComponent();
        }

        #region Private Members

        private ListViewItem m_previousItem;

        #endregion

        #region Protected and Public Methods

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            // get the currently hovered row that the items will be dragged to
            Point clientPoint = base.PointToClient(new Point(drgevent.X, drgevent.Y));
            ListViewItem hoverItem = base.GetItemAt(clientPoint.X, clientPoint.Y);

            if (!drgevent.Data.GetDataPresent(DataFormats.Text) || (string)drgevent.Data.GetData(DataFormats.Text) == "")
                return;

            // create a new list item and add it to the listing of items that should be added
            string[] draggedItems = ((string)drgevent.Data.GetData("Text")).Split('\x0');
            ArrayList insertItems = new ArrayList(draggedItems.Length);
            string controlName = "";

            foreach (string draggedItem in draggedItems)
            {
                // split the data so we can get the control name and the sub item data
                string[] data = draggedItem.Split('\x1');
                controlName = data[0];
                int itemIndex = int.Parse(data[1]);
                string[] subItems = new string[this.Columns.Count];
                for (int i = 0; i < subItems.Length; i++)
                {
                    subItems[i] = this.Items[itemIndex].SubItems[i].Text;
                }
                ListViewItem newItem = new ListViewItem(subItems);
                newItem.Checked = this.Items[itemIndex].Checked;
                newItem.ImageIndex = this.Items[itemIndex].ImageIndex;
                newItem.Tag = this.Items[itemIndex].Tag;
                for (int i = 1; i < newItem.SubItems.Count; i++)
                {
                    newItem.UseItemStyleForSubItems = this.Items[itemIndex].UseItemStyleForSubItems;
                    newItem.SubItems[i].ForeColor = this.Items[itemIndex].SubItems[i].ForeColor;
                    newItem.SubItems[i].BackColor = this.Items[itemIndex].SubItems[i].BackColor;
                }
                insertItems.Add(newItem);
            }

            if (hoverItem == null)
            {
                // the user does not wish to re-order the items, just append to the end
                for (int i = 0; i < insertItems.Count; i++)
                {
                    ListViewItem newItem = (ListViewItem)insertItems[i];
                    base.Items.Add(newItem);
                }
            }
            else
            {
                // the user wishes to re-order the items

                // get the index of the hover item
                int hoverIndex = hoverItem.Index;

                // determine if the items to be dropped are from
                // this list view. If they are, perform a hack
                // to increment the hover index so that the items
                // get moved properly.
                if (base.Name == controlName)
                {
                    if (hoverIndex > base.SelectedItems[0].Index)
                        hoverIndex++;
                }

                // insert the new items into the list view
                // by inserting the items reversely from the array list
                for (int i = insertItems.Count - 1; i >= 0; i--)
                {
                    ListViewItem newItem = (ListViewItem)insertItems[i];
                    base.Items.Insert(hoverIndex, newItem);
                }
            }

            // find the sender list view control
            DragAndDropListView listView = FindListView(this.FindForm(), controlName);

            // remove all the selected items from the previous list view
            // if the list view was found
            if (listView != null)
            {
                foreach (ListViewItem itemToRemove in listView.SelectedItems)
                {
                    listView.Items.Remove(itemToRemove);
                }
            }

            // set the back color of the previous item, then nullify it
            if (m_previousItem != null)
            {
                m_previousItem = null;
            }

            this.Invalidate();

            // call the base on drag drop to raise the event
            base.OnDragDrop(drgevent);

        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            if (!drgevent.Data.GetDataPresent(DataFormats.Text))
            {
                // the item(s) being dragged do not have any data associated
                drgevent.Effect = DragDropEffects.None;
                return;
            }

            if (base.Items.Count > 0)
            {
                // get the currently hovered row that the items will be dragged to
                Point clientPoint = base.PointToClient(new Point(drgevent.X, drgevent.Y));
                ListViewItem hoverItem = base.GetItemAt(clientPoint.X, clientPoint.Y);

                Graphics g = this.CreateGraphics();

                if (hoverItem == null)
                {
                    // no item was found, so no drop should take place
                    drgevent.Effect = DragDropEffects.Move;

                    if (m_previousItem != null)
                    {
                        m_previousItem = null;
                        Invalidate();
                    }

                    hoverItem = base.Items[base.Items.Count - 1];

                    if (this.View == View.Details || this.View == View.List)
                    {
                        g.DrawLine(new Pen(Brushes.Red, 2), new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y + hoverItem.Bounds.Height), new Point(hoverItem.Bounds.X + this.Bounds.Width, hoverItem.Bounds.Y + hoverItem.Bounds.Height));
                        g.FillPolygon(Brushes.Red, new Point[] { new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y + hoverItem.Bounds.Height - 5), new Point(hoverItem.Bounds.X + 5, hoverItem.Bounds.Y + hoverItem.Bounds.Height), new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y + hoverItem.Bounds.Height + 5) });
                        g.FillPolygon(Brushes.Red, new Point[] { new Point(this.Bounds.Width - 4, hoverItem.Bounds.Y + hoverItem.Bounds.Height - 5), new Point(this.Bounds.Width - 9, hoverItem.Bounds.Y + hoverItem.Bounds.Height), new Point(this.Bounds.Width - 4, hoverItem.Bounds.Y + hoverItem.Bounds.Height + 5) });
                    }

                    // call the base OnDragOver event
                    base.OnDragOver(drgevent);

                    return;
                }

                // determine if the user is currently hovering over a new
                // item. If so, set the previous item's back color back
                // to the default color.
                if ((m_previousItem != null && m_previousItem != hoverItem) || m_previousItem == null)
                {
                    this.Invalidate();
                }

                // set the background color of the item being hovered
                // and assign the previous item to the item being hovered
                //hoverItem.BackColor = Color.Beige;
                m_previousItem = hoverItem;

                if (this.View == View.Details || this.View == View.List)
                {
                    g.DrawLine(new Pen(Brushes.Red, 2), new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y), new Point(hoverItem.Bounds.X + this.Bounds.Width, hoverItem.Bounds.Y));
                    g.FillPolygon(Brushes.Red, new Point[] { new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y - 5), new Point(hoverItem.Bounds.X + 5, hoverItem.Bounds.Y), new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y + 5) });
                    g.FillPolygon(Brushes.Red, new Point[] { new Point(this.Bounds.Width - 4, hoverItem.Bounds.Y - 5), new Point(this.Bounds.Width - 9, hoverItem.Bounds.Y), new Point(this.Bounds.Width - 4, hoverItem.Bounds.Y + 5) });
                }

                // go through each of the selected items, and if any of the
                // selected items have the same index as the item being
                // hovered, disable dropping.
                foreach (ListViewItem itemToMove in base.SelectedItems)
                {
                    if (itemToMove.Index == hoverItem.Index)
                    {
                        drgevent.Effect = DragDropEffects.None;
                        hoverItem.EnsureVisible();
                        return;
                    }
                }
            }

            // everything is fine, allow the user to move the items
            drgevent.Effect = DragDropEffects.Move;

            // call the base OnDragOver event
            base.OnDragOver(drgevent);
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            if (!drgevent.Data.GetDataPresent(DataFormats.Text))
            {
                // the item(s) being dragged do not have any data associated
                drgevent.Effect = DragDropEffects.None;
                return;
            }

            // everything is fine, allow the user to move the items
            drgevent.Effect = DragDropEffects.Move;

            // call the base OnDragEnter event
            base.OnDragEnter(drgevent);
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            // call the DoDragDrop method
            base.DoDragDrop(GetItemText(), DragDropEffects.Move);

            // call the base OnItemDrag event
            base.OnItemDrag(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            // reset the selected items background and remove the previous item
            ResetOutOfRange();

            Invalidate();

            // call the OnLostFocus event
            base.OnLostFocus(e);
        }

        protected override void OnDragLeave(EventArgs e)
        {
            // reset the selected items background and remove the previous item
            ResetOutOfRange();

            Invalidate();

            // call the base OnDragLeave event
            base.OnDragLeave(e);
        }

        #endregion

        #region Private Methods

        private DragAndDropListView FindListView(Control parent, string controlName)
        {
            // set the output control to null
            Control theControl = null;

            // go through each control in the parent to determine if the control
            // name to be searched for is contained in the parent's controls
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl.Name.ToLower() == controlName.ToLower())
                    // the control was found, assign a reference to it
                    theControl = ctrl;

                if (theControl == null)
                    // the control was not found, so recursively go through this controls' controls.
                    theControl = FindListView(ctrl, controlName);

                if (theControl != null)
                    // the control was found in one of the two methods above, exit the loop
                    break;
            }

            // return the found control, otherwise null
            if (theControl != null)
                return (DragAndDropListView)theControl;
            else
                return null;
        }

        private string GetItemText()
        {
            string itemText = "";

            // determine if there are any selected items
            if (this.SelectedItems.Count <= 0)
                return "";

            // go through each of the selected items and add
            // their data to the output string that will be 
            // used to re-create the items when dropped.
            foreach (ListViewItem item in this.SelectedItems)
            {
                // add a separator between the different items
                if (itemText != "")
                    itemText += "\x0";

                // add the control name
                itemText += this.Name + "\x1";
                itemText += item.Index.ToString();

                //// add each of the sub item's text
                //for (int i = 0; i < item.SubItems.Count; i++)
                //    itemText += item.SubItems[i].Text + "\x2";

                //// remove the last separator
                //itemText = itemText.Substring(0, itemText.Length - 1);
            }

            // return the selected item(s) text
            return itemText;
        }

        private void ResetOutOfRange()
        {
            // determine if the previous item exists,
            // if it does, reset the background and release 
            // the previous item
            if (m_previousItem != null)
            {
                m_previousItem = null;
            }
        }

        #endregion
    }
}
