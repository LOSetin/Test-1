using System;

public class OrderNumFactory
{
    private static object locker = new object();
    private static int sn = 0;
    public static string NextNum()
    {
        lock (locker)
        {
            if (sn == 999999999)
                sn = 0;
            else
                sn++;
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + sn.ToString().PadLeft(10, '0');
        }
    }

    // 防止创建类的实例
    private OrderNumFactory() { }

}