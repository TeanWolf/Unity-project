using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    public Rigidbody2D Rb;
    Animator anim;
    SpriteRenderer sr;

    [Header("For Music")]
    [SerializeField] private AudioSource Jumping;
    [SerializeField] private AudioSource Run;
    [SerializeField] private AudioSource Attacking;


    [Header("For CheckGround")]
    bool isGrounded;
    public float checkRadius;
    public LayerMask whatIsGround;
    public Transform groundCheck;


    [Header("For Attack")]
    public float startTimeBtwAttack;
    public Transform attackPose;
    public float attackRange;
    public LayerMask WhatIsEnemy;
    public float attackRangeX;
    public float attackRangeY;
    public HpBar healthBar;
    private int currentHpPlayer;
    public bool isRunning;
    public int damage;
    GameObject AttackHitBox;
    public float reboot;
    bool isAttacking = false;
    public bool isBlock =false;


    [Header("For Dash")]
    [SerializeField] float dashSpeed = 100f;
    [SerializeField] float dashTimer;
    [SerializeField] float dashTime = 0.5f;
    bool EndDash;
    bool movementControl;
    private bool lockLunge = false;


    [Header("For Jump")]
    [SerializeField]
    int speed;
    [SerializeField]
    int JumpForce;

    [Header("For Animation")]
    private PauseMenu pauseMenu;
    public GameObject otherGameObject;

    [Header("For System Death and Health")]
    public int healthPlayer;
    public int MaxhealthPlayer;
    public int tdd =0;
    public bool isDead = false;
    public GameController gameController;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthBar.SetMaxHealth(MaxhealthPlayer);
        healthPlayer = MaxhealthPlayer;
        AttackHitBox.SetActive(false);
        EndDash = false;
        isRunning = false;
        pauseMenu = otherGameObject.GetComponent<PauseMenu>();
    }

    private void AttackReset()
    {
        isAttacking = false;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;
            int choose = UnityEngine.Random.Range(1, 4);
            anim.Play("Attack" + choose);
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPose.position, attackRange, WhatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponentInParent<Enemy>().TakeDamage(damage);
            }
            Invoke("AttackReset", reboot);
            Attacking.Play();
        }
        if(Input.GetKey(KeyCode.Z) && !isAttacking)
        {
            isBlock = true;
            anim.Play("Block");
        }
        else
        {
            isBlock = false;
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            TakeDamage(20);
        }
        if(healthPlayer <=0)
        {
            Debug.Log("Death");
            Dead();
        }
        if(pauseMenu.GameIsPause!=false)
        {
            Debug.Log("Pause");
            GetComponent<Animator>().speed = 0;
        }
    }

public void Dead()
    {
        isDead = true;
        //gameController.LoseGame();
    }

    void TakeDamage(int damage)
    {
        healthPlayer -= damage;
        if (healthPlayer < currentHpPlayer)
        {
            anim.SetTrigger("HurtPlayer");
        }
        currentHpPlayer = healthPlayer;
        healthBar.SetHealth(healthPlayer);
    }
    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded == true && !Input.GetKey(KeyCode.Z))
        {
            Jumping.Play();
            Rb.velocity = new Vector2(Rb.velocity.x, JumpForce);
            if (isAttacking == false)
            {
                anim.Play("Jump");
            }
        }
    }
      private void FixedUpdate()
    {
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if(isRunning==true)
        {
            Run.Play();
        }
        else
        {
            Run.Stop();
        }
        if (movementControl == false)
        {
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.Z))
            {
                isRunning = true;
                Rb.velocity = new Vector2(speed, Rb.velocity.y);
                if (isGrounded && !isAttacking)
                {
                    anim.Play("Walk");
                }
                transform.localScale = new Vector3(1, 1, 1);
                isRunning = false;
            }
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.Z))
            {
                isRunning = true;
                Rb.velocity = new Vector2(-speed, Rb.velocity.y);
                if (isGrounded && !isAttacking)
                {
                    anim.Play("Walk");
                }
                transform.localScale = new Vector3(-1, 1, 1);
                isRunning = false;
            }
            /*else if ((Input.GetKeyUp(KeyCode.D) && !Input.GetKey(KeyCode.Z)) || Input.GetKeyUp(KeyCode.A) && !Input.GetKey(KeyCode.Z))
                {
                Run.Stop();
            }*/
            else
            {
                Rb.velocity = new Vector2(0, Rb.velocity.y);
                if (isGrounded)
                {
                    if (isAttacking == false)
                    {
                        anim.Play("Idle");
                    }
                }

            }
            Jump();
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.Z) && !lockLunge)
        {
            lockLunge = true;
            Invoke("LungeLock", 2f);
            if (isGrounded && !isAttacking)
            {
                anim.Play("Roll");
            }
            else if(isGrounded == false && !isAttacking)
            {
                anim.Play("Roll");
            }
            StartCoroutine(Timer());
            if (EndDash == false)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Rb.velocity = new Vector2(dashSpeed, Rb.velocity.y);
            }
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.Z) && !lockLunge)
        {
            lockLunge = true;
            Invoke("LungeLock", 2f);
            if (isGrounded && !isAttacking)
            {
                anim.Play("Roll");
            }
            else if (isGrounded == false && !isAttacking)
            {
                anim.Play("Roll");
            }
                StartCoroutine(Timer());
            if (EndDash == false)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Rb.velocity = new Vector2(-dashSpeed, Rb.velocity.y);
            }
        }
    }

    void LungeLock()
    {
        lockLunge = false;
    }

    IEnumerator Timer()
    {
        //anim.SetBool("Dash", true);
        EndDash = false;
        movementControl = true;
        yield return new WaitForSeconds(dashTime);
        //anim.SetTrigger("DashE");
        movementControl = false;
        EndDash = true;
        StartCoroutine(DashTimer_kd());
    }

    IEnumerator DashTimer_kd()
    {
        yield return new WaitForSeconds(dashTimer);
        EndDash = false;
    }

    void OnDrawGizmosSelected()
    {
        //for attack
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPose.position, attackRange);
    }
}