using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 매니저 스크립트
public class GameManager : Singleton<GameManager>
{
    [Header("Item List")]
    public List<Item> item_List;

    [Header("Toggle Key")]
    public bool toggle_Ctrl = false; // 컨트롤 누르고 분할 시 사용할 bool 변수

    void Start()
    {

    }

    void Update()
    {
        Toggle_KeyDown(); 
    }

    void Toggle_KeyDown()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
            toggle_Ctrl = true;

        if(Input.GetKeyUp(KeyCode.LeftControl))
            toggle_Ctrl = false;
    }
}
