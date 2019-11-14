using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private GameObject healthIcon;
    [SerializeField] private Transform healthBar;
    [SerializeField] private OpenSceneScript openSceneScript;

    private List<GameObject> healthIcons = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < health; i++)
        {
            AddHealthIcon();
        }
    }

    public void AddHealth(int amount)
    {
        health += amount;
        for (int i = 0; i < amount; i++)
        {
            AddHealthIcon();
        }
    }

    public void RemoveHealth(int amount)
    {
        health -= amount;
        for (int i = 0; i < amount; i++)
        {
            RemoveHealthIcon();
            if (health <= 0)
            {
                Died();
            }
        }
    }

    private void AddHealthIcon()
    {
        healthIcons.Add(Instantiate(healthIcon, healthBar));
    }

    private void RemoveHealthIcon()
    {
        GameObject healthIconTemp = healthIcons[healthIcons.Count - 1];
        healthIcons.RemoveAt(healthIcons.Count - 1);
        Destroy(healthIconTemp);
    }

    private void Died()
    {
        Camera.main.transform.parent = null;
        Destroy(gameObject);
        openSceneScript.OpenLevel("MainMenu");
    }
}
