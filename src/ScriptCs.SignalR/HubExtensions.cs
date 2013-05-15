using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;

namespace ScriptCs.SignalR
{
    public static class HubExtensions
    {
        public static Task SendToAll(this HubConnectionContext hub, string messageName, object message)
        {
            return SendMessage(hub.All, messageName, message);
        }

        public static Task SendToCaller(this HubConnectionContext hub, string messageName, object message)
        {
            return SendMessage(hub.Caller, messageName, message);
        }

        private static Task SendMessage(dynamic obj, string messageName, object message)
        {
            var proxy = obj as IClientProxy;
            if (proxy != null)
            {
                return proxy.Invoke(messageName, message);
            }
            return null;
        }
    }
}