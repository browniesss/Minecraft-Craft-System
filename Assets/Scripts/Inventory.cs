using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �κ��丮 ��ũ��Ʈ
public class Inventory : Singleton<Inventory>
{
    [Header("Inventory Slots")]
    public Slot[] inventory_slots; // �κ��丮 ����
      
    void Start()
    {
        // �ʱ� ������ ����
        inventory_slots[0].AddItem(GameManager.Instance.item_List[0], 10);
        inventory_slots[1].AddItem(GameManager.Instance.item_List[8], 10);
        inventory_slots[2].AddItem(GameManager.Instance.item_List[6], 10);
        inventory_slots[3].AddItem(GameManager.Instance.item_List[9], 10);
        inventory_slots[4].AddItem(GameManager.Instance.item_List[10], 10);
        inventory_slots[5].AddItem(GameManager.Instance.item_List[11], 10);
        inventory_slots[6].AddItem(GameManager.Instance.item_List[12], 10);
    }

    void Update()
    {

    }
}
