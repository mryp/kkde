using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;

namespace TagsConverter
{
	/// <summary>
	/// kagtags.xmlファイル処理クラス
	/// </summary>
	public class KagTagsFile
	{
		#region 属性値テーブル
		/// <summary>
		/// 
		/// </summary>
		private const string NOT_FOUND_FOMAT = "■属性値なし";

		/// <summary>
		/// 属性値フォーマットテーブル
		/// </summary>
		public class AttrValueFormatTable
		{
			string m_tagName;
			string m_attrName;
			string m_valueFormat;

			/// <summary>
			/// タグ名
			/// </summary>
			public string TagName
			{
				get { return m_tagName; }
				set { m_tagName = value; }
			}
			
			/// <summary>
			/// 属性名
			/// </summary>
			public string AttrName
			{
				get { return m_attrName; }
				set { m_attrName = value; }
			}
			
			/// <summary>
			/// 属性値フォーマット
			/// </summary>
			public string ValueFormat
			{
				get { return m_valueFormat; }
				set { m_valueFormat = value; }
			}

			/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="tagName"></param>
			/// <param name="attrName"></param>
			/// <param name="valueFormat"></param>
			public AttrValueFormatTable(string tagName, string attrName, string valueFormat)
			{
				m_tagName = tagName;
				m_attrName = attrName;
				m_valueFormat = valueFormat;
			}
		}

		/// <summary>
		/// cond属性の属性値テーブル
		/// </summary>
		private readonly AttrValueFormatTable ATTR_VALUE_FOMAT_COND = new AttrValueFormatTable("", "cond", "TJS式");

		/// <summary>
		/// 属性値テーブル
		/// </summary>
		private readonly AttrValueFormatTable[] ATTR_VALUE_FOMAT_TABLE = new AttrValueFormatTable[] {			
			new AttrValueFormatTable("animstart", "layer", "背景レイヤ;前景レイヤ"),
			new AttrValueFormatTable("animstart", "page", "レイヤーページ"),
			new AttrValueFormatTable("animstart", "seg", "1以上の値"),
			new AttrValueFormatTable("animstart", "target", "ASDラベル名"),
			new AttrValueFormatTable("animstop", "layer", "背景レイヤ;前景レイヤ"),
			new AttrValueFormatTable("animstop", "page", "レイヤーページ"),
			new AttrValueFormatTable("animstop", "seg", "1以上の値"),
			new AttrValueFormatTable("autowc", "enabled", "論理値"),
			new AttrValueFormatTable("autowc", "ch", "任意文字列"),
			new AttrValueFormatTable("autowc", "time", "1以上の値"),
			new AttrValueFormatTable("backlay", "layer", "背景レイヤ;前景レイヤ;メッセージレイヤ"),
			new AttrValueFormatTable("bgmopt", "volume", "パーセント値"),
			new AttrValueFormatTable("bgmopt", "gvolume", "パーセント値"),
			new AttrValueFormatTable("button", "graphic", "画像ファイル名"),
			new AttrValueFormatTable("button", "graphickey", "RGB値;256値;adapt"),
			new AttrValueFormatTable("button", "storage", "シナリオファイル名"),
			new AttrValueFormatTable("button", "target", "ラベル名"),
			new AttrValueFormatTable("button", "recthit", "論理値"),
			new AttrValueFormatTable("button", "exp", "TJS式"),
			new AttrValueFormatTable("button", "hint", "任意文字列"),
			new AttrValueFormatTable("button", "onenter", "TJS式"),
			new AttrValueFormatTable("button", "onleave", "TJS式"),
			new AttrValueFormatTable("button", "countpage", "論理値"),
			new AttrValueFormatTable("button", "clickse", "効果音ファイル名"),
			new AttrValueFormatTable("button", "clicksebuf", "効果音バッファ番号"),
			new AttrValueFormatTable("button", "enterse", "効果音ファイル名"),
			new AttrValueFormatTable("button", "entersebuf", "効果音バッファ番号"),
			new AttrValueFormatTable("button", "leavese", "効果音ファイル名"),
			new AttrValueFormatTable("button", "leavesebuf", "効果音バッファ番号"),
			new AttrValueFormatTable("call", "storage", "シナリオファイル名"),
			new AttrValueFormatTable("call", "target", "ラベル名"),
			new AttrValueFormatTable("call", "countpage", "論理値"),
			new AttrValueFormatTable("cancelvideoevent", "slot", "ムービーオブジェクト番号"),
			new AttrValueFormatTable("cancelvideosegloop", "slot", "ムービーオブジェクト番号"),
			new AttrValueFormatTable("ch", "text", "任意文字列"),
			new AttrValueFormatTable("checkbox", "name", "任意文字列"),
			new AttrValueFormatTable("checkbox", "bgcolor", "RGB値"),
			new AttrValueFormatTable("checkbox", "opacity", "256値"),
			new AttrValueFormatTable("checkbox", "color", "RGB値"),
			new AttrValueFormatTable("clearvideolayer", "slot", "ムービーオブジェクト番号"),
			new AttrValueFormatTable("clearvideolayer", "channel", "1;2"),
			new AttrValueFormatTable("click", "storage", "シナリオファイル名"),
			new AttrValueFormatTable("click", "target", "ラベル名"),
			new AttrValueFormatTable("click", "exp", "TJS式"),
			new AttrValueFormatTable("click", "se", "効果音ファイル名"),
			new AttrValueFormatTable("click", "sebuf", "効果音バッファ番号"),
			new AttrValueFormatTable("clickskip", "enabled", "論理値"),
			new AttrValueFormatTable("close", "ask", "論理値"),
			new AttrValueFormatTable("copybookmark", "from", "0以上の値"),
			new AttrValueFormatTable("copybookmark", "to", "0以上の値"),
			new AttrValueFormatTable("copylay", "srclayer", "背景レイヤ;前景レイヤ;メッセージレイヤ"),
			new AttrValueFormatTable("copylay", "destlayer", "背景レイヤ;前景レイヤ;メッセージレイヤ"),
			new AttrValueFormatTable("copylay", "srcpage", "レイヤーページ"),
			new AttrValueFormatTable("copylay", "destpage", "レイヤーページ"),
			new AttrValueFormatTable("current", "layer", "メッセージレイヤ"),
			new AttrValueFormatTable("current", "page", "レイヤーページ"),
			new AttrValueFormatTable("current", "withback", "論理値"),
			new AttrValueFormatTable("cursor", "default", "カーソル定数;カーソルファイル名"),
			new AttrValueFormatTable("cursor", "pointed", "カーソル定数;カーソルファイル名"),
			new AttrValueFormatTable("cursor", "click", "カーソル定数;カーソルファイル名"),
			new AttrValueFormatTable("cursor", "draggable", "カーソル定数;カーソルファイル名"),
			new AttrValueFormatTable("deffont", "size", "1以上の値"),
			new AttrValueFormatTable("deffont", "face", "フォント名;user"),
			new AttrValueFormatTable("deffont", "color", "RGB値"),
			new AttrValueFormatTable("deffont", "rubysize", "1以上の値"),
			new AttrValueFormatTable("deffont", "rubyoffset", "1以上の値"),
			new AttrValueFormatTable("deffont", "shadow", "論理値"),
			new AttrValueFormatTable("deffont", "edge", "論理値"),
			new AttrValueFormatTable("deffont", "edgecolor", "RGB値"),
			new AttrValueFormatTable("deffont", "shadowcolor", "RGB値"),
			new AttrValueFormatTable("deffont", "bold", "論理値"),
			new AttrValueFormatTable("defstyle", "linespacing", "1以上の値"),
			new AttrValueFormatTable("defstyle", "pitch", "1以上の値"),
			new AttrValueFormatTable("defstyle", "linesize", "1以上の値"),
			new AttrValueFormatTable("delay", "speed", "nowait;user;ミリ秒時間"),
			new AttrValueFormatTable("disablestore", "store", "論理値"),
			new AttrValueFormatTable("disablestore", "restore", "論理値"),
			new AttrValueFormatTable("edit", "name", "任意文字列"),
			new AttrValueFormatTable("edit", "length", "1以上の値"),
			new AttrValueFormatTable("edit", "bgcolor", "RGB値"),
			new AttrValueFormatTable("edit", "opacity", "256値"),
			new AttrValueFormatTable("edit", "color", "RGB値"),
			new AttrValueFormatTable("edit", "maxchars", "0以上の値"),
			new AttrValueFormatTable("elsif", "exp", "TJS式"),
			new AttrValueFormatTable("emb", "exp", "TJS式"),
			new AttrValueFormatTable("erasebookmark", "place", "0以上の値"),
			new AttrValueFormatTable("erasemacro", "name", "マクロ名"),
			new AttrValueFormatTable("eval", "exp", "TJS式"),
			new AttrValueFormatTable("fadebgm", "volume", "パーセント値"),
			new AttrValueFormatTable("fadebgm", "time", "ミリ秒時間"),
			new AttrValueFormatTable("fadeinbgm", "storage", "BGMファイル名;1以上の値"),
			new AttrValueFormatTable("fadeinbgm", "start", "0以上の値"),
			new AttrValueFormatTable("fadeinbgm", "loop", "論理値"),
			new AttrValueFormatTable("fadeinbgm", "time", "ミリ秒時間"),
			new AttrValueFormatTable("fadeinse", "buf", "効果音バッファ番号"),
			new AttrValueFormatTable("fadeinse", "storage", "効果音ファイル名"),
			new AttrValueFormatTable("fadeinse", "start", "0以上の値"),
			new AttrValueFormatTable("fadeinse", "time", "ミリ秒時間"),
			new AttrValueFormatTable("fadeinse", "loop", "論理値"),
			new AttrValueFormatTable("fadeoutbgm", "time", "ミリ秒時間"),
			new AttrValueFormatTable("fadeoutse", "buf", "効果音バッファ番号"),
			new AttrValueFormatTable("fadeoutse", "time", "ミリ秒時間"),
			new AttrValueFormatTable("fadepausebgm", "time", "ミリ秒時間"),
			new AttrValueFormatTable("fadese", "buf", "効果音バッファ番号"),
			new AttrValueFormatTable("fadese", "time", "ミリ秒時間"),
			new AttrValueFormatTable("fadese", "volume", "パーセント値"),
			new AttrValueFormatTable("font", "size", "1以上の値;default"),
			new AttrValueFormatTable("font", "face", "フォント名;default;user"),
			new AttrValueFormatTable("font", "color", "RGB値;default"),
			new AttrValueFormatTable("font", "italic", "論理値;default"),
			new AttrValueFormatTable("font", "rubysize", "1以上の値;default"),
			new AttrValueFormatTable("font", "rubyoffset", "1以上の値;default"),
			new AttrValueFormatTable("font", "shadow", "論理値;default"),
			new AttrValueFormatTable("font", "edge", "論理値;default"),
			new AttrValueFormatTable("font", "edgecolor", "RGB値;default"),
			new AttrValueFormatTable("font", "shadowcolor", "RGB値;default"),
			new AttrValueFormatTable("font", "bold", "論理値;default"),
			new AttrValueFormatTable("freeimage", "layer", "背景レイヤ;前景レイヤ"),
			new AttrValueFormatTable("freeimage", "page", "レイヤーページ"),
			new AttrValueFormatTable("glyph", "line", "画像ファイル名"),
			new AttrValueFormatTable("glyph", "linekey", "RGB値;256値;adapt"),
			new AttrValueFormatTable("glyph", "page", "画像ファイル名"),
			new AttrValueFormatTable("glyph", "pagekey", "RGB値;256値;adapt"),
			new AttrValueFormatTable("glyph", "fix", "論理値"),
			new AttrValueFormatTable("glyph", "left", "0以上の値"),
			new AttrValueFormatTable("glyph", "top", "0以上の値"),
			new AttrValueFormatTable("goback", "ask", "論理値"),
			new AttrValueFormatTable("gotostart", "ask", "論理値"),
			new AttrValueFormatTable("graph", "storage", "画像ファイル名"),
			new AttrValueFormatTable("graph", "key", "RGB値;256値;adapt"),
			new AttrValueFormatTable("graph", "char", "論理値"),
			new AttrValueFormatTable("graph", "alt", "任意文字列"),
			new AttrValueFormatTable("hact", "exp", "TJS式"),
			new AttrValueFormatTable("hch", "text", "任意文字列"),
			new AttrValueFormatTable("hch", "expand", "論理値"),
			new AttrValueFormatTable("history", "output", "論理値"),
			new AttrValueFormatTable("history", "enabled", "論理値"),
			new AttrValueFormatTable("hr", "repage", "論理値"),
			new AttrValueFormatTable("if", "exp", "TJS式"),
			new AttrValueFormatTable("ignore", "exp", "TJS式"),
			new AttrValueFormatTable("image", "storage", "画像ファイル名"),
			new AttrValueFormatTable("image", "layer", "背景レイヤ;前景レイヤ"),
			new AttrValueFormatTable("image", "page", "レイヤーページ"),
			new AttrValueFormatTable("image", "key", "RGB値;256値;adapt"),
			new AttrValueFormatTable("image", "mode", "alpha;transp;opaque;rect;add;sub;mul;dodge;darken;lighten;screen;psadd;pssub;psmul;psscreen;psoverlay;pshlight;psslight;psdodge;psdodge5;psburn;pslighten;psdarken;psdiff;psdiff5;psexcl"),
			new AttrValueFormatTable("image", "grayscale", "論理値"),
			new AttrValueFormatTable("image", "rgamma", "実数値"),
			new AttrValueFormatTable("image", "ggamma", "実数値"),
			new AttrValueFormatTable("image", "bgamma", "実数値"),
			new AttrValueFormatTable("image", "rfloor", "256値"),
			new AttrValueFormatTable("image", "gfloor", "256値"),
			new AttrValueFormatTable("image", "bfloor", "256値"),
			new AttrValueFormatTable("image", "rceil", "256値"),
			new AttrValueFormatTable("image", "gceil", "256値"),
			new AttrValueFormatTable("image", "bceil", "256値"),
			new AttrValueFormatTable("image", "mcolor", "RGB値"),
			new AttrValueFormatTable("image", "mopacity", "256値"),
			new AttrValueFormatTable("image", "clipleft", "0以上の値"),
			new AttrValueFormatTable("image", "cliptop", "0以上の値"),
			new AttrValueFormatTable("image", "clipwidth", "1以上の値"),
			new AttrValueFormatTable("image", "clipheight", "1以上の値"),
			new AttrValueFormatTable("image", "flipud", "論理値"),
			new AttrValueFormatTable("image", "fliplr", "論理値"),
			new AttrValueFormatTable("image", "visible", "論理値"),
			new AttrValueFormatTable("image", "left", "0以上の値"),
			new AttrValueFormatTable("image", "top", "0以上の値"),
			new AttrValueFormatTable("image", "pos", "レイヤ位置"),
			new AttrValueFormatTable("image", "opacity", "256値"),
			new AttrValueFormatTable("image", "mapimage", "画像ファイル名"),
			new AttrValueFormatTable("image", "mapaction", "領域アクション定義ファイル名"),
			new AttrValueFormatTable("image", "index", "1以上の値"),
			new AttrValueFormatTable("input", "name", "任意文字列"),
			new AttrValueFormatTable("input", "prompt", "任意文字列"),
			new AttrValueFormatTable("input", "title", "任意文字列"),
			new AttrValueFormatTable("jump", "storage", "シナリオファイル名"),
			new AttrValueFormatTable("jump", "target", "ラベル名"),
			new AttrValueFormatTable("jump", "countpage", "論理値"),
			new AttrValueFormatTable("laycount", "layers", "0以上の値"),
			new AttrValueFormatTable("laycount", "messages", "1以上の値"),
			new AttrValueFormatTable("layopt", "layer", "前景レイヤ;メッセージレイヤ"),
			new AttrValueFormatTable("layopt", "page", "レイヤーページ"),
			new AttrValueFormatTable("layopt", "visible", "論理値"),
			new AttrValueFormatTable("layopt", "left", "0以上の値"),
			new AttrValueFormatTable("layopt", "top", "0以上の値"),
			new AttrValueFormatTable("layopt", "opacity", "256値"),
			new AttrValueFormatTable("layopt", "autohide", "論理値"),
			new AttrValueFormatTable("layopt", "index", "1以上の値"),
			new AttrValueFormatTable("link", "storage", "シナリオファイル名"),
			new AttrValueFormatTable("link", "target", "ラベル名"),
			new AttrValueFormatTable("link", "exp", "TJS式"),
			new AttrValueFormatTable("link", "color", "RGB値"),
			new AttrValueFormatTable("link", "hint", "任意文字列"),
			new AttrValueFormatTable("link", "onenter", "TJS式"),
			new AttrValueFormatTable("link", "onleave", "TJS式"),
			new AttrValueFormatTable("link", "countpage", "論理値"),
			new AttrValueFormatTable("link", "clickse", "効果音ファイル名"),
			new AttrValueFormatTable("link", "clicksebuf", "効果音バッファ番号"),
			new AttrValueFormatTable("link", "enterse", "効果音ファイル名"),
			new AttrValueFormatTable("link", "entersebuf", "効果音バッファ番号"),
			new AttrValueFormatTable("link", "leavese", "効果音ファイル名"),
			new AttrValueFormatTable("link", "leavesebuf", "効果音バッファ番号"),
			new AttrValueFormatTable("load", "place", "0以上の値"),
			new AttrValueFormatTable("load", "ask", "論理値"),
			new AttrValueFormatTable("loadplugin", "module", "プラグインファイル名"),
			new AttrValueFormatTable("locate", "x", "0以上の値"),
			new AttrValueFormatTable("locate", "y", "0以上の値"),
			new AttrValueFormatTable("macro", "name", "任意文字列"),
			new AttrValueFormatTable("mapaction", "layer", "背景レイヤ;前景レイヤ"),
			new AttrValueFormatTable("mapaction", "page", "レイヤーページ"),
			new AttrValueFormatTable("mapaction", "storage", "領域アクション定義ファイル名"),
			new AttrValueFormatTable("mapdisable", "layer", "背景レイヤ;前景レイヤ"),
			new AttrValueFormatTable("mapdisable", "page", "レイヤーページ"),
			new AttrValueFormatTable("mapimage", "layer", "背景レイヤ;前景レイヤ"),
			new AttrValueFormatTable("mapimage", "page", "レイヤーページ"),
			new AttrValueFormatTable("mapimage", "storage", "画像ファイル名"),
			new AttrValueFormatTable("mappfont", "storage", "フォントファイル名"),
			new AttrValueFormatTable("move", "layer", "前景レイヤ;メッセージレイヤ"),
			new AttrValueFormatTable("move", "page", "レイヤーページ"),
			new AttrValueFormatTable("move", "spline", "論理値"),
			new AttrValueFormatTable("move", "time", "ミリ秒時間"),
			new AttrValueFormatTable("move", "delay", "ミリ秒時間"),
			new AttrValueFormatTable("move", "path", "任意文字列"),
			new AttrValueFormatTable("move", "accel", "-1;0;1"),
			new AttrValueFormatTable("nextskip", "enabled", "論理値"),
			new AttrValueFormatTable("openvideo", "slot", "ムービーオブジェクト番号"),
			new AttrValueFormatTable("openvideo", "storage", "ムービーファイル名"),
			new AttrValueFormatTable("pausevideo", "slot", "ムービーオブジェクト番号"),
			new AttrValueFormatTable("pimage", "storage", "画像ファイル名"),
			new AttrValueFormatTable("pimage", "layer", "背景レイヤ;前景レイヤ"),
			new AttrValueFormatTable("pimage", "page", "レイヤーページ"),
			new AttrValueFormatTable("pimage", "key", "RGB値;256値;adapt"),
			new AttrValueFormatTable("pimage", "dx", "0以上の値"),
			new AttrValueFormatTable("pimage", "dy", "0以上の値"),
			new AttrValueFormatTable("pimage", "sx", "0以上の値"),
			new AttrValueFormatTable("pimage", "sy", "0以上の値"),
			new AttrValueFormatTable("pimage", "sw", "0以上の値"),
			new AttrValueFormatTable("pimage", "sh", "0以上の値"),
			new AttrValueFormatTable("pimage", "mode", "copy;opaque;pile;alpha;add;sub;mul;dodge;darken;lighten;screen;psadd;pssub;psmul;psscreen;psoverlay;pshlight;psslight;psdodge;psdodge5;psburn;pslighten;psdarken;psdiff;psdiff5;psexcl"),
			new AttrValueFormatTable("pimage", "opacity", "256値"),
			new AttrValueFormatTable("playbgm", "storage", "BGMファイル名"),
			new AttrValueFormatTable("playbgm", "start", "0以上の値"),
			new AttrValueFormatTable("playbgm", "loop", "論理値"),
			new AttrValueFormatTable("playse", "buf", "効果音バッファ番号"),
			new AttrValueFormatTable("playse", "storage", "効果音ファイル名"),
			new AttrValueFormatTable("playse", "start", "0以上の値"),
			new AttrValueFormatTable("playse", "loop", "論理値"),
			new AttrValueFormatTable("playvideo", "slot", "ムービーオブジェクト番号"),
			new AttrValueFormatTable("playvideo", "storage", "ムービーファイル名"),
			new AttrValueFormatTable("position", "layer", "メッセージレイヤ"),
			new AttrValueFormatTable("position", "page", "レイヤーページ"),
			new AttrValueFormatTable("position", "left", "0以上の値"),
			new AttrValueFormatTable("position", "top", "0以上の値"),
			new AttrValueFormatTable("position", "width", "0以上の値"),
			new AttrValueFormatTable("position", "height", "0以上の値"),
			new AttrValueFormatTable("position", "frame", "画像ファイル名"),
			new AttrValueFormatTable("position", "framekey", "RGB値;256値;adapt"),
			new AttrValueFormatTable("position", "color", "RGB値"),
			new AttrValueFormatTable("position", "opacity", "256値"),
			new AttrValueFormatTable("position", "marginl", "0以上の値"),
			new AttrValueFormatTable("position", "margint", "0以上の値"),
			new AttrValueFormatTable("position", "marginr", "0以上の値"),
			new AttrValueFormatTable("position", "marginb", "0以上の値"),
			new AttrValueFormatTable("position", "vertical", "論理値"),
			new AttrValueFormatTable("position", "draggable", "論理値"),
			new AttrValueFormatTable("position", "visible", "論理値"),
			new AttrValueFormatTable("preparevideo", "slot", "ムービーオブジェクト番号"),
			new AttrValueFormatTable("ptext", "layer", "背景レイヤ;前景レイヤ"),
			new AttrValueFormatTable("ptext", "page", "レイヤーページ"),
			new AttrValueFormatTable("ptext", "x", "0以上の値"),
			new AttrValueFormatTable("ptext", "y", "0以上の値"),
			new AttrValueFormatTable("ptext", "text", "任意文字列"),
			new AttrValueFormatTable("ptext", "vertical", "論理値"),
			new AttrValueFormatTable("ptext", "angle", "0以上の値"),
			new AttrValueFormatTable("ptext", "size", "1以上の値"),
			new AttrValueFormatTable("ptext", "face", "フォント名"),
			new AttrValueFormatTable("ptext", "color", "RGB値"),
			new AttrValueFormatTable("ptext", "italic", "論理値"),
			new AttrValueFormatTable("ptext", "shadow", "論理値"),
			new AttrValueFormatTable("ptext", "edge", "論理値"),
			new AttrValueFormatTable("ptext", "edgecolor", "RGB値"),
			new AttrValueFormatTable("ptext", "shadowcolor", "RGB値"),
			new AttrValueFormatTable("ptext", "bold", "論理値"),
			new AttrValueFormatTable("quake", "time", "ミリ秒時間"),
			new AttrValueFormatTable("quake", "timemode", "ms;delay"),
			new AttrValueFormatTable("quake", "hmax", "0以上の値"),
			new AttrValueFormatTable("quake", "vmax", "0以上の値"),
			new AttrValueFormatTable("r", "eol", "任意文字列"),
			new AttrValueFormatTable("rclick", "call", "論理値"),
			new AttrValueFormatTable("rclick", "jump", "論理値"),
			new AttrValueFormatTable("rclick", "target", "ラベル名"),
			new AttrValueFormatTable("rclick", "storage", "シナリオファイル名"),
			new AttrValueFormatTable("rclick", "enabled", "論理値"),
			new AttrValueFormatTable("rclick", "name", "任意文字列;default"),
			new AttrValueFormatTable("resumevideo", "slot", "ムービーオブジェクト番号"),
			new AttrValueFormatTable("return", "storage", "シナリオファイル名"),
			new AttrValueFormatTable("return", "target", "ラベル名"),
			new AttrValueFormatTable("return", "countpage", "論理値"),
			new AttrValueFormatTable("rewindvideo", "slot", "ムービーオブジェクト番号"),
			new AttrValueFormatTable("ruby", "text", "任意文字列"),
			new AttrValueFormatTable("save", "place", "0以上の値"),
			new AttrValueFormatTable("save", "ask", "論理値"),
			new AttrValueFormatTable("seopt", "buf", "効果音バッファ番号"),
			new AttrValueFormatTable("seopt", "volume", "パーセント値"),
			new AttrValueFormatTable("seopt", "gvolume", "パーセント値"),
			new AttrValueFormatTable("seopt", "pan", "-100～100の値"),
			new AttrValueFormatTable("setbgmlabel", "name", "ラベル名"),
			new AttrValueFormatTable("setbgmlabel", "storage", "シナリオファイル名"),
			new AttrValueFormatTable("setbgmlabel", "target", "ラベル名"),
			new AttrValueFormatTable("setbgmlabel", "exp", "TJS式"),
			new AttrValueFormatTable("setbgmstop", "storage", "シナリオファイル名"),
			new AttrValueFormatTable("setbgmstop", "target", "ラベル名"),
			new AttrValueFormatTable("setbgmstop", "exp", "TJS式"),
			new AttrValueFormatTable("startanchor", "enabled", "論理値"),
			new AttrValueFormatTable("stopse", "buf", "効果音バッファ番号"),
			new AttrValueFormatTable("stopvideo", "slot", "ムービーオブジェクト番号"),
			new AttrValueFormatTable("store", "enabled", "論理値"),
			new AttrValueFormatTable("style", "align", "left;top;center;right;bottom;default"),
			new AttrValueFormatTable("style", "linespacing", "0以上の値"),
			new AttrValueFormatTable("style", "pitch", "0以上の値"),
			new AttrValueFormatTable("style", "linesize", "0以上の値;default"),
			new AttrValueFormatTable("style", "autoreturn", "論理値;default"),
			new AttrValueFormatTable("tempload", "place", "0以上の値"),
			new AttrValueFormatTable("tempload", "se", "論理値"),
			new AttrValueFormatTable("tempload", "bgm", "論理値"),
			new AttrValueFormatTable("tempload", "backlay", "論理値"),
			new AttrValueFormatTable("tempsave", "place", "0以上の値"),
			new AttrValueFormatTable("timeout", "time", "ミリ秒時間"),
			new AttrValueFormatTable("timeout", "storage", "シナリオファイル名"),
			new AttrValueFormatTable("timeout", "target", "ラベル名"),
			new AttrValueFormatTable("timeout", "exp", "TJS式"),
			new AttrValueFormatTable("timeout", "se", "効果音ファイル名"),
			new AttrValueFormatTable("timeout", "sebuf", "効果音バッファ番号"),
			new AttrValueFormatTable("title", "name", "任意文字列"),
			new AttrValueFormatTable("trace", "exp", "TJS式"),
			new AttrValueFormatTable("trans", "layer", "背景レイヤ;前景レイヤ;メッセージレイヤ"),
			new AttrValueFormatTable("trans", "children", "論理値"),
			new AttrValueFormatTable("trans", "time", "ミリ秒時間"),
			new AttrValueFormatTable("trans", "method", "トランジションタイプ"),
			new AttrValueFormatTable("trans", "rule", "画像ファイル名"),
			new AttrValueFormatTable("trans", "vague", "0以上の値"),
			new AttrValueFormatTable("trans", "from", "left;top;right;bottom"),
			new AttrValueFormatTable("trans", "stay", "stayfore;stayback;nostay"),
			new AttrValueFormatTable("video", "slot", "ムービーオブジェクト番号"),
			new AttrValueFormatTable("video", "visible", "論理値"),
			new AttrValueFormatTable("video", "left", "0以上の値"),
			new AttrValueFormatTable("video", "top", "0以上の値"),
			new AttrValueFormatTable("video", "width", "1以上の値"),
			new AttrValueFormatTable("video", "height", "1以上の値"),
			new AttrValueFormatTable("video", "loop", "論理値"),
			new AttrValueFormatTable("video", "position", "ミリ秒時間"),
			new AttrValueFormatTable("video", "frame", "0以上の値"),
			new AttrValueFormatTable("video", "mode", "overlay;layer"),
			new AttrValueFormatTable("video", "playrate", "実数値"),
			new AttrValueFormatTable("video", "volume", "パーセント値"),
			new AttrValueFormatTable("video", "pan", "-100～100の値"),
			new AttrValueFormatTable("video", "audiostreamnum", "音声ストリーム番号(0?)"),
			new AttrValueFormatTable("videoevent", "slot", "ムービーオブジェクト番号"),
			new AttrValueFormatTable("videoevent", "frame", "0以上の値"),
			new AttrValueFormatTable("videolayer", "slot", "ムービーオブジェクト番号"),
			new AttrValueFormatTable("videolayer", "channel", "1;2"),
			new AttrValueFormatTable("videolayer", "page", "レイヤーページ"),
			new AttrValueFormatTable("videolayer", "layer", "0以上の値"),
			new AttrValueFormatTable("videosegloop", "slot", "ムービーオブジェクト番号"),
			new AttrValueFormatTable("videosegloop", "start", "0以上の値"),
			new AttrValueFormatTable("videosegloop", "end", "0以上の値"),
			new AttrValueFormatTable("wa", "layer", "背景レイヤ;前景レイヤ"),
			new AttrValueFormatTable("wa", "page", "レイヤーページ"),
			new AttrValueFormatTable("wa", "seg", "0以上の値"),
			new AttrValueFormatTable("wait", "time", "ミリ秒時間"),
			new AttrValueFormatTable("wait", "mode", "normal;until"),
			new AttrValueFormatTable("wait", "canskip", "論理値"),
			new AttrValueFormatTable("waittrig", "name", "任意文字列"),
			new AttrValueFormatTable("waittrig", "canskip", "論理値"),
			new AttrValueFormatTable("waittrig", "onskip", "TJS式"),
			new AttrValueFormatTable("wb", "canskip", "論理値"),
			new AttrValueFormatTable("wc", "time", "任意文字列"),
			new AttrValueFormatTable("wf", "buf", "効果音バッファ番号"),
			new AttrValueFormatTable("wf", "canskip", "論理値"),
			new AttrValueFormatTable("wheel", "storage", "シナリオファイル名"),
			new AttrValueFormatTable("wheel", "target", "ラベル名"),
			new AttrValueFormatTable("wheel", "func", "TJS式"),
			new AttrValueFormatTable("wheel", "exp", "TJS式"),
			new AttrValueFormatTable("wheel", "se", "効果音ファイル名"),
			new AttrValueFormatTable("wheel", "sebuf", "効果音バッファ番号"),
			new AttrValueFormatTable("wl", "canskip", "論理値"),
			new AttrValueFormatTable("wm", "canskip", "論理値"),
			new AttrValueFormatTable("wp", "slot", "ムービーオブジェクト番号"),
			new AttrValueFormatTable("wp", "for", "loop;period;prepare;segLoop"),
			new AttrValueFormatTable("wq", "canskip", "論理値"),
			new AttrValueFormatTable("ws", "buf", "効果音バッファ番号"),
			new AttrValueFormatTable("ws", "canskip", "論理値"),
			new AttrValueFormatTable("wt", "canskip", "論理値"),
			new AttrValueFormatTable("wv", "slot", "ムービーオブジェクト番号"),
			new AttrValueFormatTable("wv", "canskip", "論理値"),
			new AttrValueFormatTable("xchgbgm", "storage", "BGMファイル名"),
			new AttrValueFormatTable("xchgbgm", "loop", "論理値"),
			new AttrValueFormatTable("xchgbgm", "time", "ミリ秒時間"),
			new AttrValueFormatTable("xchgbgm", "overlap", "ミリ秒時間"),
			new AttrValueFormatTable("xchgbgm", "volume", "パーセント値"),
		};
		#endregion

		string m_filePath;
		Dictionary<string, KagTag> m_tags = new Dictionary<string, KagTag>();
		List<string> m_attrNameList = new List<string>();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="filePath">ファイルパス</param>
		public KagTagsFile(string filePath)
		{
			m_filePath = filePath;
			LoadFileFromXml(m_filePath);
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public KagTagsFile()
		{
			m_filePath = "";
		}

		/// <summary>
		/// tags.xmlを読み込む
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public bool LoadFileFromXml(string filePath)
		{
			m_filePath = filePath;
			if (File.Exists(m_filePath) == false)
			{
				Debug.WriteLine("指定されたファイルが存在しません");
				return false;
			}

			//XMLファイルを読み込み
			XmlDocument doc = new XmlDocument();
			doc.Load(m_filePath);
			XmlNodeList nodeList = doc.GetElementsByTagName("tag");
			if (nodeList == null)
			{
				Debug.WriteLine("指定されたXMLファイルが不正です");
				return false;
			}

			bool kagex = false;
			if (Path.GetFileName(filePath) == "kagextags.xml"
			||  Path.GetFileName(filePath) == "worldextags.xml")
			{
				kagex = true;
			}

			//ノード読み込み
			foreach (XmlElement tagNode in nodeList)
			{
				//タグノード読み込み
				KagTag tag = readTagNode(tagNode, kagex);
				if (tag == null || tag.Name == "" || m_tags.ContainsKey(tag.Name))
				{
					//取得できなかった時、またはすでに登録済みの場合は登録しない
					continue;
				}

				m_tags.Add(tag.Name, tag);
			}

			foreach (string text in m_attrNameList)
			{
				Debug.WriteLine(text);
			}

			return true;
		}

		/// <summary>
		/// タグノードを読み込む
		/// </summary>
		/// <param name="tagElement">タグエレメント</param>
		private KagTag readTagNode(XmlElement tagElement, bool kagex)
		{
			KagTag tag = new KagTag(kagex);
			foreach (XmlElement node in tagElement.ChildNodes)
			{
				switch (node.Name)
				{
					case "tagname":
						tag.Name = node.InnerText;
						break;
					case "group":
						tag.Group = node.InnerText;
						break;
					case "tagshortinfo":
						tag.ShortInfo = node.InnerText;
						break;
					case "tagremarks":
						tag.SetRemarks(node.InnerText);
						break;
					case "attr":
						KagTagAttr attr = readTagAttrNode(node);
						if (attr == null || attr.Name == "" || tag.AttrTable.ContainsKey(attr.Name))
						{
							//取得できなかった時、またはすでに登録済みの場合は登録しない
							break;
						}
						tag.AttrTable.Add(attr.Name, attr);
						m_attrNameList.Add(tag.Name + "\t" + attr.Name + "\t" + attr.Format);
						break;
				}
			}

			//属性値フォーマットを改めてセットする
			foreach (string attrName in tag.AttrTable.Keys)
			{
				string format = getAttrValueFormatFromTable(tag.Name, attrName);
				if (format != "")
				{
					//フォーマットテーブルにのっているときはそれを使用する
					tag.AttrTable[attrName].Format = format;
				}
			}
			return tag;
		}

		/// <summary>
		/// タグ属性ノードを読み込む
		/// </summary>
		/// <param name="attrElement">タグ属性エレメント</param>
		private KagTagAttr readTagAttrNode(XmlElement attrElement)
		{
			KagTagAttr attr = new KagTagAttr();
			foreach (XmlElement node in attrElement.ChildNodes)
			{
				switch (node.Name)
				{
					case "attrname":
						attr.Name = node.InnerText;
						break;
					case "attrshortinfo":
						attr.ShortInfo = node.InnerText;
						break;
					case "attrrequired":
						attr.SetRequiredFormString(node.InnerText);
						break;
					case "attrformat":
						//格納する（リストに載っていないものはこれを使用する）
						attr.Format = node.InnerText;
						break;
					case "attrinfo":
						attr.SetInfo(node.InnerText);
						break;
				}
			}

			return attr;
		}

		/// <summary>
		/// 属性値フォーマットをテーブルから取得する
		/// </summary>
		/// <param name="tagName">タグ名</param>
		/// <param name="attrName">属性名</param>
		/// <returns>属性値フォーマット 存在しないときはNOT_FOUND_FOMATを返す</returns>
		private string getAttrValueFormatFromTable(string tagName, string attrName)
		{
			foreach (AttrValueFormatTable format in this.ATTR_VALUE_FOMAT_TABLE)
			{
				if (tagName == format.TagName && attrName == format.AttrName)
				{
					return format.ValueFormat;
				}
			}

			//cond属性をチェックする
			if (KagTag.HaveCondAttr(tagName) == true && attrName == ATTR_VALUE_FOMAT_COND.AttrName)
			{
				return ATTR_VALUE_FOMAT_COND.ValueFormat;
			}

			return "";
		}

		/// <summary>
		/// KAGファイルに保存する
		/// </summary>
		/// <param name="filePath">保存するファイルパス</param>
		/// <returns>成功したときtrue</returns>
		public bool SaveFileToKag(string filePath)
		{
			if (filePath == "")
			{
				Debug.WriteLine("保存するファイルパスが不正です");
				return false;
			}
			if (m_tags.Count == 0)
			{
				Debug.WriteLine("保存するタグ項目が存在しません");
				return false;
			}

			bool ret = true;
			StreamWriter sw;
			using (sw = new StreamWriter(filePath))
			{
				try
				{
					foreach (KagTag tag in m_tags.Values)
					{
						sw.WriteLine(getWriteKagTag(tag));
					}
				}
				catch (Exception err)
				{
					ret = false;
					Debug.WriteLine("ファイルの保存に失敗しました: " + err.ToString());
				}
			}

			return ret;
		}

		/// <summary>
		/// KAGファイル出力用タグを生成する
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		private string getWriteKagTag(KagTag tag)
		{
			string text = "";

			//マクロコメント生成
			text += String.Format(";;[{0}] {1}\n", tag.Group, tag.ShortInfo);
			text += String.Format(";;{0}\n", tag.Remarks);
			foreach (KagTagAttr attr in tag.AttrTable.Values)
			{
				text += String.Format(";;{0}=", attr.Name);
				if (attr.Required)
				{
					text += String.Format("[{0}] ", "必須");
				}
				text += String.Format("{0}\\n{1}, {2}\n", attr.ShortInfo, attr.Info, attr.Format);
			}

			//タグマクロ生成
			text += String.Format("[macro name=\"{0}\"]\n", tag.Name);
			text += String.Format("[kagtag");
			foreach (KagTagAttr attr in tag.AttrTable.Values)
			{
				text += String.Format(" {0}=%{1}", attr.Name, attr.Name);
			}
			text += String.Format("]\n");
			text += String.Format("[endmacro]\n");

			return text;
		}
	}
}
