using System;
using HarmonyLib;
using NalulunaFlyingScore.Configuration;
using UnityEngine;

namespace NalulunaFlyingScore
{
	[HarmonyPatch(typeof(NoteCutScoreSpawner), "HandleNoteWasCut")]
	internal static class NoteCutScoreSpawnerHandleNoteWasCut
	{
		private static void Prefix(NoteController noteController, NoteCutInfo noteCutInfo)
		{
			if (!noteCutInfo.allIsOK || noteController.noteData.colorType == ColorType.None)
			{
				return;
			}
			NoteCutScoreSpawnerHandleNoteWasCut.isNoteCutEffect = true;
		}

		private static void Postfix()
		{
			NoteCutScoreSpawnerHandleNoteWasCut.isNoteCutEffect = false;
		}

		internal static bool isNoteCutEffect = false;
	}

	[HarmonyPatch(typeof(FlyingObjectEffect), "InitAndPresent")]
	internal static class FlyingObjectEffectInitAndPresent
	{
		private static void Postfix(FlyingObjectEffect __instance, float duration, ref float ____duration, ref Vector3 ____targetPos, ref Vector3 ____startPos, Quaternion rotation, ref bool ____shake, ref float ____shakeFrequency, ref float ____shakeStrength)
		{
			bool flag = NoteCutScoreSpawnerHandleNoteWasCut.isNoteCutEffect || duration < 0f;
			if (flag)
			{
				__instance.name = FlyingObjectEffectParameters.tag;
				bool flag2 = duration < 0f;
				if (flag2)
				{
					____duration = -1f * duration;
				}
				____shake = false;
				____shakeStrength = FlyingObjectEffectParameters.initialMomentum;
				____shakeFrequency = ____startPos.y;
				bool forward = PluginConfig.Instance.forward;
				if (forward)
				{
					bool flag3 = duration >= 0f;
					if (flag3)
					{
						float offsetY = (PluginConfig.Instance.scale - 0.5f) * FlyingObjectEffectParameters.scaleOffsetCoefY;
						bool pro = PluginConfig.Instance.pro;
						if (pro)
						{
							____targetPos = new Vector3(____targetPos.x, PluginConfig.Instance.forwardTargetYpro + offsetY, ____targetPos.z);
						}
						else
						{
							____targetPos = new Vector3(____targetPos.x, PluginConfig.Instance.forwardTargetY + offsetY, ____targetPos.z);
						}
					}
				}
				else
				{
					____targetPos = ____startPos;
				}
				bool flag4 = __instance is FlyingScoreEffect;
				if (flag4)
				{
					__instance.transform.localScale = Vector3.one * PluginConfig.Instance.scale;
				}
				else
				{
					__instance.transform.localScale = Vector3.one * PluginConfig.Instance.scale * FlyingObjectEffectParameters.spriteScale;
				}
				bool forward2 = PluginConfig.Instance.forward;
				if (forward2)
				{
					__instance.transform.localScale = __instance.transform.localScale * FlyingObjectEffectParameters.forwardScale;
				}
			}
			else
			{
				__instance.name = "FlyingSpriteEffect(Clone)";
				__instance.transform.localScale = Vector3.one;
			}
		}
	}

	[HarmonyPatch(typeof(FlyingObjectEffect), "Update")]
	internal static class FlyingObjectEffectUpdate
	{
		private static void Postfix(FlyingObjectEffect __instance, float ____duration, Vector3 ____startPos, ref Vector3 ____targetPos, float ____elapsedTime, ref float ____shakeFrequency, ref float ____shakeStrength)
		{
			bool flag = __instance.name != FlyingObjectEffectParameters.tag;
			if (!flag)
			{
				bool flag2 = !PluginConfig.Instance.pro;
				if (flag2)
				{
					____shakeStrength -= FlyingObjectEffectParameters.gravity * Time.deltaTime;
					____shakeFrequency += ____shakeStrength * Time.deltaTime;
					bool flag3 = ____shakeFrequency < ____startPos.y;
					if (flag3)
					{
						____shakeFrequency = ____startPos.y;
						____shakeStrength = ____shakeStrength * -1f * FlyingObjectEffectParameters.restitution;
					}
					float jump = ____shakeFrequency - ____startPos.y;
					bool forward = PluginConfig.Instance.forward;
					if (forward)
					{
						float t = (float)Math.Tanh((double)(FlyingObjectEffectParameters.forwardSpeed * ____elapsedTime / ____duration));
						float x = Mathf.Lerp(____startPos.x, ____targetPos.x, t);
						float y = Mathf.Lerp(____startPos.y, ____targetPos.y, t) + jump;
						float z = Mathf.Lerp(____startPos.z, ____targetPos.z, t);
						__instance.transform.localPosition = new Vector3(x, y, z);
					}
					else
					{
						__instance.transform.localPosition = new Vector3(____targetPos.x, ____targetPos.y + jump, ____targetPos.z);
					}
				}
			}
		}
	}
}
