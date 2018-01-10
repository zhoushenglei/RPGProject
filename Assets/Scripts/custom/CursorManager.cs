using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour {

    public static CursorManager _instance;

    public Texture2D cursor_normal;//正常指针
    public Texture2D cursor_npc_talk;//NPC交谈
    public Texture2D cursor_attack;//攻击
    public Texture2D cursor_lockTarget;//锁定目标
    public Texture2D cursor_pick;//拾取物品

    private Vector2 hotspot = Vector2.zero;
    private CursorMode mode = CursorMode.Auto;

    private void Start()
    {
        _instance = this;
    }

    public void SetNormal() {
        Cursor.SetCursor(cursor_normal,hotspot,mode);
    }

    public void SetNpcTalk() {
        Cursor.SetCursor(cursor_npc_talk, hotspot, mode);
    }
}
