using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour {

    public GameObject[] animalPrefabs;

    public float spawnRate;
    public float spawnRange;

    private float spawnTimer;

    private float xOffset;

    void Start () {

        spawnTimer = Time.time;

        xOffset = -spawnRange;

	}

    void Update () {

        if (GameController.state != "playing")
            return;

        if(Time.time > spawnTimer) {

            SpawnRandomAnimal();

            spawnTimer = Time.time + spawnRate;

        }

	}

    void SpawnRandomAnimal() {

        int r = Random.Range(0, animalPrefabs.Length);

        GameObject newAnimal = Instantiate(animalPrefabs[r], transform.position + new Vector3(xOffset, 0, 0), Quaternion.identity);

        xOffset += spawnRange;

        if (xOffset > spawnRange)
            xOffset = -spawnRange;

    }

}
