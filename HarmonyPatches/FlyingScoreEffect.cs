using System;
using HarmonyLib;
using NalulunaFlyingScore.Configuration;
using TMPro;
using UnityEngine;

namespace NalulunaFlyingScore
{
	[HarmonyPatch(typeof(FlyingScoreEffect), "HandleSaberSwingRatingCounterDidChangeEvent")]
	internal static class FlyingScoreEffectHandleSaberSwingRatingCounterDidChangeEvent
	{
		private static bool Prefix()
		{
			return false;
		}
	}

	[HarmonyPatch(typeof(FlyingScoreEffect), "ManualUpdate")]
	internal static class FlyingScoreEffectManualUpdate
	{
		private static bool Prefix(float t, AnimationCurve ____fadeAnimationCurve, TextMeshPro ____text, Color ____color)
		{
			____text.color = ____color.ColorWithAlpha(____fadeAnimationCurve.Evaluate(t) * ____color.a);
			return false;
		}
	}

	[HarmonyPatch(typeof(FlyingScoreEffect), "InitAndPresent")]
	internal static class FlyingScoreEffectInitAndPresent
	{
		private static void Prefix(ref float duration)
		{
			bool flag = !PluginConfig.Instance.pro;
			if (flag)
			{
				duration *= 2f;
			}
		}

		private static void Postfix(FlyingScoreEffect __instance, NoteCutInfo ____noteCutInfo, TextMeshPro ____text, SpriteRenderer ____maxCutDistanceScoreIndicator, ref float ____colorAMultiplier, int multiplier)
		{
			c__DisplayClass2_0 scoreUtils = new c__DisplayClass2_0();
			scoreUtils.noteCutInfo = ____noteCutInfo;
			scoreUtils.text = ____text;
			scoreUtils.instance = __instance;
			scoreUtils.text.richText = PluginConfig.Instance.pro;
			scoreUtils.text.SetText("");

			if (PluginConfig.Instance.italic)
			{
				scoreUtils.text.fontStyle = FontStyles.Italic;
			}
			else
			{
				scoreUtils.text.fontStyle = FontStyles.Normal;
			}

			if (Plugin.uiFontEnabled)
			{
				scoreUtils.text.font = Plugin.uiFont;
			}

			____colorAMultiplier = (float)multiplier;
			____maxCutDistanceScoreIndicator.enabled = false;

			bool flag = !scoreUtils.noteCutInfo.swingRatingCounter.didFinish;
			if (flag)
			{
				scoreUtils.noteCutInfo.swingRatingCounter.didChangeEvent -= scoreUtils.g__HandleSaberSwingRatingCounterDidChange;
				scoreUtils.noteCutInfo.swingRatingCounter.didChangeEvent += scoreUtils.g__HandleSaberSwingRatingCounterDidChange;
				scoreUtils.noteCutInfo.swingRatingCounter.didFinishEvent -= scoreUtils.g__HandleSaberSwingRatingCounterDidFinish;
				scoreUtils.noteCutInfo.swingRatingCounter.didFinishEvent += scoreUtils.g__HandleSaberSwingRatingCounterDidFinish;
			}
			scoreUtils.g__UpdateScore();
		}

		private static char[] _char = new char[1];

		private sealed class c__DisplayClass2_0
		{
			internal void g__UpdateScore()
			{
				int beforeCut;
				int afterCut;
				int cutDistance;
				ScoreModel.RawScoreWithoutMultiplier(this.noteCutInfo, out beforeCut, out afterCut, out cutDistance);
				int score = beforeCut + afterCut + cutDistance;
				bool pro = PluginConfig.Instance.pro;
				if (pro)
				{
					if (PluginConfig.Instance.hideIndicator)
					{
						this.text.SetText((uint)score);
					}
					else
					{
						char[] array = (beforeCut == 70) ? FlyingObjectEffectParameters.beforeCutGood : FlyingObjectEffectParameters.beforeCutBad;
						char[] array2 = (afterCut == 30) ? FlyingObjectEffectParameters.afterCutGood : FlyingObjectEffectParameters.afterCutBad;
						uint num = (uint)score;
						char[] array3 = (cutDistance >= (int)PluginConfig.Instance.distanceHalfScore) ? ((cutDistance == 15) ? FlyingObjectEffectParameters.cutDistanceGood : FlyingObjectEffectParameters.cutDistanceHalf) : FlyingObjectEffectParameters.cutDistanceBad;
						this.text.SetText(array, array2, num, array3);
					}
				}
				else
				{
					FlyingScoreEffectInitAndPresent._char[0] = ' ';
					int index = (int)this.instance.GetPrivateField<float>("_colorAMultiplier");
					bool flag = index < 0;
					if (flag)
					{
						int number = score / ((index == -1) ? 100 : ((index == -2) ? 10 : 1));
						bool flag2 = number > 0;
						if (flag2)
						{
							FlyingScoreEffectInitAndPresent._char[0] = (char)(48 + number % 10);
						}
					}
					this.text.SetCharArray(FlyingScoreEffectInitAndPresent._char);
				}
				bool useColor = PluginConfig.Instance.color;
				if (useColor)
				{
					Color color = FlyingObjectEffectParameters.GetTextColor(score);
					this.instance.SetPrivateField("_color", color);
				}
			}

			internal void g__HandleSaberSwingRatingCounterDidChange(ISaberSwingRatingCounter afterCutRating, float rating)
			{
				this.g__UpdateScore();
			}

			internal void g__HandleSaberSwingRatingCounterDidFinish(ISaberSwingRatingCounter afterCutRating)
			{
				this.g__UpdateScore();
				afterCutRating.didChangeEvent -= this.g__HandleSaberSwingRatingCounterDidChange;
				afterCutRating.didFinishEvent -= this.g__HandleSaberSwingRatingCounterDidFinish;
			}

			public NoteCutInfo noteCutInfo;

			public TextMeshPro text;

			public FlyingScoreEffect instance;
		}
	}
}
