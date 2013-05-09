ScriptCs.SignalR
================

# What is it?
Some simple helpers for writing SignalR in C# scripts. The main purpose of this is to allow sending messages to clients without having to use the dynamic object, which is not supported by Roslyn (and therefore ScriptCs) at the time of writing.

#How do I get it?
* This package is available on Nuget under the name ScriptCs.SignalR.
* Install from scriptcs: scriptcs -install ScriptCs.SignalR

#Quick start
Here's a complete ScriptCs example using our extensions. Because dynamic is only ever used in the compiled assembly this runs in Roslyn without issues.

We are currently using a pre-release package on NuGet due to a dependency on Owin hosting pre-release packages. **Use the -pre flag when you install rom NuGet**.

To run this example in some working directory:
* Install ScriptCs.SignalR using Nuget (scriptcs installer does not currently support pre-release packages). `nuget install ScriptCs.SignalR -pre -o packages`
* Run `scriptcs -install`
* Paste the code below into your editor as save as `server.csx`
* Run `scriptcs server.csx`

```csharp
var signalR = Require<SignalR>();
signalR.CreateServer("http://localhost:8080");
Console.WriteLine("server created");
Console.ReadLine();

public class MyHub : Hub
{
	public void Send(string message)
	{
		Console.WriteLine("Responding to message: {0}", message);
		var strings = message.Split(new[]{' '});
		foreach (var s in strings) Clients.SendToAll("addMessage", s);
		Clients.SendToCaller("addMessage", "Hello caller! Thanks for sending " + message);	
	}
}
```