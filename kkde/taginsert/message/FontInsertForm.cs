using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;

namespace kkde.taginsert.message
{
	/// <summary>
	/// メッセージ→フォント
	/// </summary>
	public partial class FontInsertForm : kkde.taginsert.BaseInsertForm
	{
		const string FONT_NAME_NON_SELECT = "";

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FontInsertForm()
		{
			InitializeComponent();

			initFontName();
		}

		/// <summary>
		/// フォント名を取得しコンボボックスにセットする
		/// </summary>
		private void initFontName()
		{
			fontNameComboBox.Items.Clear();
			fontNameComboBox.Items.Add(FONT_NAME_NON_SELECT);
			fontNameComboBox.SelectedIndex = 0;

			//フォント一覧を取得する
			InstalledFontCollection ifc = new InstalledFontCollection();
			foreach (FontFamily ff in ifc.Families)
			{
				if (ff.IsStyleAvailable(FontStyle.Regular))
				{
					fontNameComboBox.Items.Add(ff.Name);
				}
			}
		}

		/// <summary>
		/// フォントカラーを取得する
		/// </summary>
		private void fontColorRefButton_Click(object sender, EventArgs e)
		{
			if (colorSelectDialog.ShowDialog() == DialogResult.OK)
			{
				fontColorTextBox.Text = colorToRRGGBBString(colorSelectDialog.Color);
			}
		}

		/// <summary>
		/// 影の色を取得する
		/// </summary>
		private void shadowColorRefButton_Click(object sender, EventArgs e)
		{
			if (colorSelectDialog.ShowDialog() == DialogResult.OK)
			{
				shadowColorTextBox.Text = colorToRRGGBBString(colorSelectDialog.Color);
			}
		}

		/// <summary>
		/// 縁取りの色を取得する
		/// </summary>
		private void edgeColorRefButton_Click(object sender, EventArgs e)
		{
			if (colorSelectDialog.ShowDialog() == DialogResult.OK)
			{
				edgeColorTextBox.Text = colorToRRGGBBString(colorSelectDialog.Color);
			}
		}

		/// <summary>
		/// カラーから0xRRGGBB形式の文字列を返す
		/// </summary>
		/// <param name="c">カラー</param>
		/// <returns>0xRRGGBB文字列</returns>
		private string colorToRRGGBBString(Color c)
		{
			return string.Format("0x{0:X2}{1:X2}{2:X2}", c.R, c.G, c.B);
		}

		/// <summary>
		/// コードを取得する
		/// </summary>
		/// <returns></returns>
		public override string GetCode()
		{
			string startCode = "[font";
			string code = startCode;
			if (fontNameComboBox.SelectedText != FONT_NAME_NON_SELECT)
			{
				code += String.Format(" face=\"\"");
			}
			if (fontSizeTextBox.Text != "")
			{
			}
			if (fontColorTextBox.Text != "")
			{
			}
			if (italicCheckBox.Checked)
			{
			}
			if (boldCheckBox.Checked)
			{
			}
			if (shadowCheckBox.Checked)
			{
			}
			if (edgeCheckBox.Checked)
			{
			}

			if (code == startCode)
			{
				return "";	//はじめから何も変わっていないときは何も挿入しない
			}

			if (code != "")
			{
				code += "]";
				code += InputText;
				code += "[resetfont]";
			}
			return "";
		}
	}
}
