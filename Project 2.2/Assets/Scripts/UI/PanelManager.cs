using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public List<GameObject> Panels = new List<GameObject>();
    public GameObject StartingPanel;


    private void Awake()
    {
        SetPanel(StartingPanel);
    }

    public void SetPanel(GameObject panel)
    {
        SetPanel(panel.name);
    }

    public void SetPanel(string panel)
    {
        foreach (var p in Panels)
        {
            p.SetActive(p.name.Equals(panel));
        }
    }
}
