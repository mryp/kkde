using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace kkde.option
{
	/// <summary>
	/// ファイルタイプ共通のカラー設定を保持するクラス
	/// </summary>
	public class BaseColorType
	{
		#region フィールド
		private Color m_windowFront;
		private Color m_windowBack;
		private Color m_selectFront;
		private Color m_selectBack;
		private Color m_vRuler;
		private Color m_invalidLines;
		private Color m_caretMarker;
		private Color m_lineNumFront;
		private Color m_lineNumBack;
		private Color m_foldLineFront;
		private Color m_foldLineBack;
		private Color m_foldMarkerFront;
		private Color m_foldMarkerBack;
		private Color m_selectFoldLineFront;
		private Color m_selectFoldLineBack;
		private Color m_eolMarkers;
		private Color m_spaceMakers;
		private Color m_tabMarkers;
		private Color m_digits;
		#endregion

		[Category("ウィンドウ")]
		[Description("エディタの通常文字色")]
		public Color WindowFront
		{
			get { return m_windowFront; }
			set { m_windowFront = value; }
		}

		[Category("ウィンドウ")]
		[Description("エディタの通常文字背景色")]
		public Color WindowBack
		{
			get { return m_windowBack; }
			set { m_windowBack = value; }
		}

		[Category("ウィンドウ")]
		[Description("選択範囲の文字色")]
		public Color SelectTextFront
		{
			get { return m_selectFront; }
			set { m_selectFront = value; }
		}

		[Category("ウィンドウ")]
		[Description("選択範囲の背景色")]
		public Color SelectTextBack
		{
			get { return m_selectBack; }
			set { m_selectBack = value; }
		}

		[Category("記号")]
		[Description("縦ルーラの色")]
		public Color VRuler
		{
			get { return m_vRuler; }
			set { m_vRuler = value; }
		}

		[Category("記号")]
		[Description("ファイルの最後より後ろの行であることを示す記号の色")]
		public Color InvalidLines
		{
			get { return m_invalidLines; }
			set { m_invalidLines = value; }
		}

		[Category("記号")]
		[Description("カーソル行マーカーの色")]
		public Color CaretMarker
		{
			get { return m_caretMarker; }
			set { m_caretMarker = value; }
		}

		[Category("行番号")]
		[Description("行番号の文字色")]
		public Color LineNumbersFront
		{
			get { return m_lineNumFront; }
			set { m_lineNumFront = value; }
		}

		[Category("行番号")]
		[Description("行番号の背景色")]
		public Color LineNumbersBack
		{
			get { return m_lineNumBack; }
			set { m_lineNumBack = value; }
		}

		[Category("折りたたみ")]
		[Description("折りたたみ線の色")]
		public Color FoldLineFront
		{
			get { return m_foldLineFront; }
			set { m_foldLineFront = value; }
		}

		[Category("折りたたみ")]
		[Description("折りたたみ線の背景色")]
		public Color FoldLineBack
		{
			get { return m_foldLineBack; }
			set { m_foldLineBack = value; }
		}

		[Category("折りたたみ")]
		[Description("折りたたみ可能記号の色")]
		public Color FoldMarkerFront
		{
			get { return m_foldMarkerFront; }
			set { m_foldMarkerFront = value; }
		}

		[Category("折りたたみ")]
		[Description("折りたたみ可能記号の背景色")]
		public Color FoldMarkerBack
		{
			get { return m_foldMarkerBack; }
			set { m_foldMarkerBack = value; }
		}

		[Category("折りたたみ")]
		[Description("折りたたみ可能記号を選択したときの色")]
		public Color SelectFoldLineFront
		{
			get { return m_selectFoldLineFront; }
			set { m_selectFoldLineFront = value; }
		}

		[Category("折りたたみ")]
		[Description("折りたたみ可能記号を選択したときの背景色")]
		public Color SelectFoldLineBack
		{
			get { return m_selectFoldLineBack; }
			set { m_selectFoldLineBack = value; }
		}

		[Category("記号")]
		[Description("行末記号の色")]
		public Color EolMarkers
		{
			get { return m_eolMarkers; }
			set { m_eolMarkers = value; }
		}

		[Category("記号")]
		[Description("スペース記号の色")]
		public Color SpaceMakers
		{
			get { return m_spaceMakers; }
			set { m_spaceMakers = value; }
		}

		[Category("記号")]
		[Description("タブマーク記号の色")]
		public Color TabMarkers
		{
			get { return m_tabMarkers; }
			set { m_tabMarkers = value; }
		}

		[Category("数字")]
		[Description("数字文字の色")]
		public Color Digits
		{
			get { return m_digits; }
			set { m_digits = value; }
		}
	}
}
