using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class C2A_GetServerInfoHandler : AMRpcHandler<C2A_GetServerInfos, A2C_GetServerInfos>
    {
        protected override async ETTask Run(Session session, C2A_GetServerInfos request, A2C_GetServerInfos response, Action reply)
        {
            //服务器校验
            Scene scene = session.DomainScene();
            if (scene.SceneType != SceneType.Account)
            {
                Log.Error($"请求的Scene错误,当前Scene为:{scene.SceneType}");
                session.Disconnect();
                return;
            }

            //令牌校验
            string token = scene.GetComponent<TokenComponent>().Get(request.AccountId);
            if (token is null || token != request.Token)
            {
                response.Error = ErrorCode.ERR_TokenError;
                reply();
                session?.Disconnect();
                return;
            }

            //加载服务器信息
            foreach (var serverInfo in scene.GetComponent<ServerInfoManagerComponent>().ServerInfos)
            {
                response.ServerInfos.Add(serverInfo.ToNServerInfo());
            }
            reply();
            await ETTask.CompletedTask;
        }
    }
}