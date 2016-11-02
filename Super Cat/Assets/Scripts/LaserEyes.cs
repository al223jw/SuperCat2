using UnityEngine;
using System.Collections;

public class LaserEyes : MonoBehaviour {

    public float fireRate = 0;
    public LayerMask whatToHit;

    public Transform FlashPrefb;
    public Transform LaserPrefab;
    public Transform HitPrefab;

    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;
    float timeToFire = 0;
    public Transform firePoint;

	// Use this for initialization
	void Awake () {
        firePoint = transform.FindChild("firePoint");

        if(firePoint == null)
        {
            Debug.LogError("System failure. (No firePoint)");
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if(fireRate == 0)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if(Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
	}

    void Shoot()
    {
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                       Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        Vector2 firePointPos = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPos, mousePos - firePointPos, 100, whatToHit); // mousePos - firePointPos = Direction.

        if (Time.time >= timeToSpawnEffect)
        {
            Effect();
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }

        if(hit.collider != null)
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.DamageEnemy(Damage());
            }
        }
    }

    void Effect()
    {
        Instantiate(LaserPrefab, firePoint.position, firePoint.rotation);
        Transform clone = (Transform)Instantiate(FlashPrefb, firePoint.position, firePoint.rotation);
        clone.parent = firePoint;

        float size = Random.Range(0.6f, 0.9f);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, 0.02f);
    }

    private int Damage()
    {
        int damage = 10;

        for(int i = 0; i <= GameMaster.gm.uppgradeState; i++)
        {
            damage += 10;
        }
        return damage;
    }
}
