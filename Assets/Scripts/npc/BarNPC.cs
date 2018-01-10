using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarNPC : NPC {

    private PlayerStatus status;
    private bool isShow = false;
    public TweenPosition questTween;//任务对话框的显示隐藏
    public UILabel desLable;
    public bool isInTask = false;//接受任务标志位
    public int killCount = 0;//任务的进度，杀死了几只野狼
    //设置按钮的对象，对按钮的显示与隐藏进行编辑
                             //[Range(0,10)]
    public GameObject acceptBtnGo;
    public GameObject okBtnGo;
    public GameObject cancelBtnGo;
    

    private void Start()
    {
        status = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
    }

    //当鼠标位于这个collider上面的时候，会在每一帧调用这个方法，自动调用。
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) {
            
            if (isInTask){
                ShowTaskProgress();
            }else {
                ShowTaskDes();
            }
            if (isShow == false)
            {
                ShowQuest();
                isShow = true;
            }
            else {
                HideQuest();
                isShow = false;
            }
           
        }
    }

    void ShowQuest() {
        questTween.gameObject.SetActive(true);
        questTween.PlayForward();//向前播放动画
    }

    void HideQuest() {
        questTween.PlayReverse();//向后播放动画
    }

    //在未接受任务时的文本和按钮。
    void ShowTaskDes() {
        desLable.text = "Task：\nTKill 10 wolves.\nhortation:\n1000$";
        okBtnGo.SetActive(false);
        acceptBtnGo.SetActive(true);
        cancelBtnGo.SetActive(true);
    }
    //在任务中显示的文本和按钮。
    void ShowTaskProgress() {
        desLable.text = "The task schedule is:"+killCount+"/10只";
        okBtnGo.SetActive(true);
        acceptBtnGo.SetActive(false);
        cancelBtnGo.SetActive(false);
    }

    //任务系统点击叉号关闭任务面板
    public void OnCloseButtomClick() {
        HideQuest();
    }
    //接受任务
    public void OnAcceptButtonClick() {
        ShowTaskProgress();
        isInTask = true;//表示在任务中
    }
    //提交任务
    public void OnOkButtonClick() {
        if (killCount >= 10){
            status.GetCoin(1000);
            killCount = 0;
            ShowTaskDes();
        }else {
            HideQuest();
        }
    }

}
