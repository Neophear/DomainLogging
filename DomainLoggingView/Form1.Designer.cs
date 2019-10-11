namespace DomainLoggingView
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
            this.txtSQL = new System.Windows.Forms.TextBox();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.stsBottom = new System.Windows.Forms.StatusStrip();
            this.tssBottom = new System.Windows.Forms.ToolStripStatusLabel();
            this.cbbPresets = new System.Windows.Forms.ComboBox();
            this.btnRunSQL = new System.Windows.Forms.Button();
            this.btnFillPreset = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.stsBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSQL
            // 
            this.txtSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQL.Font = new System.Drawing.Font("Consolas", 9.25F);
            this.txtSQL.Location = new System.Drawing.Point(0, 30);
            this.txtSQL.Multiline = true;
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSQL.Size = new System.Drawing.Size(593, 158);
            this.txtSQL.TabIndex = 2;
            this.txtSQL.WordWrap = false;
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AllowUserToResizeRows = false;
            this.dgvResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(0, 0);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvResult.Size = new System.Drawing.Size(593, 292);
            this.dgvResult.TabIndex = 0;
            // 
            // stsBottom
            // 
            this.stsBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssBottom});
            this.stsBottom.Location = new System.Drawing.Point(0, 484);
            this.stsBottom.Name = "stsBottom";
            this.stsBottom.Size = new System.Drawing.Size(593, 22);
            this.stsBottom.SizingGrip = false;
            this.stsBottom.TabIndex = 5;
            // 
            // tssBottom
            // 
            this.tssBottom.Name = "tssBottom";
            this.tssBottom.Size = new System.Drawing.Size(61, 17);
            this.tssBottom.Text = "tssBottom";
            // 
            // cbbPresets
            // 
            this.cbbPresets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbPresets.DisplayMember = "Name";
            this.cbbPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPresets.FormattingEnabled = true;
            this.cbbPresets.Location = new System.Drawing.Point(3, 3);
            this.cbbPresets.Name = "cbbPresets";
            this.cbbPresets.Size = new System.Drawing.Size(235, 21);
            this.cbbPresets.TabIndex = 4;
            this.cbbPresets.ValueMember = "Value";
            // 
            // btnRunSQL
            // 
            this.btnRunSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunSQL.Location = new System.Drawing.Point(340, 3);
            this.btnRunSQL.Name = "btnRunSQL";
            this.btnRunSQL.Size = new System.Drawing.Size(90, 23);
            this.btnRunSQL.TabIndex = 5;
            this.btnRunSQL.Text = "Kør SQL";
            this.btnRunSQL.UseVisualStyleBackColor = true;
            this.btnRunSQL.Click += new System.EventHandler(this.btnRunSQL_Click);
            // 
            // btnFillPreset
            // 
            this.btnFillPreset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFillPreset.Location = new System.Drawing.Point(244, 3);
            this.btnFillPreset.Name = "btnFillPreset";
            this.btnFillPreset.Size = new System.Drawing.Size(90, 23);
            this.btnFillPreset.TabIndex = 3;
            this.btnFillPreset.Text = "Indsæt preset";
            this.btnFillPreset.UseVisualStyleBackColor = true;
            this.btnFillPreset.Click += new System.EventHandler(this.btnFillPreset_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtSQL);
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Panel1MinSize = 100;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvResult);
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(593, 484);
            this.splitContainer1.SplitterDistance = 188;
            this.splitContainer1.TabIndex = 6;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cbbPresets);
            this.flowLayoutPanel1.Controls.Add(this.btnFillPreset);
            this.flowLayoutPanel1.Controls.Add(this.btnRunSQL);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(593, 30);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 506);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.stsBottom);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(450, 400);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Hent log";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.stsBottom.ResumeLayout(false);
            this.stsBottom.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSQL;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.StatusStrip stsBottom;
        private System.Windows.Forms.ToolStripStatusLabel tssBottom;
        private System.Windows.Forms.ComboBox cbbPresets;
        private System.Windows.Forms.Button btnRunSQL;
        private System.Windows.Forms.Button btnFillPreset;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}

