ScriptCs.SignalR
================

# What is it?
Some simple helpers for writing SignalR in C# scripts. The main purpose of this is to allow sending messages to clients without having to use the dynamic object, which is not supported by Roslyn (and therefore ScriptCs) at the time of writing.

#How do I get it?
* This package is available on Nuget under the name ScriptCs.SignalR as a pre-release package. Use the -pre flag.
* Install from NuGet: `NuGet install ScriptCS.SignalR`

#Quick start
Here's a complete ScriptCs example using our extensions. Because dynamic is only ever used in the compiled assembly this runs in Roslyn without issues.

We are currently using a pre-release package on NuGet due to a dependency on Owin hosting pre-release packages. **Use the -pre flag when you install rom NuGet**.

To run this example in some working directory:
* Install ScriptCs.SignalR using Nuget (scriptcs installer does not currently support pre-release packages). `nuget install ScriptCs.SignalR -pre -o packages`
* Run `scriptcs -install`
* Paste the server code below into your editor as save as `server.csx`
* Paste the client code below into your editor and save as `client.html`
* Run `scriptcs server.csx`

Server code:
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

Client code:
```html
  <script src="http://code.jquery.com/jquery-1.7.min.js" type="text/javascript"></script>
  <script src="script/signalr.js" type="text/javascript"></script>
  <script src="http://localhost:8080/signalr/hubs" type="text/javascript"></script>
  <script type="text/javascript">
  $(function () {

	  var conn = $.connection.hub;
	  conn.url = "http://localhost:8080/signalr";
	  var myhub = conn.proxies.myhub;
	  
	  conn.start()
		.done(function(state) {
			console.log("connection open: " + state.host);
			$("#broadcast").click(function () {
				myhub.server.send($('#msg').val());
          });
		})
		.fail(function(e) {
			console.log("connection failed: " + e);
		});	

	  myhub.client.addMessage = function (data) {
          $('#messages').append('<li>' + data + '</li>');
      };
		
  });
  </script>

  <input type="text" id="msg" />
  <input type="button" id="broadcast" value="broadcast" />

  <ul id="messages">
  </ul>
```