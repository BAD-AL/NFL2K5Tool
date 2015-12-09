using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NFL2K5Tool
{
    public partial class IntAttrControl : UserControl
    {
        public IntAttrControl()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get { return mLabel.Text; }
            set { mLabel.Text = value; }
        }

        public int Value
        {
            get { return (int)mAttrUpDown.Value; }
            set { mAttrUpDown.Value = value; }
        }

        public decimal Min
        {
            get { return mAttrUpDown.Minimum; }
            set { mAttrUpDown.Minimum = value; }
        }

        public decimal Max
        {
            get { return mAttrUpDown.Maximum; }
            set { mAttrUpDown.Maximum = value; }
        }

        public event EventHandler ValueChanged;

        private void mAttrUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
        }

    }
}
