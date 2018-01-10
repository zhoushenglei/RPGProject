using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroType {
    Swordman,
    Magician
}

public class PlayerStatus : MonoBehaviour {

    public HeroType heroType;

    public int grade = 1;
    public int hp = 100;
    public int mp = 100;
    public int coin = 200;//金币数量

    public int attack = 20;//攻击力
    public int attack_plus = 0;
    public int def = 20;//防御力
    public int def_plus = 0;
    public int speed = 20;//速度
    public int speed_plus = 0;
    public int point_remain = 0;//升级待分配的点数


    public void GetCoin(int count) {
        coin += count;
    }

    public bool GetPoint(int point = 1) {
        if (point_remain >= point)
        {
            point_remain -= point;
            return true;
        }
        else {
            return false;
        }
    }
}
