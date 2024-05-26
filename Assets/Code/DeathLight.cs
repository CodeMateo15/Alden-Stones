using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLight : MonoBehaviour
{
    Vector3 player;
    private bool go = false;

    public float lightSpeed = 3f;

    Vector3 localScale;
    Vector3 endPos;
    Vector3 startPos;
    private bool movingRight = true;

    private HealthBar healthBar;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform.position;
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
        startPos = this.transform.position;
    }

    void FixedUpdate()
    {
        player = GameObject.FindWithTag("Player").transform.position;

        if (go == false)
        {
            endPos = startPos + new Vector3(0, 1f);
            float step = lightSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, endPos, step);

            if (Vector2.Distance(transform.position, endPos) < 0.01f)
            {
                go = true;
            }
        }

        if (go == true)
        {
            float step = lightSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, player, step);

            if (transform.position.x > player.x)
            {
                movingRight = false;
            }
            if (transform.position.x < player.x)
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

            if (Vector2.Distance(transform.position, player) < 0.01f)
            {
                Health.currentHealth += 5;
                healthBar.SetHealth(Health.currentHealth);
                Destroy(this.gameObject);
            }
        }
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
}