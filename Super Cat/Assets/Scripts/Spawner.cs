using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Bullet
    {
        public string name;
        public Transform bullet;
        public int count;
        public float rate;
    }

    public Bullet[] waves;
    private int nextWave = 0;

    public Transform[] Spawnpoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    void Start()
    {

        if(Spawnpoints.Length == 0)
        {
            Debug.LogError("No spawnpoints");
        }

        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if(state == SpawnState.WAITING)
        {
            if(!BulletsIsAlive())
            {
                NoMoreBullets();
            }
            else
            {
                return;
            }
        }

        if(waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnBullets(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void NoMoreBullets()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length)
        {
            nextWave = 0;
        }
        else
        {
            nextWave++;
        }
    }

    bool BulletsIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Bullet").Length == 0)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnBullets(Bullet _bullet)
    {
        state = SpawnState.SPAWNING;

        for(int i = 0; i < _bullet.count; i++)
        {
            SpawnBullet(_bullet.bullet);
            yield return new WaitForSeconds(1f / _bullet.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnBullet(Transform _bullet)
    {
        Transform _sp = Spawnpoints[Random.Range(0, Spawnpoints.Length)];
        Instantiate(_bullet, _sp.position, _sp.rotation);
    }
}
