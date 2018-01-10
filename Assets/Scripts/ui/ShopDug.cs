using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDug : MonoBehaviour {

    public static ShopDug _instance;
    private TweenPosition twee;
    private bool isShow = false;

    private GameObject numberDiaLog;
    private UIInput numberInput;
    private int buy_id;

    private PlayerStatus playerStatus;

   // private GameObject buy1001;

    private void Awake()
    {
        //buy1001 = this.transform.Find("DugItem1001/Buy_xphy").GetComponent<GameObject>();
      //  buy1001.SetActive(false);

        _instance = this;
        twee = this.GetComponent<TweenPosition>();

        numberDiaLog = this.transform.Find("NumberDialog").gameObject;
        numberInput = this.transform.Find("NumberDialog/NumberInput").GetComponent<UIInput>();
        numberDiaLog.SetActive(false);

        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
    }

    public void ShowDurg() {
        if (isShow == false)
        {
            twee.PlayForward();
            isShow = true;
        }
        else {
            twee.PlayReverse();
            isShow = false;
        }
    }

    public void ColseButtonClick() {
        ShowDurg();
    }

    public void OnBuyId1001() {
        Buy(1001);
    }
    public void OnBuyId1002()
    {
        Buy(1002);
    }
    public void OnBuyId1003()
    {
        Buy(1003);
    }

    void Buy(int id) {
        ShowNumberDialog();
        this.buy_id = id;
    }

    //显示计数器和把购买数量归0
    void ShowNumberDialog() {
        numberDiaLog.SetActive(true);
        numberInput.value = "0";
    }

    public void OnOKBuyButtonClick()
    {
        int count = int.Parse(numberInput.value);//购买个数
        ObjectOnInfo info = ObjectInfo._instance.GetObjctInfoById(buy_id);//购买价格
        int price = info.price_buy;
        int price_total = price * count;
        bool success = Inventory._instance.GetCoin(price_total);
        if (success)
        {
            if (count > 0) {
                Inventory._instance.GetId(buy_id,count);
            }
        }
        numberDiaLog.SetActive(false);
    }
}
