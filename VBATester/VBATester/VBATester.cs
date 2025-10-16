using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using HShow = HShowObject;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Core;
using Microsoft.Vbe.Interop;
using System.Runtime.InteropServices;

namespace VBATester
{
    public partial class VBATester : Form
    {
        PowerPoint.Application powerPointApp;
        PowerPoint.Presentation presentation;

        HShow.Application hshowApp;
        HShow.Presentation hshowPresentation;

        bool isOpenExistFile = false;
        bool isShow = false;

        public VBATester()
        {
            InitializeComponent();
            //application = new opApplication((this.ApplicationCombo.SelectedIndex == 0) ? false : true);
        }

        private void Background_Click(object sender, EventArgs e)
        {
            if (presentation == null || powerPointApp == null) {
                return;
            }

            this.ActiveControl = null;
        }

        private void NewFileButton_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Text == "Save") {
                if (presentation == null || powerPointApp == null) {
                    return;
                }

                if (string.IsNullOrEmpty(this.FileNameText.Text)) {
                    return;
                }

                if (isOpenExistFile) {
                    // 기존 파일 사용
                    presentation.Save();
                } else {
                    if (string.IsNullOrEmpty(this.FileNameText.Text)) {
                        return;
                    }

                    // 새파일
                    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                    folderBrowserDialog.Description = "폴더 선택";
                    folderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    string savePath = "";
                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK) {
                        savePath = folderBrowserDialog.SelectedPath;
                        // 여기에서 선택한 폴더에 대한 작업을 수행할 수 있습니다.
                    } else {
                        return;
                    }

                    if (File.Exists(savePath + "\\" + this.FileNameText.Text)) {
                        File.Delete(savePath + "\\" + this.FileNameText.Text);
                    }

                    presentation.SaveAs(savePath + "\\" + this.FileNameText.Text, PowerPoint.PpSaveAsFileType.ppSaveAsOpenXMLPresentation);
                    isOpenExistFile = true;
                    this.FileNameText.Text = presentation.FullName;
                    this.FileNameText.Enabled = false;
                }

                ChangedFileInformation();
                return;
            }

            // 새로운 문서 생성
            powerPointApp = new PowerPoint.Application();
            presentation = powerPointApp.Presentations.Add();
            powerPointApp.WindowState = PowerPoint.PpWindowState.ppWindowMinimized;

            PowerPoint.Slide slide = presentation.Slides.Add(1, PowerPoint.PpSlideLayout.ppLayoutTitle);

            // 슬라이드에 제목과 내용을 추가합니다.
            slide.Shapes[1].TextFrame.TextRange.Text = "제목 슬라이드";
            slide.Shapes[2].TextFrame.TextRange.Text = "내용을 입력하세요.";

            (sender as Button).Text = "Save";
            this.closeButton.Enabled = true;

            this.SlideNumberText.Enabled = true;
            this.SlideNumberText.Text = "1";

            ChangedFileInformation();

            
            
        }

        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            string fileName = "";
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "프레젠테이션 문서(*.pptx;*.ppt)|*.pptx;.ppt|모든 파일 (*.*)|*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK) {
                fileName = fileDialog.FileName;
                // 새로운 문서 생성
                powerPointApp = new PowerPoint.Application();
                presentation = powerPointApp.Presentations.Open(fileName);
                powerPointApp.WindowState = PowerPoint.PpWindowState.ppWindowMinimized;

                this.NewFileButton.Text = "Save";
                (sender as Button).Enabled = false;
                this.FileNameText.Text = presentation.FullName;
                this.FileNameText.Enabled = false;
                isOpenExistFile = true;
            }


            this.closeButton.Enabled = true;
            this.SlideNumberText.Enabled = true;
            this.SlideNumberText.Text = "1";
            ChangedFileInformation();
        }

        private void FileNameText_Click(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.ResetText();
        }

        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Name == "SlideNumberText") {
                ChangedFileInformation();
            } else if(textBox.Name == "FileNameText") {
                if (textBox.Text.Length == 0) {
                    textBox.Text = "파일명을 입력해주세요";
                }
            }   
        }

        private void TextBox_PressEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                this.ActiveControl = null;
            }

            TextBox textBox = sender as TextBox;
            if (textBox.Name == "SlideNumberText") {
                ChangedFileInformation();
            }
        }

        private void VBAScriptText_TextChanged(object sender, EventArgs e)
        {

        }

        private void ExecuteVBAScript_Click(object sender, EventArgs e)
        {
            if (presentation == null || powerPointApp == null) {
                return;
            }

            if (presentation == null) {
                return;
            }

            string vbaScript = this.VBAScriptText.Text;
            if (string.IsNullOrEmpty(vbaScript)) {
                return;
            }

            if(!vbaScript.Contains("Sub ")) {
                return;
            }

            VBProject vbaProject = presentation.VBProject;

            // 새로운 모듈을 추가합니다.
            VBComponent module = vbaProject.VBComponents.Add(vbext_ComponentType.vbext_ct_StdModule);

            // 모듈에 VBA 코드를 추가합니다.
            module.CodeModule.AddFromString(vbaScript);
            var codeModule = module.CodeModule;
            int lineCount = codeModule.CountOfLines;
            string macroName = "";
            for (int i = 1; i <= lineCount; i++) {
                string line = codeModule.get_Lines(i, 1);
                if (line.Trim().StartsWith("Sub")) {
                    macroName = line.Split(' ')[1].Trim();
                    break;
                }
            }

            if (!string.IsNullOrEmpty(macroName)) {
                try {
                    // VBA 스크립트 실행
                    powerPointApp.Run(macroName.Replace("()", ""));
                } catch (Exception ex) {
                    MessageBox.Show("VBA Script 오류 : \n" + ex.Message);
                }
            }

            vbaProject.VBComponents.Remove(module);

            ChangedFileInformation();
        }

        private void CloseForm(object sender, EventArgs e)
        {
            CloseApp();
        }

        private void CloseApp()
        {
            if (presentation != null) {
                presentation.Close();
                Marshal.ReleaseComObject(presentation);
                presentation = null;
            }

            if (powerPointApp != null) {
                powerPointApp.Quit();
                Marshal.ReleaseComObject(powerPointApp);
                powerPointApp = null;
            }
        }

        private void ChangedFileInformation()
        {
            if (presentation == null || powerPointApp == null) {
                return;
            }

            int slideNum = 1;
            int.TryParse(this.SlideNumberText.Text, out slideNum);
            this.pictureBox1.Image = GetSlideThumbnail(presentation, slideNum);
        }

        Image GetSlideThumbnail(PowerPoint.Presentation presentation, int slideIndex)
        {
            if (slideIndex < 0 || presentation.Slides.Count == 0) {
                return null;
            }

            PowerPoint.Slide slide = presentation.Slides[slideIndex];

            // 썸네일 이미지를 가져오기 위한 임시 파일 경로
            string tempfilePath = System.IO.Path.GetTempFileName();
            string tempImagePath = tempfilePath + ".png";

            try {
                // 슬라이드를 이미지로 저장
                slide.Export(tempImagePath, "PNG");

                // 이미지 파일을 읽어와 Image로 반환
                using (FileStream stream = new FileStream(tempImagePath, FileMode.Open, FileAccess.Read)) {
                    Image slideImage = Image.FromStream(stream);
                    return new Bitmap(slideImage);  // 복제본을 반환하여 stream을 닫아도 이미지를 사용할 수 있게 함
                }
            } catch (Exception ex) {
                Console.WriteLine("Error during image retrieval: " + ex.Message);
                return null;
            } finally {
                // 임시 파일 삭제
                if (System.IO.File.Exists(tempfilePath)) {
                    System.IO.File.Delete(tempfilePath);
                }

                if (System.IO.File.Exists(tempImagePath)) {
                    System.IO.File.Delete(tempImagePath);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void SlideNumberText_TextChanged(object sender, EventArgs e)
        {

        }

        private void MoveSlide_Click(object sender, EventArgs e)
        {
            if (presentation == null || powerPointApp == null) {
                return;
            }

            int slideNum = 1;
            int.TryParse(this.SlideNumberText.Text, out slideNum);

            Button button = sender as Button;
            if (button.Name == "nextButton") {
                ++slideNum;
                if (slideNum <= presentation.Slides.Count) {
                    this.SlideNumberText.Text = slideNum.ToString();
                }

                ChangedFileInformation();
            } else if (button.Name == "prevButton") {
                --slideNum;
                if (slideNum != 0) {
                    this.SlideNumberText.Text = slideNum.ToString();
                }

                ChangedFileInformation();
            }
        }

        private void ApplicationCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedIndex == 0) {
                isShow = false;
            } else {
                isShow = true;
            }
        }

        private void CloseApp_Click(object sender, EventArgs e)
        {
            CloseApp();
            this.SelectFileButton.Enabled = true;
            this.NewFileButton.Text = "New File";

            this.pictureBox1.Image = null;
            this.closeButton.Enabled = false;
            this.FileNameText.ResetText();
            this.FileNameText.Enabled = true;
            this.SlideNumberText.Enabled = false;
        }

        private void AnalysisPresentation_Click(object sender, EventArgs e)
        {
            if (presentation == null) {
                return;
            }

            TitleShapeResult.Text = "";
            for (int index = 1; index <= presentation.Slides.Count; ++index) {
                var slide = presentation.Slides[index];
                AnalysisSlide(slide);
            }

        }

        private void AnalysisSlide(PowerPoint.Slide slide)
        {
            // 특정 슬라이드 분석
            if (slide == null) {
                return;
            }

            PowerPoint.Shape titleShape = null;
            // 제목 shape가 지정되어 있는 경우
            if (slide.Shapes.HasTitle == MsoTriState.msoTrue) {
                titleShape = slide.Shapes.Title;
            } else {
                // 제목 shape가 지정되어 있지 않은 경우
                
                for (int index = 1; index <= slide.Shapes.Count; ++index) {
                    var nowShape = slide.Shapes[index];

                    // 제목 shape을 찾으러 가보자구
                    float maxFontSize = 1;
                    if ((presentation.PageSetup.SlideHeight / 5) >= nowShape.Top) {
                        if (nowShape.Width >= presentation.PageSetup.SlideWidth / 2) {
                            if (nowShape.HasTextFrame == MsoTriState.msoTrue && nowShape.TextEffect.FontSize > maxFontSize) {
                                maxFontSize = nowShape.TextEffect.FontSize;
                                titleShape = nowShape;
                            }
                        }
                    }
                }   
            }

            if (titleShape != null) {
                if (titleShape.HasTextFrame == MsoTriState.msoTrue && titleShape.TextFrame.TextRange.Text != "") {
                    titleShape.TextFrame.DeleteText();
                    titleShape.TextFrame.TextRange.Text = "제목을 입력해주세요....";
                }
            }


            // 이제 내용 shape 초기화 해보자구
            List<int> removeList = new List<int>();
            for (int index = 1; index <= slide.Shapes.Count; ++index) {
                var nowShape = slide.Shapes[index];
                if (nowShape != titleShape) {
                    if (IsResetShape(nowShape.Type)) {
                        ResetShape(nowShape);
                    } else if (nowShape.Type == MsoShapeType.msoTable && nowShape.HasTable == MsoTriState.msoTrue) {
                        var table = nowShape.Table;
                        // 테이블 내용 초기화
                        for (int row = 1; row <= table.Rows.Count; row++) {
                            for (int col = 1; col <= table.Columns.Count; col++) {
                                table.Cell(row, col).Shape.TextFrame.TextRange.Text = "";
                            }
                        }
                    }
                }
            }
        }

        private bool IsResetShape(MsoShapeType shapeType)
        {
            switch (shapeType) {
            case MsoShapeType.msoAutoShape:
            case MsoShapeType.msoDiagram:
            case MsoShapeType.msoFreeform:
            case MsoShapeType.msoGroup:
            case MsoShapeType.msoPlaceholder:
            case MsoShapeType.msoShapeTypeMixed:
            case MsoShapeType.msoTextBox:
                return true;
            }

            return false;
        }

        private void ResetShape(PowerPoint.Shape shape)
        {
            if (shape.Type == MsoShapeType.msoDiagram) {

            } else if (shape.Type == MsoShapeType.msoGroup) {
                for (int index = 1; index <= shape.GroupItems.Count; ++index) {
                    ResetShape(shape.GroupItems[index]);
                }   
            }

            if (shape.HasTextFrame == MsoTriState.msoTrue && shape.TextFrame.TextRange.Text != "") {
                shape.TextFrame.DeleteText();
                var effect = shape;
                
                shape.TextFrame.TextRange.Text = "내용을 입력해주세요....";
                CopyTextEffect(effect, shape);
            }
        }

        private void CopyTextEffect(PowerPoint.Shape shapeA, PowerPoint.Shape shapeB)
        {
            if (shapeA.TextFrame.HasText == MsoTriState.msoTrue && shapeB.TextFrame.HasText == MsoTriState.msoTrue) {
                PowerPoint.TextRange textA = shapeA.TextFrame.TextRange;
                PowerPoint.TextRange textB = shapeB.TextFrame.TextRange;

                // 글꼴 복사
                textB.Font.Name = textA.Font.Name;
                textB.Font.Size = textA.Font.Size;
                textB.Font.Bold = textA.Font.Bold;
                textB.Font.Italic = textA.Font.Italic;
                textB.Font.Underline = textA.Font.Underline;
                textB.Font.Color.RGB = textA.Font.Color.RGB;
                textB.Font.Shadow = textA.Font.Shadow;
                textB.Font.Emboss = textA.Font.Emboss;
                textB.Font.Subscript = textA.Font.Subscript;
                textB.Font.Superscript = textA.Font.Superscript;
            }
        }
    }
}
