using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float x;
    private float y;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    Animator animator;

    public float enemySpeed;
    private float startSpeed;

    public bool move;
    public bool patrol;
    private bool movingRight = true;

    private float waitTime;
    public float startWaitTime;

    public float attackRange = 5f;
    public LayerMask playerLayers;

    private Rigidbody2D rBody;

    Vector3 endPos;
    Vector3 startPos;
    Vector3 localScale;
    Vector3 target;

    void Start()
    {
        localScale = transform.localScale;
        animator = GetComponent<Animator>();
        startSpeed = enemySpeed;
        rBody = GetComponent<Rigidbody2D>();

        target = GameObject.FindWithTag("Player").transform.position;

        waitTime = startWaitTime;
        startPos = this.transform.position;
        move = true;
    }

    void FixedUpdate()
    {
        float step = enemySpeed * Time.deltaTime;
        target = GameObject.FindWithTag("Player").transform.position;

        Vector2 pos = Vector2.MoveTowards(transform.position, endPos, step);
        rBody.MovePosition(pos);

       if (transform.position.x > endPos.x)
       {
         movingRight = false;
       }
       if (transform.position.x < endPos.x)
       {
         movingRight = true;
       }
       if (movingRight == true)
       {
         moveRight();
       }
       if (movingRight == false)
       {
         moveLeft();
       }

       if (move == true)
       {
         Patrol();
       }

       if (Vector2.Distance(transform.position, endPos) < 0.2f)
       {
         if (waitTime <= 0)
         {
           startWaitTime = Random.Range(1, 4);
           waitTime = startWaitTime;
           move = true;
          }
         else
         {
           waitTime -= Time.deltaTime;
           animator.SetBool("Patrol", false);
         }
       }

        Collider2D[] attackPlayer = Physics2D.OverlapCircleAll(this.transform.position, attackRange, playerLayers);

        foreach (Collider2D player in attackPlayer)
        {
            Vector2 posPlayer = Vector2.MoveTowards(transform.position, target, step);
            rBody.MovePosition(posPlayer);

            if (transform.position.x > target.x)
            {
                movingRight = false;
            }
            if (transform.position.x < target.x)
            {
                movingRight = true;
            }
            if (movingRight == true)
            {
                moveRight();
            }
            if (movingRight == false)
            {
                moveLeft();
            }

            animator.SetBool("Patrol", true);

            if (Vector2.Distance(transform.position, target) < 0.2f)
            {
                enemySpeed = 0f;
            }
            else
            {
                enemySpeed = startSpeed;
            }
        }
    }

    void Patrol()
    {
        x = Random.Range(minX, maxX);
        y = Random.Range(minY, maxY);
        animator.SetBool("Patrol", true);
        endPos = startPos + new Vector3(x, y);
        move = false;
    }

    private void moveRight()
    {
        movingRight = true;
        localScale.x = 1;
        transform.localScale = localScale;
    }

    private void moveLeft()
    {
        movingRight = false;
        localScale.x = -1;
        transform.localScale = localScale;
    }

    void OnDrawGizmosSelected()
    {
        if (this == null)
            return;

        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }
}