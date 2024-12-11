
namespace AgileStructure
{
    partial class FindGene
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindGene));
            groupBox1 = new System.Windows.Forms.GroupBox();
            btnFind = new System.Windows.Forms.Button();
            txtCoordinates = new System.Windows.Forms.TextBox();
            txtGene = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            btnAccept = new System.Windows.Forms.Button();
            btnQuit = new System.Windows.Forms.Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBox1.Controls.Add(btnFind);
            groupBox1.Controls.Add(txtCoordinates);
            groupBox1.Controls.Add(txtGene);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnAccept);
            groupBox1.Location = new System.Drawing.Point(14, 14);
            groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Size = new System.Drawing.Size(722, 157);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Gene selection";
            // 
            // btnFind
            // 
            btnFind.Location = new System.Drawing.Point(628, 58);
            btnFind.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnFind.Name = "btnFind";
            btnFind.Size = new System.Drawing.Size(88, 27);
            btnFind.TabIndex = 1;
            btnFind.Text = "Find";
            btnFind.UseVisualStyleBackColor = true;
            btnFind.Click += btnFind_Click;
            // 
            // txtCoordinates
            // 
            txtCoordinates.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtCoordinates.Location = new System.Drawing.Point(10, 91);
            txtCoordinates.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtCoordinates.Name = "txtCoordinates";
            txtCoordinates.Size = new System.Drawing.Size(704, 23);
            txtCoordinates.TabIndex = 2;
            txtCoordinates.KeyDown += txtCoordinates_KeyDown;
            // 
            // txtGene
            // 
            txtGene.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtGene.Location = new System.Drawing.Point(10, 61);
            txtGene.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtGene.Name = "txtGene";
            txtGene.Size = new System.Drawing.Size(610, 23);
            txtGene.TabIndex = 0;
            txtGene.TextChanged += txtGene_TextChanged;
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label1.Location = new System.Drawing.Point(0, 30);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(708, 28);
            label1.TabIndex = 1;
            label1.Text = "Start to type the gene/feature's name in the text box and press 'Accept' when to accept the gene and view reads aligned to it.";
            // 
            // btnAccept
            // 
            btnAccept.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnAccept.Enabled = false;
            btnAccept.Location = new System.Drawing.Point(628, 121);
            btnAccept.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnAccept.Name = "btnAccept";
            btnAccept.Size = new System.Drawing.Size(88, 27);
            btnAccept.TabIndex = 3;
            btnAccept.Text = "Accept";
            btnAccept.UseVisualStyleBackColor = true;
            btnAccept.Click += button1_Click;
            // 
            // btnQuit
            // 
            btnQuit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnQuit.Location = new System.Drawing.Point(14, 178);
            btnQuit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnQuit.Name = "btnQuit";
            btnQuit.Size = new System.Drawing.Size(88, 27);
            btnQuit.TabIndex = 4;
            btnQuit.Text = "Close";
            btnQuit.UseVisualStyleBackColor = true;
            btnQuit.Click += btnQuit_Click;
            // 
            // FindGene
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(750, 222);
            Controls.Add(btnQuit);
            Controls.Add(groupBox1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FindGene";
            Text = "Find a gene's coordinatesGene";
            Load += FindGene_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCoordinates;
        private System.Windows.Forms.TextBox txtGene;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Button btnFind;
    }
}