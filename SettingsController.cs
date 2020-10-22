using System;
using BeatSaberMarkupLanguage.Attributes;
using NalulunaFlyingScore.Configuration;

namespace NalulunaFlyingScore
{
	public class SettingsController : PersistentSingleton<SettingsController>
	{
		[UIValue("color")]
		public bool color
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
			}
		}

		[UIValue("forward")]
		public bool forward
		{
			get
			{
				return this._forward;
			}
			set
			{
				this._forward = value;
			}
		}

		[UIValue("pro")]
		public bool pro
		{
			get
			{
				return this._pro;
			}
			set
			{
				this._pro = value;
			}
		}

		[UIValue("italic")]
		public bool italic
		{
			get
			{
				return this._italic;
			}
			set
			{
				this._italic = value;
			}
		}

		[UIValue("scale")]
		public float scale
		{
			get
			{
				return this._scale;
			}
			set
			{
				this._scale = value;
			}
		}

		[UIValue("noScoreText")]
		public bool noScoreText
		{
			get
			{
				return this._noScoreText;
			}
			set
			{
				this._noScoreText = value;
			}
		}

		[UIValue("noMissText")]
		public bool noMissText
		{
			get
			{
				return this._noMissText;
			}
			set
			{
				this._noMissText = value;
			}
		}

		[UIAction("#apply")]
		public void OnApply()
		{
			PluginConfig.Instance.color = this._color;
			PluginConfig.Instance.forward = this._forward;
			PluginConfig.Instance.pro = this._pro;
			PluginConfig.Instance.italic = this._italic;
			PluginConfig.Instance.scale = this._scale;
			PluginConfig.Instance.noScoreText = this._noScoreText;
			PluginConfig.Instance.noMissText = this._noMissText;
		}

		private void Awake()
		{
			this._color = PluginConfig.Instance.color;
			this._forward = PluginConfig.Instance.forward;
			this._pro = PluginConfig.Instance.pro;
			this._italic = PluginConfig.Instance.italic;
			this._scale = PluginConfig.Instance.scale;
			this._noScoreText = PluginConfig.Instance.noScoreText;
			this._noMissText = PluginConfig.Instance.noMissText;
		}

		private bool _color;

		private bool _forward;

		private bool _pro;

		private bool _italic;

		private float _scale;

		private bool _noScoreText;

		private bool _noMissText;
	}
}
