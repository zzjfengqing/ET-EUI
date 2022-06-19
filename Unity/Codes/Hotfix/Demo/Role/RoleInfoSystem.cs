namespace ET
{
    public static class RoleInfoSystem
    {
        public static void FromMessage(this RoleInfo self, RoleInfoProto roleInfoProto)
        {
            self.Id = roleInfoProto.Id;
            self.AccountId = roleInfoProto.AccountId;
            self.Name = roleInfoProto.Name;
            self.State = roleInfoProto.State;
            self.ServerId = roleInfoProto.ServerId;
            self.LastLoginTime = roleInfoProto.LastLoginTime;
            self.CreateTime = roleInfoProto.CreateTime;
        }

        public static RoleInfoProto ToMessage(this  RoleInfo self)
        {
            return new RoleInfoProto()
            {
                Id = self.Id,
                Name = self.Name,
                State = self.State,
                AccountId = self.AccountId,
                LastLoginTime = self.LastLoginTime,
                CreateTime = self.CreateTime,
                ServerId = self.ServerId
            };
        }
    }
}