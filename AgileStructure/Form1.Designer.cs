
namespace AgileStructure
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtEnd = new System.Windows.Forms.TextBox();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.cboRef = new System.Windows.Forms.ComboBox();
            this.btnGetReads = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.gbpP1 = new System.Windows.Forms.GroupBox();
            this.p1 = new System.Windows.Forms.PictureBox();
            this.gbpP2 = new System.Windows.Forms.GroupBox();
            this.p2 = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.txtsEnd = new System.Windows.Forms.TextBox();
            this.txtsStart = new System.Windows.Forms.TextBox();
            this.cboSecondaries = new System.Windows.Forms.ComboBox();
            this.btnSecondaryView = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openBAMFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lookForIndelsWithinAReadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gTFAnnotationFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geneCoordinatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.repeatAnnotationFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showRepeatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.showPositionOfCursorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewReadDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveSelectedReadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSelectedReadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.alignerStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.variantDeterminationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.useSoftClipDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.variantTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.deletionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inversionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.translocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usePrimaryAlignmentsCIGARStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.switchRegionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.primaryAlignmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.secondaryAlignmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.gbpP1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).BeginInit();
            this.gbpP2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.p2)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtEnd);
            this.groupBox1.Controls.Add(this.txtStart);
            this.groupBox1.Controls.Add(this.cboRef);
            this.groupBox1.Controls.Add(this.btnGetReads);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1163, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Region selection";
            // 
            // txtEnd
            // 
            this.txtEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEnd.Location = new System.Drawing.Point(962, 21);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(114, 20);
            this.txtEnd.TabIndex = 6;
            this.txtEnd.TextChanged += new System.EventHandler(this.txtEnd_TextChanged);
            this.txtEnd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNumber_KeyDown);
            this.txtEnd.Leave += new System.EventHandler(this.txtEnd_Leave);
            this.txtEnd.Validating += new System.ComponentModel.CancelEventHandler(this.txtNumber_Validating);
            // 
            // txtStart
            // 
            this.txtStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStart.Location = new System.Drawing.Point(842, 21);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(114, 20);
            this.txtStart.TabIndex = 5;
            this.txtStart.TextChanged += new System.EventHandler(this.txtStart_TextChanged);
            this.txtStart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNumber_KeyDown);
            this.txtStart.Leave += new System.EventHandler(this.txtStart_Leave);
            this.txtStart.Validating += new System.ComponentModel.CancelEventHandler(this.txtNumber_Validating);
            // 
            // cboRef
            // 
            this.cboRef.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboRef.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRef.FormattingEnabled = true;
            this.cboRef.Location = new System.Drawing.Point(87, 21);
            this.cboRef.Name = "cboRef";
            this.cboRef.Size = new System.Drawing.Size(749, 21);
            this.cboRef.TabIndex = 4;
            this.cboRef.SelectedIndexChanged += new System.EventHandler(this.cboRef_SelectedIndexChanged);
            // 
            // btnGetReads
            // 
            this.btnGetReads.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetReads.Location = new System.Drawing.Point(1082, 19);
            this.btnGetReads.Name = "btnGetReads";
            this.btnGetReads.Size = new System.Drawing.Size(75, 23);
            this.btnGetReads.TabIndex = 3;
            this.btnGetReads.Text = "Get reads";
            this.btnGetReads.UseVisualStyleBackColor = true;
            this.btnGetReads.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "BAM file";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // gbpP1
            // 
            this.gbpP1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbpP1.Controls.Add(this.p1);
            this.gbpP1.Location = new System.Drawing.Point(0, 65);
            this.gbpP1.Name = "gbpP1";
            this.gbpP1.Size = new System.Drawing.Size(1166, 221);
            this.gbpP1.TabIndex = 1;
            this.gbpP1.TabStop = false;
            // 
            // p1
            // 
            this.p1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p1.Location = new System.Drawing.Point(3, 16);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(1160, 202);
            this.p1.TabIndex = 0;
            this.p1.TabStop = false;
            this.p1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.p1_MouseClick);
            this.p1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.p1_MouseDown);
            this.p1.MouseLeave += new System.EventHandler(this.p1_MouseLeave);
            this.p1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.p1_MouseMove);
            this.p1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.p1_MouseUp);
            // 
            // gbpP2
            // 
            this.gbpP2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbpP2.Controls.Add(this.p2);
            this.gbpP2.Location = new System.Drawing.Point(0, 65);
            this.gbpP2.Name = "gbpP2";
            this.gbpP2.Size = new System.Drawing.Size(1169, 216);
            this.gbpP2.TabIndex = 2;
            this.gbpP2.TabStop = false;
            // 
            // p2
            // 
            this.p2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p2.Location = new System.Drawing.Point(3, 16);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(1163, 197);
            this.p2.TabIndex = 0;
            this.p2.TabStop = false;
            this.p2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.p2_MouseClick);
            this.p2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.p2_MouseDown);
            this.p2.MouseLeave += new System.EventHandler(this.p2_MouseLeave);
            this.p2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.p2_MouseMove);
            this.p2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.p2_MouseUp);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnFilter);
            this.groupBox4.Controls.Add(this.txtsEnd);
            this.groupBox4.Controls.Add(this.txtsStart);
            this.groupBox4.Controls.Add(this.cboSecondaries);
            this.groupBox4.Controls.Add(this.btnSecondaryView);
            this.groupBox4.Location = new System.Drawing.Point(0, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1166, 56);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Secondary alignment region";
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(9, 19);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 9;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // txtsEnd
            // 
            this.txtsEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtsEnd.Location = new System.Drawing.Point(965, 21);
            this.txtsEnd.Name = "txtsEnd";
            this.txtsEnd.Size = new System.Drawing.Size(114, 20);
            this.txtsEnd.TabIndex = 8;
            this.txtsEnd.TextChanged += new System.EventHandler(this.txtsEnd_TextChanged);
            this.txtsEnd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNumber_KeyDown);
            this.txtsEnd.Leave += new System.EventHandler(this.txtsEnd_Leave);
            this.txtsEnd.Validating += new System.ComponentModel.CancelEventHandler(this.txtNumber_Validating);
            // 
            // txtsStart
            // 
            this.txtsStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtsStart.Location = new System.Drawing.Point(845, 21);
            this.txtsStart.Name = "txtsStart";
            this.txtsStart.Size = new System.Drawing.Size(114, 20);
            this.txtsStart.TabIndex = 7;
            this.txtsStart.TextChanged += new System.EventHandler(this.txtsStart_TextChanged);
            this.txtsStart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNumber_KeyDown);
            this.txtsStart.Leave += new System.EventHandler(this.txtsStart_Leave);
            this.txtsStart.Validating += new System.ComponentModel.CancelEventHandler(this.txtNumber_Validating);
            // 
            // cboSecondaries
            // 
            this.cboSecondaries.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSecondaries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSecondaries.FormattingEnabled = true;
            this.cboSecondaries.Location = new System.Drawing.Point(87, 21);
            this.cboSecondaries.Name = "cboSecondaries";
            this.cboSecondaries.Size = new System.Drawing.Size(752, 21);
            this.cboSecondaries.TabIndex = 4;
            this.cboSecondaries.SelectedIndexChanged += new System.EventHandler(this.cboSecondaries_SelectedIndexChanged);
            // 
            // btnSecondaryView
            // 
            this.btnSecondaryView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSecondaryView.Location = new System.Drawing.Point(1085, 19);
            this.btnSecondaryView.Name = "btnSecondaryView";
            this.btnSecondaryView.Size = new System.Drawing.Size(75, 23);
            this.btnSecondaryView.TabIndex = 3;
            this.btnSecondaryView.Text = "Select";
            this.btnSecondaryView.UseVisualStyleBackColor = true;
            this.btnSecondaryView.Click += new System.EventHandler(this.btnSecondaryView_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 27);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.gbpP1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel2.Controls.Add(this.gbpP2);
            this.splitContainer1.Size = new System.Drawing.Size(1169, 580);
            this.splitContainer1.SplitterDistance = 289;
            this.splitContainer1.TabIndex = 4;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.fileToolStripMenuItem,
            this.dataToolStripMenuItem,
            this.variantDeterminationToolStripMenuItem,
            this.historyToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1193, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openBAMFileToolStripMenuItem,
            this.toolStripMenuItem9,
            this.onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem,
            this.lookForIndelsWithinAReadToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(62, 22);
            this.fileToolStripMenuItem1.Text = "Analysis";
            // 
            // openBAMFileToolStripMenuItem
            // 
            this.openBAMFileToolStripMenuItem.Name = "openBAMFileToolStripMenuItem";
            this.openBAMFileToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.openBAMFileToolStripMenuItem.Text = "Open BAM file";
            this.openBAMFileToolStripMenuItem.Click += new System.EventHandler(this.openBAMFileToolStripMenuItem_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(303, 6);
            // 
            // onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem
            // 
            this.onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem.Name = "onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem";
            this.onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem.Text = "Only show reads with secondary alignments";
            this.onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem.Click += new System.EventHandler(this.onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem_Click);
            // 
            // lookForIndelsWithinAReadToolStripMenuItem
            // 
            this.lookForIndelsWithinAReadToolStripMenuItem.Name = "lookForIndelsWithinAReadToolStripMenuItem";
            this.lookForIndelsWithinAReadToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.lookForIndelsWithinAReadToolStripMenuItem.Text = "Look for indels within a read";
            this.lookForIndelsWithinAReadToolStripMenuItem.Click += new System.EventHandler(this.lookForIndelsWithinAReadToolStripMenuItem_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gTFAnnotationFileToolStripMenuItem,
            this.geneCoordinatesToolStripMenuItem,
            this.toolStripMenuItem4,
            this.repeatAnnotationFileToolStripMenuItem,
            this.showRepeatsToolStripMenuItem,
            this.toolStripMenuItem5,
            this.showPositionOfCursorToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(79, 22);
            this.fileToolStripMenuItem.Text = "Annotation";
            // 
            // gTFAnnotationFileToolStripMenuItem
            // 
            this.gTFAnnotationFileToolStripMenuItem.Name = "gTFAnnotationFileToolStripMenuItem";
            this.gTFAnnotationFileToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.gTFAnnotationFileToolStripMenuItem.Text = "Gene annotation file";
            this.gTFAnnotationFileToolStripMenuItem.Click += new System.EventHandler(this.gTFAnnotationFileToolStripMenuItem_Click);
            // 
            // geneCoordinatesToolStripMenuItem
            // 
            this.geneCoordinatesToolStripMenuItem.Enabled = false;
            this.geneCoordinatesToolStripMenuItem.Name = "geneCoordinatesToolStripMenuItem";
            this.geneCoordinatesToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.geneCoordinatesToolStripMenuItem.Text = "Gene coordinates";
            this.geneCoordinatesToolStripMenuItem.Click += new System.EventHandler(this.geneCoordinatesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(196, 6);
            // 
            // repeatAnnotationFileToolStripMenuItem
            // 
            this.repeatAnnotationFileToolStripMenuItem.Name = "repeatAnnotationFileToolStripMenuItem";
            this.repeatAnnotationFileToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.repeatAnnotationFileToolStripMenuItem.Text = "Repeat annotation file";
            this.repeatAnnotationFileToolStripMenuItem.Click += new System.EventHandler(this.repeatAnnotationFileToolStripMenuItem_Click);
            // 
            // showRepeatsToolStripMenuItem
            // 
            this.showRepeatsToolStripMenuItem.Enabled = false;
            this.showRepeatsToolStripMenuItem.Name = "showRepeatsToolStripMenuItem";
            this.showRepeatsToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.showRepeatsToolStripMenuItem.Text = "Show repeats";
            this.showRepeatsToolStripMenuItem.Click += new System.EventHandler(this.showRepeatsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(196, 6);
            // 
            // showPositionOfCursorToolStripMenuItem
            // 
            this.showPositionOfCursorToolStripMenuItem.Name = "showPositionOfCursorToolStripMenuItem";
            this.showPositionOfCursorToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.showPositionOfCursorToolStripMenuItem.Text = "Show position of cursor";
            this.showPositionOfCursorToolStripMenuItem.Click += new System.EventHandler(this.showPositionOfCursorToolStripMenuItem_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewReadDataToolStripMenuItem,
            this.toolStripMenuItem1,
            this.saveSelectedReadsToolStripMenuItem,
            this.clearSelectedReadsToolStripMenuItem,
            this.toolStripMenuItem6,
            this.alignerStringToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(43, 22);
            this.dataToolStripMenuItem.Text = "Data";
            // 
            // viewReadDataToolStripMenuItem
            // 
            this.viewReadDataToolStripMenuItem.Name = "viewReadDataToolStripMenuItem";
            this.viewReadDataToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.viewReadDataToolStripMenuItem.Text = "View read data";
            this.viewReadDataToolStripMenuItem.Click += new System.EventHandler(this.viewReadDataToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(175, 6);
            // 
            // saveSelectedReadsToolStripMenuItem
            // 
            this.saveSelectedReadsToolStripMenuItem.Enabled = false;
            this.saveSelectedReadsToolStripMenuItem.Name = "saveSelectedReadsToolStripMenuItem";
            this.saveSelectedReadsToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.saveSelectedReadsToolStripMenuItem.Text = "Save selected reads";
            this.saveSelectedReadsToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedReadsToolStripMenuItem_Click);
            // 
            // clearSelectedReadsToolStripMenuItem
            // 
            this.clearSelectedReadsToolStripMenuItem.Name = "clearSelectedReadsToolStripMenuItem";
            this.clearSelectedReadsToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.clearSelectedReadsToolStripMenuItem.Text = "Clear selected reads";
            this.clearSelectedReadsToolStripMenuItem.Click += new System.EventHandler(this.clearSelectedReadsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(175, 6);
            // 
            // alignerStringToolStripMenuItem
            // 
            this.alignerStringToolStripMenuItem.Name = "alignerStringToolStripMenuItem";
            this.alignerStringToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.alignerStringToolStripMenuItem.Text = "Aligner string";
            this.alignerStringToolStripMenuItem.Click += new System.EventHandler(this.alignerStringToolStripMenuItem_Click);
            // 
            // variantDeterminationToolStripMenuItem
            // 
            this.variantDeterminationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.useSoftClipDataToolStripMenuItem,
            this.usePrimaryAlignmentsCIGARStringToolStripMenuItem,
            this.toolStripMenuItem8,
            this.switchRegionToolStripMenuItem});
            this.variantDeterminationToolStripMenuItem.Name = "variantDeterminationToolStripMenuItem";
            this.variantDeterminationToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.variantDeterminationToolStripMenuItem.Text = "Variant determination";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(269, 6);
            // 
            // useSoftClipDataToolStripMenuItem
            // 
            this.useSoftClipDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.variantTypeToolStripMenuItem,
            this.toolStripMenuItem2,
            this.deletionsToolStripMenuItem,
            this.inversionToolStripMenuItem,
            this.duplicationToolStripMenuItem,
            this.insertionToolStripMenuItem,
            this.translocationToolStripMenuItem});
            this.useSoftClipDataToolStripMenuItem.Name = "useSoftClipDataToolStripMenuItem";
            this.useSoftClipDataToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.useSoftClipDataToolStripMenuItem.Text = "Use soft clip data";
            // 
            // variantTypeToolStripMenuItem
            // 
            this.variantTypeToolStripMenuItem.Name = "variantTypeToolStripMenuItem";
            this.variantTypeToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.variantTypeToolStripMenuItem.Text = "Variant type";
            this.variantTypeToolStripMenuItem.Click += new System.EventHandler(this.variantTypeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(141, 6);
            // 
            // deletionsToolStripMenuItem
            // 
            this.deletionsToolStripMenuItem.Name = "deletionsToolStripMenuItem";
            this.deletionsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.deletionsToolStripMenuItem.Text = "Deletion";
            this.deletionsToolStripMenuItem.Click += new System.EventHandler(this.deletionsToolStripMenuItem_Click);
            // 
            // inversionToolStripMenuItem
            // 
            this.inversionToolStripMenuItem.Name = "inversionToolStripMenuItem";
            this.inversionToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.inversionToolStripMenuItem.Text = "Inversion";
            this.inversionToolStripMenuItem.Click += new System.EventHandler(this.inversionToolStripMenuItem_Click);
            // 
            // duplicationToolStripMenuItem
            // 
            this.duplicationToolStripMenuItem.Name = "duplicationToolStripMenuItem";
            this.duplicationToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.duplicationToolStripMenuItem.Text = "Duplication";
            this.duplicationToolStripMenuItem.Click += new System.EventHandler(this.duplicationToolStripMenuItem_Click);
            // 
            // insertionToolStripMenuItem
            // 
            this.insertionToolStripMenuItem.Name = "insertionToolStripMenuItem";
            this.insertionToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.insertionToolStripMenuItem.Text = "Insertion";
            this.insertionToolStripMenuItem.Click += new System.EventHandler(this.insertionToolStripMenuItem_Click);
            // 
            // translocationToolStripMenuItem
            // 
            this.translocationToolStripMenuItem.Name = "translocationToolStripMenuItem";
            this.translocationToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.translocationToolStripMenuItem.Text = "Translocation";
            this.translocationToolStripMenuItem.Click += new System.EventHandler(this.translocationToolStripMenuItem_Click);
            // 
            // usePrimaryAlignmentsCIGARStringToolStripMenuItem
            // 
            this.usePrimaryAlignmentsCIGARStringToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertationToolStripMenuItem,
            this.deletionToolStripMenuItem});
            this.usePrimaryAlignmentsCIGARStringToolStripMenuItem.Name = "usePrimaryAlignmentsCIGARStringToolStripMenuItem";
            this.usePrimaryAlignmentsCIGARStringToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.usePrimaryAlignmentsCIGARStringToolStripMenuItem.Text = "Use Primary alignment\'s CIGAR string";
            // 
            // insertationToolStripMenuItem
            // 
            this.insertationToolStripMenuItem.Name = "insertationToolStripMenuItem";
            this.insertationToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.insertationToolStripMenuItem.Text = "Insertation";
            this.insertationToolStripMenuItem.Click += new System.EventHandler(this.insertationToolStripMenuItem_Click);
            // 
            // deletionToolStripMenuItem
            // 
            this.deletionToolStripMenuItem.Name = "deletionToolStripMenuItem";
            this.deletionToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.deletionToolStripMenuItem.Text = "Deletion";
            this.deletionToolStripMenuItem.Click += new System.EventHandler(this.deletionToolStripMenuItem_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(269, 6);
            // 
            // switchRegionToolStripMenuItem
            // 
            this.switchRegionToolStripMenuItem.Name = "switchRegionToolStripMenuItem";
            this.switchRegionToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.switchRegionToolStripMenuItem.Text = "Switch region";
            this.switchRegionToolStripMenuItem.Click += new System.EventHandler(this.switchRegionToolStripMenuItem_Click);
            // 
            // historyToolStripMenuItem
            // 
            this.historyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.primaryAlignmentsToolStripMenuItem,
            this.secondaryAlignmentsToolStripMenuItem});
            this.historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            this.historyToolStripMenuItem.Size = new System.Drawing.Size(57, 22);
            this.historyToolStripMenuItem.Text = "History";
            // 
            // primaryAlignmentsToolStripMenuItem
            // 
            this.primaryAlignmentsToolStripMenuItem.Name = "primaryAlignmentsToolStripMenuItem";
            this.primaryAlignmentsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.primaryAlignmentsToolStripMenuItem.Text = "Primary alignments";
            // 
            // secondaryAlignmentsToolStripMenuItem
            // 
            this.secondaryAlignmentsToolStripMenuItem.Name = "secondaryAlignmentsToolStripMenuItem";
            this.secondaryAlignmentsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.secondaryAlignmentsToolStripMenuItem.Text = "Secondary alignments";
            // 
            // timer2
            // 
            this.timer2.Interval = 250;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 633);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Genomic rearrangements";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbpP1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.p1)).EndInit();
            this.gbpP2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.p2)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cboRef;
        private System.Windows.Forms.Button btnGetReads;
        private System.Windows.Forms.GroupBox gbpP1;
        private System.Windows.Forms.PictureBox p1;
        private System.Windows.Forms.GroupBox gbpP2;
        private System.Windows.Forms.PictureBox p2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cboSecondaries;
        private System.Windows.Forms.Button btnSecondaryView;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txtEnd;
        private System.Windows.Forms.TextBox txtStart;
        private System.Windows.Forms.TextBox txtsEnd;
        private System.Windows.Forms.TextBox txtsStart;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gTFAnnotationFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem repeatAnnotationFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showRepeatsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewReadDataToolStripMenuItem;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripMenuItem variantDeterminationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedReadsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearSelectedReadsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inversionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem translocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem switchRegionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem variantTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem historyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem primaryAlignmentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem secondaryAlignmentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem geneCoordinatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem showPositionOfCursorToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem alignerStringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lookForIndelsWithinAReadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useSoftClipDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usePrimaryAlignmentsCIGARStringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openBAMFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem;
    }
}

