using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float timeBeforeStart;
    public float timeBetweenWaves;
    public float spawnRate;

    public GameObject spikePrefab;

    public int leftAmountOfSpikes;
    public int rightAmountOfSpikes;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(timeBeforeStart);
        while(true)
        {
            int randomAmountOfSpikes = Random.Range(leftAmountOfSpikes, rightAmountOfSpikes);
            for (int i = 0; i < randomAmountOfSpikes; i++)
            {
                if(gameObject.tag == "DownGenerator")
                {
                    Instantiate(spikePrefab, transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(spawnRate);
                }
                else
                {
                    Instantiate(spikePrefab, transform.position, Quaternion.Euler(0, 180, 180));
                    yield return new WaitForSeconds(spawnRate);
                }
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
}
