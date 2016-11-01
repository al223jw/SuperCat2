using UnityEngine;
using System.Collections;

public class ShieldBox : MonoBehaviour {

    private UpgradeManager upgradeScript;

    public Rigidbody2D NoCoinBoxPrefab;
    public Transform shieldBox;

    void Awake()
    {

    }

    void Start()
    {
        upgradeScript = GameObject.Find("UpgradeManager").GetComponent<UpgradeManager>();
    }

    void Update()
    {

    }

    public void GetShield(ShieldBox box)
    {
        GameMaster.GetShield(box);
        Destroy(box.gameObject, 0.2f);
        Instantiate(NoCoinBoxPrefab, shieldBox.position, shieldBox.rotation);

        upgradeScript.AddShield();
    }
}
