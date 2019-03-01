namespace BenchmarkRunner.Controls
{
    partial class OptionsControl
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
            this.components = new System.ComponentModel.Container();
            this.chkMemoryDiagnoser = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkCsv = new System.Windows.Forms.CheckBox();
            this.chkCsvMeasurements = new System.Windows.Forms.CheckBox();
            this.chkHtml = new System.Windows.Forms.CheckBox();
            this.chkMarkdownDefault = new System.Windows.Forms.CheckBox();
            this.chkMarkdownAtlassian = new System.Windows.Forms.CheckBox();
            this.chkMarkdownStackOverflow = new System.Windows.Forms.CheckBox();
            this.chkMarkdownGitHub = new System.Windows.Forms.CheckBox();
            this.chkPlain = new System.Windows.Forms.CheckBox();
            this.chkRPlot = new System.Windows.Forms.CheckBox();
            this.chkJsonDefault = new System.Windows.Forms.CheckBox();
            this.chkJsonBrief = new System.Windows.Forms.CheckBox();
            this.chkJsonFull = new System.Windows.Forms.CheckBox();
            this.chkAsciiDoc = new System.Windows.Forms.CheckBox();
            this.chkXmlDefault = new System.Windows.Forms.CheckBox();
            this.chkXmlBrief = new System.Windows.Forms.CheckBox();
            this.chkXmlFull = new System.Windows.Forms.CheckBox();
            this.chkDisassembly = new System.Windows.Forms.CheckBox();
            this.txtCommandLine = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lnkHelp = new System.Windows.Forms.LinkLabel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkClr = new System.Windows.Forms.CheckBox();
            this.chkCore = new System.Windows.Forms.CheckBox();
            this.chkMono = new System.Windows.Forms.CheckBox();
            this.chkCoreRT = new System.Windows.Forms.CheckBox();
            this.chkEtwProfiler = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkMemoryDiagnoser
            // 
            this.chkMemoryDiagnoser.AutoSize = true;
            this.chkMemoryDiagnoser.Location = new System.Drawing.Point(6, 19);
            this.chkMemoryDiagnoser.Name = "chkMemoryDiagnoser";
            this.chkMemoryDiagnoser.Size = new System.Drawing.Size(63, 17);
            this.chkMemoryDiagnoser.TabIndex = 0;
            this.chkMemoryDiagnoser.Text = "Memory";
            this.chkMemoryDiagnoser.UseVisualStyleBackColor = true;
            this.chkMemoryDiagnoser.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.flowLayoutPanel2);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(670, 49);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Runtimes";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chkEtwProfiler);
            this.groupBox2.Controls.Add(this.chkDisassembly);
            this.groupBox2.Controls.Add(this.chkMemoryDiagnoser);
            this.groupBox2.Location = new System.Drawing.Point(3, 58);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(670, 48);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Diagnosers";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.flowLayoutPanel1);
            this.groupBox3.Location = new System.Drawing.Point(3, 112);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(670, 116);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Exporters";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.lnkHelp);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.txtCommandLine);
            this.groupBox4.Location = new System.Drawing.Point(3, 234);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(670, 70);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Custom Parameters";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.chkPlain);
            this.flowLayoutPanel1.Controls.Add(this.chkHtml);
            this.flowLayoutPanel1.Controls.Add(this.chkAsciiDoc);
            this.flowLayoutPanel1.Controls.Add(this.chkCsv);
            this.flowLayoutPanel1.Controls.Add(this.chkCsvMeasurements);
            this.flowLayoutPanel1.Controls.Add(this.chkMarkdownDefault);
            this.flowLayoutPanel1.Controls.Add(this.chkMarkdownAtlassian);
            this.flowLayoutPanel1.Controls.Add(this.chkMarkdownStackOverflow);
            this.flowLayoutPanel1.Controls.Add(this.chkMarkdownGitHub);
            this.flowLayoutPanel1.Controls.Add(this.chkJsonDefault);
            this.flowLayoutPanel1.Controls.Add(this.chkJsonBrief);
            this.flowLayoutPanel1.Controls.Add(this.chkJsonFull);
            this.flowLayoutPanel1.Controls.Add(this.chkXmlDefault);
            this.flowLayoutPanel1.Controls.Add(this.chkXmlBrief);
            this.flowLayoutPanel1.Controls.Add(this.chkXmlFull);
            this.flowLayoutPanel1.Controls.Add(this.chkRPlot);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(664, 97);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // chkCsv
            // 
            this.chkCsv.AutoSize = true;
            this.chkCsv.Location = new System.Drawing.Point(3, 72);
            this.chkCsv.Name = "chkCsv";
            this.chkCsv.Size = new System.Drawing.Size(47, 17);
            this.chkCsv.TabIndex = 0;
            this.chkCsv.Text = "CSV";
            this.chkCsv.UseVisualStyleBackColor = true;
            this.chkCsv.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // chkCsvMeasurements
            // 
            this.chkCsvMeasurements.AutoSize = true;
            this.chkCsvMeasurements.Location = new System.Drawing.Point(88, 3);
            this.chkCsvMeasurements.Name = "chkCsvMeasurements";
            this.chkCsvMeasurements.Size = new System.Drawing.Size(119, 17);
            this.chkCsvMeasurements.TabIndex = 1;
            this.chkCsvMeasurements.Text = "CSV Measurements";
            this.chkCsvMeasurements.UseVisualStyleBackColor = true;
            this.chkCsvMeasurements.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // chkHtml
            // 
            this.chkHtml.AutoSize = true;
            this.chkHtml.Location = new System.Drawing.Point(3, 26);
            this.chkHtml.Name = "chkHtml";
            this.chkHtml.Size = new System.Drawing.Size(56, 17);
            this.chkHtml.TabIndex = 2;
            this.chkHtml.Text = "HTML";
            this.chkHtml.UseVisualStyleBackColor = true;
            this.chkHtml.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // chkMarkdownDefault
            // 
            this.chkMarkdownDefault.AutoSize = true;
            this.chkMarkdownDefault.Checked = true;
            this.chkMarkdownDefault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMarkdownDefault.Enabled = false;
            this.chkMarkdownDefault.Location = new System.Drawing.Point(88, 26);
            this.chkMarkdownDefault.Name = "chkMarkdownDefault";
            this.chkMarkdownDefault.Size = new System.Drawing.Size(113, 17);
            this.chkMarkdownDefault.TabIndex = 3;
            this.chkMarkdownDefault.Text = "Markdown Default";
            this.chkMarkdownDefault.UseVisualStyleBackColor = true;
            this.chkMarkdownDefault.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // chkMarkdownAtlassian
            // 
            this.chkMarkdownAtlassian.AutoSize = true;
            this.chkMarkdownAtlassian.Location = new System.Drawing.Point(88, 49);
            this.chkMarkdownAtlassian.Name = "chkMarkdownAtlassian";
            this.chkMarkdownAtlassian.Size = new System.Drawing.Size(121, 17);
            this.chkMarkdownAtlassian.TabIndex = 4;
            this.chkMarkdownAtlassian.Text = "Markdown Atlassian";
            this.chkMarkdownAtlassian.UseVisualStyleBackColor = true;
            this.chkMarkdownAtlassian.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // chkMarkdownStackOverflow
            // 
            this.chkMarkdownStackOverflow.AutoSize = true;
            this.chkMarkdownStackOverflow.Location = new System.Drawing.Point(88, 72);
            this.chkMarkdownStackOverflow.Name = "chkMarkdownStackOverflow";
            this.chkMarkdownStackOverflow.Size = new System.Drawing.Size(149, 17);
            this.chkMarkdownStackOverflow.TabIndex = 5;
            this.chkMarkdownStackOverflow.Text = "Markdown StackOverflow";
            this.chkMarkdownStackOverflow.UseVisualStyleBackColor = true;
            this.chkMarkdownStackOverflow.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // chkMarkdownGitHub
            // 
            this.chkMarkdownGitHub.AutoSize = true;
            this.chkMarkdownGitHub.Location = new System.Drawing.Point(243, 3);
            this.chkMarkdownGitHub.Name = "chkMarkdownGitHub";
            this.chkMarkdownGitHub.Size = new System.Drawing.Size(112, 17);
            this.chkMarkdownGitHub.TabIndex = 6;
            this.chkMarkdownGitHub.Text = "Markdown GitHub";
            this.chkMarkdownGitHub.UseVisualStyleBackColor = true;
            this.chkMarkdownGitHub.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // chkPlain
            // 
            this.chkPlain.AutoSize = true;
            this.chkPlain.Location = new System.Drawing.Point(3, 3);
            this.chkPlain.Name = "chkPlain";
            this.chkPlain.Size = new System.Drawing.Size(49, 17);
            this.chkPlain.TabIndex = 7;
            this.chkPlain.Text = "Plain";
            this.chkPlain.UseVisualStyleBackColor = true;
            this.chkPlain.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // chkRPlot
            // 
            this.chkRPlot.AutoSize = true;
            this.chkRPlot.Location = new System.Drawing.Point(361, 72);
            this.chkRPlot.Name = "chkRPlot";
            this.chkRPlot.Size = new System.Drawing.Size(55, 17);
            this.chkRPlot.TabIndex = 8;
            this.chkRPlot.Text = "R Plot";
            this.chkRPlot.UseVisualStyleBackColor = true;
            this.chkRPlot.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // chkJsonDefault
            // 
            this.chkJsonDefault.AutoSize = true;
            this.chkJsonDefault.Location = new System.Drawing.Point(243, 26);
            this.chkJsonDefault.Name = "chkJsonDefault";
            this.chkJsonDefault.Size = new System.Drawing.Size(91, 17);
            this.chkJsonDefault.TabIndex = 9;
            this.chkJsonDefault.Text = "JSON Default";
            this.chkJsonDefault.UseVisualStyleBackColor = true;
            this.chkJsonDefault.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // chkJsonBrief
            // 
            this.chkJsonBrief.AutoSize = true;
            this.chkJsonBrief.Location = new System.Drawing.Point(243, 49);
            this.chkJsonBrief.Name = "chkJsonBrief";
            this.chkJsonBrief.Size = new System.Drawing.Size(78, 17);
            this.chkJsonBrief.TabIndex = 10;
            this.chkJsonBrief.Text = "JSON Brief";
            this.chkJsonBrief.UseVisualStyleBackColor = true;
            this.chkJsonBrief.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // chkJsonFull
            // 
            this.chkJsonFull.AutoSize = true;
            this.chkJsonFull.Location = new System.Drawing.Point(243, 72);
            this.chkJsonFull.Name = "chkJsonFull";
            this.chkJsonFull.Size = new System.Drawing.Size(73, 17);
            this.chkJsonFull.TabIndex = 11;
            this.chkJsonFull.Text = "JSON Full";
            this.chkJsonFull.UseVisualStyleBackColor = true;
            this.chkJsonFull.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // chkAsciiDoc
            // 
            this.chkAsciiDoc.AutoSize = true;
            this.chkAsciiDoc.Location = new System.Drawing.Point(3, 49);
            this.chkAsciiDoc.Name = "chkAsciiDoc";
            this.chkAsciiDoc.Size = new System.Drawing.Size(79, 17);
            this.chkAsciiDoc.TabIndex = 12;
            this.chkAsciiDoc.Text = "ASCII DOC";
            this.chkAsciiDoc.UseVisualStyleBackColor = true;
            this.chkAsciiDoc.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // chkXmlDefault
            // 
            this.chkXmlDefault.AutoSize = true;
            this.chkXmlDefault.Location = new System.Drawing.Point(361, 3);
            this.chkXmlDefault.Name = "chkXmlDefault";
            this.chkXmlDefault.Size = new System.Drawing.Size(85, 17);
            this.chkXmlDefault.TabIndex = 13;
            this.chkXmlDefault.Text = "XML Default";
            this.chkXmlDefault.UseVisualStyleBackColor = true;
            this.chkXmlDefault.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // chkXmlBrief
            // 
            this.chkXmlBrief.AutoSize = true;
            this.chkXmlBrief.Location = new System.Drawing.Point(361, 26);
            this.chkXmlBrief.Name = "chkXmlBrief";
            this.chkXmlBrief.Size = new System.Drawing.Size(72, 17);
            this.chkXmlBrief.TabIndex = 14;
            this.chkXmlBrief.Text = "XML Brief";
            this.chkXmlBrief.UseVisualStyleBackColor = true;
            this.chkXmlBrief.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // chkXmlFull
            // 
            this.chkXmlFull.AutoSize = true;
            this.chkXmlFull.Location = new System.Drawing.Point(361, 49);
            this.chkXmlFull.Name = "chkXmlFull";
            this.chkXmlFull.Size = new System.Drawing.Size(67, 17);
            this.chkXmlFull.TabIndex = 15;
            this.chkXmlFull.Text = "XML Full";
            this.chkXmlFull.UseVisualStyleBackColor = true;
            this.chkXmlFull.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // chkDisassembly
            // 
            this.chkDisassembly.AutoSize = true;
            this.chkDisassembly.Location = new System.Drawing.Point(75, 19);
            this.chkDisassembly.Name = "chkDisassembly";
            this.chkDisassembly.Size = new System.Drawing.Size(84, 17);
            this.chkDisassembly.TabIndex = 1;
            this.chkDisassembly.Text = "Disassembly";
            this.chkDisassembly.UseVisualStyleBackColor = true;
            this.chkDisassembly.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // txtCommandLine
            // 
            this.txtCommandLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommandLine.Location = new System.Drawing.Point(6, 44);
            this.txtCommandLine.Name = "txtCommandLine";
            this.txtCommandLine.Size = new System.Drawing.Size(658, 20);
            this.txtCommandLine.TabIndex = 0;
            this.txtCommandLine.Leave += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(265, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Additional BenchmarkDotnet command line parameters";
            // 
            // lnkHelp
            // 
            this.lnkHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkHelp.AutoSize = true;
            this.lnkHelp.Location = new System.Drawing.Point(635, 25);
            this.lnkHelp.Name = "lnkHelp";
            this.lnkHelp.Size = new System.Drawing.Size(29, 13);
            this.lnkHelp.TabIndex = 2;
            this.lnkHelp.TabStop = true;
            this.lnkHelp.Text = "Help";
            this.lnkHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHelp_LinkClicked);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.chkClr);
            this.flowLayoutPanel2.Controls.Add(this.chkCore);
            this.flowLayoutPanel2.Controls.Add(this.chkMono);
            this.flowLayoutPanel2.Controls.Add(this.chkCoreRT);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(664, 30);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // chkClr
            // 
            this.chkClr.AutoSize = true;
            this.chkClr.Location = new System.Drawing.Point(3, 3);
            this.chkClr.Name = "chkClr";
            this.chkClr.Size = new System.Drawing.Size(47, 17);
            this.chkClr.TabIndex = 8;
            this.chkClr.Text = "CLR";
            this.chkClr.UseVisualStyleBackColor = true;
            this.chkClr.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // chkCore
            // 
            this.chkCore.AutoSize = true;
            this.chkCore.Location = new System.Drawing.Point(56, 3);
            this.chkCore.Name = "chkCore";
            this.chkCore.Size = new System.Drawing.Size(48, 17);
            this.chkCore.TabIndex = 9;
            this.chkCore.Text = "Core";
            this.chkCore.UseVisualStyleBackColor = true;
            this.chkCore.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // chkMono
            // 
            this.chkMono.AutoSize = true;
            this.chkMono.Location = new System.Drawing.Point(110, 3);
            this.chkMono.Name = "chkMono";
            this.chkMono.Size = new System.Drawing.Size(53, 17);
            this.chkMono.TabIndex = 10;
            this.chkMono.Text = "Mono";
            this.chkMono.UseVisualStyleBackColor = true;
            this.chkMono.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // chkCoreRT
            // 
            this.chkCoreRT.AutoSize = true;
            this.chkCoreRT.Location = new System.Drawing.Point(169, 3);
            this.chkCoreRT.Name = "chkCoreRT";
            this.chkCoreRT.Size = new System.Drawing.Size(63, 17);
            this.chkCoreRT.TabIndex = 11;
            this.chkCoreRT.Text = "CoreRT";
            this.chkCoreRT.UseVisualStyleBackColor = true;
            this.chkCoreRT.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // chkEtwProfiler
            // 
            this.chkEtwProfiler.AutoSize = true;
            this.chkEtwProfiler.Location = new System.Drawing.Point(165, 19);
            this.chkEtwProfiler.Name = "chkEtwProfiler";
            this.chkEtwProfiler.Size = new System.Drawing.Size(86, 17);
            this.chkEtwProfiler.TabIndex = 2;
            this.chkEtwProfiler.Text = "ETW Profiler";
            this.chkEtwProfiler.UseVisualStyleBackColor = true;
            this.chkEtwProfiler.CheckedChanged += new System.EventHandler(this.OptionsControl_Changed);
            // 
            // toolTip
            // 
            this.toolTip.ToolTipTitle = "Must be enabled for ";
            // 
            // OptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "OptionsControl";
            this.Size = new System.Drawing.Size(676, 366);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkMemoryDiagnoser;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox chkCsv;
        private System.Windows.Forms.CheckBox chkCsvMeasurements;
        private System.Windows.Forms.CheckBox chkHtml;
        private System.Windows.Forms.CheckBox chkMarkdownDefault;
        private System.Windows.Forms.CheckBox chkMarkdownAtlassian;
        private System.Windows.Forms.CheckBox chkMarkdownStackOverflow;
        private System.Windows.Forms.CheckBox chkMarkdownGitHub;
        private System.Windows.Forms.CheckBox chkPlain;
        private System.Windows.Forms.CheckBox chkRPlot;
        private System.Windows.Forms.CheckBox chkJsonDefault;
        private System.Windows.Forms.CheckBox chkJsonBrief;
        private System.Windows.Forms.CheckBox chkJsonFull;
        private System.Windows.Forms.CheckBox chkAsciiDoc;
        private System.Windows.Forms.CheckBox chkXmlDefault;
        private System.Windows.Forms.CheckBox chkXmlBrief;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkDisassembly;
        private System.Windows.Forms.CheckBox chkXmlFull;
        private System.Windows.Forms.LinkLabel lnkHelp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCommandLine;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox chkClr;
        private System.Windows.Forms.CheckBox chkCore;
        private System.Windows.Forms.CheckBox chkMono;
        private System.Windows.Forms.CheckBox chkCoreRT;
        private System.Windows.Forms.CheckBox chkEtwProfiler;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
