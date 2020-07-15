using System;
public static class HRT_TimeFormat
{


    public static long ConvertDateTimeToLong(DateTime dt)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        TimeSpan toNow = dt.Subtract(dtStart);
        long timeStamp = toNow.Ticks;
        timeStamp = long.Parse(timeStamp.ToString().Substring(0, timeStamp.ToString().Length - 4));
        return timeStamp;
    }


    // long --> DateTime
    public static DateTime ConvertLongToDateTime(long d)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(d + "0000");
        TimeSpan toNow = new TimeSpan(lTime);
        DateTime dtResult = dtStart.Add(toNow);
        return dtResult;
    }


    /// <summary>
    /// 服务器发过来的时间进行格式转化
    /// </summary>
    public static string ToTimeFormatString(this long seconds)
    {

        //long timeLong = (System.DateTime.Now.Ticks - System.DateTime.UtcNow.Ticks) / 10000;
        //DateTime time = new DateTime(1970, 1, 1);
        //time = time.AddSeconds(seconds);
        //time = time.AddMilliseconds(timeLong);
        string ret = SecondsToDateTime(seconds).ToString("yyyy/MM/dd  HH:mm");
        return ret;
    }
    /// <summary>
    /// UTC距1970年秒数 - 转成本地时间
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    private static DateTime SecondsToDateTime(long seconds)
    {
        DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local).AddSeconds(seconds).ToLocalTime();
        if (time.Year < 1970 || time.Year > 2030)
        {
            time = new DateTime(2000, time.Month, time.Day, time.Hour, time.Minute, time.Second);
        }
        return time;
    }
    public static string ToTimeFormatHour(this long seconds)
    {
        //long timeLong = (System.DateTime.Now.Ticks - System.DateTime.UtcNow.Ticks) / 10000;
        //DateTime time = new DateTime(1970, 1, 1);
        //time = time.AddSeconds(seconds);
        //time = time.AddMilliseconds(timeLong);
        //string ret = time.ToString("HH:mm");
        string ret = SecondsToDateTime(seconds).ToString("HH:mm");
        return ret;
    }
    public static string ToTimeFormatMounthAndDay(this long seconds)
    {
        //long timeLong = (System.DateTime.Now.Ticks - System.DateTime.UtcNow.Ticks) / 10000;
        //DateTime time = new DateTime(1970, 1, 1);
        //time = time.AddSeconds(seconds);
        //time = time.AddMilliseconds(timeLong);
        string ret = SecondsToDateTime(seconds).ToString("MM月dd日");
        return ret;
    }
    public static string ToSmartTimeFormatString(this int seconds)
    {
        string ret = "";
        if ((seconds / 86400) > 0)
        {
            ret = string.Format("{0:d}d {1:d2}:{2:d2}:{3:d2}", ((seconds) / 86400), ((seconds % 86400) / 3600), ((seconds % 3600) / 60), (seconds % 60));
        }
        else if ((((int)seconds) / 3600) > 0)
        {
            ret = string.Format("{0:d2}:{1:d2}:{2:d2}", (seconds / 3600), ((seconds % 3600) / 60), (seconds % 60));
        }
        else
        {
            ret = string.Format("{0:d2}:{1:d2}", ((seconds % 3600) / 60), (seconds % 60));
        }
        return ret;
    }
    /// <summary>
    /// 把时间格式化成xx单一的时间
    /// overTime 剩余时间
    /// </summary>
    public static string InitFormatTime(this long overTime)
    {
        long DayNum = overTime / 86400;
        if (DayNum > 0)
        {
            return string.Format("{0}天", DayNum);
        }
        long hoursNum = (overTime / 3600) % 3600;
        if (hoursNum > 0)
        {
            return string.Format("{0}小时", hoursNum);
        }
        long MinuteNum = (overTime / 60) % 60;
        if (MinuteNum > 0)
        {
            return string.Format("{0}分钟", MinuteNum);
        }
        long sNum = overTime % 60;
        if (sNum > 0)
        {
            return string.Format("{0}秒", sNum);
        }
        return string.Format("{0}秒", 0);
    }
    //时间转换年月日时分秒
    public static string ToTimeString(this long seconds)
    {
        //long timeLong = (System.DateTime.Now.Ticks - System.DateTime.UtcNow.Ticks) / 10000;
        //DateTime time = new DateTime(1970, 1, 1);
        //time = time.AddSeconds(seconds);
        //time = time.AddMilliseconds(timeLong);
        string ret = SecondsToDateTime(seconds).ToString("yyyy/MM/dd    HH:mm");
        return ret;
    }
}