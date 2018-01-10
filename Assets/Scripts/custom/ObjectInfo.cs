using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{

    public static ObjectInfo _instance;
    public TextAsset objectsInfoLIst;

    //定义一个字典存放数据
    private Dictionary<int, ObjectOnInfo> objectInfoDict = new Dictionary<int, ObjectOnInfo>();

    private void Awake()
    {
        _instance = this;
        ReadInfo();
        // print(objectInfoDict.Keys.Count);
    }

    public ObjectOnInfo GetObjctInfoById(int id)
    {
        ObjectOnInfo info = new ObjectOnInfo();
        objectInfoDict.TryGetValue(id, out info);
        return info;
    }

    void ReadInfo()
    {
        string text = objectsInfoLIst.text;
        string[] strArray = text.Split('\n');
        ObjectOnInfo info; //存储读取的信息

        foreach (string str in strArray)
        {
            info = new ObjectOnInfo();
            string[] proArray = str.Split(',');
            int id = int.Parse(proArray[0]);
            string name = proArray[1];
            string icon_name = proArray[2];
            string str_type = proArray[3];
            ObjectType type = ObjectType.Drug;
            switch (str_type)
            {
                case "Drug":
                    type = ObjectType.Drug;
                    break;
                case "Equip":
                    type = ObjectType.Equip;
                    break;
                case "Mat":
                    type = ObjectType.Mat;
                    break;
                default:
                    break;
            }
            info.id = id;
            info.name = name;
            info.icon_name = icon_name;
            info.type = type;
            if (type == ObjectType.Drug)
            {
                int hp = int.Parse(proArray[4]);
                int mp = int.Parse(proArray[5]);
                int price_sell = int.Parse(proArray[6]);
                int price_buy = int.Parse(proArray[7]);
                info.hp = hp;
                info.mp = mp;
                info.price_sell = price_sell;
                info.price_buy = price_buy;
            }
            else if (type == ObjectType.Equip)
            {
                info.attack = int.Parse(proArray[4]);
                info.def = int.Parse(proArray[5]);
                info.speed = int.Parse(proArray[6]);
                info.price_sell = int.Parse(proArray[9]);
                info.price_buy = int.Parse(proArray[10]);
                string str_dressType = proArray[7];
                switch (str_dressType)
                {
                    case "Headgear":
                        info.dressType = DressType.Headgear;
                        break;
                    case "Armor":
                        info.dressType = DressType.Armor;
                        break;
                    case "Right_Hand":
                        info.dressType = DressType.Right_Hand;
                        break;
                    case "Left_Hand":
                        info.dressType = DressType.Left_Hand;
                        break;
                    case "Shoe":
                        info.dressType = DressType.Shoe;
                        break;
                    case "Accessory":
                        info.dressType = DressType.Accessory;
                        break;
                    default:
                        break;
                }
                string str_applicationType = proArray[8];
                switch (str_applicationType)
                {
                    case "Swordman":
                        info.applicationType = ApplicationType.Swordman;
                        break;
                    case "Magician":
                        info.applicationType = ApplicationType.Magician;
                        break;
                    case "Common":
                        info.applicationType = ApplicationType.Common;
                        break;
                    default:
                        break;
                }
            }
            objectInfoDict.Add(id, info);//添加数据到字典中，ID为key，可以很方便的根据ID查看数据
        }
    }
}

//战士类型
public enum ApplicationType
{
    Swordman,//战士
    Magician,//法师
    Common//职业通用
}
//物品类型
public enum ObjectType
{
    Drug,
    Equip,
    Mat
}
//装备穿戴类型
public enum DressType
{
    Headgear,
    Armor,
    Right_Hand,
    Left_Hand,
    Shoe,
    Accessory
}

public class ObjectOnInfo
{
    public int id;
    public string name;
    public string icon_name;//这个名称是存储在图集中的名称
    public ObjectType type;
    public int hp;
    public int mp;
    public int price_sell;
    public int price_buy;

    public int attack;//攻击力
    public int def;//防御力
    public int speed;//速度
    public DressType dressType;//穿戴类型
    public ApplicationType applicationType;//职业类型
}
