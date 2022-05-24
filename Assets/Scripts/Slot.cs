using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 슬롯 스크립트
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

        // Image 설정 
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

    public void OnBeginDrag(PointerEventData eventData) // 드래그 시작시
    {
        if (item != null) // 해당 슬롯이 아이템이 있었다면 
        {
            // 드래그하는 아이템을 설정
            DragSlot.Instance.drag_Slot = this;
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
            if (this.item != null) // 만약 해당 슬롯에 아이템이 있었다면
            {
                if (this.item == DragSlot.Instance.draging_item && this != DragSlot.Instance.drag_Slot &&
                    this.item.item_type != Item_Type.Not_Overlap_Item) // 같은 아이템이고 드래그 중인 슬롯이 아니였고
                    // 겹칠 수 있는 아이템이라면
                {
                    this.Item_CountAdd(DragSlot.Instance.item_Count); // 아이템을 겹쳐줌.
                    DragSlot.Instance.drag_Slot?.Slot_Clear();
                    DragSlot.Instance.drag_result_Slot?.Slot_Clear();
                }
                else // 그게 아니라면
                {
                    // 슬롯 체인지

                    Item temp_item = this.item;
                    int temp_Count = this.item_Count;

                    AddItem(DragSlot.Instance.draging_item, DragSlot.Instance.item_Count);
                    DragSlot.Instance.drag_Slot.AddItem(temp_item, temp_Count);
                }
            }
            else // 해당 슬롯에 아이템이 없었다면
            {
                if (!GameManager.Instance.toggle_Ctrl || DragSlot.Instance.drag_Slot == null) // 컨트롤 키가 눌려있지 않았거나
                    // 드래그 중인 슬롯이 없었다면
                {
                    // 아이템 추가
                    AddItem(DragSlot.Instance.draging_item, DragSlot.Instance.item_Count);
                    DragSlot.Instance.drag_Slot?.Slot_Clear();
                    DragSlot.Instance.drag_result_Slot?.Slot_Clear();
                }
                else // 컨트롤 키가 눌려있었다면 
                {
                    if (DragSlot.Instance.item_Count >= 2) // 아이템 분할
                    {
                        AddItem(DragSlot.Instance.draging_item, DragSlot.Instance.item_Count / 2);
                        DragSlot.Instance.drag_Slot.Item_CountMinus(DragSlot.Instance.item_Count / 2);
                        DragSlot.Instance.drag_result_Slot?.Slot_Clear();
                    }
                }
            }

            if (CraftPlace.Instance.craft_Slot.Contains(this)) // 만약 조합대 슬롯에 올렸다면 
            {
                CraftPlace.Instance.Craft_Check(this.item); // 레시피 검사 조합 함수 호출
            }

            if (!CraftPlace.Instance.craft_Slot.Contains(this) &&
                CraftPlace.Instance.craft_Slot.Contains(DragSlot.Instance.drag_Slot)) // 조합대에서 아이템을 내렸을경우
                // 남은 조합대의 아이템이 있는지 검사 후 다시 레시피 검사 실행
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
                !CraftPlace.Instance.craft_Slot.Contains(this)) // 결과 슬롯에서 아이템 드래그해서 옮길 시 
                // 조합대의 아이템들 개수 감소
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

    public void OnEndDrag(PointerEventData eventData) // 드래그가 끝날 시 
    {
        DragSlot.Instance.Drag_Image_End(); // 드래그 하던 스킬에게 드래그가 끝났다는 함수를 호출함. 
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
