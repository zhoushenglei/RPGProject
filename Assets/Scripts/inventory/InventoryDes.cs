using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDes : MonoBehaviour {
    public static InventoryDes _instance;
    private float time = 0;

    private UILabel label;
    // Use this for initialization
    void Start()
    {
        _instance = this;
        label = this.GetComponentInChildren<UILabel>();
        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        //当控件显示的时候，且鼠标不在触发在物品上的事件时，进行显示时间减短，然后隐藏
        if (this.gameObject.activeInHierarchy == true) {
            time -= Time.deltaTime;
            if (time <= 0) {
                this.gameObject.SetActive(false);
            }
        }
	}

    public void Show(int id) {
        this.gameObject.SetActive(true);
        time = 0.1f;
        //转化为世界坐标
        transform.position = UICamera.currentCamera.ScreenToWorldPoint(Input.mousePosition);

       // print("============================"+id);
        ObjectOnInfo info = ObjectInfo._instance.GetObjctInfoById(id);
        string des = "";
        switch (info.type) {
            case ObjectType.Drug:
                des = GetDrugDes(info);
                break;
            case ObjectType.Equip:
                des = GetEquipDes(info);
                break;
            default:
                break;
        }
        label.text = des;
    }

    string GetDrugDes(ObjectOnInfo info) {
        string str = "";
        str += "名称：" + info.name+"\n";
        str += "HP：" + info.hp + "\n";
        str += "MP：" + info.mp + "\n";
        str += "出售价：" + info.price_sell + "\n";
        str += "购买价：" + info.price_buy + "\n";
        //print("============================" + str);
        return str;
    }

    string GetEquipDes(ObjectOnInfo info) {
        string str = "";
        str += "名称：" + info.name + "\n";
        switch (info.dressType) {
            case DressType.Headgear:
                str += "穿戴部位：头盔" + "\n";
                break;
            case DressType.Accessory:
                str += "穿戴部位：饰品" + "\n";
                break;
            case DressType.Armor:
                str += "穿戴部位：法杖" + "\n";
                break;
            case DressType.Left_Hand:
                str += "穿戴部位：左手" + "\n";
                break;
            case DressType.Right_Hand:
                str += "穿戴部位：右手" + "\n";
                break;
            case DressType.Shoe:
                str += "穿戴部位：鞋子" + "\n";
                break;
            default:
                break;
        }

        switch (info.applicationType) {
            case ApplicationType.Swordman:
                str += "适用职业：战士" + "\n";
                break;
            case ApplicationType.Magician:
                str += "适用职业：法师" + "\n";
                break;
            case ApplicationType.Common:
                str += "适用职业：所有职业" + "\n";
                break;
            default:
                break;
        }

        str += "攻击值：" + info.attack + "\n";
        str += "防御值：" + info.def + "\n";
        str += "速度值：" + info.speed + "\n";
        str += "出售价：" + info.price_sell + "\n";
        str += "购买价：" + info.price_buy + "\n";
        return str;
    }
}
