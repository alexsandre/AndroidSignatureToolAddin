﻿using System;
using System.IO;

namespace AndroidSignatureTool.Core
{
	public static class PlatformDetection
	{
		public readonly static bool IsMac;
        public readonly static bool IsLinux;

		static PlatformDetection ()
		{
			IsMac = IsRunningOnMac();
            IsLinux = IsRunningLinux();
		}

        static bool IsRunningLinux()
        {
            return System.Environment.OSVersion.Platform == System.PlatformID.Unix;
        }

		//From Managed.Windows.Forms/XplatUI
		static bool IsRunningOnMac ()
		{
			IntPtr buf = IntPtr.Zero;
			try {
				buf = System.Runtime.InteropServices.Marshal.AllocHGlobal (8192);
				// This is a hacktastic way of getting sysname from uname ()
				if (uname (buf) == 0) {
					string os = System.Runtime.InteropServices.Marshal.PtrToStringAnsi (buf);
					if (os == "Darwin")
						return true;
				}
			} catch {
			} finally {
				if (buf != IntPtr.Zero)
					System.Runtime.InteropServices.Marshal.FreeHGlobal (buf);
			}
			return false;
		}

		[System.Runtime.InteropServices.DllImport ("libc")]
		static extern int uname (IntPtr buf);
	}
}

