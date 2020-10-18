using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Windows.Speech;

namespace RubySpook.Modules
{
    static class SpeechWriter
    {
        private static HarmonyMethod GetPatch(string name) => new HarmonyMethod(typeof(SpeechWriter).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic));

        private static Harmony harmony;
        public static void Init()
        {
            harmony = new Harmony("RubySpook_SpeechWriter");
            harmony.Patch(typeof(SpeechRecognitionController).GetMethod("OnPhraseRecognized", BindingFlags.NonPublic | BindingFlags.Instance), GetPatch(nameof(OnPrePhraseRecognizedPatch)), null, null);
        }

        private static bool OnPrePhraseRecognizedPatch(PhraseRecognizedEventArgs args)
        {
            Console.WriteLine("[SpeechWriter] " + args.text);
            return true;
        }
    }
}
