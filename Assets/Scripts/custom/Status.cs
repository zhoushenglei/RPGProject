using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {

    public static Status _instance;

    private TweenPosition tween;
    private bool isShow = false;

    private UILabel label_attack;
    private UILabel label_def;
    private UILabel label_speed;
    private UILabel label_pointRemain;
    private UILabel label_summary;

    private GameObject addButton_attackGo;
    private GameObject addButton_defGo;
    private GameObject addButton_speedGo;

    private PlayerStatus playerStatus;

    private void Awake()
    {
        _instance = this;
        tween = this.GetComponent<TweenPosition>();

        label_attack = transform.Find("attack").GetComponent<UILabel>();
        label_def = transform.Find("def").GetComponent<UILabel>();
        label_speed = transform.Find("speed").GetComponent<UILabel>();
        label_pointRemain = transform.Find("point_remain").GetComponent<UILabel>();
        label_summary = transform.Find("summary").GetComponent<UILabel>();

        addButton_attackGo = transform.Find("addButton_attack").gameObject;
        addButton_defGo = transform.Find("addButoon_def").gameObject;
        addButton_speedGo = transform.Find("addButton_speed").gameObject;

        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
    }

    public void ShowStatus() {
        if (isShow == false)
        {
            UpdateShow();
            tween.PlayForward();
            isShow = true;
        }
        else if(isShow){
            tween.PlayReverse();
            isShow = false;
        }
    }

    void UpdateShow()
    {//更新显示Status的信息，（根据PlayerStatus文件中的值）
        label_attack.text = playerStatus.attack + "+" + playerStatus.attack_plus;
        label_def.text = playerStatus.def + "+" + playerStatus.def_plus;
        label_speed.text = playerStatus.speed + "+" + playerStatus.speed_plus;
        label_pointRemain.text = playerStatus.point_remain.ToString();
        label_summary.text = "伤害:" + (playerStatus.attack + playerStatus.attack_plus)
            + "  "+"防御:"+(playerStatus.def+playerStatus.def_plus)
            + "  "+"速度:"+(playerStatus.speed+playerStatus.speed_plus);
        //print("----------"+playerStatus.point_remain);
        if (playerStatus.point_remain > 0)
        {
            addButton_attackGo.SetActive(true);
            addButton_defGo.SetActive(true);
            addButton_speedGo.SetActive(true);
        }
        else {
            addButton_attackGo.SetActive(false);
            addButton_defGo.SetActive(false);
            addButton_speedGo.SetActive(false);
        }
    }

    public void OnAddAttackClick() {
        bool success = playerStatus.GetPoint();
        if (success) {
            playerStatus.attack_plus++;
            UpdateShow();
        }
    }
    public void OnAddDefClick() {
        bool success = playerStatus.GetPoint();
        if (success)
        {
            playerStatus.def_plus++;
            UpdateShow();
        }
    }
    public void OnAddSpeedClick() {
        bool success = playerStatus.GetPoint();
        if (success)
        {
            playerStatus.speed_plus++;
            UpdateShow();
        }
    }

}
