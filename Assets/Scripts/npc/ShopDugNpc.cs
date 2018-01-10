using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDugNpc : NPC {

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) {

            ShopDug._instance.ShowDurg();
        }
       
    }
}
