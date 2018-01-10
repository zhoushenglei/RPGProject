using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : MonoBehaviour {

    public static EquipmentItem _instance;
    public int id;

    private UISprite sprite;
    //鼠标在移到物体上方
    private bool isHover = false;

    private void Awake()
    {
        _instance = this;
        sprite = this.GetComponent<UISprite>();
    }

    private void Update()
    {
        if (isHover) {//当鼠标在这个物品栏之上的时候。
            if (Input.GetMouseButtonDown(1)) {//右键点击，卸载装备
                EquipmentUI._instance.takeOff(id,this.gameObject);
            }
        }
    }

    public void setId(int id) {
        this.id = id;
        ObjectOnInfo info = ObjectInfo._instance.GetObjctInfoById(id);
        setInfo(info);
    }

    public void setInfo(ObjectOnInfo info) {
        this.id = info.id;
        sprite.spriteName = info.icon_name;
    }

    public void OnHover(bool isOver) {
       // print("鼠标在装备上了");
        isHover = isOver;
    }
}
