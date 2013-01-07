namespace BIDS2008R2Downgrade
{
  partial class FrmTest
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
      this.lblChooseReport = new System.Windows.Forms.Label();
      this.cmdLoad = new System.Windows.Forms.Button();
      this.cmdDowngrade = new System.Windows.Forms.Button();
      this.cmdSaveAs = new System.Windows.Forms.Button();
      this.dlgSaveAs = new System.Windows.Forms.SaveFileDialog();
      this.cboErrorLevel = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.lblExecutionResult = new System.Windows.Forms.Label();
      this.txtOutput = new System.Windows.Forms.RichTextBox();
      this.lblExecutionStatus = new System.Windows.Forms.Label();
      this.txtDowngradeStatus = new System.Windows.Forms.RichTextBox();
      this.txtReportPath = new System.Windows.Forms.TextBox();
      this.cmdBrowseReport = new System.Windows.Forms.Button();
      this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblChooseReport
      // 
      this.lblChooseReport.AutoSize = true;
      this.lblChooseReport.Location = new System.Drawing.Point(12, 9);
      this.lblChooseReport.Name = "lblChooseReport";
      this.lblChooseReport.Size = new System.Drawing.Size(92, 13);
      this.lblChooseReport.TabIndex = 0;
      this.lblChooseReport.Text = "Choose report file:";
      // 
      // cmdLoad
      // 
      this.cmdLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdLoad.Location = new System.Drawing.Point(690, 25);
      this.cmdLoad.Name = "cmdLoad";
      this.cmdLoad.Size = new System.Drawing.Size(75, 23);
      this.cmdLoad.TabIndex = 3;
      this.cmdLoad.Text = "Load";
      this.cmdLoad.UseVisualStyleBackColor = true;
      this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
      // 
      // cmdDowngrade
      // 
      this.cmdDowngrade.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdDowngrade.Location = new System.Drawing.Point(609, 54);
      this.cmdDowngrade.Name = "cmdDowngrade";
      this.cmdDowngrade.Size = new System.Drawing.Size(75, 23);
      this.cmdDowngrade.TabIndex = 6;
      this.cmdDowngrade.Text = "Downgrade";
      this.cmdDowngrade.UseVisualStyleBackColor = true;
      this.cmdDowngrade.Click += new System.EventHandler(this.cmdDowngrade_Click);
      // 
      // cmdSaveAs
      // 
      this.cmdSaveAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdSaveAs.Location = new System.Drawing.Point(690, 54);
      this.cmdSaveAs.Name = "cmdSaveAs";
      this.cmdSaveAs.Size = new System.Drawing.Size(75, 23);
      this.cmdSaveAs.TabIndex = 7;
      this.cmdSaveAs.Text = "Save As";
      this.cmdSaveAs.UseVisualStyleBackColor = true;
      this.cmdSaveAs.Click += new System.EventHandler(this.cmdSaveAs_Click);
      // 
      // dlgSaveAs
      // 
      this.dlgSaveAs.Filter = "SSRS Reports (*.rdl)|*.rdl|All Files (*.*)|*.*";
      this.dlgSaveAs.Title = "Save As";
      // 
      // cboErrorLevel
      // 
      this.cboErrorLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cboErrorLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboErrorLevel.FormattingEnabled = true;
      this.cboErrorLevel.Items.AddRange(new object[] {
            "FatalError (0)",
            "Drastic Layout Error (1)",
            "Significant Layout Error (2)",
            "Minor Layout Error (3)",
            "Warning (4)"});
      this.cboErrorLevel.Location = new System.Drawing.Point(383, 56);
      this.cboErrorLevel.Name = "cboErrorLevel";
      this.cboErrorLevel.Size = new System.Drawing.Size(220, 21);
      this.cboErrorLevel.TabIndex = 5;
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(320, 59);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(57, 13);
      this.label1.TabIndex = 4;
      this.label1.Text = "Error level:";
      // 
      // splitContainer1
      // 
      this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer1.Location = new System.Drawing.Point(15, 83);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.lblExecutionResult);
      this.splitContainer1.Panel1.Controls.Add(this.txtOutput);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.lblExecutionStatus);
      this.splitContainer1.Panel2.Controls.Add(this.txtDowngradeStatus);
      this.splitContainer1.Size = new System.Drawing.Size(750, 474);
      this.splitContainer1.SplitterDistance = 257;
      this.splitContainer1.TabIndex = 11;
      // 
      // lblExecutionResult
      // 
      this.lblExecutionResult.AutoSize = true;
      this.lblExecutionResult.Location = new System.Drawing.Point(-3, 0);
      this.lblExecutionResult.Name = "lblExecutionResult";
      this.lblExecutionResult.Size = new System.Drawing.Size(85, 13);
      this.lblExecutionResult.TabIndex = 0;
      this.lblExecutionResult.Text = "Execution result:";
      // 
      // txtOutput
      // 
      this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtOutput.Location = new System.Drawing.Point(0, 16);
      this.txtOutput.Name = "txtOutput";
      this.txtOutput.Size = new System.Drawing.Size(750, 238);
      this.txtOutput.TabIndex = 1;
      this.txtOutput.Text = "";
      // 
      // lblExecutionStatus
      // 
      this.lblExecutionStatus.AutoSize = true;
      this.lblExecutionStatus.Location = new System.Drawing.Point(3, 0);
      this.lblExecutionStatus.Name = "lblExecutionStatus";
      this.lblExecutionStatus.Size = new System.Drawing.Size(96, 13);
      this.lblExecutionStatus.TabIndex = 0;
      this.lblExecutionStatus.Text = "Downgrade status:";
      // 
      // txtDowngradeStatus
      // 
      this.txtDowngradeStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDowngradeStatus.Location = new System.Drawing.Point(0, 19);
      this.txtDowngradeStatus.Name = "txtDowngradeStatus";
      this.txtDowngradeStatus.Size = new System.Drawing.Size(750, 194);
      this.txtDowngradeStatus.TabIndex = 1;
      this.txtDowngradeStatus.Text = "";
      // 
      // txtReportPath
      // 
      this.txtReportPath.Location = new System.Drawing.Point(15, 28);
      this.txtReportPath.Name = "txtReportPath";
      this.txtReportPath.Size = new System.Drawing.Size(588, 20);
      this.txtReportPath.TabIndex = 1;
      // 
      // cmdBrowseReport
      // 
      this.cmdBrowseReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdBrowseReport.Location = new System.Drawing.Point(609, 25);
      this.cmdBrowseReport.Name = "cmdBrowseReport";
      this.cmdBrowseReport.Size = new System.Drawing.Size(75, 23);
      this.cmdBrowseReport.TabIndex = 2;
      this.cmdBrowseReport.Text = "Browse ...";
      this.cmdBrowseReport.UseVisualStyleBackColor = true;
      this.cmdBrowseReport.Click += new System.EventHandler(this.cmdBrowseReport_Click);
      // 
      // dlgOpen
      // 
      this.dlgOpen.Filter = "SSRS Reports (*.rdl)|*.rdl|All Files (*.*)|*.*";
      this.dlgOpen.Title = "Choose a Reporting Services report:";
      // 
      // FrmTest
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(774, 564);
      this.Controls.Add(this.cmdBrowseReport);
      this.Controls.Add(this.txtReportPath);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cboErrorLevel);
      this.Controls.Add(this.cmdSaveAs);
      this.Controls.Add(this.cmdDowngrade);
      this.Controls.Add(this.cmdLoad);
      this.Controls.Add(this.lblChooseReport);
      this.Name = "FrmTest";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Reporting Services 2008 R2 Downgrade";
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblChooseReport;
    private System.Windows.Forms.Button cmdLoad;
    private System.Windows.Forms.Button cmdDowngrade;
    private System.Windows.Forms.Button cmdSaveAs;
    private System.Windows.Forms.SaveFileDialog dlgSaveAs;
    private System.Windows.Forms.ComboBox cboErrorLevel;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Label lblExecutionResult;
    private System.Windows.Forms.RichTextBox txtOutput;
    private System.Windows.Forms.Label lblExecutionStatus;
    private System.Windows.Forms.RichTextBox txtDowngradeStatus;
    private System.Windows.Forms.TextBox txtReportPath;
    private System.Windows.Forms.Button cmdBrowseReport;
    private System.Windows.Forms.OpenFileDialog dlgOpen;
  }
}

