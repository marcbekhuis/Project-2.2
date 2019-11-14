using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                object reference = GameObject.FindObjectOfType<T>();
                if (reference == null)
                    reference = new GameObject("singleton::" + typeof(T).ToString()).AddComponent(typeof(T));
                _instance = (T)reference;
            }

            return _instance;
        }
    }

    public static T GetInstanceIfNotNull
    {
        get
        {
            return _instance;
        }
    }

    public static bool IsInstanceNull
    {
        get
        {
            return (_instance == null);
        }
    }

    private void OnDestroy()
    {
        _instance = null;
    }
}