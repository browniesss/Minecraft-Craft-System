using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item_Type // ������ ����
{
    Overlap_Item = 1, // 1�� �̻� ���ĳ��� �� �ִ� ������  
    Not_Overlap_Item // 1���ۿ� �� ���ĳ��� ������
}

public class Item : MonoBehaviour
{
    public string item_name;
    public string item_tag;
    public Item_Type item_type;
}
