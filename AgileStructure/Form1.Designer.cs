
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            groupBox1 = new System.Windows.Forms.GroupBox();
            txtEnd = new System.Windows.Forms.TextBox();
            txtStart = new System.Windows.Forms.TextBox();
            cboRef = new System.Windows.Forms.ComboBox();
            btnGetReads = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            gbpP1 = new System.Windows.Forms.GroupBox();
            p1 = new System.Windows.Forms.PictureBox();
            gbpP2 = new System.Windows.Forms.GroupBox();
            p2 = new System.Windows.Forms.PictureBox();
            groupBox4 = new System.Windows.Forms.GroupBox();
            btnFilter = new System.Windows.Forms.Button();
            txtsEnd = new System.Windows.Forms.TextBox();
            txtsStart = new System.Windows.Forms.TextBox();
            cboSecondaries = new System.Windows.Forms.ComboBox();
            btnSecondaryView = new System.Windows.Forms.Button();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            timer1 = new System.Windows.Forms.Timer(components);
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            openBAMFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            onlyShowReadsWithALargeIndelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            lookForIndelsWithinAReadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            gTFAnnotationFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            geneCoordinatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            repeatAnnotationFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            showRepeatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            showPositionOfCursorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            viewReadDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            saveSelectedReadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            clearSelectedReadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            alignerStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            variantDeterminationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            useSoftClipDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            variantTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            deletionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            duplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            insertionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            inversionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            translocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            usePrimaryAlignmentsCIGARStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            deletionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            insertationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            switchRegionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            historyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            primaryAlignmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            secondaryAlignmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            timer2 = new System.Windows.Forms.Timer(components);
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            btnQuit = new System.Windows.Forms.Button();
            groupBox1.SuspendLayout();
            gbpP1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)p1).BeginInit();
            gbpP2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)p2).BeginInit();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBox1.Controls.Add(txtEnd);
            groupBox1.Controls.Add(txtStart);
            groupBox1.Controls.Add(cboRef);
            groupBox1.Controls.Add(btnGetReads);
            groupBox1.Controls.Add(button2);
            groupBox1.Location = new System.Drawing.Point(6, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(1157, 72);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Region selection";
            // 
            // txtEnd
            // 
            txtEnd.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            txtEnd.Location = new System.Drawing.Point(922, 24);
            txtEnd.Name = "txtEnd";
            txtEnd.Size = new System.Drawing.Size(132, 23);
            txtEnd.TabIndex = 4;
            txtEnd.TextChanged += txtEnd_TextChanged;
            txtEnd.KeyDown += txtNumber_KeyDown;
            txtEnd.Leave += txtEnd_Leave;
            txtEnd.Validating += txtNumber_Validating;
            // 
            // txtStart
            // 
            txtStart.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            txtStart.Location = new System.Drawing.Point(782, 24);
            txtStart.Name = "txtStart";
            txtStart.Size = new System.Drawing.Size(132, 23);
            txtStart.TabIndex = 3;
            txtStart.TextChanged += txtStart_TextChanged;
            txtStart.KeyDown += txtNumber_KeyDown;
            txtStart.Leave += txtStart_Leave;
            txtStart.Validating += txtNumber_Validating;
            // 
            // cboRef
            // 
            cboRef.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cboRef.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboRef.FormattingEnabled = true;
            cboRef.Location = new System.Drawing.Point(101, 24);
            cboRef.Name = "cboRef";
            cboRef.Size = new System.Drawing.Size(673, 23);
            cboRef.TabIndex = 2;
            cboRef.SelectedIndexChanged += cboRef_SelectedIndexChanged;
            // 
            // btnGetReads
            // 
            btnGetReads.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnGetReads.Location = new System.Drawing.Point(1062, 22);
            btnGetReads.Name = "btnGetReads";
            btnGetReads.Size = new System.Drawing.Size(87, 27);
            btnGetReads.TabIndex = 5;
            btnGetReads.Text = "Get reads";
            btnGetReads.UseVisualStyleBackColor = true;
            btnGetReads.Click += button4_Click;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(7, 22);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(87, 27);
            button2.TabIndex = 1;
            button2.Text = "BAM file";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // gbpP1
            // 
            gbpP1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            gbpP1.Controls.Add(p1);
            gbpP1.Location = new System.Drawing.Point(6, 75);
            gbpP1.Name = "gbpP1";
            gbpP1.Size = new System.Drawing.Size(1157, 255);
            gbpP1.TabIndex = 6;
            gbpP1.TabStop = false;
            // 
            // p1
            // 
            p1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            p1.Location = new System.Drawing.Point(3, 19);
            p1.Name = "p1";
            p1.Size = new System.Drawing.Size(1151, 233);
            p1.TabIndex = 0;
            p1.TabStop = false;
            p1.MouseClick += p1_MouseClick;
            p1.MouseDown += p1_MouseDown;
            p1.MouseLeave += p1_MouseLeave;
            p1.MouseMove += p1_MouseMove;
            p1.MouseUp += p1_MouseUp;
            // 
            // gbpP2
            // 
            gbpP2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            gbpP2.Controls.Add(p2);
            gbpP2.Location = new System.Drawing.Point(6, 75);
            gbpP2.Name = "gbpP2";
            gbpP2.Size = new System.Drawing.Size(1157, 249);
            gbpP2.TabIndex = 2;
            gbpP2.TabStop = false;
            // 
            // p2
            // 
            p2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            p2.Location = new System.Drawing.Point(3, 19);
            p2.Name = "p2";
            p2.Size = new System.Drawing.Size(1151, 228);
            p2.TabIndex = 0;
            p2.TabStop = false;
            p2.MouseClick += p2_MouseClick;
            p2.MouseDown += p2_MouseDown;
            p2.MouseLeave += p2_MouseLeave;
            p2.MouseMove += p2_MouseMove;
            p2.MouseUp += p2_MouseUp;
            // 
            // groupBox4
            // 
            groupBox4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBox4.Controls.Add(btnFilter);
            groupBox4.Controls.Add(txtsEnd);
            groupBox4.Controls.Add(txtsStart);
            groupBox4.Controls.Add(cboSecondaries);
            groupBox4.Controls.Add(btnSecondaryView);
            groupBox4.Location = new System.Drawing.Point(6, 3);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(1157, 65);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "Secondary alignment region";
            // 
            // btnFilter
            // 
            btnFilter.Location = new System.Drawing.Point(11, 22);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new System.Drawing.Size(87, 27);
            btnFilter.TabIndex = 8;
            btnFilter.Text = "Filter";
            btnFilter.UseVisualStyleBackColor = true;
            btnFilter.Click += btnFilter_Click;
            // 
            // txtsEnd
            // 
            txtsEnd.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            txtsEnd.Location = new System.Drawing.Point(923, 24);
            txtsEnd.Name = "txtsEnd";
            txtsEnd.Size = new System.Drawing.Size(132, 23);
            txtsEnd.TabIndex = 11;
            txtsEnd.TextChanged += txtsEnd_TextChanged;
            txtsEnd.KeyDown += txtNumber_KeyDown;
            txtsEnd.Leave += txtsEnd_Leave;
            txtsEnd.Validating += txtNumber_Validating;
            // 
            // txtsStart
            // 
            txtsStart.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            txtsStart.Location = new System.Drawing.Point(783, 24);
            txtsStart.Name = "txtsStart";
            txtsStart.Size = new System.Drawing.Size(132, 23);
            txtsStart.TabIndex = 10;
            txtsStart.TextChanged += txtsStart_TextChanged;
            txtsStart.KeyDown += txtNumber_KeyDown;
            txtsStart.Leave += txtsStart_Leave;
            txtsStart.Validating += txtNumber_Validating;
            // 
            // cboSecondaries
            // 
            cboSecondaries.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cboSecondaries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboSecondaries.FormattingEnabled = true;
            cboSecondaries.Location = new System.Drawing.Point(101, 24);
            cboSecondaries.Name = "cboSecondaries";
            cboSecondaries.Size = new System.Drawing.Size(674, 23);
            cboSecondaries.TabIndex = 9;
            cboSecondaries.SelectedIndexChanged += cboSecondaries_SelectedIndexChanged;
            // 
            // btnSecondaryView
            // 
            btnSecondaryView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnSecondaryView.Location = new System.Drawing.Point(1063, 22);
            btnSecondaryView.Name = "btnSecondaryView";
            btnSecondaryView.Size = new System.Drawing.Size(87, 27);
            btnSecondaryView.TabIndex = 12;
            btnSecondaryView.Text = "Select";
            btnSecondaryView.UseVisualStyleBackColor = true;
            btnSecondaryView.Click += btnSecondaryView_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            splitContainer1.Location = new System.Drawing.Point(0, 20);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(groupBox1);
            splitContainer1.Panel1.Controls.Add(gbpP1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBox4);
            splitContainer1.Panel2.Controls.Add(gbpP2);
            splitContainer1.Size = new System.Drawing.Size(1167, 678);
            splitContainer1.SplitterDistance = 335;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 7;
            splitContainer1.SplitterMoved += splitContainer1_SplitterMoved;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem1, fileToolStripMenuItem, dataToolStripMenuItem, variantDeterminationToolStripMenuItem, historyToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            menuStrip1.Size = new System.Drawing.Size(1167, 24);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { openBAMFileToolStripMenuItem, toolStripMenuItem9, onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem, onlyShowReadsWithALargeIndelToolStripMenuItem, toolStripMenuItem7, lookForIndelsWithinAReadToolStripMenuItem });
            fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            fileToolStripMenuItem1.Size = new System.Drawing.Size(62, 22);
            fileToolStripMenuItem1.Text = "Analysis";
            // 
            // openBAMFileToolStripMenuItem
            // 
            openBAMFileToolStripMenuItem.Name = "openBAMFileToolStripMenuItem";
            openBAMFileToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            openBAMFileToolStripMenuItem.Text = "Open BAM file";
            openBAMFileToolStripMenuItem.Click += openBAMFileToolStripMenuItem_Click;
            // 
            // toolStripMenuItem9
            // 
            toolStripMenuItem9.Name = "toolStripMenuItem9";
            toolStripMenuItem9.Size = new System.Drawing.Size(303, 6);
            // 
            // onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem
            // 
            onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem.Name = "onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem";
            onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem.Text = "Only show reads with secondary alignments";
            onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem.Click += onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem_Click;
            // 
            // onlyShowReadsWithALargeIndelToolStripMenuItem
            // 
            onlyShowReadsWithALargeIndelToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            onlyShowReadsWithALargeIndelToolStripMenuItem.Name = "onlyShowReadsWithALargeIndelToolStripMenuItem";
            onlyShowReadsWithALargeIndelToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            onlyShowReadsWithALargeIndelToolStripMenuItem.Text = "Only show reads with a large indel";
            onlyShowReadsWithALargeIndelToolStripMenuItem.Click += onlyShowReadsWithALargeIndelToolStripMenuItem_Click;
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            toolStripMenuItem7.Size = new System.Drawing.Size(303, 6);
            // 
            // lookForIndelsWithinAReadToolStripMenuItem
            // 
            lookForIndelsWithinAReadToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            lookForIndelsWithinAReadToolStripMenuItem.Name = "lookForIndelsWithinAReadToolStripMenuItem";
            lookForIndelsWithinAReadToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            lookForIndelsWithinAReadToolStripMenuItem.Text = "Look for indels within a read";
            lookForIndelsWithinAReadToolStripMenuItem.Click += lookForIndelsWithinAReadToolStripMenuItem_Click;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { gTFAnnotationFileToolStripMenuItem, geneCoordinatesToolStripMenuItem, toolStripMenuItem4, repeatAnnotationFileToolStripMenuItem, showRepeatsToolStripMenuItem, toolStripMenuItem5, showPositionOfCursorToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(79, 22);
            fileToolStripMenuItem.Text = "Annotation";
            // 
            // gTFAnnotationFileToolStripMenuItem
            // 
            gTFAnnotationFileToolStripMenuItem.Name = "gTFAnnotationFileToolStripMenuItem";
            gTFAnnotationFileToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            gTFAnnotationFileToolStripMenuItem.Text = "Gene annotation file";
            gTFAnnotationFileToolStripMenuItem.Click += gTFAnnotationFileToolStripMenuItem_Click;
            // 
            // geneCoordinatesToolStripMenuItem
            // 
            geneCoordinatesToolStripMenuItem.Enabled = false;
            geneCoordinatesToolStripMenuItem.Name = "geneCoordinatesToolStripMenuItem";
            geneCoordinatesToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            geneCoordinatesToolStripMenuItem.Text = "Gene coordinates";
            geneCoordinatesToolStripMenuItem.Click += geneCoordinatesToolStripMenuItem_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new System.Drawing.Size(218, 6);
            // 
            // repeatAnnotationFileToolStripMenuItem
            // 
            repeatAnnotationFileToolStripMenuItem.Name = "repeatAnnotationFileToolStripMenuItem";
            repeatAnnotationFileToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            repeatAnnotationFileToolStripMenuItem.Text = "Select repeat annotation file";
            repeatAnnotationFileToolStripMenuItem.Click += repeatAnnotationFileToolStripMenuItem_Click;
            // 
            // showRepeatsToolStripMenuItem
            // 
            showRepeatsToolStripMenuItem.Enabled = false;
            showRepeatsToolStripMenuItem.Name = "showRepeatsToolStripMenuItem";
            showRepeatsToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            showRepeatsToolStripMenuItem.Text = "Show repeats";
            showRepeatsToolStripMenuItem.Click += showRepeatsToolStripMenuItem_Click;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new System.Drawing.Size(218, 6);
            // 
            // showPositionOfCursorToolStripMenuItem
            // 
            showPositionOfCursorToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            showPositionOfCursorToolStripMenuItem.Name = "showPositionOfCursorToolStripMenuItem";
            showPositionOfCursorToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            showPositionOfCursorToolStripMenuItem.Text = "Show position of cursor";
            showPositionOfCursorToolStripMenuItem.Click += showPositionOfCursorToolStripMenuItem_Click;
            // 
            // dataToolStripMenuItem
            // 
            dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { viewReadDataToolStripMenuItem, toolStripMenuItem1, saveSelectedReadsToolStripMenuItem, clearSelectedReadsToolStripMenuItem, toolStripMenuItem6, alignerStringToolStripMenuItem });
            dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            dataToolStripMenuItem.Size = new System.Drawing.Size(43, 22);
            dataToolStripMenuItem.Text = "Data";
            // 
            // viewReadDataToolStripMenuItem
            // 
            viewReadDataToolStripMenuItem.Name = "viewReadDataToolStripMenuItem";
            viewReadDataToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            viewReadDataToolStripMenuItem.Text = "View read data";
            viewReadDataToolStripMenuItem.Click += viewReadDataToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(175, 6);
            // 
            // saveSelectedReadsToolStripMenuItem
            // 
            saveSelectedReadsToolStripMenuItem.Enabled = false;
            saveSelectedReadsToolStripMenuItem.Name = "saveSelectedReadsToolStripMenuItem";
            saveSelectedReadsToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            saveSelectedReadsToolStripMenuItem.Text = "Save selected reads";
            saveSelectedReadsToolStripMenuItem.Click += saveSelectedReadsToolStripMenuItem_Click;
            // 
            // clearSelectedReadsToolStripMenuItem
            // 
            clearSelectedReadsToolStripMenuItem.Name = "clearSelectedReadsToolStripMenuItem";
            clearSelectedReadsToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            clearSelectedReadsToolStripMenuItem.Text = "Clear selected reads";
            clearSelectedReadsToolStripMenuItem.Click += clearSelectedReadsToolStripMenuItem_Click;
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new System.Drawing.Size(175, 6);
            // 
            // alignerStringToolStripMenuItem
            // 
            alignerStringToolStripMenuItem.Name = "alignerStringToolStripMenuItem";
            alignerStringToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            alignerStringToolStripMenuItem.Text = "Aligner string";
            alignerStringToolStripMenuItem.Click += alignerStringToolStripMenuItem_Click;
            // 
            // variantDeterminationToolStripMenuItem
            // 
            variantDeterminationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem3, useSoftClipDataToolStripMenuItem, usePrimaryAlignmentsCIGARStringToolStripMenuItem, toolStripMenuItem8, switchRegionToolStripMenuItem });
            variantDeterminationToolStripMenuItem.Name = "variantDeterminationToolStripMenuItem";
            variantDeterminationToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            variantDeterminationToolStripMenuItem.Text = "Variant determination";
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new System.Drawing.Size(269, 6);
            // 
            // useSoftClipDataToolStripMenuItem
            // 
            useSoftClipDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { variantTypeToolStripMenuItem, toolStripMenuItem2, deletionsToolStripMenuItem, duplicationToolStripMenuItem, insertionToolStripMenuItem, inversionToolStripMenuItem, translocationToolStripMenuItem });
            useSoftClipDataToolStripMenuItem.Name = "useSoftClipDataToolStripMenuItem";
            useSoftClipDataToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            useSoftClipDataToolStripMenuItem.Text = "Use soft clip data";
            // 
            // variantTypeToolStripMenuItem
            // 
            variantTypeToolStripMenuItem.Name = "variantTypeToolStripMenuItem";
            variantTypeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            variantTypeToolStripMenuItem.Text = "Variant type";
            variantTypeToolStripMenuItem.Click += variantTypeToolStripMenuItem_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
            // 
            // deletionsToolStripMenuItem
            // 
            deletionsToolStripMenuItem.Name = "deletionsToolStripMenuItem";
            deletionsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            deletionsToolStripMenuItem.Text = "Deletion";
            deletionsToolStripMenuItem.Click += deletionsToolStripMenuItem_Click;
            // 
            // duplicationToolStripMenuItem
            // 
            duplicationToolStripMenuItem.Name = "duplicationToolStripMenuItem";
            duplicationToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            duplicationToolStripMenuItem.Text = "Duplication";
            duplicationToolStripMenuItem.Click += duplicationToolStripMenuItem_Click;
            // 
            // insertionToolStripMenuItem
            // 
            insertionToolStripMenuItem.Name = "insertionToolStripMenuItem";
            insertionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            insertionToolStripMenuItem.Text = "Insertion";
            insertionToolStripMenuItem.Click += insertionToolStripMenuItem_Click;
            // 
            // inversionToolStripMenuItem
            // 
            inversionToolStripMenuItem.Name = "inversionToolStripMenuItem";
            inversionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            inversionToolStripMenuItem.Text = "Inversion";
            inversionToolStripMenuItem.Click += inversionToolStripMenuItem_Click;
            // 
            // translocationToolStripMenuItem
            // 
            translocationToolStripMenuItem.Name = "translocationToolStripMenuItem";
            translocationToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            translocationToolStripMenuItem.Text = "Translocation";
            translocationToolStripMenuItem.Click += translocationToolStripMenuItem_Click;
            // 
            // usePrimaryAlignmentsCIGARStringToolStripMenuItem
            // 
            usePrimaryAlignmentsCIGARStringToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { deletionToolStripMenuItem, insertationToolStripMenuItem });
            usePrimaryAlignmentsCIGARStringToolStripMenuItem.Name = "usePrimaryAlignmentsCIGARStringToolStripMenuItem";
            usePrimaryAlignmentsCIGARStringToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            usePrimaryAlignmentsCIGARStringToolStripMenuItem.Text = "Use Primary alignment's CIGAR string";
            // 
            // deletionToolStripMenuItem
            // 
            deletionToolStripMenuItem.Name = "deletionToolStripMenuItem";
            deletionToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            deletionToolStripMenuItem.Text = "Deletion";
            deletionToolStripMenuItem.Click += deletionToolStripMenuItem_Click;
            // 
            // insertationToolStripMenuItem
            // 
            insertationToolStripMenuItem.Name = "insertationToolStripMenuItem";
            insertationToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            insertationToolStripMenuItem.Text = "Insertion";
            insertationToolStripMenuItem.Click += insertationToolStripMenuItem_Click;
            // 
            // toolStripMenuItem8
            // 
            toolStripMenuItem8.Name = "toolStripMenuItem8";
            toolStripMenuItem8.Size = new System.Drawing.Size(269, 6);
            // 
            // switchRegionToolStripMenuItem
            // 
            switchRegionToolStripMenuItem.Name = "switchRegionToolStripMenuItem";
            switchRegionToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            switchRegionToolStripMenuItem.Text = "Switch region";
            switchRegionToolStripMenuItem.Click += switchRegionToolStripMenuItem_Click;
            // 
            // historyToolStripMenuItem
            // 
            historyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { primaryAlignmentsToolStripMenuItem, secondaryAlignmentsToolStripMenuItem });
            historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            historyToolStripMenuItem.Size = new System.Drawing.Size(57, 22);
            historyToolStripMenuItem.Text = "History";
            // 
            // primaryAlignmentsToolStripMenuItem
            // 
            primaryAlignmentsToolStripMenuItem.Name = "primaryAlignmentsToolStripMenuItem";
            primaryAlignmentsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            primaryAlignmentsToolStripMenuItem.Text = "Primary alignments";
            // 
            // secondaryAlignmentsToolStripMenuItem
            // 
            secondaryAlignmentsToolStripMenuItem.Name = "secondaryAlignmentsToolStripMenuItem";
            secondaryAlignmentsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            secondaryAlignmentsToolStripMenuItem.Text = "Secondary alignments";
            // 
            // timer2
            // 
            timer2.Interval = 250;
            timer2.Tick += timer2_Tick;
            // 
            // btnQuit
            // 
            btnQuit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnQuit.Location = new System.Drawing.Point(1065, 704);
            btnQuit.Name = "btnQuit";
            btnQuit.Size = new System.Drawing.Size(87, 27);
            btnQuit.TabIndex = 8;
            btnQuit.Text = "Quit";
            btnQuit.UseVisualStyleBackColor = true;
            btnQuit.Click += btnQuit_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1167, 744);
            Controls.Add(btnQuit);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MinimumSize = new System.Drawing.Size(1179, 766);
            Name = "Form1";
            Text = "Genomic rearrangements";
            Load += Form1_Load;
            Resize += Form1_Resize;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            gbpP1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)p1).EndInit();
            gbpP2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)p2).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem onlyShowReadsWithALargeIndelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unbalancedTranslocationToolStripMenuItem;
    }
}

