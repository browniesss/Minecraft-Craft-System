using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 드래그하는 슬롯의 스크립트
public class DragSlot : Singleton<DragSlot>
{
    [Header("Draging Item Info")] // 드래그 중인 아이템의 정보
    public Slot drag_Slot;
    public ResultSlot drag_result_Slot; // 드래그 중인 결과 슬롯
    public Item draging_item;
    public int item_Count;

    [Header("own Info")]
    public Image own_Image;

    void Start()
    {

    }

    public void Drag_Image_Set(Item item, Image image, int count)  // 드래그 하는 이미지 세팅
    {
        draging_item = item;
        item_Count = count;

        own_Image.sprite = image.sprite;

        own_Image.color = new Color(255, 255, 255, 1f);
    }

    public void Drag_Image_End() // 드래그 끝날 때 호출될 함수
    {
        drag_Slot = null;
        drag_result_Slot = null;
        draging_item = null;
        item_Count = 0;

        own_Image.sprite = null;
        own_Image.color = new Color(255, 255, 255, 0f);
    }

    void Update()
    {

    }
}
