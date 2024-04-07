using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapScript : MonoBehaviour
{

    [SerializeField] GameObject gameOverScreen;
    [SerializeField] TMP_Text lvlIndicator;
    [SerializeField] TMP_Text gameOverScore;
    [SerializeField] TMP_Text gameOverLvl;


    [SerializeField] GameObject gunPrefub;
    [SerializeField] GameObject enemyPrefub;
    [SerializeField] float enemySpeed;
    [SerializeField] Transform player;
    [SerializeField] float minRange;
    [SerializeField] float maxRange;
    [SerializeField] Vector2Int bottoLeft, mapSize;
    RectInt border
    {
        get
        {
            return new RectInt(bottoLeft, mapSize);
        }
    }

    [SerializeField] float lvlTime;
    
    int gunsOnMap;
    int enemiesOnMap;

    int enemiesToSpawn;
    int lvl;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        enemiesToSpawn = 5;
        StartCoroutine(NextLvl());
        for (int i = 0;i < 5;i++)
        {
            SpawnGun();
        }
    }

    IEnumerator NextLvl()
    {
        while (true)
        {
            yield return new WaitForSeconds(lvlTime);
            lvl++;
            lvlTime -= 0.05f;
            lvlTime = Mathf.Clamp(lvlTime, 1, 10);
            for (int i = 0; i < enemiesToSpawn + lvl; i++)
            {
                SpawnEnemy();
            }
        }
    } 

    void SpawnEnemy()
    {
        float x = Random.Range(-1, 1);
        float y= Random.Range(-1, 1);
        Vector2 spawnPos;
        float range = Random.Range(minRange, maxRange);
        spawnPos = new Vector2(x, y) * range;
        GameObject enemy = Instantiate(enemyPrefub, spawnPos, Quaternion.identity);
        enemy.GetComponent<EnemyMovement>().player = player;
        enemy.GetComponent<EnemyMovement>().movementSpeed = enemySpeed;
    }

    public void SpawnGun()
    {
        if(gunsOnMap < 5)
        {
            int x = Random.Range(border.x, border.x + border.width);
            int y = Random.Range(border.y, border.x + border.height);
            Vector2 spawnPos = new Vector2(x, y);
            Instantiate(gunPrefub, spawnPos, Quaternion.identity);
            gunsOnMap++;
        }
    }

    public void RemoveGun()
    {
        gunsOnMap--;
        SpawnGun();
    }

    public void GameOver(int kills)
    {
        StopAllCoroutines();
        gameOverLvl.text = "Died at lvl " + lvl;
        gameOverScore.text = "Total of " + kills + " Enemies killed";
        gameOverScreen.SetActive(true);
    }

    public void StartOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
