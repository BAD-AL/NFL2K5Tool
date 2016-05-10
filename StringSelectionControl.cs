using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NFL2K5Tool
{
    public partial class StringSelectionControl : UserControl
    {
        public StringSelectionControl()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get { return mLabel.Text; }
            set { mLabel.Text = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public ComboBoxStyle DropDownStyle
        {
            get { return mComboBox.DropDownStyle; }
            set { mComboBox.DropDownStyle = value; }
        }

        public virtual string Value
        {
            get 
            {
                string retVal = "";
                if (mComboBox.SelectedItem != null)
                    retVal = mComboBox.SelectedItem.ToString();
                return retVal;
            }
            set
            {
                int index = mComboBox.Items.IndexOf(value);
                if (index > -1)
                    mComboBox.SelectedIndex = index;
            }
        }

        private Type mRepresentedValue = typeof(string);

        /// <summary>
        /// For controls tied to an enum, this should be set to the value represented by the control.
        /// </summary>
        public Type RepresentedValue
        {
            get { return mRepresentedValue; }
            set
            {
                if (value != null && value.IsEnum)
                {
                    mRepresentedValue = value;
                    SetItems( Enum.GetNames(mRepresentedValue));
                }
            }
        }

        /// <summary>
        /// Set the values for the control
        /// </summary>
        public void SetItems(object[] values)
        {
            mComboBox.Items.Clear();
            mComboBox.Items.AddRange(values);
        }

        public event EventHandler ValueChanged;

        private void mComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnValueChanged(e);
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
        }
    }
}
