using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace NFL2K5Tool
{
    public partial class DateValueControl : UserControl
    {
        public DateValueControl()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get { return mLabel.Text; }
            set { mLabel.Text = value; }
        }


        public virtual string Value
        {
            get
            {
                return mDateTimePicker.Value.ToString("M/d/yyyy");
            }
            set
            {
                try
                {
                    mDateTimePicker.Value = DateTime.ParseExact(value, "M/d/yyyy", CultureInfo.InvariantCulture);
                }
                catch (Exception e) { StaticUtils.AddError(""+e.Message); }
            }
        }

        public event EventHandler ValueChanged;

        private void DateChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
        }
    }
}
