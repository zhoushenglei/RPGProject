//**************************************************************************
// 
//  文件名称(File Name)：Helper.cs
//
//  功能描述(Description)：帮助类
//
//  作者(Author)：Mr、Chen
//
//  日期(Create Date)：2015.4.14
//
//  修改记录(Revision History)：
//
//**************************************************************************

using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Xml;
using System.Collections.Generic;


/// <summary>
/// 帮助类
/// </summary>
public static class Helper 
{
    //事件帮助的全局引用
    //public static EventHelper eventHelper;

    //全局声音监听
    public static GameObject globalAudioListener;

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
	///  根据父节点和要查找的节点的名字，返回多个节点
	/// </summary>
	/// <param name="parent"></param>
	/// <param name="name"></param>
	/// <param name="inactive"></param>
	/// <returns></returns>
	public static GameObject[] findChildsByName(GameObject parent, string name, bool inactive = true)
	{
		Transform[] trans = parent.GetComponentsInChildren<Transform>(inactive);
		List<GameObject> results = new List<GameObject>();

		foreach (Transform t in trans)
		{
			if (t.name == name)
			{
				results.Add(t.gameObject);
			}
		}

		return results.ToArray();
	}

	/// <summary>
	/// 获取皮肤
	/// </summary>
	/// <param name="lodNode"></param>
	/// <returns></returns>
	public static SkinnedMeshRenderer getEntitySkin(GameObject go)
	{
		Transform[] transChild = go.GetComponentsInChildren<Transform>();
		foreach (Transform tran in transChild)
		{
			if (tran.GetComponent<SkinnedMeshRenderer>() != null)
			{
				return tran.GetComponent<SkinnedMeshRenderer>();
			}
		}
		return null;
	}

    /// <summary>
    /// 删除所有的子单位
    /// </summary>
    /// <param name="parent"></param>
    public static void removeAllChild(Transform parent)
    {
        Transform[] trans = parent.GetComponentsInChildren<Transform>(true);

        foreach (Transform t in trans)
        {
            if (t != parent)
            {
                GameObject.Destroy(t.gameObject);
            }
        }
    }

    /// <summary>
    /// 设置层编号
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="no"></param>
    public static void setLayerNo(GameObject parent, int no)
    {
        Transform[] trans = parent.GetComponentsInChildren<Transform>();

        foreach (Transform t in trans)
        {
            t.gameObject.layer = no;
        }
    }

    /// <summary>
    /// 设置gameobject 的可见性
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="value"></param>
    public static void setGameObjectVisible(GameObject obj, bool value)
    {
        if (value)
            showGameObject(obj);
        else
            hideGameObject(obj);
    }
    /// <summary>
    ///隐藏单位
    /// </summary>
    /// <param name='obj'>
    /// Object.
    /// </param>
    public static void hideGameObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    /// <summary>
    /// 显示单位
    /// </summary>
    /// <param name='obj'>
    /// Object.
    /// </param>
    public static void showGameObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    /// <summary>
    /// 返回2个点的平面距离，忽略Y的值
    /// </summary>
    /// <param name="one"></param>
    /// <param name="two"></param>
    /// <returns></returns>
    public static float getTwoPointDistance(Vector3 one, Vector3 two)
    {
        Vector2 vecOne = new Vector2(one.x, one.z);
        Vector2 vecTwo = new Vector2(two.x, two.z);
        Vector2 result = vecOne - vecTwo;


        return result.magnitude;
    }
    #region XML的数据读取

    /// <summary>
    /// 将xml的字符串属性变成int
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="attributeName">属性名称</param>
    /// <returns></returns>
    public static int xml2Int(XmlReader reader, string attributeName)
    {
        int result = 0;

        if (reader[attributeName] == null)
        {
            return result;
        }
        else
        {
            if (reader[attributeName] != "")
            {
                result = int.Parse(reader[attributeName]);
            }
        }

        return result;
    }

    /// <summary>
    /// 将xml的字符串属性变成string
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="attributeName">属性名称</param>
    /// <returns></returns>
    public static string xml2String(XmlReader reader, string attributeName)
    {
        string result = "";

        if (reader[attributeName] == null)
        {
            return result;
        }
        else
        {

            result = reader[attributeName];
        }

        return result;
    }

    /// <summary>
    /// 将xml的字符串属性变成float
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="attributeName">属性名称</param>
    /// <returns></returns>
    public static float xml2Float(XmlReader reader, string attributeName)
    {
        float result = 0;

        if (reader[attributeName] == null)
        {
            return result;
        }
        else
        {
            if (reader[attributeName] != "")
            {
                result = float.Parse(reader[attributeName]);
            }
        }

        return result;
    }

    /// <summary>
    /// 将xml的字符串属性变成bool
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="attributeName">属性名称</param>
    /// <returns></returns>
    public static bool xml2Bool(XmlReader reader, string attributeName)
    {
        bool result = false;

        if (reader[attributeName] == null)
        {
            return result;
        }
        else
        {
            if (reader[attributeName] != "")
            {
                int value = int.Parse(reader[attributeName]);

                result = !(value == 0);
            }
        }

        return result;
    }

    #endregion
    
    #region 判断范围

    /// <summary>
    /// 返回目标点是不是在圆的里面
    /// </summary>
    /// <param name="origin">原点</param>
    /// <param name="target">目标点</param>
    /// <param name="radius">半径</param>
    /// <returns></returns>
    public static bool isInCircular(Vector2 origin, Vector2 target, float radius)
    {
        //计算2点的距离
        float distance = (origin - target).magnitude;

        //判断在不在圆的范围呢
        return distance <= radius;
    }

    /// <summary>
    /// 返回目标点是不是在扇形的里面
    /// </summary>
    /// <param name="origin">原点</param>
    /// <param name="target">目标点</param>
    /// <param name="radius">半径</param>
    /// <param name="degree">角度</param>
    /// <param name="faceDegree">原点的朝向角度</param>
    /// <returns></returns>
    public static bool isInSector(Vector2 origin,
        Vector2 target,
        float radius,
        int degree,
        float faceDegree)
    {
        bool result = false;

        //计算2点的距离
        float distance = (origin - target).magnitude;

        if(distance <= radius)
        {
            //计算夹角
            float deg = Vector2.Angle(origin- target, Vector2.up);
            deg = Math.Abs(deg - faceDegree);
            Debug.DrawLine(new Vector3(origin.x, 23.0f, origin.y), new Vector3(target.x, 23.0f, target.y), Color.black, 5.0f);
            result = deg <= (degree / 2);
        }
        

        //判断在不在圆的范围呢
        return result;
    }

    /// <summary>
    /// 将一个Vector2绕一个原点旋转
    /// </summary>
    /// <param name="origion">原点</param>
    /// <param name="target">要旋转的点</param>
    /// <param name="angle">旋转的角度</param>
    /// <returns>旋转后的点</returns>
    public static Vector2 rotateVector2(Vector2 origion, Vector2 target, float angle)
    {
        Vector2 vec2Dir = target - origion;
        float entityAngles = angleWithRight(vec2Dir);
        float newAngles = entityAngles + angle;  //计算夹角
        Vector2 newVec2 = new Vector2(Mathf.Cos(Mathf.Deg2Rad * newAngles), Mathf.Sin(Mathf.Deg2Rad * newAngles));
        newVec2 = newVec2 * vec2Dir.magnitude + origion;
        return newVec2;
    }

    /// <summary>
    /// 返回向量与Vector2.right的角度，带符号的
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static float angleWithRight(Vector2 from)
    {
        float resultAngles = Vector2.Angle(from, Vector2.right);
        float dot = Vector2.Dot(new Vector2(from.x, from.y), Vector2.up);
        if (dot != 0)
        {
            resultAngles = resultAngles * Mathf.Abs(dot) / dot;
        }

        return resultAngles;
    }

    #endregion

    /// <summary>
    /// 将ArrayList里面的byte数组进行组合，拼接到输出byte数组
    /// </summary>
    /// <param name="toBytes">输出byte数组</param>
    /// <param name="bytesArray">存有byte数组的arrayList</param>
    public static byte[] buildBytesWithArrayList(ArrayList bytesArray)
    {
        int sourceIndex = 0;
        int length = 0;
        byte[] result;
        foreach (byte[] bytes in bytesArray)
        {
            length += bytes.Length;
        }

        result = new byte[length];
        foreach(byte[] bytes in bytesArray)
        {
            Array.Copy(bytes, 0, result, sourceIndex, bytes.Length);
            sourceIndex += bytes.Length;
        }

        return result;
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
        for ( int i = 0; i < retStr.Length; ++i )
        {
            retInt[i] = int.Parse(retStr[i]);
        }
        return retInt;
    }

   

    /// 合并数组
    /// </summary>
    /// <param name="First">第一个数组</param>
    /// <param name="Second">第二个数组</param>
    /// <returns>合并后的数组(第一个数组+第二个数组，长度为两个数组的长度)</returns>
    public static int[] MergerArray(int[] First, int[] Second)
    {
        int[] result = new int[First.Length + Second.Length];
        First.CopyTo(result, 0);
        Second.CopyTo(result, First.Length);
        return result;
    }

    
    

    /// <summary>
    /// 将Bytes数组打印出来
    /// </summary>
    /// <param name="bytes"></param>
    public static void logBytes(byte[] bytes)
    {
        string info = "输出字节数组：";
        foreach(byte bt in bytes)
        {
            info += " " + (Convert.ToString(bt, 16));
        }
        MonoBehaviour.print(info);
        MonoBehaviour.print("是否是小端：" + BitConverter.IsLittleEndian);
    }

    /// <summary>
    /// 转化为大头在前
    /// </summary>
    /// <param name="data">short类型数据</param>
    /// <returns></returns>
    public static short getBigEndian(short data)
    {
        if (BitConverter.IsLittleEndian)
        {
           return IPAddress.HostToNetworkOrder(data);
        }
        else
        {
            return data;
        }
    }

    /// <summary>
    /// 转化为大头在前
    /// </summary>
    /// <param name="data">int类型数据</param>
    /// <returns></returns>
    public static int getBigEndian(int data)
    {
        if (BitConverter.IsLittleEndian)
        {
            return IPAddress.HostToNetworkOrder(data);
        }
        else
        {
            return data;
        }
    }

    /// <summary>
    /// 转化为大头在前
    /// </summary>
    /// <param name="data">long类型数据</param>
    /// <returns></returns>
    public static long getBigEndian(long data)
    {
        if (BitConverter.IsLittleEndian)
        {
            return IPAddress.HostToNetworkOrder(data);
        }
        else
        {
            return data;
        }
    }

    /// <summary>
    /// 转化为小头在前
    /// </summary>
    /// <param name="data">short类型数据</param>
    /// <returns></returns>
    public static short getLittleEndian(short data)
    {
        if(BitConverter.IsLittleEndian)
        {
            return IPAddress.NetworkToHostOrder(data);
        }
        else
        {
            return data;
        }
    }

    /// <summary>
    /// 转化为小头在前
    /// </summary>
    /// <param name="data">int类型数据</param>
    /// <returns></returns>
    public static int getLittleEndian(int data)
    {
        if (BitConverter.IsLittleEndian)
        {
            return IPAddress.NetworkToHostOrder(data);
        }
        else
        {
            return data;
        }
    }

    /// <summary>
    /// 转化为小头在前
    /// </summary>
    /// <param name="data">long类型数据</param>
    /// <returns></returns>
    public static long getLittleEndian(long data)
    {
        if (BitConverter.IsLittleEndian)
        {
            return IPAddress.NetworkToHostOrder(data);
        }
        else
        {
            return data;
        }
    }

    /// <summary>
    /// 将byte转化为bool
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool boolFromByte(byte b)
    {
        if (b == 0x01)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

  

    
  
}
