using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject enemy;

    private float itemSpawnCycle = 10f;
    private float timeElapsed = 0f;

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > itemSpawnCycle) {
            GameObject temp;
            temp = (GameObject)Instantiate(enemy);
            Vector3 pos = temp.transform.position;
            temp.transform.position = new Vector3(Random.Range(-10f,10f), 0.0f, Random.Range(-10f,10f));
            timeElapsed -= itemSpawnCycle;
        }
    }
}
