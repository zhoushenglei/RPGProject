using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour {

    public static EquipmentUI _instance;
    public GameObject equipment;

    private TweenPosition twee;
    private bool isShow = false;

    private GameObject headgear;
    private GameObject armor;
    private GameObject right_Hand;
    private GameObject left_Hand;
    private GameObject shoe;
    private GameObject accessory;
    private PlayerStatus ps;//挂载人物下的PlayerStatus类

    private int attack = 0;
    private int def = 0;
    private int speed = 0;

    void Awake()
    {
        _instance = this;
        twee = this.GetComponent<TweenPosition>();

        headgear = transform.Find("Headgear").gameObject;
        armor = transform.Find("Armor").gameObject;
        right_Hand = transform.Find("Right_Hand").gameObject;
        left_Hand = transform.Find("Left_Hand").gameObject;
        shoe = transform.Find("Shoe").gameObject;
        accessory = transform.Find("Accessory").gameObject;

        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
    }

    public void ShowEquipment() {
        if (isShow == false) {
            twee.PlayForward();
            isShow = true;
        } else if (isShow) {
            twee.PlayReverse();
            isShow = false;
        }
    }

    //处理装备的穿戴与脱下
    public bool DressEquipment(int id) {
        ObjectOnInfo info = ObjectInfo._instance.GetObjctInfoById(id);
        if (info.type != ObjectType.Equip) {
            return false;
        }
        if (ps.heroType == HeroType.Magician) {
            if (info.applicationType == ApplicationType.Swordman) {
                return false;
            }
        }
        if (ps.heroType == HeroType.Swordman) {
            if (info.applicationType == ApplicationType.Magician) {
                return false;
            }
        }

        GameObject parent = null;
        switch (info.dressType) {
            case DressType.Headgear:
                //print("----这是头盔");
                parent = headgear;
                break;
            case DressType.Armor:
                //print("----这是盔甲");
                parent = armor;
                break;
            case DressType.Left_Hand:
                parent = left_Hand;
                //print("------这是左手");
                break;
            case DressType.Right_Hand:
                //print("----这是右手");
                parent = right_Hand;
                break;
            case DressType.Shoe:
                //print("----这是鞋子");
                parent = shoe;
                break;
            case DressType.Accessory:
                //print("----这是饰品");
                parent = accessory;
                break;
            default:
                break;
        }
        EquipmentItem item = parent.GetComponentInChildren<EquipmentItem>();
       // EquipmentItem._instance.setInfo(info);
        if (item != null)
        {
            Inventory._instance.GetId(item.id);//把已经穿戴的装备脱下放回背包
            item.setInfo(info);// 更新新装备
        }
        else {
           GameObject itemGo = NGUITools.AddChild(parent, equipment);
            itemGo.transform.localPosition = Vector3.zero;
            itemGo.GetComponent<EquipmentItem>().setInfo(info);
        }
        UpdateProperty();
        return true;
    }

    //EquipmentItem方法调用，记录装备的属性
    public void takeOff(int id,GameObject go) {
        Inventory._instance.GetId(id);
        GameObject.Destroy(go);
        UpdateProperty();
    }

    //装备更新后更新人物属性
    void UpdateProperty() {
        this.attack = 0;
        this.def = 0;
        this.speed = 0;

       EquipmentItem headgerItem = headgear.GetComponentInChildren<EquipmentItem>();
        PlusProperty(headgerItem);
       EquipmentItem armorItem = armor.GetComponentInChildren<EquipmentItem>();
        PlusProperty(armorItem);
       EquipmentItem leftHandItem = left_Hand.GetComponentInChildren<EquipmentItem>();
        PlusProperty(leftHandItem);
       EquipmentItem rightHandItem = right_Hand.GetComponentInChildren<EquipmentItem>();
        PlusProperty(rightHandItem);
       EquipmentItem shoeItem = shoe.GetComponentInChildren<EquipmentItem>();
        PlusProperty(shoeItem);
       EquipmentItem haccessoryItem = accessory.GetComponentInChildren<EquipmentItem>();
        PlusProperty(haccessoryItem);

    }

    void PlusProperty(EquipmentItem item) {
        if (item != null)
        {
            ObjectOnInfo equipmentInfo = ObjectInfo._instance.GetObjctInfoById(item.id);
            this.attack += equipmentInfo.attack;
            this.def += equipmentInfo.def;
            this.speed += equipmentInfo.speed;
        }
    }
}
