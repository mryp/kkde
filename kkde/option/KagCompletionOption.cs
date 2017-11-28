using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.option
{
	/// <summary>
	/// KAG入力補完関連オプション
	/// </summary>
	public class KagCompletionOption
	{
		/// <summary>
		/// タグの種類
		/// </summary>
		public enum TagType
		{
			/// <summary>
			/// @
			/// </summary>
			Atmark,
			/// <summary>
			/// []
			/// </summary>
			Bracket,
		}

		/// <summary>
		/// ワールド拡張ビューワーの項目ダブルクリック自動さ
		/// </summary>
		public enum WorldExViewDCOption
		{
			/// <summary>
			/// プレビュー画面でプレビュー
			/// </summary>
			Preview,

			/// <summary>
			/// 吉里吉里でプレビュー
			/// </summary>
			PreviewEx,
		}

		#region フィールド
		//全体
		bool m_useAttrValueDqRegion = true;

		//ファイル名拡張子
		string m_scenarioFileExt = "*.ks";
		string m_imageFileExt = "*.bmp;*.jpg;*.jpeg;*.jpe;*.png;*.eri;*.tlg";
		string m_seFileExt = "*.wav;*.ogg;*.tc";
		string m_cursorFileExt = "*.cur;*.ani";
		string m_bgmFileExt = "*.wav;*.ogg;*.tcw;*.smf;*.mid";
		string m_actionFileExt = "*.ma";
		string m_pluginFileExt = "*.dll";
		string m_fontFileExt = "*.tft";
		string m_videoFileExt = "*.avi;*.mpg;*.mpeg;*.mpv;*.swf";

		//数値
		string m_zeroOverNumberList = "0;1;2;3;4;5;6;7;8;9;10;20;30;40;50;60;70;80;90;100;200;500;1000";
		string m_oneOverNumberList = "1;2;3;4;5;6;7;8;9;10;20;30;40;50;60;70;80;90;100;200;500;1000";
		string m_percentNumberList = "0;10;20;30;40;50;60;70;80;90;100";
		string m_byteNumberList = "0;16;32;48;64;80;96;112;128;144;160;176;192;208;224;240;255";
		string m_msTimeNumberList = "0;100;200;300;400;500;600;700;800;900;1000;1200;1500;2000;3000;5000";
		string m_realNumberList = "-10.0;-5.0;-2.0;-1.0;-0.5;-0.4;-0.3;-0.2;-0.1;-0.8;0;0.1;0.2;0.3;0.4;0.5;0.8;1.0;2.0;5.0;10.0";
		string m_pmHundredNumberList = "-100;-90;-80;-70;-60;-50;-40;-30;-20;-10;0;10;20;30;40;50;60;70;80;90;100";
		string m_rgbNumberList = "0x000000;0xFFFFFF";
		string m_argbNumberList = "0x00000000;0x00FFFFFF;0xFF000000;0xFFFFFFFF";
		string m_pmbyteNumberList = "0;16;32;48;64;80;96;112;128;144;160;176;192;208;224;240;255;-16;-32;-48;-64;-80;-96;-112;-128;-144;-160;-176;-192;-208;-224;-240;-255";
		string m_hueNumberList = "0;30;60;90;120;150;180;-30;-60;-90;-120;-150;-180";

		//任意文字列
		string m_otherStringList = "";
		string m_tjsStringList = "";
		string m_fontStringList = "ＭＳ ゴシック;ＭＳ 明朝;ＭＳ Ｐゴシック;ＭＳ Ｐ明朝";

		//最大値指定
		int m_layerMaxNumber = 3;
		int m_messageLayerMaxNumber = 2;
		int m_seBufferMaxNumber = 3;
		int m_videoBufferMaxNumber = 3;

		//定数
		string m_baseLayerList = "base";
		string m_boolValueList = "true;false";
		string m_layerPageList = "fore;back";
		string m_layerPosList = "left;left_center;center;right_center;right";
		string m_cursorDefList = "crDefault;crNone;crArrow;crCross;crIBeam;crHBeam;crSizeNESW;crSizeNS;crSizeNWSE;crSizeWE;crUpArrow;crHourGlass;crDrag;crNoDrop;crHSplit;crVSplit;crMultiDrag;crSQLWait;crNo;crAppStart;crHelp;crHandPoint;crSizeAll";
		string m_colorcompModeList = "ltOpaque;ltAlpha;ltAddAlpha;ltAdditive;ltSubtractive;ltMultiplicative;ltDodge;ltLighten;ltDarken;ltScreen;ltPsNormal;ltPsAdditive;ltPsSubtractive;ltPsMultiplicative;ltPsScreen;ltPsOverlay;ltPsHardLight;ltPsSoftLight;ltPsColorDodge;ltPsColorDodge5;ltPsColorBurn;ltPsLighten;ltPsDarken;ltPsDifference;ltPsDifference5;ltPsExclusion";
		string m_kagexAction = "MoveAction;RandomAction;SquareAction;TriangleAction;SinAction;CosAction;FallAction;LoopMoveAction";
		string m_kagexLtbType = "wipeltor;wipertol;fade;trans";

		//プラグインの有無により変化するもの
//		string m_transMethodList = "universal;scroll;crossfade";	//extrans.dllの有無により変化

		//シナリオの状態により変化するもの
//		string m_labelList = "";		//シナリオファイルのラベル名
//		string m_asdLabelList = "";		//ASDファイルのラベル名
//		string m_macroNameList = "";	//マクロ名

		bool m_isInsertedTagCopyExWorldEx = false;
		bool m_isAddedTagSignWorldEx = true;
		bool m_isAddedMsgTagWorldEx = false;
		string m_worldExSearchPathChar = "fgimage";
		string m_worldExSearchPathEvent = "image";
		string m_worldExSearchPathStage = "bgimage";
		string m_worldExSearchPathBgm = "bgm";
		string m_worldExSearchPathSe = "sound";
		TagType m_worldExCopyTagType = TagType.Atmark;
		WorldExViewDCOption m_worldExDoubleDef = WorldExViewDCOption.Preview;
		#endregion

		#region プロパティ
		/// <summary>
		/// 属性値入力時に"で囲むかどうか
		/// </summary>
		public bool UseAttrValueDqRegion
		{
			get { return m_useAttrValueDqRegion; }
			set { m_useAttrValueDqRegion = value; }
		}

		/// <summary>
		/// シナリオファイルの拡張子リスト
		/// </summary>
		public string ScenarioFileExt
		{
			get { return m_scenarioFileExt; }
			set { m_scenarioFileExt = value; }
		}

		/// <summary>
		/// 画像ファイルの拡張子リスト
		/// </summary>
		public string ImageFileExt
		{
			get { return m_imageFileExt; }
			set { m_imageFileExt = value; }
		}

		/// <summary>
		/// 効果音ファイルの拡張子リスト
		/// </summary>
		public string SeFileExt
		{
			get { return m_seFileExt; }
			set { m_seFileExt = value; }
		}

		/// <summary>
		/// カーソルファイルの拡張子リスト
		/// </summary>
		public string CursorFileExt
		{
			get { return m_cursorFileExt; }
			set { m_cursorFileExt = value; }
		}

		/// <summary>
		/// BGMファイルの拡張子リスト
		/// </summary>
		public string BgmFileExt
		{
			get { return m_bgmFileExt; }
			set { m_bgmFileExt = value; }
		}

		/// <summary>
		/// 領域アクション定義ファイルの拡張子リスト
		/// </summary>
		public string ActionFileExt
		{
			get { return m_actionFileExt; }
			set { m_actionFileExt = value; }
		}

		/// <summary>
		/// プラグインファイルの拡張子リスト
		/// </summary>
		public string PluginFileExt
		{
			get { return m_pluginFileExt; }
			set { m_pluginFileExt = value; }
		}

		/// <summary>
		/// レンダリング済みフォントファイルの拡張子リスト
		/// 
		/// </summary>
		public string FontFileExt
		{
			get { return m_fontFileExt; }
			set { m_fontFileExt = value; }
		}

		/// <summary>
		/// ムービーファイルの拡張子リスト
		/// </summary>
		public string VideoFileExt
		{
			get { return m_videoFileExt; }
			set { m_videoFileExt = value; }
		}

		/// <summary>
		/// 0以上の数値リスト（セミコロン区切り）
		/// </summary>
		public string ZeroOverNumberList
		{
			get { return m_zeroOverNumberList; }
			set { m_zeroOverNumberList = value; }
		}

		/// <summary>
		/// 1以上の数値リスト（セミコロン区切り）
		/// </summary>
		public string OneOverNumberList
		{
			get { return m_oneOverNumberList; }
			set { m_oneOverNumberList = value; }
		}

		/// <summary>
		/// 0～100の数値リスト（セミコロン区切り）
		/// </summary>
		public string PercentNumberList
		{
			get { return m_percentNumberList; }
			set { m_percentNumberList = value; }
		}

		/// <summary>
		/// 0～255の数値リスト（セミコロン区切り）
		/// </summary>
		public string ByteNumberList
		{
			get { return m_byteNumberList; }
			set { m_byteNumberList = value; }
		}

		/// <summary>
		/// ミリ秒時間の数値リスト（セミコロン区切り）
		/// </summary>
		public string MsTimeNumberList
		{
			get { return m_msTimeNumberList; }
			set { m_msTimeNumberList = value; }
		}

		/// <summary>
		/// 実数の数値リスト（セミコロン区切り）
		/// </summary>
		public string RealNumberList
		{
			get { return m_realNumberList; }
			set { m_realNumberList = value; }
		}

		/// <summary>
		/// -100～100の数値リスト（セミコロン区切り）
		/// </summary>
		public string PmHundredNumberList
		{
			get { return m_pmHundredNumberList; }
			set { m_pmHundredNumberList = value; }
		}

		/// <summary>
		/// 0xRRGGBB形式の数値（セミコロン区切り）
		/// </summary>
		public string RgbNumberList
		{
			get { return m_rgbNumberList; }
			set { m_rgbNumberList = value; }
		}

		/// <summary>
		/// 0xAARRGGBB形式の数値（セミコロン区切り）
		/// </summary>
		public string ArgbNumberList
		{
			get { return m_argbNumberList; }
			set { m_argbNumberList = value; }
		}

		/// <summary>
		/// -255～255の数値リスト
		/// </summary>
		public string PmbyteNumberList
		{
			get { return m_pmbyteNumberList; }
			set { m_pmbyteNumberList = value; }
		}

		/// <summary>
		/// -180～180の数値リスト
		/// </summary>
		public string HueNumberList
		{
			get { return m_hueNumberList; }
			set { m_hueNumberList = value; }
		}

		/// <summary>
		/// 任意文字列リスト
		/// </summary>
		public string OtherStringList
		{
			get { return m_otherStringList; }
			set { m_otherStringList = value; }
		}

		/// <summary>
		/// TJS式文字列リスト
		/// </summary>
		public string TjsStringList
		{
			get { return m_tjsStringList; }
			set { m_tjsStringList = value; }
		}

		/// <summary>
		/// フォント名リスト
		/// </summary>
		public string FontStringList
		{
			get { return m_fontStringList; }
			set { m_fontStringList = value; }
		}

		/// <summary>
		/// 前景レイヤ数の最大値
		/// </summary>
		public int LayerMaxNumber
		{
			get { return m_layerMaxNumber; }
			set { m_layerMaxNumber = value; }
		}

		/// <summary>
		/// メッセージレイヤ数の最大値
		/// </summary>
		public int MessageLayerMaxNumber
		{
			get { return m_messageLayerMaxNumber; }
			set { m_messageLayerMaxNumber = value; }
		}

		/// <summary>
		/// 効果音バッファ数の最大値
		/// </summary>
		public int SeBufferMaxNumber
		{
			get { return m_seBufferMaxNumber; }
			set { m_seBufferMaxNumber = value; }
		}

		/// <summary>
		/// ムービーオブジェクト番号の最大値
		/// </summary>
		public int VideoBufferMaxNumber
		{
			get { return m_videoBufferMaxNumber; }
			set { m_videoBufferMaxNumber = value; }
		}

		/// <summary>
		/// 背景レイヤリスト
		/// </summary>
		public string BaseLayerList
		{
			get { return m_baseLayerList; }
			set { m_baseLayerList = value; }
		}

		/// <summary>
		/// 論理値リスト
		/// </summary>
		public string BoolValueList
		{
			get { return m_boolValueList; }
			set { m_boolValueList = value; }
		}
		
		/// <summary>
		/// レイヤーページリスト
		/// </summary>
		public string LayerPageList
		{
			get { return m_layerPageList; }
			set { m_layerPageList = value; }
		}

		/// <summary>
		/// レイヤー位置リスト
		/// </summary>
		public string LayerPosList
		{
			get { return m_layerPosList; }
			set { m_layerPosList = value; }
		}

		/// <summary>
		/// カーソル定数リスト
		/// </summary>
		public string CursorDefList
		{
			get { return m_cursorDefList; }
			set { m_cursorDefList = value; }
		}

		/// <summary>
		/// 合成モード定数リスト
		/// </summary>
		public string ColorcompModeList
		{
			get { return m_colorcompModeList; }
			set { m_colorcompModeList = value; }
		}

		/// <summary>
		/// KAGEXアクションモジュールリスト
		/// </summary>
		public string KagexAction
		{
			get { return m_kagexAction; }
			set { m_kagexAction = value; }
		}

		/// <summary>
		/// KAGEX ltb～タグのレイヤー遷移タイプリスト
		/// </summary>
		public string KagexLtbType
		{
			get { return m_kagexLtbType; }
			set { m_kagexLtbType = value; }
		}

		/// <summary>
		/// 拡張タグコピーの時挿入動作を行うかどうか
		/// </summary>
		public bool IsInsertedTagCopyExWorldEx
		{
			get { return m_isInsertedTagCopyExWorldEx; }
			set { m_isInsertedTagCopyExWorldEx = value; }
		}

		/// <summary>
		/// ワールド拡張タグコピー時にタグ記号を付けるかどうか
		/// </summary>
		public bool IsAddedTagSignWorldEx
		{
			get { return m_isAddedTagSignWorldEx; }
			set { m_isAddedTagSignWorldEx = value; }
		}

		/// <summary>
		/// ワールド拡張タグコピー時にmsgoff-msgonタグで囲むかどうか
		/// </summary>
		public bool IsAddedMsgTagWorldEx
		{
			get { return m_isAddedMsgTagWorldEx; }
			set { m_isAddedMsgTagWorldEx = value; }
		}

		/// <summary>
		/// ワールド拡張ビューワー立ち絵として検索するフォルダ
		/// </summary>
		public string WorldExSearchPathChar
		{
			get { return m_worldExSearchPathChar; }
			set { m_worldExSearchPathChar = value; }
		}

		/// <summary>
		/// ワールド拡張ビューワーでイベント絵として検索するフォルダ
		/// </summary>
		public string WorldExSearchPathEvent
		{
			get { return m_worldExSearchPathEvent; }
			set { m_worldExSearchPathEvent = value; }
		}

		/// <summary>
		/// ワールド拡張ビューワーで背景絵として検索するフォルダ
		/// </summary>
		public string WorldExSearchPathStage
		{
			get { return m_worldExSearchPathStage; }
			set { m_worldExSearchPathStage = value; }
		}

		/// <summary>
		/// ワールド拡張ビューワーでBGMとして検索するフォルダ
		/// </summary>
		public string WorldExSearchPathBgm
		{
			get { return m_worldExSearchPathBgm; }
			set { m_worldExSearchPathBgm = value; }
		}

		/// <summary>
		/// ワールド拡張ビューワーで効果音として検索するフォルダ
		/// </summary>
		public string WorldExSearchPathSe
		{
			get { return m_worldExSearchPathSe; }
			set { m_worldExSearchPathSe = value; }
		}

		/// <summary>
		/// ワールド拡張タグコピーでタグ記号を付けるときのタグ記号の種類
		/// </summary>
		public TagType WorldExCopyTagType
		{
			get { return m_worldExCopyTagType; }
			set { m_worldExCopyTagType = value; }
		}

		/// <summary>
		/// ワールド拡張ビューワーでアイテムのダブルクリック時の動作設定
		/// </summary>
		public WorldExViewDCOption WorldExDoubleDef
		{
			get { return m_worldExDoubleDef; }
			set { m_worldExDoubleDef = value; }
		}
		#endregion
	}
}
