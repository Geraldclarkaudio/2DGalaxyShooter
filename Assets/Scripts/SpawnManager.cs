using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _followerEnemyPrefab;

    [SerializeField]
    private GameObject _laserEnemyPrefab;

    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject[] powerups;

    [SerializeField]
    private bool isEnemyAlive = false;//
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
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Wave1Spawn()
    {
        yield return new WaitForSeconds(3.0f);

        while (_enemySpawned < 10 && stopSpawning == false)
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

            yield return new WaitForSeconds(2.0f);

            //Spawn Follower Enemy
            Vector3 posToSpawn2 = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject followEnemy = Instantiate(_followerEnemyPrefab, posToSpawn2, Quaternion.identity);
            followEnemy.transform.parent = _enemyContainer.transform;
            _enemySpawned++;
            

            yield return new WaitForSeconds(4.0f);
        }

        if (_enemySpawned == 10)
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
        yield return new WaitForSeconds(3.0f);

        while (_enemySpawned < 20 && stopSpawning == false)
        {
            
            //spawn regular enemy
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            _enemySpawned++;
            yield return new WaitForSeconds(Random.Range(2.0f, 5));

            //Spawn Follower Enemy
            Vector3 posToSpawn2 = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject followEnemy = Instantiate(_followerEnemyPrefab, posToSpawn2, Quaternion.identity);
            followEnemy.transform.parent = _enemyContainer.transform;
            _enemySpawned++;


            yield return new WaitForSeconds(Random.Range(4.0f, 6.0f));
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
        yield return new WaitForSeconds(3.0f);

        while (_enemySpawned < 40 && stopSpawning == false)
        {

            //spawn regular enemy
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            _enemySpawned++;

            yield return new WaitForSeconds(Random.Range(2.0f, 5));

            //Spawn Follower Enemy
            Vector3 posToSpawn2 = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject followEnemy = Instantiate(_followerEnemyPrefab, posToSpawn2, Quaternion.identity);
            followEnemy.transform.parent = _enemyContainer.transform;
            _enemySpawned++;

            yield return new WaitForSeconds(Random.Range(4.0f, 6.0f));

            //Spawn Laser Beam Enemy
            Vector3 posToSpawn3 = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject laserEnemy = Instantiate(_laserEnemyPrefab, posToSpawn2, Quaternion.identity);
            laserEnemy.transform.parent = _enemyContainer.transform;
            _enemySpawned++;


            yield return new WaitForSeconds(Random.Range(2.0f, 5.0f));
        }

        if (_enemySpawned == 40)
        {
            _wave++;
            _uiManager.UpdateWave(_wave);
            _enemySpawned = 0;
            StopCoroutine(Wave3Spawn());
            yield return new WaitForSeconds(5.0f);
            //StartCoroutine BOSS BATTLE

        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, 6);
            Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3f, 8f));
        }
    }


    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }

    public void EnemyAlive()//

    {
        isEnemyAlive = true;
    }
}

