using Harmony;

namespace ShowMapLocation
{
    [HarmonyPatch(typeof(Panel_Map), "ResetToNormal")]
    internal class Panel_Map_ResetToNormal
    {
        internal static void Prefix(Panel_Map __instance, ref Panel_Map.ResetOpts opts)
        {
            if (GameManager.IsStoryMode())
            {
                return;
            }

            string mapName = __instance.GetMapNameOfCurrentScene();
            bool canBeMapped = SceneCanBeMapped(mapName);
            if (!canBeMapped)
            {
                return;
            }

            int currentIndex = __instance.GetIndexOfCurrentScene();
            int selectedIndex = __instance.m_RegionSelectedIndex;
            if (currentIndex == selectedIndex)
            {
                opts |= Panel_Map.ResetOpts.ShowPlayer;
            }
        }

        private static bool SceneCanBeMapped(string sceneName)
        {
            return RegionManager.SceneIsRegion(sceneName) || sceneName == "DamRiverTransitionZoneB" || (sceneName == "HighwayTransitionZone" || sceneName == "RavineTransitionZone");// || sceneName == "MountainTownRegionSandbox";
        }
    }
}