using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    
    private TweenPosition tween;
    private int coinCount = 5000;
    //private UILabel coinLabel;


    public static Inventory _instance;
    public List<InventoryItemGird> itemGirdList = new List<InventoryItemGird>();//背包中的所有空格
    private UILabel coinNumberLabel;
    public GameObject inventoryItem;


    private void Awake()
    {
        _instance = this;
        tween = this.GetComponent<TweenPosition>();
        coinNumberLabel = this.transform.Find("Coin_bg/Coin_num").GetComponent<UILabel>();
       // coinLabel = this.transform.Find("Coin_bg/Coin_num").GetComponent<UILabel>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) ){
            //GetId(Random.Range(1001,1004));
            GetId(Random.Range(2001, 2022));
        }
    }

    //拾取到ID的物品，并添加到背包,处理拾取的物体
    public void GetId(int id ,int count = 1) {
        
        InventoryItemGird gird = null;
        //1.在背包中查找是否存在该物品,根据物品的ID在背包中查找物品
        foreach (InventoryItemGird temp in itemGirdList) {
            if (temp.id == id) {
                gird = temp;
                break;
            }
        }
        if (gird != null){/*背包中存在该物品*/
            //2.如果存在该物品，num+1；
            gird.PlusNumber(count);

        }else {//背包中不存在该物品
            foreach (InventoryItemGird temp in itemGirdList){
                if (temp.id == 0){
                    gird = temp;
                    break;
                }
            }
            //3.如果不存在，查找背包空的方格，然后把InventoryItem放到这个方格下面。
            if (gird != null) {
                GameObject itemGo = NGUITools.AddChild(gird.gameObject,inventoryItem);
                itemGo.transform.localPosition = Vector3.zero;//添加的物品在格子的正中央。
                itemGo.GetComponent<UISprite>().depth = 8;//物体显示的优先级
                //更新格子的信息
                gird.SetId(id,count);
            }
        }
    }

    private bool isShow = false;

    void ShowBag() {
        isShow = true;
        this.gameObject.SetActive(true);
        tween.PlayForward();
    }

    void HideBag() {
        isShow = false;
        tween.PlayReverse();
    }

    public void TransformState() {//转化背包的的显示或者隐藏
        if (isShow == false)
        {
            ShowBag();
        }
        else if (isShow == true) {
            HideBag();
        }
    }

    //使用金币的方法
    public bool GetCoin(int count) {
        if (count <= coinCount){
            coinCount -= count;
            coinNumberLabel.text = coinCount.ToString();
            return true;
        }
        return false;
    }

}
