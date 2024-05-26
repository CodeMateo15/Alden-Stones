using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static int maxHealth = 100;
    public int damage = 5;
    public static int currentHealth;
    public ParticleSystem blood;

    private PauseGame over;

    private HealthBar healthBar;

    private CameraShake shake;

    public float score;
    public float beforeScore;

    void Start()
    {
        over = GameObject.FindGameObjectWithTag("GameOver").GetComponent<PauseGame>();
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        shake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();

        score = PlayerPrefs.GetFloat("CurrentScore");
    }

    void Update()
    {
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            shake.CamShake();
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            StartCoroutine("PlayerFlash");
        }
    }

    public IEnumerator PlayerFlash()

    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        CreateBlood();
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        CreateBlood();
    }

    void CreateBlood()
    {
        blood.Play();
    }

    void Dead()
    {
        beforeScore = PlayerPrefs.GetFloat("Score");
        if (score >= beforeScore)
        {
            PlayerPrefs.SetFloat("Score", score);
        }
        over.gameOver = true;
        Destroy(this.gameObject);
        UnityEngine.Debug.Log("You dead");
    }
}
