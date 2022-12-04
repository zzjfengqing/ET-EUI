using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class A2R_GetRealmKeyHandler : AMActorRpcHandler<Scene, A2R_GetRealmKey, R2A_GetRealmKey>
    {
        protected override async ETTask Run(Scene scene, A2R_GetRealmKey request, R2A_GetRealmKey response, Action reply)
        {
            //服务器类型校验
            if (scene.SceneType != SceneType.Realm)
            {
                Log.Error($"请求的Scene错误,当前Scene为:{scene.SceneType}");
                response.Error = ErrorCode.ERR_RequestSceneTypeError;
                reply();
                return;
            }

            //发放登陆令牌
            string token = TimeHelper.ServerNow().ToString() + RandomHelper.RandInt64().ToString();
            //scene.GetComponent<TokenComponent>().Remove(request.AccountId);
            scene.GetComponent<TokenComponent>().Add(request.AccountId, token);
            response.RealmKey = token;
            reply();
            await ETTask.CompletedTask;
        }
    }
}