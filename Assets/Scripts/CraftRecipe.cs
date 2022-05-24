using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Recipe // 레시피
{
    public bool isblank;
    public string type;
    public string[] pattern;
    public List<Pattern_Key> key;
    public ResultItem result;
}

[Serializable]
public class Pattern_Key // 레시피 조합법에 들어갈 아이템의 key와 아이템 정보
{
    public char key;
    public Item_Info item;
}

[Serializable]
public class Result_Key 
{
    public string key;
    public ResultItem item;
}

[Serializable]
public class Item_Info // 아이템 정보
{
    public string item_name;
    public string item_tag;
}

[Serializable]
public class ResultItem // 결과 아이템의 정보
{
    public Item item;
    public string item_info;
    public int count;
}

public class CraftRecipe : Singleton<CraftRecipe>
{
    public Recipe[] recipe;

    void Start()
    {
        Data_Load();

        Data_Save();
    }

    void Data_Save() // json 데이터 SAVE
    {
        string toJson = JsonHelper.ToJson(recipe, true); // jsonHelper 라는 스크립트를 이용해 배열을 json 형태로 변환

        string fileName = "Craft_Recipe";
        string path = Application.dataPath + "/Resources/" + fileName + ".Json";

        File.WriteAllText(path, toJson); 
    }

    void Data_Load()  // json 데이터 Load
    {
        string _fileName = "Craft_Recipe";

        string path = Application.dataPath + "/Resources/" + _fileName + ".Json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            recipe = JsonHelper.FromJson<Recipe>(json); // jsonHelper라는 스크립트를 이용해 배열을 json 형태로 받아옴

            foreach (var item in recipe)
            {
                foreach (var n in GameManager.Instance.item_List)  // 아이템을 넣어줌.
                {
                    if (item.result.item_info == n.item_name ||
                        item.result.item_info == n.item_tag)
                        item.result.item = n;
                }
            }
        }
    }

    void OnApplicationQuit() // 종료 시 저장
    {
        Data_Save();
    }
}