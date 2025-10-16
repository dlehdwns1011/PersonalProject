using System;
using System.Drawing;
using System.Windows.Forms;

namespace VBATester
{
    partial class VBATester
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.NewFileButton = new System.Windows.Forms.Button();
            this.FileNameText = new System.Windows.Forms.TextBox();
            this.VBAScriptText = new System.Windows.Forms.RichTextBox();
            this.ExecuteVBAScript = new System.Windows.Forms.Button();
            this.SelectFileButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SlideNumberText = new System.Windows.Forms.TextBox();
            this.prevButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.ApplicationCombo = new System.Windows.Forms.ComboBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.AnalysisPresentation = new System.Windows.Forms.Button();
            this.TitleShapeResult = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // NewFileButton
            // 
            this.NewFileButton.Location = new System.Drawing.Point(240, 12);
            this.NewFileButton.Name = "NewFileButton";
            this.NewFileButton.Size = new System.Drawing.Size(180, 20);
            this.NewFileButton.TabIndex = 2;
            this.NewFileButton.Text = "New File";
            this.NewFileButton.UseVisualStyleBackColor = true;
            this.NewFileButton.Click += new System.EventHandler(this.NewFileButton_Click);
            // 
            // FileNameText
            // 
            this.FileNameText.Location = new System.Drawing.Point(10, 38);
            this.FileNameText.Name = "FileNameText";
            this.FileNameText.Size = new System.Drawing.Size(778, 21);
            this.FileNameText.TabIndex = 3;
            this.FileNameText.Text = "파일명을 입력해주세요";
            this.FileNameText.Click += new System.EventHandler(this.FileNameText_Click);
            this.FileNameText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_PressEnter);
            this.FileNameText.LostFocus += new System.EventHandler(this.TextBox_LostFocus);
            // 
            // VBAScriptText
            // 
            this.VBAScriptText.AcceptsTab = true;
            this.VBAScriptText.Font = new System.Drawing.Font("Arial", 10F);
            this.VBAScriptText.Location = new System.Drawing.Point(10, 65);
            this.VBAScriptText.Name = "VBAScriptText";
            this.VBAScriptText.Size = new System.Drawing.Size(778, 373);
            this.VBAScriptText.TabIndex = 4;
            this.VBAScriptText.Text = "";
            this.VBAScriptText.TextChanged += new System.EventHandler(this.VBAScriptText_TextChanged);
            // 
            // ExecuteVBAScript
            // 
            this.ExecuteVBAScript.Location = new System.Drawing.Point(10, 444);
            this.ExecuteVBAScript.Name = "ExecuteVBAScript";
            this.ExecuteVBAScript.Size = new System.Drawing.Size(778, 20);
            this.ExecuteVBAScript.TabIndex = 5;
            this.ExecuteVBAScript.Text = "Execute VBAScript";
            this.ExecuteVBAScript.UseVisualStyleBackColor = true;
            this.ExecuteVBAScript.Click += new System.EventHandler(this.ExecuteVBAScript_Click);
            // 
            // SelectFileButton
            // 
            this.SelectFileButton.Location = new System.Drawing.Point(424, 12);
            this.SelectFileButton.Name = "SelectFileButton";
            this.SelectFileButton.Size = new System.Drawing.Size(180, 20);
            this.SelectFileButton.TabIndex = 6;
            this.SelectFileButton.Text = "Select File";
            this.SelectFileButton.UseVisualStyleBackColor = true;
            this.SelectFileButton.Click += new System.EventHandler(this.SelectFileButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(807, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(628, 426);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // SlideNumberText
            // 
            this.SlideNumberText.Enabled = false;
            this.SlideNumberText.Location = new System.Drawing.Point(1109, 445);
            this.SlideNumberText.Name = "SlideNumberText";
            this.SlideNumberText.Size = new System.Drawing.Size(50, 21);
            this.SlideNumberText.TabIndex = 8;
            this.SlideNumberText.Text = "1";
            this.SlideNumberText.TextChanged += new System.EventHandler(this.SlideNumberText_TextChanged);
            this.SlideNumberText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_PressEnter);
            this.SlideNumberText.LostFocus += new System.EventHandler(this.TextBox_LostFocus);
            // 
            // prevButton
            // 
            this.prevButton.Location = new System.Drawing.Point(1083, 445);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(20, 20);
            this.prevButton.TabIndex = 9;
            this.prevButton.Text = "◀";
            this.prevButton.UseVisualStyleBackColor = true;
            this.prevButton.Click += new System.EventHandler(this.MoveSlide_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(1165, 445);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(20, 20);
            this.nextButton.TabIndex = 10;
            this.nextButton.Text = "▶";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.MoveSlide_Click);
            // 
            // ApplicationCombo
            // 
            this.ApplicationCombo.FormattingEnabled = true;
            this.ApplicationCombo.Items.AddRange(new object[] {
            "PowerPoint",
            "HShow"});
            this.ApplicationCombo.Location = new System.Drawing.Point(10, 12);
            this.ApplicationCombo.Name = "ApplicationCombo";
            this.ApplicationCombo.Size = new System.Drawing.Size(220, 20);
            this.ApplicationCombo.TabIndex = 11;
            this.ApplicationCombo.SelectedIndexChanged += new System.EventHandler(this.ApplicationCombo_SelectedIndexChanged);
            // 
            // closeButton
            // 
            this.closeButton.Enabled = false;
            this.closeButton.Location = new System.Drawing.Point(608, 12);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(180, 20);
            this.closeButton.TabIndex = 12;
            this.closeButton.Text = "Close App";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.CloseApp_Click);
            // 
            // AnalysisPresentation
            // 
            this.AnalysisPresentation.Location = new System.Drawing.Point(12, 685);
            this.AnalysisPresentation.Name = "AnalysisPresentation";
            this.AnalysisPresentation.Size = new System.Drawing.Size(145, 20);
            this.AnalysisPresentation.TabIndex = 13;
            this.AnalysisPresentation.Text = "Analysis Presentation";
            this.AnalysisPresentation.UseVisualStyleBackColor = true;
            this.AnalysisPresentation.Click += new System.EventHandler(this.AnalysisPresentation_Click);
            // 
            // TitleShapeResult
            // 
            this.TitleShapeResult.Font = new System.Drawing.Font("Arial", 10F);
            this.TitleShapeResult.Location = new System.Drawing.Point(12, 472);
            this.TitleShapeResult.Name = "TitleShapeResult";
            this.TitleShapeResult.Size = new System.Drawing.Size(1423, 207);
            this.TitleShapeResult.TabIndex = 14;
            this.TitleShapeResult.Text = "test";
            // 
            // VBATester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1455, 717);
            this.Controls.Add(this.TitleShapeResult);
            this.Controls.Add(this.AnalysisPresentation);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.ApplicationCombo);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.prevButton);
            this.Controls.Add(this.SlideNumberText);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.SelectFileButton);
            this.Controls.Add(this.ExecuteVBAScript);
            this.Controls.Add(this.VBAScriptText);
            this.Controls.Add(this.FileNameText);
            this.Controls.Add(this.NewFileButton);
            this.Name = "VBATester";
            this.Text = "VBATester";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CloseForm);
            this.Click += new System.EventHandler(this.Background_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button NewFileButton;
        private System.Windows.Forms.TextBox FileNameText;
        private System.Windows.Forms.RichTextBox VBAScriptText;
        private System.Windows.Forms.Button ExecuteVBAScript;
        private System.Windows.Forms.Button SelectFileButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private TextBox SlideNumberText;
        private Button prevButton;
        private Button nextButton;
        private ComboBox ApplicationCombo;

        private string[] applicationData = { "PowerPoint", "HShow" };
        private Button closeButton;
        private Button AnalysisPresentation;
        private RichTextBox TitleShapeResult;
    }
}

