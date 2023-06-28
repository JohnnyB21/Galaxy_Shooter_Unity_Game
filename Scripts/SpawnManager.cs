using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject[] powerups;

    private bool _stopSpawning = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }


    // Spawn game objects every 5 seconds
    // Create coroutine of type IEnumerator -- Yield events
    // while loop

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            float random = Random.Range(-9f, 9f);
            Vector3 position = new Vector3(random, 8, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            int randPowerup = Random.Range(0,3); 
            Vector3 pos = new Vector3(Random.Range(-9f, 9f), 8, 0);
            Instantiate(powerups[randPowerup], pos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3f, 9f));

        }
    }

    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }
}
