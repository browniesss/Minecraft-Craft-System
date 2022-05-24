using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 제작 하는 슬롯 스크립트
public class CraftPlace : Singleton<CraftPlace>
{
    [Header("Craft Slots")]
    public List<Slot> craft_Slot; // 제작 슬롯들

    [Header("Result Slot")]
    public ResultSlot result_Slot; // 결과 슬롯

    void Start()
    {

    }

    void Update()
    {

    }

    // 조합법 체크 함수 
    public void Craft_Check(Item item)
    {
        List<Recipe> tempRecipe = new List<Recipe>(); // 계산 횟수를 줄이기 위해
        // 현재 조합대에 올라와있는 조합법들만 담을 레시피 리스트

        List<Result_Key> tempResult = new List<Result_Key>(); // 뽑아낸 조합법의 문자열로 만들어낸 패턴과 결과 아이템을
        // 담을 리스트

        List<bool> tempBool = new List<bool>(); // 조합이 빈 공간이 들어가는지 판별할 리스트

        Dictionary<int, Dictionary<char, string>> temp_Dic = // 만들어진 조합법의 index와 key와 아이템 name or tag
            new Dictionary<int, Dictionary<char, string>>(); // 를 저장할 Dictionary 

        int dic_Count = 0;
        foreach (Recipe recp in CraftRecipe.Instance.recipe) // 해당 아이템이 들어간 레시피만 추출
        {
            foreach (var item_key in recp.key)
            {
                if (item_key.item.item_name == item?.item_name || // 키 값 검사후 레시피 추출
                    item_key.item.item_tag == item?.item_tag)
                {
                    tempRecipe.Add(recp);
                    break;
                }
            }
        }

        foreach (Recipe recp in tempRecipe) // 추출해낸 레시피들을 탐색
        {
            string recpPattern = "";
            foreach (string c in recp.pattern) // 레시피들의 문자로 만들어진 키 값들을 받아와서 문자열로 
            { // 레시피 패턴을 만들어냄
                recpPattern += c;
                recpPattern += '\n';
            }

            List<string> tempCheck = new List<string>(); // 결과 값에 아이템의 tag 혹은 name 을 넣어줌.
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
            foreach (var c in craft_Slot) // 위 리스트에 담아놓은 아이템의 이름과 태그에 일치하지 않는
                                          // 조합법들의 결과를
                                          // 걸러냄.
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

            Result_Key temp = new Result_Key(); // 걸러내지지 않은 결과값을 리스트에 저장.
            temp.key = recpPattern;
            temp.item = recp.result;

            tempResult.Add(temp);

            Dictionary<char, string> newDic = new Dictionary<char, string>(); // 아이템의 key와 이름 or 태그를 저장할
                                                                              // Dictionary 생성
            foreach (Pattern_Key key in recp.key) // 저장 
            {
                if (key.item.item_name == "")
                    newDic.Add(key.key, key.item.item_tag);
                else
                    newDic.Add(key.key, key.item.item_name);
            }
            tempBool.Add(recp.isblank);
            temp_Dic.Add(dic_Count++, newDic);
        }

        for (int i = 0; i < temp_Dic.Count; i++) // 위에서 걸러서 만들어진 Dictionary를 검사
        {
            string craft_str = "";
            string[] str = { "", "", "" };

            for (int k = 0; k < craft_Slot.Count; k++) // 조합대에 올라온 아이템들을 검사.
            {
                if (!tempBool[i]) // 빈칸이 포함되지 않은 레시피라면
                {
                    if (craft_Slot[k].item != null) // 아이템이 있다면
                    {
                        char key = '~';

                        // key 값을 해당 아이템의 tag 혹은 name으로 딕셔너리의 value 값을 검사해 key값을 받아옴
                        key = temp_Dic[i].FirstOrDefault(x => x.Value == craft_Slot[k].item.item_name ||
                        x.Value == craft_Slot[k].item.item_tag).Key;

                        if (key == '~') // 받아온 값이 없다면 break;
                            break;

                        switch (k / 3) // 슬롯의 열을 검사해서 각 열에 맞는 문자열 배열에 넣어줌.
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
                else // 만약 빈칸이 포함된 레시피라면
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
                    else // 아이템이 없다면 
                    {
                        switch (k / 3) // 각 열에 맞는 문자열 배열에 공백 추가
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

            // 문자열들을 검사해 최종 문자열에 더해주고
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

            // 결과 패턴과 아이템이 담긴 리스트를 검사해 결과 패턴과 만들어진 결과를 검사
            foreach (var res in tempResult)
            {
                if (res.key == craft_str) // 같다면 
                {
                    result_Slot.AddItem(res.item.item, res.item.count); // 결과 슬롯에 아이템 추가
                    return;
                }
            }
        }

        result_Slot.Slot_Clear();
    }
}
