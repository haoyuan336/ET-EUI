using ET;
using ProtoBuf;
using System.Collections.Generic;
namespace ET
{
	[ResponseType(nameof(M2C_TestResponse))]
	[Message(OuterOpcode.C2M_TestRequest)]
	[ProtoContract]
	public partial class C2M_TestRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string request { get; set; }

		[ProtoMember(2)]
		public List<string> key = new List<string>();

		[ProtoMember(3)]
		public List<string> value = new List<string>();

	}

	[Message(OuterOpcode.M2C_TestResponse)]
	[ProtoContract]
	public partial class M2C_TestResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public string response { get; set; }

	}

	[ResponseType(nameof(Actor_TransferResponse))]
	[Message(OuterOpcode.Actor_TransferRequest)]
	[ProtoContract]
	public partial class Actor_TransferRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public int MapIndex { get; set; }

	}

	[Message(OuterOpcode.Actor_TransferResponse)]
	[ProtoContract]
	public partial class Actor_TransferResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_EnterMap))]
	[Message(OuterOpcode.C2G_EnterMap)]
	[ProtoContract]
	public partial class C2G_EnterMap: Object, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long Account { get; set; }

	}

	[Message(OuterOpcode.G2C_EnterMap)]
	[ProtoContract]
	public partial class G2C_EnterMap: Object, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

// 自己unitId
		[ProtoMember(4)]
		public long MyId { get; set; }

	}

	[Message(OuterOpcode.MoveInfo)]
	[ProtoContract]
	public partial class MoveInfo: Object
	{
		[ProtoMember(1)]
		public List<float> X = new List<float>();

		[ProtoMember(2)]
		public List<float> Y = new List<float>();

		[ProtoMember(3)]
		public List<float> Z = new List<float>();

		[ProtoMember(4)]
		public float A { get; set; }

		[ProtoMember(5)]
		public float B { get; set; }

		[ProtoMember(6)]
		public float C { get; set; }

		[ProtoMember(7)]
		public float W { get; set; }

		[ProtoMember(8)]
		public int TurnSpeed { get; set; }

	}

	[Message(OuterOpcode.UnitInfo)]
	[ProtoContract]
	public partial class UnitInfo: Object
	{
		[ProtoMember(1)]
		public long UnitId { get; set; }

		[ProtoMember(2)]
		public int ConfigId { get; set; }

		[ProtoMember(3)]
		public int Type { get; set; }

		[ProtoMember(4)]
		public float X { get; set; }

		[ProtoMember(5)]
		public float Y { get; set; }

		[ProtoMember(6)]
		public float Z { get; set; }

		[ProtoMember(7)]
		public float ForwardX { get; set; }

		[ProtoMember(8)]
		public float ForwardY { get; set; }

		[ProtoMember(9)]
		public float ForwardZ { get; set; }

		[ProtoMember(10)]
		public List<int> Ks = new List<int>();

		[ProtoMember(11)]
		public List<long> Vs = new List<long>();

		[ProtoMember(12)]
		public MoveInfo MoveInfo { get; set; }

	}

	[Message(OuterOpcode.M2C_CreateUnits)]
	[ProtoContract]
	public partial class M2C_CreateUnits: Object, IActorMessage
	{
		[ProtoMember(2)]
		public List<UnitInfo> Units = new List<UnitInfo>();

	}

	[Message(OuterOpcode.M2C_CreateMyUnit)]
	[ProtoContract]
	public partial class M2C_CreateMyUnit: Object, IActorMessage
	{
		[ProtoMember(1)]
		public UnitInfo Unit { get; set; }

	}

	[Message(OuterOpcode.M2C_StartSceneChange)]
	[ProtoContract]
	public partial class M2C_StartSceneChange: Object, IActorMessage
	{
		[ProtoMember(1)]
		public long SceneInstanceId { get; set; }

		[ProtoMember(2)]
		public string SceneName { get; set; }

	}

	[Message(OuterOpcode.M2C_RemoveUnits)]
	[ProtoContract]
	public partial class M2C_RemoveUnits: Object, IActorMessage
	{
		[ProtoMember(2)]
		public List<long> Units = new List<long>();

	}

	[Message(OuterOpcode.C2M_PathfindingResult)]
	[ProtoContract]
	public partial class C2M_PathfindingResult: Object, IActorLocationMessage
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public float X { get; set; }

		[ProtoMember(2)]
		public float Y { get; set; }

		[ProtoMember(3)]
		public float Z { get; set; }

	}

	[Message(OuterOpcode.C2M_Stop)]
	[ProtoContract]
	public partial class C2M_Stop: Object, IActorLocationMessage
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

	}

	[Message(OuterOpcode.M2C_PathfindingResult)]
	[ProtoContract]
	public partial class M2C_PathfindingResult: Object, IActorMessage
	{
		[ProtoMember(1)]
		public long Id { get; set; }

		[ProtoMember(2)]
		public float X { get; set; }

		[ProtoMember(3)]
		public float Y { get; set; }

		[ProtoMember(4)]
		public float Z { get; set; }

		[ProtoMember(5)]
		public List<float> Xs = new List<float>();

		[ProtoMember(6)]
		public List<float> Ys = new List<float>();

		[ProtoMember(7)]
		public List<float> Zs = new List<float>();

	}

	[Message(OuterOpcode.M2C_Stop)]
	[ProtoContract]
	public partial class M2C_Stop: Object, IActorMessage
	{
		[ProtoMember(1)]
		public int Error { get; set; }

		[ProtoMember(2)]
		public long Id { get; set; }

		[ProtoMember(3)]
		public float X { get; set; }

		[ProtoMember(4)]
		public float Y { get; set; }

		[ProtoMember(5)]
		public float Z { get; set; }

		[ProtoMember(6)]
		public float A { get; set; }

		[ProtoMember(7)]
		public float B { get; set; }

		[ProtoMember(8)]
		public float C { get; set; }

		[ProtoMember(9)]
		public float W { get; set; }

	}

	[ResponseType(nameof(G2C_Ping))]
	[Message(OuterOpcode.C2G_Ping)]
	[ProtoContract]
	public partial class C2G_Ping: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

	}

	[Message(OuterOpcode.G2C_Ping)]
	[ProtoContract]
	public partial class G2C_Ping: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public long Time { get; set; }

	}

	[Message(OuterOpcode.G2C_Test)]
	[ProtoContract]
	public partial class G2C_Test: Object, IMessage
	{
	}

	[ResponseType(nameof(M2C_Reload))]
	[Message(OuterOpcode.C2M_Reload)]
	[ProtoContract]
	public partial class C2M_Reload: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string Account { get; set; }

		[ProtoMember(2)]
		public string Password { get; set; }

	}

	[Message(OuterOpcode.M2C_Reload)]
	[ProtoContract]
	public partial class M2C_Reload: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(R2C_Login))]
	[Message(OuterOpcode.C2R_Login)]
	[ProtoContract]
	public partial class C2R_Login: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string Account { get; set; }

		[ProtoMember(2)]
		public string Password { get; set; }

	}

	[Message(OuterOpcode.R2C_Login)]
	[ProtoContract]
	public partial class R2C_Login: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public string Address { get; set; }

		[ProtoMember(2)]
		public long Key { get; set; }

		[ProtoMember(3)]
		public long GateId { get; set; }

	}

	[ResponseType(nameof(G2C_LoginGate))]
	[Message(OuterOpcode.C2G_LoginGate)]
	[ProtoContract]
	public partial class C2G_LoginGate: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Key { get; set; }

		[ProtoMember(2)]
		public long GateId { get; set; }

	}

	[Message(OuterOpcode.G2C_LoginGate)]
	[ProtoContract]
	public partial class G2C_LoginGate: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public long PlayerId { get; set; }

	}

	[Message(OuterOpcode.G2C_TestHotfixMessage)]
	[ProtoContract]
	public partial class G2C_TestHotfixMessage: Object, IMessage
	{
		[ProtoMember(1)]
		public string Info { get; set; }

	}

	[ResponseType(nameof(M2C_TestRobotCase))]
	[Message(OuterOpcode.C2M_TestRobotCase)]
	[ProtoContract]
	public partial class C2M_TestRobotCase: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public int N { get; set; }

	}

	[Message(OuterOpcode.M2C_TestRobotCase)]
	[ProtoContract]
	public partial class M2C_TestRobotCase: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public int N { get; set; }

	}

	[ResponseType(nameof(M2C_TransferMap))]
	[Message(OuterOpcode.C2M_TransferMap)]
	[ProtoContract]
	public partial class C2M_TransferMap: Object, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterOpcode.M2C_TransferMap)]
	[ProtoContract]
	public partial class M2C_TransferMap: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(R2C_LoginTest))]
	[Message(OuterOpcode.C2R_LoginTest)]
	[ProtoContract]
	public partial class C2R_LoginTest: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string Account { get; set; }

		[ProtoMember(2)]
		public string Password { get; set; }

	}

	[Message(OuterOpcode.R2C_LoginTest)]
	[ProtoContract]
	public partial class R2C_LoginTest: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public string GateAddress { get; set; }

		[ProtoMember(2)]
		public string key { get; set; }

	}

	[Message(OuterOpcode.C2R_SayHello)]
	[ProtoContract]
	public partial class C2R_SayHello: Object, IMessage
	{
		[ProtoMember(1)]
		public string Hello { get; set; }

	}

	[Message(OuterOpcode.R2C_SayGoodBye)]
	[ProtoContract]
	public partial class R2C_SayGoodBye: Object, IMessage
	{
		[ProtoMember(1)]
		public string GoodBye { get; set; }

	}

	[ResponseType(nameof(M2C_TestActorLocationResponse))]
	[Message(OuterOpcode.C2M_TestActorLocationRequest)]
	[ProtoContract]
	public partial class C2M_TestActorLocationRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string Content { get; set; }

	}

	[Message(OuterOpcode.M2C_TestActorLocationResponse)]
	[ProtoContract]
	public partial class M2C_TestActorLocationResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public string Content { get; set; }

	}

	[Message(OuterOpcode.C2M_TestActorLocationMessage)]
	[ProtoContract]
	public partial class C2M_TestActorLocationMessage: Object, IActorLocationMessage
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string Content { get; set; }

	}

	[Message(OuterOpcode.M2C_TestActorMessage)]
	[ProtoContract]
	public partial class M2C_TestActorMessage: Object, IActorMessage
	{
		[ProtoMember(1)]
		public string Content { get; set; }

	}

// message C2M_MatchRoomActorLocationMessage //IActorLocationMessage
// {
// 	int32 RpcId = 90;
// 	string Content = 1;
// }
// message M2C_SyncCurrentMatchingCount	//IActorMessage
// {
// 	int32 Content = 1;
// }
	[ResponseType(nameof(A2C_LoginAccount))]
	[Message(OuterOpcode.C2A_LoginAccount)]
	[ProtoContract]
	public partial class C2A_LoginAccount: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string AccountName { get; set; }

		[ProtoMember(2)]
		public string Password { get; set; }

	}

	[Message(OuterOpcode.A2C_LoginAccount)]
	[ProtoContract]
	public partial class A2C_LoginAccount: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public string Token { get; set; }

		[ProtoMember(2)]
		public long AccountId { get; set; }

	}

	[Message(OuterOpcode.ServerInfoProto)]
	[ProtoContract]
	public partial class ServerInfoProto: Object
	{
		[ProtoMember(1)]
		public long Id { get; set; }

		[ProtoMember(2)]
		public int Status { get; set; }

		[ProtoMember(3)]
		public string ServerName { get; set; }

	}

	[ResponseType(nameof(A2C_GetServerInfo))]
	[Message(OuterOpcode.C2A_GetServerInfo)]
	[ProtoContract]
	public partial class C2A_GetServerInfo: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string Token { get; set; }

		[ProtoMember(2)]
		public long AccountId { get; set; }

	}

	[Message(OuterOpcode.A2C_GetServerInfo)]
	[ProtoContract]
	public partial class A2C_GetServerInfo: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<ServerInfoProto> ServerInfoList = new List<ServerInfoProto>();

	}

	[ResponseType(nameof(A2C_GetRealmKey))]
	[Message(OuterOpcode.C2A_GetRealmKey)]
	[ProtoContract]
	public partial class C2A_GetRealmKey: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string Token { get; set; }

		[ProtoMember(2)]
		public long ServerId { get; set; }

		[ProtoMember(3)]
		public long AccountId { get; set; }

	}

	[Message(OuterOpcode.A2C_GetRealmKey)]
	[ProtoContract]
	public partial class A2C_GetRealmKey: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public string RealmKey { get; set; }

		[ProtoMember(2)]
		public string RealmAddress { get; set; }

	}

	[ResponseType(nameof(R2A_GetRealmKey))]
	[Message(OuterOpcode.A2R_GetRealmKey)]
	[ProtoContract]
	public partial class A2R_GetRealmKey: Object, IActorRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

	}

	[Message(OuterOpcode.R2A_GetRealmKey)]
	[ProtoContract]
	public partial class R2A_GetRealmKey: Object, IActorResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public string Token { get; set; }

	}

	[ResponseType(nameof(R2C_LoginRealm))]
	[Message(OuterOpcode.C2R_LoginRealm)]
	[ProtoContract]
	public partial class C2R_LoginRealm: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(2)]
		public string Token { get; set; }

	}

	[Message(OuterOpcode.R2C_LoginRealm)]
	[ProtoContract]
	public partial class R2C_LoginRealm: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public string GateAddress { get; set; }

		[ProtoMember(2)]
		public long GateKey { get; set; }

	}

	[ResponseType(nameof(G2C_LoginGate))]
	[Message(OuterOpcode.C2G_LoginGateRequeset)]
	[ProtoContract]
	public partial class C2G_LoginGateRequeset: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long GateKey { get; set; }

		[ProtoMember(2)]
		public long AccountId { get; set; }

	}

	[Message(OuterOpcode.G2C_LoginGateResponse)]
	[ProtoContract]
	public partial class G2C_LoginGateResponse: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public long PlayerId { get; set; }

	}

	[ResponseType(nameof(M2C_MatchRoomResponse))]
	[Message(OuterOpcode.C2M_MatchRoomRequest)]
	[ProtoContract]
	public partial class C2M_MatchRoomRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

	}

	[Message(OuterOpcode.M2C_MatchRoomResponse)]
	[ProtoContract]
	public partial class M2C_MatchRoomResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_CancelMatchRoomResponse))]
	[Message(OuterOpcode.C2M_CancelMatchRoomRequest)]
	[ProtoContract]
	public partial class C2M_CancelMatchRoomRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

	}

	[Message(OuterOpcode.M2C_CancelMatchRoomResponse)]
	[ProtoContract]
	public partial class M2C_CancelMatchRoomResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[Message(OuterOpcode.DiamondInfo)]
	[ProtoContract]
	public partial class DiamondInfo: Object
	{
		[ProtoMember(1)]
		public long Id { get; set; }

		[ProtoMember(2)]
		public int HangIndex { get; set; }

		[ProtoMember(3)]
		public int LieIndex { get; set; }

		[ProtoMember(4)]
		public int DiamondType { get; set; }

		[ProtoMember(5)]
		public int InitLieIndex { get; set; }

		[ProtoMember(6)]
		public int InitHangIndex { get; set; }

		[ProtoMember(7)]
		public int BoomType { get; set; }

// int64 HeroCardId = 8;
// float HeroCardAddAttack = 9;
// float HeroCardEndAttack = 10;
// float HeroCardAddAngry = 11;
// float HeroCardEndAngry = 12;
		[ProtoMember(8)]
		public HeroCardInfo HeroCardInfo { get; set; }

		[ProtoMember(9)]
		public int ConfigId { get; set; }

	}

	[Message(OuterOpcode.M2C_InitMapData)]
	[ProtoContract]
	public partial class M2C_InitMapData: Object, IActorMessage
	{
//初始化地图数据
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public List<DiamondInfo> DiamondInfo = new List<DiamondInfo>();

	}

//同步房间信息
	[Message(OuterOpcode.M2C_SyncRoomInfo)]
	[ProtoContract]
	public partial class M2C_SyncRoomInfo: Object, IActorMessage
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long RoomId { get; set; }

		[ProtoMember(2)]
		public int TurnIndex { get; set; }

		[ProtoMember(3)]
		public int MySeatIndex { get; set; }

		[ProtoMember(4)]
		public int SeatCount { get; set; }

		[ProtoMember(5)]
		public int LevelNum { get; set; }

	}

	[Message(OuterOpcode.C2M_PlayerScrollScreen)]
	[ProtoContract]
	public partial class C2M_PlayerScrollScreen: Object, IActorLocationMessage
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public int StartX { get; set; }

		[ProtoMember(2)]
		public int StartY { get; set; }

		[ProtoMember(3)]
		public int DirType { get; set; }

		[ProtoMember(4)]
		public long RoomId { get; set; }

	}

	[Message(OuterOpcode.C2M_PlayerClickHeroMode)]
	[ProtoContract]
	public partial class C2M_PlayerClickHeroMode: Object, IActorLocationMessage
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long HeroId { get; set; }

		[ProtoMember(2)]
		public long RoomId { get; set; }

	}

	[Message(OuterOpcode.M2C_PlayerChooseAttackHero)]
	[ProtoContract]
	public partial class M2C_PlayerChooseAttackHero: Object, IActorMessage
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long HeroId { get; set; }

	}

	[Message(OuterOpcode.M2C_ChangeCurrentTurnSeatIndex)]
	[ProtoContract]
	public partial class M2C_ChangeCurrentTurnSeatIndex: Object, IActorMessage
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public int CurrentTurnIndex { get; set; }

	}

	[Message(OuterOpcode.DiamondAction)]
	[ProtoContract]
	public partial class DiamondAction: Object
	{
//宝石的action
		[ProtoMember(1)]
		public int ActionType { get; set; }

		[ProtoMember(2)]
		public DiamondInfo DiamondInfo { get; set; }

// repeated AddItemAction AddAttackActions = 3;
		[ProtoMember(4)]
		public List<AddItemAction> AddAngryActions = new List<AddItemAction>();

	}

	[Message(OuterOpcode.MakeSureAttackHeroAction)]
	[ProtoContract]
	public partial class MakeSureAttackHeroAction: Object
	{
//确定发动攻击的英雄
		[ProtoMember(1)]
		public HeroCardDataComponentInfo HeroCardDataComponentInfo { get; set; }

	}

	[Message(OuterOpcode.DiamondActionItem)]
	[ProtoContract]
	public partial class DiamondActionItem: Object
	{
		[ProtoMember(1)]
		public List<DiamondAction> DiamondActions = new List<DiamondAction>();

		[ProtoMember(2)]
		public int CrashType { get; set; }

// repeated AddItemAction AddAttackItemActions = 2;
// repeated AddItemAction AddAngryItemActions = 3;
		[ProtoMember(4)]
		public List<MakeSureAttackHeroAction> MakeSureAttackHeroActions = new List<MakeSureAttackHeroAction>();

	}

	[Message(OuterOpcode.GameLoseResultAction)]
	[ProtoContract]
	public partial class GameLoseResultAction: Object
	{
		[ProtoMember(1)]
		public long LoseAccountId { get; set; }

	}

	[Message(OuterOpcode.RecoveryAction)]
	[ProtoContract]
	public partial class RecoveryAction: Object
	{
		[ProtoMember(1)]
		public HeroCardDataComponentInfo HeroCardDataComponentInfo { get; set; }

	}

	[Message(OuterOpcode.AttackAction)]
	[ProtoContract]
	public partial class AttackAction: Object
	{
// HeroCardInfo AttackHeroCardInfo = 1;
// HeroCardInfo BeAttackHeroCardInfo = 2;
		[ProtoMember(2)]
		public List<HeroBuffInfo> HeroBuffInfos = new List<HeroBuffInfo>();

		[ProtoMember(3)]
		public List<HeroCardDataComponentInfo> BeAttackHeroCardDataComponentInfos = new List<HeroCardDataComponentInfo>();

		[ProtoMember(4)]
		public HeroCardDataComponentInfo AttackHeroCardDataComponentInfo { get; set; }

	}

	[Message(OuterOpcode.HeroBuffInfo)]
	[ProtoContract]
	public partial class HeroBuffInfo: Object
	{
		[ProtoMember(2)]
		public long HeroId { get; set; }

		[ProtoMember(1)]
		public List<BuffInfo> BuffInfos = new List<BuffInfo>();

	}

	[Message(OuterOpcode.AttackActionItem)]
	[ProtoContract]
	public partial class AttackActionItem: Object
	{
		[ProtoMember(1)]
		public List<AttackAction> AttackActions = new List<AttackAction>();

		[ProtoMember(2)]
		public List<HeroBuffInfo> HeroBuffInfos = new List<HeroBuffInfo>();

	}

	[Message(OuterOpcode.AddItemAction)]
	[ProtoContract]
	public partial class AddItemAction: Object
	{
		[ProtoMember(1)]
		public List<DiamondInfo> DiamondInfos = new List<DiamondInfo>();

		[ProtoMember(2)]
		public HeroCardInfo HeroCardInfo { get; set; }

		[ProtoMember(3)]
		public HeroCardDataComponentInfo HeroCardDataComponentInfo { get; set; }

		[ProtoMember(4)]
		public CrashCommonInfo CrashCommonInfo { get; set; }

	}

	[Message(OuterOpcode.InitHeroCardDataActionItem)]
	[ProtoContract]
	public partial class InitHeroCardDataActionItem: Object
	{
//初始化英雄卡牌的数据
		[ProtoMember(1)]
		public HeroCardDataComponentInfo HeroCardDataComponentInfo { get; set; }

	}

	[Message(OuterOpcode.ComboActionItem)]
	[ProtoContract]
	public partial class ComboActionItem: Object
	{
		[ProtoMember(1)]
		public List<AddItemAction> AddAttackActions = new List<AddItemAction>();

	}

	[Message(OuterOpcode.AddRoundAngryItem)]
	[ProtoContract]
	public partial class AddRoundAngryItem: Object
	{
//增加每回合的怒气值
		[ProtoMember(1)]
		public List<HeroCardDataComponentInfo> HeroCardDataComponentInfos = new List<HeroCardDataComponentInfo>();

	}

	[Message(OuterOpcode.UpdateHeroBuffInfoItem)]
	[ProtoContract]
	public partial class UpdateHeroBuffInfoItem: Object
	{
		[ProtoMember(1)]
		public List<HeroBuffInfo> HeroBuffInfos = new List<HeroBuffInfo>();

	}

	[Message(OuterOpcode.M2C_SyncDiamondAction)]
	[ProtoContract]
	public partial class M2C_SyncDiamondAction: Object, IActorMessage
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

// repeated DiamondActionItem DiamondActionItems = 1;
// repeated AttackActionItem AttackActionItems = 2;
// // repeated InitHeroCardDataActionItem InitHeroCardDataActionItems = 3;
// GameLoseResultAction GameLoseResultAction = 4;
// ComboActionItem ComboActionItem = 5;
// AddRoundAngryItem AddRoundAngryItem = 6;
		[ProtoMember(7)]
		public ActionMessage ActionMessage { get; set; }

// UpdateHeroBuffInfoItem UpdateHeroBuffInfoItem = 8;	//更新英雄信息
// repeated ActionMessage ActionMessages = 7;
// UpdateHeroBuffInfoItem UpdateHeroBuffInfoItem = 7;
// repeated AddItemAction AddAttackItemActions = 4;
// repeated AddItemAction AddAngryItemActions = 5;
	}

// message MakeSureAttackHeroAction
// {
// 	repeated int64 HeroIds = 1;
// }
	[Message(OuterOpcode.CombeAction)]
	[ProtoContract]
	public partial class CombeAction: Object
	{
		[ProtoMember(1)]
		public int CombeTime { get; set; }

		[ProtoMember(2)]
		public int CrashCount { get; set; }

		[ProtoMember(3)]
		public int CrashDiamondType { get; set; }

	}

	[Message(OuterOpcode.UpdateHeroInfoAction)]
	[ProtoContract]
	public partial class UpdateHeroInfoAction: Object
	{
		[ProtoMember(1)]
		public HeroCardDataComponentInfo HeroCardDataComponentInfo { get; set; }

// repeated BuffInfo BuffInfos = 2;
	}

	[Message(OuterOpcode.UpdateHeroBuffInfo)]
	[ProtoContract]
	public partial class UpdateHeroBuffInfo: Object
	{
		[ProtoMember(1)]
		public long HeroId { get; set; }

		[ProtoMember(3)]
		public HeroCardDataComponentInfo HeroCardDataComponentInfo { get; set; }

		[ProtoMember(2)]
		public List<BuffInfo> BuffInfos = new List<BuffInfo>();

	}

	[Message(OuterOpcode.HideAttackMarkAction)]
	[ProtoContract]
	public partial class HideAttackMarkAction: Object
	{
		[ProtoMember(1)]
		public HeroCardDataComponentInfo HeroCardDataComponentInfo { get; set; }

	}

	[Message(OuterOpcode.AttackBeganAction)]
	[ProtoContract]
	public partial class AttackBeganAction: Object
	{
	}

	[Message(OuterOpcode.AttackEndAction)]
	[ProtoContract]
	public partial class AttackEndAction: Object
	{
	}

	[Message(OuterOpcode.BuffDamageAction)]
	[ProtoContract]
	public partial class BuffDamageAction: Object
	{
		[ProtoMember(1)]
		public BuffInfo BuffInfo { get; set; }

		[ProtoMember(2)]
		public HeroCardDataComponentInfo HeroCardDataComponentInfo { get; set; }

		[ProtoMember(3)]
		public int DamageCount { get; set; }

	}

	[Message(OuterOpcode.ActionMessage)]
	[ProtoContract]
	public partial class ActionMessage: Object
	{
// int32 ActionType = 1;	//动作类型
		[ProtoMember(1)]
		public int PlayType { get; set; }

// DiamondActionItem DiamondActionItem = 2; //宝石动作
// AttackActionItem AttackActionItems = 3;	//攻击动作
// GameLoseResultAction GameLoseResultAction = 4;	//游戏结果
// ComboActionItem ComboActionItem = 5;	//连击特效
// AddRoundAngryItem AddRoundAngryItem = 6;	//增加怒气
		[ProtoMember(2)]
		public DiamondAction DiamondAction { get; set; }

		[ProtoMember(3)]
		public AttackAction AttackAction { get; set; }

		[ProtoMember(4)]
		public MakeSureAttackHeroAction MakeSureAttackHeroAction { get; set; }

		[ProtoMember(5)]
		public CombeAction CombeAction { get; set; }

		[ProtoMember(6)]
		public UpdateHeroInfoAction UpdateHeroInfoAction { get; set; }

		[ProtoMember(7)]
		public List<ActionMessage> ActionMessages = new List<ActionMessage>();

		[ProtoMember(8)]
		public GameLoseResultAction GameLoseResultAction { get; set; }

		[ProtoMember(9)]
		public UpdateHeroBuffInfo UpdateHeroBuffInfo { get; set; }

		[ProtoMember(10)]
		public HideAttackMarkAction HideAttackMarkAction { get; set; }

		[ProtoMember(11)]
		public AttackBeganAction AttackBeganAction { get; set; }

		[ProtoMember(12)]
		public AttackEndAction AttackEndAction { get; set; }

		[ProtoMember(13)]
		public BuffDamageAction BuffDamageAction { get; set; }

		[ProtoMember(14)]
		public RecoveryAction RecoveryAction { get; set; }

		[ProtoMember(15)]
		public AdditionDamageAction AdditionDamageAction { get; set; }

	}

	[Message(OuterOpcode.AdditionDamageAction)]
	[ProtoContract]
	public partial class AdditionDamageAction: Object
	{
		[ProtoMember(1)]
		public HeroCardDataComponentInfo HeroCardDataComponentInfo { get; set; }

	}

	[Message(OuterOpcode.SkillInfo)]
	[ProtoContract]
	public partial class SkillInfo: Object
	{
//技能类型
		[ProtoMember(1)]
		public long SkillId { get; set; }

		[ProtoMember(2)]
		public string SkillName { get; set; }

		[ProtoMember(3)]
		public int SkillType { get; set; }

		[ProtoMember(4)]
		public int SkillConfigId { get; set; }

		[ProtoMember(5)]
		public string SkillAnimName { get; set; }

		[ProtoMember(6)]
		public long OwnerId { get; set; }

	}

	[Message(OuterOpcode.HeroCardInfo)]
	[ProtoContract]
	public partial class HeroCardInfo: Object
	{
		[ProtoMember(1)]
		public long HeroId { get; set; }

		[ProtoMember(2)]
		public string HeroName { get; set; }

		[ProtoMember(3)]
		public int ConfigId { get; set; }

		[ProtoMember(4)]
		public long OwnerId { get; set; }

		[ProtoMember(5)]
		public long TroopId { get; set; }

		[ProtoMember(6)]
		public int InTroopIndex { get; set; }

		[ProtoMember(7)]
		public int CampIndex { get; set; }

		[ProtoMember(8)]
		public int HeroColor { get; set; }

// int64 CastSkillId = 9;	//技能类型
// float Attack = 10;	//当前的攻击力
// float HP = 11;			//血量
// repeated SkillInfo SkillInfos = 12; //技能列表
// float DiamondAttack = 13;			//宝石攻击力
// float Angry = 14;			//怒气值
// float Defence = 15;			//防御值
		[ProtoMember(16)]
		public int Level { get; set; }

// float TotalHP = 17;	//总血量
		[ProtoMember(18)]
		public int Star { get; set; }

		[ProtoMember(19)]
		public int Count { get; set; }

		[ProtoMember(20)]
		public HeroCardDataComponentInfo HeroCardDataComponentInfo { get; set; }

		[ProtoMember(21)]
		public int Rank { get; set; }

		[ProtoMember(22)]
		public int CurrentExp { get; set; }

		[ProtoMember(23)]
		public bool IsLock { get; set; }

// repeated long SkillIdList = 11;	//技能id
	}

	[ResponseType(nameof(M2C_GetAllHeroCardListResponse))]
	[Message(OuterOpcode.C2M_GetAllHeroCardListRequest)]
	[ProtoContract]
	public partial class C2M_GetAllHeroCardListRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Account { get; set; }

		[ProtoMember(3)]
		public string Token { get; set; }

		[ProtoMember(4)]
		public int BagType { get; set; }

	}

	[Message(OuterOpcode.M2C_GetAllHeroCardListResponse)]
	[ProtoContract]
	public partial class M2C_GetAllHeroCardListResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<HeroCardInfo> HeroCardInfos = new List<HeroCardInfo>();

	}

	[ResponseType(nameof(M2C_CallHeroCardResponse))]
	[Message(OuterOpcode.C2M_CallHeroCardRequest)]
	[ProtoContract]
	public partial class C2M_CallHeroCardRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Account { get; set; }

	}

	[Message(OuterOpcode.M2C_CallHeroCardResponse)]
	[ProtoContract]
	public partial class M2C_CallHeroCardResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public HeroCardInfo HeroCardInfo { get; set; }

	}

	[Message(OuterOpcode.TroopInfo)]
	[ProtoContract]
	public partial class TroopInfo: Object
	{
		[ProtoMember(1)]
		public long TroopId { get; set; }

		[ProtoMember(2)]
		public int Index { get; set; }

	}

	[ResponseType(nameof(M2C_GetAllTroopInfosResponse))]
	[Message(OuterOpcode.C2M_GetAllTroopInfosRequest)]
	[ProtoContract]
	public partial class C2M_GetAllTroopInfosRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Account { get; set; }

	}

	[Message(OuterOpcode.M2C_GetAllTroopInfosResponse)]
	[ProtoContract]
	public partial class M2C_GetAllTroopInfosResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<TroopInfo> TroopInfos = new List<TroopInfo>();

	}

	[ResponseType(nameof(M2C_CreateTroopResponse))]
	[Message(OuterOpcode.C2M_CreateTroopRequest)]
	[ProtoContract]
	public partial class C2M_CreateTroopRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

	}

	[Message(OuterOpcode.M2C_CreateTroopResponse)]
	[ProtoContract]
	public partial class M2C_CreateTroopResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public TroopInfo TroopInfo { get; set; }

	}

	[ResponseType(nameof(M2C_GetCurrentTroopHeroInfosResponse))]
	[Message(OuterOpcode.C2M_GetCurrentTroopHeroInfosRequest)]
	[ProtoContract]
	public partial class C2M_GetCurrentTroopHeroInfosRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

	}

	[Message(OuterOpcode.M2C_GetCurrentTroopHeroInfosResponse)]
	[ProtoContract]
	public partial class M2C_GetCurrentTroopHeroInfosResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<HeroCardInfo> HeroCardInfos = new List<HeroCardInfo>();

	}

	[ResponseType(nameof(M2C_SetHeroToTroopResponse))]
	[Message(OuterOpcode.C2M_SetHeroToTroopRequest)]
	[ProtoContract]
	public partial class C2M_SetHeroToTroopRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

// int64 TroopId = 1;
		[ProtoMember(2)]
		public long HeroId { get; set; }

// int64 AccountId = 3;
// int32 Index = 3;
	}

	[Message(OuterOpcode.M2C_SetHeroToTroopResponse)]
	[ProtoContract]
	public partial class M2C_SetHeroToTroopResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<HeroCardInfo> HeroCardInfos = new List<HeroCardInfo>();

	}

	[ResponseType(nameof(M2C_UnSetHeroFromTroopResponse))]
	[Message(OuterOpcode.C2M_UnSetHeroFromTroopRequest)]
	[ProtoContract]
	public partial class C2M_UnSetHeroFromTroopRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

// int64 TroopId = 1;
		[ProtoMember(2)]
		public long HeroId { get; set; }

// int64 AccountId = 3;
// int32 InTroopIndex = 3;
	}

	[Message(OuterOpcode.M2C_UnSetHeroFromTroopResponse)]
	[ProtoContract]
	public partial class M2C_UnSetHeroFromTroopResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<HeroCardInfo> HeroCardInfos = new List<HeroCardInfo>();

	}

	[ResponseType(nameof(M2C_StartPVEGameResponse))]
	[Message(OuterOpcode.C2M_StartPVEGameRequest)]
	[ProtoContract]
	public partial class C2M_StartPVEGameRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(2)]
		public long TroopId { get; set; }

	}

	[Message(OuterOpcode.M2C_StartPVEGameResponse)]
	[ProtoContract]
	public partial class M2C_StartPVEGameResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[Message(OuterOpcode.C2M_GameReadyMessage)]
	[ProtoContract]
	public partial class C2M_GameReadyMessage: Object, IActorLocationMessage
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

	}

	[Message(OuterOpcode.M2C_CreateHeroCardInRoom)]
	[ProtoContract]
	public partial class M2C_CreateHeroCardInRoom: Object, IActorMessage
	{
		[ProtoMember(1)]
		public List<HeroCardInfo> HeroCardInfos = new List<HeroCardInfo>();

		[ProtoMember(2)]
		public List<HeroCardDataComponentInfo> HeroCardDataComponentInfos = new List<HeroCardDataComponentInfo>();

		[ProtoMember(3)]
		public List<SkillInfo> SkillInfos = new List<SkillInfo>();

	}

// message C2M_PlayerReadyTurnRequest //IActorLocationRequest
// {
// 	int32 RpcId = 90;
// 	int64 AccountId = 1;
// 	int64 RoomId = 2;	//房间iD
// }
// message M2C_PlayerReadyTurnResponse //IActorLocationResponse
// {
// 	int32 RpcId = 90;
// 	int32 Error = 91;
// 	string Message = 92;
// }
	[Message(OuterOpcode.M2C_SyncHeroCardTurnData)]
	[ProtoContract]
	public partial class M2C_SyncHeroCardTurnData: Object, IActorMessage
	{
		[ProtoMember(1)]
		public List<HeroCardInfo> HeroCardInfos = new List<HeroCardInfo>();

	}

	[ResponseType(nameof(M2C_AddItemResponse))]
	[Message(OuterOpcode.C2M_AddItemRequest)]
	[ProtoContract]
	public partial class C2M_AddItemRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(2)]
		public int Count { get; set; }

		[ProtoMember(3)]
		public int ConfigId { get; set; }

	}

	[Message(OuterOpcode.M2C_AddItemResponse)]
	[ProtoContract]
	public partial class M2C_AddItemResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public ItemInfo ItemInfo { get; set; }

	}

	[ResponseType(nameof(M2C_GetGoldInfoResponse))]
	[Message(OuterOpcode.C2M_GetGoldInfoRequest)]
	[ProtoContract]
	public partial class C2M_GetGoldInfoRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

	}

	[Message(OuterOpcode.ItemInfo)]
	[ProtoContract]
	public partial class ItemInfo: Object
	{
		[ProtoMember(1)]
		public long ItemId { get; set; }

		[ProtoMember(2)]
		public int ConfigId { get; set; }

		[ProtoMember(3)]
		public int Count { get; set; }

	}

	[Message(OuterOpcode.M2C_GetGoldInfoResponse)]
	[ProtoContract]
	public partial class M2C_GetGoldInfoResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

// int32 GoldCount = 1;
// int32 PowerCount = 2;
// int32 DiamondCount = 3;
		[ProtoMember(1)]
		public List<ItemInfo> ItemInfos = new List<ItemInfo>();

	}

	[ResponseType(nameof(M2C_GetUserExpInfoResponse))]
	[Message(OuterOpcode.C2M_GetUserExpInfoRequest)]
	[ProtoContract]
	public partial class C2M_GetUserExpInfoRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

	}

	[Message(OuterOpcode.M2C_GetUserExpInfoResponse)]
	[ProtoContract]
	public partial class M2C_GetUserExpInfoResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public int Exp { get; set; }

		[ProtoMember(2)]
		public int UserLevel { get; set; }

		[ProtoMember(3)]
		public string UserName { get; set; }

	}

	[ResponseType(nameof(M2C_BackGameToMainMenuResponse))]
	[Message(OuterOpcode.C2M_BackGameToMainMenuRequest)]
	[ProtoContract]
	public partial class C2M_BackGameToMainMenuRequest: Object, IActorLocationRequest
	{
//退出游戏进入主页面
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Account { get; set; }

		[ProtoMember(2)]
		public long RoomId { get; set; }

	}

	[Message(OuterOpcode.M2C_BackGameToMainMenuResponse)]
	[ProtoContract]
	public partial class M2C_BackGameToMainMenuResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[Message(OuterOpcode.WeaponInfo)]
	[ProtoContract]
	public partial class WeaponInfo: Object
	{
		[ProtoMember(1)]
		public long WeaponId { get; set; }

		[ProtoMember(2)]
		public int ConfigId { get; set; }

		[ProtoMember(3)]
		public int Count { get; set; }

		[ProtoMember(4)]
		public int Level { get; set; }

		[ProtoMember(5)]
		public long OnWeaponHeroId { get; set; }

		[ProtoMember(6)]
		public int CurrentExp { get; set; }

	}

	[Message(OuterOpcode.GoodsInfo)]
	[ProtoContract]
	public partial class GoodsInfo: Object
	{
		[ProtoMember(1)]
		public long GoodsId { get; set; }

		[ProtoMember(2)]
		public int ConfigId { get; set; }

	}

	[ResponseType(nameof(M2C_BuyGoodsResponse))]
	[Message(OuterOpcode.C2M_BuyGoodsRequest)]
	[ProtoContract]
	public partial class C2M_BuyGoodsRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(2)]
		public int Count { get; set; }

		[ProtoMember(3)]
		public int ConfigId { get; set; }

	}

	[Message(OuterOpcode.M2C_BuyGoodsResponse)]
	[ProtoContract]
	public partial class M2C_BuyGoodsResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public GoodsInfo GoodsInfo { get; set; }

		[ProtoMember(2)]
		public ItemInfo ItemInfo { get; set; }

	}

	[ResponseType(nameof(M2C_BuyWeaponsResponse))]
	[Message(OuterOpcode.C2M_BuyWeaponsRequest)]
	[ProtoContract]
	public partial class C2M_BuyWeaponsRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(2)]
		public int Count { get; set; }

		[ProtoMember(3)]
		public int ConfigId { get; set; }

	}

	[Message(OuterOpcode.M2C_BuyWeaponsResponse)]
	[ProtoContract]
	public partial class M2C_BuyWeaponsResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(2)]
		public WeaponInfo WeaponInfo { get; set; }

	}

	[ResponseType(nameof(M2C_GetAllWeaponsResponse))]
	[Message(OuterOpcode.C2M_GetAllWeaponsRequest)]
	[ProtoContract]
	public partial class C2M_GetAllWeaponsRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

	}

	[Message(OuterOpcode.M2C_GetAllWeaponsResponse)]
	[ProtoContract]
	public partial class M2C_GetAllWeaponsResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<WeaponInfo> WeaponInfos = new List<WeaponInfo>();

	}

	[ResponseType(nameof(M2C_StrenthenHeroResponse))]
	[Message(OuterOpcode.C2M_StrenthenHeroRequest)]
	[ProtoContract]
	public partial class C2M_StrenthenHeroRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(2)]
		public HeroCardInfo TargetHeroCardInfo { get; set; }

		[ProtoMember(3)]
		public List<HeroCardInfo> ChooseHeroCardInfos = new List<HeroCardInfo>();

	}

	[Message(OuterOpcode.M2C_StrenthenHeroResponse)]
	[ProtoContract]
	public partial class M2C_StrenthenHeroResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

// repeated WeaponInfo WeaponInfos = 1;
		[ProtoMember(1)]
		public HeroCardInfo HeroCardInfo { get; set; }

	}

	[ResponseType(nameof(M2C_GetAllItemResponse))]
	[Message(OuterOpcode.C2M_GetAllItemRequest)]
	[ProtoContract]
	public partial class C2M_GetAllItemRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

	}

	[Message(OuterOpcode.M2C_GetAllItemResponse)]
	[ProtoContract]
	public partial class M2C_GetAllItemResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<ItemInfo> ItemInfos = new List<ItemInfo>();

	}

	[ResponseType(nameof(M2C_GetItemInfoResponse))]
	[Message(OuterOpcode.C2M_GetItemInfoRequest)]
	[ProtoContract]
	public partial class C2M_GetItemInfoRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(2)]
		public int ConfigId { get; set; }

	}

	[Message(OuterOpcode.M2C_GetItemInfoResponse)]
	[ProtoContract]
	public partial class M2C_GetItemInfoResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public ItemInfo ItemInfo { get; set; }

	}

	[Message(OuterOpcode.CrashCommonInfo)]
	[ProtoContract]
	public partial class CrashCommonInfo: Object
	{
		[ProtoMember(1)]
		public int FirstCrashCount { get; set; }

		[ProtoMember(2)]
		public int CommonCount { get; set; }

		[ProtoMember(3)]
		public int FirstCrashColor { get; set; }

	}

	[Message(OuterOpcode.HeroCardDataComponentInfo)]
	[ProtoContract]
	public partial class HeroCardDataComponentInfo: Object
	{
		[ProtoMember(1)]
		public int DiamondAttackAddition { get; set; }

		[ProtoMember(2)]
		public int HP { get; set; }

// int32 HeroAttack = 3 ;	//英雄伤害
// int32 WeaponAttack = 4;	//装备伤害
// int32 SkillAttack = 5;	//技能伤害
		[ProtoMember(6)]
		public int Damage { get; set; }

// int32 CriticalDamage = 7;//暴击伤害
		[ProtoMember(8)]
		public int ConfigId { get; set; }

		[ProtoMember(9)]
		public int Angry { get; set; }

		[ProtoMember(10)]
		public int TotalHP { get; set; }

		[ProtoMember(11)]
		public long HeroId { get; set; }

		[ProtoMember(12)]
		public long CurrentSkillId { get; set; }

		[ProtoMember(13)]
		public SkillInfo CurrentSkillInfo { get; set; }

		[ProtoMember(14)]
		public bool IsCritical { get; set; }

		[ProtoMember(15)]
		public int AddAngry { get; set; }

		[ProtoMember(16)]
		public int AddHP { get; set; }

// public int HP;  //当前的血量
// public int DiamondAttack;
// public int HeroAttack;
// public int WeaponAttack;
// public int SkillAttack;
// public int NormalDamage;
// public int CriticalDamage; //暴击伤害
	}

	[ResponseType(nameof(M2C_GetHeroCardByIdResponse))]
	[Message(OuterOpcode.C2M_GetHeroCardByIdRequest)]
	[ProtoContract]
	public partial class C2M_GetHeroCardByIdRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Account { get; set; }

		[ProtoMember(2)]
		public long HeroId { get; set; }

	}

	[Message(OuterOpcode.M2C_GetHeroCardByIdResponse)]
	[ProtoContract]
	public partial class M2C_GetHeroCardByIdResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public HeroCardInfo HeroCardInfo { get; set; }

	}

	[ResponseType(nameof(M2C_UpdateHeroRankResponse))]
	[Message(OuterOpcode.C2M_UpdateHeroRankRequest)]
	[ProtoContract]
	public partial class C2M_UpdateHeroRankRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Account { get; set; }

		[ProtoMember(2)]
		public long HeroId { get; set; }

	}

	[Message(OuterOpcode.M2C_UpdateHeroRankResponse)]
	[ProtoContract]
	public partial class M2C_UpdateHeroRankResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public HeroCardInfo HeroCardInfo { get; set; }

	}

	[ResponseType(nameof(M2C_UpdateHeroLevelResponse))]
	[Message(OuterOpcode.C2M_UpdateHeroLevelRequest)]
	[ProtoContract]
	public partial class C2M_UpdateHeroLevelRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Account { get; set; }

		[ProtoMember(2)]
		public long HeroId { get; set; }

	}

	[Message(OuterOpcode.M2C_UpdateHeroLevelResponse)]
	[ProtoContract]
	public partial class M2C_UpdateHeroLevelResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public HeroCardInfo HeroCardInfo { get; set; }

	}

	[ResponseType(nameof(M2C_UpdateHeroStarResponse))]
	[Message(OuterOpcode.C2M_UpdateHeroStarRequest)]
	[ProtoContract]
	public partial class C2M_UpdateHeroStarRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Account { get; set; }

		[ProtoMember(2)]
		public long HeroId { get; set; }

		[ProtoMember(3)]
		public long MaterialHeroId { get; set; }

	}

	[Message(OuterOpcode.M2C_UpdateHeroStarResponse)]
	[ProtoContract]
	public partial class M2C_UpdateHeroStarResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public HeroCardInfo HeroCardInfo { get; set; }

	}

	[ResponseType(nameof(M2C_UpdateOnWeaponResponse))]
	[Message(OuterOpcode.C2M_UpdateOnWeaponRequest)]
	[ProtoContract]
	public partial class C2M_UpdateOnWeaponRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Account { get; set; }

		[ProtoMember(2)]
		public long HeroId { get; set; }

		[ProtoMember(3)]
		public long WeaponId { get; set; }

	}

	[Message(OuterOpcode.M2C_UpdateOnWeaponResponse)]
	[ProtoContract]
	public partial class M2C_UpdateOnWeaponResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public HeroCardInfo HeroCardInfo { get; set; }

		[ProtoMember(2)]
		public List<WeaponInfo> WeaponInfos = new List<WeaponInfo>();

	}

	[ResponseType(nameof(M2C_OffWeaponResponse))]
	[Message(OuterOpcode.C2M_OffWeaponRequest)]
	[ProtoContract]
	public partial class C2M_OffWeaponRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Account { get; set; }

		[ProtoMember(2)]
		public long HeroId { get; set; }

		[ProtoMember(3)]
		public long WeaponId { get; set; }

	}

	[Message(OuterOpcode.M2C_OffWeaponResponse)]
	[ProtoContract]
	public partial class M2C_OffWeaponResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public HeroCardInfo HeroCardInfo { get; set; }

		[ProtoMember(2)]
		public List<WeaponInfo> WeaponInfos = new List<WeaponInfo>();

	}

	[ResponseType(nameof(M2C_GetOnWeaponsResponse))]
	[Message(OuterOpcode.C2M_GetOnWeaponsRequest)]
	[ProtoContract]
	public partial class C2M_GetOnWeaponsRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Account { get; set; }

		[ProtoMember(2)]
		public long HeroId { get; set; }

	}

	[Message(OuterOpcode.M2C_GetOnWeaponsResponse)]
	[ProtoContract]
	public partial class M2C_GetOnWeaponsResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<WeaponInfo> WeaponInfos = new List<WeaponInfo>();

	}

	[ResponseType(nameof(M2C_GetWeaponWordBarsResponse))]
	[Message(OuterOpcode.C2M_GetWeaponWordBarsRequest)]
	[ProtoContract]
	public partial class C2M_GetWeaponWordBarsRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Account { get; set; }

		[ProtoMember(2)]
		public long WeaponId { get; set; }

	}

	[Message(OuterOpcode.M2C_GetWeaponWordBarsResponse)]
	[ProtoContract]
	public partial class M2C_GetWeaponWordBarsResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<WordBarInfo> WordBarInfos = new List<WordBarInfo>();

	}

	[Message(OuterOpcode.WordBarInfo)]
	[ProtoContract]
	public partial class WordBarInfo: Object
	{
		[ProtoMember(1)]
		public long WordBarId { get; set; }

		[ProtoMember(2)]
		public long OnwerId { get; set; }

		[ProtoMember(3)]
		public int ConfigId { get; set; }

		[ProtoMember(4)]
		public int Value { get; set; }

		[ProtoMember(5)]
		public bool IsMain { get; set; }

	}

	[ResponseType(nameof(M2C_StrengthenWeaponResponse))]
	[Message(OuterOpcode.C2M_StrengthenWeaponRequest)]
	[ProtoContract]
	public partial class C2M_StrengthenWeaponRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(2)]
		public WeaponInfo TargetWeaponInfo { get; set; }

		[ProtoMember(3)]
		public List<WeaponInfo> ChooseWeaponInfos = new List<WeaponInfo>();

	}

	[Message(OuterOpcode.M2C_StrengthenWeaponResponse)]
	[ProtoContract]
	public partial class M2C_StrengthenWeaponResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

// repeated WeaponInfo WeaponInfos = 1;
		[ProtoMember(1)]
		public WeaponInfo WeaponInfo { get; set; }

	}

	[ResponseType(nameof(M2C_GetWordBarInfosWithWeaponListResponse))]
	[Message(OuterOpcode.C2M_GetWordBarInfosWithWeaponListRequest)]
	[ProtoContract]
	public partial class C2M_GetWordBarInfosWithWeaponListRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(2)]
		public List<long> WeaponInfoIds = new List<long>();

	}

	[Message(OuterOpcode.M2C_GetWordBarInfosWithWeaponListResponse)]
	[ProtoContract]
	public partial class M2C_GetWordBarInfosWithWeaponListResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

// repeated WeaponInfo WeaponInfos = 1;
// WeaponInfo WeaponInfo = 1;	//升级完成的英雄信息
		[ProtoMember(1)]
		public List<WordBarInfo> WordBarInfos = new List<WordBarInfo>();

	}

	[ResponseType(nameof(M2C_WeaponWordBarClearNormalResponse))]
	[Message(OuterOpcode.C2M_WeaponWordBarClearNormalRequest)]
	[ProtoContract]
	public partial class C2M_WeaponWordBarClearNormalRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(2)]
		public List<long> WordBarIds = new List<long>();

		[ProtoMember(3)]
		public long WeaponId { get; set; }

	}

	[Message(OuterOpcode.M2C_WeaponWordBarClearNormalResponse)]
	[ProtoContract]
	public partial class M2C_WeaponWordBarClearNormalResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<WordBarInfo> WordBarInfos = new List<WordBarInfo>();

	}

	[ResponseType(nameof(M2C_WeaponWordBarSpeicalClearResponse))]
	[Message(OuterOpcode.C2M_WeaponWordBarSpeicalClearRequest)]
	[ProtoContract]
	public partial class C2M_WeaponWordBarSpeicalClearRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(2)]
		public List<long> WordBarIds = new List<long>();

		[ProtoMember(3)]
		public long WeaponId { get; set; }

	}

	[Message(OuterOpcode.M2C_WeaponWordBarSpeicalClearResponse)]
	[ProtoContract]
	public partial class M2C_WeaponWordBarSpeicalClearResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<WordBarInfo> WordBarInfos = new List<WordBarInfo>();

	}

	[Message(OuterOpcode.MailInfo)]
	[ProtoContract]
	public partial class MailInfo: Object
	{
// public long ReceiveId; //发送者id
// public long SendId; //收者id
// public string SendTime; //发送时间
// public bool IsRead = false; //是否已读
// public bool IsGet = false; //是否已经领取
// public string SendName; //发送者名字
		[ProtoMember(1)]
		public long MailId { get; set; }

		[ProtoMember(2)]
		public long ReceiveId { get; set; }

		[ProtoMember(3)]
		public long SendId { get; set; }

		[ProtoMember(4)]
		public string SendName { get; set; }

		[ProtoMember(5)]
		public long SendTime { get; set; }

		[ProtoMember(6)]
		public bool IsRead { get; set; }

		[ProtoMember(7)]
		public bool IsGet { get; set; }

		[ProtoMember(8)]
		public string Title { get; set; }

		[ProtoMember(9)]
		public string Content { get; set; }

		[ProtoMember(10)]
		public int MailType { get; set; }

	}

	[ResponseType(nameof(M2C_GetAllMailResponse))]
	[Message(OuterOpcode.C2M_GetAllMailRequest)]
	[ProtoContract]
	public partial class C2M_GetAllMailRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

	}

	[Message(OuterOpcode.M2C_GetAllMailResponse)]
	[ProtoContract]
	public partial class M2C_GetAllMailResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<MailInfo> MailInfos = new List<MailInfo>();

	}

	[Message(OuterOpcode.C2M_RegisterNewMailBoxMessage)]
	[ProtoContract]
	public partial class C2M_RegisterNewMailBoxMessage: Object, IActorLocationMessage
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

	}

	[Message(OuterOpcode.M2C_SendMails)]
	[ProtoContract]
	public partial class M2C_SendMails: Object, IActorMessage
	{
		[ProtoMember(1)]
		public List<MailInfo> MailInfos = new List<MailInfo>();

	}

	[ResponseType(nameof(M2C_ReadMailsResponse))]
	[Message(OuterOpcode.C2M_ReadMailsRequest)]
	[ProtoContract]
	public partial class C2M_ReadMailsRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(2)]
		public List<long> MailIds = new List<long>();

	}

	[Message(OuterOpcode.M2C_ReadMailsResponse)]
	[ProtoContract]
	public partial class M2C_ReadMailsResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<MailInfo> MailInfos = new List<MailInfo>();

	}

	[ResponseType(nameof(M2C_GetAllAwardInfoResponse))]
	[Message(OuterOpcode.C2M_GetAllAwardInfoRequest)]
	[ProtoContract]
	public partial class C2M_GetAllAwardInfoRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(2)]
		public long OwnerId { get; set; }

	}

	[Message(OuterOpcode.M2C_GetAllAwardInfoResponse)]
	[ProtoContract]
	public partial class M2C_GetAllAwardInfoResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<HeroCardInfo> HeroCardInfos = new List<HeroCardInfo>();

		[ProtoMember(2)]
		public List<WeaponInfo> WeaponInfos = new List<WeaponInfo>();

		[ProtoMember(3)]
		public List<ItemInfo> ItemInfos = new List<ItemInfo>();

	}

	[ResponseType(nameof(M2C_ReceiveAllAwardResponse))]
	[Message(OuterOpcode.C2M_ReceiveAllAwardRequest)]
	[ProtoContract]
	public partial class C2M_ReceiveAllAwardRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(2)]
		public long OwnerId { get; set; }

	}

	[Message(OuterOpcode.M2C_ReceiveAllAwardResponse)]
	[ProtoContract]
	public partial class M2C_ReceiveAllAwardResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public MailInfo MailInfo { get; set; }

	}

	[Message(OuterOpcode.AccountInfo)]
	[ProtoContract]
	public partial class AccountInfo: Object
	{
		[ProtoMember(1)]
		public long Account { get; set; }

		[ProtoMember(2)]
		public string Name { get; set; }

		[ProtoMember(3)]
		public string NickName { get; set; }

		[ProtoMember(4)]
		public long LastLogonTime { get; set; }

		[ProtoMember(5)]
		public long CreateTime { get; set; }

		[ProtoMember(6)]
		public int PvELevelNumber { get; set; }

		[ProtoMember(7)]
		public long ItemId { get; set; }

// int32 HeadImageConfigId = 7;	//头像图片的配置Id
// int32 HeadFrameImageConfigId = 8;	//头像框的配置id
		[ProtoMember(9)]
		public long CurrentTroopId { get; set; }

	}

	[ResponseType(nameof(M2C_GetFriendRecommendListResponse))]
	[Message(OuterOpcode.C2M_GetFriendRecommendListRequest)]
	[ProtoContract]
	public partial class C2M_GetFriendRecommendListRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

	}

	[Message(OuterOpcode.M2C_GetFriendRecommendListResponse)]
	[ProtoContract]
	public partial class M2C_GetFriendRecommendListResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<AccountInfo> AccountInfos = new List<AccountInfo>();

	}

	[ResponseType(nameof(M2C_AddFriendResponse))]
	[Message(OuterOpcode.C2M_AddFriendRequest)]
	[ProtoContract]
	public partial class C2M_AddFriendRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Account { get; set; }

		[ProtoMember(2)]
		public AccountInfo TargetInfo { get; set; }

	}

	[Message(OuterOpcode.M2C_AddFriendResponse)]
	[ProtoContract]
	public partial class M2C_AddFriendResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_GetFriendApplyListResponse))]
	[Message(OuterOpcode.C2M_GetFriendApplyListRequest)]
	[ProtoContract]
	public partial class C2M_GetFriendApplyListRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

	}

	[Message(OuterOpcode.M2C_GetFriendApplyListResponse)]
	[ProtoContract]
	public partial class M2C_GetFriendApplyListResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<AccountInfo> AccountInfo = new List<AccountInfo>();

		[ProtoMember(2)]
		public List<MailInfo> MailInfo = new List<MailInfo>();

	}

	[ResponseType(nameof(M2C_ProcessFriendApplyResponse))]
	[Message(OuterOpcode.C2M_ProcessFriendApplyRequest)]
	[ProtoContract]
	public partial class C2M_ProcessFriendApplyRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(2)]
		public AccountInfo AccountInfo { get; set; }

		[ProtoMember(3)]
		public int ApplyProcessType { get; set; }

	}

	[Message(OuterOpcode.M2C_ProcessFriendApplyResponse)]
	[ProtoContract]
	public partial class M2C_ProcessFriendApplyResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_GetAllFriendsResponse))]
	[Message(OuterOpcode.C2M_GetAllFriendsRequest)]
	[ProtoContract]
	public partial class C2M_GetAllFriendsRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

	}

	[Message(OuterOpcode.M2C_GetAllFriendsResponse)]
	[ProtoContract]
	public partial class M2C_GetAllFriendsResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<FriendInfo> FriendInfos = new List<FriendInfo>();

		[ProtoMember(2)]
		public List<AccountInfo> AccountInfos = new List<AccountInfo>();

	}

	[Message(OuterOpcode.FriendInfo)]
	[ProtoContract]
	public partial class FriendInfo: Object
	{
		[ProtoMember(1)]
		public long FriendsId { get; set; }

		[ProtoMember(2)]
		public long OwnerId { get; set; }

		[ProtoMember(3)]
		public long FriendId { get; set; }

		[ProtoMember(4)]
		public long CreateTime { get; set; }

		[ProtoMember(5)]
		public bool IsGift { get; set; }

	}

	[ResponseType(nameof(M2C_GiveGiftToFriendResponse))]
	[Message(OuterOpcode.C2M_GiveGiftToFriendRequest)]
	[ProtoContract]
	public partial class C2M_GiveGiftToFriendRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Account { get; set; }

		[ProtoMember(2)]
		public AccountInfo AccountInfo { get; set; }

	}

	[Message(OuterOpcode.M2C_GiveGiftToFriendResponse)]
	[ProtoContract]
	public partial class M2C_GiveGiftToFriendResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public FriendInfo FriendInfo { get; set; }

	}

	[ResponseType(nameof(M2C_OneKeyGiveAndGetResponse))]
	[Message(OuterOpcode.C2M_OneKeyGiveAndGetRequest)]
	[ProtoContract]
	public partial class C2M_OneKeyGiveAndGetRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Account { get; set; }

	}

	[Message(OuterOpcode.M2C_OneKeyGiveAndGetResponse)]
	[ProtoContract]
	public partial class M2C_OneKeyGiveAndGetResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public List<FriendInfo> FriendInfos = new List<FriendInfo>();

		[ProtoMember(5)]
		public ItemInfo PowerItemInfo { get; set; }

		[ProtoMember(6)]
		public List<long> AccountIds = new List<long>();

	}

	[Message(OuterOpcode.ChatInfo)]
	[ProtoContract]
	public partial class ChatInfo: Object
	{
		[ProtoMember(1)]
		public AccountInfo AccountInfo { get; set; }

		[ProtoMember(2)]
		public string ChatText { get; set; }

	}

	[ResponseType(nameof(M2C_ChatToFriendResponse))]
	[Message(OuterOpcode.C2M_ChatToFriendRequest)]
	[ProtoContract]
	public partial class C2M_ChatToFriendRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(2)]
		public AccountInfo AccountInfo { get; set; }

		[ProtoMember(3)]
		public string ChatText { get; set; }

	}

	[Message(OuterOpcode.M2C_ChatToFriendResponse)]
	[ProtoContract]
	public partial class M2C_ChatToFriendResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public ChatInfo ChatInfo { get; set; }

	}

	[Message(OuterOpcode.M2C_ReceiveChatFromFriend)]
	[ProtoContract]
	public partial class M2C_ReceiveChatFromFriend: Object, IActorMessage
	{
		[ProtoMember(1)]
		public ChatInfo ChatInfo { get; set; }

	}

	[ResponseType(nameof(M2C_GetAccountInfoWidthAccointIdResponse))]
	[Message(OuterOpcode.C2M_GetAccountInfoWithAccountIdRequest)]
	[ProtoContract]
	public partial class C2M_GetAccountInfoWithAccountIdRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

	}

	[Message(OuterOpcode.M2C_GetAccountInfoWidthAccointIdResponse)]
	[ProtoContract]
	public partial class M2C_GetAccountInfoWidthAccointIdResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public AccountInfo AccountInfo { get; set; }

	}

	[ResponseType(nameof(M2C_GetPlayerOwnHeroTypeCountResponse))]
	[Message(OuterOpcode.C2M_GetPlayerOwnHeroTypeCountRequest)]
	[ProtoContract]
	public partial class C2M_GetPlayerOwnHeroTypeCountRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

	}

	[Message(OuterOpcode.M2C_GetPlayerOwnHeroTypeCountResponse)]
	[ProtoContract]
	public partial class M2C_GetPlayerOwnHeroTypeCountResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public int Count { get; set; }

	}

	[ResponseType(nameof(M2C_ChangePlayerHeadOrFrameResponse))]
	[Message(OuterOpcode.C2M_ChangePlayerHeadOrFrameRequest)]
	[ProtoContract]
	public partial class C2M_ChangePlayerHeadOrFrameRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(3)]
		public long ItemId { get; set; }

		[ProtoMember(2)]
		public int HeadType { get; set; }

// int32 ConfigId = 3;
	}

	[Message(OuterOpcode.M2C_ChangePlayerHeadOrFrameResponse)]
	[ProtoContract]
	public partial class M2C_ChangePlayerHeadOrFrameResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public AccountInfo AccountInfo { get; set; }

	}

	[ResponseType(nameof(M2C_SearchAccountWithNameResponse))]
	[Message(OuterOpcode.C2M_SearchAccountWithNameRequest)]
	[ProtoContract]
	public partial class C2M_SearchAccountWithNameRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(2)]
		public string Name { get; set; }

	}

	[Message(OuterOpcode.M2C_SearchAccountWithNameResponse)]
	[ProtoContract]
	public partial class M2C_SearchAccountWithNameResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<AccountInfo> AccountInfos = new List<AccountInfo>();

	}

	[ResponseType(nameof(M2C_DelAllReadMailResponse))]
	[Message(OuterOpcode.C2M_DelAllReadMailRequest)]
	[ProtoContract]
	public partial class C2M_DelAllReadMailRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

	}

	[Message(OuterOpcode.M2C_DelAllReadMailResponse)]
	[ProtoContract]
	public partial class M2C_DelAllReadMailResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<MailInfo> MailInfos = new List<MailInfo>();

	}

	[Message(OuterOpcode.GameTaskInfo)]
	[ProtoContract]
	public partial class GameTaskInfo: Object
	{
		[ProtoMember(1)]
		public long TaskId { get; set; }

		[ProtoMember(2)]
		public int TaskState { get; set; }

		[ProtoMember(3)]
		public int ConfigId { get; set; }

		[ProtoMember(4)]
		public long CreateTime { get; set; }

		[ProtoMember(5)]
		public int ActionCount { get; set; }

	}

	[ResponseType(nameof(M2C_GetTaskInfoWithConfigIdResponse))]
	[Message(OuterOpcode.C2M_GetTaskInfoWithConfigIdReqeust)]
	[ProtoContract]
	public partial class C2M_GetTaskInfoWithConfigIdReqeust: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(2)]
		public int ConfigId { get; set; }

	}

	[Message(OuterOpcode.M2C_GetTaskInfoWithConfigIdResponse)]
	[ProtoContract]
	public partial class M2C_GetTaskInfoWithConfigIdResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public GameTaskInfo GameTaskInfo { get; set; }

	}

	[ResponseType(nameof(M2C_GetActivePointValueByConfigIdResponse))]
	[Message(OuterOpcode.C2M_GetActivePointValueByConfigIdRequest)]
	[ProtoContract]
	public partial class C2M_GetActivePointValueByConfigIdRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(2)]
		public int ConfigId { get; set; }

	}

	[Message(OuterOpcode.M2C_GetActivePointValueByConfigIdResponse)]
	[ProtoContract]
	public partial class M2C_GetActivePointValueByConfigIdResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public int Value { get; set; }

	}

	[ResponseType(nameof(M2C_GetGameTaskAwardResponse))]
	[Message(OuterOpcode.C2M_GetGameTaskAwardRequest)]
	[ProtoContract]
	public partial class C2M_GetGameTaskAwardRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(2)]
		public long TaskId { get; set; }

	}

	[Message(OuterOpcode.M2C_GetGameTaskAwardResponse)]
	[ProtoContract]
	public partial class M2C_GetGameTaskAwardResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<ItemInfo> ItemInfos = new List<ItemInfo>();

		[ProtoMember(2)]
		public GameTaskInfo GameTaskInfo { get; set; }

	}

	[ResponseType(nameof(M2C_EnterNextLevelPvEGameResponse))]
	[Message(OuterOpcode.C2M_EnterNextLevelPvEGameRequest)]
	[ProtoContract]
	public partial class C2M_EnterNextLevelPvEGameRequest: Object, IActorLocationRequest
	{
//需要退出当前游戏
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Account { get; set; }

		[ProtoMember(2)]
		public long RoomId { get; set; }

	}

	[Message(OuterOpcode.M2C_EnterNextLevelPvEGameResponse)]
	[ProtoContract]
	public partial class M2C_EnterNextLevelPvEGameResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_EnterChangeTempSceneResponse))]
	[Message(OuterOpcode.C2M_EnterChangeTempSceneRequest)]
	[ProtoContract]
	public partial class C2M_EnterChangeTempSceneRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Account { get; set; }

		[ProtoMember(2)]
		public long RoomId { get; set; }

	}

	[Message(OuterOpcode.M2C_EnterChangeTempSceneResponse)]
	[ProtoContract]
	public partial class M2C_EnterChangeTempSceneResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_PlayerChooseLevelNumResponse))]
	[Message(OuterOpcode.C2M_PlayerChooseLevelNumRequest)]
	[ProtoContract]
	public partial class C2M_PlayerChooseLevelNumRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Account { get; set; }

		[ProtoMember(2)]
		public int LevelNum { get; set; }

	}

	[Message(OuterOpcode.M2C_PlayerChooseLevelNumResponse)]
	[ProtoContract]
	public partial class M2C_PlayerChooseLevelNumResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_LockHeroCardResponse))]
	[Message(OuterOpcode.C2M_LockHeroCardRequest)]
	[ProtoContract]
	public partial class C2M_LockHeroCardRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Account { get; set; }

		[ProtoMember(2)]
		public long HeroId { get; set; }

		[ProtoMember(3)]
		public bool Lock { get; set; }

	}

	[Message(OuterOpcode.M2C_LockHeroCardResponse)]
	[ProtoContract]
	public partial class M2C_LockHeroCardResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[Message(OuterOpcode.M2C_MatchPVPSuccess)]
	[ProtoContract]
	public partial class M2C_MatchPVPSuccess: Object, IActorMessage
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long RoomId { get; set; }

		[ProtoMember(2)]
		public List<long> AccountIds = new List<long>();

	}

	[ResponseType(nameof(M2C_ReadyToPVPRoomResponse))]
	[Message(OuterOpcode.C2M_ReadyToPVPRoomRequest)]
	[ProtoContract]
	public partial class C2M_ReadyToPVPRoomRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Account { get; set; }

	}

	[Message(OuterOpcode.M2C_ReadyToPVPRoomResponse)]
	[ProtoContract]
	public partial class M2C_ReadyToPVPRoomResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_GetCurrentTroopIndexResponse))]
	[Message(OuterOpcode.C2M_GetCurrentTroopIndexRequest)]
	[ProtoContract]
	public partial class C2M_GetCurrentTroopIndexRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

	}

	[Message(OuterOpcode.M2C_GetCurrentTroopIndexResponse)]
	[ProtoContract]
	public partial class M2C_GetCurrentTroopIndexResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public int CurrentTroopIndex { get; set; }

	}

	[ResponseType(nameof(M2C_PlayerChooseTroopIndexResponse))]
	[Message(OuterOpcode.C2M_PlayerChooseTroopIndexRequest)]
	[ProtoContract]
	public partial class C2M_PlayerChooseTroopIndexRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public int Index { get; set; }

	}

	[Message(OuterOpcode.M2C_PlayerChooseTroopIndexResponse)]
	[ProtoContract]
	public partial class M2C_PlayerChooseTroopIndexResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<HeroCardInfo> HeroCardInfo = new List<HeroCardInfo>();

	}

	[Message(OuterOpcode.M2C_CreateHeroModeMessage)]
	[ProtoContract]
	public partial class M2C_CreateHeroModeMessage: Object, IActorMessage
	{
//创建英雄模型的消息
		[ProtoMember(1)]
		public HeroCardInfo HeroCardInfo { get; set; }

	}

	[ResponseType(nameof(M2C_GetCurrentShowHeroCardInfoResponse))]
	[Message(OuterOpcode.C2M_GetCurrentShowHeroCardInfoRequest)]
	[ProtoContract]
	public partial class C2M_GetCurrentShowHeroCardInfoRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

	}

	[Message(OuterOpcode.M2C_GetCurrentShowHeroCardInfoResponse)]
	[ProtoContract]
	public partial class M2C_GetCurrentShowHeroCardInfoResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public HeroCardInfo HeroCardInfo { get; set; }

	}

	[ResponseType(nameof(M2C_SetCurrentShowHeroCardInfoResponse))]
	[Message(OuterOpcode.C2M_SetCurrentShowHeroCardInfoRequest)]
	[ProtoContract]
	public partial class C2M_SetCurrentShowHeroCardInfoRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long HeroId { get; set; }

	}

	[Message(OuterOpcode.M2C_SetCurrentShowHeroCardInfoResponse)]
	[ProtoContract]
	public partial class M2C_SetCurrentShowHeroCardInfoResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[Message(OuterOpcode.BuffInfo)]
	[ProtoContract]
	public partial class BuffInfo: Object
	{
		[ProtoMember(1)]
		public long BuffId { get; set; }

		[ProtoMember(2)]
		public int ConfigId { get; set; }

		[ProtoMember(3)]
		public int RoundCount { get; set; }

		[ProtoMember(4)]
		public int HealthShield { get; set; }

	}

}
