using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace kkde.option
{
	/// <summary>
	/// ツールバーの方向
	/// </summary>
	public enum ToolBarDirection
	{
		Top,
		Bottom,
		Left,
		Right,
	}

	/// <summary>
	/// ツールバーの位置を保持するクラス
	/// </summary>
	public class ToolBarPos : IComparable
	{
		ToolBarDirection m_direction = ToolBarDirection.Top;
		Point m_pos = new Point(-1, -1);

		/// <summary>
		/// ツールバーをセットする方向
		/// </summary>
		public ToolBarDirection Direction
		{
			get { return m_direction; }
			set { m_direction = value; }
		}
		
		/// <summary>
		/// ツールバーの位置
		/// </summary>
		public Point Location
		{
			get { return m_pos; }
			set { m_pos = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="pos">位置</param>
		/// <param name="dirc">方向</param>
		public ToolBarPos(Point pos, ToolBarDirection dirc)
		{
			m_pos = pos;
			m_direction = dirc;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ToolBarPos()
		{
		}

		#region IComparable メンバ
		/// <summary>
		/// 位置を比較する（ソート用）
		/// </summary>
		public int CompareTo(object obj)
		{
			ToolBarPos pos = obj as ToolBarPos;
			if (pos == null)
			{
				throw new ArgumentException("引数がToolBarPos型ではありません");
			}

			int comp = this.Location.Y.CompareTo(pos.Location.Y);	//縦を比較する
			if (comp == 0)
			{
				comp = this.Location.X.CompareTo(pos.Location.X);	//縦が同じ時は横で比較する
			}
			return comp;
		}
		#endregion
	}
}
