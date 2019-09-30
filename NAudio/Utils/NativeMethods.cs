using System;
using System.Runtime.InteropServices;
using NAudio.Wave;

namespace NAudio.Utils
{
    /// <summary>
    /// General purpose native methods for internal NAudio use
    /// </summary>
    class NativeMethods
    {
#if !WINDOWS_UWP
        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);
#else

        [DllImport("api-ms-win-core-synch-l1-2-0.dll", CharSet = CharSet.Unicode, ExactSpelling = false,
            PreserveSig = true, SetLastError = true)]
        internal static extern IntPtr CreateEventExW(IntPtr lpEventAttributes, IntPtr lpName, int dwFlags,
                                                     EventAccess dwDesiredAccess);




        [System.Flags]
        enum LoadLibraryFlags : uint
        {
            None = 0,
            DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
            LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
            LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
            LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
            LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
            LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,
            LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,
            LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
            LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,
            LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,
            LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008
        }

        [DllImport("api-ms-win-core-libraryloader-l1-2-0.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern IntPtr LoadLibraryExW(string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);

        //FIXME not sure if Unicode or not
        [DllImport("api-ms-win-core-libraryloader-l1-2-0.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("api-ms-win-core-libraryloader-l1-2-0.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);

        public static IntPtr LoadLibrary(string dllToLoad) {
            return LoadLibraryExW(dllToLoad, IntPtr.Zero, LoadLibraryFlags.None);
        }
#endif
    }
}
