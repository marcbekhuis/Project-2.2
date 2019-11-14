using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int points = 1;

    private LevelScore levelScore;

    private void Start()
    {
        levelScore = FindObjectOfType<LevelScore>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            levelScore.AddScore(points);
            Destroy(this.gameObject);
        }
    }
}
