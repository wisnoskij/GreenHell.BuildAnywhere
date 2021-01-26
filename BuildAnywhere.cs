using HarmonyLib;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildAnywhere : Mod
{
	private const string ModName = "buildanywhere";
	private const string HarmonyId = "com.wisnoski.greenhell." + ModName;
	Harmony instance;
	public void Start()
	{
		Debug.Log(string.Format("Mod {0} is attempting to load.", ModName));
		instance = new Harmony(HarmonyId);
		instance.PatchAll(Assembly.GetExecutingAssembly());
		Debug.Log(string.Format("Mod {0} has been loaded!", ModName));
	}
	
	public void OnModUnload()
	{
		Debug.Log(string.Format("Mod {0} has been unloaded!", ModName));
	}
}

[HarmonyPatch(typeof(ConstructionGhost))]
[HarmonyPatch("UpdateProhibitionType")]
internal class Patch_ConstructionGhost
{
	static void Postfix(ConstructionGhost __instance)
	{
		Traverse.Create(__instance).Field("m_ProhibitionType").SetValue(ConstructionGhost.ProhibitionType.None);
	}
}
