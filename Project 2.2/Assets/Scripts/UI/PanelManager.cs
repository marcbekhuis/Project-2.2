using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject StartingPanel;
    public List<GameObject> Panels = new List<GameObject>();

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
