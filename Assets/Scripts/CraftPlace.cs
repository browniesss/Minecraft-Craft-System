using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftPlace : Singleton<CraftPlace>
{
    [Header("Craft Slots")]
    public List<Slot> craft_Slot;

    [Header("Result Slot")]
    public ResultSlot result_Slot;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Craft_Check(Item item)
    {
        List<Recipe> tempRecipe = new List<Recipe>();
        List<Result_Key> tempResult = new List<Result_Key>();
        List<bool> tempBool = new List<bool>();

        Dictionary<int, Dictionary<char, string>> temp_Dic =
            new Dictionary<int, Dictionary<char, string>>();

        int dic_Count = 0;
        foreach (Recipe recp in CraftRecipe.Instance.recipe) // 해당 아이템이 들어간 레시피만 추출
        {
            foreach (var item_key in recp.key)
            {
                if (item_key.item.item_name == item?.item_name ||
                    item_key.item.item_tag == item?.item_tag)
                {
                    tempRecipe.Add(recp);
                    break;
                }
            }
        }

        foreach (Recipe recp in tempRecipe)
        {
            string recpPattern = "";
            foreach (string c in recp.pattern)
            {
                recpPattern += c;
                recpPattern += '\n';
            }

            List<string> tempCheck = new List<string>();
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
            foreach (var c in craft_Slot)
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

            Result_Key temp = new Result_Key();
            temp.key = recpPattern;
            temp.item = recp.result;

            tempResult.Add(temp);

            Dictionary<char, string> newDic = new Dictionary<char, string>();
            foreach (Pattern_Key key in recp.key)
            {
                if (key.item.item_name == "")
                    newDic.Add(key.key, key.item.item_tag);
                else
                    newDic.Add(key.key, key.item.item_name);
            }
            tempBool.Add(recp.isblank);
            temp_Dic.Add(dic_Count++, newDic);
        }

        for (int i = 0; i < temp_Dic.Count; i++)
        {
            string craft_str = "";
            string[] str = { "", "", "" };

            for (int k = 0; k < craft_Slot.Count; k++)
            {
                if (!tempBool[i])
                {
                    if (craft_Slot[k].item != null)
                    {
                        char key = '~';
                        Dictionary<char, string> newDic = new Dictionary<char, string>();

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
                }
                else
                {
                    if (craft_Slot[k].item != null)
                    {
                        char key = '~';
                        Dictionary<char, string> newDic = new Dictionary<char, string>();

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
                    else
                    {
                        switch (k / 3)
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

            foreach (var res in tempResult)
            {
                Debug.Log("집" + craft_str + "<< craft_str" + res.key + "<< res_key");
                if (res.key == craft_str)
                {
                    Debug.Log("완성템");
                    result_Slot.AddItem(res.item.item, res.item.count);
                    return;
                }
            }
        }

        result_Slot.Slot_Clear();
        Debug.Log("zz");
    }
}
