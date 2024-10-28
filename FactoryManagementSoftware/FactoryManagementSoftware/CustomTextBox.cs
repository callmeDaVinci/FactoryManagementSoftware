using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactoryManagementSoftware
{
    public class CustomTextBox : TextBox
    {
        private ListBox _listBox;
        private bool _isAdded;
        private String[] _values;
        private String _formerValue = String.Empty;

        public CustomTextBox()
        {
            InitializeComponent();
            ResetListBox();

            // Set DrawMode and add DrawItem event for custom rendering
            _listBox.DrawMode = DrawMode.OwnerDrawFixed;
        }

        private void InitializeComponent()
        {
            this._listBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // _listBox
            // 
            this._listBox.ItemHeight = 30;
            this._listBox.Location = new System.Drawing.Point(0, 0);
            this._listBox.Name = "_listBox";
            this._listBox.Size = new System.Drawing.Size(120, 84);
            this._listBox.TabIndex = 0;
            this._listBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this._listBox_DrawItem);
            this._listBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.this_KeyDown);
            this._listBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this._listBox_MouseDoubleClick);
            // 
            // CustomTextBox
            // 
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.this_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.this_KeyUp);
            this.ResumeLayout(false);

        }

        private void ShowListBox()
        {
            if (!_isAdded)
            {
                Parent.Controls.Add(_listBox);
                //this.Controls.Add(_listBox);
                _listBox.Left = Left;
                _listBox.Top = Top + Height;
                _isAdded = true;
            }
            _listBox.Visible = true;
            _listBox.BringToFront();
        }

        private void ResetListBox()
        {
            _listBox.Visible = false;
        }

        private void this_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateListBox();
        }

        private void this_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Tab:
                    {
                        if (_listBox.Visible)
                        {
                            Text = _listBox.SelectedItem.ToString();
                            ResetListBox();
                            _formerValue = Text;
                            this.Select(this.Text.Length, 0);
                            e.Handled = true;
                        }
                        break;
                    }
                case Keys.Down:
                    {
                        if ((_listBox.Visible) && (_listBox.SelectedIndex < _listBox.Items.Count - 1))
                            _listBox.SelectedIndex++;
                        e.Handled = true;
                        break;
                    }
                case Keys.Up:
                    {
                        if ((_listBox.Visible) && (_listBox.SelectedIndex > 0))
                            _listBox.SelectedIndex--;
                        e.Handled = true;
                        break;
                    }


            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Tab:
                    if (_listBox.Visible)
                        return true;
                    else
                        return false;
                default:
                    return base.IsInputKey(keyData);
            }
        }

        private void UpdateListBox()
        {
            if (Text == _formerValue)
                return;

            _formerValue = this.Text;
            string word = this.Text;

            if (_values != null && word.Length > 0)
            {
                var searchTerms = word.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                string[] matches = Array.FindAll(_values, x =>
                {
                    if (x == null) return false;
                    var text = x.ToLower();

                    // Ensure all search terms are present in the text
                    return searchTerms.All(term => text.Contains(term));
                });

                if (matches.Length > 0)
                {
                    ShowListBox();
                    _listBox.BeginUpdate();
                    _listBox.Items.Clear();
                    Array.ForEach(matches, x => _listBox.Items.Add(x));
                    _listBox.SelectedIndex = 0;

                    // Set a maximum height for the ListBox
                    const int maxVisibleItems = 10;
                    int visibleItemsCount = Math.Min(_listBox.Items.Count, maxVisibleItems);
                    int itemHeight = _listBox.ItemHeight;
                    _listBox.Height = visibleItemsCount * itemHeight;

                    // Calculate the width dynamically based on item content
                    int maxWidth = 0;
                    using (Graphics graphics = _listBox.CreateGraphics())
                    {
                        foreach (var item in _listBox.Items)
                        {
                            int itemWidth = (int)graphics.MeasureString(item.ToString() + "_", _listBox.Font).Width;
                            maxWidth = Math.Max(maxWidth, itemWidth);
                        }
                        _listBox.Width = Math.Max(this.Width, maxWidth);
                    }

                    _listBox.EndUpdate();
                }
                else
                {
                    ResetListBox();
                }
            }
            else
            {
                ResetListBox();
            }
        }






        public String[] Values
        {
            get
            {
                return _values;
            }
            set
            {
                _values = value;
            }
        }

       

        private void _listBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_listBox.Visible)
            {
                Text = _listBox.SelectedItem.ToString();
                ResetListBox();
                _formerValue = Text;
                this.Select(this.Text.Length, 0);
            }
        }

        private void _listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the background of the ListBox control for each item.
            e.DrawBackground();

            // Determine the text color based on whether the item is selected
            Brush textBrush;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                // Set the font color to white for the selected item
                textBrush = Brushes.White;
            }
            else
            {
                // Set the font color to black for non-selected items
                textBrush = Brushes.Black;
            }

            // Draw the current item text
            if (e.Index >= 0)
            {
                string text = _listBox.Items[e.Index].ToString();
                e.Graphics.DrawString(
                    text,
                    new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Regular), // Regular font
                    textBrush,
                    e.Bounds
                );
            }

            // Draw the focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }
    }
}
