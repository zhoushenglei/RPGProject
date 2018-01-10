using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : MonoBehaviour {

    public static SkillUI _instance;
    private bool isShow = false;
    private TweenPosition twee;

    private void Awake()
    {
        _instance = this;
        twee = this.transform.GetComponent<TweenPosition>();
    }

    public void ShowSkillUI() {
        if (isShow == false)
        {
            twee.PlayForward();
            isShow = true;
        }
        else if (isShow) {
            twee.PlayReverse();
            isShow = false;
        }
      
    }

}
