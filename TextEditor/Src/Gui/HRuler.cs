// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision: 1925 $</version>
// </file>

using System;
using System.Drawing;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;
using System.Diagnostics;

namespace ICSharpCode.TextEditor
{
	/// <summary>
	/// Horizontal ruler - text column measuring ruler at the top of the text area.
	/// </summary>
	public class HRuler : Control
	{
		TextArea textArea;
		
		public HRuler(TextArea textArea)
		{
			this.Height = 5;
			this.textArea = textArea;
		}
		
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			int num = textArea.VirtualTop.X;	//スクロール位置から開始番号を決定する
			HighlightColor color = textArea.Document.HighlightingStrategy.GetColorFor("Default");
			Font editorFont = color.GetFont(textArea.Document.TextEditorProperties.FontContainer);
			Font font = new Font(editorFont.FontFamily, editorFont.Size * 0.75F);
			for (float x = textArea.TextView.DrawingPosition.Left; x < textArea.TextView.DrawingPosition.Right; x += textArea.TextView.WideSpaceWidth) {
				int offset = Height - (int)((double)Height / (double)3);
				if (num % 5 == 0) {
					offset = Height - Height / 2;
				}
				
				if (num % 10 == 0) {
					//10桁ごとに位置番号を表示する
					offset = 2;
					g.DrawString(num.ToString(), font
						, BrushRegistry.GetBrush(color.Color), (float)x, (float)1);
				}

				//縦線描画
				++num;
				g.DrawLine(BrushRegistry.GetPen(color.Color),
				           (int)x, offset, (int)x, Height - 1);
			}

			//下線表示
			g.DrawLine(BrushRegistry.GetPen(color.Color), textArea.TextView.DrawingPosition.Left, Height - 1
				, textArea.TextView.DrawingPosition.Right, Height - 1);
		}
		
		protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
		{
			//横定規はエディタのデフォルト値を使用する
			HighlightColor color = textArea.Document.HighlightingStrategy.GetColorFor("Default");

			int x = 0;
			if (textArea.TextEditorProperties.IsIconBarVisible)
			{
				x = textArea.IconBarMargin.DrawingPosition.Width;
				if (x != 0)
				{
					//アイコン表示領域が存在するときはその部分を描画する
					e.Graphics.FillRectangle(BrushRegistry.GetBrush(SystemColors.Control),
										 new Rectangle(0, 0, x, Height));
					e.Graphics.DrawLine(SystemPens.ControlDark, x - 1, 0, x - 1, Height);
				}
			}
			e.Graphics.FillRectangle(BrushRegistry.GetBrush(color.BackgroundColor),
									 new Rectangle(x,
			                                       0,
			                                       Width,
			                                       Height));
		}
	}
}
