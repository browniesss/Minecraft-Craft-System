using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �巡���ϴ� ������ ��ũ��Ʈ
public class DragSlot : Singleton<DragSlot>
{
    [Header("Draging Item Info")] // �巡�� ���� �������� ����
    public Slot drag_Slot;
    public ResultSlot drag_result_Slot; // �巡�� ���� ��� ����
    public Item draging_item;
    public int item_Count;

    [Header("own Info")]
    public Image own_Image;

    void Start()
    {

    }

    public void Drag_Image_Set(Item item, Image image, int count)  // �巡�� �ϴ� �̹��� ����
    {
        draging_item = item;
        item_Count = count;

        own_Image.sprite = image.sprite;

        own_Image.color = new Color(255, 255, 255, 1f);
    }

    public void Drag_Image_End() // �巡�� ���� �� ȣ��� �Լ�
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
