using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public enum AccountType
    {
        /// <summary>
        /// 普通玩家
        /// </summary>
        General,

        /// <summary>
        /// 黑名单
        /// </summary>
        BlackList
    }

    public class Account : Entity, IAwake
    {
        /// <summary>
        /// 账户名
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 账户密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 账号创建时间
        /// </summary>
        public long CreateTime { get; set; }

        /// <summary>
        /// 账号类型
        /// </summary>
        public int AccountType { get; set; }
    }
}