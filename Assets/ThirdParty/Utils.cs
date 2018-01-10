using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine.UI;
using System;

public static class Utils
{
    #region 2017_7_26后新加
    public static string ReservationStatus
    {
        set { PlayerPrefs.SetString("ReservationStatus", value); }

        get { return PlayerPrefs.GetString("ReservationStatus"); }
    }
    /// <summary>
    /// 时间格式转换
    /// </summary>
    /// <param name="Time"></param>
    /// <returns></returns>
    public static string TimeConversion(string Time)
    {
        string str = "";
        DateTime t1 = Convert.ToDateTime(Time);
        TimeSpan ts = DateTime.Now.Subtract(t1);
        if (ts.Days > 0)
            str = ts.Days.ToString() + "天前";
        else if (ts.Hours > 0)
            str = ts.Hours.ToString() + "小时前";
        else if (ts.Minutes > 0)
            str = ts.Minutes.ToString() + "分钟前";
        else if (ts.Seconds < 3)
            str = "刚刚";
        else
            str = ts.Seconds.ToString() + "秒前";
        return str;
    }
    #endregion
    /// <summary>
    /// 判断一个字符是否是中文
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static bool isChinese(char c)
    {
        //根据字节码判断
        return c >= 0x4E00 && c <= 0x9FA5;
    }

    /// <summary>
    /// 判断一个字符串是否含有中文
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool isChinese(string str)
    {
        if (str == null) return false;
        foreach (char c in str.ToCharArray())
        {
            //有一个中文字符就返回
            if (isChinese(c))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 获得一个字符串的长度
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int getStrDescLen(string str)
    {
        char[] strArr = str.ToCharArray();
        int length = 0;
        foreach (char c in strArr)
        {
            if (isChinese(c))
            {
                length += 2;
            }
            else
            {
                length += 1;
            }
        }
        return length;
    }

    /// <summary>
    /// 邮箱地址检测
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsMailCheck(string email)
    {
        bool tag = true;
        string expression = @"^([\w-\.]+)@(([0−9]1,3\.[0−9]1,3\.[0−9]1,3\.)|(([\w−]+\.)+))([a−zA−Z]2,4|[0−9]1,3)(?)$";
        Regex regex = new Regex(expression);
        tag = regex.IsMatch(email.Trim());
        return tag;
    }

    /// <summary>
    /// 判断输入的字符串是否是一个合法的手机号
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    //public static bool IsMobilePhone(string input)
    //{
    //    Regex regex = new Regex("^1[3,5,6,7,8]\\d{9}$"); //("[0-9]{11,11}");//new Regex("^13\\d{9}$");
    //    return regex.IsMatch(input.Trim());

    //    //电信手机号正则
    //    //string dianxin = @"^1[3578][01379]\d{8}$";
    //    //Regex dReg = new Regex(dianxin);
    //    //联通手机号正则
    //    string liantong = @"^1[34578][01256]\d{8}$";
    //    Regex tReg = new Regex(liantong);
    //    //移动手机号正则
    //    string yidong = @"^134[012345678]\d{7}|[34578][01235678]\d{8}$";
    //    Regex yReg = new Regex(yidong);
    //    string mobile = input.Trim();
    //    if (dReg.IsMatch(mobile) || tReg.IsMatch(mobile) || yReg.IsMatch(mobile))
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    /// <summary>
    /// 屏蔽非法字符串 (如果有出现非法字符，那么用***来替换)
    /// </summary>
    /// <param name="strText"></param>
    /// <returns></returns>
    //public static string CheckKeyword(string strText)
    //{
    //    ArrayList list = ForbiddenStringManager.GetForbiddenList();
    //    foreach (string str in list)    //循环遍历文件流
    //    {
    //        if (strText.Contains(str))
    //        {
    //            int lg = str.Length;
    //            string sg = "";
    //            for (int i = 0; i < lg; i++)
    //            {
    //                sg += "*";
    //            }
    //            strText = strText.Replace(str, sg);  //如果含有txt文档中的关键字,则替换为"***"
    //        }
    //    }
    //    return strText;
    //}

    /// <summary>
    /// 屏蔽非法字符串 (如果有出现非法字符，那么用***来替换)
    /// </summary>
    /// <param name="strText"></param>
    /// <returns></returns>
    //public static bool CheckIsHaveKeyword(string strText)
    //{
    //    ArrayList list = ForbiddenStringManager.GetForbiddenList();
    //    foreach (string str in list)    //循环遍历文件流
    //    {
    //        if (strText.Contains(str))
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}


    /// <summary>
    /// 根据父节点和要查找的节点的名字，返回节点
    /// </summary>
    /// <param name="parent">父节点的引用</param>
    /// <param name="name">要查找的子节点的名字</param>
    /// <returns>子节点的引用</returns>
    public static GameObject findChildByName(GameObject parent, string name, bool inactive = false)
    {
        Transform[] trans = parent.GetComponentsInChildren<Transform>(inactive);
        GameObject result = null;

        foreach (Transform t in trans)
        {
            if (t.name == name)
            {
                result = t.gameObject;
                return result;
            }
        }

        return result;
    }

    /// <summary>
    /// 根据父节点和要查找的节点的名字，返回节点数组
    /// </summary>
    /// <param name="parent">父节点的引用</param>
    /// <param name="name">要查找的子节点的名字</param>
    /// <returns>子节点的引用</returns>
    public static Transform[] findChildArrByName(GameObject parent, string name, bool inactive = false)
    {
        Transform[] trans = parent.GetComponentsInChildren<Transform>(inactive);
        return trans;
    }

    /// <summary>
    /// 将整形字符数组转成整形数组
    /// </summary>
    /// <param name="str"></param>
    /// <param name="split"></param>
    /// <returns></returns>
    public static int[] toIntArray(string str, string split = ",")
    {
        if (str == null) return null;   //保护

        string[] retStr = str.Split(split.ToCharArray());

        if (retStr.Length == 0)
            return null;

        int[] retInt = new int[retStr.Length];
        for (int i = 0; i < retStr.Length; ++i)
        {
            retInt[i] = int.Parse(retStr[i]);
        }
        return retInt;
    }
    /// <summary>
    /// 把字符串转换成float数组
    /// </summary>
    /// <param name="str"></param>
    /// <param name="split"></param>
    /// <returns></returns>
    public static float[] toFloatArray(string str, string split = ",")
    {
        if (str == null) return null;   //保护

        string[] retStr = str.Split(split.ToCharArray());

        if (retStr.Length == 0)
            return null;

        float[] retInt = new float[retStr.Length];
        for (int i = 0; i < retStr.Length; ++i)
        {
            retInt[i] = float.Parse(retStr[i]);
        }
        return retInt;
    }
    /// <summary>
    /// 把字符串转换成Vector3
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static Vector3 toStringVector(string str, string split = ",")
    {
        float[] Arr = toFloatArray(str, split);
        Vector3 rec = new Vector3(Arr[0], Arr[1], Arr[2]);
        if (str.Length <= 1)
            return Vector3.zero;
        return rec;

    }
    /// <summary>
    /// 将字符串分割转成数组
    /// </summary>
    /// <param name="str"></param>
    /// <param name="split"></param>
    /// <returns></returns>
    public static string[] toStringArray(string str, char split = ',')
    {
        if (str == null) return null;   //保护

        string[] retStr = str.Split(split);
        return retStr;
    }
    /// <summary>
    /// 将字符串数组转换为List
    /// </summary>
    /// <param name="listArr"></param>
    /// <returns></returns>
    public static List<string> toStringArrChangeList(string[] listArr)
    {
        List<string> listRes = new List<string>();
        for (int i = 0; i < listArr.Length; i++)
        {
            listRes.Add(listArr[i]);
        }
        return listRes;
    }

    /// <summary>
    /// 获取随机不同的字符串
    /// </summary>
    /// <param name="listTotal"></param>
    /// <param name="needName"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    //public static List<string> getRandomDiffectStr(List<string> listTotal, string needName, int num)
    //{
    //    List<string> listRes = new List<string>();
    //    listRes.Add(needName);
    //    Hashtable hashtable = new Hashtable();
    //    while (listRes.Count <= num)
    //    {
    //        int randIndex = UnityEngine.Random.Range(0, listTotal.Count);
    //        if (!listRes.Contains(listTotal[randIndex]))
    //        {
    //            listRes.Add(listTotal[randIndex]);
    //        }
    //    }
    //    return listRes;
    //}

    /// <summary>
    /// 两点之间的距离
    /// </summary>
    /// <param name="star"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static float PointStarToPointEndLength(Vector3 star, Vector3 end)
    {
        float disX = Mathf.Abs(star.x - end.x);
        float disY = Mathf.Abs(star.y - end.y);
        float disance = Mathf.Sqrt(disX * disX + disY * disY);
        return disance;
    }

    /// <summary>
    /// 获得两点拖到距离的时间
    /// </summary>
    /// <param name="star"></param>
    /// <param name="end"></param>
    /// <param name="maxTime"></param>
    /// <param name="maxdiance"></param>
    /// <returns></returns>
    public static float moveLengthTime(Vector3 star, Vector3 end, float maxTime, float maxdiance, float lastTime = 0.05f)
    {
        float disance = PointStarToPointEndLength(star, end);
        if (disance > maxdiance)
        {
            return 0;
        }
        float leftDiance = maxdiance - disance;
        float speed = maxdiance / maxTime;
        float time = 0.0f;
        if (lastTime < 0)
        {
            time = leftDiance / speed * 0.4f;//1f; //2.4f
        }
        else
        {
            time = maxTime - disance / speed + lastTime; //maxTime - leftDiance / speed;
        }
        if (time > maxTime)
            time = maxTime;
        if (time < 0)
            time = 0.0f;
        ///Debug.Log("leftDiance=="+ leftDiance+ "  disance "+ disance);
        //Debug.Log("time=="+ time+ "  maxTime"+ maxTime);
        return time;
    }
    /// <summary>
    /// 两点连线  是否在一个三角形范围内
    /// </summary>
    /// <param name="star"></param>
    /// <param name="end1"></param>
    /// <param name="end2"></param>
    /// <param name="targetStar"></param>
    /// <param name="targetEnd"></param>
    /// <returns></returns>
    public static bool pointLineIsInTriangle(Vector3 star, Vector3 end1, Vector3 end2, Vector3 targetStar, Vector3 targetEnd)
    {
        bool resultPoint1 = pointIsInTriangle(star, end1, end2, targetStar);
        bool resultPoint2 = pointIsInTriangle(star, end1, end2, targetEnd);
        bool resultDirect = pointToPointDirect(targetStar, targetEnd);
        bool result = (resultPoint1 && resultPoint2 && resultDirect);
        return result;
    }
    public static bool pointToPointDirect(Vector3 target, Vector3 target2)
    {
        // mu biao kaishi dian 1
        if (target2.y > target.y)
        {
            //Debug.Log("bu zai fan wei nei  target1 point zai shang");
            return false;
        }
        if (target2.x == target.x || target2.y == target.y)
        {
            // Debug.Log("zuo biao yi yang");
            return false;
        }
        return true;
    }

    //显示00：00：00
    public static string getTimeHourMinusSecondColdStr(int startTime)
    {
        if (startTime <= 0)
        {
            return "00:00:00";
        }
        int hour = startTime / 3600;
        int minute = (startTime - 3600 * hour) / 60;
        int second = startTime % 60;
        string hourStr = "";
        string minuteStr = "";
        string secondStr = "";
        if (hour < 10)
        {
            hourStr = "0" + hour.ToString();
        }
        else
        {
            hourStr = "" + hour.ToString();
        }
        if (minute < 10)
        {
            minuteStr = "0" + minute.ToString();
        }
        else
        {
            minuteStr = "" + minute.ToString();
        }
        if (second < 10)
        {
            secondStr = "0" + second.ToString();
        }
        else
        {
            secondStr = "" + second.ToString();
        }
        string stringEndTime = "";
        stringEndTime = hourStr + ":" + minuteStr + ":" + secondStr;
        return stringEndTime;
    }

    //显示00：00
    public static string getTimeMinusSecondColdStr(int startTime)
    {
        if (startTime <= 0)
        {
            return "00:00";
        }
        int hour = startTime / 3600;
        int minute = (startTime - 3600 * hour) / 60;
        int second = startTime % 60;
        string minuteStr = "";
        string secondStr = "";
        if (minute < 10)
        {
            minuteStr = "0" + minute.ToString();
        }
        else
        {
            minuteStr = "" + minute.ToString();
        }
        if (second < 10)
        {
            secondStr = "0" + second.ToString();
        }
        else
        {
            secondStr = "" + second.ToString();
        }
        string stringEndTime = "";
        stringEndTime = minuteStr + ":" + secondStr;
        return stringEndTime;
    }




    /// <summary>
    /// 判断一个点是否在三角形范围内
    /// </summary>
    /// <param name="star"> 位置起点1</param>
    /// <param name="end1">位置点2</param>
    /// <param name="end2">位置点3</param>
    /// <param name="target">目标点</param>
    /// <returns></returns>
    public static bool pointIsInTriangle(Vector3 A, Vector3 B, Vector3 C, Vector3 P)
    {
        float signOfTrig = (B.x - A.x) * (C.y - A.y) - (B.y - A.y) * (C.x - A.x);
        float signOfAB = (B.x - A.x) * (P.y - A.y) - (B.y - A.y) * (P.x - A.x);
        float signOfCA = (A.x - C.x) * (P.y - C.y) - (A.y - C.y) * (P.x - C.x);
        float signOfBC = (C.x - B.x) * (P.y - C.y) - (C.y - B.y) * (P.x - C.x);

        bool d1 = (signOfAB * signOfTrig > 0);
        bool d2 = (signOfCA * signOfTrig > 0);
        bool d3 = (signOfBC * signOfTrig > 0);
        return d1 && d2 && d3;
    }

    /// <summary>
    /// 判断一个点是否在圆形区域内
    /// </summary>
    /// <param name="targetPoint"></param>
    /// <param name="mousePoint"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static bool pointIsInCircular(Vector3 targetPoint, Vector3 mousePoint, float radius, float targetRadius = 0.0f)
    {
        float disX = Mathf.Abs(targetPoint.x - mousePoint.x);
        float disY = Mathf.Abs(targetPoint.y - mousePoint.y);
        float disance = Mathf.Sqrt(disX * disX + disY * disY);
        bool result = disance < radius + targetRadius;
        return result;
    }

    //public static Vector3 WorldToUI(Vector3 pos)
    //{
    //    //float resolutionX = canvasScaler.referenceResolution.x;

    //    //float resolutionY = canvasScaler.referenceResolution.y;
    //    Vector3 viewportPos = Camera.main.WorldToViewportPoint(pos);
    //    //Vector3 uiPos = new Vector3(viewportPos.x * resolutionX - resolutionX* 0.5f,viewportPos.y * resolutionY - resolutionY *0.5f,0);
    //    Vector3 uiPos = pos;//new Vector3(viewportPos.x * resolutionX, viewportPos.y * resolutionY, 0);
    //    return uiPos;
    //}

    public static void WorldToUI(GameObject imgHitdrag)
    {
        float xoffset, yoffset;
        xoffset = 1334f / (float)Screen.width * Input.mousePosition.x;
        yoffset = 750f / (float)Screen.height * Input.mousePosition.y;
        imgHitdrag.GetComponent<RectTransform>().anchoredPosition = new Vector2(xoffset, yoffset);

        //currentButton.GetComponent<Transform>().localPosition = localPoint;
    }

    /// <summary>
    /// 获得拖动点的位置
    /// </summary>
    /// <param name="starPoint"></param>
    /// <returns></returns>
    public static Vector3 WorldToUIHitDrag(Vector3 starPoint)
    {
        float xoffset, yoffset;
        xoffset = 1334f / (float)Screen.width * starPoint.x;
        yoffset = 750f / (float)Screen.height * starPoint.y;
        Vector3 vecPos = new Vector3(xoffset, yoffset, 0);
        return vecPos;
    }

    /// <summary>
    /// 获得拖动点的位置
    /// </summary>
    /// <param name="starPoint"></param>
    /// <returns></returns>
    public static Vector3 WorldToUIHitDrag(Vector3 starPoint, Vector3 offset)
    {
        float xoffset, yoffset;
        xoffset = 1334f / (float)Screen.width * starPoint.x;
        yoffset = 750f / (float)Screen.height * starPoint.y;
        Vector3 vecPos = new Vector3(xoffset + offset.x, yoffset + offset.y, 0);
        return vecPos;
    }


    /// <summary>
    /// 把字符串转化成整形
    /// </summary>
    /// <param name="nameValue"></param>
    /// <param name="xe"></param>
    /// <returns></returns>
    public static int getGetAttributeXMLStrInt(string nameValue, XmlElement xe)
    {
        string nameStr = xe.GetAttribute(nameValue);
        if (nameStr.Length > 0)
        {
            return int.Parse(nameStr);
        }
        else
        {
            return 0;
        }
    }

    public static string GetWWWPath(string path, string name)
    {
        string s = null;
#if UNITY_ANDROID
       s = "jar:file://"+path+"/"+name;  
#elif UNITY_IPHONE
       s = path+"/"+name;  
#elif UNITY_EDITOR || UNITY_STANDALONE
        s = "file://" + path + "/" + name;
#else
        s = path + "/" + name;
#endif
        return s;
    }

    public static void HightLight(GameObject go, bool value)
    {
        if (value)
            go.GetComponent<MeshRenderer>().material.SetFloat("_node_2339", 0.001F);
        else
            go.GetComponent<MeshRenderer>().material.SetFloat("_node_2339", 0.0F);
    }


    public static void setTextColor(Text textName, Color colorValue)
    {
        textName.color = colorValue;
    }
    static public GameObject AddChild(GameObject parent) { return AddChild(parent, true); }
    static public GameObject AddChild(GameObject parent, bool undo)
    {
        GameObject go = new GameObject();
#if UNITY_EDITOR
        if (undo) UnityEditor.Undo.RegisterCreatedObjectUndo(go, "Create Object");
#endif
        if (parent != null)
        {
            Transform t = go.transform;
            t.parent = parent.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            go.layer = parent.layer;
        }
        return go;
    }

}
