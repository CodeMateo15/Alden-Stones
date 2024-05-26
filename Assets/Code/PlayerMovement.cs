using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 characterScale;
    float characterScaleX;

    public float moveSpeed = 5f;
    float horizontalMove = 0f;
    float verticalMove = 0f;

    public Rigidbody2D rb;
    public Animator animator;
    public ParticleSystem dust;

    Vector2 movement;
    private bool facingRight = true;
    private bool facingUp = true;
    public float horizontal;
    public float vertical;

    void Start()
    {
        characterScale = transform.localScale;
        characterScaleX = characterScale.x;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * moveSpeed;

        if (Input.GetAxis("Horizontal") < 0)
        {
            characterScale.x = -characterScaleX;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = characterScaleX;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            animator.SetFloat("Speed", Mathf.Abs(verticalMove));
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            animator.SetFloat("Speed", Mathf.Abs(verticalMove));
        }
        transform.localScale = characterScale;

        if((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
            FlipRight();
        }
        if ((vertical > 0 && !facingUp) || (vertical < 0 && facingUp))
        {
            FlipUp();
        }

    }

    void FlipRight()
    {
        facingRight = !facingRight;
        CreateDust();
    }

    void FlipUp()
    {
        facingUp = !facingUp;
        CreateDust();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void CreateDust()
    {
        dust.Play();
    }

}
