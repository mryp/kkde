using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.kagex
{
	/// <summary>
	/// アクションハンドラ情報を管理するクラス
	/// </summary>
	public static class ActionHandlerManager
	{
		#region 定数
		public const string HANDLER_NAME_NULL = "NULL";
		public const string HANDLER_NAME_INT = "Integer";
		public const string HANDLER_NAME_MOVE = "MoveAction";
		public const string HANDLER_NAME_RANDOM = "RandomAction";
		public const string HANDLER_NAME_SQUARE = "SquareAction";
		public const string HANDLER_NAME_TRIANGLE = "TriangleAction";
		public const string HANDLER_NAME_SIN = "SinAction";
		public const string HANDLER_NAME_COS = "CosAction";
		public const string HANDLER_NAME_FALL = "FallAction";
		public const string HANDLER_NAME_LOOP = "LoopMoveAction";

		public static readonly string[] HandlerNameList = { HANDLER_NAME_NULL, HANDLER_NAME_INT, HANDLER_NAME_MOVE, HANDLER_NAME_RANDOM, HANDLER_NAME_SQUARE
									, HANDLER_NAME_TRIANGLE, HANDLER_NAME_SIN, HANDLER_NAME_COS, HANDLER_NAME_FALL, HANDLER_NAME_LOOP};
		#endregion

		#region 静的メソッド
		/// <summary>
		/// ハンドラ名からアクションクラスを生成する
		/// </summary>
		/// <param name="name">ハンドラ名（HANDLER_NAME_～）</param>
		/// <returns>アクション情報クラスを返却する</returns>
		public static IActionInfo CreateActionInfoFromName(string name)
		{
			IActionInfo info = new NullActionInfo(); ;
			switch (name)
			{
				case HANDLER_NAME_NULL:
					info = new NullActionInfo();
					break;
				case HANDLER_NAME_INT:
					info = new IntegerActionInfo();
					break;
				case HANDLER_NAME_MOVE:
					info = new MoveActionInfo();
					break;
				case HANDLER_NAME_RANDOM:
					info = new RandomActionInfo();
					break;
				case HANDLER_NAME_SQUARE:
					info = new SquareActionInfo();
					break;
				case HANDLER_NAME_TRIANGLE:
					info = new TriangleActionInfo();
					break;
				case HANDLER_NAME_SIN:
					info = new SinActionInfo();
					break;
				case HANDLER_NAME_COS:
					info = new CosActionInfo();
					break;
				case HANDLER_NAME_FALL:
					info = new FallActionInfo();
					break;
				case HANDLER_NAME_LOOP:
					info = new LoopMoveActionInfo();
					break;
				default:
					info = new NullActionInfo();
					break;
			}

			return info;
		}
		#endregion
	}
}
