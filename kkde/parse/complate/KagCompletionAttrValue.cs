using System;
using System.Collections.Generic;
using System.Text;
using kkde.global;
using kkde.project;
using kkde.option;
using System.IO;

namespace kkde.parse.complate
{
	/// <summary>
	/// KAG属性値の入力補完リストを扱うクラス
	/// </summary>
	public static class KagCompletionAttrValue
	{
		#region 定数
		//ファイル名リスト
		public const string TYPE_FILE_SCENARIO = "シナリオファイル名";
		public const string TYPE_FILE_IMAGE = "画像ファイル名";
		public const string TYPE_FILE_SE = "効果音ファイル名";
		public const string TYPE_FILE_CURSOR = "カーソルファイル名";
		public const string TYPE_FILE_BGM = "BGMファイル名";
		public const string TYPE_FILE_ACTION = "領域アクション定義ファイル名";
		public const string TYPE_FILE_PLUGIN = "プラグインファイル名";
		public const string TYPE_FILE_FONT = "フォントファイル名";
		public const string TYPE_FILE_VIDEO = "ムービーファイル名";

		//数値
		public const string TYPE_NUM_ZERO_OVER = "0以上の値";
		public const string TYPE_NUM_ONE_OVER = "1以上の値";
		public const string TYPE_NUM_PERCENT = "パーセント値";
		public const string TYPE_NUM_BYTE = "256値";
		public const string TYPE_NUM_MSTIME = "ミリ秒時間";
		public const string TYPE_NUM_REAL = "実数値";
		public const string TYPE_NUM_PAN = "-100～100の値";
		public const string TYPE_NUM_RGB = "RGB値";

		public const string TYPE_NUM_ARGB = "ARGB値";
		public const string TYPE_NUM_PMBYTE = "-255～255の値";
		public const string TYPE_NUM_HUE = "-180～180の値";

		//その他の文字列
		public const string TYPE_STRING_TJS = "TJS式";
		public const string TYPE_STRING_FONT = "フォント名";
		public const string TYPE_STRING_OTHER = "任意文字列";

		//最大値指定
		public const string TYPE_MAX_VIDEO_OBJECT = "ムービーオブジェクト番号";
		public const string TYPE_MAX_SE_BUFFER = "効果音バッファ番号";
		public const string TYPE_MAX_LAYER = "前景レイヤ";
		public const string TYPE_MAX_MESSAGE_LAYER = "メッセージレイヤ";

		//定数
		public const string TYPE_CONST_LAYER_PAGE = "レイヤーページ";
		public const string TYPE_CONST_LAYER_POS = "レイヤ位置";
		public const string TYPE_CONST_BOOL = "論理値";
		public const string TYPE_CONST_CURSOR = "カーソル定数";
		public const string TYPE_CONST_BASE_LAYER = "背景レイヤ";
		public const string TYPE_CONST_COLORCOMP_MODE = "合成モード";
		public const string TYPE_CONST_KAGEX_ACTION = "アクション";
		public const string TYPE_CONST_KAGEX_LTBMODE = "レイヤ切り替え種別";

		//プラグインによって変化
		public const string TYPE_OTHER_TRANSMETH = "トランジションタイプ";

		//シナリオ状態によって変化
		public const string TYPE_STATE_LABEL = "ラベル名";
		public const string TYPE_STATE_ASD_LABEL = "ASDラベル名";
		public const string TYPE_STATE_MACRO = "マクロ名";

		//KAGEXワールド拡張関連
		public const string TYPE_WORLDEX_CHAR_NAME = "キャラクタ名前WORLDEX";
		public const string TYPE_WORLDEX_CHAR_POSE = "キャラクタ姿勢WORLDEX";
		public const string TYPE_WORLDEX_CHAR_DRESS = "キャラクタ服装WORLDEX";
		public const string TYPE_WORLDEX_CHAR_FACE = "キャラクタ表情WORLDEX";
		public const string TYPE_WORLDEX_TRANS = "トランジションWORLDEX";
		public const string TYPE_WORLDEX_ACTION = "アクションWORLDEX";
		public const string TYPE_WORLDEX_LEVEL = "レベルWORLDEX";
		public const string TYPE_WORLDEX_POS = "ポジションWORLDEX";
		public const string TYPE_WORLDEX_STAGE = "舞台名WORLDEX";
		public const string TYPE_WORLDEX_TIME = "舞台時間WORLDEX";

		/// <summary>
		/// ファイル名を指定する属性名
		/// </summary>
		public const string TAG_ATTR_FILENAME = "storage";

		/// <summary>
		/// メッセージレイヤにつけるプレフィクス
		/// </summary>
		public const string TAG_PREFIX_MESSAGELAYER = "message";
		#endregion

		/// <summary>
		/// KAG属性値入力補完データリストを取得する
		/// </summary>
		/// <param name="attr">属性情報</param>
		/// <param name="compInfo">入力補完情報</param>
		/// <returns>入力補完データリスト</returns>
		public static KagCompletionData[] GetCompletionDataList(KagMacroAttr attr, KagTagKindInfo compInfo)
		{
			if (attr == null)
			{
				return null;	//何も返せない
			}
			//設定取得
			EditorOption option = GlobalStatus.EditorManager.GetEditorOption(FileType.KrkrType.Kag);
			if (option == null)
			{
				return null;	//何も返せない
			}
			string[] valueTypeList = splitAttrValue(attr.ValueType);
			if (valueTypeList == null)
			{
				return null;
			}

			List<KagCompletionData> dataList = new List<KagCompletionData>();
			foreach (string valueType in valueTypeList)
			{
				//属性値リスト取得
				string[] valueList = getAttrValueList(valueType, option.KagCompOption, compInfo);
				if (valueList == null)
				{
					continue;
				}

				//取得した属性値を追加
				foreach (string value in valueList)
				{
					dataList.Add(new KagCompletionData(value, attr.Comment, KagCompletionData.Kind.AttrValue));
				}
			}
			
			return dataList.ToArray();
		}

		/// <summary>
		/// 属性値タイプ文字列から属性値リストを取得する
		/// </summary>
		/// <param name="valueType">属性値タイプ</param>
		/// <param name="option">KAG入力補完オプション</param>
		/// <returns>属性値リスト</returns>
		private static string[] getAttrValueList(string valueType, KagCompletionOption option, KagTagKindInfo compInfo)
		{
			string[] list = null;
			switch (valueType)
			{
				case TYPE_FILE_SCENARIO:
					list = getFileNameList(option.ScenarioFileExt, TYPE_FILE_SCENARIO);
					break;
				case TYPE_FILE_IMAGE:
					list = getFileNameList(option.ImageFileExt, TYPE_FILE_IMAGE);
					break;
				case TYPE_FILE_SE:
					list = getFileNameList(option.SeFileExt, TYPE_FILE_SE);
					break;
				case TYPE_FILE_CURSOR:
					list = getFileNameList(option.CursorFileExt, TYPE_FILE_CURSOR);
					break;
				case TYPE_FILE_BGM:
					list = getFileNameList(option.BgmFileExt, TYPE_FILE_BGM);
					break;
				case TYPE_FILE_ACTION:
					list = getFileNameList(option.ActionFileExt, TYPE_FILE_ACTION);
					break;
				case TYPE_FILE_PLUGIN:
					list = getFileNameListPlugin(option.PluginFileExt, TYPE_FILE_PLUGIN);
					break;
				case TYPE_FILE_FONT:
					list = getFileNameList(option.FontFileExt, TYPE_FILE_FONT);
					break;
				case TYPE_FILE_VIDEO:
					list = getFileNameList(option.VideoFileExt, TYPE_FILE_VIDEO);
					break;
				case TYPE_NUM_ZERO_OVER:
					list = splitAttrValue(option.ZeroOverNumberList);
					break;
				case TYPE_NUM_ONE_OVER:
					list = splitAttrValue(option.OneOverNumberList);
					break;
				case TYPE_NUM_PERCENT:
					list = splitAttrValue(option.PercentNumberList);
					break;
				case TYPE_NUM_BYTE:
					list = splitAttrValue(option.ByteNumberList);
					break;
				case TYPE_NUM_MSTIME:
					list = splitAttrValue(option.MsTimeNumberList);
					break;
				case TYPE_NUM_REAL:
					list = splitAttrValue(option.RealNumberList);
					break;
				case TYPE_NUM_PAN:
					list = splitAttrValue(option.PmHundredNumberList);
					break;
				case TYPE_NUM_RGB:
					list = splitAttrValue(option.RgbNumberList);
					break;
				case TYPE_NUM_ARGB:
					list = splitAttrValue(option.ArgbNumberList);
					break;
				case TYPE_NUM_PMBYTE:
					list = splitAttrValue(option.PmbyteNumberList);
					break;
				case TYPE_NUM_HUE:
					list = splitAttrValue(option.HueNumberList);
					break;
				case TYPE_STRING_TJS:
					list = splitAttrValue(option.TjsStringList);
					break;
				case TYPE_STRING_FONT:
					list = splitAttrValue(option.FontStringList);
					break;
				case TYPE_STRING_OTHER:
					list = splitAttrValue(option.OtherStringList);
					break;
				case TYPE_MAX_VIDEO_OBJECT:
					list = getNumberList(0, option.VideoBufferMaxNumber - 1);
					break;
				case TYPE_MAX_SE_BUFFER:
					list = getNumberList(0, option.SeBufferMaxNumber - 1);
					break;
				case TYPE_MAX_LAYER:
					list = getNumberList(0, option.LayerMaxNumber - 1);
					break;
				case TYPE_MAX_MESSAGE_LAYER:
					list = getNumberListForMeslay(0, option.MessageLayerMaxNumber - 1);
					break;
				case TYPE_CONST_LAYER_PAGE:
					list = splitAttrValue(option.LayerPageList);
					break;
				case TYPE_CONST_LAYER_POS:
					list = splitAttrValue(option.LayerPosList);
					break;
				case TYPE_CONST_BOOL:
					list = splitAttrValue(option.BoolValueList);
					break;
				case TYPE_CONST_CURSOR:
					list = splitAttrValue(option.CursorDefList);
					break;
				case TYPE_CONST_BASE_LAYER:
					list = splitAttrValue(option.BaseLayerList);
					break;
				case TYPE_OTHER_TRANSMETH:
					list = getTransMthodList();
					break;
				case TYPE_STATE_LABEL:
					list = getLabelList(compInfo);
					break;
				case TYPE_STATE_ASD_LABEL:
					//未実装
					break;
				case TYPE_CONST_COLORCOMP_MODE:
					list = splitAttrValue(option.ColorcompModeList);
					break;
				case TYPE_CONST_KAGEX_ACTION:
					list = splitAttrValue(option.KagexAction);
					break;
				case TYPE_CONST_KAGEX_LTBMODE:
					list = splitAttrValue(option.KagexLtbType);
					break;
				case TYPE_STATE_MACRO:
					list = getMacroNameList();
					break;
				case TYPE_WORLDEX_CHAR_NAME:
					list = getKagexCharNameList(compInfo);
					break;
				case TYPE_WORLDEX_CHAR_POSE:
					list = getKagexCharPoseList(compInfo);
					break;
				case TYPE_WORLDEX_CHAR_DRESS:
					list = getKagexCharDressList(compInfo);
					break;
				case TYPE_WORLDEX_CHAR_FACE:
					list = getKagexCharFaceList(compInfo);
					break;
				case TYPE_WORLDEX_TRANS:
					list = getKagexTransList();
					break;
				case TYPE_WORLDEX_ACTION:
					list = getKagexActionList();
					break;
				case TYPE_WORLDEX_LEVEL:
					list = getKagexLevelList();
					break;
				case TYPE_WORLDEX_POS:
					list = getKagexPosList();
					break;
				case TYPE_WORLDEX_STAGE:
					list = getKagexStageList();
					break;
				case TYPE_WORLDEX_TIME:
					list = getKagexTimeList();
					break;
				default:
					list = new string[] {valueType};	//見つからないときはそのままと判断する
					break;
			}

			return list;
		}

		/// <summary>
		/// 属性値タイプリストを分解する
		/// （例：0;1;2）
		/// </summary>
		/// <param name="valueList">属性値リスト</param>
		/// <returns>属性タイプ</returns>
		private static string[] splitAttrValue(string valueList)
		{
			string[] splitValue = valueList.Split(';');
			return splitValue;
		}

		/// <summary>
		/// 数値リストを作成する
		/// </summary>
		/// <param name="min">最小値</param>
		/// <param name="max">最大値</param>
		/// <returns>最小値から最大値までの数値リスト</returns>
		private static string[] getNumberList(int min, int max)
		{
			List<string> list = new List<string>();
			for (int i = min; i <= max; i++)
			{
				list.Add(i.ToString());
			}

			return list.ToArray();
		}

		/// <summary>
		/// メッセージレイヤリストを作成する
		/// </summary>
		/// <param name="min">最小値</param>
		/// <param name="max">最大値</param>
		/// <returns>最小値から最大値までのメッセージレイヤリスト</returns>
		private static string[] getNumberListForMeslay(int min, int max)
		{
			string[] numList = getNumberList(min, max);

			List<string> list = new List<string>();
			list.Add(TAG_PREFIX_MESSAGELAYER);		//デフォルトメッセージレイヤ
			foreach (string num in numList)
			{
				list.Add(TAG_PREFIX_MESSAGELAYER + num);
			}

			return list.ToArray();
		}

		/// <summary>
		/// ファイル名リストを取得する
		/// </summary>
		/// <param name="pattern">検索拡張子パターン（例：*.png;*.jpg）</param>
		/// <param name="valueType">属性値タイプ</param>
		/// <returns>ファイル名リスト</returns>
		private static string[] getFileNameList(string pattern, string valueType)
		{
			//検索ディレクトリを取得する
			string dirPath = GlobalStatus.Project.DataFullPath;
			if (String.IsNullOrEmpty(dirPath))
			{
				return null;
			}

			//パスを取得する
			string[] pathList = util.FileUtil.GetDirectoryFile(dirPath, pattern, SearchOption.AllDirectories);
			List<string> fileNameList = new List<string>();
			foreach (string path in pathList)
			{
				//ファイル名に変換する
				switch (valueType)
				{
					case TYPE_FILE_IMAGE:
					case TYPE_FILE_SE:
					case TYPE_FILE_BGM:
						fileNameList.Add(Path.GetFileNameWithoutExtension(path));	//拡張子なし
						break;
					default:
						fileNameList.Add(Path.GetFileName(path));					//拡張子あり
						break;
				}
			}

			return fileNameList.ToArray();
		}

		/// <summary>
		/// ファイル名リストを取得する（プラグイン専用）
		/// </summary>
		/// <param name="pattern">検索パターン</param>
		/// <param name="valueType">プラグインを表す値</param>
		/// <returns>取得したファイル名リスト</returns>
		private static string[] getFileNameListPlugin(string pattern, string valueType)
		{
			//検索ディレクトリを取得する
			string exeDirPath = GlobalStatus.Project.ExeFullPath;		//実行フォルダと同じパス
			if (String.IsNullOrEmpty(exeDirPath))
			{
				return null;
			}
			exeDirPath = Path.GetDirectoryName(exeDirPath);
			string pluginDirPath = Path.Combine(exeDirPath, "plugin");	//プラグインフォルダ
			string[] dirList = { exeDirPath, pluginDirPath };

			//パスを取得する
			List<string> fileNameList = new List<string>();
			foreach (string dirPath in dirList)
			{
				string[] pathList = util.FileUtil.GetDirectoryFile(dirPath, pattern, SearchOption.TopDirectoryOnly);
				foreach (string path in pathList)
				{
					//ファイル名に変換する
					fileNameList.Add(Path.GetFileName(path));	//プラグインは必ず拡張子あり
				}
			}

			return fileNameList.ToArray();
		}

		/// <summary>
		/// ラベル名リストを取得する
		/// </summary>
		/// <returns>ラベル名リスト</returns>
		private static string[] getLabelList(KagTagKindInfo compInfo)
		{
			string fileName = "";
			if (compInfo.AttrTable.ContainsKey(TAG_ATTR_FILENAME)
			&& compInfo.AttrTable[TAG_ATTR_FILENAME] != "")
			{
				//ファイル名がすでに指定され散るときはそのファイルを探す
				fileName = compInfo.AttrTable[TAG_ATTR_FILENAME];
			}
			else
			{
				//セットされていないときは現在のファイルのラベルを表示する
				fileName = Path.GetFileName(GlobalStatus.EditorManager.ActiveEditor.FileName);	//現在開いているファイル名
			}

			List<string> list = new List<string>();
			foreach (KagLabelItem item in GlobalStatus.ParserSrv.GetLabelListAll())
			{
				if (Path.GetFileName(item.FilePath) == fileName)	//指定したファイルのみ登録する
				{
					list.Add(item.LabelName);
				}
			}

			return list.ToArray();
		}

		/// <summary>
		/// マクロ名リストを取得する
		/// </summary>
		/// <returns>マクロ名リスト</returns>
		private static string[] getMacroNameList()
		{
			List<string> list = new List<string>();
			foreach (KagMacro macro in GlobalStatus.ParserSrv.GetKagMacroList())
			{
				list.Add(macro.Name);
			}

			return list.ToArray();
		}

		/// <summary>
		/// トランジションメソッド名リストを取得する
		/// （暫定対応）
		/// </summary>
		/// <returns>トランジションメソッド名リスト</returns>
		private static string[] getTransMthodList()
		{
			List<string> list = new List<string>();

			//デフォルト
			list.Add("crossfade");
			list.Add("universal");
			list.Add("scroll");

			//extrans.dll使用時
			list.Add("wave");
			list.Add("mosaic");
			list.Add("turn");
			list.Add("rotatezoom");
			list.Add("rotatevanish");
			list.Add("rotateswap");
			list.Add("ripple");

			return list.ToArray();
		}

		/// <summary>
		/// KAGEXワールド拡張のキャラクタ名リストを取得する
		/// </summary>
		/// <param name="compInfo"></param>
		/// <returns></returns>
		private static string[] getKagexCharNameList(KagTagKindInfo compInfo)
		{
			kkde.parse.kagex.KagexCompletionUnit cu = GlobalStatus.ParserSrv.GetKagexEnvinitInfo();
			if (cu == null)
			{
				return null;
			}

			List<string> list = cu.GetCharNameList();
			if (list == null)
			{
				return null;
			}

			return list.ToArray();
		}

		/// <summary>
		/// KAGEXワールド拡張のキャラクター姿勢名リストを取得する
		/// </summary>
		/// <param name="compInfo"></param>
		/// <returns></returns>
		private static string[] getKagexCharPoseList(KagTagKindInfo compInfo)
		{
			string name = getKagexCharNameFromAttrTable(compInfo);
			if (name == "")
			{
				return null;	//キャラクター名が指定されていない
			}

			kkde.parse.kagex.KagexCompletionUnit cu = GlobalStatus.ParserSrv.GetKagexEnvinitInfo();
			if (cu == null)
			{
				return null;
			}
			List<string> list = cu.GetCharPoseList(name);
			if (list == null)
			{
				return null;
			}

			return list.ToArray();
		}

		/// <summary>
		/// すでにかかれている属性リストからキャラクター名を取得する
		/// </summary>
		/// <param name="compInfo">すでにかかれ散る属性リスト</param>
		/// <returns>キャラクター名 見つからなかったときはから文字を返す</returns>
		private static string getKagexCharNameFromAttrTable(KagTagKindInfo compInfo)
		{
			string name;
			if (compInfo.AttrTable.ContainsKey("initname"))
			{
				name = compInfo.AttrTable["initname"];
			}
			else if (compInfo.AttrTable.ContainsKey("name"))
			{
				name = compInfo.AttrTable["name"];
			}
			else
			{
				name = "";
			}

			return name;
		}

		/// <summary>
		/// すでにかかれている属性リストからキャラクターの姿勢を取得する
		/// </summary>
		/// <param name="compInfo"></param>
		/// <param name="cu"></param>
		/// <returns></returns>
		private static string getKagexCharPoseFromAttrTable(KagTagKindInfo compInfo, kkde.parse.kagex.KagexCompletionUnit cu)
		{
			string pose;
			if (compInfo.AttrTable.ContainsKey("pose"))
			{
				pose = compInfo.AttrTable["pose"];
			}
			else
			{
				pose = "";
			}

			return pose;
		}

		/// <summary>
		/// ワールド拡張の表情リストを取得する
		/// </summary>
		/// <param name="compInfo"></param>
		/// <returns></returns>
		private static string[] getKagexCharFaceList(KagTagKindInfo compInfo)
		{
			string name = getKagexCharNameFromAttrTable(compInfo);
			if (name == "")
			{
				return null;	//キャラクター名が指定されていない
			}
			kkde.parse.kagex.KagexCompletionUnit cu = GlobalStatus.ParserSrv.GetKagexEnvinitInfo();
			if (cu == null)
			{
				return null;	//ワールド拡張が読み込めていない
			}
			string pose = getKagexCharPoseFromAttrTable(compInfo, cu);
			if (pose == "")
			{
				return null;	//キャラクター名が指定されていない
			}

			List<string> list = cu.GetCharFaceList(name, pose);
			if (list == null)
			{
				return null;
			}

			return list.ToArray();
		}

		/// <summary>
		/// ワールド拡張の服装リストを取得する
		/// </summary>
		/// <param name="compInfo"></param>
		/// <returns></returns>
		private static string[] getKagexCharDressList(KagTagKindInfo compInfo)
		{
			string name = getKagexCharNameFromAttrTable(compInfo);
			if (name == "")
			{
				return null;	//キャラクター名が指定されていない
			}
			kkde.parse.kagex.KagexCompletionUnit cu = GlobalStatus.ParserSrv.GetKagexEnvinitInfo();
			if (cu == null)
			{
				return null;	//ワールド拡張が読み込めていない
			}
			string pose = getKagexCharPoseFromAttrTable(compInfo, cu);
			if (pose == "")
			{
				return null;	//キャラクター名が指定されていない
			}

			List<string> list = cu.GetCharDressList(name, pose);
			if (list == null)
			{
				return null;
			}

			return list.ToArray();
		}
		
		/// <summary>
		/// ワールド拡張の舞台時間リストを取得する
		/// </summary>
		/// <returns></returns>
		private static string[] getKagexTimeList()
		{
			kkde.parse.kagex.KagexCompletionUnit cu = GlobalStatus.ParserSrv.GetKagexEnvinitInfo();
			if (cu == null)
			{
				return null;	//ワールド拡張が読み込めていない
			}

			List<string> list = cu.GetTimeList();
			if (list == null)
			{
				return null;
			}

			return list.ToArray();
		}

		/// <summary>
		/// ワールド拡張の舞台リストを取得する
		/// </summary>
		/// <returns></returns>
		private static string[] getKagexStageList()
		{
			kkde.parse.kagex.KagexCompletionUnit cu = GlobalStatus.ParserSrv.GetKagexEnvinitInfo();
			if (cu == null)
			{
				return null;	//ワールド拡張が読み込めていない
			}

			List<string> list = cu.GetStageList();
			if (list == null)
			{
				return null;
			}

			return list.ToArray();
		}

		/// <summary>
		/// ワールド拡張の位置リストを取得する
		/// </summary>
		/// <returns></returns>
		private static string[] getKagexPosList()
		{
			kkde.parse.kagex.KagexCompletionUnit cu = GlobalStatus.ParserSrv.GetKagexEnvinitInfo();
			if (cu == null)
			{
				return null;	//ワールド拡張が読み込めていない
			}

			List<string> list = cu.GetPosList();
			if (list == null)
			{
				return null;
			}

			return list.ToArray();
		}

		/// <summary>
		/// ワールド拡張のレベルリストを取得する
		/// </summary>
		/// <returns></returns>
		private static string[] getKagexLevelList()
		{
			kkde.parse.kagex.KagexCompletionUnit cu = GlobalStatus.ParserSrv.GetKagexEnvinitInfo();
			if (cu == null)
			{
				return null;	//ワールド拡張が読み込めていない
			}

			List<string> list = cu.GetLevelList();
			if (list == null)
			{
				return null;
			}

			return list.ToArray();
		}

		/// <summary>
		/// ワールド拡張のアクションリストを取得する
		/// </summary>
		/// <returns></returns>
		private static string[] getKagexActionList()
		{
			kkde.parse.kagex.KagexCompletionUnit cu = GlobalStatus.ParserSrv.GetKagexEnvinitInfo();
			if (cu == null)
			{
				return null;	//ワールド拡張が読み込めていない
			}

			List<string> list = cu.GetActionList();
			if (list == null)
			{
				return null;
			}

			return list.ToArray();
		}

		/// <summary>
		/// ワールド拡張のトランジションリストを取得する
		/// </summary>
		/// <returns></returns>
		private static string[] getKagexTransList()
		{
			kkde.parse.kagex.KagexCompletionUnit cu = GlobalStatus.ParserSrv.GetKagexEnvinitInfo();
			if (cu == null)
			{
				return null;	//ワールド拡張が読み込めていない
			}

			List<string> list = cu.GetTransList();
			if (list == null)
			{
				return null;
			}

			return list.ToArray();
		}
	}
}
