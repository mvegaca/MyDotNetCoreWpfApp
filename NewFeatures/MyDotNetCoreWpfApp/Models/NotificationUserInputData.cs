using System;
using System.Runtime.InteropServices;

namespace MyDotNetCoreWpfApp.Models
{
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct NotificationUserInputData
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Key;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string Value;
    }
}
