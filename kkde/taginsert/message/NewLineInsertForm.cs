using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace kkde.taginsert.message
{
	/// <summary>
	/// 改行タグを挿入するフォーム
	/// </summary>
	public partial class NewLineInsertForm : kkde.taginsert.BaseInsertForm
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public NewLineInsertForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 挿入するコード
		/// </summary>
		/// <returns>挿入コード</returns>
		public override string GetCode()
		{
			return "[r]";
		}
	}
}
