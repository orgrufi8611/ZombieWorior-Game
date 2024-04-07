using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefub;
    [SerializeField] float shotSpeed;
    Animator animator;
    int shots = 8;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void Shoot(Vector2 direction)
    {
        animator.SetTrigger("Shoot");
        StartCoroutine(CreateShot(direction));
    }

    IEnumerator CreateShot(Vector2 direction)
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(bulletPrefub, transform).GetComponent<Rigidbody2D>().velocity = direction * shotSpeed;
        shots--;
        if (shots == 0)
        {
            Destroy(gameObject);
        }
    }
}
