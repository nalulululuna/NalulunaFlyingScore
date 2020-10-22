using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using UnityEngine;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace NalulunaFlyingScore.Configuration
{
	internal class PluginConfig
	{
		public static PluginConfig Instance { get; set; }

		public virtual bool color { get; set; } = true;

		public virtual bool forward { get; set; } = true;

		public virtual float forwardTargetY { get; set; } = 0.1f;

		public virtual float forwardTargetYpro { get; set; } = 0.5f;

		public virtual float forwardTargetZ { get; set; } = 6.55f;

		public virtual float forwardTargetZpro { get; set; } = 7.55f;

		public virtual bool pro { get; set; } = false;

		public virtual bool italic { get; set; } = false;
		
		public virtual float scale { get; set; } = 0.6f;

		public virtual bool noScoreText { get; set; } = false;

		public virtual bool noMissText { get; set; } = false;

		public virtual byte distanceHalfScore { get; set; } = 15;

		public Color color1
		{
			get
			{
				return new Color32(this.color1r, this.color1g, this.color1b, byte.MaxValue);
			}
		}

		public virtual byte color1r { get; set; } = 0;

		public virtual byte color1g { get; set; } = byte.MaxValue;

		public virtual byte color1b { get; set; } = byte.MaxValue;

		public virtual bool color1transparent { get; set; } = false;

		public virtual byte score1 { get; set; } = 115;

		public Color color2
		{
			get
			{
				return new Color32(this.color2r, this.color2g, this.color2b, byte.MaxValue);
			}
		}

		public virtual byte color2r { get; set; } = 0;

		public virtual byte color2g { get; set; } = 127;

		public virtual byte color2b { get; set; } = byte.MaxValue;

		public virtual bool color2transparent { get; set; } = false;

		public virtual byte score2 { get; set; } = 110;

		public Color color3
		{
			get
			{
				return new Color32(this.color3r, this.color3g, this.color3b, byte.MaxValue);
			}
		}

		public virtual byte color3r { get; set; } = 0;

		public virtual byte color3g { get; set; } = byte.MaxValue;

		public virtual byte color3b { get; set; } = 0;

		public virtual bool color3transparent { get; set; } = false;

		public virtual byte score3 { get; set; } = 101;

		public Color color4
		{
			get
			{
				return new Color32(this.color4r, this.color4g, this.color4b, byte.MaxValue);
			}
		}

		public virtual byte color4r { get; set; } = byte.MaxValue;

		public virtual byte color4g { get; set; } = byte.MaxValue;

		public virtual byte color4b { get; set; } = 0;

		public virtual bool color4transparent { get; set; } = false;

		public virtual byte score4 { get; set; } = 80;

		public Color color5
		{
			get
			{
				return new Color32(this.color5r, this.color5g, this.color5b, byte.MaxValue);
			}
		}

		public virtual byte color5r { get; set; } = byte.MaxValue;

		public virtual byte color5g { get; set; } = 0;

		public virtual byte color5b { get; set; } = 0;

		public virtual bool color5transparent { get; set; } = false;

		public virtual byte score5 { get; set; } = 60;

		public Color color6
		{
			get
			{
				return new Color32(this.color6r, this.color6g, this.color6b, byte.MaxValue);
			}
		}

		public virtual byte color6r { get; set; } = byte.MaxValue;

		public virtual byte color6g { get; set; } = 0;

		public virtual byte color6b { get; set; } = 0;

		public virtual bool color6transparent { get; set; } = false;

		public virtual byte score6 { get; set; } = 40;

		public Color color7
		{
			get
			{
				return new Color32(this.color7r, this.color7g, this.color7b, byte.MaxValue);
			}
		}

		public virtual byte color7r { get; set; } = 0;

		public virtual byte color7g { get; set; } = 0;

		public virtual byte color7b { get; set; } = 0;

		public virtual bool color7transparent { get; set; } = false;

		public virtual bool hideIndicator { get; set; } = false;

		public virtual string font { get; set; } = "";
	}
}
