using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.editor
{
	/// <summary>
	/// センターに表示するドックフォーム
	/// </summary>
	public interface IEditorDocContent
	{
		String FileName
		{
			get;
		}

		bool IsClosed
		{
			get;
		}

		void LoadFile(string fileName);

		void SaveFile(string fileName);


		void ActivateForm();

		void CloseForm();
	}
}
