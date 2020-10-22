using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using HarmonyLib;
using NalulunaFlyingScore.Configuration;
using UnityEngine;

namespace NalulunaFlyingScore
{
	[HarmonyPatch(typeof(FlyingSpriteSpawner), "SpawnFlyingSprite")]
	internal class FlyingSpriteSpawnerSpawnFlyingSprite
	{
		private static bool Prefix()
		{
			return !PluginConfig.Instance.noMissText;
		}
	}

	[HarmonyPatch(typeof(FlyingScoreSpawner), "SpawnFlyingScore")]
	internal static class FlyingScoreSpawnerSpawnFlyingScore
	{
		private static bool Prefix(FlyingScoreSpawner __instance, FlyingScoreEffect.Pool ____flyingScoreEffectPool, Vector3 pos, NoteCutInfo noteCutInfo, Quaternion rotation, Quaternion inverseRotation)
		{
			IEnumerator g__SpawnFlyingScoreEffectCoroutine(int index, Vector3 position)
			{
				yield return new WaitForSecondsRealtime(FlyingObjectEffectParameters.timeShift * index);

				FlyingScoreEffect flyingScoreEffect = ____flyingScoreEffectPool.Spawn();
				flyingScoreEffect.didFinishEvent += __instance.HandleFlyingScoreEffectDidFinish;
				flyingScoreEffect.transform.localPosition = position;
				position = inverseRotation * position;
				position.z = 0f;
				if (PluginConfig.Instance.forward)
				{
					float offsetY = (PluginConfig.Instance.scale - 0.5f) * FlyingObjectEffectParameters.scaleOffsetCoefY;
					position = new Vector3(position.x, PluginConfig.Instance.forwardTargetY + offsetY, pos.z + PluginConfig.Instance.forwardTargetZ);
					position = rotation * position;
				}
				flyingScoreEffect.InitAndPresent(noteCutInfo, index * -1, -0.7f, position, rotation, Color.white);
			}

			bool noScoreText = PluginConfig.Instance.noScoreText;
			bool result;
			if (noScoreText)
			{
				result = false;
			}
			else
			{
				bool flag = !PluginConfig.Instance.pro;
				if (flag)
				{
					float offsetX = FlyingObjectEffectParameters.scoreNumberOffsetX * PluginConfig.Instance.scale;
					bool forward = PluginConfig.Instance.forward;
					if (forward)
					{
						offsetX *= FlyingObjectEffectParameters.forwardScale;
					}
					Vector3 offset = new Vector3(offsetX, 0f, 0f);
					Vector3 pos2 = rotation * (Quaternion.Inverse(rotation) * pos - offset);
					Vector3 pos3 = rotation * (Quaternion.Inverse(rotation) * pos + offset);
					PersistentSingleton<SharedCoroutineStarter>.instance.StartCoroutine(g__SpawnFlyingScoreEffectCoroutine(1, pos2));
					PersistentSingleton<SharedCoroutineStarter>.instance.StartCoroutine(g__SpawnFlyingScoreEffectCoroutine(2, pos));
					PersistentSingleton<SharedCoroutineStarter>.instance.StartCoroutine(g__SpawnFlyingScoreEffectCoroutine(3, pos3));
				}
				result = true;
			}
			return result;
		}
	}
}
