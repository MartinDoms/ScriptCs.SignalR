using ScriptCs.Contracts;

namespace ScriptCs.SignalR
{
    public class ScriptPack : IScriptPack
    {
        IScriptPackContext IScriptPack.GetContext()
        {
            return new SignalR();
        }

        void IScriptPack.Initialize(IScriptPackSession session)
        {
            session.ImportNamespace("Microsoft.Owin.Hosting");
            session.ImportNamespace("Microsoft.AspNet.SignalR");
        }

        void IScriptPack.Terminate()
        {
        }
    }
}