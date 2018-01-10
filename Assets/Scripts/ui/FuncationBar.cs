using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncationBar : MonoBehaviour {
   

    public void OnStatusButtonClick() {
        Status._instance.ShowStatus();
    }
    public void OnBagButtonClick()
    {
        Inventory._instance.TransformState();
    }
    public void OnSkillButtonClick()
    {
        SkillUI._instance.ShowSkillUI();
    }
    public void OnEquipButtonClick()
    {
        EquipmentUI._instance.ShowEquipment();
    }
    public void OnSettingButtonClick()
    {

    }
}
