using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using kkde.project;
using kkde.global;
using System.IO;

namespace kkde.option
{
	public static class TypeColorFile
	{
		/// <summary>
		/// デフォルトのカラータイプ
		/// </summary>
		public enum DefaultColorType
		{
			/// <summary>
			/// 白ベース
			/// </summary>
			White,
			/// <summary>
			/// 黒ベース
			/// </summary>
			Black,
		}

		/// <summary>
		/// カラー設定をファイルに保存する
		/// </summary>
		/// <param name="type"></param>
		/// <param name="colorType"></param>
		public static bool SaveFile(FileType.KrkrType type, BaseColorType colorType)
		{
			string fileName = "";
			string text = "";
			switch (type)
			{
				case FileType.KrkrType.Kag:
					fileName = ConstEnvOption.KagModeFilePath;
					text = createKagXshd((KagColorType)colorType);
					break;
				case FileType.KrkrType.Tjs:
					fileName = ConstEnvOption.TjsModeFilePath;
					text = createTjsXshd((TjsColorType)colorType);
					break;
				default:
					fileName = ConstEnvOption.DefModeFilePath;
					text = createDefXshd(colorType);
					break;
			}
			if (text == "")
			{
				return false;
			}

			//ファイルに保存する
			bool ret = true;
			StreamWriter sw;
			using (sw = new StreamWriter(fileName, false))
			{
				try
				{
					sw.Write(text);
				}
				catch (Exception err)
				{
					System.Diagnostics.Debug.WriteLine("カラー設定ファイルの保存に失敗しました: " + err.ToString());
					ret = false;
				}
			}

			return ret;
		}

		/// <summary>
		/// デフォルト強調定義ファイルを作成する
		/// </summary>
		/// <param name="fileName">作成するファイル名</param>
		private static string createDefXshd(BaseColorType colorType)
		{
			string text = "";
			text += String.Format("<?xml version=\"1.0\"?>\n");
			text += String.Format("\n");
			text += String.Format("<!-- syntaxdefinition for Default by PORING SOFT -->\n");
			text += String.Format("<SyntaxDefinition name=\"Default\" extensions=\".*\">\n");

			//共通部の色設定
			text += String.Format("    <Environment>\n");
			text += String.Format("        <Default      color=\"{0}\" bgcolor=\"{1}\" />\n", getColorName(colorType.WindowFront), getColorName(colorType.WindowBack));
			text += String.Format("        <Selection    color=\"{0}\" bgcolor=\"{1}\" />\n", getColorName(colorType.SelectTextFront), getColorName(colorType.SelectTextBack));
			text += String.Format("        <VRuler       color=\"{0}\" />\n", getColorName(colorType.VRuler));
			text += String.Format("        <InvalidLines color=\"{0}\" />\n", getColorName(colorType.InvalidLines));
			text += String.Format("        <CaretMarker  color=\"{0}\" />\n", getColorName(colorType.CaretMarker));
			text += String.Format("        \n");
			text += String.Format("        <LineNumbers  color=\"{0}\" bgcolor=\"{1}\" />\n", getColorName(colorType.LineNumbersFront), getColorName(colorType.LineNumbersBack));
			text += String.Format("        \n");
			text += String.Format("        <FoldLine     color=\"{0}\" bgcolor=\"{1}\" />\n", getColorName(colorType.FoldLineFront), getColorName(colorType.FoldLineBack));
			text += String.Format("        <FoldMarker   color=\"{0}\" bgcolor=\"{1}\" />\n", getColorName(colorType.FoldMarkerFront), getColorName(colorType.FoldMarkerBack));
			text += String.Format("        <SelectedFoldLine color=\"{0}\" bgcolor=\"{1}\" />\n", getColorName(colorType.SelectFoldLineFront), getColorName(colorType.SelectFoldLineBack));
			text += String.Format("        \n");
			text += String.Format("        <EOLMarkers   color=\"{0}\" />\n", getColorName(colorType.EolMarkers));
			text += String.Format("        <SpaceMarkers color=\"{0}\" />\n", getColorName(colorType.SpaceMakers));
			text += String.Format("        <TabMarkers   color=\"{0}\" />\n", getColorName(colorType.TabMarkers));
			text += String.Format("    </Environment>\n");
			text += String.Format("    \n");

			//数値の色設定
			text += String.Format("    <Digits name=\"Digits\" bold=\"false\" italic=\"false\" color=\"{0}\" />\n", getColorName(colorType.Digits));
			text += String.Format("    \n");

			text += String.Format("    <RuleSets>\n");
			text += String.Format("        <RuleSet ignorecase=\"true\">\n");
			text += String.Format("        </RuleSet>\n");
			text += String.Format("    </RuleSets>\n");
			text += String.Format("</SyntaxDefinition>\n");
			text += String.Format("\n");

			return text;
		}

		/// <summary>
		/// KAG強調定義ファイルを作成する
		/// </summary>
		/// <param name="fileName"></param>
		private static string createKagXshd(KagColorType colorType)
		{
			string text = "";
			text += String.Format("<?xml version=\"1.0\"?>\n");
			text += String.Format("\n");
			text += String.Format("<!-- syntaxdefinition for KAG by PORING SOFT -->\n");
			text += String.Format("<SyntaxDefinition name=\"KAG\" extensions=\".ks\">\n");

			//共通部の色設定
			text += String.Format("    <Environment>\n");
			text += String.Format("        <Default      color=\"{0}\" bgcolor=\"{1}\" />\n", getColorName(colorType.WindowFront), getColorName(colorType.WindowBack));
			text += String.Format("        <Selection    color=\"{0}\" bgcolor=\"{1}\" />\n", getColorName(colorType.SelectTextFront), getColorName(colorType.SelectTextBack));
			text += String.Format("        <VRuler       color=\"{0}\" />\n", getColorName(colorType.VRuler));
			text += String.Format("        <InvalidLines color=\"{0}\" />\n", getColorName(colorType.InvalidLines));
			text += String.Format("        <CaretMarker  color=\"{0}\" />\n", getColorName(colorType.CaretMarker));
			text += String.Format("        \n");
			text += String.Format("        <LineNumbers  color=\"{0}\" bgcolor=\"{1}\" />\n", getColorName(colorType.LineNumbersFront), getColorName(colorType.LineNumbersBack));
			text += String.Format("        \n");
			text += String.Format("        <FoldLine     color=\"{0}\" bgcolor=\"{1}\" />\n", getColorName(colorType.FoldLineFront), getColorName(colorType.FoldLineBack));
			text += String.Format("        <FoldMarker   color=\"{0}\" bgcolor=\"{1}\" />\n", getColorName(colorType.FoldMarkerFront), getColorName(colorType.FoldMarkerBack));
			text += String.Format("        <SelectedFoldLine color=\"{0}\" bgcolor=\"{1}\" />\n", getColorName(colorType.SelectFoldLineFront), getColorName(colorType.SelectFoldLineBack));
			text += String.Format("        \n");
			text += String.Format("        <EOLMarkers   color=\"{0}\" />\n", getColorName(colorType.EolMarkers));
			text += String.Format("        <SpaceMarkers color=\"{0}\" />\n", getColorName(colorType.SpaceMakers));
			text += String.Format("        <TabMarkers   color=\"{0}\" />\n", getColorName(colorType.TabMarkers));
			text += String.Format("    </Environment>\n");
			text += String.Format("    \n");

			//プロパティの設定
			text += String.Format("    <Properties>\n");
			text += String.Format("        <Property name=\"LineComment\" value=\";\"/>\n");
			text += String.Format("    </Properties>\n");
			text += String.Format("    \n");

			//数値の色設定
			text += String.Format("    <Digits name=\"Digits\" bold=\"false\" italic=\"false\" color=\"{0}\" />\n", getColorName(colorType.Digits));
			text += String.Format("    \n");

			//KAG3の色設定
			text += String.Format("    <RuleSets>\n");
			text += String.Format("        <RuleSet ignorecase=\"true\" noescapesequences=\"true\">\n");
			text += String.Format("            <Delimiters>=[]@ </Delimiters>\n");
			text += String.Format("            \n");
			text += String.Format("            <Span name=\"LineComment\" bold=\"false\" italic=\"false\" color=\"{0}\" stopateol=\"true\">\n", getColorName(colorType.Comment));
			text += String.Format("                <Begin>@^;</Begin>\n");
			text += String.Format("            </Span>\n");
			text += String.Format("            \n");
			text += String.Format("            <Span name=\"TJS2Tag\" rule=\"TJS2TagRuleSet\" bold=\"false\" italic=\"false\" color=\"{0}\" stopateol=\"false\">\n", getColorName(colorType.TjsScript));
			text += String.Format("                <Begin>[iscript]</Begin>\n");
			text += String.Format("                <End>[endscript]</End>\n");
			text += String.Format("            </Span>\n");
			text += String.Format("            \n");
			text += String.Format("            <Span name=\"TJS2Command\" rule=\"TJS2TagRuleSet\" bold=\"false\" italic=\"false\" color=\"{0}\" stopateol=\"false\">\n", getColorName(colorType.TjsScript));
			text += String.Format("                <Begin>@@iscript</Begin>\n");
			text += String.Format("                <End>@@endscript</End>\n");
			text += String.Format("            </Span>\n");
			text += String.Format("            \n");
			text += String.Format("            <Span name=\"KAG3Tag\" rule=\"KAG3TagRuleSet\" bold=\"false\" italic=\"false\" color=\"{0}\" stopateol=\"true\">\n", getColorName(colorType.TagName));
			text += String.Format("                <Begin>[</Begin>\n");
			text += String.Format("                <End>]</End>\n");
			text += String.Format("            </Span>\n");
			text += String.Format("            \n");
			text += String.Format("            <Span name=\"KAG3Command\" rule=\"KAG3TagRuleSet\" bold=\"false\" italic=\"false\" color=\"{0}\" stopateol=\"true\">\n", getColorName(colorType.TagName));
			text += String.Format("                <Begin>@^@@</Begin>\n");
			text += String.Format("            </Span>\n");
			text += String.Format("            \n");
			text += String.Format("            <Span name=\"KAG3Label\" bold=\"false\" italic=\"false\" color=\"{0}\" stopateol=\"true\">\n", getColorName(colorType.Label));
			text += String.Format("                <Begin>@^*</Begin>\n");
			text += String.Format("                <End>|</End>\n");
			text += String.Format("            </Span>\n");
			text += String.Format("            \n");
			text += String.Format("        </RuleSet>\n");
			text += String.Format("        \n");
			text += String.Format("        <RuleSet name=\"TJS2TagRuleSet\" reference=\"TJS2\" />\n");
			text += String.Format("        \n");
			text += String.Format("        <RuleSet name=\"KAG3TagRuleSet\" ignorecase=\"false\" noescapesequences=\"true\">\n");
			text += String.Format("            <Delimiters>=</Delimiters>\n");
			text += String.Format("            \n");
			text += String.Format("            <Span name=\"String\" bold=\"false\" italic=\"false\" color=\"{0}\" stopateol=\"true\">\n", getColorName(colorType.AttributeValue));
			text += String.Format("                <Begin>&quot;</Begin>\n");
			text += String.Format("                <End>&quot;</End>\n");
			text += String.Format("            </Span>\n");
			text += String.Format("            \n");
			text += String.Format("            <Span name=\"Char\" bold=\"false\" italic=\"false\" color=\"{0}\" stopateol=\"true\">\n", getColorName(colorType.AttributeValue));
			text += String.Format("                <Begin>&apos;</Begin>\n");
			text += String.Format("                <End>&apos;</End>\n");
			text += String.Format("            </Span>\n");
			text += String.Format("            \n");
			text += String.Format("            <MarkPrevious bold=\"false\" italic=\"false\" color=\"{0}\">=</MarkPrevious>\n", getColorName(colorType.AttributeName));
			text += String.Format("            \n");
			text += String.Format("            <KeyWords name=\"Assignment\" bold=\"false\" italic=\"false\" color=\"{0}\">\n", getColorName(colorType.AttributeValue));
			text += String.Format("                <Key word=\"=\" />\n");
			text += String.Format("            </KeyWords>\n");
			text += String.Format("        </RuleSet>\n");
			text += String.Format("    </RuleSets>\n");

			text += String.Format("</SyntaxDefinition>\n");
			text += String.Format("\n");

			return text;
		}

		/// <summary>
		/// TJS強調定義ファイルを作成する
		/// </summary>
		/// <param name="fileName">作成するファイル名</param>
		private static string createTjsXshd(TjsColorType colorType)
		{
			string text = "";
			text += String.Format("<?xml version=\"1.0\"?>\n");
			text += String.Format("\n");
			text += String.Format("<!-- syntaxdefinition for TJS2 by PORING SOFT -->\n");
			text += String.Format("<SyntaxDefinition name=\"TJS2\" extensions=\".tjs\">\n");

			//共通部の色設定
			text += String.Format("    <Environment>\n");
			text += String.Format("        <Default      color=\"{0}\" bgcolor=\"{1}\" />\n", getColorName(colorType.WindowFront), getColorName(colorType.WindowBack));
			text += String.Format("        <Selection    color=\"{0}\" bgcolor=\"{1}\" />\n", getColorName(colorType.SelectTextFront), getColorName(colorType.SelectTextBack));
			text += String.Format("        <VRuler       color=\"{0}\" />\n", getColorName(colorType.VRuler));
			text += String.Format("        <InvalidLines color=\"{0}\" />\n", getColorName(colorType.InvalidLines));
			text += String.Format("        <CaretMarker  color=\"{0}\" />\n", getColorName(colorType.CaretMarker));
			text += String.Format("        \n");
			text += String.Format("        <LineNumbers  color=\"{0}\" bgcolor=\"{1}\" />\n", getColorName(colorType.LineNumbersFront), getColorName(colorType.LineNumbersBack));
			text += String.Format("        \n");
			text += String.Format("        <FoldLine     color=\"{0}\" bgcolor=\"{1}\" />\n", getColorName(colorType.FoldLineFront), getColorName(colorType.FoldLineBack));
			text += String.Format("        <FoldMarker   color=\"{0}\" bgcolor=\"{1}\" />\n", getColorName(colorType.FoldMarkerFront), getColorName(colorType.FoldMarkerBack));
			text += String.Format("        <SelectedFoldLine color=\"{0}\" bgcolor=\"{1}\" />\n", getColorName(colorType.SelectFoldLineFront), getColorName(colorType.SelectFoldLineBack));
			text += String.Format("        \n");
			text += String.Format("        <EOLMarkers   color=\"{0}\" />\n", getColorName(colorType.EolMarkers));
			text += String.Format("        <SpaceMarkers color=\"{0}\" />\n", getColorName(colorType.SpaceMakers));
			text += String.Format("        <TabMarkers   color=\"{0}\" />\n", getColorName(colorType.TabMarkers));
			text += String.Format("    </Environment>\n");
			text += String.Format("    \n");

			//プロパティの設定
			text += String.Format("    <Properties>\n");
			text += String.Format("        <Property name=\"LineComment\" value=\"//\"/>\n");
			text += String.Format("    </Properties>\n");
			text += String.Format("    \n");

			//数値の色設定
			text += String.Format("    <Digits name=\"Digits\" bold=\"false\" italic=\"false\" color=\"{0}\" />\n", getColorName(colorType.Digits));
			text += String.Format("    \n");

			//TJSの設定
			text += String.Format("    <RuleSets>\n");
			text += String.Format("        <RuleSet ignorecase=\"false\">\n");
			text += "            <Delimiters>=!&gt;&lt;+-/*%&amp;|^~.}{,;][?:()</Delimiters>\n";
			text += String.Format("            \n");
			text += String.Format("            <Span name=\"LineComment\" bold=\"false\" italic=\"false\" color=\"{0}\" stopateol=\"true\">\n", getColorName(colorType.Comment));
			text += String.Format("                <Begin>//</Begin>\n");
			text += String.Format("            </Span>\n");
			text += String.Format("            \n");
			text += String.Format("            <Span name=\"BlockComment\" bold=\"false\" italic=\"false\" color=\"{0}\" stopateol=\"false\">\n", getColorName(colorType.Comment));
			text += String.Format("                <Begin>/*</Begin>\n");
			text += String.Format("                <End>*/</End>\n");
			text += String.Format("            </Span>\n");
			text += String.Format("            \n");
			text += String.Format("            <Span name=\"String\" bold=\"false\" italic=\"false\" color=\"{0}\" stopateol=\"false\" escapecharacter=\"\\\">\n", getColorName(colorType.String));
			text += String.Format("                <Begin>&quot;</Begin>\n");
			text += String.Format("                <End>&quot;</End>\n");
			text += String.Format("            </Span>\n");
			text += String.Format("            <Span name =\"AtString\" bold=\"false\" italic=\"false\" color=\"{0}\" stopateol=\"false\">\n", getColorName(colorType.String));
			text += String.Format("                <Begin>@@&quot;</Begin>\n");
			text += String.Format("                <End>&quot;</End>\n");
			text += String.Format("            </Span>\n");
			text += String.Format("            \n");
			text += String.Format("            <Span name=\"Character\" bold=\"false\" italic=\"false\" color=\"{0}\" stopateol=\"true\" escapecharacter=\"\\\">\n", getColorName(colorType.String));
			text += String.Format("                <Begin>&apos;</Begin>\n");
			text += String.Format("                <End>&apos;</End>\n");
			text += String.Format("            </Span>\n");
			text += String.Format("            <Span name=\"AtCharacter\" bold=\"false\" italic=\"false\" color=\"{0}\" stopateol=\"false\">\n", getColorName(colorType.String));
			text += String.Format("                <Begin>@@&apos;</Begin>\n");
			text += String.Format("                <End>&apos;</End>\n");
			text += String.Format("            </Span>\n");
			text += String.Format("            \n");
			//正規表現は削除
			//text += String.Format("            <Span name=\"Regexp\" bold=\"false\" italic=\"false\" color=\"{0}\" stopateol=\"true\">\n", getColorName(colorType.Regexp));
			//text += String.Format("                <Begin>/</Begin>\n");
			//text += String.Format("                <End>/</End>\n");
			//text += String.Format("            </Span>\n");
			//text += String.Format("            \n");
			text += String.Format("            <Span name=\"Octet\" bold=\"false\" italic=\"false\" color=\"{0}\" stopateol=\"true\">\n", getColorName(colorType.Octet));
			text += String.Format("                <Begin>&lt;%</Begin>\n");
			text += String.Format("                <End>%&gt;</End>\n");
			text += String.Format("            </Span>\n");
			text += String.Format("            \n");
			text += String.Format("            <KeyWords name=\"TJSPreProcessorWords\" bold=\"false\" italic=\"false\" color=\"{0}\">\n", getColorName(colorType.PreProcessorKeyWord));
			text += String.Format("                <Key word=\"@set\" />\n");
			text += String.Format("                <Key word=\"@if\" />\n");
			text += String.Format("                <Key word=\"@endif\" />\n");
			text += String.Format("            </KeyWords>\n");
			text += String.Format("            \n");
			text += String.Format("            <KeyWords name=\"TJSKeyWords\" bold=\"false\" italic=\"false\" color=\"{0}\">\n", getColorName(colorType.KeyWord));
			text += String.Format("                <Key word=\"break\" />\n");
			text += String.Format("                <Key word=\"continue\" />\n");
			text += String.Format("                <Key word=\"const\" />\n");
			text += String.Format("                <Key word=\"catch\" />\n");
			text += String.Format("                <Key word=\"class\" />\n");
			text += String.Format("                <Key word=\"case\" />\n");
			text += String.Format("                <Key word=\"debugger\" />\n");
			text += String.Format("                <Key word=\"default\" />\n");
			text += String.Format("                <Key word=\"delete\" />\n");
			text += String.Format("                <Key word=\"do\" />\n");
			text += String.Format("                <Key word=\"extends\" />\n");
			text += String.Format("                <Key word=\"export\" />\n");
			text += String.Format("                <Key word=\"enum\" />\n");
			text += String.Format("                <Key word=\"else\" />\n");
			text += String.Format("                <Key word=\"function\" />\n");
			text += String.Format("                <Key word=\"finally\" />\n");
			text += String.Format("                <Key word=\"false\" />\n");
			text += String.Format("                <Key word=\"for\" />\n");
			text += String.Format("                <Key word=\"global\" />\n");
			text += String.Format("                <Key word=\"getter\" />\n");
			text += String.Format("                <Key word=\"goto\" />\n");
			text += String.Format("                <Key word=\"incontextof\" />\n");
			text += String.Format("                <Key word=\"Infinity\" />\n");
			text += String.Format("                <Key word=\"invalidate\" />\n");
			text += String.Format("                <Key word=\"instanceof\" />\n");
			text += String.Format("                <Key word=\"isvalid\" />\n");
			text += String.Format("                <Key word=\"import\" />\n");
			text += String.Format("                <Key word=\"int\" />\n");
			text += String.Format("                <Key word=\"in\" />\n");
			text += String.Format("                <Key word=\"if\" />\n");
			text += String.Format("                <Key word=\"NaN\" />\n");
			text += String.Format("                <Key word=\"null\" />\n");
			text += String.Format("                <Key word=\"new\" />\n");
			text += String.Format("                <Key word=\"octet\" />\n");
			text += String.Format("                <Key word=\"protected\" />\n");
			text += String.Format("                <Key word=\"property\" />\n");
			text += String.Format("                <Key word=\"private\" />\n");
			text += String.Format("                <Key word=\"public\" />\n");
			text += String.Format("                <Key word=\"return\" />\n");
			text += String.Format("                <Key word=\"real\" />\n");
			text += String.Format("                <Key word=\"synchronized\" />\n");
			text += String.Format("                <Key word=\"switch\" />\n");
			text += String.Format("                <Key word=\"static\" />\n");
			text += String.Format("                <Key word=\"setter\" />\n");
			text += String.Format("                <Key word=\"string\" />\n");
			text += String.Format("                <Key word=\"super\" />\n");
			text += String.Format("                <Key word=\"typeof\" />\n");
			text += String.Format("                <Key word=\"throw\" />\n");
			text += String.Format("                <Key word=\"this\" />\n");
			text += String.Format("                <Key word=\"true\" />\n");
			text += String.Format("                <Key word=\"try\" />\n");
			text += String.Format("                <Key word=\"void\" />\n");
			text += String.Format("                <Key word=\"var\" />\n");
			text += String.Format("                <Key word=\"while\" />\n");
			text += String.Format("                <Key word=\"with\" />\n");
			text += String.Format("            </KeyWords>\n");
			text += String.Format("        </RuleSet>\n");
			text += String.Format("    </RuleSets>\n");

			text += String.Format("</SyntaxDefinition>\n");
			text += String.Format("\n");

			return text;
		}

		/// <summary>
		/// カラーから設定ファイルの色名に変換する
		/// </summary>
		/// <param name="color">変換するカラー</param>
		/// <returns>変換したカラー名</returns>
		private static string getColorName(Color color)
		{
			string name = "";
			if (color.IsEmpty)
			{
				name = "#000000";
			}
			else if (color.IsSystemColor)
			{
				name = "SystemColors." + color.Name;
			}
			else if (color.IsNamedColor || color.IsKnownColor)
			{
				name = color.Name;
			}
			else
			{
				name = String.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
			}

			return name;
		}

		/// <summary>
		/// デフォルトのカラーオブジェクトを取得する
		/// </summary>
		/// <param name="type">取得したいファイルのタイプ</param>
		/// <param name="deftype">デフォルトカラータイプ</param>
		/// <returns>カラーオブジェクト</returns>
		public static BaseColorType GetDefault(FileType.KrkrType type, DefaultColorType deftype)
		{
			BaseColorType colorType = null;
			switch (type)
			{
				case FileType.KrkrType.Kag:
					colorType = getDefaultKagColor(deftype);
					break;
				case FileType.KrkrType.Tjs:
					colorType = getDefaultTjsColor(deftype);
					break;
				default:
					colorType = getDefaultColor(deftype);
					break;
			}

			return colorType;
		}

		/// <summary>
		/// 共通部分のデフォルトカラーを取得する
		/// </summary>
		/// <param name="deftype">ベースとなるカラータイプ</param>
		/// <returns>カラーオブジェクト</returns>
		private static BaseColorType getDefaultColor(DefaultColorType deftype)
		{
			BaseColorType type = new BaseColorType();

			switch (deftype)
			{
				case DefaultColorType.Black:
					type.WindowFront = Color.FromArgb(0xDD, 0xDD, 0xDD);
					type.WindowBack = Color.FromArgb(0x22, 0x22, 0x22);
					type.SelectTextFront = Color.FromArgb(0xDD, 0xDD, 0xDD);
					type.SelectTextBack = Color.FromArgb(0x80, 0x80, 0x80);
					type.VRuler = Color.FromArgb(0x80, 0x80, 0x80);
					type.InvalidLines = Color.FromArgb(0x80, 0x00, 0x00);
					type.CaretMarker = Color.FromArgb(0x80, 0x40, 0x00);
					type.LineNumbersFront = Color.FromArgb(0xCC, 0xCC, 0xCC);
					type.LineNumbersBack = Color.FromArgb(0x22, 0x22, 0x22);
					type.FoldLineFront = Color.FromArgb(0x80, 0x80, 0x80);
					type.FoldLineBack = Color.White;
					type.FoldMarkerFront = Color.FromArgb(0x80, 0x80, 0x80);
					type.FoldMarkerBack = Color.Black;
					type.SelectFoldLineFront = Color.White;
					type.SelectFoldLineBack = Color.White;
					type.EolMarkers = Color.FromArgb(0x00, 0x80, 0x80);
					type.SpaceMakers = Color.FromArgb(0x80, 0x80, 0x80);
					type.TabMarkers = Color.FromArgb(0x80, 0x80, 0x80);
					type.Digits = Color.FromArgb(0xDD, 0xDD, 0xDD);
					break;
				case DefaultColorType.White:
				default:
					type.WindowFront = SystemColors.WindowText;
					type.WindowBack = SystemColors.Window;
					type.SelectTextFront = SystemColors.HighlightText;
					type.SelectTextBack = SystemColors.Highlight;
					type.VRuler = SystemColors.ControlDark;
					type.InvalidLines = Color.Red;
					type.CaretMarker = Color.FromArgb(0xFF, 0xE0, 0xC0);
					type.LineNumbersFront = Color.Teal;
					type.LineNumbersBack = SystemColors.Window;
					type.FoldLineFront = Color.FromArgb(0x80, 0x80, 0x80);
					type.FoldLineBack = Color.Black;
					type.FoldMarkerFront = Color.FromArgb(0x80, 0x80, 0x80);
					type.FoldMarkerBack = Color.White;
					type.SelectFoldLineFront = Color.Black;
					type.SelectFoldLineBack = Color.Black;
					type.EolMarkers = Color.Teal;
					type.SpaceMakers = SystemColors.Control;
					type.TabMarkers = SystemColors.Control;
					type.Digits = SystemColors.WindowText;
					break;
			}

			return type;
		}

		/// <summary>
		/// KAGファイルのデフォルトカラーオブジェクトを取得する
		/// </summary>
		/// <param name="deftype">ベースとなるカラータイプ</param>
		/// <returns>カラーオブジェクト</returns>
		private static KagColorType getDefaultKagColor(DefaultColorType deftype)
		{
			BaseColorType baseType = getDefaultColor(deftype);
			KagColorType type = new KagColorType();
			type.WindowFront = baseType.WindowFront;
			type.WindowBack = baseType.WindowBack;
			type.SelectTextFront = baseType.SelectTextFront;
			type.SelectTextBack = baseType.SelectTextBack;
			type.VRuler = baseType.VRuler;
			type.InvalidLines = baseType.InvalidLines;
			type.CaretMarker = baseType.CaretMarker;
			type.LineNumbersFront = baseType.LineNumbersFront;
			type.LineNumbersBack = baseType.LineNumbersBack;
			type.FoldLineFront = baseType.FoldLineFront;
			type.FoldLineBack = baseType.FoldLineBack;
			type.FoldMarkerFront = baseType.FoldMarkerFront;
			type.FoldMarkerBack = baseType.FoldMarkerBack;
			type.SelectFoldLineFront = baseType.SelectFoldLineFront;
			type.SelectFoldLineBack = baseType.SelectFoldLineBack;
			type.EolMarkers = baseType.EolMarkers;
			type.SpaceMakers = baseType.SpaceMakers;
			type.TabMarkers = baseType.TabMarkers;
			type.Digits = baseType.Digits;

			switch (deftype)
			{
				case DefaultColorType.Black:
					type.Comment = Color.FromArgb(0x64, 0xB1, 0xFF);
					type.TjsScript = Color.FromArgb(0xDD, 0xDD, 0xDD);
					type.TagName = Color.FromArgb(0xFF, 0x80, 0x80);
					type.Label = Color.FromArgb(0x80, 0xFF, 0x00);
					type.AttributeName = Color.FromArgb(0xC0, 0xC0, 0x40);
					type.AttributeValue = Color.FromArgb(0x00, 0xA4, 0x5F);
					break;
				case DefaultColorType.White:
				default:
					type.Comment = Color.DarkGreen;
					type.TjsScript = SystemColors.WindowText;
					type.TagName = Color.DarkMagenta;
					type.Label = Color.Green;
					type.AttributeName = Color.Red;
					type.AttributeValue = Color.Blue;
					break;
			}

			return type;
		}

		/// <summary>
		/// TJSファイルのデフォルトカラーオブジェクトを取得する
		/// </summary>
		/// <param name="deftype">ベースとなるカラータイプ</param>
		/// <returns>カラーオブジェクト</returns>
		private static TjsColorType getDefaultTjsColor(DefaultColorType deftype)
		{
			BaseColorType baseType = (BaseColorType)getDefaultColor(deftype);
			TjsColorType type = new TjsColorType();
			type.WindowFront = baseType.WindowFront;
			type.WindowBack = baseType.WindowBack;
			type.SelectTextFront = baseType.SelectTextFront;
			type.SelectTextBack = baseType.SelectTextBack;
			type.VRuler = baseType.VRuler;
			type.InvalidLines = baseType.InvalidLines;
			type.CaretMarker = baseType.CaretMarker;
			type.LineNumbersFront = baseType.LineNumbersFront;
			type.LineNumbersBack = baseType.LineNumbersBack;
			type.FoldLineFront = baseType.FoldLineFront;
			type.FoldLineBack = baseType.FoldLineBack;
			type.FoldMarkerFront = baseType.FoldMarkerFront;
			type.FoldMarkerBack = baseType.FoldMarkerBack;
			type.SelectFoldLineFront = baseType.SelectFoldLineFront;
			type.SelectFoldLineBack = baseType.SelectFoldLineBack;
			type.EolMarkers = baseType.EolMarkers;
			type.SpaceMakers = baseType.SpaceMakers;
			type.TabMarkers = baseType.TabMarkers;
			type.Digits = baseType.Digits;


			switch (deftype)
			{
				case DefaultColorType.Black:
					type.Comment = Color.FromArgb(0x00, 0xC0, 0xFF);
					type.String = Color.FromArgb(0x80, 0xFF, 0x80);
					type.Regexp = Color.FromArgb(0x80, 0xFF, 0x80);
					type.Octet = Color.FromArgb(0x80, 0xFF, 0x80);
					type.PreProcessorKeyWord = Color.FromArgb(0xFF, 0xFF, 0x80);
					type.KeyWord = Color.FromArgb(0xFF, 0x80, 0x80);
					break;
				case DefaultColorType.White:
				default:
					type.Comment = Color.DarkCyan;
					type.String = Color.Green;
					type.Regexp = Color.Green;
					type.Octet = Color.Green;
					type.PreProcessorKeyWord = Color.Blue;
					type.KeyWord = Color.Red;
					break;
			}

			return type;
		}
	}
}
