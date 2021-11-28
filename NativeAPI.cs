using System;
using System.Runtime.InteropServices;

namespace HookLib
{
    internal class NativeAPI
    {
        private const String KERNEL32 = "kernel32.dll";

        [DllImport(KERNEL32, SetLastError = true)]
        internal static extern IntPtr GetModuleHandle(string lib);

        [DllImport(KERNEL32, SetLastError = true)]
        internal static extern IntPtr GetProcAddress(IntPtr Module, string Function);

        [DllImport(KERNEL32, SetLastError = true)]
        internal static extern bool WriteProcessMemory(IntPtr ProcessHandle, IntPtr Address, byte[] CodeToInject, uint Size, int NumberOfBytes);

        [DllImport(KERNEL32, SetLastError = true)]
        internal static extern bool ReadProcessMemory(IntPtr ProcHandle, IntPtr BaseAddress, byte[] Buffer, uint size, int NumOfBytes);
    }
}
