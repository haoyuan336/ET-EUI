using System.Collections.Generic;
using OfficeOpenXml.FormulaParsing.Excel.Functions;

namespace ET.Room
{
    public class Room: Entity, IAwake,IDestroy, IUpdate
    {
        public List<Unit> Units = new List<Unit>();
        public int CurrentTurnIndex = 0;
        public int HangCount = 0;
        public int LieCount = 0;
        public Diamond[,] Diamonds = null;
    }
}