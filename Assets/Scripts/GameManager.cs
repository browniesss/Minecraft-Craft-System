using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �Ŵ��� ��ũ��Ʈ
public class GameManager : Singleton<GameManager>
{
    [Header("Item List")]
    public List<Item> item_List;

    [Header("Toggle Key")]
    public bool toggle_Ctrl = false; // ��Ʈ�� ������ ���� �� ����� bool ����

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
