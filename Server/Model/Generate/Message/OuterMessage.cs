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

	[Message(OuterOpcode.C2M_MatchRoomActorLocationMessage)]
	[ProtoContract]
	public partial class C2M_MatchRoomActorLocationMessage: Object, IActorLocationMessage
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string Content { get; set; }

	}

	[Message(OuterOpcode.M2C_SyncCurrentMatchingCount)]
	[ProtoContract]
	public partial class M2C_SyncCurrentMatchingCount: Object, IActorMessage
	{
		[ProtoMember(1)]
		public int Content { get; set; }

	}

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

	[Message(OuterOpcode.M2C_SyncCreateRoomMessage)]
	[ProtoContract]
	public partial class M2C_SyncCreateRoomMessage: Object, IActorMessage
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public int InRoomIndex { get; set; }

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

	[Message(OuterOpcode.M2C_ChangeCurrentTurnSeatIndex)]
	[ProtoContract]
	public partial class M2C_ChangeCurrentTurnSeatIndex: Object, IActorMessage
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public int CurrentTurnIndex { get; set; }

	}

	[Message(OuterOpcode.M2C_SyncDiamondUpdatePos)]
	[ProtoContract]
	public partial class M2C_SyncDiamondUpdatePos: Object, IActorMessage
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public List<DiamondInfo> DiamondInfos = new List<DiamondInfo>();

	}

	[Message(OuterOpcode.DiamondAction)]
	[ProtoContract]
	public partial class DiamondAction: Object
	{
//宝石的action
		[ProtoMember(90)]
		public int PpcId { get; set; }

		[ProtoMember(1)]
		public int ActionType { get; set; }

		[ProtoMember(2)]
		public DiamondInfo DiamondInfo { get; set; }

	}

	[Message(OuterOpcode.DiamondActionItem)]
	[ProtoContract]
	public partial class DiamondActionItem: Object
	{
		[ProtoMember(1)]
		public List<DiamondAction> DiamondActions = new List<DiamondAction>();

	}

	[Message(OuterOpcode.GameLoseResultAction)]
	[ProtoContract]
	public partial class GameLoseResultAction: Object
	{
		[ProtoMember(1)]
		public long LoseAccountId { get; set; }

	}

	[Message(OuterOpcode.AttackAction)]
	[ProtoContract]
	public partial class AttackAction: Object
	{
		[ProtoMember(1)]
		public HeroCardInfo AttackHeroCardInfo { get; set; }

		[ProtoMember(2)]
		public List<HeroCardInfo> BeAttackHeroCardInfo = new List<HeroCardInfo>();

	}

	[Message(OuterOpcode.AttackActionItem)]
	[ProtoContract]
	public partial class AttackActionItem: Object
	{
		[ProtoMember(1)]
		public List<AttackAction> AttackActions = new List<AttackAction>();

	}

	[Message(OuterOpcode.M2C_SyncDiamondAction)]
	[ProtoContract]
	public partial class M2C_SyncDiamondAction: Object, IActorMessage
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public List<DiamondActionItem> DiamondActionItems = new List<DiamondActionItem>();

		[ProtoMember(2)]
		public List<AttackActionItem> AttackActionItems = new List<AttackActionItem>();

		[ProtoMember(3)]
		public GameLoseResultAction GameLoseResultAction { get; set; }

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

		[ProtoMember(9)]
		public long CastSkillId { get; set; }

		[ProtoMember(10)]
		public float Attack { get; set; }

		[ProtoMember(11)]
		public float HP { get; set; }

		[ProtoMember(12)]
		public List<SkillInfo> SkillInfos = new List<SkillInfo>();

		[ProtoMember(13)]
		public float DiamondAttack { get; set; }

		[ProtoMember(14)]
		public float Angry { get; set; }

		[ProtoMember(15)]
		public float Defence { get; set; }

		[ProtoMember(16)]
		public int Level { get; set; }

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

	[ResponseType(nameof(M2C_GetHeroInfosWithTroopIdResponse))]
	[Message(OuterOpcode.C2M_GetHeroInfosWithTroopIdRequest)]
	[ProtoContract]
	public partial class C2M_GetHeroInfosWithTroopIdRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long TroopId { get; set; }

	}

	[Message(OuterOpcode.M2C_GetHeroInfosWithTroopIdResponse)]
	[ProtoContract]
	public partial class M2C_GetHeroInfosWithTroopIdResponse: Object, IActorLocationResponse
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

		[ProtoMember(1)]
		public long TroopId { get; set; }

		[ProtoMember(2)]
		public long HeroId { get; set; }

		[ProtoMember(3)]
		public int InTroopIndex { get; set; }

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
		public HeroCardInfo HeroCardInfo { get; set; }

	}

	[ResponseType(nameof(M2C_StartPVEGameResponse))]
	[Message(OuterOpcode.C2M_StartPVEGameRequest)]
	[ProtoContract]
	public partial class C2M_StartPVEGameRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccoundId { get; set; }

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
		public List<SkillInfo> SkillInfos = new List<SkillInfo>();

	}

	[ResponseType(nameof(M2C_PlayerReadyTurnResponse))]
	[Message(OuterOpcode.C2M_PlayerReadyTurnRequest)]
	[ProtoContract]
	public partial class C2M_PlayerReadyTurnRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(2)]
		public long RoomId { get; set; }

	}

	[Message(OuterOpcode.M2C_PlayerReadyTurnResponse)]
	[ProtoContract]
	public partial class M2C_PlayerReadyTurnResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[Message(OuterOpcode.M2C_SyncHeroCardTurnData)]
	[ProtoContract]
	public partial class M2C_SyncHeroCardTurnData: Object, IActorMessage
	{
		[ProtoMember(1)]
		public List<HeroCardInfo> HeroCardInfos = new List<HeroCardInfo>();

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

		[ProtoMember(1)]
		public int GoldCount { get; set; }

		[ProtoMember(2)]
		public int PowerCount { get; set; }

		[ProtoMember(3)]
		public int DiamondCount { get; set; }

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

}
