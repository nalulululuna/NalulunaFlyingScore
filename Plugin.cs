using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Settings;
using HarmonyLib;
using IPA;
using IPA.Config.Stores;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using TMPro;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;

namespace NalulunaFlyingScore
{
	[Plugin(RuntimeOptions.DynamicInit)]
	public class Plugin
	{
		public const string HarmonyId = "com.github.nalulululuna.NalulunaFlyingScore";
		internal static Harmony harmony => new Harmony(HarmonyId);

		internal static Plugin instance { get; private set; }
		internal static string Name => "NalulunaFlyingScore";

		internal static TMP_FontAsset uiFont;
		internal static bool uiFontEnabled;

		[Init]
		public Plugin(IPALogger logger, IPA.Config.Config conf)
		{
			instance = this;
			Logger.log = logger;
			Logger.log.Debug("Logger initialized.");

			Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
			Logger.log.Debug("Config loaded.");
		}

		[OnEnable]
		public void OnEnable()
		{
			BSMLSettings.instance.AddSettingsMenu("<size=90%>Naluluna Flying Score</size>", $"{Name}.Views.Settings.bsml", SettingsController.instance);
			ApplyHarmonyPatches();

			if (Configuration.PluginConfig.Instance.font != "")
			{
				PersistentSingleton<SharedCoroutineStarter>.instance.StartCoroutine(LoadFontCoroutine());
			}
		}

		IEnumerator LoadFontCoroutine()
		{
			yield return new WaitForSecondsRealtime(5f);
			if (File.Exists(Configuration.PluginConfig.Instance.font))
			{


				try
				{
					Font font = FontManager.AddFontFile(Configuration.PluginConfig.Instance.font);
					TMP_FontAsset fontAsset = BeatSaberUI.CreateTMPFont(font);
					uiFont = BeatSaberUI.CreateFixedUIFontClone(fontAsset);
					if (uiFont != null)
					{
						uiFontEnabled = true;
					}
				}
				catch
				{
					Logger.log.Error($"Failed to load font: {Configuration.PluginConfig.Instance.font}");
				}
			}
		}

		[OnDisable]
		public void OnDisable()
		{
			Plugin.RemoveHarmonyPatches();
			BSMLSettings.instance.RemoveSettingsMenu(SettingsController.instance);
		}

		public static void ApplyHarmonyPatches()
		{
			try
			{
				Logger.log?.Debug("Applying Harmony patches.");
				Plugin.harmony.PatchAll(Assembly.GetExecutingAssembly());
			}
			catch (Exception ex)
			{
				Logger.log?.Critical("Error applying Harmony patches: " + ex.Message);
				Logger.log?.Debug(ex);
			}				
		}

		public static void RemoveHarmonyPatches()
		{
			try
			{
				Plugin.harmony.UnpatchAll("com.github.nalulululuna.NalulunaFlyingScore");
			}
			catch (Exception ex)
			{
				Logger.log?.Critical("Error removing Harmony patches: " + ex.Message);
				Logger.log?.Debug(ex);
			}
		}
	}
}
