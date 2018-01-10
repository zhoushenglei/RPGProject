using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    private PlayerMove move;
	// Use this for initialization
	void Start () {
       
        move = this.GetComponent<PlayerMove>();
	}

    private void LateUpdate(){
       
        if (move.state == PlayerState.Moving) {
            PlayAnim("Walk");
        } else if (move.state == PlayerState.Idle) {
            PlayAnim("Idle");
        }
    }

    void PlayAnim(string animName) {
       this.GetComponent<Animation>().CrossFade(animName);

    }
}
