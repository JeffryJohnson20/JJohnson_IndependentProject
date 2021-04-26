using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] flyingPrefabs;
    public GameObject ufo1;
    public GameObject ufo2;
    private float zRange = 180.0f;
    private float xRange = 13.0f;
    private float ufo1X = -1.5f;
    private float ufoY = 15.5f;
    private float ufo2X = 3.25f;
    private float ufo2Z = 192.0f;
    private PlayerController plyrCtrl;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAsteroid", 3.0f, 1.5f);
        InvokeRepeating("SpawnUfo", 3.0f, 1.5f);
        plyrCtrl = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnRandomAsteroid()
    {
        if (plyrCtrl.playerAlive == true)
        {
            float randXRange = Random.Range(-xRange, xRange);
            float randZRange = Random.Range(-2, zRange);
            int flyingPrefabIndex = Random.Range(0, flyingPrefabs.Length);
            Vector3 randPos = new Vector3(randXRange, 30, randZRange);
            Instantiate(flyingPrefabs[flyingPrefabIndex], randPos,
                flyingPrefabs[flyingPrefabIndex].transform.rotation);
        }
    }

    void SpawnUfo()
    {
        if (plyrCtrl.playerAlive == true)
        {
            Vector3 ufo1Pos = new Vector3(ufo1X, ufoY, 0);
            Instantiate(ufo1, ufo1Pos, ufo1.transform.rotation);

            Vector3 ufo2Pos = new Vector3(ufo2X, ufoY, ufo2Z);
            Instantiate(ufo2, ufo2Pos, ufo2.transform.rotation);
        }
    }
}
