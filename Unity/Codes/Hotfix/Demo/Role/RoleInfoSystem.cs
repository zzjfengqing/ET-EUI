using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class RoleInfoSystem
    {
        public static void FromNServerInfo(this RoleInfo self, NRoleInfo nServerInfo)
        {
            self.Id = nServerInfo.Id;
            self.Name = nServerInfo.Name;
            self.ServerId = nServerInfo.ServerId;
            self.Status = nServerInfo.Status;
            self.AccountId = nServerInfo.AccountId;
            self.LastLoginTIme = nServerInfo.LastLoginTIme;
            self.CreateTime = nServerInfo.CreateTime;
        }

        public static NRoleInfo ToNServerInfo(this RoleInfo self)
        {
            return new NRoleInfo()
            {
                Id = self.Id,
                Name = self.Name,
                ServerId = self.ServerId,
                Status = self.Status,
                AccountId = self.AccountId,
                LastLoginTIme = self.LastLoginTIme,
                CreateTime = self.CreateTime,
            };
        }
    }
}