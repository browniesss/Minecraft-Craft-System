using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item_Type // 아이템 종류
{
    Overlap_Item = 1, // 1개 이상씩 겹쳐놓을 수 있는 아이템  
    Not_Overlap_Item // 1개밖에 못 겹쳐놓는 아이템
}

public class Item : MonoBehaviour
{
    public string item_name;
    public string item_tag;
    public Item_Type item_type;
}
