using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Test {
    internal static class Program {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main() {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            PptTextExtractor pptTextExtractor = new PptTextExtractor();
            pptTextExtractor.ExtractAllTextFromPpt(@"C:\Users\dongjun.lee\Documents\dataloadertest\애국가.pptx");

        }
    }
}
