using HarmonyLib;
using System.Reflection;
using UnityEngine;

public class BuildAnywhere : Mod
{
	private const string ModName = "buildanywhere";
	private const string HarmonyId = "com.wisnoski.greenhell." + ModName;
	Harmony instance_buildanywhere;
	public void Start()
	{
		instance_buildanywhere = new Harmony(HarmonyId);
		instance_buildanywhere.PatchAll(Assembly.GetExecutingAssembly());
		Debug.Log(string.Format("Mod {0} has been loaded!", ModName));
	}
	
	public void OnModUnload()
	{
		instance_buildanywhere.UnpatchAll(ModName);
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
