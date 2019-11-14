using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol : MonoBehaviour
{
    public bool checkBottom = true;
    [SerializeField] private Vector2 checkOffset;
    public float diraction, moveSpeed;
    [SerializeField] private float distance;
    [SerializeField] private new Rigidbody2D rigidbody2D;

    private void Reset()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnDrawGizmosSelected()
    {
        diraction = Mathf.Sign(moveSpeed);
        Vector2 checkPosision = new Vector2(transform.position.x + checkOffset.x * diraction, transform.position.y + checkOffset.y);

        Gizmos.color = Color.red;
        
        Gizmos.DrawLine(Vector2.down * distance + checkPosision, checkPosision);
        Gizmos.DrawLine((Vector2.right * diraction) * distance + checkPosision, checkPosision);
    }

    private void FixedUpdate()
    {
        //checks bottem
        diraction = Mathf.Sign(moveSpeed);
        Vector2 checkPosision = new Vector2(transform.position.x + checkOffset.x * diraction, transform.position.y + checkOffset.y); 
        RaycastHit2D hitInfoDown = Physics2D.Raycast(checkPosision, Vector2.down, distance);
        RaycastHit2D hitInfoFront = Physics2D.Raycast(checkPosision, Vector2.right * diraction, distance);
        if ((hitInfoDown.collider == null && checkBottom)|| hitInfoFront.collider != null)
        {
            moveSpeed *= -1;
            transform.localScale = new Vector2(Mathf.Sign(moveSpeed), 1);
        }
        rigidbody2D.velocity = new Vector2(moveSpeed, rigidbody2D.velocity.y);
        
    }
}
