namespace AgileStructure
{
    partial class DrawSelectedReads
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrawSelectedReads));
            groupBox1 = new System.Windows.Forms.GroupBox();
            btnAnnotate = new System.Windows.Forms.Button();
            label5 = new System.Windows.Forms.Label();
            numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            btnShow = new System.Windows.Forms.Button();
            btnAccept2 = new System.Windows.Forms.Button();
            lblSecondary2 = new System.Windows.Forms.Label();
            lblPrimary2 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            btnAccept1 = new System.Windows.Forms.Button();
            lblSecondary1 = new System.Windows.Forms.Label();
            lblPrimary1 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            p1 = new System.Windows.Forms.PictureBox();
            btnDraw = new System.Windows.Forms.Button();
            button1 = new System.Windows.Forms.Button();
            groupBox2 = new System.Windows.Forms.GroupBox();
            btnClear = new System.Windows.Forms.Button();
            timer1 = new System.Windows.Forms.Timer(components);
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)p1).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBox1.Controls.Add(btnAnnotate);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(numericUpDown1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnShow);
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
            groupBox1.Size = new System.Drawing.Size(689, 184);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Break point selection";
            // 
            // btnAnnotate
            // 
            btnAnnotate.Enabled = false;
            btnAnnotate.Location = new System.Drawing.Point(255, 151);
            btnAnnotate.Name = "btnAnnotate";
            btnAnnotate.Size = new System.Drawing.Size(75, 23);
            btnAnnotate.TabIndex = 26;
            btnAnnotate.Text = "Annotate";
            btnAnnotate.UseVisualStyleBackColor = true;
            btnAnnotate.Click += btnAnnotate_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(6, 155);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(167, 15);
            label5.TabIndex = 25;
            label5.Text = "Annotate selected breakpoints";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            numericUpDown1.Location = new System.Drawing.Point(608, 122);
            numericUpDown1.Maximum = new decimal(new int[] { 101, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new System.Drawing.Size(75, 23);
            numericUpDown1.TabIndex = 24;
            numericUpDown1.Value = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // label3
            // 
            label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(410, 124);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(173, 15);
            label3.TabIndex = 23;
            label3.Text = "Merge breakpints if closer than:";
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(410, 155);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(192, 15);
            label2.TabIndex = 22;
            label2.Text = "Show break points in main window";
            // 
            // btnShow
            // 
            btnShow.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnShow.Location = new System.Drawing.Point(608, 151);
            btnShow.Name = "btnShow";
            btnShow.Size = new System.Drawing.Size(75, 23);
            btnShow.TabIndex = 21;
            btnShow.Text = "Show";
            btnShow.UseVisualStyleBackColor = true;
            btnShow.Click += btnShow_Click;
            // 
            // btnAccept2
            // 
            btnAccept2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnAccept2.Location = new System.Drawing.Point(608, 64);
            btnAccept2.Name = "btnAccept2";
            btnAccept2.Size = new System.Drawing.Size(75, 23);
            btnAccept2.TabIndex = 20;
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
            lblSecondary2.TabIndex = 19;
            lblSecondary2.Text = "Not set";
            // 
            // lblPrimary2
            // 
            lblPrimary2.AutoSize = true;
            lblPrimary2.Location = new System.Drawing.Point(6, 97);
            lblPrimary2.Name = "lblPrimary2";
            lblPrimary2.Size = new System.Drawing.Size(45, 15);
            lblPrimary2.TabIndex = 18;
            lblPrimary2.Text = "Not set";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(6, 68);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(469, 15);
            label4.TabIndex = 17;
            label4.Text = "Select the second break point in the primary alignment (upper) panel and press 'Accept'";
            // 
            // btnAccept1
            // 
            btnAccept1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnAccept1.Location = new System.Drawing.Point(608, 15);
            btnAccept1.Name = "btnAccept1";
            btnAccept1.Size = new System.Drawing.Size(75, 23);
            btnAccept1.TabIndex = 16;
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
            lblSecondary1.TabIndex = 15;
            lblSecondary1.Text = "Not set";
            // 
            // lblPrimary1
            // 
            lblPrimary1.AutoSize = true;
            lblPrimary1.Location = new System.Drawing.Point(6, 44);
            lblPrimary1.Name = "lblPrimary1";
            lblPrimary1.Size = new System.Drawing.Size(45, 15);
            lblPrimary1.TabIndex = 14;
            lblPrimary1.Text = "Not set";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 19);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(451, 15);
            label1.TabIndex = 13;
            label1.Text = "Select the first break point in the primary alignment (upper) panel and press 'Accept'";
            // 
            // p1
            // 
            p1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            p1.Location = new System.Drawing.Point(6, 22);
            p1.Name = "p1";
            p1.Size = new System.Drawing.Size(677, 264);
            p1.TabIndex = 23;
            p1.TabStop = false;
            // 
            // btnDraw
            // 
            btnDraw.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnDraw.Location = new System.Drawing.Point(608, 292);
            btnDraw.Name = "btnDraw";
            btnDraw.Size = new System.Drawing.Size(75, 23);
            btnDraw.TabIndex = 21;
            btnDraw.Text = "Save";
            btnDraw.UseVisualStyleBackColor = true;
            btnDraw.Click += btnDraw_Click;
            // 
            // button1
            // 
            button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            button1.Location = new System.Drawing.Point(18, 535);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Close";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBox2.Controls.Add(btnClear);
            groupBox2.Controls.Add(p1);
            groupBox2.Controls.Add(btnDraw);
            groupBox2.Location = new System.Drawing.Point(12, 202);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(689, 321);
            groupBox2.TabIndex = 22;
            groupBox2.TabStop = false;
            groupBox2.Text = "Schematic";
            // 
            // btnClear
            // 
            btnClear.Location = new System.Drawing.Point(527, 292);
            btnClear.Name = "btnClear";
            btnClear.Size = new System.Drawing.Size(75, 23);
            btnClear.TabIndex = 24;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // timer1
            // 
            timer1.Interval = 200;
            timer1.Tick += timer1_Tick;
            // 
            // DrawSelectedReads
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(713, 570);
            Controls.Add(groupBox2);
            Controls.Add(button1);
            Controls.Add(groupBox1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MinimumSize = new System.Drawing.Size(729, 609);
            Name = "DrawSelectedReads";
            Text = "Draw schematic display";
            FormClosing += DrawSelectedReads_FormClosing;
            Resize += DrawSelectedReads_Resize;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)p1).EndInit();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox p1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.Button btnAccept2;
        private System.Windows.Forms.Label lblSecondary2;
        private System.Windows.Forms.Label lblPrimary2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAccept1;
        private System.Windows.Forms.Label lblSecondary1;
        private System.Windows.Forms.Label lblPrimary1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAnnotate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClear;
    }
}