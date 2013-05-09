ScriptCs.SignalR
================

# What is it?
Some simple helpers for writing SignalR in C# scripts. The main purpose of this is to allow sending messages to clients without having to use the dynamic object, which is not supported by Roslyn (and therefore ScriptCs) at the time of writing.

#How do I get it?
* This package is available on Nuget under the name ScriptCs.SignalR.
* Install from scriptcs: scriptcs -install ScriptCs.SignalR

#Quick start
Here's a complete ScriptCs example using our extensions. Because dynamic is only ever used in the compiled assembly this runs in Roslyn without issues.

```csharp
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using System.Web;
using System.Web.Routing;
using Owin; 
using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using ScriptCs.SignalR;


using (WebApplication.Start<Startup>("http://localhost:8080")) {
	Console.WriteLine("Server running...");
	Console.ReadLine();
}

public class Startup
{
	public void Configuration(IAppBuilder app)
	{     
		// Turn cross domain on 
		var config = new HubConfiguration { EnableCrossDomain = true };

		// This will map out to http://localhost:8080/signalr by default
		app.MapHubs(config);
	}
}
	
public class MyHub : Hub
{
	public void Send(string message)
	{
		Console.WriteLine("Repsonding to message: {0}", message);
		var strings = message.Split(new[]{' '});
		foreach (var s in strings) Clients.SendToAll("addMessage", s);
		Clients.SendToCaller("addMessage", "hello caller!");
	}
}
```