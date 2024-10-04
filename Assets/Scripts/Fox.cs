using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Fox : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] Collider2D standingColider;
    [SerializeField] Transform headColiderCheck;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    float horizontalValue;
    [SerializeField] bool isGrounded;
    bool isRunning = false;
    bool isfacingRight = true;
    bool coyoteJump;
    //bool canDoubleJump = false;
    bool multipleJump;
    [SerializeField] bool isCrouch = false;

    public float speed = 1;
    float speedRunMofify = 2f;
    float speedCrouchModify = 0.5f;
    float radiusCollision = 0.2f;
    [SerializeField] int totalsJump;
    int avaiableJump;
    [SerializeField] float jumpPower = 5f;
    bool soundGround = true;

    AudioManager audioManager;

    private void Awake()
    {
        avaiableJump = totalsJump;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (CanMove() == false)
            return;

        horizontalValue = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetButtonDown("Crouch"))
        {
            isCrouch = true;
        }
        if (Input.GetButtonUp("Crouch"))
        {
            isCrouch = false;
        }

    }

    private void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValue, isCrouch);
        if (Enemy.isEnemyDeath || EnemyAi.isEnemyDeath)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            Enemy.isEnemyDeath = false;
            EnemyAi.isEnemyDeath = false;
        }
        if(FindObjectOfType<Death>().isDeath == true)
        {
            rb.velocity = new Vector2(rb.velocity.x * -2f, rb.velocity.y);
        }

    }

    bool CanMove()
    {
        bool can = true;
        if (FindObjectOfType<Death>().isDeath)
            can = false;
        return can;
    }

    void GroundCheck()
    {
        bool wasGrouned = isGrounded;
        isGrounded = false;
        Collider2D[] colider = Physics2D.OverlapCircleAll(groundCheck.position, radiusCollision, groundLayer);
        if (colider.Length > 0)
        {
            isGrounded = true;
            if (!wasGrouned)
            {
                avaiableJump = totalsJump;
                multipleJump = false;
            }
        }
        else
        {
            if (wasGrouned)
            {
                StartCoroutine(CoyoteJumpDelay());
            }
        }
        if(isGrounded == false)
        {
            soundGround = true;
        }
        if (isGrounded == true && soundGround == true)
        {
            audioManager.PlaySFX(audioManager.hitTheGround);
            soundGround = false;
        }
        // can replace code 
        // isGrounded = Physics2D.OverlapCircle(groundCheck.position, radiusCollision, groundLayer);
        animator.SetBool("IsJump", !isGrounded);
        //if(isGrounded)
        //{
        //    canDoubleJump = true;
        //}
    }

    IEnumerator CoyoteJumpDelay()
    {
        coyoteJump = true;
        yield return new WaitForSeconds(0.2f);
        coyoteJump = false;
    }

    void Jump()
    {
        if (isGrounded && isCrouch == false)
        {
            multipleJump = true;
            avaiableJump--;
            rb.velocity = Vector2.up * jumpPower;
            animator.SetBool("IsJump", true);

        }
        else
        {
            if (coyoteJump)
            {
                multipleJump = true;
                avaiableJump--;
                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("IsJump", true);
            }
            if (multipleJump && avaiableJump > 0)
            {
                avaiableJump--;
                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("IsJump", true);
            }
        }
        //if (canDoubleJump)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        //    canDoubleJump = false;
        //    animator.SetBool("IsJump", true);
        //}
    }

    void Move(float dir, bool crouchFlag)
    {
        #region crouch

        if(!crouchFlag)
        {
            if (Physics2D.OverlapCircle(headColiderCheck.position, radiusCollision, groundLayer))
            {
                crouchFlag = true;
            }
        }

        if(isGrounded)
        {
            animator.SetBool("IsCrouch", crouchFlag);
            standingColider.enabled = !crouchFlag;
        }



        #endregion

        #region Move & run

        float xVal = dir * speed * 100 * Time.fixedDeltaTime;
        if (isRunning)
        {
            xVal *= speedRunMofify;
        }
        if(crouchFlag && isGrounded)
        {
            xVal *= speedCrouchModify;
        }
        Vector2 targetVeloc = new Vector2(xVal, rb.velocity.y);
        rb.velocity = targetVeloc;
        Flip(dir);

        animator.SetFloat("xVeloctity", MathF.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
        #endregion

        
    }

    void Flip(float dir)
    {
        if(isfacingRight && dir < 0 || !isfacingRight && dir > 0)
        {
            isfacingRight = !isfacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }


}   
