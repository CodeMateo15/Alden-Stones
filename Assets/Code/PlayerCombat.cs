using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public ParticleSystem spark;

    public int attackDamage = 40;
    public float attackRange = 0.5f;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    private CameraShake shake;

    public AudioClip[] otherClip;
    public AudioSource randomSound;

    public bool wizard;

    void Start()
    {
        shake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
    }

    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    void Attack()
    {
        animator.SetTrigger("Attack");
        PlaySound();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        if (wizard == true)
        {
            CreateSpark();
        }
        foreach(Collider2D enemy in hitEnemies)
        {
            shake.CamShake();
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            CreateSpark();
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void CreateSpark()
    {
        spark.Play();
    }

    void PlaySound()
    {
        randomSound.clip = otherClip[Random.Range(0, otherClip.Length)];
        randomSound.Play();
    }
}
