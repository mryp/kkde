using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace kkde.kagex
{
	/// <summary>
	/// 入力なしと有効の選択肢を表示するプロパティグリッドのコンバーター
	/// </summary>
	class PropertyGridOnOffConverter : StringConverter 
	{
		/// <summary>
		/// リスト表示する文字列を返す
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new StandardValuesCollection(new string[] { "", "true"});
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
