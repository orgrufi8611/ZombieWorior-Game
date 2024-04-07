using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainsawScript : MonoBehaviour
{
    public PlayerMovement player;
    public int index;
    int kills;
    // Start is called before the first frame update
    void Start()
    {
        kills = 0;   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            kills++;
            player.AddKill();
            if(kills == 3)
            {
                player.NewChainSaw();
            }
            else if(kills == 5) 
            {
                player.RemoveChaisaw(index);
                Destroy(gameObject);
            }
        }
    }
}
