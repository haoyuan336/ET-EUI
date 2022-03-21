using System;

namespace ET
{
    public class GetAllHeroListInfoRequestHandler: AMRpcHandler<C2M_GetAllHeroCardListRequest, M2C_GetAllHeroCardListResponse>
    {
        protected override async ETTask Run(Session session, C2M_GetAllHeroCardListRequest request, M2C_GetAllHeroCardListResponse response,
        Action reply)
        {
            Log.Debug("get all hero list info");
            M2C_GetAllHeroCardListResponse m2CGetAllHeroCardListResponse = new M2C_GetAllHeroCardListResponse();
            m2CGetAllHeroCardListResponse.Error = ErrorCode.ERR_Success;
            reply();
            await ETTask.CompletedTask;
        }
    }
}