using System;

namespace HookLib
{
    public class HookLib
    {
        private string LibToHook { get; set; }
        private string FunctionToHook { get; set; }
        public byte[] NewBytes { get; set; }
        private uint SizeOfNewBytes { get; set; }
        public bool IsHooked { get; set; }
        public byte[] OldBytes { get; set; }
        private IntPtr ProcessToHook { get; set; }

        public HookLib(IntPtr ProcessToPatch, string LibName, string FunctionName, byte[] BytesToHook, uint SizeOfBytesToHook)
        {
            OldBytes = new byte[SizeOfBytesToHook]; //first we need a buffer to restore old function bytes to unhook it
            ProcessToHook = ProcessToPatch;
            LibToHook = LibName;//the lib ex kernel32 or ntdll
            FunctionToHook = FunctionName;//name of the function you want to hook 
            NewBytes = BytesToHook;//bytes you want to use as replacement of our function address
            SizeOfNewBytes = SizeOfBytesToHook;//the size of hooked bytes
        }

        public bool HookedFunction() 
        {
            IntPtr AddressOfLib = NativeAPI.GetModuleHandle(LibToHook);//getting lib address in our program
            IntPtr FunctionAddress = NativeAPI.GetProcAddress(AddressOfLib, FunctionToHook);//getting function address in our program
            NativeAPI.ReadProcessMemory(ProcessToHook, FunctionAddress, OldBytes, SizeOfNewBytes, 0);//read the original bytes from our function address and store them if you want to restore
            return IsHooked = NativeAPI.WriteProcessMemory(ProcessToHook, FunctionAddress, NewBytes, SizeOfNewBytes, 0);// here we hooked the function : the bytes at function address are replaced by our code (asm or opcode !)
        }
     
        public bool UnHookedFunction() 
        {
            IntPtr AddressOfLib = NativeAPI.GetModuleHandle(LibToHook);//getting lib address in our program
            IntPtr FunctionAddress = NativeAPI.GetProcAddress(AddressOfLib, FunctionToHook);//getting function address in our program
            if (NativeAPI.WriteProcessMemory(ProcessToHook, FunctionAddress, OldBytes, SizeOfNewBytes, 0))//here we unhook the function by setting the original bytes from our buffer
                IsHooked = false;
            else
                IsHooked = true;
            return IsHooked;
        }
    }
}
