using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed4 = 2.0f;
    //[SerializeField] private float bounciness4 = 100;
    [SerializeField] private float knockbackForce4 = 200f;
    [SerializeField] private float upwardForce4 = 100f;
    [SerializeField] private int damageGiven4 = 1;
    [SerializeField] private float health ;
    [SerializeField] private float maxHealth = 5f ;
    [SerializeField] private GameObject player;
    private float cooldown = 0.5f;
    private float lastAttack;

    private SpriteRenderer rend4;
    private Animator anim4;
    private Animator playAnim4;


    private bool canMove4 = true;
    private bool inRange4;

    private void Start()
    {
        rend4 = GetComponent<SpriteRenderer>();
        anim4 = GetComponent<Animator>();
        health = maxHealth;
        playAnim4 = player.GetComponent<Animator>();

    }




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange4 == true)
        {
            TakeDamage(1);
        }

        if(health <= 0)
        {
            Death();
        }
    }



    void FixedUpdate()
    {
        if (!canMove4)
            return;
        transform.Translate(new Vector2(moveSpeed4, 0) * Time.deltaTime);

        if (moveSpeed4 < 0)
        {
            rend4.flipX = true;  //
        }

        if (moveSpeed4 > 0)
        {
            rend4.flipX = false;  //
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBlock"))
        {
            moveSpeed4 = -moveSpeed4;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            moveSpeed4 = -moveSpeed4;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().TakeDamage(damageGiven4);


            if (other.transform.position.x > transform.position.x)
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockBack(knockbackForce4, upwardForce4);
            }
            else
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockBack(-knockbackForce4, upwardForce4);
            }
        }
    }



    private void Death()
    {
        anim4.SetTrigger("Death");

        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        // GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        Destroy(gameObject, 0.8f);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            anim4.SetTrigger("Attack");
            inRange4 = true;
            //TakeDamage(1);
        }

    
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange4 = false;
        }

    }



    private void TakeDamage(float damage)
    {
        if(Time.time - lastAttack < cooldown)
        {
            return;
        }
        anim4.SetTrigger("Hit");
        lastAttack = Time.time;
        health -= damage;
    }

}