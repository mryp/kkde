using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using kkde.project;
using ICSharpCode.TextEditor.Document;
using kkde.global;
using System.IO;
using System.Collections;

namespace kkde.option
{
	public partial class TypeOptionForm : Form
	{
		#region 定数
		#endregion

		#region フィールド
		/// <summary>
		/// 設定を行うファイルタイプ
		/// </summary>
		FileType.KrkrType m_krkrType;

		/// <summary>
		/// 設定の変更を行ったかどうか
		/// </summary>
		bool m_isOptionChanged = false;

		/// <summary>
		/// フォント情報一時保持変数
		/// </summary>
		Font m_editorOptionFont;
		#endregion

		#region プロパティ
		/// <summary>
		/// 設定の変更を行ったかどうか（変更したときはtrueを返す）
		/// </summary>
		public bool IsOptionChanged
		{
			get { return m_isOptionChanged; }
		}
		#endregion

		#region 初期化
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="krkrType">設定を行うタイプ</param>
		public TypeOptionForm(FileType.KrkrType krkrType)
		{
			InitializeComponent();
			m_isOptionChanged = false;

			//画面初期化
			initEditorOptionItem();

			//タイプ画面の初期化
			typeComboBox.Items.Add(FileType.KrkrTypeToString(FileType.KrkrType.Kag));
			typeComboBox.Items.Add(FileType.KrkrTypeToString(FileType.KrkrType.Tjs));
			typeComboBox.Items.Add(FileType.KrkrTypeToString(FileType.KrkrType.Unknown));

			//現在選択するファイルタイプの設定
			m_krkrType = krkrType;
			foreach (string item in typeComboBox.Items)
			{
				if (item == FileType.KrkrTypeToString(m_krkrType))
				{
					typeComboBox.SelectedItem = FileType.KrkrTypeToString(m_krkrType);
					break;
				}
			}
		}

		/// <summary>
		/// エディタ設定関連項目の情報を初期化する
		/// </summary>
		private void initEditorOptionItem()
		{
			//対応文字コード名リストをセット
			foreach (string item in EditorOption.ENCODING_NAME_LIST)
			{
				encodingNameComboBox.Items.Add(item);
			}

			//対応改行コードリストをセット
			foreach (string item in EditorOption.LINE_TERM_NAME_LIST)
			{
				lineTerminatorNameComboBox.Items.Add(item);
			}

			//カーソル行表示リストをセット
			lineViewerStyleComboBox.Items.Add(LineViewerStyle.None);
			lineViewerStyleComboBox.Items.Add(LineViewerStyle.FullRow);

			//インデント方法リストをセット
			indentStyleComboBox.Items.Add(IndentStyle.None);
			indentStyleComboBox.Items.Add(IndentStyle.Smart);
			indentStyleComboBox.Items.Add(IndentStyle.Auto);

			//ブロックハイライト位置をセット
			bracketMatchingStyleComboBox.Items.Add(BracketMatchingStyle.After);
			bracketMatchingStyleComboBox.Items.Add(BracketMatchingStyle.Before);

			//KAGEX・タグコピータグ記号をセット
			worldExCopyTagTypeComboBox.Items.Add(KagCompletionOption.TagType.Atmark);
			worldExCopyTagTypeComboBox.Items.Add(KagCompletionOption.TagType.Bracket);
		}

		/// <summary>
		/// ファイルを読み込み画面状態を初期化する
		/// </summary>
		public void loadOption()
		{
			if (m_krkrType == FileType.KrkrType.Kag)
			{
				//カラー設定
				KagColorType type = new KagColorType();
				IHighlightingStrategy hs = HighlightingManager.Manager.FindHighlighter("KAG");
				loadBaseTypeColor(type, hs);
				loadKagTypeColor(type, (DefaultHighlightingStrategy)hs);
				colorPropertyGrid.SelectedObject = type;

				//タブ表示切り替え
				optionTabControl.TabPages.Clear();
				optionTabControl.TabPages.Add(colorTabPage);
				optionTabControl.TabPages.Add(fontTabPage);
				optionTabControl.TabPages.Add(showTabPage);
				optionTabControl.TabPages.Add(actionTabPage);
				optionTabControl.TabPages.Add(completeTabPage);
				optionTabControl.TabPages.Add(kag1TabPage);
				optionTabControl.TabPages.Add(kag2TabPage);
				optionTabControl.TabPages.Add(kagex1TabPage);
			}
			else if (m_krkrType == FileType.KrkrType.Tjs)
			{
				TjsColorType type = new TjsColorType();
				IHighlightingStrategy hs = HighlightingManager.Manager.FindHighlighter("TJS2");
				loadBaseTypeColor(type, hs);
				loadTjsTypeColor(type, (DefaultHighlightingStrategy)hs);
				colorPropertyGrid.SelectedObject = type;

				//タブ表示切り替え
				optionTabControl.TabPages.Clear();
				optionTabControl.TabPages.Add(colorTabPage);
				optionTabControl.TabPages.Add(fontTabPage);
				optionTabControl.TabPages.Add(showTabPage);
				optionTabControl.TabPages.Add(actionTabPage);
				//optionTabControl.TabPages.Add(completeTabPage);
			}
			else
			{
				BaseColorType type = new BaseColorType();
				IHighlightingStrategy hs = HighlightingManager.Manager.FindHighlighter("Default");
				loadBaseTypeColor(type, hs);
				colorPropertyGrid.SelectedObject = type;

				//タブ表示切り替え
				optionTabControl.TabPages.Clear();
				optionTabControl.TabPages.Add(colorTabPage);
				optionTabControl.TabPages.Add(fontTabPage);
				optionTabControl.TabPages.Add(showTabPage);
				optionTabControl.TabPages.Add(actionTabPage);
			}

			//エディタ設定
			EditorOption editorOption = GlobalStatus.EditorManager.GetEditorOption(m_krkrType);
			loadEditorOption(editorOption);
		}

		/// <summary>
		/// ベースとなるカラーを読み込む
		/// </summary>
		/// <param name="type">設定するカラータイプ</param>
		/// <param name="hs">読み込むカラー情報</param>
		private void loadBaseTypeColor(BaseColorType type, IHighlightingStrategy hs)
		{
			HighlightColor color;

			color = hs.GetColorFor("Default");
			type.WindowFront = color.Color;
			type.WindowBack = color.BackgroundColor;

			color = hs.GetColorFor("Selection");
			type.SelectTextFront = color.Color;
			type.SelectTextBack = color.BackgroundColor;

			color = hs.GetColorFor("VRuler");
			type.VRuler = color.Color;

			color = hs.GetColorFor("InvalidLines");
			type.InvalidLines = color.Color;

			color = hs.GetColorFor("CaretMarker");
			type.CaretMarker = color.Color;

			color = hs.GetColorFor("LineNumbers");
			type.LineNumbersFront = color.Color;
			type.LineNumbersBack = color.BackgroundColor;

			color = hs.GetColorFor("FoldLine");
			type.FoldLineFront = color.Color;
			type.FoldLineBack = color.BackgroundColor;

			color = hs.GetColorFor("FoldMarker");
			type.FoldMarkerFront = color.Color;
			type.FoldMarkerBack = color.BackgroundColor;

			color = hs.GetColorFor("SelectedFoldLine");
			type.SelectFoldLineFront = color.Color;
			type.SelectFoldLineBack = color.BackgroundColor;

			color = hs.GetColorFor("EOLMarkers");
			type.EolMarkers = color.Color;

			color = hs.GetColorFor("SpaceMarkers");
			type.SpaceMakers = color.Color;

			color = hs.GetColorFor("TabMarkers");
			type.TabMarkers = color.Color;

			color = ((DefaultHighlightingStrategy)hs).DigitColor;
			type.Digits = color.Color;
		}

		/// <summary>
		/// KAG3独自のカラー設定部分を読み込む
		/// </summary>
		/// <param name="type">設定するカラータイプ</param>
		/// <param name="hs">カラー情報</param>
		private void loadKagTypeColor(KagColorType type, DefaultHighlightingStrategy hs)
		{
			HighlightRuleSet rule;
			ArrayList spanList;
			
			//デフォルトルールセットから取得する
			rule = hs.GetRuleSet(null);
			spanList = rule.Spans;
			foreach (Span span in spanList)
			{
				switch (span.Name)
				{
					case "LineComment":
						type.Comment = span.Color.Color;
						break;
					case "TJS2Tag":
						type.TjsScript = span.Color.Color;
						break;
					case "KAG3Tag":
						type.TagName = span.Color.Color;
						break;
					case "KAG3Label":
						type.Label = span.Color.Color;
						break;
				}
			}

			//Kag3Tagルールセットから取得する
			rule = hs.FindHighlightRuleSet("KAG3TagRuleSet");
			spanList = rule.Spans;
			foreach (Span span in spanList)
			{
				switch (span.Name)
				{
					case "String":
						type.AttributeValue = span.Color.Color;
						break;
				}
			}

			//プレマークリストから属性名カラーを取得する
			List<PrevMarker> pmList = rule.PrevMarkList;
			if (pmList.Count > 0)
			{
				type.AttributeName = pmList[0].Color.Color;
			}
		}

		/// <summary>
		/// TJS独自のカラーを読み込む
		/// </summary>
		/// <param name="type">セットするカラータイプ</param>
		/// <param name="hs">情報を読み取るカラー情報</param>
		private void loadTjsTypeColor(TjsColorType type, DefaultHighlightingStrategy hs)
		{
			HighlightRuleSet rule;
			ArrayList spanList;
			HighlightColor color;

			//デフォルトルールセットから取得する
			rule = hs.GetRuleSet(null);
			spanList = rule.Spans;
			foreach (Span span in spanList)
			{
				switch (span.Name)
				{
					case "LineComment":
						type.Comment = span.Color.Color;
						break;
					case "String":
						type.String = span.Color.Color;
						break;
					case "Regexp":
						type.Regexp = span.Color.Color;
						break;
					case "Octet":
						type.Octet = span.Color.Color;
						break;
				}
			}

			//キーワードの色を取得する
			Dictionary<string, HighlightColor> keywordsList = rule.KeyWordsList;
			color = keywordsList["TJSPreProcessorWords"];
			if (color != null)
			{
				type.PreProcessorKeyWord = color.Color;
			}
			color = keywordsList["TJSKeyWords"];
			if (color != null)
			{
				type.KeyWord = color.Color;
			}
		}

		/// <summary>
		/// エディタオプション情報を画面に反映する
		/// </summary>
		/// <param name="editorOption">エディタオプション情報</param>
		private void loadEditorOption(EditorOption option)
		{
			//フォント
			updateFontPreviewBox(option.Font);
			useAntiAliasedFontCheckBox.Checked = option.UseAntiAliasedFont;
			encodingNameComboBox.SelectedItem = option.EncodingName;
			lineTerminatorNameComboBox.SelectedItem = option.LineTerminatorName;

			//表示
			showSpaceCheckBox.Checked = option.ShowSpaces;
			showWideSpaceCheckBox.Checked = option.ShowWideSpaces;
			showTabsCheckBox.Checked = option.ShowTabs;
			showEOLMarkerCheckBox.Checked = option.ShowEOLMarker;
			showInvalidLinesCheckBox.Checked = option.ShowInvalidLines;
			showMatchingBracketCheckBox.Checked = option.ShowMatchingBracket;
			showLineNumbersCheckBox.Checked = option.ShowLineNumbers;
			isIconBarVisibleCheckBox.Checked = option.IsIconBarVisible;
			enableFoldingCheckBox.Checked = option.EnableFolding;
			showHorizontalRulerCheckBox.Checked = option.ShowHorizontalRuler;
			showVerticalRulerCheckBox.Checked = option.ShowVerticalRuler;
			verticalRulerRowUpDown.Value = option.VerticalRulerRow;
			lineViewerStyleComboBox.SelectedItem = option.LineViewerStyle;

			//動作
			tabIndentUpDown.Value = option.TabIndent;
			convertTabsToSpacesCheckBox.Checked = option.ConvertTabsToSpaces;
			indentationSizeUpDown.Value = option.IndentationSize;
			indentStyleComboBox.SelectedItem = option.IndentStyle;
			allowCaretBeyondEOLCheckBox.Checked = option.AllowCaretBeyondEOL;
			createBackupCopyCheckBox.Checked = option.CreateBackupCopy;
			mouseWheelScrollDownCheckBox.Checked = option.MouseWheelScrollDown;
			mouseWheeltextZoomCheckBox.Checked = option.MouseWheelTextZoom;
			hideMouseCursorCheckBox.Checked = option.HideMouseCursor;
			cutCopyWholeLineCheckBox.Checked = option.CutCopyWholeLine;
			autoInsertCurlyBracketCheckBox.Checked = option.AutoInsertCurlyBracket;
			useCustomLineCheckBox.Checked = option.UseCustomLine;
			bracketMatchingStyleComboBox.SelectedItem = option.BracketMatchingStyle;

			//入力補完
			useCodeCompletionCheckbox.Checked = option.UseCodeCompletion;
			parseActionFileSaveCheckBox.Checked = option.ParseActionFileSave;

			//KAG1
			useAttrValueDqRegionCheckBox.Checked = option.KagCompOption.UseAttrValueDqRegion;

			zeroOverNumberListBox.Text = option.KagCompOption.ZeroOverNumberList;
			oneOverNumberListBox.Text = option.KagCompOption.OneOverNumberList;
			percentNumberListBox.Text = option.KagCompOption.PercentNumberList;
			byteNumberListBox.Text = option.KagCompOption.ByteNumberList;
			msTimeNumberListBox.Text = option.KagCompOption.MsTimeNumberList;
			realNumberListBox.Text = option.KagCompOption.RealNumberList;
			pmHundredNumberListBox.Text = option.KagCompOption.PmHundredNumberList;
			rgbNumberListBox.Text = option.KagCompOption.RgbNumberList;

			otherStringListBox.Text = option.KagCompOption.OtherStringList;
			tjsStringListBox.Text = option.KagCompOption.TjsStringList;
			fontStringListBox.Text = option.KagCompOption.FontStringList;

			//KAG2
			scenarioFileExtBox.Text = option.KagCompOption.ScenarioFileExt;
			imageFileExtBox.Text = option.KagCompOption.ImageFileExt;
			seFileExtBox.Text = option.KagCompOption.SeFileExt;
			cursorFileExtBox.Text = option.KagCompOption.CursorFileExt;
			bgmFileExtBox.Text = option.KagCompOption.BgmFileExt;
			actionFileExtBox.Text = option.KagCompOption.ActionFileExt;
			pluginFileExtBox.Text = option.KagCompOption.PluginFileExt;
			fontFileExtBox.Text = option.KagCompOption.FontFileExt;
			videoFileExtBox.Text = option.KagCompOption.VideoFileExt;

			layerMaxNumberBox.Value = option.KagCompOption.LayerMaxNumber;
			messageLayerMaxNumberBox.Value = option.KagCompOption.MessageLayerMaxNumber;
			seBufferMaxNumberBox.Value = option.KagCompOption.SeBufferMaxNumber;
			videoBufferMaxNumberBox.Value = option.KagCompOption.VideoBufferMaxNumber;

			//KAGEX
			isInsertedTagCopyExWorldExCheckBox.Checked = option.KagCompOption.IsInsertedTagCopyExWorldEx;
			isAddedTagSignWorldExCheckBox.Checked = option.KagCompOption.IsAddedTagSignWorldEx;
			isAddedMsgTagWorldExCheckBox.Checked = option.KagCompOption.IsAddedMsgTagWorldEx;
			if (option.KagCompOption.WorldExDoubleDef == KagCompletionOption.WorldExViewDCOption.Preview)
			{
				worldExDCActionPreviewRadioButton.Checked = true;
				worldExDCActionPreviewExRadioButton.Checked = false;
			}
			else
			{
				worldExDCActionPreviewRadioButton.Checked = false;
				worldExDCActionPreviewExRadioButton.Checked = true;
			}
			worldExSearchPathCharTextBox.Text = option.KagCompOption.WorldExSearchPathChar;
			worldExSearchPathEventTextBox.Text = option.KagCompOption.WorldExSearchPathEvent;
			worldExSearchPathStageTextBox.Text = option.KagCompOption.WorldExSearchPathSe;
			worldExSearchPathBgmTextBox.Text = option.KagCompOption.WorldExSearchPathBgm;
			worldExSearchPathSeTextBox.Text = option.KagCompOption.WorldExSearchPathSe;
			worldExCopyTagTypeComboBox.SelectedItem = option.KagCompOption.WorldExCopyTagType;
		}

		/// <summary>
		/// フォント名表示ボックスを更新する
		/// </summary>
		/// <param name="font"></param>
		private void updateFontPreviewBox(Font font)
		{
			m_editorOptionFont = font;
			fontPreviewBox.Text = String.Format("Name={0}, Size={1}", font.Name, font.Size);
		}
		#endregion

		#region 全体イベント
		/// <summary>
		/// 設定を破棄して閉じる
		/// </summary>
		private void closeButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// 設定を保存して閉じる
		/// </summary>
		private void saveButton_Click(object sender, EventArgs e)
		{
			try
			{
				//設定をファイルに保存
				saveColorOptionFile(m_krkrType);
				saveEditorOptionFile(m_krkrType);

				//設定を反映する
				HighlightingManager.Manager.ReloadSyntaxModes();
				GlobalStatus.EditorManager.SetOptionAll();
			}
			catch (Exception err)
			{
				util.Msgbox.Error("設定の保存に失敗しました\n" + err.Message);
				return;	//閉じない
			}

			m_isOptionChanged = true;
			this.Close();
		}


		/// <summary>
		/// 編集タイプを変更する
		/// </summary>
		private void typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			string selectText = (string)typeComboBox.SelectedItem;
			if (selectText == "")
			{
				//何も選択していないので何もしない
				return;
			}

			m_krkrType = FileType.StringToKrkrType(selectText);
			loadOption();
		}
		#endregion

		#region カラー設定ファイル保存
		/// <summary>
		/// カラー設定ファイルを保存する
		/// </summary>
		/// <param name="type"></param>
		private void saveColorOptionFile(FileType.KrkrType type)
		{
			bool ret = TypeColorFile.SaveFile(type, (BaseColorType)colorPropertyGrid.SelectedObject);
			if (ret == false)
			{
				throw new ApplicationException("カラー設定ファイルの保存に失敗しました");
			}
		}
		#endregion

		#region エディタ設定ファイル保存
		private void saveEditorOptionFile(FileType.KrkrType m_krkrType)
		{
			EditorOption option = GlobalStatus.EditorManager.GetEditorOption(m_krkrType);
			if (option == null)
			{
				throw new ApplicationException("設定情報の取得に失敗しました");
			}

			//フォント
			option.FontName = m_editorOptionFont.Name;
			option.FontSize = (int)Math.Round(m_editorOptionFont.Size);
			option.UseAntiAliasedFont = useAntiAliasedFontCheckBox.Checked;
			option.EncodingName = (string)encodingNameComboBox.SelectedItem;
			option.LineTerminatorName = (string)lineTerminatorNameComboBox.SelectedItem;

			//表示
			option.ShowSpaces = showSpaceCheckBox.Checked;
			option.ShowWideSpaces = showWideSpaceCheckBox.Checked;
			option.ShowTabs = showTabsCheckBox.Checked;
			option.ShowEOLMarker = showEOLMarkerCheckBox.Checked;
			option.ShowInvalidLines = showInvalidLinesCheckBox.Checked;
			option.ShowMatchingBracket = showMatchingBracketCheckBox.Checked;
			option.ShowLineNumbers = showLineNumbersCheckBox.Checked;
			option.IsIconBarVisible = isIconBarVisibleCheckBox.Checked;
			option.EnableFolding = enableFoldingCheckBox.Checked;
			option.ShowHorizontalRuler = showHorizontalRulerCheckBox.Checked;
			option.ShowVerticalRuler = showVerticalRulerCheckBox.Checked;
			option.VerticalRulerRow = (int)verticalRulerRowUpDown.Value;
			option.LineViewerStyle = (LineViewerStyle)lineViewerStyleComboBox.SelectedItem;

			//動作
			option.TabIndent = (int)tabIndentUpDown.Value;
			option.ConvertTabsToSpaces = convertTabsToSpacesCheckBox.Checked;
			option.IndentationSize = (int)indentationSizeUpDown.Value;
			option.IndentStyle = (IndentStyle)indentStyleComboBox.SelectedItem;
			option.AllowCaretBeyondEOL = allowCaretBeyondEOLCheckBox.Checked;
			option.CreateBackupCopy = createBackupCopyCheckBox.Checked;
			option.MouseWheelScrollDown = mouseWheelScrollDownCheckBox.Checked;
			option.MouseWheelTextZoom = mouseWheeltextZoomCheckBox.Checked;
			option.HideMouseCursor = hideMouseCursorCheckBox.Checked;
			option.CutCopyWholeLine = cutCopyWholeLineCheckBox.Checked;
			option.AutoInsertCurlyBracket = autoInsertCurlyBracketCheckBox.Checked;
			option.UseCustomLine = useCustomLineCheckBox.Checked;
			option.BracketMatchingStyle = (BracketMatchingStyle)bracketMatchingStyleComboBox.SelectedItem;

			//入力補完
			option.UseCodeCompletion = useCodeCompletionCheckbox.Checked;
			option.ParseActionFileSave = parseActionFileSaveCheckBox.Checked;

			//KAG1
			option.KagCompOption.UseAttrValueDqRegion = useAttrValueDqRegionCheckBox.Checked;

			option.KagCompOption.ZeroOverNumberList = zeroOverNumberListBox.Text;
			option.KagCompOption.OneOverNumberList = oneOverNumberListBox.Text;
			option.KagCompOption.PercentNumberList = percentNumberListBox.Text;
			option.KagCompOption.ByteNumberList = byteNumberListBox.Text;
			option.KagCompOption.MsTimeNumberList = msTimeNumberListBox.Text;
			option.KagCompOption.RealNumberList = realNumberListBox.Text;
			option.KagCompOption.PmHundredNumberList = pmHundredNumberListBox.Text;
			option.KagCompOption.RgbNumberList = rgbNumberListBox.Text;

			option.KagCompOption.OtherStringList = otherStringListBox.Text;
			option.KagCompOption.TjsStringList = tjsStringListBox.Text;
			option.KagCompOption.FontStringList = fontStringListBox.Text;

			//KAG2
			option.KagCompOption.ScenarioFileExt = scenarioFileExtBox.Text;
			option.KagCompOption.ImageFileExt = imageFileExtBox.Text;
			option.KagCompOption.SeFileExt = seFileExtBox.Text;
			option.KagCompOption.CursorFileExt = cursorFileExtBox.Text;
			option.KagCompOption.BgmFileExt = bgmFileExtBox.Text;
			option.KagCompOption.ActionFileExt = actionFileExtBox.Text;
			option.KagCompOption.PluginFileExt = pluginFileExtBox.Text;
			option.KagCompOption.FontFileExt = fontFileExtBox.Text;
			option.KagCompOption.VideoFileExt = videoFileExtBox.Text;

			option.KagCompOption.LayerMaxNumber = (int)layerMaxNumberBox.Value;
			option.KagCompOption.MessageLayerMaxNumber = (int)messageLayerMaxNumberBox.Value;
			option.KagCompOption.SeBufferMaxNumber = (int)seBufferMaxNumberBox.Value;
			option.KagCompOption.VideoBufferMaxNumber = (int)videoBufferMaxNumberBox.Value;

			//KAGEX
			option.KagCompOption.IsInsertedTagCopyExWorldEx = isInsertedTagCopyExWorldExCheckBox.Checked;
			option.KagCompOption.IsAddedTagSignWorldEx = isAddedTagSignWorldExCheckBox.Checked;
			option.KagCompOption.IsAddedMsgTagWorldEx = isAddedMsgTagWorldExCheckBox.Checked;
			option.KagCompOption.WorldExSearchPathChar = worldExSearchPathCharTextBox.Text;
			option.KagCompOption.WorldExSearchPathEvent = worldExSearchPathEventTextBox.Text;
			option.KagCompOption.WorldExSearchPathSe = worldExSearchPathStageTextBox.Text;
			option.KagCompOption.WorldExSearchPathBgm = worldExSearchPathBgmTextBox.Text;
			option.KagCompOption.WorldExSearchPathSe = worldExSearchPathSeTextBox.Text;
			option.KagCompOption.WorldExCopyTagType = (KagCompletionOption.TagType)worldExCopyTagTypeComboBox.SelectedItem;
			if (worldExDCActionPreviewRadioButton.Checked)
			{
				option.KagCompOption.WorldExDoubleDef = KagCompletionOption.WorldExViewDCOption.Preview;
			}
			else if (worldExDCActionPreviewExRadioButton.Checked)
			{
				option.KagCompOption.WorldExDoubleDef = KagCompletionOption.WorldExViewDCOption.PreviewEx;
			}

			//ファイルに保存する
			string fileName = GlobalStatus.EditorManager.GetEditorOptionFilePath(m_krkrType);
			EditorOption.SaveFile(fileName, option);
		}
		#endregion

		#region カラー設定イベント
		/// <summary>
		/// デフォルト色をセットする
		/// </summary>
		private void colorInitDefButton_Click(object sender, EventArgs e)
		{
			colorPropertyGrid.SelectedObject = TypeColorFile.GetDefault(m_krkrType, TypeColorFile.DefaultColorType.White);
		}

		/// <summary>
		/// デフォルト（黒）をセットする
		/// </summary>
		private void colorInitBlackButton_Click(object sender, EventArgs e)
		{
			colorPropertyGrid.SelectedObject = TypeColorFile.GetDefault(m_krkrType, TypeColorFile.DefaultColorType.Black);
		}
		#endregion

		#region エディタ設定イベント
		/// <summary>
		/// フォント選択ダイアログを表示しフォント設定を行う
		/// </summary>
		private void fontSelectButton_Click(object sender, EventArgs e)
		{
			editorFontDialog.Font = m_editorOptionFont;
			if (editorFontDialog.ShowDialog() == DialogResult.OK)
			{
				updateFontPreviewBox(editorFontDialog.Font);
			}
		}

		/// <summary>
		/// フォント設定を初期化する
		/// </summary>
		private void initFontButton_Click(object sender, EventArgs e)
		{
			//フォント設定はタイプによらず共通
			EditorOption option = EditorOption.GetDefault(FileType.KrkrType.Unknown);
			updateFontPreviewBox(new Font(option.FontName, option.FontSize));
			useAntiAliasedFontCheckBox.Checked = option.UseAntiAliasedFont;
			encodingNameComboBox.SelectedItem = option.EncodingName;
			lineTerminatorNameComboBox.SelectedItem = option.LineTerminatorName;
		}

		/// <summary>
		/// 表示設定を初期化する
		/// </summary>
		private void initDispButton_Click(object sender, EventArgs e)
		{
			EditorOption option = EditorOption.GetDefault(m_krkrType);

			showSpaceCheckBox.Checked = option.ShowSpaces;
			showWideSpaceCheckBox.Checked = option.ShowWideSpaces;
			showTabsCheckBox.Checked = option.ShowTabs;
			showEOLMarkerCheckBox.Checked = option.ShowEOLMarker;
			showInvalidLinesCheckBox.Checked = option.ShowInvalidLines;
			showMatchingBracketCheckBox.Checked = option.ShowMatchingBracket;
			showLineNumbersCheckBox.Checked = option.ShowLineNumbers;
			isIconBarVisibleCheckBox.Checked = option.IsIconBarVisible;
			enableFoldingCheckBox.Checked = option.EnableFolding;
			showHorizontalRulerCheckBox.Checked = option.ShowHorizontalRuler;
			showVerticalRulerCheckBox.Checked = option.ShowVerticalRuler;
			verticalRulerRowUpDown.Value = option.VerticalRulerRow;
			lineViewerStyleComboBox.SelectedItem = option.LineViewerStyle;
		}

		/// <summary>
		/// 動作設定を初期化する
		/// </summary>
		private void initActionButton_Click(object sender, EventArgs e)
		{
			EditorOption option = EditorOption.GetDefault(m_krkrType);

			tabIndentUpDown.Value = option.TabIndent;
			convertTabsToSpacesCheckBox.Checked = option.ConvertTabsToSpaces;
			indentationSizeUpDown.Value = option.IndentationSize;
			indentStyleComboBox.SelectedItem = option.IndentStyle;
			allowCaretBeyondEOLCheckBox.Checked = option.AllowCaretBeyondEOL;
			createBackupCopyCheckBox.Checked = option.CreateBackupCopy;
			mouseWheelScrollDownCheckBox.Checked = option.MouseWheelScrollDown;
			mouseWheeltextZoomCheckBox.Checked = option.MouseWheelTextZoom;
			hideMouseCursorCheckBox.Checked = option.HideMouseCursor;
			cutCopyWholeLineCheckBox.Checked = option.CutCopyWholeLine;
			autoInsertCurlyBracketCheckBox.Checked = option.AutoInsertCurlyBracket;
			useCustomLineCheckBox.Checked = option.UseCustomLine;
			bracketMatchingStyleComboBox.SelectedItem = option.BracketMatchingStyle;
		}
		#endregion

		#region KAG設定イベント
		/// <summary>
		/// KAG入力補完リストを指定したテキストボックスにセットする
		/// </summary>
		/// <param name="textBox">入出力を行うテキストボックス</param>
		private void inputKagCompletionListItem(TextBox textBox)
		{
			KagCompletionListInputBox box = new KagCompletionListInputBox(textBox.Text);
			if (box.ShowDialog() == DialogResult.OK)
			{
				textBox.Text = box.InputText;
			}
		}

		/// <summary>
		/// 0以上の数値を入力する
		/// </summary>
		private void zeroOverNumberListRefButton_Click(object sender, EventArgs e)
		{
			inputKagCompletionListItem(zeroOverNumberListBox);
		}

		/// <summary>
		/// 1以上の数値を入力する
		/// </summary>
		private void oneOverNumberListRefButton_Click(object sender, EventArgs e)
		{
			inputKagCompletionListItem(oneOverNumberListBox);
		}

		/// <summary>
		/// 0～100の数値を入力する
		/// </summary>
		private void percentNumberListRefButton_Click(object sender, EventArgs e)
		{
			inputKagCompletionListItem(percentNumberListBox);
		}

		/// <summary>
		/// 0～255の数値を入力する
		/// </summary>
		private void byteNumberListRefButton_Click(object sender, EventArgs e)
		{
			inputKagCompletionListItem(byteNumberListBox);
		}

		/// <summary>
		/// 0～100の数値を入力する
		/// </summary>
		private void pmHundredNumberListRefButton_Click(object sender, EventArgs e)
		{
			inputKagCompletionListItem(pmHundredNumberListBox);
		}

		/// <summary>
		/// 実数値を入力する
		/// </summary>
		private void realNumberListRefButton_Click(object sender, EventArgs e)
		{
			inputKagCompletionListItem(realNumberListBox);
		}

		/// <summary>
		/// ミリ秒時間を入力する
		/// </summary>
		private void msTimeNumberListRefButton_Click(object sender, EventArgs e)
		{
			inputKagCompletionListItem(msTimeNumberListBox);
		}

		/// <summary>
		/// RGB数値を入力する
		/// </summary>
		private void rgbNumberListRefButton_Click(object sender, EventArgs e)
		{
			inputKagCompletionListItem(rgbNumberListBox);
		}

		/// <summary>
		/// フォント文字列を入力する
		/// </summary>
		private void fontStringListRefButton_Click(object sender, EventArgs e)
		{
			inputKagCompletionListItem(fontStringListBox);
		}

		/// <summary>
		/// TJS式文字列を入力する
		/// </summary>
		private void tjsStringListRefButton_Click(object sender, EventArgs e)
		{
			inputKagCompletionListItem(tjsStringListBox);
		}

		/// <summary>
		/// 任意の文字列を入力する
		/// </summary>
		private void otherStringListRefButton_Click(object sender, EventArgs e)
		{
			inputKagCompletionListItem(otherStringListBox);
		}

		/// <summary>
		/// シナリオファイル拡張子を入力する
		/// </summary>
		private void scenarioFileExtRefButton_Click(object sender, EventArgs e)
		{
			inputKagCompletionListItem(scenarioFileExtBox);
		}

		/// <summary>
		/// 画像ファイル拡張子を入力する
		/// </summary>
		private void imageFileExtRefButton_Click(object sender, EventArgs e)
		{
			inputKagCompletionListItem(imageFileExtBox);
		}

		/// <summary>
		/// SEファイル拡張子を入力する
		/// </summary>
		private void seFileExtRefButton_Click(object sender, EventArgs e)
		{
			inputKagCompletionListItem(seFileExtBox);
		}

		/// <summary>
		/// BGMファイル拡張子を入力する
		/// </summary>
		private void bgmFileExtRefButton_Click(object sender, EventArgs e)
		{
			inputKagCompletionListItem(bgmFileExtBox);
		}

		/// <summary>
		/// ムービーファイル拡張子を入力する
		/// </summary>
		private void videoFileExtRefButton_Click(object sender, EventArgs e)
		{
			inputKagCompletionListItem(videoFileExtBox);
		}

		/// <summary>
		/// プラグインファイル拡張子を入力する
		/// </summary>
		private void pluginFileExtRefButton_Click(object sender, EventArgs e)
		{
			inputKagCompletionListItem(pluginFileExtBox);
		}

		/// <summary>
		/// フォントファイル拡張子を入力する
		/// </summary>
		private void fontFileExtRefButton_Click(object sender, EventArgs e)
		{
			inputKagCompletionListItem(fontFileExtBox);
		}

		/// <summary>
		/// 領域定義ファイル拡張子を入力する
		/// </summary>
		private void actionFileExtRefButton_Click(object sender, EventArgs e)
		{
			inputKagCompletionListItem(actionFileExtBox);
		}

		/// <summary>
		/// カーソルファイル拡張子を入力する
		/// </summary>
		private void cursorFileExtRefButton_Click(object sender, EventArgs e)
		{
			inputKagCompletionListItem(cursorFileExtBox);
		}
		#endregion

	}
}
