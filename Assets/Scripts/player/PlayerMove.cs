using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    Moving,
    Idle
}

public class PlayerMove : MonoBehaviour {
    public float speed = 3;
    public PlayerState state = PlayerState.Idle;
    public bool isMoving = false;
    private PlayerDir dir;
    private CharacterController controller;
   

	// Use this for initialization
	void Start () {
        dir = this.GetComponent<PlayerDir>();
        controller = this.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        //Distance:返回两者之间的距离。
        float distance = Vector3.Distance(dir.targetPosition,transform.position);
        if (distance > 0.05f)
        {
            controller.SimpleMove(transform.forward * speed);
            //Debug.Log("=============================");
            state = PlayerState.Moving;
            isMoving = true;
        }
        else {
            state = PlayerState.Idle;
            isMoving = false;
        }
	}
}
