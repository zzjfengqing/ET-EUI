using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class ServerInfoConfigCategory : ProtoObject
    {
        public static ServerInfoConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, ServerInfoConfig> dict = new Dictionary<int, ServerInfoConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<ServerInfoConfig> list = new List<ServerInfoConfig>();
		
        public ServerInfoConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (ServerInfoConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public ServerInfoConfig Get(int id)
        {
            this.dict.TryGetValue(id, out ServerInfoConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (ServerInfoConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, ServerInfoConfig> GetAll()
        {
            return this.dict;
        }

        public ServerInfoConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class ServerInfoConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public string ServerName { get; set; }

	}
}
