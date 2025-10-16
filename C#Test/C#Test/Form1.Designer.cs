using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace C_Test {
    partial class Form1 {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing) {
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
        private void InitializeComponent() {
            this.SetLadderButton = new System.Windows.Forms.Button();
            this.ResetValueButton = new System.Windows.Forms.Button();
            this.ResetAllButton = new System.Windows.Forms.Button();
            this.ShowReseultButton = new System.Windows.Forms.Button();
            this.ResultTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SetLadderButton
            // 
            this.SetLadderButton.BackColor = System.Drawing.Color.Green;
            this.SetLadderButton.Location = new System.Drawing.Point(80, 20);
            this.SetLadderButton.Name = "SetLadderButton";
            this.SetLadderButton.Size = new System.Drawing.Size(100, 30);
            this.SetLadderButton.TabIndex = 0;
            this.SetLadderButton.Text = "Set Ladder";
            this.SetLadderButton.UseVisualStyleBackColor = false;
            this.SetLadderButton.Click += new System.EventHandler(this.SetLadderButton_Click);
            // 
            // ResetValueButton
            // 
            this.ResetValueButton.BackColor = System.Drawing.Color.IndianRed;
            this.ResetValueButton.Location = new System.Drawing.Point(200, 20);
            this.ResetValueButton.Name = "ResetValueButton";
            this.ResetValueButton.Size = new System.Drawing.Size(100, 30);
            this.ResetValueButton.TabIndex = 1;
            this.ResetValueButton.Text = "Reset Value";
            this.ResetValueButton.UseVisualStyleBackColor = false;
            this.ResetValueButton.Click += new System.EventHandler(this.ResetValueButton_Click);
            // 
            // ResetAllButton
            // 
            this.ResetAllButton.BackColor = System.Drawing.Color.Cyan;
            this.ResetAllButton.Location = new System.Drawing.Point(320, 20);
            this.ResetAllButton.Name = "ResetAllButton";
            this.ResetAllButton.Size = new System.Drawing.Size(100, 30);
            this.ResetAllButton.TabIndex = 2;
            this.ResetAllButton.Text = "Reset All";
            this.ResetAllButton.UseVisualStyleBackColor = false;
            this.ResetAllButton.Click += new System.EventHandler(this.ResetAllButton_Click);
            // 
            // ShowReseultButton
            // 
            this.ShowReseultButton.BackColor = System.Drawing.Color.LimeGreen;
            this.ShowReseultButton.Location = new System.Drawing.Point(440, 20);
            this.ShowReseultButton.Name = "ShowReseultButton";
            this.ShowReseultButton.Size = new System.Drawing.Size(100, 30);
            this.ShowReseultButton.TabIndex = 3;
            this.ShowReseultButton.Text = "Show Result";
            this.ShowReseultButton.UseVisualStyleBackColor = false;
            this.ShowReseultButton.Click += new System.EventHandler(this.ShowResultButton_Click);
            // 
            // ResultTextBox
            // 
            this.ResultTextBox.BackColor = System.Drawing.Color.Beige;
            this.ResultTextBox.Enabled = false;
            this.ResultTextBox.Font = new System.Drawing.Font("굴림", 16F, System.Drawing.FontStyle.Bold);
            this.ResultTextBox.ForeColor = System.Drawing.Color.Black;
            this.ResultTextBox.Location = new System.Drawing.Point(800, 100);
            this.ResultTextBox.Multiline = true;
            this.ResultTextBox.Name = "ResultTextBox";
            this.ResultTextBox.Size = new System.Drawing.Size(200, 250);
            this.ResultTextBox.TabIndex = 4;
            this.ResultTextBox.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.SetLadderButton);
            this.Controls.Add(this.ResetValueButton);
            this.Controls.Add(this.ResetAllButton);
            this.Controls.Add(this.ShowReseultButton);
            this.Controls.Add(this.ResultTextBox);
            this.Name = "Form1";
            this.Text = "Tester";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button SetLadderButton;
        private Button ResetValueButton;
        private Button ResetAllButton;
        private Button ShowReseultButton;
        private TextBox ResultTextBox;

        private List<Button> ButtonList = new List<Button>();
        private List<TextBox> TextBoxList = new List<TextBox>();
    }
}

