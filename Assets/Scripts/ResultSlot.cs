using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 결과 슬롯
public class ResultSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
{
    [Header("ResultSlot Info")]
    public Item item;
    public int item_Count;

    [SerializeField]
    private Image image;
    [SerializeField]
    private Text countText;

    public void Awake()
    {
        image = GetComponent<Image>();
    }

    public void AddItem(Item item, int itemcount)
    {
        this.item = item;
        this.item_Count = itemcount;

        // Image 설정 
        image.sprite = item.GetComponent<Image>().sprite;
        image.color = new Color(255, 255, 255, 1);

        countText.text = item_Count.ToString();
        countText.color = new Color(50, 50, 50, 1);
    }

    public void OnBeginDrag(PointerEventData eventData) // 드래그 시작시
    {
        if (item != null) // 해당 슬롯이 아이템이 있었다면 
        {
            // 드래그하는 아이템을 설정
            DragSlot.Instance.drag_result_Slot = this;
            DragSlot.Instance.Drag_Image_Set(item, image, item_Count);
            DragSlot.Instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData) // 드래그 중일때 
    {
        DragSlot.Instance.transform.position = eventData.position; // 드래그하는 아이템의 좌표를 마우스 좌표로 넣어줌.
    }

    public void OnDrop(PointerEventData eventData) // 이 슬롯에 Drop 이 일어났을때 
    {
        if (DragSlot.Instance.draging_item != null) // 드래그 중인 아이템이 있었다면
        {
           
        }
    }

    public void OnEndDrag(PointerEventData eventData) // 드래그가 끝날 시 
    {
        DragSlot.Instance.Drag_Image_End(); // 드래그 하던 슬롯에게 드래그가 끝났다는 함수를 호출함. 
    }

    public void Slot_Clear() // 해당 슬롯을 비워주는 함수.
    {
        this.item = null;
        this.item_Count = 0;

        this.image.sprite = null;
        this.image.color = new Color(255, 255, 255, 0);

        countText.color = new Color(50, 50, 50, 0);
    }

    void Update()
    {
        
    }
}
