using UnityEngine;
using System.Collections;

public class UpgradeBox : MonoBehaviour {

    private UpgradeManager upgradeScript;

    public Rigidbody2D NoUppgradeBoxPrefab;
    public Transform upgradeBox;
   
    void Awake()
    {
        
        //    if(le == null)
        //    {
        //        le = GameObject.FindGameObjectWithTag("Head");
        //        // headlessCat = transform.FindChild("Body").transform;

        //        if (headlessCat == null)
        //        {
        //            Debug.LogError("System failure. (No body)");
        //        }
        //    }
    }

    // Use this for initialization
    void Start () {
        upgradeScript = GameObject.Find("UpgradeManager").GetComponent<UpgradeManager>();
    }
	
	// Update is called once per frame
	void Update () {
      
	}

    public void GetUppgrade(UpgradeBox uppgrade)
    {
        GameMaster.GetUppgrade(uppgrade);
        Destroy(uppgrade.gameObject);
        Instantiate(NoUppgradeBoxPrefab, upgradeBox.position, upgradeBox.rotation);

        upgradeScript.AddUpgrade();
    }
}
