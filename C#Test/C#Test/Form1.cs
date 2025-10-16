using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Test {
    public partial class Form1 : Form {
        public Form1() {
            this.Paint += new PaintEventHandler(OnPaint);

            for (int buttonCount = 0; buttonCount < 6; ++buttonCount) {
                this.ButtonList.Add(new Button());
                this.ButtonList.Last().Location = new System.Drawing.Point(80 + 120 * buttonCount, 80);
                this.ButtonList.Last().Name = "button" + (buttonCount + 1) + "";
                this.ButtonList.Last().Size = new System.Drawing.Size(100, 50); //button1
                this.ButtonList.Last().TabIndex = 0;
                this.ButtonList.Last().Text = buttonCount + 1 + "";
                this.ButtonList.Last().UseVisualStyleBackColor = true;
                this.ButtonList.Last().Click += new System.EventHandler(this.button_Click);
                this.Controls.Add(this.ButtonList.Last());

                this.TextBoxList.Add(new TextBox());
                this.TextBoxList.Last().Location = new System.Drawing.Point(80 + 120 * buttonCount, 500);
                this.TextBoxList.Last().Name = "textbox" + (buttonCount + 1) + ""; //textbox1
                this.TextBoxList.Last().Size = new System.Drawing.Size(100, 50);
                this.TextBoxList.Last().Click += new System.EventHandler(this.textbox_Click);
                this.Controls.Add(this.TextBoxList.Last());
            }

            InitializeComponent();
        }

        void OnPaint(object sender, PaintEventArgs e) {
            Graphics graphics = e.Graphics;
            graphics.Clear(Color.White);
            Pen pen = new Pen(Color.Black);

            for (int index = 0; index < 6; ++index) {
                var button = this.ButtonList[index];
                var textbox = this.TextBoxList[index];
                graphics.DrawLine(pen, button.Bounds.X + button.Bounds.Width / 2, button.Bounds.Bottom,
                    textbox.Bounds.X + textbox.Bounds.Width / 2, textbox.Bounds.Top);
            }

            if (pointsData != null) {
                foreach (var pointData in pointsData) {
                    if(pointData != null) {
                        foreach (var pointNode in pointData) {
                            if (pointNode.GetRight() != null) {
                                graphics.DrawLine(pen, pointNode.Position, pointNode.GetRight().Position);
                            }
                        }
                    }
                }
            }

            pen.Dispose();
            graphics.Dispose();
        }

        public async void OnAnimation(int startNodeIndex) {
            if (isSetLadder == false) {
                return;
            }

            timer.Interval = 200;
            timer.Tick += new EventHandler(Animation_DrawLine);

            var startNode = pointsData[startNodeIndex][0];
            var graphics = CreateGraphics();
            Point startPoint = new Point(ButtonList[startNodeIndex].Bounds.Left + ButtonList[startNodeIndex].Bounds.Width / 2
                , ButtonList[startNodeIndex].Bounds.Bottom);

            Pen drawPen = new Pen(Color.Red);
            drawPen.Width = 5;
            graphics.DrawLine(drawPen, startPoint, startNode.Position);
            var beforeNode = startNode;
            PointNode drawNode = null;
            

            while (startNode.downPoint != null) {
                
                drawNode = startNode;
                
                if (startNode.GetLeft() != null && beforeNode != startNode.GetLeft()) {
                    beforeNode = startNode;
                    startNode = startNode.GetLeft();
                } else if(startNode.GetRight() != null && beforeNode != startNode.GetRight()) {
                    beforeNode = startNode;
                    startNode = startNode.GetRight();
                } else if (startNode.downPoint != null) {

                    startNode = startNode.downPoint;
                } else {
                    startNode = null;
                }

                if (startNode != null) {

                    animationStartPoint = drawNode.Position;
                    animationEndPoint = startNode.Position;

                    //graphics.DrawLine(drawPen, drawNode.Position, startNode.Position);

                    graphics.DrawLine(drawPen, drawNode.Position, animationStartPoint);

                    timer.Start();
                }
            }

            

            if (drawNode != null) {
                var desPoint = drawNode.Position;
                desPoint.Y += 30;
                drawPen.Width = 10;

                foreach (var textBox in TextBoxList) {
                    if (textBox.Bounds.Contains(desPoint)) {
                        graphics.DrawRectangle(drawPen, textBox.Bounds);

                        break;
                    }
                }
            }

            drawPen.Dispose();
            graphics.Dispose();
            return;
        }

        private PointF animationStartPoint;
        private PointF animationEndPoint;
        private Timer timer = new Timer();
        public void Animation_DrawLine(object sender, EventArgs e) {
            
            animationStartPoint.X += (animationEndPoint.X - animationStartPoint.X) * 0.2f;
            animationStartPoint.Y += (animationEndPoint.Y - animationStartPoint.Y) * 0.2f;
            if (animationStartPoint == animationEndPoint) {
                timer.Stop();
            }
        }


        private bool isSetLadder = false;
        private List<PointNode[]> pointsData = new List<PointNode[]>();
        private void SetLadderButton_Click(object sender, EventArgs e) {
            this.ResultTextBox.Visible = false;
            this.ResultTextBox.Text = "";

            Button ladderButton = sender as Button;
            if(ladderButton.Text == "Reset Ladder") {
                foreach (var pointData in pointsData) {
                    foreach (var pointNode in pointData) {
                        pointNode.Clear();
                    }
                }

                pointsData.Clear();
                ladderButton.Text = "Set Ladder";
                ladderButton.BackColor = Color.Green;
                isSetLadder = false;

                Refresh();
                return;
            }

            Random random = new Random();
            for (int index = 0; index < 6; ++index) {
                var button = this.ButtonList[index];
                var textbox = this.TextBoxList[index];

                PointNode[] buttonNodes = new PointNode[13];
                for(int index2 = 0; index2 < 13; ++index2) {
                    var nodeX = (button.Bounds.X + button.Bounds.Width / 2);
                    var nodeY = button.Bottom + (textbox.Bounds.Top - button.Bottom) / 13 * (index2 + 1) + 20;

                    buttonNodes[index2] = new PointNode(new Point(nodeX, nodeY));
                    if(index2 != 0) {
                        buttonNodes[index2 - 1].downPoint = buttonNodes[index2];
                    }
                }

                pointsData.Add(buttonNodes);
            }

            Random rand = new Random();
            for (int index = 0; index < 5; ++index) {
                var points = pointsData[index];
                var div = index % 2;
                for (int index2 = 0; index2 < 12; ++index2) {
                    var isSet = rand.Next(1, 50) % 2;
                    if (index2 % 2 == div && isSet == 1) {
                        pointsData[index + 1][index2].SetLeft(points[index2]);
                        points[index2].SetRight(pointsData[index + 1][index2]);
                    }
                }
            }

            ladderButton.Text = "Reset Ladder";
            ladderButton.BackColor = Color.Red;
            isSetLadder = true;
            Refresh();
        }

        private void ResetValueButton_Click(object sender, EventArgs e) {
            this.ResultTextBox.Visible = false;
            this.ResultTextBox.Text = "";

            foreach (var textBox in this.TextBoxList) {
                textBox.Text = "";
            }
        }

        private void ResetAllButton_Click(object sender, EventArgs e) {
            this.ResultTextBox.Visible = false;
            this.ResultTextBox.Text = "";

            foreach (var textBox in this.TextBoxList) {
                textBox.Text = "";
            }

            if (this.SetLadderButton.Text == "Reset Ladder") {
                foreach (var pointData in pointsData) {
                    foreach (var pointNode in pointData) {
                        pointNode.Clear();
                    }
                }

                pointsData.Clear();
                this.SetLadderButton.Text = "Set Ladder";
                this.SetLadderButton.BackColor = Color.Green;
                

                isSetLadder = false;    
            }

            Refresh();
            return;
        }

        private void ShowResultButton_Click(object sender, EventArgs e) {
            this.ResultTextBox.Visible = false;
            this.ResultTextBox.Text = "";

            var result = GetAllResult();
            if (result == null) {
                return;
            }

            this.ResultTextBox.Visible = true;

            int buttonNum = 1;
            foreach (var str in result) {
                this.ResultTextBox.Text += " " + (buttonNum++).ToString() + " --> " + str;
                this.ResultTextBox.Text += Environment.NewLine + Environment.NewLine;
            }

            
        }

        private void button_Click(object sender, EventArgs e) {
            Button clickedButton = sender as Button;
            if (clickedButton == null) {
                return;
            }

            string buttonName = clickedButton.Name;
            int buttonNum = 0;
            int.TryParse(buttonName.Last<char>().ToString(), out buttonNum);

            Refresh();
            OnAnimation(--buttonNum);
        }

        private void textbox_Click(object sender, EventArgs e) {
            TextBox clickedTextbox = sender as TextBox;
            if (clickedTextbox == null) {
                return;
            }
        }

        private List<string> GetAllResult() {
            if (pointsData.Count == 0) {
                return null;
            }

            List<string> result = new List<string>();
            for (int index = 0; index < 6; ++index) {
                var startNode = pointsData[index][0];
                var beforeNode = startNode;
                PointNode lastNode = null;
                while (startNode.downPoint != null) {
                    lastNode = startNode;

                    if (startNode.GetLeft() != null && beforeNode != startNode.GetLeft()) {
                        beforeNode = startNode;
                        startNode = startNode.GetLeft();
                    } else if (startNode.GetRight() != null && beforeNode != startNode.GetRight()) {
                        beforeNode = startNode;
                        startNode = startNode.GetRight();
                    } else if (startNode.downPoint != null) {

                        startNode = startNode.downPoint;
                    } else {
                        startNode = null;
                    }
                }

                if (lastNode != null) {
                    var desPoint = lastNode.Position;
                    desPoint.Y += 30;

                    foreach (var textBox in TextBoxList) {
                        if (textBox.Bounds.Contains(desPoint)) {
                            result.Add(textBox.Text);
                            break;
                        }
                    }
                }
                
            }

            return result;
        }
    }
}
