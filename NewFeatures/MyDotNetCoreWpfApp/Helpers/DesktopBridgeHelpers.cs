using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MyDotNetCoreWpfApp.Helpers
{
    /// <summary>
    /// Code from https://github.com/qmatteoq/DesktopBridgeHelpers/edit/master/DesktopBridge.Helpers/Helpers.cs
    /// </summary>
    public class DesktopBridgeHelpers
    {
        const long APPMODEL_ERROR_NO_PACKAGE = 15700L;

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern int GetCurrentPackageFullName(ref int packageFullNameLength, StringBuilder packageFullName);

        private static bool? _isRunningAsUwp;
        public static bool IsRunningAsUwp()
        {
            if (_isRunningAsUwp == null)
            {
                if (IsWindows7OrLower)
                {
                    _isRunningAsUwp = false;
                }
                else
                {
                    int length = 0;
                    StringBuilder sb = new StringBuilder(0);
                    int result = GetCurrentPackageFullName(ref length, sb);

                    sb = new StringBuilder(length);
                    result = GetCurrentPackageFullName(ref length, sb);

                    _isRunningAsUwp = result != APPMODEL_ERROR_NO_PACKAGE;
                }
            }

            return _isRunningAsUwp.Value;
        }

        public static bool IsWindows7OrLower
        {
            get
            {
                int versionMajor = Environment.OSVersion.Version.Major;
                int versionMinor = Environment.OSVersion.Version.Minor;
                double version = versionMajor + (double)versionMinor / 10;
                return version <= 6.1;
            }
        }
    }
}
