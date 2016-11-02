using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

    [System.Serializable]
    public class BossStats
    {

        public int maxHealth = 1500;


        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public int damage = 100;

        public void Init()
        {
            curHealth = maxHealth;
        }

    }

    public BossStats stats = new BossStats();

    void Start()
    {
        stats.Init();
    }

    public void DamageBoss(int damage)
    {
        stats.curHealth -= damage;
        if (stats.curHealth <= 0)
        {
            GameMaster.KillBoss(this);
        }
    }

    void OnCollisionEnter2D(Collision2D _colInfo)
    {
        Player _player = _colInfo.collider.GetComponent<Player>();

        if (_player != null)
        {
            if(GameMaster.gm.shieldState == true)
            {
                DamageBoss(500);
                _player.DamagePlayer(stats.damage);
            }
            else
            {
                _player.DamagePlayer(stats.damage);
            }
        }
    }

}
