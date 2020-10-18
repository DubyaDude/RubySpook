using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;
using Valve.VR.InteractionSystem;

namespace RubySpook.Modules
{
    static class MegaLobby
    {
        private static HarmonyMethod GetPatch(string name) => new HarmonyMethod(typeof(MegaLobby).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic));

        private static Harmony harmony;
        public static void Init()
        {
            harmony = new Harmony("RubySpook_MegaLobby");
            SceneManager.sceneLoaded += WaitingForAssemblies;
        }

        private static void WaitingForAssemblies(Scene arg0, LoadSceneMode arg1)
        {
            SceneManager.sceneLoaded -= WaitingForAssemblies;
            harmony.Patch(typeof(PhotonNetwork).GetMethods().First(x=> x.Name == "CreateRoom" && x.GetParameters()?.Length == 4), GetPatch(nameof(OnPreCreateRoomPatch)), null, null);
            harmony.Patch(typeof(ServerListItem).GetMethod("SetUI"), null, GetPatch(nameof(OnPostSetUIPatch)), null);
        }

        private static bool OnPreCreateRoomPatch(ref RoomOptions roomOptions)
        {
            if(roomOptions?.MaxPlayers == 4)
            {
                Console.WriteLine("[MegaLobby] Room MaxPlayers Changed to 99");
                roomOptions.MaxPlayers = 99;
            }
            return true;
        }
        private static void OnPostSetUIPatch(ServerListItem __instance, string population, ref RoomOptions roomOptions)
        {
            __instance.serverPopulation.text = population + "/" + roomOptions.MaxPlayers;
        }
    }
}
