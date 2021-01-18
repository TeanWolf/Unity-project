using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Public Variables;
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector]public Transform target;
    [HideInInspector]public bool inRange;
    public GameObject hotZone;
    public GameObject triggerArea;
    #endregion

    #region Private Variables
    public Animator Anim;
    private float distance;
    private bool attackMode;
    private bool cooling;
    private float intTimer;
    private PlayerControler playerControler;
    public int healthPlayer;
    public int damagePlayer;
    public GameObject HitBox;
    public GameObject HurtBox;
    public GameObject otherGameObject;
    public bool IsDead = false;
    public bool IsDeadTrue = false;
    public GameObject EnemyCollider;
    [SerializeField] public int tdd;
    private Collider2D hitBox;
    private BoxCollider2D[] hurtBox;
    private IEnumerator btwAttack;

    #endregion
    [SerializeField]
    public int health;
    private int DamageEnemy;
    private Rigidbody2D rb;

    void Start()
    {
        //btwAttack = BtwAttack();
    }

    void Awake()
    {
        playerControler = otherGameObject.GetComponent<PlayerControler>();
        SelectTarget();
        intTimer = timer;
        Anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (IsDeadTrue == false)
        {
            health -= damage;
            Anim.SetTrigger("Hurt");
        }
    }

    void Update()
    {
        if(!attackMode)
        {
            Move();
        }

        if(!InsideofLimits() && !inRange && !Anim.GetCurrentAnimatorStateInfo(0).IsName("LightBandit_Attack"))
        {
            SelectTarget();
        }

        if(inRange)
        {
            EnemyLogic();
        }
       if (health <=0)
        {
            Anim.SetBool("isDead", true);
            IsDead = true;
            IsDeadTrue = true;
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            this.enabled = false;
            Destroy(gameObject, 1.25f);
            IsDead = false;
            IsDeadTrue = true;
        }
    }

    void EnemyLogic()
    {
        if (IsDead == false)
        {
            distance = Vector2.Distance(transform.position, target.position);

            if (distance > attackDistance)
            {
                StopAttack();
            }
            else if (attackDistance >= distance && cooling == false)
            {
                HitBox.SetActive(true);
                Attack();
            }

            if (cooling)
            {
                Cooldown();
                Anim.SetBool("Attack", false);
            }
        }
    }

     void Move()
    {
        HitBox.SetActive(false);
        Anim.SetBool("canWalk", true);

        if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("LightBandit_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D HitBox)
    {
        if (HitBox.gameObject.tag == "HurtBox")
        {
            Damage();
        }
    }

    void Damage()
    {
        if(playerControler.isBlock==false)
        {
        Debug.Log("Trigger");
        tdd++;
        playerControler.healthPlayer -= damagePlayer;
        Anim.SetTrigger("HurtPlayer");
            }
    }

    void Attack()
    {
        timer = intTimer;
        attackMode = true;
        Anim.SetBool("canWalk", false);
        Anim.SetBool("Attack", true);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if(timer <=0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        Anim.SetBool("Attack", false);
    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideofLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if(distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation =transform.eulerAngles;
        if(transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }
}
    
