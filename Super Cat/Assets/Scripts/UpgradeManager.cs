using UnityEngine;
using System.Collections;
using System;

public class UpgradeManager : MonoBehaviour {

    public LaserEyes laserScript;

    public Transform head;
    public Transform body;

    public GameObject glassesPrefab;
    public GameObject capePrefab;
    public GameObject glovePrefab;
    public GameObject hatPrefab;
    public GameObject tailPrefab;
    public GameObject shieldPrefab;

    private enum UPGRADE_TYPE {
        NONE,
        GLASSES,
        CAPE,
        GLOVE,
        HAT,
        TAIL
    };

    private UPGRADE_TYPE currentUpgrade = UPGRADE_TYPE.NONE;

    private Transform glassesSpawnpoint;
    private Transform capeSpawnpoint;
    private Transform glovesSpawnpoint;
    private Transform hatSpawnpoint;
    private Transform tailSpawnpoint;
    private Transform shieldSpawnpoint;
    
    private GameMaster gameMaster;


    // Use this for initialization
    void Start () {
        glassesSpawnpoint = head.GetChild(0);
        hatSpawnpoint = head.GetChild(1);
        capeSpawnpoint = body.GetChild(0);
        glovesSpawnpoint = body.GetChild(1);
        tailSpawnpoint = body.GetChild(2);
        shieldSpawnpoint = body.GetChild(3);

        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    public GameObject shield;

	// Update is called once per frame
	void Update () {
        if (gameMaster.shieldState == false)
            Destroy(shield.gameObject);
	}

    public void AddUpgrade()
    {
        currentUpgrade++;
        addUpgrade(currentUpgrade);
    }

    public void AddShield()
    {
        if(gameMaster.shieldState == true)
        {
            shield = Instantiate(shieldPrefab, shieldSpawnpoint.position, shieldSpawnpoint.rotation) as GameObject;
            shield.transform.parent = body.gameObject.transform;
        }
    }

    private void addUpgrade(UPGRADE_TYPE type)
    {
        GameObject prefab;
        Transform trans;
        Transform target = body;

        switch (type)
        {
            case UPGRADE_TYPE.CAPE:
                prefab = capePrefab;
                trans = capeSpawnpoint.transform;
                break;
            case UPGRADE_TYPE.GLASSES:
                prefab = glassesPrefab;
                trans = glassesSpawnpoint.transform;
                target = head;
                break;
            case UPGRADE_TYPE.GLOVE:
                prefab = glovePrefab;
                trans = glovesSpawnpoint.transform;
                break;
            case UPGRADE_TYPE.HAT:
                prefab = hatPrefab;
                trans = hatSpawnpoint.transform;
                target = head;
                break;
            case UPGRADE_TYPE.TAIL:
                prefab = tailPrefab;
                trans = tailSpawnpoint.transform;
                break;
            default:
                gameMaster.coin += 10;
                return;
        }

        GameObject upgrade = Instantiate(prefab, trans.position, trans.rotation) as GameObject;
        upgrade.transform.parent = target.gameObject.transform;

        Vector3 scale = upgrade.transform.localScale;
        scale.x *= Math.Sign(target.transform.localScale.x);
        upgrade.transform.localScale = scale;
    }
}
