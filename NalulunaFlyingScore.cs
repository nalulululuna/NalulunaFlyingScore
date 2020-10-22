using NalulunaFlyingScore.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NalulunaFlyingScore
{
	internal static class FlyingObjectEffectParameters
	{
		internal static Color GetTextColor(int score)
		{
			const float alpha = 0.5f;
			bool flag = score >= (int)PluginConfig.Instance.score1;
			Color color;
			if (flag)
			{
				color = (PluginConfig.Instance.color1transparent ? Color.clear : PluginConfig.Instance.color1.ColorWithAlpha(alpha));
			}
			else
			{
				bool flag2 = score >= (int)PluginConfig.Instance.score2;
				if (flag2)
				{
					color = (PluginConfig.Instance.color2transparent ? Color.clear : PluginConfig.Instance.color2.ColorWithAlpha(alpha));
				}
				else
				{
					bool flag3 = score >= (int)PluginConfig.Instance.score3;
					if (flag3)
					{
						color = (PluginConfig.Instance.color3transparent ? Color.clear : PluginConfig.Instance.color3.ColorWithAlpha(alpha));
					}
					else
					{
						bool flag4 = score >= (int)PluginConfig.Instance.score4;
						if (flag4)
						{
							color = (PluginConfig.Instance.color4transparent ? Color.clear : PluginConfig.Instance.color4.ColorWithAlpha(alpha));
						}
						else
						{
							bool flag5 = score >= (int)PluginConfig.Instance.score5;
							if (flag5)
							{
								color = (PluginConfig.Instance.color5transparent ? Color.clear : PluginConfig.Instance.color5.ColorWithAlpha(alpha));
							}
							else
							{
								bool flag6 = score >= (int)PluginConfig.Instance.score6;
								if (flag6)
								{
									color = (PluginConfig.Instance.color6transparent ? Color.clear : PluginConfig.Instance.color6.ColorWithAlpha(alpha));
								}
								else
								{
									color = (PluginConfig.Instance.color7transparent ? Color.clear : PluginConfig.Instance.color7.ColorWithAlpha(alpha));
								}
							}
						}
					}
				}
			}
			return color;
		}

		internal static readonly string tag = "NalulunaFlyingScore";

		internal static readonly float timeShift = 0.05f;

		internal static readonly float initialMomentum = 4f;

		internal static readonly float gravity = 20f;

		internal static readonly float restitution = 0.6f;

		internal static readonly float forwardSpeed = 5f;

		internal static readonly float scoreNumberOffsetX = 0.088f;

		internal static readonly float spriteScale = 0.5f;

		internal static readonly float forwardScale = 2f;

		internal static readonly float scaleOffsetCoefY = 0.37f;

		internal static readonly char[] beforeCutGood = new char[]
		{
			'<',
			'l',
			'i',
			'n',
			'e',
			'-',
			'h',
			'e',
			'i',
			'g',
			'h',
			't',
			'=',
			'7',
			'5',
			'%',
			'>',
			'<',
			'a',
			'l',
			'p',
			'h',
			'a',
			'=',
			'#',
			'F',
			'F',
			'>',
			'_'
		};

		internal static readonly char[] beforeCutBad = new char[]
		{
			'<',
			'l',
			'i',
			'n',
			'e',
			'-',
			'h',
			'e',
			'i',
			'g',
			'h',
			't',
			'=',
			'7',
			'5',
			'%',
			'>',
			'<',
			'a',
			'l',
			'p',
			'h',
			'a',
			'=',
			'#',
			'0',
			'0',
			'>',
			'_'
		};

		internal static readonly char[] afterCutGood = new char[]
		{
			'<',
			'a',
			'l',
			'p',
			'h',
			'a',
			'=',
			'#',
			'F',
			'F',
			'>',
			'_',
			'\\',
			'n',
			'<',
			'l',
			'i',
			'n',
			'e',
			'-',
			'h',
			'e',
			'i',
			'g',
			'h',
			't',
			'=',
			'1',
			'8',
			'%',
			'>',
			'<',
			'a',
			'l',
			'p',
			'h',
			'a',
			'=',
			'#',
			'F',
			'F',
			'>'
		};

		internal static readonly char[] afterCutBad = new char[]
		{
			'<',
			'a',
			'l',
			'p',
			'h',
			'a',
			'=',
			'#',
			'0',
			'0',
			'>',
			'_',
			'\\',
			'n',
			'<',
			'l',
			'i',
			'n',
			'e',
			'-',
			'h',
			'e',
			'i',
			'g',
			'h',
			't',
			'=',
			'1',
			'8',
			'%',
			'>',
			'<',
			'a',
			'l',
			'p',
			'h',
			'a',
			'=',
			'#',
			'F',
			'F',
			'>'
		};

		internal static readonly char[] cutDistanceGood = new char[]
		{
			'\\',
			'n',
			'<',
			'a',
			'l',
			'p',
			'h',
			'a',
			'=',
			'#',
			'F',
			'F',
			'>',
			'_',
			'_'
		};

		internal static readonly char[] cutDistanceHalf = new char[]
		{
			'\\',
			'n',
			'<',
			'a',
			'l',
			'p',
			'h',
			'a',
			'=',
			'#',
			'F',
			'F',
			'>',
			'_'
		};

		internal static readonly char[] cutDistanceBad = new char[]
		{
			'\\',
			'n',
			'<',
			'a',
			'l',
			'p',
			'h',
			'a',
			'=',
			'#',
			'0',
			'0',
			'>',
			'_',
			'_'
		};
	}
}
