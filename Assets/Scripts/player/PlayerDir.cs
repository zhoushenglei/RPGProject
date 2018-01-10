using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDir : MonoBehaviour {

    public GameObject effect_click_prefabs; //鼠标点击的效果
    public Vector3 targetPosition = Vector3.zero;//人物位置
    private bool isMoving = false;//表示鼠标的是否按下
    private PlayerMove playerMove;

    private void Start()
    {
        //初始化人物当前所在位置
        targetPosition = transform.position;
        playerMove = this.GetComponent<PlayerMove>();
    }


    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0) && UICamera.hoveredObject == null) {
            //做射线检测
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//把鼠标点击的点转换为射线
            RaycastHit hitInfo;
            bool isCollider = Physics.Raycast(ray, out hitInfo);
            if (isCollider && hitInfo.collider.tag == Tags.ground) {
                isMoving = true;
                //实例化点击效果
                ShowClickEffect(hitInfo.point);
                //人物朝向点击方向
                LookAtarget(hitInfo.point);
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            isMoving = false;
        }

        if (isMoving)
        {
            //得到人物的移动的目标位置
            //让人物的朝向移动位置
            //做射线检测
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//把鼠标点击的点转换为射线
            RaycastHit hitInfo;
            bool isCollider = Physics.Raycast(ray, out hitInfo);
            if (isCollider && hitInfo.collider.tag == Tags.ground)
            {
                LookAtarget(hitInfo.point);
            }
        }else {
            if (playerMove.isMoving) {
                LookAtarget(targetPosition);
            }
        }
	}

    //实例化点击的效果
    public void ShowClickEffect(Vector3 hitPoint) {
        hitPoint = new Vector3(hitPoint.x,hitPoint.y + 0.1f,hitPoint.z);
        GameObject.Instantiate(effect_click_prefabs, hitPoint, Quaternion.identity);
    }
    //让人物朝向目标位置
    void LookAtarget(Vector3 hitPoint) {
        targetPosition = hitPoint;
        targetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        this.transform.LookAt(targetPosition);
    }
}
