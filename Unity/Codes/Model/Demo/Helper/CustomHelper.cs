namespace ET
{
    public static class CustomHelper
    {
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
    }
}