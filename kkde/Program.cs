using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace kkde
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

			MainForm mainForm = new MainForm();
			//util.SplashForm.ShowSplash(mainForm);	//スプラッシュフォームは使用しない
			Application.Run(mainForm);
        }
    }
}
