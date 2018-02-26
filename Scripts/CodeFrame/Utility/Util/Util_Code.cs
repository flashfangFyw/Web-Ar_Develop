using UnityEngine;
using System.Collections;
using ffDevelopmentSpace;
using System;


/* 
    Author:     fyw 
    CreateDate: 2018-02-25 17:21:15 
    Desc:       字符转化工具 
*/


public class Util_Code: MonoBehaviour 
{
    public static int Int(object o)
    {
        return Convert.ToInt32(o);
    }

    public static float Float(object o)
    {
        return (float)Math.Round(Convert.ToSingle(o), 2);
    }

    public static long Long(object o)
    {
        return Convert.ToInt64(o);
    }
}
