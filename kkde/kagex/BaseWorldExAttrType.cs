using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace kkde.kagex
{
	/// <summary>
	/// ワールド拡張属性のベース型
	/// </summary>
	[DefaultPropertyAttribute("Afx")]	//デフォルトで選択状態にするプロパティ
	public class BaseWorldExAttrType
	{
		#region フィールド
		private string m_name;

		//位置関連
		private string m_type;
		private WorldExPotision m_opacity;
		private WorldExPotision m_rotate;
		private WorldExPotision m_zoom;
		private WorldExPotision m_afx;
		private WorldExPotision m_afy;
		private string m_reset;
		private WorldExPotision m_xpos;
		private WorldExPotision m_ypos;

		//時間関連
		private string m_time;

		//動作関連
		private string m_accel;
		private string m_action;
		private string m_trans;
		private string m_stopaction;
		private string m_stoptrans;
		private string m_sync;
		private string m_nosync;
		private string m_nowait;
		private string m_fade;

		//表示関連
		private string m_visible;
		private string m_show;
		private string m_hide;

		//色関連
		private string m_grayscale;
		private string m_rgamma;
		private string m_ggamma;
		private string m_bgamma;
		private string m_resetcolor;
		#endregion

		#region 操作メソッド
		public BaseWorldExAttrType()
		{
			ResetAll();
		}

		/// <summary>
		/// 属性値を全て初期化する
		/// </summary>
		public virtual void ResetAttr()
		{
			//リストの初期化
			m_type = "";
			m_opacity = new WorldExPotision();
			m_rotate = new WorldExPotision();
			m_zoom = new WorldExPotision();
			m_afx = new WorldExPotision();
			m_afy = new WorldExPotision();
			m_reset = "";
			m_xpos = new WorldExPotision();
			m_ypos = new WorldExPotision();

			m_time = "";

			m_accel = "";
			m_action = "";
			m_trans = "";
			m_stopaction = "";
			m_stoptrans = "";
			m_sync = "";
			m_nosync = "";
			m_nowait = "";
			m_fade = "";

			m_visible = "";
			m_show = "";
			m_hide = "";

			m_grayscale = "";
			m_rgamma = "";
			m_ggamma = "";
			m_bgamma = "";
			m_resetcolor = "";
		}

		/// <summary>
		/// すべて初期化する
		/// </summary>
		public virtual void ResetAll()
		{
			m_name = "";

			ResetAttr();
		}

		/// <summary>
		/// オブジェクト名を返す
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return "WorldEX共通オブジェクト: " + m_name;
		}

		/// <summary>
		/// オブジェクト内容ををKAG属性としてとして出力する
		/// </summary>
		/// <returns></returns>
		public virtual string ToKagTagAttr()
		{
			string attr = "";
			if (m_type != "")
			{
				attr += String.Format("type=\"{0}\" ", m_type);
			}
			if (m_opacity.ToString() != "")
			{
				attr += String.Format("opacity={0} ", m_opacity);
			}
			if (m_rotate.ToString() != "")
			{
				attr += String.Format("rotate={0} ", m_rotate);
			}
			if (m_zoom.ToString() != "")
			{
				attr += String.Format("zoom={0} ", m_zoom);
			}
			if (m_afx.ToString() != "")
			{
				attr += String.Format("afx={0} ", m_afx);
			}
			if (m_afy.ToString() != "")
			{
				attr += String.Format("afy={0} ", m_afy);
			}
			if (m_reset != "")
			{
				attr += String.Format("reset ");
			}
			if (m_xpos.ToString() != "")
			{
				attr += String.Format("xpos={0} ", m_xpos);
			}
			if (m_ypos.ToString() != "")
			{
				attr += String.Format("ypos={0} ", m_ypos);
			}
			if (m_time != "")
			{
				attr += String.Format("time={0} ", m_time);
			}

			if (m_accel != "")
			{
				attr += String.Format("accel={0} ", m_accel);
			}
			if (m_action != "")
			{
				attr += String.Format("action=\"{0}\" ", m_action);
			}
			if (m_trans != "")
			{
				attr += String.Format("trans=\"{0}\" ", m_trans);
			}
			if (m_stopaction != "")
			{
				attr += String.Format("stopaction ");
			}
			if (m_stoptrans != "")
			{
				attr += String.Format("stoptrans ");
			}
			if (m_sync != "")
			{
				attr += String.Format("sync ");
			}
			if (m_nosync != "")
			{
				attr += String.Format("nosync ");
			}
			if (m_nowait != "")
			{
				attr += String.Format("nowait ");
			}
			if (m_fade != "")
			{
				attr += String.Format("fade ");
			}
			if (m_visible != "")
			{
				attr += String.Format("visible={0} ", m_visible);
			}
			if (m_show != "")
			{
				attr += String.Format("show ");
			}
			if (m_hide != "")
			{
				attr += String.Format("hide ");
			}
			if (m_grayscale != "")
			{
				attr += String.Format("grayscale={0} ", m_grayscale);
			}
			if (m_rgamma != "")
			{
				attr += String.Format("rgamma={0} ", m_rgamma);
			}
			if (m_ggamma != "")
			{
				attr += String.Format("ggamma={0} ", m_ggamma);
			}
			if (m_bgamma != "")
			{
				attr += String.Format("bgamma={0} ", m_bgamma);
			}
			if (m_resetcolor != "")
			{
				attr += String.Format("resetcolor ");
			}

			return attr;
		}
		#endregion

		/// <summary>
		/// この属性の名前（PropertyGridには登録しない）
		/// </summary>
		[Browsable(false)]
		public string Name
		{
			get { return m_name; }
			set { m_name = value; }
		}

		[Category("位置")]
		[Description("合成モードを指定します")]
		[TypeConverter(typeof(PropertyGridCompModeConverter))]
		public string Type
		{
			get { return m_type; }
			set { m_type = value; }
		}

		[Category("位置")]
		[Description("不透明度（パーセント）を指定します（0 ～ 255）\n初期化・相対指定有効")]
		[TypeConverter(typeof(WorldExPotisionObjectConverter))]
		public WorldExPotision Opacity
		{
			get { return m_opacity; }
			set { m_opacity = value; }
		}

		[Category("位置")]
		[Description("回転角度（度）を指定します（0 ～ 360）\n初期化・相対指定有効")]
		[TypeConverter(typeof(WorldExPotisionObjectConverter))]
		public WorldExPotision Rotate
		{
			get { return m_rotate; }
			set { m_rotate = value; }
		}

		[Category("位置")]
		[Description("拡大率（パーセント）を指定します(0 ～ 400）\n初期化・相対指定有効")]
		[TypeConverter(typeof(WorldExPotisionObjectConverter))]
		public WorldExPotision Zoom
		{
			get { return m_zoom; }
			set { m_zoom = value; }
		}

		[Category("位置")]
		[Description("回転・拡大の原点X座標を指定します（-2000 ～ 2000）\n初期化・相対指定有効")]
		[TypeConverter(typeof(WorldExPotisionObjectConverter))]
		public WorldExPotision Afx
		{
			get { return m_afx; }
			set { m_afx = value; }
		}

		[Category("位置")]
		[Description("回転・拡大の原点Y座標を指定します（-2000 ～ 2000）\n初期化・相対指定有効")]
		[TypeConverter(typeof(WorldExPotisionObjectConverter))]
		public WorldExPotision Afy
		{
			get { return m_afy; }
			set { m_afy = value; }
		}

		[Category("位置")]
		[Description("表示状態の初期化を行います")]
		public string Reset
		{
			get { return m_reset; }
			set { m_reset = value; }
		}

		[Category("位置")]
		[Description("オブジェクトのX座標位置を指定します（-2000 ～ 2000）\n初期化・相対指定有効")]
		[TypeConverter(typeof(WorldExPotisionObjectConverter))]
		public WorldExPotision Xpos
		{
			get { return m_xpos; }
			set { m_xpos = value; }
		}

		[Category("位置")]
		[Description("オブジェクトのY座標位置を指定します（-2000 ～ 2000）\n初期化・相対指定有効")]
		[TypeConverter(typeof(WorldExPotisionObjectConverter))]
		public WorldExPotision Ypos
		{
			get { return m_ypos; }
			set { m_ypos = value; }
		}

		[Category("時間")]
		[Description("動作時間（ms）を指定します（0 ～ 100000）")]
		public string Time
		{
			get { return m_time; }
			set { m_time = value; }
		}

		[Category("動作")]
		[Description("アクション時の加減速を指定します（-1, 0, 1）\n")]
		[TypeConverter(typeof(PropertyGridAccelConverter))]
		public string Accel
		{
			get { return m_accel; }
			set { m_accel = value; }
		}

		[Category("動作")]
		[Description("アクション名を指定します")]
		[TypeConverter(typeof(PropertyGridWorldExActionConverter))]
		public string Action
		{
			get { return m_action; }
			set { m_action = value; }
		}

		[Category("動作")]
		[Description("トランジション名を指定します")]
		[TypeConverter(typeof(PropertyGridWorldExTransConverter))]
		public string Trans
		{
			get { return m_trans; }
			set { m_trans = value; }
		}

		[Category("動作")]
		[Description("アクションの強制停止を行います")]
		[TypeConverter(typeof(PropertyGridOnOffConverter))]
		public string Stopaction
		{
			get { return m_stopaction; }
			set { m_stopaction = value; }
		}

		[Category("動作")]
		[Description("トランジションの強制停止を行います")]
		[TypeConverter(typeof(PropertyGridOnOffConverter))]
		public string Stoptrans
		{
			get { return m_stoptrans; }
			set { m_stoptrans = value; }
		}

		[Category("動作")]
		[Description("アクションおよびトランジションの同期を行います")]
		[TypeConverter(typeof(PropertyGridOnOffConverter))]
		public string Sync
		{
			get { return m_sync; }
			set { m_sync = value; }
		}

		[Category("動作")]
		[Description("アクションおよびトランジションの非同期動作を行います")]
		[TypeConverter(typeof(PropertyGridOnOffConverter))]
		public string Nosync
		{
			get { return m_nosync; }
			set { m_nosync = value; }
		}

		[Category("動作")]
		[Description("アクションおよびトランジションの同期指定を一時的に解除します")]
		[TypeConverter(typeof(PropertyGridOnOffConverter))]
		public string Nowait
		{
			get { return m_nowait; }
			set { m_nowait = value; }
		}

		[Category("動作")]
		[Description("表示指定のときはフェードイン、非表示指定のときはフェードアウトを行います")]
		[TypeConverter(typeof(PropertyGridOnOffConverter))]
		public string Fade
		{
			get { return m_fade; }
			set { m_fade = value; }
		}

		[Category("表示")]
		[Description("表示状態を指定します（true, false）")]
		[TypeConverter(typeof(PropertyGridOnOffConverter))]
		public string Visible
		{
			get { return m_visible; }
			set { m_visible = value; }
		}

		[Category("表示")]
		[Description("表示します")]
		[TypeConverter(typeof(PropertyGridOnOffConverter))]
		public string Show
		{
			get { return m_show; }
			set { m_show = value; }
		}

		[Category("表示")]
		[Description("非表示にします")]
		[TypeConverter(typeof(PropertyGridOnOffConverter))]
		public string Hide
		{
			get { return m_hide; }
			set { m_hide = value; }
		}

		[Category("色")]
		[Description("グレースケールにします")]
		[TypeConverter(typeof(PropertyGridOnOffConverter))]
		public string Grayscale
		{
			get { return m_grayscale; }
			set { m_grayscale = value; }
		}

		[Category("色")]
		[Description("赤色成分のガンマ補正を行います（0.1 ～ 9.9）")]
		public string Rgamma
		{
			get { return m_rgamma; }
			set { m_rgamma = value; }
		}

		[Category("色")]
		[Description("緑色成分のガンマ補正を行います（0.1 ～ 9.9）")]
		public string Ggamma
		{
			get { return m_ggamma; }
			set { m_ggamma = value; }
		}

		[Category("色")]
		[Description("青色成分のガンマ補正を行います（0.1 ～ 9.9）")]
		public string Bgamma
		{
			get { return m_bgamma; }
			set { m_bgamma = value; }
		}

		[Category("色")]
		[Description("grayscale/rgamma/ggamma/bgammaを初期化します")]
		[TypeConverter(typeof(PropertyGridOnOffConverter))]
		public string Resetcolor
		{
			get { return m_resetcolor; }
			set { m_resetcolor = value; }
		}
	}
}
