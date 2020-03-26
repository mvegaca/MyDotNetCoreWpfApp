using System;
using System.Runtime.InteropServices;
using MyDotNetCoreWpfApp.Models;

namespace MyDotNetCoreWpfApp.Notifications
{
    [ComImport,
        Guid("53E31837-6600-4A81-9395-75CFFE746F94"), ComVisible(true),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface INotificationActivationCallback
    {
        void Activate(
            [In, MarshalAs(UnmanagedType.LPWStr)]
            string appUserModelId,
            [In, MarshalAs(UnmanagedType.LPWStr)]
            string invokedArgs,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            NotificationUserInputData[] data,
            [In, MarshalAs(UnmanagedType.U4)]
            uint dataCount);
    }
}
