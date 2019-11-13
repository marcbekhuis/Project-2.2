using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeed : Singleton<PlayerSpeed>
{
    public PlayerSate playerSate = PlayerSate.Standing;
    public float runningSpeed = 1f;
    public float walkingSpeed = 0.5f;
    
    public Transform player;

    public enum PlayerSate
    {
        Running,
        Standing,
        Walking
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
