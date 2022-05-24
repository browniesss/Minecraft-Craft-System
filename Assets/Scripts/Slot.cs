using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// ���� ��ũ��Ʈ
public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
{
    [Header("Slot Item Info")]
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

    public void Item_CountAdd(int amount)
    {
        this.item_Count += amount;

        countText.text = item_Count.ToString();
    }

    public void Item_CountMinus(int amount)
    {
        this.item_Count -= amount;

        countText.text = item_Count.ToString();
    }

    public void Update_Info()
    {
        this.countText.text = item_Count.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData) // �巡�� ���۽�
    {
        if (item != null) // �ش� ������ �������� �־��ٸ� 
        {
            // �巡���ϴ� �������� ����
            DragSlot.Instance.drag_Slot = this;
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
            if (this.item != null) // ���� �ش� ���Կ� �������� �־��ٸ�
            {
                if (this.item == DragSlot.Instance.draging_item && this != DragSlot.Instance.drag_Slot &&
                    this.item.item_type != Item_Type.Not_Overlap_Item) // ���� �������̰� �巡�� ���� ������ �ƴϿ���
                    // ��ĥ �� �ִ� �������̶��
                {
                    this.Item_CountAdd(DragSlot.Instance.item_Count); // �������� ������.
                    DragSlot.Instance.drag_Slot?.Slot_Clear();
                    DragSlot.Instance.drag_result_Slot?.Slot_Clear();
                }
                else // �װ� �ƴ϶��
                {
                    // ���� ü����

                    Item temp_item = this.item;
                    int temp_Count = this.item_Count;

                    AddItem(DragSlot.Instance.draging_item, DragSlot.Instance.item_Count);
                    DragSlot.Instance.drag_Slot.AddItem(temp_item, temp_Count);
                }
            }
            else // �ش� ���Կ� �������� �����ٸ�
            {
                if (!GameManager.Instance.toggle_Ctrl || DragSlot.Instance.drag_Slot == null) // ��Ʈ�� Ű�� �������� �ʾҰų�
                    // �巡�� ���� ������ �����ٸ�
                {
                    // ������ �߰�
                    AddItem(DragSlot.Instance.draging_item, DragSlot.Instance.item_Count);
                    DragSlot.Instance.drag_Slot?.Slot_Clear();
                    DragSlot.Instance.drag_result_Slot?.Slot_Clear();
                }
                else // ��Ʈ�� Ű�� �����־��ٸ� 
                {
                    if (DragSlot.Instance.item_Count >= 2) // ������ ����
                    {
                        AddItem(DragSlot.Instance.draging_item, DragSlot.Instance.item_Count / 2);
                        DragSlot.Instance.drag_Slot.Item_CountMinus(DragSlot.Instance.item_Count / 2);
                        DragSlot.Instance.drag_result_Slot?.Slot_Clear();
                    }
                }
            }

            if (CraftPlace.Instance.craft_Slot.Contains(this)) // ���� ���մ� ���Կ� �÷ȴٸ� 
            {
                CraftPlace.Instance.Craft_Check(this.item); // ������ �˻� ���� �Լ� ȣ��
            }

            if (!CraftPlace.Instance.craft_Slot.Contains(this) &&
                CraftPlace.Instance.craft_Slot.Contains(DragSlot.Instance.drag_Slot)) // ���մ뿡�� �������� ���������
                // ���� ���մ��� �������� �ִ��� �˻� �� �ٽ� ������ �˻� ����
            {
                Item temp_ite = null;
                foreach (var item in CraftPlace.Instance.craft_Slot)
                {
                    if (item.item != null)
                    {
                        temp_ite = item.item;
                        break;
                    }
                }
                CraftPlace.Instance.Craft_Check(temp_ite);
            }

            if (DragSlot.Instance.drag_result_Slot != null &&
                !CraftPlace.Instance.craft_Slot.Contains(this)) // ��� ���Կ��� ������ �巡���ؼ� �ű� �� 
                // ���մ��� �����۵� ���� ����
            {
                foreach (var slot in CraftPlace.Instance.craft_Slot)
                {
                    if (slot.item != null)
                    {
                        CraftPlace.Instance.Craft_Check(slot.item);
                        slot.item_Count--;
                        slot.Update_Info();
                        if (slot.item_Count == 0)
                        {
                            slot.Slot_Clear();
                            continue;
                        }
                    }
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData) // �巡�װ� ���� �� 
    {
        DragSlot.Instance.Drag_Image_End(); // �巡�� �ϴ� ��ų���� �巡�װ� �����ٴ� �Լ��� ȣ����. 
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
