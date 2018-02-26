using sy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using ffDevelopmentSpace;

public class CommUtils
{
   
    public static Color colorHx16toRGB(string strHxColor)
    {
        if (!strHxColor.StartsWith("#"))
            strHxColor = "#" + strHxColor;
        if (strHxColor.Length == 7)
        {
            return new Color(Int32.Parse(strHxColor.Substring(1, 2), System.Globalization.NumberStyles.HexNumber) / 255.0f, Int32.Parse(strHxColor.Substring(3, 2), System.Globalization.NumberStyles.HexNumber) / 255.0f, Int32.Parse(strHxColor.Substring(5, 2), System.Globalization.NumberStyles.HexNumber) / 255.0f);
        }
        else if (strHxColor.Length == 9)
        {
            return new Color(Int32.Parse(strHxColor.Substring(1, 2), System.Globalization.NumberStyles.HexNumber) / 255.0f, Int32.Parse(strHxColor.Substring(3, 2), System.Globalization.NumberStyles.HexNumber) / 255.0f, Int32.Parse(strHxColor.Substring(5, 2), System.Globalization.NumberStyles.HexNumber) / 255.0f, Int32.Parse(strHxColor.Substring(7, 2), System.Globalization.NumberStyles.HexNumber) / 255.0f);
        }
        else
        {
            return Color.white;
        }
    }

    public static int getItemAttributeValue(List<ItemAttribute> attr, ItemAttributeKind key)
    {
        foreach (ItemAttribute item in attr)
        {
            if (item != null && item.key == (int)key)
            {
                return item.value;
            }
        }

        return 0;
    }

    public static void setItemAttributeValue(List<ItemAttribute> attr, ItemAttributeKind key, int value)
    {
        bool bFound = false;
        foreach (ItemAttribute item in attr)
        {
            if (item != null && item.key == (int)key)
            {
                item.value = value;
                bFound = true;
                break;
            }
        }

        if (!bFound)
        {
            ItemAttribute item = new ItemAttribute();
            item.key = (int)key;
            item.value = value;
            attr.Add(item);
        }
    }



    public static UITweener createUITweener(RectTransform recTra, TweenType type)
    {
        TweenPosition twPos = new TweenPosition();
        float posY = recTra.anchoredPosition.y;
        switch (type)
        {
            case TweenType.TOP_IN:
                recTra.anchoredPosition = new Vector2(recTra.anchoredPosition.x, recTra.sizeDelta.y);
                twPos.from = recTra.anchoredPosition;
                twPos.to = new Vector2(recTra.anchoredPosition.x, posY);
                twPos.method = UITweener.Method.QuintEaseInOut;
                twPos.duration = .5f;
                break;

            case TweenType.TOP_OUT:
                twPos.from = recTra.anchoredPosition;
                twPos.to = new Vector2(recTra.anchoredPosition.x, recTra.sizeDelta.y);
                twPos.method = UITweener.Method.QuintEaseInOut;
                twPos.duration = .4f;
                break;
        }

        twPos.trans = recTra;
        return twPos;
    }

    /// 将html文本转化为 文本内容方法NoHTML
    public static string htmltotext(string Htmlstring)
    {
        //删除脚本
        Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
        //删除HTML
        Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"([/r/n])[/s]+", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "/", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "/xa1", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "/xa2", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "/xa3", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "/xa9", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&#(/d+);", "", RegexOptions.IgnoreCase);
        //替换掉 < 和 > 标记
        Htmlstring.Replace("<", "");
        Htmlstring.Replace(">", "");
        Htmlstring.Replace("\n", "\r\n");
        //返回去掉html标记的字符串
        return Htmlstring;
    }

    public static string get_uft8(string unicodeString)
    {
        UTF8Encoding utf8 = new UTF8Encoding();
        Byte[] encodedBytes = utf8.GetBytes(unicodeString);
        String decodedString = utf8.GetString(encodedBytes);
        return decodedString;
    }


    //获取美术指定的表现颜色
    public static string GetStandardColor(int index, out Color clColor)
    {
        string strColor = "#f0f0f0";
        clColor = Color.white;
        switch (index)
        {
            case 1://普通文字类型
                strColor = "#f0f0f0";
                break;

            case 2://弱化文字类型
                strColor = "#99a6b2";
                break;

            case 3://绿色品质文字
                strColor = "#58a344";
                break;

            case 4://蓝色品质文字
                strColor = "#508ab2";
                break;

            case 5://紫色品质文字
                strColor = "#8e50b2";
                break;

            case 6://金色品质文字
                strColor = "#c8ac3b";
                break;

            case 7://红色品质文字
                strColor = "#d0682c";
                break;

            case 8://增益提升文字
                strColor = "#61ea56";
                break;

            case 9://提示不足文字
                strColor = "#e74f4c";
                break;

            case 10://TITAL类型文字
                strColor = "#a1a7b0";
                break;

            case 11://数字123
                strColor = "#f0f0f0";
                break;

            case 12://数字123456
                strColor = "#f0f0f0";
                break;

            case 13://伤害弹出数字 红
                strColor = "#e74f4c";
                break;

            case 14://伤害弹出数字 绿
                strColor = "#61ea56";
                break;
        }

        if (!ColorUtility.TryParseHtmlString(strColor, out clColor))
        {
            clColor = Color.white;
        }
        return strColor;
    }

    public static string GetStandardColor(int index)
    {
        string strColor = "#f0f0f0";
        switch (index)
        {
            case 1://普通文字类型
                strColor = "#f0f0f0";
                break;

            case 2://弱化文字类型
                strColor = "#879095";
                break;

            case 3://绿色品质文字
                strColor = "#58a344";
                break;

            case 4://蓝色品质文字
                strColor = "#508ab2";
                break;

            case 5://紫色品质文字
                strColor = "#8e50b2";
                break;

            case 6://金色品质文字
                strColor = "#c8ac3b";
                break;

            case 7://红色品质文字
                strColor = "#d0682c";
                break;

            case 8://增益提升文字
                strColor = "#61ea56";
                break;

            case 9://提示不足文字
                strColor = "#e74f4c";
                break;

            case 10://TITAL类型文字
                strColor = "#a1a7b0";
                break;

            case 11://数字123
                strColor = "#f0f0f0";
                break;

            case 12://数字123456
                strColor = "#f0f0f0";
                break;

            case 13://伤害弹出数字 红
                strColor = "#e74f4c";
                break;

            case 14://伤害弹出数字 绿
                strColor = "#61ea56";
                break;
        }
        return strColor;
    }

    

    //计算战斗力通过OtherPlayerInfo
    public static int CalculatedCombat(OtherPlayerInfo playerInfo)
    {
        if (playerInfo == null) return 0;
        int totalCombat = 0;
        for (int i = 0; i < playerInfo.heros.Count; i++)
        {
            totalCombat += playerInfo.heros[i].attr1[0];
        }
        return totalCombat;
    }
}