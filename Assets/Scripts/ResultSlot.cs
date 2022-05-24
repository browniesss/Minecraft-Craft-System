using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// ��� ����
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

        // Image ���� 
        image.sprite = item.GetComponent<Image>().sprite;
        image.color = new Color(255, 255, 255, 1);

        countText.text = item_Count.ToString();
        countText.color = new Color(50, 50, 50, 1);
    }

    public void OnBeginDrag(PointerEventData eventData) // �巡�� ���۽�
    {
        if (item != null) // �ش� ������ �������� �־��ٸ� 
        {
            // �巡���ϴ� �������� ����
            DragSlot.Instance.drag_result_Slot = this;
            DragSlot.Instance.Drag_Image_Set(item, image, item_Count);
            DragSlot.Instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData) // �巡�� ���϶� 
    {
        DragSlot.Instance.transform.position = eventData.position; // �巡���ϴ� �������� ��ǥ�� ���콺 ��ǥ�� �־���.
    }

    public void OnDrop(PointerEventData eventData) // �� ���Կ� Drop �� �Ͼ���� 
    {
        if (DragSlot.Instance.draging_item != null) // �巡�� ���� �������� �־��ٸ�
        {
           
        }
    }

    public void OnEndDrag(PointerEventData eventData) // �巡�װ� ���� �� 
    {
        DragSlot.Instance.Drag_Image_End(); // �巡�� �ϴ� ���Կ��� �巡�װ� �����ٴ� �Լ��� ȣ����. 
    }

    public void Slot_Clear() // �ش� ������ ����ִ� �Լ�.
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
