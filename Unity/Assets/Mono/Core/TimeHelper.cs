using System;
using System.Globalization;

namespace ET
{
    public static class TimeHelper
    {
        public const long OneDay = 86400000;
        public const long Hour = 3600000;
        public const long Minute = 60000;

        // public static string GetLastTimeByMSecond(long disTime)
        // {
        //     var year = disTime / 1000 / (60 * 60 * 24 * 365);
        //     var day = disTime / 1000 / (60 * 60 * 24);
        //     var hour = disTime / 1000 / (60 * 60);
        //     var min = disTime / 1000 / 60;
        //     var second = disTime / 1000;
        //     string result = "";
        //     if (year != 0)
        //     {
        //         result = $"{year}年前";
        //     }
        //     else if (day != 0)
        //     {
        //         result = $"{day}天前";
        //     }
        //     else if (hour != 0)
        //     {
        //         result = $"{hour}小时前";
        //     }
        //     else if (min != 0)
        //     {
        //         result = $"{min}分钟前";
        //     }
        //     else if (second != 0)
        //     {
        //         result = $"{second}秒前";
        //     }
        //
        //     return result;
        // }

        /// <summary>
        /// 客户端时间
        /// </summary>
        /// <returns></returns>
        public static long ClientNow()
        {
            return TimeInfo.Instance.ClientNow();
        }

        public static long ClientNowSeconds()
        {
            return ClientNow() / 1000;
        }

        public static DateTime DateTimeNow()
        {
            return DateTime.Now;
        }

        public static long ServerNow()
        {
            return TimeInfo.Instance.ServerNow();
        }

        public static long ClientFrameTime()
        {
            return TimeInfo.Instance.ClientFrameTime();
        }

        public static long ServerFrameTime()
        {
            return TimeInfo.Instance.ServerFrameTime();
        }
    }
}