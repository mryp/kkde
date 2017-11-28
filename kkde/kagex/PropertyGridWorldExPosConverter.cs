using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace kkde.kagex
{
	/// <summary>
	/// プロパティグリッドの変換クラス（ポジション用）
	/// </summary>
	public class PropertyGridWorldExPosConverter : StringConverter 
	{
		static List<string> staticlist = null;
		public static void SetList(List<string> list)
		{
			staticlist = list;
		}

		/// <summary>
		/// リスト表示する文字列を返す
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			List<string> list = new List<string>();
			list.Add("");	//何も選択しない状態を追加
			if (staticlist != null)
			{
				list.AddRange(staticlist);	//アクションリストを追加
			}

			return new StandardValuesCollection(list.ToArray());
		}

		/// <summary>
		/// GetStandardValues関数を呼び出すかどうか
		/// trueのとき呼び出される
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;	//GetStandardValues を呼び出すようにする
		}

		/// <summary>
		/// ユーザー入力を認めるかどうか
		/// trueのときは認めない（リストを選択するだけ）
		/// falseのときは入力できる
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;	//ユーザー入力不可とする
		}
	}
}
