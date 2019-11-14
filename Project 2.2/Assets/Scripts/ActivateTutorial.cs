using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTutorial : MonoBehaviour
{
    public GameObject TutorialPanel;

    public PanelManager PanelManager;
    // Start is called before the first frame update
    void Start()
    {
        if (SaveManager.Instance.SaveData.EnableTutorial)
        {
            PanelManager.SetPanel(TutorialPanel);
        }
    }
    
}
