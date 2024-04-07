using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float movementSpeed;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = player.position - transform.position;
        int dirX = direction.x < 0 ? -1 : 1;
        if(direction.x == 0)
        {
            dirX = 0;
        }
        int dirY = direction.y < 0 ? -1 : 1;
        if (direction.y == 0)
        {
            dirY = 0;
        }
        animator.SetInteger("DirX", dirX);
        animator.SetInteger("DirY", dirY);

        direction.Normalize();
        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Gun") 
        {
            StartCoroutine(DestroyEnemy());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Gun")
        {
            StartCoroutine(DestroyEnemy());
        }
    }

    IEnumerator DestroyEnemy()
    {
        animator.SetTrigger("Dead");
        yield return new WaitForSeconds(0.12f);
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
