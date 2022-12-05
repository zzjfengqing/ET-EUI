using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class R2G_GetLoginGateKeyHandler : AMActorRpcHandler<Scene, R2G_GetLoginGateKey, G2R_GetLoginGateKey>
    {
        protected override async ETTask Run(Scene scene, R2G_GetLoginGateKey request, G2R_GetLoginGateKey response, Action reply)
        {
            #region 校验

            //服务器类型校验
            if (scene.SceneType != SceneType.Gate)
            {
                Log.Error($"请求的Scene错误,当前Scene为:{scene.SceneType}");
                response.Error = ErrorCode.ERR_RequestSceneTypeError;
                reply();
                return;
            }

            #endregion 校验

            #region 发放本Gate登陆令牌

            string key = RandomHelper.RandInt64().ToString() + TimeHelper.ServerNow().ToString();
            //scene.GetComponent<GateSessionKeyComponent>().Remove(request.AccountId);
            scene.GetComponent<GateSessionKeyComponent>().Add(request.AccountId, key);
            response.GateSessionToken = key;
            reply();

            #endregion 发放本Gate登陆令牌

            await ETTask.CompletedTask;
        }
    }
}