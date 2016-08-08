using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    [System.Serializable]
    public class EnemyStats
    {

        public int maxHealth = 30;


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

    public EnemyStats stats = new EnemyStats();

    void Start()
    {
        stats.Init();
    }

    public void DamageEnemy(int damage)
    {
        stats.curHealth -= damage;
        if (stats.curHealth <= 0)
        {
            GameMaster.KillEnemy(this);
        }
    }

    void OnCollisionEnter2D(Collision2D _colInfo)
    {
        Player _player = _colInfo.collider.GetComponent<Player>();

        if(_player != null)
        {
            _player.DamagePlayer(stats.damage);
        }
    }
}
