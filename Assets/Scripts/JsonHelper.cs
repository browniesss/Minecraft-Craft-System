using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Json �� �迭�� �����ϰ� �� ��ũ��Ʈ

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
        public T[] items; // Json ������Ʈ�� ���δ� Wrapper Ŭ������ ���� ����
        // Json�� �迭�� ���� �� �ְ���
    }
}
