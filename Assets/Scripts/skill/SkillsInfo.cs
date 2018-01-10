using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsInfo : MonoBehaviour {

    public static SkillsInfo _instance;
    public TextAsset skillsInfoText;

    //创建字典
    private Dictionary<int, SkillInfo> skillInfoDict = new Dictionary<int, SkillInfo>();

    private void Awake()
    {
        _instance = this;
        //初始化技能字典的信息
        InitSkillInfoDict();
    }

    //初始化技能字典的信息
    void InitSkillInfoDict() {

    }

}

//使用角色
public enum ApplicableRole {
    Swordman,
    Magician
}

//作用类型
public enum ApplyType {
    Passive,//增益HP、MP
    Buff,//增加伤害、防御、速度和攻击速度
    SingleTarget,//攻击单个目标
    MultiTarget//攻击多个目标
}
//作用属性
public enum ApplyProperty {
    Attack,
    Def,
    Speed,
    AttackSpeed,
    HP,
    MP
}

//技能释放类型
public enum ReleaseType {
    Self,//当前位置释放
    Enemy,//制定敌人释放
    Position//指定位置释放
}

public class SkillInfo {
    public int id;
    public string name;//技能名字
    public string icon_name;//技能图标
    public string des;//技能秒速
    public ApplyType applyType;
    public ApplyProperty applyProperty;
    public int applyValue;//技能作用值
    public int applyTime;//作用时间
    public int mp;//消耗
    public int coldTime;//冷却时间
    public ApplicableRole applicableRole;//角色
    public int level;//等级
    public ReleaseType releaseType;
    public float distance; //技能释放距离
}