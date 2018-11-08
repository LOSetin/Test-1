using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arrow.Framework
{
    /// <summary>
    /// 金额数字转换为大写
    /// </summary>
    public static class MoneyHelper
    {
        public static string ConvertToUpper(double inputNum)
        {
            if (inputNum > 0)
            {
                string strTemp = inputNum.ToString("f2");
                return ProcessLeft(strTemp) + ProcessRight(strTemp);
            }
            return string.Empty;
        }

        #region 处理整数部分
        private static string ProcessLeft(string inputNum)
        {
            int temp = Convert.ToInt32(inputNum.Substring(0, inputNum.IndexOf('.')));
            Stack<string> processStack = new Stack<string>();
            string[] separate = { "拾", "佰", "仟", "万", "十", "佰", "仟", "亿" };
            int i = 0;
            if (temp < 10)
            {
                return ConvertNumToUpper(temp);
            }
            else
            {
                while (true)
                {
                    if (temp % 10 != 0)
                    {
                        if (i != 0)
                            processStack.Push(separate[i - 1]);
                        processStack.Push(ConvertNumToUpper(temp % 10));
                    }
                    else
                    {
                        if (processStack.Count != 0 && processStack.Peek() != "零")
                            processStack.Push("零");

                    }
                    i++;
                    temp = temp / 10;
                    if (temp == 0)
                        break;
                }
            }
            string returnStr = "";
            while (processStack.Count != 0)
                returnStr = returnStr + processStack.Pop();
            return returnStr;
        }
        #endregion

        #region 处理小数部分
        private static string ProcessRight(string inputNum)
        {
            int temp1 = Convert.ToInt32(inputNum.Substring(inputNum.IndexOf('.') + 1, 1));
            int temp2 = Convert.ToInt32(inputNum.Substring(inputNum.IndexOf('.') + 2, 1));
            if (temp1 == 0 && temp2 == 0)
                return "圆整";
            else
                return "圆零" + ConvertNumToUpper(temp1) + "角" + ConvertNumToUpper(temp2) + "分";

        }
        #endregion

        #region 数字转换成大写
        private static string ConvertNumToUpper(int input)
        {
            string[] UpperNum = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            if (input <= 9)
            {
                return UpperNum[input];
            }
            else
            {
                throw new Exception("转换错误");
            }
        }
        #endregion
    }
}
