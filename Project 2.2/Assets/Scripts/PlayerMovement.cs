using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(VoiceCommandEvent))]
public class PlayerMovement : Singleton<PlayerMovement>
{
    public PlayerState playerState = PlayerState.Standing;

    [SerializeField] private float runningSpeed = 1f;
    [SerializeField] private float walkingSpeed = 0.5f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private LayerMask layerMask;

    private Rigidbody2D rigidbody2D;

    public enum PlayerState
    {
        Running,
        Standing,
        Walking,
        Jumping,
        Falling
    }

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapBox((Vector2)this.transform.position + new Vector2(0, -0.505f), new Vector2(0.2f, 0.02f), 0, layerMask) && (playerState == PlayerState.Jumping || playerState == PlayerState.Falling))
        {
            if (rigidbody2D.velocity.x == walkingSpeed)
            {
                playerState = PlayerState.Walking;
            }
            else if (rigidbody2D.velocity.x == runningSpeed)
            {
                playerState = PlayerState.Running;
            }
            else
            {
                playerState = PlayerState.Standing;
            }
        }
        else if (rigidbody2D.velocity.y < 0)
        {
            playerState = PlayerState.Falling;
        }
        Move(walkingSpeed);
    }

    private void Move(float speed)
    {
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    }

    public void Jump()
    {
        if (playerState != PlayerState.Jumping && playerState != PlayerState.Falling)
        {
            rigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            playerState = PlayerState.Jumping;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube((Vector2)this.transform.position + new Vector2(0, -0.505f), new Vector2(0.2f, 0.02f));
    }
}
