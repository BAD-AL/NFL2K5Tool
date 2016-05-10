using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NFL2K5Tool
{
    /// <summary>
    /// a control that has a string selection element and an expandable picture element.
    /// </summary>
    public partial class PictureChooser : StringSelectionControl
    {
        public event EventHandler PictureBoxClicked;

        /// <summary>
        /// A control that has a combobox and expandable picture box when entered.
        /// </summary>
        public PictureChooser()
        {
            InitializeComponent();
            this.mPictureBox.Click += new EventHandler(mPictureBox_Click);
        }

        public Size ExpandedSize = new Size(126, 155);

        public Size NormalSize = new System.Drawing.Size(126, 48);

        /// <summary>
        /// Called when picture box is clicked.
        /// </summary>
        protected virtual void OnPictureBoxClicked() 
        {
            if (this.PictureBoxClicked != null)
                this.PictureBoxClicked(this, EventArgs.Empty);
        }

        public PictureBox PictureBox { get { return this.mPictureBox; } }

        protected override void OnEnter(EventArgs e)
        {
            this.Size = this.ExpandedSize;
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            Size = NormalSize;
            base.OnLeave(e);
        }

        void mPictureBox_Click(object sender, EventArgs e)
        {
            OnPictureBoxClicked();
        }

    }
}
