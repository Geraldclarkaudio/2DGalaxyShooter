using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject avoiderEnemy;

    [SerializeField]
    private GameObject shieldEnemy;

    [SerializeField]
    private GameObject _behindEnemyPrefab;

    [SerializeField]
    private GameObject _bossEnemyPrefab;

    [SerializeField]
    private GameObject _followerEnemyPrefab;

    [SerializeField]
    private GameObject _laserEnemyPrefab;

    [SerializeField]
    private GameObject _enemyContainer;


    //POWERUP SPAWN MANAGER STUFF
    public int total;
    public int randomNumber;

    //public int enemyTotal;
   // public int enemyRandomNumber;

    public List<GameObject> powerup;
    public List<GameObject> enemy;

    public int[] table = {

        40, //ammo
        25, // heat seek
        15,//health
        10, // shield
        8 //triple 
    };

   /* public int[] enemyTable =
    {
        50,//regular enemy
        20,// follower enemy
        10,//behind enemy
        20,//avoider enemy
        10,//laser beam enemy
    };*/

    [SerializeField]
  
    private bool stopSpawning = false;

    //wave stuff
    [SerializeField]
    private int _wave;
    [SerializeField]
    private int _enemySpawned;

    private UIManager _uiManager;


    // Start is called before the first frame update

    private void Start()
    {
        _wave = 1;
        _enemySpawned = 0;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

    }
    public void StartSpawning()
    {

       StartCoroutine(Wave1Spawn());
       // StartCoroutine(SpawnEnemyRoutine());
       StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   /* void ChooseEnemy()
    {
        enemyTotal = 0;

        foreach (var item in enemyTable)
        {
            enemyTotal += item;
        }

        enemyRandomNumber = Random.Range(0, total);

        for (int i = 0; i < enemyTable.Length; i++)
        {
            if (enemyRandomNumber <= enemyTable[i])
            {
                Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                Instantiate(enemy[i], posToSpawn, Quaternion.identity);
                return;
            }
            else
            {
                enemyRandomNumber -= enemyTable[i];
            }
        }
    }*/

    void ChooseAPowerUp()
    {
        total = 0;

        foreach (var item in table)
        {
            total += item;
        }

        randomNumber = Random.Range(0, total);

        for (int i = 0; i < table.Length; i++)
        {
            if (randomNumber <= table[i])
            {
                Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                Instantiate(powerup[i], posToSpawn, Quaternion.identity);
                return;
            }
            else
            {
                randomNumber -= table[i];
            }
        }
    }

   IEnumerator Wave1Spawn()
    {
        yield return new WaitForSeconds(1.0f);

        while (_enemySpawned < 12 && stopSpawning == false)
        {
           if(_enemySpawned == 0)
            {
                _uiManager.UpdateWave(_wave);
            }

            //spawn regular enemy
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
             GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
             newEnemy.transform.parent = _enemyContainer.transform;
             _enemySpawned++;

            yield return new WaitForSeconds(1.0f);

            //Spawn Follower Enemy
            Vector3 posToSpawn2 = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject followEnemy = Instantiate(_followerEnemyPrefab, posToSpawn2, Quaternion.identity);
            followEnemy.transform.parent = _enemyContainer.transform;
            _enemySpawned++;

            yield return new WaitForSeconds(2.5f);

            //Spawn BehindFire Enemy
            Vector3 posToSpawn3 = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject behindEnemy = Instantiate(_behindEnemyPrefab, posToSpawn3, Quaternion.Euler(0, 0, 90));
            behindEnemy.transform.parent = _enemyContainer.transform;
            _enemySpawned++;

            yield return new WaitForSeconds(3.0f);
        }

        if (_enemySpawned == 12) // needs to be a multiple of the amount of prefabs or the coroutine stops and wont start the next.. 
        {
            _wave++;
            _uiManager.UpdateWave(_wave);
            _enemySpawned = 0;
            StopCoroutine(Wave1Spawn());
           
            yield return new WaitForSeconds(5.0f);
            
            StartCoroutine(Wave2Spawn());
        }
    }

    IEnumerator Wave2Spawn()
    {

        while (_enemySpawned < 20 && stopSpawning == false)
        {
            
            //spawn regular enemy
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            _enemySpawned++;
            yield return new WaitForSeconds(1.0f);

            //Spawn Follower Enemy
            Vector3 posToSpawn2 = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject followEnemy = Instantiate(_followerEnemyPrefab, posToSpawn2, Quaternion.identity);
            followEnemy.transform.parent = _enemyContainer.transform;
            _enemySpawned++;
            yield return new WaitForSeconds(2.5f);

            Vector3 posToSpawn3 = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject behindEnemy = Instantiate(_behindEnemyPrefab, posToSpawn3, Quaternion.Euler(0, 0, 90));
            behindEnemy.transform.parent = _enemyContainer.transform;
            _enemySpawned++;
            yield return new WaitForSeconds(3.0f);

            Vector3 posToSpawn4 = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject avoidEnemy = Instantiate(avoiderEnemy, posToSpawn4, Quaternion.identity);
            avoidEnemy.transform.parent = _enemyContainer.transform;
            _enemySpawned++;
            yield return new WaitForSeconds(2.5f);

            Vector3 posToSpawn5 = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject shield = Instantiate(shieldEnemy, posToSpawn5, Quaternion.identity);
            shield.transform.parent = _enemyContainer.transform;
            _enemySpawned++;
            yield return new WaitForSeconds(2.5f);

        }

        if (_enemySpawned == 20)
        {
            _wave++;
            _uiManager.UpdateWave(_wave);
            _enemySpawned = 0;
            StopCoroutine(Wave2Spawn());
            yield return new WaitForSeconds(5.0f);
            StartCoroutine(Wave3Spawn());
        }
    }

    IEnumerator Wave3Spawn()
    {
        

        while (_enemySpawned < 40 && stopSpawning == false)
        {

            //spawn regular enemy
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            _enemySpawned++;

            yield return new WaitForSeconds(Random.Range(1.0f, 2));

            //Spawn Follower Enemy
            Vector3 posToSpawn2 = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject followEnemy = Instantiate(_followerEnemyPrefab, posToSpawn2, Quaternion.identity);
            followEnemy.transform.parent = _enemyContainer.transform;
            _enemySpawned++;

            yield return new WaitForSeconds(Random.Range(1.0f, 2.5f));


            //Spawn Laser Beam Enemy
            Vector3 posToSpawn3 = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject laserEnemy = Instantiate(_laserEnemyPrefab, posToSpawn2, Quaternion.identity);
            laserEnemy.transform.parent = _enemyContainer.transform;
            _enemySpawned++;
            yield return new WaitForSeconds(Random.Range(4.0f, 5.0f));


            Vector3 posToSpawn5 = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject shield = Instantiate(shieldEnemy, posToSpawn5, Quaternion.identity);
            shield.transform.parent = _enemyContainer.transform;
            _enemySpawned++;
            yield return new WaitForSeconds(2.5f);

            Vector3 posToSpawn4 = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject behindEnemy = Instantiate(_behindEnemyPrefab, posToSpawn4, Quaternion.Euler(0, 0, 90));
            behindEnemy.transform.parent = _enemyContainer.transform;
            _enemySpawned++;
            yield return new WaitForSeconds(3.0f);

            Vector3 posToSpawn6 = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject avoidEnemy = Instantiate(avoiderEnemy, posToSpawn6, Quaternion.identity);
            avoidEnemy.transform.parent = _enemyContainer.transform;
            _enemySpawned++;
            yield return new WaitForSeconds(3.5f);
        }

        if (_enemySpawned >= 40)
        {
            _wave++;
            _uiManager.UpdateWave(_wave);
            _enemySpawned = 0;
            StopCoroutine(Wave3Spawn());
            yield return new WaitForSeconds(2.0f);
            StartCoroutine(BossBattle());

        }
    }

    IEnumerator BossBattle()
    {
        yield return new WaitForSeconds(2);

        while (_enemySpawned < 1 && stopSpawning == false)
        {
            Vector3 posToSpawn4 = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject BossEnemy = Instantiate(_bossEnemyPrefab, posToSpawn4, Quaternion.identity);
            BossEnemy.transform.parent = _enemyContainer.transform;
            _enemySpawned++;
            StopCoroutine(BossBattle());
            yield return new WaitForSeconds(5f);
        }  
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (stopSpawning == false)
        {
            ChooseAPowerUp();

            yield return new WaitForSeconds(Random.Range(2f,2.5f));

        }
    }
    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }

    public void YouWin()
    {
        _wave++;
        _uiManager.UpdateWave(_wave);
        StopCoroutine(SpawnPowerUpRoutine());
    }
}

