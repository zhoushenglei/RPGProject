using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public float distance = 0;
    public float scrollSpeed = 10;
    public float rotateSpeed = 2;

    private Transform player;
    private Vector3 offSetPostion;//镜头位置偏移量
    private bool isRotating = false;//鼠标水平滑动


	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        transform.LookAt(player.position);
        offSetPostion = transform.position - player.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = offSetPostion + player.position;
        //处理视野的旋转
        RotateView();
        //处理视野的拉近拉远效果
        ScrollView();
       
	}

    void ScrollView() { 
        distance = offSetPostion.magnitude;
        distance += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        distance = Mathf.Clamp(distance, 2, 18);//限制镜头最近和最远距离
        offSetPostion = offSetPostion.normalized * distance;
    }

    void RotateView() {
        //Input.GetAxis("Mouse X");//得到鼠标在水平方向上的滑动
       // Input.GetAxis("Mouse Y");//得到鼠标在垂直方向上的滑动

        if (Input.GetMouseButtonDown(1)) {
            isRotating = true;
        }

        if (Input.GetMouseButtonUp(1)) {
            isRotating = false;
        }

        if (isRotating) {

            Vector3 originalPos = transform.position;
            Quaternion originalRotation = transform.rotation;

            transform.RotateAround(player.position,player.up,rotateSpeed * Input.GetAxis("Mouse X"));
            transform.RotateAround(player.position, transform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));
            float x = transform.eulerAngles.x;

            if (x>80 || x<10) {
                transform.position = originalPos;
                transform.rotation = originalRotation;
            }
        }
        offSetPostion = transform.position - player.position;
    }
}
