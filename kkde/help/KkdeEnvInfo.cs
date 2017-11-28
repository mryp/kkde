using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Management;
using System.Text.RegularExpressions;

namespace kkde.help
{
	/// <summary>
	/// KKDEの動作している環境情報
	/// </summary>
	public class KkdeEnvInfo
	{
		/// <summary>
		/// KKDEのバージョンを取得する
		/// </summary>
		public static string Version
		{
			get
			{
				return String.Format("KiriKiri Development Environment version {0}", Application.ProductVersion);
			}
		}

		public static string OSVersion
		{
			get
			{
				return Environment.OSVersion.ToString();
			}
		}

		/// <summary>
		/// OSのバージョンを取得する
		/// </summary>
		public static string OSVersionFromWMI
		{
			get
			{
				try
				{
					ManagementClass mc = new System.Management.ManagementClass("Win32_OperatingSystem");
					ManagementObjectCollection moc = mc.GetInstances();
					foreach (ManagementObject mo in moc)
					{
						return String.Format("{0} {1}", mo["Caption"].ToString().Trim(), mo["Version"]);
					}
				}
				catch
				{
					//何もしない
				}

				return "不明なOS";
			}
		}

		/// <summary>
		/// 吉里吉里のバージョンを取得する
		/// </summary>
		public static string KrkrVersion
		{
			get
			{
				string krkr = "吉里吉里 SDK version ";
				if (File.Exists(global.GlobalStatus.Project.ExeFullPath))
				{
					FileVersionInfo vi = FileVersionInfo.GetVersionInfo(global.GlobalStatus.Project.ExeFullPath);
					krkr += vi.FileVersion;
				}
				else
				{
					krkr += "不明";
				}

				return krkr;
			}
		}

		/// <summary>
		/// .NET Frameworkのバージョンを取得する
		/// </summary>
		public static string DotnetVersion
		{
			get
			{
				return String.Format(".NET Framework version {0}", Environment.Version);
			}
		}

		/// <summary>
		/// CPUの種類とクロックを取得する
		/// </summary>
		public static string CPUVersion
		{
			get
			{
				try
				{
					ManagementClass mc = new System.Management.ManagementClass("Win32_Processor");
					ManagementObjectCollection moc = mc.GetInstances();
					foreach (ManagementObject mo in moc)
					{
						string info = String.Format("{0} ({1:#,##0} MHz)", mo["Name"], mo["MaxClockSpeed"]);
						
						return Regex.Replace(info, @"\s+", @" ");	//連続空白を削除する
					}
				}
				catch
				{
					//何もしない
				}

				return "不明CPU";
			}
		}

		/// <summary>
		/// 物理メモリサイズを取得する
		/// </summary>
		public static string MemorySize
		{
			get
			{
				try
				{
					ManagementClass mc = new System.Management.ManagementClass("Win32_OperatingSystem");
					ManagementObjectCollection moc = mc.GetInstances();
					foreach (ManagementObject mo in moc)
					{
						return String.Format("物理メモリ:{0,12:#,##0},000 Byte", mo["TotalVisibleMemorySize"]);
					}

				}
				catch
				{
					//何もしない
				}

				return "不明 byte";
			}
		}

		/// <summary>
		/// 使用メモリサイズを取得する
		/// </summary>
		public static string UseMemorySize
		{
			get
			{
				return String.Format("使用メモリ:{0,16:#,##0} Byte", Environment.WorkingSet);
			}
		}

		/// <summary>
		/// GCメモリサイズを取得する
		/// </summary>
		public static string GcMemorySize
		{
			get
			{
				return String.Format("GCメモリ:{0,18:#,##0} Byte", GC.GetTotalMemory(false));
			}
		}
	}
}
