﻿using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    public static GameMaster gm;
    public int coin = 0;
    public int uppgradeState = 0;
    public bool shieldState = false;
    private float deleteExplosions = 2f;

    void Awake()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 1;
    public Transform hitPrefab;
    public Transform bossKillPrefab;

    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    private GameObject winLevel;


    public void EndGame()
    {
        gameOverUI.SetActive(true);
    }

    public void winlevel()
    {
        winLevel.SetActive(true);
    }

    public static void KillPlayer(Player player)
    {
        gm._KillPlayer(player);
    }

    private void _KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        gm.EndGame();
    }


    public static void KillEnemy(Enemy enemy)
    {
        gm._KillEnemy(enemy);
    }

    private void _KillEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
        Instantiate(hitPrefab, enemy.transform.position, enemy.transform.rotation);
    }

    public static void KillBoss(Boss boss)
    {
        gm._KillBoss(boss);
    }

    private void _KillBoss(Boss boss)
    {
        Destroy(boss.gameObject);
        Instantiate(bossKillPrefab, boss.transform.position, boss.transform.rotation);
    }


    public static void GetCoin(CoinBox coin)
    {
        gm._GetCoin(coin);
    }

    private void _GetCoin(CoinBox _coin)
    {
        coin++;
    }

    public static void GetShield(ShieldBox box)
    {
        gm._GetShield(box);
    }

    private void _GetShield(ShieldBox box)
    {
        shieldState = true;
    }

    public static void GetUppgrade(UpgradeBox uppgrade)
    {
        gm._GetUppgrade(uppgrade);
    }
    
    private void _GetUppgrade(UpgradeBox _uppgrade)
    {
        uppgradeState++; 
    } 

    void Update()
    {
        deleteExplosions -= Time.deltaTime;
        if(deleteExplosions <= 0f)
        {
            deleteExplosions = 2f;
            GameObject explosion = GameObject.FindGameObjectWithTag("Explosion");
            if(explosion != null)
            { 
                Destroy(explosion.gameObject);
            }
        }
    }
}
