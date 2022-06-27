using System.Numerics;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace ET
{
    public static class CustomHelper
    {
        public static Vector2 GetIndexVectorWidthDit(Vector2 indexV, ScrollDirType type)
        {
            float lieIndex = indexV.x;
            float hangIndex = indexV.y;

            switch (type)
            {
                case ScrollDirType.Down:
                    hangIndex -= 1;
                    break;
                case ScrollDirType.Left:
                    lieIndex -= 1;
                    break;
                case ScrollDirType.Right:
                    lieIndex += 1;
                    break;
                case ScrollDirType.Up:
                    hangIndex += 1;
                    break;
            }

            return new Vector2(lieIndex, hangIndex);
        }

        public static string GetLastTimeByMSecond(long disTime)
        {
            var year = disTime / 1000 / (60 * 60 * 24 * 365);
            var day = disTime / 1000 / (60 * 60 * 24);
            var hour = disTime / 1000 / (60 * 60);
            var min = disTime / 1000 / 60;
            var second = disTime / 1000;
            string result = "";
            if (year != 0)
            {
                result = $"{year}年前";
            }
            else if (day != 0)
            {
                result = $"{day}天前";
            }
            else if (hour != 0)
            {
                result = $"{hour}小时前";
            }
            else if (min != 0)
            {
                result = $"{min}分钟前";
            }
            else if (second != 0)
            {
                result = $"{second}秒前";
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static long GetCurrentDayTime()
        {
            return (int) (TimeHelper.ServerNow() / TimeHelper.OneDay) * TimeHelper.OneDay;
        }

        public static Vector3 GetDiamondPos(float lieCount, float hangCount, float lieIndex, float hangIndex, float distance, float offsetZ)
        {
            // go.transform.position = new Vector3((liecount * 0.5f - a.Diamond.LieIndex - 1) * distance, 0,
            //     (hangCount * 0.5f - 0.5f - a.Diamond.HangIndex + 0.5f) * distance);

            return new Vector3((lieCount - 1) * 0.5f * distance - lieIndex * distance, 0,
                (hangCount - 1) * 0.5f * distance - hangIndex * distance + offsetZ);
        }

        public static Vector2 GetDiamondIndexWidthPos(Vector2 pos, float lieCount, float hangCount, float distance, float offsetZ)
        {
            float lieIndex = ((lieCount - 1) * 0.5f * distance - pos.x) / distance;
            float hangIndex = (pos.y - offsetZ - (hangCount - 1) * 0.5f * distance) / -distance;
            return new Vector2(lieIndex, hangIndex);
        }
    }
}