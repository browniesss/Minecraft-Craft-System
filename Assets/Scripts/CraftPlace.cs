using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ���� �ϴ� ���� ��ũ��Ʈ
public class CraftPlace : Singleton<CraftPlace>
{
    [Header("Craft Slots")]
    public List<Slot> craft_Slot; // ���� ���Ե�

    [Header("Result Slot")]
    public ResultSlot result_Slot; // ��� ����

    void Start()
    {

    }

    void Update()
    {

    }

    // ���չ� üũ �Լ� 
    public void Craft_Check(Item item)
    {
        List<Recipe> tempRecipe = new List<Recipe>(); // ��� Ƚ���� ���̱� ����
        // ���� ���մ뿡 �ö���ִ� ���չ��鸸 ���� ������ ����Ʈ

        List<Result_Key> tempResult = new List<Result_Key>(); // �̾Ƴ� ���չ��� ���ڿ��� ���� ���ϰ� ��� ��������
        // ���� ����Ʈ

        List<bool> tempBool = new List<bool>(); // ������ �� ������ ������ �Ǻ��� ����Ʈ

        Dictionary<int, Dictionary<char, string>> temp_Dic = // ������� ���չ��� index�� key�� ������ name or tag
            new Dictionary<int, Dictionary<char, string>>(); // �� ������ Dictionary 

        int dic_Count = 0;
        foreach (Recipe recp in CraftRecipe.Instance.recipe) // �ش� �������� �� �����Ǹ� ����
        {
            foreach (var item_key in recp.key)
            {
                if (item_key.item.item_name == item?.item_name || // Ű �� �˻��� ������ ����
                    item_key.item.item_tag == item?.item_tag)
                {
                    tempRecipe.Add(recp);
                    break;
                }
            }
        }

        foreach (Recipe recp in tempRecipe) // �����س� �����ǵ��� Ž��
        {
            string recpPattern = "";
            foreach (string c in recp.pattern) // �����ǵ��� ���ڷ� ������� Ű ������ �޾ƿͼ� ���ڿ��� 
            { // ������ ������ ����
                recpPattern += c;
                recpPattern += '\n';
            }

            List<string> tempCheck = new List<string>(); // ��� ���� �������� tag Ȥ�� name �� �־���.
            foreach (var c in recp.key)
            {
                if (c.item.item_name != "")
                {
                    tempCheck.Add(c.item.item_name);
                }
                else
                {
                    tempCheck.Add(c.item.item_tag);
                }
            }
            bool check_bool = false;
            foreach (var c in craft_Slot) // �� ����Ʈ�� ��Ƴ��� �������� �̸��� �±׿� ��ġ���� �ʴ�
                                          // ���չ����� �����
                                          // �ɷ���.
            {
                if (c.item != null)
                {
                    if (!tempCheck.Contains(c.item?.item_name) && !tempCheck.Contains(c.item?.item_tag))
                    {
                        check_bool = true;
                    }
                }
            }

            if (check_bool)
                continue;

            Result_Key temp = new Result_Key(); // �ɷ������� ���� ������� ����Ʈ�� ����.
            temp.key = recpPattern;
            temp.item = recp.result;

            tempResult.Add(temp);

            Dictionary<char, string> newDic = new Dictionary<char, string>(); // �������� key�� �̸� or �±׸� ������
                                                                              // Dictionary ����
            foreach (Pattern_Key key in recp.key) // ���� 
            {
                if (key.item.item_name == "")
                    newDic.Add(key.key, key.item.item_tag);
                else
                    newDic.Add(key.key, key.item.item_name);
            }
            tempBool.Add(recp.isblank);
            temp_Dic.Add(dic_Count++, newDic);
        }

        for (int i = 0; i < temp_Dic.Count; i++) // ������ �ɷ��� ������� Dictionary�� �˻�
        {
            string craft_str = "";
            string[] str = { "", "", "" };

            for (int k = 0; k < craft_Slot.Count; k++) // ���մ뿡 �ö�� �����۵��� �˻�.
            {
                if (!tempBool[i]) // ��ĭ�� ���Ե��� ���� �����Ƕ��
                {
                    if (craft_Slot[k].item != null) // �������� �ִٸ�
                    {
                        char key = '~';

                        // key ���� �ش� �������� tag Ȥ�� name���� ��ųʸ��� value ���� �˻��� key���� �޾ƿ�
                        key = temp_Dic[i].FirstOrDefault(x => x.Value == craft_Slot[k].item.item_name ||
                        x.Value == craft_Slot[k].item.item_tag).Key;

                        if (key == '~') // �޾ƿ� ���� ���ٸ� break;
                            break;

                        switch (k / 3) // ������ ���� �˻��ؼ� �� ���� �´� ���ڿ� �迭�� �־���.
                        {
                            case 0:
                                str[0] += key;
                                break;
                            case 1:
                                str[1] += key;
                                break;
                            case 2:
                                str[2] += key;
                                break;
                        }
                    }
                }
                else // ���� ��ĭ�� ���Ե� �����Ƕ��
                {
                    if (craft_Slot[k].item != null)
                    {
                        char key = '~';

                        key = temp_Dic[i].FirstOrDefault(x => x.Value == craft_Slot[k].item.item_name ||
                        x.Value == craft_Slot[k].item.item_tag).Key;

                        if (key == '~')
                            break;

                        switch (k / 3)
                        {
                            case 0:
                                str[0] += key;
                                break;
                            case 1:
                                str[1] += key;
                                break;
                            case 2:
                                str[2] += key;
                                break;
                        }
                    }
                    else // �������� ���ٸ� 
                    {
                        switch (k / 3) // �� ���� �´� ���ڿ� �迭�� ���� �߰�
                        {
                            case 0:
                                str[0] += ' ';
                                break;
                            case 1:
                                str[1] += ' ';
                                break;
                            case 2:
                                str[2] += ' ';
                                break;
                        }
                    }
                }
            }

            // ���ڿ����� �˻��� ���� ���ڿ��� �����ְ�
            if (str[0] != "")
            {
                craft_str += str[0];
                craft_str += '\n';
            }
            if (str[1] != "")
            {
                craft_str += str[1];
                craft_str += '\n';
            }
            if (str[1] == "" && str[0] != "" && str[2] != "")
            {
                craft_str += '\n';
            }
            if (str[2] != "")
            {
                craft_str += str[2];
                craft_str += '\n';
            }

            // ��� ���ϰ� �������� ��� ����Ʈ�� �˻��� ��� ���ϰ� ������� ����� �˻�
            foreach (var res in tempResult)
            {
                if (res.key == craft_str) // ���ٸ� 
                {
                    result_Slot.AddItem(res.item.item, res.item.count); // ��� ���Կ� ������ �߰�
                    return;
                }
            }
        }

        result_Slot.Slot_Clear();
    }
}
