using CloudDataProtection.Core.Messaging.Rpc;

namespace CloudDataProtection.Messaging.Server.Dto
{
    public class GetUserEmailRpcOutput : RpcResponse
    {
        public string Email { get; set; }
    }
}