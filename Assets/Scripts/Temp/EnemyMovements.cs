using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;
   // [SerializeField] private float bounciness = 100;
    [SerializeField] private float knockbackForce = 200f;
    [SerializeField] private float upwardForce = 100f;
    [SerializeField] private GameObject player;
    [SerializeField] private int damageGiven = 1;


    private SpriteRenderer rend;
    private Animator anim;
    private Animator playAnim;

    private bool canMove = true;

    private bool inRange;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playAnim = player.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange == true)
        {

            TakeDamage();
            playAnim.SetTrigger("Attack");
        }
    }

    void FixedUpdate()
    {
        if (!canMove)
            return;
        transform.Translate(new Vector2(moveSpeed, 0) * Time.deltaTime);

        if (moveSpeed < 0)
        {
            rend.flipX = true;  //
        }

        if (moveSpeed > 0)
        {
            rend.flipX = false;  //
        }

      
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBlock"))
        {
            moveSpeed = -moveSpeed;  
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            moveSpeed = -moveSpeed;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().TakeDamage(damageGiven);


            if (other.transform.position.x > transform.position.x)
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockBack(knockbackForce, upwardForce);
            }
            else
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockBack(-knockbackForce, upwardForce);
            }
        }
    }



    private void TakeDamage()
    {
        anim.SetTrigger("Hit");

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
           
            inRange = true;
            Debug.Log("inRange");
            
            canMove = false;

            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
