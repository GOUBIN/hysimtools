using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public static class HRT_TypeExtension
{
    #region string


    ///"utf-8"
    public static string DecodeBase64(this string code, string code_type)
    {
        string decode = "";
        byte[] bytes = Convert.FromBase64String(code);
        try
        {
            decode = Encoding.GetEncoding(code_type).GetString(bytes);
        }
        catch
        {
            decode = code;
        }
        return decode;
    }


    //"utf-8"
    public static string EncodeBase64(this string code, string code_type)
    {
        string encode = "";
        byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code);
        try
        {
            encode = Convert.ToBase64String(bytes);
        }
        catch
        {
            encode = code;
        }
        return encode;
    }


    public static Vector3 ToVector3(this string str)
    {
        
        string[] strs = str.Split(',');
        Vector3 vector3 = new Vector3(strs[0].ToFloat(), strs[1].ToFloat(), strs[2].ToFloat());
        return vector3;
    }

    public static Vector2 ToVector2(this string str)
    {
        string[] strs = str.Split(',');
        Vector2 vector2 = new Vector2(strs[0].ToFloat(), strs[1].ToFloat());
        return vector2;
    }

    public static int ToInt(this string str)
    {

        return int.Parse(str);
    }

    public static float ToFloat(this string str)
    {
        
        return float.Parse(str);
    }

    public static double ToDouble(this string str)
    {
        return double.Parse(str);
    }


    public static bool IsContainsChinese(this string str)
    {
        bool temp = false;
        foreach (char item in str)
        {
            if (item >= 0x4E00 && item <= 0x29FA5)
            {
                temp = true;
            }
        }
        return temp;
    }
    public static List<string> GetList(this string str, char key)
    {
        string[] array = str.Split(key);

        return array.ToList();
    }

    public static bool NullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }


    public static string GetPinYin(this string str)
    {

        return HRT_SortChinese.GetFirstPY(str);
    }
    #endregion

    #region Array <=> List
    public static List<T> ToList<T>(this T[] array) 
    {
        List<T> temp = new List<T>(array);
        return temp;
    }

    public static List<T> GetClone<T>(this List<T> ary)
    {
        T[] temp = new T[ary.Count];
        ary.CopyTo(temp);
        return temp.ToList();
    }

    public static List<T> And<T>(this List<T> ary,List<T> temp)
    {
        ary.AddRange(temp);
        return ary;
    }
    #endregion


    #region Dictionary ->Keys
    public static List<k> GetKeys<k,v>(this Dictionary<k,v> dic)
    {
        List<k> keys = new List<k>();
        foreach (k item in dic.Keys)
        {
            keys.Add(item);
        }

        return keys;
    }
    #endregion

    #region Enum
    /// <summary>
    /// 将枚举转为集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<string> EnumToList<T>(this Enum myenum)
    {
        List<string> list = new List<string>();

        foreach (var item in Enum.GetNames(typeof(T)))
        {
            list.Add(item);
        }


        //foreach (var e in Enum.GetValues(myenum))
        //{
        //    list.Add(e.ToString());
        //}
        return list;
    }
    #endregion

    public static void SetText(this UnityEngine.UI.Text text)
    {
        string str = text.text;
        string result = HRT_AppConfig.Instance.Get(str);
        if (result != "")
        {
            text.text =result;
        }
    }


    public static bool IsIn(this double val,double min, double max)
    {
        if (val >= min && val<=max)
        {
            return true;
        }else
        {
            return false;
        }
    }
}
