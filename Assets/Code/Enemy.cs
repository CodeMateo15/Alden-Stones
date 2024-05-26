using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public GameObject moveLight;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
            Instantiate(moveLight, transform.position, moveLight.transform.rotation);
            Destroy(this.gameObject);
        }
    }

    void Die()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().score += 1000f;
        GetComponent<Collider2D>().enabled = false;
        GameObject.FindGameObjectWithTag("EnemyTile").GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

}
