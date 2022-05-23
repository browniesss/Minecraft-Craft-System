using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Recipe
{
    public bool isblank;
    public string type;
    public string[] pattern;
    public List<Pattern_Key> key;
    public ResultItem result;
}

[Serializable]
public class Pattern_Key
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
public class Item_Info
{
    public string item_name;
    public string item_tag;
}

[Serializable]
public class ResultItem
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

    void Data_Save()
    {
        string toJson = JsonHelper.ToJson(recipe, true);

        string fileName = "Craft_Recipe";
        string path = Application.dataPath + "/Resources/" + fileName + ".Json";

        File.WriteAllText(path, toJson);
    }

    void Data_Load()
    {
        string _fileName = "Craft_Recipe";

        string path = Application.dataPath + "/Resources/" + _fileName + ".Json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            recipe = JsonHelper.FromJson<Recipe>(json);

            foreach (var item in recipe)
            {
                foreach (var n in GameManager.Instance.item_List)
                {
                    if (item.result.item_info == n.item_name ||
                        item.result.item_info == n.item_tag)
                        item.result.item = n;
                }
            }
        }
    }

    void OnApplicationQuit()
    {
        Data_Save();
    }
}