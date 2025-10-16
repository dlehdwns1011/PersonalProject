using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Core;
using OOXMLDataLoader;

using HShow = HShowObject;

namespace automationtest {
    public partial class Form1 : Form {

        int ere = 0;
        public Form1() {
            
            InitializeComponent();
            var slideinfos = OOXMLDataLoader.SlideInfoMaker.GetSlideInfo(@"C:\Users\dongjun.lee\AppData\Roaming\Hnc\User\Assistant Template\제비\Example Template.pptx");

            //PowerPoint.Application application = new PowerPoint.Application();



            //var hello = application.Presentations.Open("C:\\Users\\dongjun.lee\\Downloads\\PowerPointAssistant 빌드.pptx", 0, MsoTriState.msoFalse, MsoTriState.msoFalse);
            ////hello.SaveAs("test.pptx");

            ////hello.Close();
            ////application.Quit();
            //application.Activate();    

            return;
        }
    }
}
