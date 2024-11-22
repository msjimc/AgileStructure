namespace AgileStructure
{
    partial class colourCoder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(colourCoder));
            groupBox1 = new System.Windows.Forms.GroupBox();
            btnCustom = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            btnBlack = new System.Windows.Forms.Button();
            btnOrchid = new System.Windows.Forms.Button();
            btnYellow = new System.Windows.Forms.Button();
            btnOrange = new System.Windows.Forms.Button();
            btnGrey = new System.Windows.Forms.Button();
            btnBlue = new System.Windows.Forms.Button();
            btnRed = new System.Windows.Forms.Button();
            btnGreen = new System.Windows.Forms.Button();
            btnClose = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            btnClear = new System.Windows.Forms.Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnClear);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnCustom);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnBlack);
            groupBox1.Controls.Add(btnOrchid);
            groupBox1.Controls.Add(btnYellow);
            groupBox1.Controls.Add(btnOrange);
            groupBox1.Controls.Add(btnGrey);
            groupBox1.Controls.Add(btnBlue);
            groupBox1.Controls.Add(btnRed);
            groupBox1.Controls.Add(btnGreen);
            groupBox1.Location = new System.Drawing.Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(413, 214);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Colour selection";
            // 
            // btnCustom
            // 
            btnCustom.Location = new System.Drawing.Point(294, 138);
            btnCustom.Name = "btnCustom";
            btnCustom.Size = new System.Drawing.Size(75, 23);
            btnCustom.TabIndex = 9;
            btnCustom.Text = "Custom";
            btnCustom.UseVisualStyleBackColor = true;
            btnCustom.Click += btnCustom_Click;
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(6, 19);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(401, 55);
            label1.TabIndex = 8;
            label1.Text = "Select the reads in the main window and then press the appropriate button to change their selection colour. To select a custom colour press the 'Custom' button for the colour selection dialogue box.";
            // 
            // btnBlack
            // 
            btnBlack.Location = new System.Drawing.Point(294, 109);
            btnBlack.Name = "btnBlack";
            btnBlack.Size = new System.Drawing.Size(75, 23);
            btnBlack.TabIndex = 7;
            btnBlack.Text = "Black";
            btnBlack.UseVisualStyleBackColor = true;
            btnBlack.Click += btnBlack_Click;
            // 
            // btnOrchid
            // 
            btnOrchid.Location = new System.Drawing.Point(294, 80);
            btnOrchid.Name = "btnOrchid";
            btnOrchid.Size = new System.Drawing.Size(75, 23);
            btnOrchid.TabIndex = 6;
            btnOrchid.Text = "Orchid";
            btnOrchid.UseVisualStyleBackColor = true;
            btnOrchid.Click += btnOrchid_Click;
            // 
            // btnYellow
            // 
            btnYellow.Location = new System.Drawing.Point(169, 138);
            btnYellow.Name = "btnYellow";
            btnYellow.Size = new System.Drawing.Size(75, 23);
            btnYellow.TabIndex = 5;
            btnYellow.Text = "Yellow";
            btnYellow.UseVisualStyleBackColor = true;
            btnYellow.Click += btnYellow_Click;
            // 
            // btnOrange
            // 
            btnOrange.Location = new System.Drawing.Point(169, 109);
            btnOrange.Name = "btnOrange";
            btnOrange.Size = new System.Drawing.Size(75, 23);
            btnOrange.TabIndex = 4;
            btnOrange.Text = "Orange";
            btnOrange.UseVisualStyleBackColor = true;
            btnOrange.Click += btnOrange_Click;
            // 
            // btnGrey
            // 
            btnGrey.Location = new System.Drawing.Point(169, 80);
            btnGrey.Name = "btnGrey";
            btnGrey.Size = new System.Drawing.Size(75, 23);
            btnGrey.TabIndex = 3;
            btnGrey.Text = "Grey";
            btnGrey.UseVisualStyleBackColor = true;
            btnGrey.Click += btnGrey_Click;
            // 
            // btnBlue
            // 
            btnBlue.Location = new System.Drawing.Point(38, 138);
            btnBlue.Name = "btnBlue";
            btnBlue.Size = new System.Drawing.Size(75, 23);
            btnBlue.TabIndex = 2;
            btnBlue.Text = "Blue";
            btnBlue.UseVisualStyleBackColor = true;
            btnBlue.Click += btnBlue_Click;
            // 
            // btnRed
            // 
            btnRed.Location = new System.Drawing.Point(38, 109);
            btnRed.Name = "btnRed";
            btnRed.Size = new System.Drawing.Size(75, 23);
            btnRed.TabIndex = 1;
            btnRed.Text = "Red";
            btnRed.UseVisualStyleBackColor = true;
            btnRed.Click += btnRed_Click;
            // 
            // btnGreen
            // 
            btnGreen.Location = new System.Drawing.Point(38, 80);
            btnGreen.Name = "btnGreen";
            btnGreen.Size = new System.Drawing.Size(75, 23);
            btnGreen.TabIndex = 0;
            btnGreen.Text = "Green";
            btnGreen.UseVisualStyleBackColor = true;
            btnGreen.Click += btnGreen_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new System.Drawing.Point(350, 232);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(75, 23);
            btnClose.TabIndex = 1;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(206, 181);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(82, 15);
            label2.TabIndex = 10;
            label2.Text = "Deselect reads";
            // 
            // btnClear
            // 
            btnClear.Location = new System.Drawing.Point(294, 177);
            btnClear.Name = "btnClear";
            btnClear.Size = new System.Drawing.Size(75, 23);
            btnClear.TabIndex = 11;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // colourCoder
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(439, 267);
            Controls.Add(btnClose);
            Controls.Add(groupBox1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "colourCoder";
            Text = "Colour selection";
            FormClosing += colourCoder_FormClosing;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.Button btnOrchid;
        private System.Windows.Forms.Button btnYellow;
        private System.Windows.Forms.Button btnOrange;
        private System.Windows.Forms.Button btnGrey;
        private System.Windows.Forms.Button btnBlue;
        private System.Windows.Forms.Button btnRed;
        private System.Windows.Forms.Button btnGreen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnBlack;
        private System.Windows.Forms.Button btnCustom;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label2;
    }
}