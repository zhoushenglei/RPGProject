using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemGird : MonoBehaviour {

    public int id;
    private ObjectOnInfo info = null;//存储物品信息
    public int num = 0;
    public UILabel numLabel;

	// Use this for initialization
	void Start () {
        numLabel = this.GetComponentInChildren<UILabel>();
	}


    //调用方法更新背包单元格内容的显示
    public void SetId(int id,int num = 1) {
        this.id = id;
        info = ObjectInfo._instance.GetObjctInfoById(id);//通过ID得到这个物品
        InventoryItem item = this.GetComponentInChildren<InventoryItem>();
        item.SetIconName(id,info.icon_name);
        //更新显示
        //numLabel.gameObject.SetActive(true);
        numLabel.enabled = true;
        this.num = num;
        numLabel.text = num.ToString();
    }

    public void PlusNumber(int num = 1) {
        this.num += num;
        numLabel.text = this.num.ToString();
    }

    //穿戴装备时减去装备数量
    public bool MinusNumber(int num = 1) {
        if (this.num >= num)
        {
            this.num -= num;
            numLabel.text = this.num.ToString();
            if (this.num == 0) {
                ClearInfo();//清空物品的存储信息
                GameObject.Destroy(this.GetComponentInChildren<InventoryItem>().gameObject);//销毁物品的Item
            }
            return true;
        }
            return false;
    }

    //把物体拖离一个空格到另一个空格，清空这个空格的内容。
    public void ClearInfo() {
        id = 0;
        num = 0;
        info = null;
        //numLabel.gameObject.SetActive(false);
        numLabel.enabled = false;
    }
}
