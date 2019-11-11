using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance => _instance ?? (_instance = MonoBehaviour.FindObjectOfType<T>());

    private void OnDestroy()
    {
        _instance = null;
    }

}