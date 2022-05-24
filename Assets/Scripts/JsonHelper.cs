using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Json 을 배열로 저장하게 할 스크립트

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool print)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items = array;
        return JsonUtility.ToJson(wrapper, print);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] items; // Json 오브젝트를 감싸는 Wrapper 클래스를 따로 만들어서
        // Json을 배열로 받을 수 있게함
    }
}
