using System;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.CSharp.RuntimeBinder;

namespace ScriptCs.SignalR
{
    public static class HubExtensions
    {
        public static void SendToAll(this HubConnectionContext hub, string messageName, object message)
        {
            SendMessage(hub.All, messageName, message);
        }

        public static void SendToCaller(this HubConnectionContext hub, string messageName, object message)
        {
            SendMessage(hub.Caller, messageName, message);
        }

        private static void SendMessage(dynamic obj, string messageName, object message)
        {
            var dynamicObject = obj as DynamicObject;
            var callSiteBinder = Binder.InvokeMember(CSharpBinderFlags.None, messageName, Enumerable.Empty<Type>(), typeof(HubConnectionContext),
                                    new[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), 
                                            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) });
            var callSite = CallSite<Action<CallSite, object, object>>.Create(callSiteBinder);
            callSite.Target(callSite, dynamicObject, message);
        }
    }
}