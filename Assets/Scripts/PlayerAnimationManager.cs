using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    Animator animator;
    int dirX;
    int dirY;
    int walkDirX;
    int walkDirY;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        dirX = mousePos.x < 0 ? -1:1;
        animator.SetInteger("IdleDirX", dirX);
        
        dirY = mousePos.y < 0 ? -1 : 1;
        animator.SetInteger("IdleDirY", dirY);
        
        float inputX= Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        
        if (inputX < 0)
        {
            walkDirX = -1;
        }
        else if (inputX > 0)
        {
            walkDirX = 1;
        }
        else
        {
            walkDirX = 0;
        }
        
        animator.SetInteger("RunDirX", walkDirX);
        
        if(inputY < 0)
        {
            walkDirY = -1;
        }
        else if(inputY > 0)
        {
            walkDirY = 1;
        }
        else
        {
            walkDirY = 0;
        }
        animator.SetInteger("RunDirY", walkDirY);
    }
}
