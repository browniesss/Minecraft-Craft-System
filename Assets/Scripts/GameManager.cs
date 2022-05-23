using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Item List")]
    public List<Item> item_List;

    [Header("Toggle Key")]
    public bool toggle_Ctrl = false;

    void Start()
    {

    }

    // Update is called once per frame
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
