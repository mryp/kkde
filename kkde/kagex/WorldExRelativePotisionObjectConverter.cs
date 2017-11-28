using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace kkde.kagex
{
	/// <summary>
	/// WorldExRelativePotisionの値をPropertyGridで使用できるようにする変換クラス
	/// </summary>
	public class WorldExRelativePotisionObjectConverter : ExpandableObjectConverter
	{
		/// <summary>
		/// 指定した型に変換できるかどうか
		/// </summary>
		/// <param name="context"></param>
		/// <param name="destinationType"></param>
		/// <returns></returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(WorldExRelativePotision))
			{
				return true;
			}
			return base.CanConvertTo(context, destinationType);
		}

		/// <summary>
		/// WorldExPotisionオブジェクトを文字列型に変換する
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <param name="destinationType"></param>
		/// <returns></returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is WorldExRelativePotision)
			{
				WorldExRelativePotision pos = (WorldExRelativePotision)value;
				return pos.ToString();
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>
		/// 指定した型から変換できるかどうか
		/// </summary>
		/// <param name="context"></param>
		/// <param name="sourceType"></param>
		/// <returns></returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
			{
				return true;
			}

			return base.CanConvertFrom(context, sourceType);
		}

		/// <summary>
		/// 文字列からWorldExPostionオブジェクトに変換する
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string str = (string)value;
				WorldExRelativePotision pos = new WorldExRelativePotision();
				if (str.IndexOf("@") != -1)
				{
					pos.IsRelative = true;
					str = str.Replace("@", "");	//消去する
				}

				pos.Pos = str;
				return pos;
			}

			return base.ConvertFrom(context, culture, value);
		}

	}
}
