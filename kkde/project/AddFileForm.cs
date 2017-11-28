using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using kkde.global;

namespace kkde.project
{
	/// <summary>
	/// プロジェクトノードの追加する項目一覧を表示する
	/// </summary>
	public partial class AddFileForm : Form
	{
		const string CUSTOM_FILE_NAME_KAG = "custom.ks";
		const string CUSTOM_FILE_NAME_TJS = "custom.tjs";

		string m_parentPath = "";
		string m_addPath = "";

		ListViewItem listItemKagEmpty = new ListViewItem(new string[] {
            "空のKAGシナリオファイル",
            "何も記述されていないKAGシナリオファイル"}, -1);
		ListViewItem listItemKagCustom = new ListViewItem(new string[] {
            "カスタムKAGシナリオファイル",
            "KAGシナリオ用テンプレートを使用する"}, -1);
		ListViewItem listItemTjsEmpty = new ListViewItem(new string[] {
            "空のTJSファイル",
            "何も記述されていないTJSファイル"}, -1);
		ListViewItem listItemTjsCustom = new ListViewItem(new string[] {
            "カスタムTJSファイル",
            "TJSファイル用テンプレートを使用する"}, -1);
		ListViewItem listItemScreenEmpty = new ListViewItem(new string[] {
            "空のKKDEスクリーンファイル",
            "何も追加されていないKKDEスクリーンファイル"}, -1);
		ListViewItem listItemTextEmpty = new ListViewItem(new string[] {
            "テキストファイル",
            "空のテキストファイル"}, -1);
		ListViewItem listItemDir = new ListViewItem(new string[] {
            "フォルダ",
            "空のフォルダ"}, -1);

		/// <summary>
		/// 追加したファイルのフルパス（読み取り専用）
		/// </summary>
		public string AddPath
		{
			get
			{
				return m_addPath;
			}
		}

		/// <summary>
		/// 追加したファイル名（読み取り専用）
		/// </summary>
		public string AddFileName
		{
			get
			{
				if (m_addPath == "")
				{
					return "";
				}
				return Path.GetFileName(m_addPath);
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public AddFileForm(bool addDir)
			: this("", addDir)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="parentPath">追加する親ディレクトリパス</param>
		public AddFileForm(string parentPath, bool addDir)
		{
			InitializeComponent();

			//フィールド初期化
			addListView.Items.Add(listItemKagEmpty);
			addListView.Items.Add(listItemKagCustom);
			addListView.Items.Add(listItemTjsEmpty);
			addListView.Items.Add(listItemTjsCustom);
			addListView.Items.Add(listItemScreenEmpty);
			addListView.Items.Add(listItemTextEmpty);
			if (addDir)
			{
				addListView.Items.Add(listItemDir);	//ディレクトリも追加対象とする
			}

			pathBox.Text = parentPath;
			if (pathBox.Text != "")
			{
				pathRefButton.Enabled = false;	//パスがすでに入力されているときは変更できないようにする
			}
		}

		/// <summary>
		/// OKボタンを押したとき
		/// </summary>
		private void okButton_Click(object sender, EventArgs e)
		{
			if (Directory.Exists(pathBox.Text) == false)
			{
				util.Msgbox.Warning("保存先フォルダを選択してください");
				return;
			}
			if (nameBox.Text == "")
			{
				util.Msgbox.Warning("作成する名前を入力してください");
				return;
			}
			if (isInvalidFileName(nameBox.Text) == true)
			{
				util.Msgbox.Warning("ファイル名として使用できない文字が含まれています");
				return;
			}
			if (addListView.SelectedItems.Count == 0)
			{
				util.Msgbox.Warning("追加する種類が選択されていません");
				return;
			}

			try
			{
				m_parentPath = pathBox.Text;
				addSelectFile(addListView.SelectedItems[0], nameBox.Text);
			}
			catch (ApplicationException err)
			{
				util.Msgbox.Error("ファイルの追加ができませんでした\n" + err.Message);
				m_addPath = "";
				return;
			}

			this.Close();
		}

		/// <summary>
		/// 指定したitemのファイルを作成する
		/// </summary>
		/// <param name="item">作成するファイルの種類</param>
		/// <param name="name">作成するファイル名</param>
		private void addSelectFile(ListViewItem item, string name)
		{
			m_addPath = "";
			if (item.Text == listItemKagEmpty.Text)
			{
				m_addPath = createEmptyFile(name, FileType.KrkrType.Kag);
			}
			else if (item.Text == listItemKagCustom.Text)
			{
				m_addPath = copyCustomFile(name, FileType.KrkrType.Kag);
			}
			else if (item.Text == listItemTjsEmpty.Text)
			{
				m_addPath = createEmptyFile(name, FileType.KrkrType.Tjs);
			}
			else if (item.Text == listItemTjsCustom.Text)
			{
				m_addPath = copyCustomFile(name, FileType.KrkrType.Tjs);
			}
			else if (item.Text == listItemScreenEmpty.Text)
			{
				m_addPath = createEmptyFile(name, FileType.KrkrType.Screen);
			}
			else if (item.Text == listItemTextEmpty.Text)
			{
				m_addPath = createEmptyFile(name, FileType.KrkrType.Text);
			}
			else if (item.Text == listItemDir.Text)
			{
				m_addPath = Path.Combine(m_parentPath, name);
				if (Directory.Exists(m_addPath))
				{
					throw new ApplicationException(name + " フォルダはすでに存在しています");
				}
				Directory.CreateDirectory(m_addPath);
			}
			else
			{
				throw new ApplicationException("選択した項目 " + item.Text + " は未対応です");
			}
		}

		/// <summary>
		/// 空のファイルを作成する
		/// </summary>
		/// <param name="name">ファイル名</param>
		/// <param name="ext">拡張子（ドット付き）</param>
		private string createEmptyFile(string name, FileType.KrkrType krkrType)
		{
			string path = getCreateFilePath(name, krkrType);

			File.AppendAllText(path, "");
			return path;
		}

		/// <summary>
		/// カスタムファイルをコピーする
		/// </summary>
		/// <param name="name">コピーを作成するファイル名</param>
		/// <param name="krkrType">コピーするカスタムファイルのファイルタイプ</param>
		/// <returns>作成したファイルのパス</returns>
		private string copyCustomFile(string name, FileType.KrkrType krkrType)
		{
			string path = getCreateFilePath(name, krkrType);
			string srcPath;
			switch (krkrType)
			{
				case FileType.KrkrType.Kag:
					srcPath = Path.Combine(option.ConstEnvOption.TemplateFilePath, CUSTOM_FILE_NAME_KAG);
					break;
				case FileType.KrkrType.Tjs:
					srcPath = Path.Combine(option.ConstEnvOption.TemplateFilePath, CUSTOM_FILE_NAME_TJS);
					break;
				default:
					//エラー
					throw new ApplicationException("カスタムファイルに対応したファイルタイプではありません");
			}
			if (File.Exists(srcPath) == false)
			{
				throw new ApplicationException("カスタムファイルが見つかりません");
			}

			File.Copy(srcPath, path);
			return path;
		}

		/// <summary>
		/// 作成するためのフルパスを生成する
		/// </summary>
		/// <param name="name">作成するファイル名</param>
		/// <param name="krkrType">ファイルタイプ</param>
		/// <returns>作成したフルパス</returns>
		private string getCreateFilePath(string name, FileType.KrkrType krkrType)
		{
			string ext = FileType.GetKrkrTextFileExt(krkrType);
			if (name.EndsWith(ext) != true)
			{
				//名前に拡張子が無いときはつける
				name += ext;
			}

			string path = Path.Combine(m_parentPath, name);
			if (File.Exists(path))
			{
				throw new ApplicationException(name + " ファイルはすでに存在しています。");
			}

			return path;
		}

		/// <summary>
		/// 無効なファイル名になる文字が含まれているかどうか
		/// 含まれているときtrueを返す
		/// </summary>
		/// <param name="name">調べるファイル名</param>
		/// <returns>無効な文字が含まれているときはtrueを返す</returns>
		private bool isInvalidFileName(string name)
		{
			char[] invalidChars =  Path.GetInvalidFileNameChars();

			foreach (char c in name)
			{
				foreach (char invalid in invalidChars)
				{
					if (c == invalid)
					{
						return true;
					}
				}
			}

			return false;
		}

		/// <summary>
		/// キャンセルボタンを押したとき
		/// </summary>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			//何もせずに閉じる
			m_addPath = "";
			this.Close();
		}

		/// <summary>
		/// フォルダ選択ダイアログを表示する
		/// </summary>
		private void pathRefButton_Click(object sender, EventArgs e)
		{
			if (dirPathDialog.SelectedPath == "")
			{
				if (GlobalStatus.Project != null)
				{
					dirPathDialog.SelectedPath = GlobalStatus.Project.DataFullPath;
				}
			}
			if (dirPathDialog.ShowDialog() == DialogResult.OK)
			{
				pathBox.Text = dirPathDialog.SelectedPath;
			}
		}
	}
}
