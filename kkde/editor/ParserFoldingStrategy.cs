using System;
using System.Collections.Generic;
using System.Text;

using ICSharpCode.TextEditor.Document;
using kkde.parse;
using System.Diagnostics;

namespace kkde.editor
{
	/// <summary>
	/// 解析結果から折りたたみを行うクラス
	/// </summary>
	class ParserFoldingStrategy : IFoldingStrategy
	{
		List<FoldMarker> m_markerList = new List<FoldMarker>();

		/// <summary>
		/// 折りたたみマーカーリストを取得する
		/// </summary>
		/// <param name="document">ドキュメント</param>
		/// <param name="fileName">ファイルパス</param>
		/// <param name="parseInformation">解析結果</param>
		/// <returns>マーカーリスト</returns>
		public List<FoldMarker> GenerateFoldMarkers(IDocument document, string fileName, object parseInformation)
		{
			//Debug.WriteLine("GenerateFoldMarkers *START*");
			if (parseInformation == null)
			{
				//解析内容が無いときは何もしない
				m_markerList.Clear();
				return m_markerList;
			}

			if (parseInformation is KagFoldingInfo)		//KAGの結果が渡された時
			{
				KagFoldingInfo KagInfo = (KagFoldingInfo)parseInformation;
				List<FoldMarker> newMarkerList = new List<FoldMarker>();

				if (KagInfo.RgionList != null && KagInfo.RgionList.Length != 0)
				{
					newMarkerList.AddRange(getRegionFoldMarker(document, KagInfo.RgionList));
				}
				if (KagInfo.LabelList != null && KagInfo.LabelList.Length != 0)
				{
					newMarkerList.AddRange(getLabelFoldMarker(document, KagInfo.LabelList));
				}

				//Debug.WriteLine(String.Format("newCount={0}, oldCount={1}", newMarkerList.Count, m_markerList.Count));
				if (newMarkerList.Count != m_markerList.Count)
				{
					//追加・削除があったときだけセットする
					m_markerList = newMarkerList;
					
				}
			}

			return m_markerList;
		}

		/// <summary>
		/// 折りたたみ範囲指定をセットする
		/// </summary>
		/// <param name="document">ドキュメント</param>
		/// <param name="list">折りたたみ範囲指定オブジェクトリスト</param>
		private List<FoldMarker> getRegionFoldMarker(IDocument document, RegionItem[] list)
		{
			List<FoldMarker> markerList = new List<FoldMarker>();
			Stack<RegionItem> itemStack = new Stack<RegionItem>();
			foreach (RegionItem item in list)
			{
				if (item.Position == RegionItem.Pos.Start)		//開始位置
				{
					//開始位置を覚えておく
					itemStack.Push(item);
				}
				else if (item.Position == RegionItem.Pos.End)	//終了位置
				{
					if (itemStack.Count > 0)
					{
						//一番最近セットした開始位置を取得する
						RegionItem startItem = itemStack.Pop();
						if (startItem != null && item.LineNumber < document.TotalNumberOfLines)
						{
							//マークをセットする
							markerList.Add(new FoldMarker(document, startItem.LineNumber - 1, 0
								, item.LineNumber - 1, item.Text.Length - 1, FoldType.Region, startItem.Text));
						}
					}
				}
			}

			return markerList;
		}

		/// <summary>
		/// ラベル範囲指定をセットする
		/// </summary>
		/// <param name="document">ドキュメント</param>
		/// <param name="kagLabelItem">ラベルリスト</param>
		private List<FoldMarker> getLabelFoldMarker(IDocument document, KagLabelItem[] list)
		{
			List<FoldMarker> markerList = new List<FoldMarker>();
			KagLabelItem startLabel = null;
			foreach (KagLabelItem item in list)
			{
				if (startLabel != null)
				{
					if (startLabel.LineNumber < item.LineNumber)
					{
						markerList.Add(getLabelFoldMarkerFormLine(document, startLabel.Label, startLabel.LineNumber, item.LineNumber));
					}
				}

				startLabel = item;
			}

			//最後のラベルを処理する
			if (startLabel != null)
			{
				int endLine = document.TotalNumberOfLines - 1;
				if (startLabel.LineNumber < endLine - 1)	//一番下の位置の時はやらない
				{
					markerList.Add(getLabelFoldMarkerFormLine(document, startLabel.Label, startLabel.LineNumber, endLine));
				}
			}

			return markerList;
		}

		/// <summary>
		/// ラベル範囲マーカーを作成する
		/// </summary>
		/// <param name="document">ドキュメント</param>
		/// <param name="labelText">折りたたみ時に表示するラベル名</param>
		/// <param name="startLine">折りたたみ開始ラベル位置</param>
		/// <param name="endLine">次のラベル位置</param>
		/// <returns>マーカー</returns>
		private FoldMarker getLabelFoldMarkerFormLine(IDocument document, string labelText, int startLine, int endLine)
		{
			LineSegment line = document.GetLineSegment(endLine - 1);
			int lineLen = 0;
			if (line.Length != 0)
			{
				lineLen = line.Length - 1;
			}

			return new FoldMarker(document, startLine, 0, endLine - 1, lineLen, FoldType.Unspecified, labelText);
		}
	}
}
