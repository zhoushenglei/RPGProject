using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : UIDragDropItem {

    private UISprite sprite;
    private int id;
    private void Awake()
    {
        sprite = this.GetComponent<UISprite>();
    }
    private void Update()
    {
        if (isHover)
        {
           // print(id+"----");
            InventoryDes._instance.Show(id);
            if (Input.GetMouseButtonDown(1)) {
               bool success = EquipmentUI._instance.DressEquipment(id);
                if (success) {
                    transform.GetComponentInParent<InventoryItemGird>().MinusNumber();
                }
            }
        }
    }

    //拖拽物品结束时调用的方法
    protected override void OnDragDropRelease(GameObject surface){
        base.OnDragDropRelease(surface);
        if (surface != null){
            if (surface.tag == Tags.inventory_item_grid) {//当拖放到一个空的格子中的时候
                if (surface == this.transform.parent.gameObject) {//把物体拖到自己的格子中
                    ResetPosition();
                } else {
                    InventoryItemGird oldParent = this.transform.parent.GetComponent<InventoryItemGird>();
                   
                    this.transform.parent = surface.transform;
                    ResetPosition();
                    InventoryItemGird newParent = surface.GetComponent<InventoryItemGird>();
                    newParent.SetId(oldParent.id,oldParent.num);

                    oldParent.ClearInfo();
                }
            } else if (surface.tag == Tags.inventory_item) {//当拖放到一个有物体的格子中的时候
                InventoryItemGird grid1 = this.transform.parent.GetComponent<InventoryItemGird>();
                InventoryItemGird grid2 = surface.transform.parent.GetComponent<InventoryItemGird>();
                //InventoryItemGird grid3 = grid1;
                int id = grid1.id;int num = grid1.num;
                grid1.SetId(grid2.id,grid2.num);
                grid2.SetId(id, num);
                ResetPosition();
            } else{//当物体拖到格子以外的时候
                ResetPosition();
            }
        }else {
            ResetPosition();
        }
    }

    void ResetPosition() {
        transform.localPosition = Vector3.zero;
    }

    public void SetId(int id) {
        ObjectOnInfo info = ObjectInfo._instance.GetObjctInfoById(id);
        sprite.spriteName = info.icon_name;
    }

    public void SetIconName(int id,string icon_name) {
        sprite.spriteName = icon_name;
        this.id = id;
    }

    //鼠标在物体上时显示物品信息
    private bool isHover = false;

    public void OnHoverOver()
    {
        isHover = true;
    }
    public void OnHoverOut()
    {
        isHover = false;
    }
}
