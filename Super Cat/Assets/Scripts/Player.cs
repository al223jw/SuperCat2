using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    [System.Serializable]
    public class PlayerStats {

        public int maxHealth = 100;
        public int coins = 0;


        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void Init()
        {
            curHealth = maxHealth;
        }

    }

    public PlayerStats stats = new PlayerStats();

    void Start()
    {
        stats.Init();
    }

    public void DamagePlayer(int damage)
    {
        if(GameMaster.gm.shieldState == true)
        {
            GameMaster.gm.shieldState = false;    
        }
        else
        {
            GameMaster.KillPlayer(this);
        }
    }

    void OnCollisionEnter2D(Collision2D _object)
    {
        CoinBox _coinBox = _object.collider.GetComponent<CoinBox>();
        UpgradeBox _uppgradeBox = _object.collider.GetComponent<UpgradeBox>();
        ShieldBox _shieldBox = _object.collider.GetComponent<ShieldBox>();

        if (_object.gameObject.tag == "CoinBox")
        {
            _coinBox.GetCoin(_coinBox);
        }

        if (_object.gameObject.tag == "UppgradeBox")
        {
            _uppgradeBox.GetUppgrade(_uppgradeBox);
        }

        if(_object.gameObject.tag == "shieldBox")
        {
            _shieldBox.GetShield(_shieldBox);
        }

        if (_object.gameObject.tag == "winIcon")
        {
            GameMaster.gm.winlevel();
        }
    }
}
