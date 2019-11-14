using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsUI : MonoBehaviour
{
    public GameObject levelUI;
    
    private void OnEnable()
    {
        transform.DestroyChildren();
        for (int i = 0; i < SaveManager.Instance.SaveData.levels.Count; i++)
        {
            var go = Instantiate(levelUI, transform);
        }
    }
}
