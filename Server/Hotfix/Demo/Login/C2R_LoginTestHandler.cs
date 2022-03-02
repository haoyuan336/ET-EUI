using System;

namespace ET
{
    public class C2R_LoginTestHandler:AMRpcHandler<C2R_LoginTest, R2C_LoginTest>
    {
        protected override async ETTask Run(Session session, C2R_LoginTest request, R2C_LoginTest response, Action reply)
        {
            response.key = "11111";
            reply();
            await ETTask.CompletedTask;
        }
    }
}