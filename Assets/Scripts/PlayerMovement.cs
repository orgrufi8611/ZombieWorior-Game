using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] TMP_Text killsIndicator;
    [SerializeField] TMP_Text livesIndicator;
    
    [SerializeField] Transform gunPos;
    [SerializeField] GameObject gunPrefub;
    [SerializeField] GameObject chainsawPrefub;
    [SerializeField] List<GameObject> chainsaws = new List<GameObject>();
    
    
    [SerializeField] float movementSpeed;
    [SerializeField] float chainsawSpeed;
    [SerializeField] int lives;
    [SerializeField] MapScript map;
    GameObject gun = null;
    
    int kills;
    float time;
    
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        kills = 0;
        NewChainSaw();
    }

    // Update is called once per frame
    void Update()
    {
        killsIndicator.text = "Killed " + kills;
        livesIndicator.text = lives.ToString();
        time += Time.deltaTime;
        float x = Mathf.Cos(chainsawSpeed * time);
        float y = Mathf.Sin(chainsawSpeed * time);
        foreach(var chainsaw in  chainsaws)
            chainsaw.transform.localPosition = new Vector2(2 * x, 2 * y);

        float dirX = Input.GetAxis("Horizontal");
        float dirY = Input.GetAxis("Vertical");
        transform.Translate(dirX * movementSpeed * Time.deltaTime, dirY * movementSpeed * Time.deltaTime, 0);
        int rotation = dirX < 0 ? -1 : 1;
        if (gun != null)
        {
            gun.transform.localScale = new Vector2(rotation,1);
        }
        gunPos.localPosition = new Vector3(gunPos.localPosition.x * rotation,gunPos.localPosition.y,gunPos.localPosition.z);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetMouseButtonDown(0) && gun != null)
        {
            gun.GetComponent<GunScript>().Shoot((mousePos - (Vector2)transform.position).normalized);
        }

    }

    

    public void NewChainSaw()
    {
        int index;
        GameObject chainsaw = Instantiate(chainsawPrefub,transform.position,transform.rotation);
        chainsaw.transform.SetParent(transform);
        chainsaw.GetComponent<ChainsawScript>().player = this;
        if(chainsaws.Count > 0)
        {
            index = chainsaws.FindIndex(null);
            chainsaws[index] = chainsaw;
        }
        else
        {
            chainsaws.Add(chainsaw);
            index = 0;
        }
        chainsaw.GetComponent<ChainsawScript>().index = index;
    }

    public void RemoveChaisaw(int index)
    {
        chainsaws[index] = null;
    }

    public void AddKill()
    {
        kills++;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            lives--;
            if (lives == 0)
            {
                map.GameOver(kills);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Gun")
        {
            if(gun == null)
            {
                gun = Instantiate(gunPrefub, gunPos);
                gun.transform.SetParent(transform);
                map.RemoveGun();
            }
        }
    }
}
