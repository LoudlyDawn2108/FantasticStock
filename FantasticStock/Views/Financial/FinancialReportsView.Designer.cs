using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FantasticStock.Views.Financial
{
    partial class FinancialReportsView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageParameters = new System.Windows.Forms.TabPage();
            this.panelParameters = new System.Windows.Forms.Panel();
            this.groupBoxDisplayOptions = new System.Windows.Forms.GroupBox();
            this.cboDisplayOptions = new System.Windows.Forms.ComboBox();
            this.cboDetailLevel = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBoxAccounts = new System.Windows.Forms.GroupBox();
            this.tvAccounts = new System.Windows.Forms.TreeView();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.groupBoxDateRange = new System.Windows.Forms.GroupBox();
            this.dtpComparisonEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblComparisonEndDate = new System.Windows.Forms.Label();
            this.dtpComparisonStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblComparisonStartDate = new System.Windows.Forms.Label();
            this.cboComparisonPeriod = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxReportType = new System.Windows.Forms.GroupBox();
            this.cboReportType = new System.Windows.Forms.ComboBox();
            this.lblReportType = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabPageReport = new System.Windows.Forms.TabPage();
            this.panelReport = new System.Windows.Forms.Panel();
            this.flpReportContent = new System.Windows.Forms.FlowLayoutPanel();
            this.panelReportHeader = new System.Windows.Forms.Panel();
            this.btnExportReport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPageParameters.SuspendLayout();
            this.panelParameters.SuspendLayout();
            this.groupBoxDisplayOptions.SuspendLayout();
            this.groupBoxAccounts.SuspendLayout();
            this.groupBoxDateRange.SuspendLayout();
            this.groupBoxReportType.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.tabPageReport.SuspendLayout();
            this.panelReport.SuspendLayout();
            this.panelReportHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageParameters);
            this.tabControl.Controls.Add(this.tabPageReport);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(950, 600);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageParameters
            // 
            this.tabPageParameters.Controls.Add(this.panelParameters);
            this.tabPageParameters.Controls.Add(this.panelHeader);
            this.tabPageParameters.Location = new System.Drawing.Point(4, 22);
            this.tabPageParameters.Name = "tabPageParameters";
            this.tabPageParameters.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageParameters.Size = new System.Drawing.Size(942, 574);
            this.tabPageParameters.TabIndex = 0;
            this.tabPageParameters.Text = "Report Parameters";
            this.tabPageParameters.UseVisualStyleBackColor = true;
            // 
            // panelParameters
            // 
            this.panelParameters.Controls.Add(this.groupBoxDisplayOptions);
            this.panelParameters.Controls.Add(this.groupBoxAccounts);
            this.panelParameters.Controls.Add(this.btnGenerateReport);
            this.panelParameters.Controls.Add(this.groupBoxDateRange);
            this.panelParameters.Controls.Add(this.groupBoxReportType);
            this.panelParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelParameters.Location = new System.Drawing.Point(3, 53);
            this.panelParameters.Name = "panelParameters";
            this.panelParameters.Size = new System.Drawing.Size(936, 518);
            this.panelParameters.TabIndex = 1;
            // 
            // groupBoxDisplayOptions
            // 
            this.groupBoxDisplayOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDisplayOptions.Controls.Add(this.cboDisplayOptions);
            this.groupBoxDisplayOptions.Controls.Add(this.cboDetailLevel);
            this.groupBoxDisplayOptions.Controls.Add(this.label7);
            this.groupBoxDisplayOptions.Controls.Add(this.label8);
            this.groupBoxDisplayOptions.Location = new System.Drawing.Point(498, 212);
            this.groupBoxDisplayOptions.Name = "groupBoxDisplayOptions";
            this.groupBoxDisplayOptions.Size = new System.Drawing.Size(430, 100);
            this.groupBoxDisplayOptions.TabIndex = 4;
            this.groupBoxDisplayOptions.TabStop = false;
            this.groupBoxDisplayOptions.Text = "Display Options";
            // 
            // cboDisplayOptions
            // 
            this.cboDisplayOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDisplayOptions.FormattingEnabled = true;
            this.cboDisplayOptions.Location = new System.Drawing.Point(117, 59);
            this.cboDisplayOptions.Name = "cboDisplayOptions";
            this.cboDisplayOptions.Size = new System.Drawing.Size(293, 21);
            this.cboDisplayOptions.TabIndex = 3;
            // 
            // cboDetailLevel
            // 
            this.cboDetailLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDetailLevel.FormattingEnabled = true;
            this.cboDetailLevel.Location = new System.Drawing.Point(117, 29);
            this.cboDetailLevel.Name = "cboDetailLevel";
            this.cboDetailLevel.Size = new System.Drawing.Size(293, 21);
            this.cboDetailLevel.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Display Options:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Detail Level:";
            // 
            // groupBoxAccounts
            // 
            this.groupBoxAccounts.Controls.Add(this.tvAccounts);
            this.groupBoxAccounts.Location = new System.Drawing.Point(10, 212);
            this.groupBoxAccounts.Name = "groupBoxAccounts";
            this.groupBoxAccounts.Size = new System.Drawing.Size(463, 242);
            this.groupBoxAccounts.TabIndex = 3;
            this.groupBoxAccounts.TabStop = false;
            this.groupBoxAccounts.Text = "Select Accounts";
            // 
            // tvAccounts
            // 
            this.tvAccounts.CheckBoxes = true;
            this.tvAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvAccounts.Location = new System.Drawing.Point(3, 16);
            this.tvAccounts.Name = "tvAccounts";
            this.tvAccounts.Size = new System.Drawing.Size(457, 223);
            this.tvAccounts.TabIndex = 0;
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateReport.Location = new System.Drawing.Point(738, 460);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(190, 35);
            this.btnGenerateReport.TabIndex = 2;
            this.btnGenerateReport.Text = "Generate Report";
            this.btnGenerateReport.UseVisualStyleBackColor = true;
            // 
            // groupBoxDateRange
            // 
            this.groupBoxDateRange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDateRange.Controls.Add(this.dtpComparisonEndDate);
            this.groupBoxDateRange.Controls.Add(this.lblComparisonEndDate);
            this.groupBoxDateRange.Controls.Add(this.dtpComparisonStartDate);
            this.groupBoxDateRange.Controls.Add(this.lblComparisonStartDate);
            this.groupBoxDateRange.Controls.Add(this.cboComparisonPeriod);
            this.groupBoxDateRange.Controls.Add(this.label6);
            this.groupBoxDateRange.Controls.Add(this.dtpEndDate);
            this.groupBoxDateRange.Controls.Add(this.label5);
            this.groupBoxDateRange.Controls.Add(this.dtpStartDate);
            this.groupBoxDateRange.Controls.Add(this.label4);
            this.groupBoxDateRange.Location = new System.Drawing.Point(10, 95);
            this.groupBoxDateRange.Name = "groupBoxDateRange";
            this.groupBoxDateRange.Size = new System.Drawing.Size(917, 111);
            this.groupBoxDateRange.TabIndex = 1;
            this.groupBoxDateRange.TabStop = false;
            this.groupBoxDateRange.Text = "Date Range";
            // 
            // dtpComparisonEndDate
            // 
            this.dtpComparisonEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpComparisonEndDate.Location = new System.Drawing.Point(617, 74);
            this.dtpComparisonEndDate.Name = "dtpComparisonEndDate";
            this.dtpComparisonEndDate.Size = new System.Drawing.Size(113, 20);
            this.dtpComparisonEndDate.TabIndex = 0;
            // 
            // lblComparisonEndDate
            // 
            this.lblComparisonEndDate.Location = new System.Drawing.Point(498, 77);
            this.lblComparisonEndDate.Name = "lblComparisonEndDate";
            this.lblComparisonEndDate.Size = new System.Drawing.Size(113, 13);
            this.lblComparisonEndDate.TabIndex = 8;
            this.lblComparisonEndDate.Text = "Comparison End Date:";
            // 
            // dtpComparisonStartDate
            // 
            this.dtpComparisonStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpComparisonStartDate.Location = new System.Drawing.Point(358, 74);
            this.dtpComparisonStartDate.Name = "dtpComparisonStartDate";
            this.dtpComparisonStartDate.Size = new System.Drawing.Size(119, 20);
            this.dtpComparisonStartDate.TabIndex = 7;
            // 
            // lblComparisonStartDate
            // 
            this.lblComparisonStartDate.AutoSize = true;
            this.lblComparisonStartDate.Location = new System.Drawing.Point(239, 77);
            this.lblComparisonStartDate.Name = "lblComparisonStartDate";
            this.lblComparisonStartDate.Size = new System.Drawing.Size(116, 13);
            this.lblComparisonStartDate.TabIndex = 6;
            this.lblComparisonStartDate.Text = "Comparison Start Date:";
            // 
            // cboComparisonPeriod
            // 
            this.cboComparisonPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboComparisonPeriod.FormattingEnabled = true;
            this.cboComparisonPeriod.Location = new System.Drawing.Point(117, 74);
            this.cboComparisonPeriod.Name = "cboComparisonPeriod";
            this.cboComparisonPeriod.Size = new System.Drawing.Size(115, 21);
            this.cboComparisonPeriod.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Comparison Period:";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(358, 34);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(119, 20);
            this.dtpEndDate.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(301, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "End Date:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(117, 34);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(119, 20);
            this.dtpStartDate.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Start Date:";
            // 
            // groupBoxReportType
            // 
            this.groupBoxReportType.Controls.Add(this.cboReportType);
            this.groupBoxReportType.Controls.Add(this.lblReportType);
            this.groupBoxReportType.Location = new System.Drawing.Point(10, 22);
            this.groupBoxReportType.Name = "groupBoxReportType";
            this.groupBoxReportType.Size = new System.Drawing.Size(917, 67);
            this.groupBoxReportType.TabIndex = 0;
            this.groupBoxReportType.TabStop = false;
            this.groupBoxReportType.Text = "Report Type";
            // 
            // cboReportType
            // 
            this.cboReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReportType.FormattingEnabled = true;
            this.cboReportType.Location = new System.Drawing.Point(117, 28);
            this.cboReportType.Name = "cboReportType";
            this.cboReportType.Size = new System.Drawing.Size(374, 21);
            this.cboReportType.TabIndex = 1;
            // 
            // lblReportType
            // 
            this.lblReportType.AutoSize = true;
            this.lblReportType.Location = new System.Drawing.Point(20, 31);
            this.lblReportType.Name = "lblReportType";
            this.lblReportType.Size = new System.Drawing.Size(69, 13);
            this.lblReportType.TabIndex = 0;
            this.lblReportType.Text = "Report Type:";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelHeader.Controls.Add(this.lblStatus);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(3, 3);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(936, 50);
            this.panelHeader.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblStatus.Location = new System.Drawing.Point(733, 19);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(174, 15);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Report generated successfully.";
            this.lblStatus.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(12, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(192, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Financial Reporting";
            // 
            // tabPageReport
            // 
            this.tabPageReport.Controls.Add(this.panelReport);
            this.tabPageReport.Controls.Add(this.panelReportHeader);
            this.tabPageReport.Location = new System.Drawing.Point(4, 22);
            this.tabPageReport.Name = "tabPageReport";
            this.tabPageReport.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageReport.Size = new System.Drawing.Size(942, 574);
            this.tabPageReport.TabIndex = 1;
            this.tabPageReport.Text = "Report View";
            this.tabPageReport.UseVisualStyleBackColor = true;
            // 
            // panelReport
            // 
            this.panelReport.AutoScroll = true;
            this.panelReport.Controls.Add(this.flpReportContent);
            this.panelReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelReport.Location = new System.Drawing.Point(3, 53);
            this.panelReport.Name = "panelReport";
            this.panelReport.Size = new System.Drawing.Size(936, 518);
            this.panelReport.TabIndex = 1;
            // 
            // flpReportContent
            // 
            this.flpReportContent.AutoScroll = true;
            this.flpReportContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpReportContent.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpReportContent.Location = new System.Drawing.Point(0, 0);
            this.flpReportContent.Name = "flpReportContent";
            this.flpReportContent.Padding = new System.Windows.Forms.Padding(20);
            this.flpReportContent.Size = new System.Drawing.Size(936, 518);
            this.flpReportContent.TabIndex = 0;
            this.flpReportContent.WrapContents = false;
            // 
            // panelReportHeader
            // 
            this.panelReportHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelReportHeader.Controls.Add(this.label1);
            this.panelReportHeader.Controls.Add(this.btnExportReport);
            this.panelReportHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelReportHeader.Location = new System.Drawing.Point(3, 3);
            this.panelReportHeader.Name = "panelReportHeader";
            this.panelReportHeader.Size = new System.Drawing.Size(936, 50);
            this.panelReportHeader.TabIndex = 0;
            // 
            // btnExportReport
            // 
            this.btnExportReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportReport.Location = new System.Drawing.Point(793, 13);
            this.btnExportReport.Name = "btnExportReport";
            this.btnExportReport.Size = new System.Drawing.Size(135, 25);
            this.btnExportReport.TabIndex = 0;
            this.btnExportReport.Text = "Export Report";
            this.btnExportReport.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(373, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Report Statement (startdate to enddate)";
            // 
            // FinancialReportsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "FinancialReportsView";
            this.Size = new System.Drawing.Size(950, 600);
            this.tabControl.ResumeLayout(false);
            this.tabPageParameters.ResumeLayout(false);
            this.panelParameters.ResumeLayout(false);
            this.groupBoxDisplayOptions.ResumeLayout(false);
            this.groupBoxDisplayOptions.PerformLayout();
            this.groupBoxAccounts.ResumeLayout(false);
            this.groupBoxDateRange.ResumeLayout(false);
            this.groupBoxDateRange.PerformLayout();
            this.groupBoxReportType.ResumeLayout(false);
            this.groupBoxReportType.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.tabPageReport.ResumeLayout(false);
            this.panelReport.ResumeLayout(false);
            this.panelReportHeader.ResumeLayout(false);
            this.panelReportHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageParameters;
        private System.Windows.Forms.Panel panelParameters;
        private System.Windows.Forms.GroupBox groupBoxDisplayOptions;
        private System.Windows.Forms.ComboBox cboDisplayOptions;
        private System.Windows.Forms.ComboBox cboDetailLevel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBoxAccounts;
        private System.Windows.Forms.TreeView tvAccounts;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.GroupBox groupBoxDateRange;
        private System.Windows.Forms.DateTimePicker dtpComparisonEndDate;
        private System.Windows.Forms.Label lblComparisonEndDate;
        private System.Windows.Forms.DateTimePicker dtpComparisonStartDate;
        private System.Windows.Forms.Label lblComparisonStartDate;
        private System.Windows.Forms.ComboBox cboComparisonPeriod;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBoxReportType;
        private System.Windows.Forms.ComboBox cboReportType;
        private System.Windows.Forms.Label lblReportType;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabPage tabPageReport;
        private System.Windows.Forms.Panel panelReport;
        private System.Windows.Forms.FlowLayoutPanel flpReportContent;
        private System.Windows.Forms.Panel panelReportHeader;
        private System.Windows.Forms.Button btnExportReport;
        private Label label1;
    }
}
