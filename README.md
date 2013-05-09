ScriptCs.SignalR

Some simple helpers for writing SignalR in C# scripts. 
The main purpose of this is to allow sending messages 
to clients without having to use the dynamic object, which 
is not supported by Roslyn (and therefore ScriptCs) at 
the time of writing.