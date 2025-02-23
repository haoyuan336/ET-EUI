﻿using System.Collections.Generic;


namespace ET
{
	public static class RealmGateAddressHelper
	{
		public static StartSceneConfig GetGate(int zone, long accountId)
		{
			List<StartSceneConfig> zoneGates = StartSceneConfigCategory.Instance.Gates[zone];
			
			// int n = RandomHelper.RandomNumber(0, zoneGates.Count);
			int n = accountId.GetHashCode() % zoneGates.Count;
			
	
			return zoneGates[n];
		}

		public static StartSceneConfig GetReal(long serverId)
		{
			return StartSceneConfigCategory.Instance.Realms[(int)serverId];
		}
	}
}
