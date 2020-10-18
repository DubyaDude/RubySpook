using HarmonyLib;
using RubySpook.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine.SceneManagement;

namespace RubySpook
{
    public static class Loader
    {
        public static void Initialize()
        {
            ConsoleHelper.Show();
            Assembly.LoadFile(Directory.GetCurrentDirectory() + @"\AutoLoader\0Harmony.dll");
            try
            {
                Cheats.Init();
                Console.WriteLine("[RubySpook] Cheats Initialized!");
            }
            catch(Exception e)
            {
                Console.WriteLine("Failed to Init Cheats!\n" + e);
            }
            try
            {
                SpeechWriter.Init();
                Console.WriteLine("[RubySpook] Speech Writer Initialized!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Init Speech Writer!\n" + e);
            }
            try
            {
                MegaLobby.Init();
                Console.WriteLine("[RubySpook] Mega Lobby Initialized!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Init Mega Lobby!\n" + e);
            }
        }
    }

    static class ConsoleHelper
    {
        [DllImport("kernel32.dll")]
        private static extern int AllocConsole();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int CONSOLE_HIDE = 0;
        const int CONSOLE_SHOW = 5;
        const string version = "v1.0";

        private static void InitConsole()
        {
            AllocConsole();
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            });
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
            Console.Clear();
            Console.WriteLine($"[UAI] RubySpook {version} loaded. Powered By 0Harmony by Pardeike and UnityAssemblyInjector by Avail");
            Console.Title = "RubySpook";
        }

        public static void Show()
        {
            if (GetConsoleWindow() == IntPtr.Zero)
                InitConsole();
            ShowWindow(GetConsoleWindow(), CONSOLE_SHOW);
        }
        public static void Hide()
        {
            if (GetConsoleWindow() != IntPtr.Zero)
                ShowWindow(GetConsoleWindow(), CONSOLE_HIDE);
        }
    }
}
