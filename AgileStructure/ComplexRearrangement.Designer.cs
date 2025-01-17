﻿namespace AgileStructure
{
    partial class ComplexRearrangement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComplexRearrangement));
            groupBox1 = new System.Windows.Forms.GroupBox();
            btnDraw = new System.Windows.Forms.Button();
            chkColour = new System.Windows.Forms.CheckBox();
            label2 = new System.Windows.Forms.Label();
            btnDrawBP = new System.Windows.Forms.Button();
            txtAnswer = new System.Windows.Forms.TextBox();
            lblAnswer = new System.Windows.Forms.Label();
            btnFind = new System.Windows.Forms.Button();
            btnAccept2 = new System.Windows.Forms.Button();
            lblSecondary2 = new System.Windows.Forms.Label();
            lblPrimary2 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            btnAccept1 = new System.Windows.Forms.Button();
            lblSecondary1 = new System.Windows.Forms.Label();
            lblPrimary1 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            btnClose = new System.Windows.Forms.Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBox1.Controls.Add(btnDraw);
            groupBox1.Controls.Add(chkColour);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnDrawBP);
            groupBox1.Controls.Add(txtAnswer);
            groupBox1.Controls.Add(lblAnswer);
            groupBox1.Controls.Add(btnFind);
            groupBox1.Controls.Add(btnAccept2);
            groupBox1.Controls.Add(lblSecondary2);
            groupBox1.Controls.Add(lblPrimary2);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(btnAccept1);
            groupBox1.Controls.Add(lblSecondary1);
            groupBox1.Controls.Add(lblPrimary1);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new System.Drawing.Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(768, 242);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Break point selection";
            // 
            // btnDraw
            // 
            btnDraw.Enabled = false;
            btnDraw.Location = new System.Drawing.Point(687, 184);
            btnDraw.Name = "btnDraw";
            btnDraw.Size = new System.Drawing.Size(75, 23);
            btnDraw.TabIndex = 14;
            btnDraw.Text = "Draw";
            btnDraw.UseVisualStyleBackColor = true;
            btnDraw.Click += btnDraw_Click;
            // 
            // chkColour
            // 
            chkColour.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            chkColour.AutoSize = true;
            chkColour.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            chkColour.Checked = true;
            chkColour.CheckState = System.Windows.Forms.CheckState.Checked;
            chkColour.Location = new System.Drawing.Point(700, 143);
            chkColour.Name = "chkColour";
            chkColour.Size = new System.Drawing.Size(62, 19);
            chkColour.TabIndex = 13;
            chkColour.Text = "Colour";
            chkColour.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(489, 118);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(192, 15);
            label2.TabIndex = 12;
            label2.Text = "Show break points in main window";
            // 
            // btnDrawBP
            // 
            btnDrawBP.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnDrawBP.Location = new System.Drawing.Point(687, 114);
            btnDrawBP.Name = "btnDrawBP";
            btnDrawBP.Size = new System.Drawing.Size(75, 23);
            btnDrawBP.TabIndex = 11;
            btnDrawBP.Text = "Show ";
            btnDrawBP.UseVisualStyleBackColor = true;
            btnDrawBP.Click += btnDrawBP_Click;
            // 
            // txtAnswer
            // 
            txtAnswer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtAnswer.Location = new System.Drawing.Point(6, 140);
            txtAnswer.Multiline = true;
            txtAnswer.Name = "txtAnswer";
            txtAnswer.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            txtAnswer.Size = new System.Drawing.Size(675, 96);
            txtAnswer.TabIndex = 10;
            // 
            // lblAnswer
            // 
            lblAnswer.AutoSize = true;
            lblAnswer.Location = new System.Drawing.Point(6, 122);
            lblAnswer.Name = "lblAnswer";
            lblAnswer.Size = new System.Drawing.Size(250, 15);
            lblAnswer.TabIndex = 9;
            lblAnswer.Text = "Press 'Annotate' to identify the rearrangement";
            // 
            // btnFind
            // 
            btnFind.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnFind.Location = new System.Drawing.Point(687, 213);
            btnFind.Name = "btnFind";
            btnFind.Size = new System.Drawing.Size(75, 23);
            btnFind.TabIndex = 8;
            btnFind.Text = "Find";
            btnFind.UseVisualStyleBackColor = true;
            btnFind.Click += btnFind_Click;
            // 
            // btnAccept2
            // 
            btnAccept2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnAccept2.Location = new System.Drawing.Point(687, 68);
            btnAccept2.Name = "btnAccept2";
            btnAccept2.Size = new System.Drawing.Size(75, 23);
            btnAccept2.TabIndex = 7;
            btnAccept2.Text = "Accept";
            btnAccept2.UseVisualStyleBackColor = true;
            btnAccept2.Click += btnAccept2_Click;
            // 
            // lblSecondary2
            // 
            lblSecondary2.AutoSize = true;
            lblSecondary2.Location = new System.Drawing.Point(255, 97);
            lblSecondary2.Name = "lblSecondary2";
            lblSecondary2.Size = new System.Drawing.Size(45, 15);
            lblSecondary2.TabIndex = 6;
            lblSecondary2.Text = "Not set";
            // 
            // lblPrimary2
            // 
            lblPrimary2.AutoSize = true;
            lblPrimary2.Location = new System.Drawing.Point(6, 97);
            lblPrimary2.Name = "lblPrimary2";
            lblPrimary2.Size = new System.Drawing.Size(45, 15);
            lblPrimary2.TabIndex = 5;
            lblPrimary2.Text = "Not set";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(6, 72);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(469, 15);
            label4.TabIndex = 4;
            label4.Text = "Select the second break point in the primary alignment (upper) panel and press 'Accept'";
            // 
            // btnAccept1
            // 
            btnAccept1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnAccept1.Location = new System.Drawing.Point(687, 15);
            btnAccept1.Name = "btnAccept1";
            btnAccept1.Size = new System.Drawing.Size(75, 23);
            btnAccept1.TabIndex = 3;
            btnAccept1.Text = "Accept";
            btnAccept1.UseVisualStyleBackColor = true;
            btnAccept1.Click += btnAccept1_Click;
            // 
            // lblSecondary1
            // 
            lblSecondary1.AutoSize = true;
            lblSecondary1.Location = new System.Drawing.Point(255, 44);
            lblSecondary1.Name = "lblSecondary1";
            lblSecondary1.Size = new System.Drawing.Size(45, 15);
            lblSecondary1.TabIndex = 2;
            lblSecondary1.Text = "Not set";
            // 
            // lblPrimary1
            // 
            lblPrimary1.AutoSize = true;
            lblPrimary1.Location = new System.Drawing.Point(6, 44);
            lblPrimary1.Name = "lblPrimary1";
            lblPrimary1.Size = new System.Drawing.Size(45, 15);
            lblPrimary1.TabIndex = 1;
            lblPrimary1.Text = "Not set";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 19);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(451, 15);
            label1.TabIndex = 0;
            label1.Text = "Select the first break point in the primary alignment (upper) panel and press 'Accept'";
            // 
            // btnClose
            // 
            btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnClose.Location = new System.Drawing.Point(18, 262);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(75, 23);
            btnClose.TabIndex = 1;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // ComplexRearrangement
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(792, 297);
            Controls.Add(btnClose);
            Controls.Add(groupBox1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "ComplexRearrangement";
            Text = "Complex rearrangement";
            FormClosing += ComplexRearrangement_FormClosing;
            Load += ComplexRearrangement_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAccept2;
        private System.Windows.Forms.Label lblSecondary2;
        private System.Windows.Forms.Label lblPrimary2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAccept1;
        private System.Windows.Forms.Label lblSecondary1;
        private System.Windows.Forms.Label lblPrimary1;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblAnswer;
        private System.Windows.Forms.TextBox txtAnswer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDrawBP;
        private System.Windows.Forms.CheckBox chkColour;
        private System.Windows.Forms.Button btnDraw;
    }
}