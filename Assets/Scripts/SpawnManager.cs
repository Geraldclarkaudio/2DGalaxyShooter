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
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject[] powerups;

    [SerializeField]
    private bool isEnemyAlive = false;//
    


    private bool stopSpawning = false;
    // Start is called before the first frame update

   
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnFollowEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemyRoutine()
    {

        yield return new WaitForSeconds(3.0f);
        //while loop 
        while (stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8.0f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            

            newEnemy.transform.parent = _enemyContainer.transform;
            
            yield return new WaitForSeconds(2.0f);
        }
    }

    IEnumerator SpawnFollowEnemyRoutine()
    {
        yield return new WaitForSeconds(10.0f);

        while (stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8.0f), 7, 0);
            GameObject follower = Instantiate(_followerEnemyPrefab, posToSpawn, Quaternion.identity);
            follower.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(5f);
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

