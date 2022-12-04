using System;
using System.Collections.Generic;

namespace ET
{
    public static class RealmGateAddressHelper
    {
        public static StartSceneConfig GetGate(int zone, long accountId)
        {
            List<StartSceneConfig> zoneGates = StartSceneConfigCategory.Instance.Gates[zone];

            //int n = RandomHelper.RandomNumber(0, zoneGates.Count);
            int n = accountId.GetHashCode() % zoneGates.Count;

            return zoneGates[n];
        }

        public static StartSceneConfig GetRealm(int zone)
        {
            StartSceneConfig zoneRealm = StartSceneConfigCategory.Instance.Realms[zone];
            return zoneRealm;
        }
    }
}