using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RubySpook.Modules
{
    static class Cheats
    {
        private static HarmonyMethod GetPatch(string name) => new HarmonyMethod(typeof(Cheats).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic));

        private static Harmony harmony;
        public static void Init()
        {
            harmony = new Harmony("RubySpook_Cheats");
            harmony.Patch(typeof(FileBasedPrefs).GetMethod("GetInt"), GetPatch(nameof(OnPreGetInt)), null, null);
            harmony.Patch(typeof(FileBasedPrefs).GetMethod("SetInt"), GetPatch(nameof(OnPreSetInt)), null, null);
            harmony.Patch(typeof(AntiCheatSystem).GetMethod("CheckPlayerMoney"), GetPatch(nameof(OnPreAntiCheatCheck)), null, null);
        }

        private static bool OnPreGetInt(string key, ref int __result)
        {
            if (key == "myTotalExp" || key == "PlayersMoney")
                __result = 999999;
            else if (key.EndsWith("Inventory"))
                __result = 99;
            else
                return true;
            return false;
        }

        private static bool OnPreSetInt(string key)
        {
            if (key == "myTotalExp" || key == "PlayersMoney" || key.EndsWith("Inventory"))
                return false;
            return true;
        }

        private static bool OnPreAntiCheatCheck()
        {
            return false;
        }
    }
}
