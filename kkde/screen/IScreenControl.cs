using System;
using System.Collections.Generic;
using System.Text;
using kkde.screen.control;
using System.Drawing;
using System.Xml;

namespace kkde.screen
{
	/// <summary>
	/// スクリーンコントロール用インターフェース
	/// </summary>
	public interface IScreenControl
	{
		/// <summary>
		/// パラメーターの取得（プロパティエディタに表示するプロパティ）
		/// </summary>
		/// <returns></returns>
		BaseType GetPropertyParam();

		/// <summary>
		/// 画像ファイルパスが変更されたときに呼ばれるメソッド
		/// </summary>
		/// <param name="filePath"></param>
		void SetImagePath(String filePath);

		/// <summary>
		/// 移動用メソッド（プロパティエディタで位置の値を変更したときに実行するメソッド）
		/// </summary>
		/// <param name="pos"></param>
		void MoveControl(Point pos);

		/// <summary>
		/// 現在のコントロールの情報をXMLに書き出す
		/// </summary>
		/// <param name="xw"></param>
		void WriteXml(XmlTextWriter xw);

		/// <summary>
		/// XMLからコントロールの情報を読み込む
		/// </summary>
		/// <param name="xr"></param>
		void ReadXml(XmlTextReader xr);
	}
}
