using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Windows.Forms;

namespace TagsXmlMaker
{
	/// <summary>
	/// kagtags.xmlファイル処理クラス
	/// </summary>
	public class KagTagsFile
	{
		string m_filePath;
		TreeNode rootNode = new TreeNode("tdb");

		/// <summary>
		/// 読み込んだノードのルートを取得する
		/// </summary>
		public TreeNode RootNode
		{
			get { return rootNode; }
		}

		/// <summary>
		/// ファイルパス
		/// </summary>
		public string FilePath
		{
			get { return m_filePath; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="filePath">ファイルパス</param>
		public KagTagsFile(string filePath)
		{
			m_filePath = filePath;
			LoadFromXmlFile(m_filePath);
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
		public bool LoadFromXmlFile(string filePath)
		{
			if (File.Exists(filePath) == false)
			{
				Debug.WriteLine("指定されたファイルが存在しません");
				return false;
			}

			//XMLファイルを読み込み
			m_filePath = filePath;
			XmlDocument doc = new XmlDocument();
			doc.Load(m_filePath);
			XmlNodeList nodeList = doc.GetElementsByTagName("tag");
			if (nodeList == null)
			{
				Debug.WriteLine("指定されたXMLファイルが不正です");
				return false;
			}

			//ノード読み込み
			rootNode.Nodes.Clear();
			foreach (XmlElement tagNode in nodeList)
			{
				//タグノード読み込み
				TreeNode tag = readTagNode(tagNode);
				if (tag == null)
				{
					//取得できなかった時、またはすでに登録済みの場合は登録しない
					continue;
				}

				rootNode.Nodes.Add(tag);
			}

			return true;
		}

		/// <summary>
		/// タグノードを読み込む
		/// </summary>
		/// <param name="tagElement">タグエレメント</param>
		private TreeNode readTagNode(XmlElement tagElement)
		{
			TreeNode tagNode = new TreeNode();
			KagTag tag = new KagTag();
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
						tag.Remarks = convertReadString(node.InnerXml);
						break;
					case "attr":
						TreeNode attrNode = readTagAttrNode(node);
						if (attrNode == null)
						{
							//取得できなかった時、またはすでに登録済みの場合は登録しない
							break;
						}
						tagNode.Nodes.Add(attrNode);
						break;
				}
			}

			tagNode.Text = tag.Name;
			tagNode.Tag = tag;
			return tagNode;
		}

		/// <summary>
		/// タグ属性ノードを読み込む
		/// </summary>
		/// <param name="attrElement">タグ属性エレメント</param>
		private TreeNode readTagAttrNode(XmlElement attrElement)
		{
			TreeNode attrNode = new TreeNode();
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
						attr.Required = node.InnerText;
						break;
					case "attrformat":
						attr.Format = convertReadString(node.InnerXml);
						break;
					case "attrinfo":
						attr.Info = node.InnerText;
						break;
				}
			}

			attrNode.Text = attr.Name;
			attrNode.Tag = attr;
			return attrNode;
		}

		/// <summary>
		/// ツリーノードからファイルを保存する
		/// </summary>
		/// <param name="node">"tdb"ノード</param>
		public void SaveFromTreeNode(TreeNode node, string filePath)
		{
			if (node == null || filePath == "")
			{
				Debug.WriteLine("保存できるノードがありません");
				return;
			}

			m_filePath = filePath;
			using (FileStream fs = new FileStream(m_filePath, FileMode.Create))
			{
				using (XmlTextWriter xw = new XmlTextWriter(fs, Encoding.UTF8))
				{
					xw.Formatting = Formatting.Indented;

					xw.WriteStartDocument();
					xw.WriteStartElement("tdb");

					foreach (TreeNode tagNode in node.Nodes)
					{
						KagTag tag = tagNode.Tag as KagTag;
						if (tag == null)
						{
							continue;
						}

						xw.WriteStartElement("tag");
						xw.WriteAttributeString("id", String.Format("tag_{0}", tag.Name));

						writeElement(xw, "tagname", tag.Name);
						writeElement(xw, "group", tag.Group);
						writeElement(xw, "tagshortinfo", tag.ShortInfo);
						writeElement(xw, "tagremarks", tag.Remarks);

						foreach (TreeNode attrNode in tagNode.Nodes)
						{
							KagTagAttr attr = attrNode.Tag as KagTagAttr;
							if (attr == null)
							{
								continue;
							}

							xw.WriteStartElement("attr");
							xw.WriteAttributeString("id", String.Format("attr_{0}_{1}", tag.Name, attr.Name));

							writeElement(xw, "attrname", attr.Name);
							writeElement(xw, "attrshortinfo", attr.ShortInfo);
							writeElement(xw, "attrrequired", attr.Required);
							writeElement(xw, "attrformat", attr.Format);
							writeElement(xw, "attrinfo", attr.Info);

							xw.WriteEndElement();
						}

						xw.WriteEndElement();
					}

					xw.WriteEndElement();
					xw.WriteEndDocument();
				}
			}
		}

		/// <summary>
		/// 単純エレメントを書き込む
		/// 例：<name>value</name>
		/// </summary>
		/// <param name="xw"></param>
		/// <param name="elementName"></param>
		/// <param name="value"></param>
		private void writeElement(XmlTextWriter xw, string elementName, string value)
		{
			xw.WriteStartElement(elementName);
			xw.WriteRaw(convertWriteString(value));
			xw.WriteEndElement();
		}

		/// <summary>
		/// 書き込み用文字列に変換する
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		private string convertWriteString(string text)
		{
			//エンティティを変換する（<, >は変換しない）
			text = text.Replace("&", "&amp;");
			text = text.Replace("\"", "&quot;");
			text = text.Replace("'", "&apos;");
			text = text.Replace("\r\n", "<br />");
			text = text.Replace("\n", "<br />");

			return text;
		}

		/// <summary>
		/// 読み込みよう文字列に変換する
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		private string convertReadString(string text)
		{
			text = text.Replace("<br />", "\r\n");
			text = text.Replace("<br>", "\r\n");

			return text;
		}
	}
}
