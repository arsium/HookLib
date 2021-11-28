# HookLib
A library to hook functions locally !

How to use it ?

```
byte[] ret_opcode = { 0xC3 }; //ret asm opcode
HookLib.HookLib your_hook;
your_hook = new HookLib.HookLib(Process.GetCurrentProcess().Handle, "alibrarynamelikekernel32", "functionnameliketerminateprocess", ret_opcode, 1);
your_hook.HookedFunction();
your_hook.UnHookedFunction();
```
