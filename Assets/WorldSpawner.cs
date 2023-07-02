using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] worldItems;
    private GameObject player;
    Vector2 lastSpawnpos;
    private string zone;
    private int distanceBetweenObstacles;
    private int obstacleToPlayerSpawnDistance;
    private string[] zones = {"tubes", "spikes", "coins"};
    Vector2 lastCoin;
    private void Start()
    {
        lastCoin =  new Vector2(0, Random.Range(-4f,4));
        distanceBetweenObstacles = 0;
        player = GameObject.Find("Player");
        lastSpawnpos = player.transform.position;
        obstacleToPlayerSpawnDistance = 15;
        zone = "coins";
        
    }

    private void Update()
    {
        if(player.transform.position.x - lastSpawnpos.x >= distanceBetweenObstacles)
        {
            int changeZoneProb = Random.Range(0, 100);

            switch (zone)
            {
                case "tubes":
                    float tubeHeigthPos = Random.Range(5f, 9f);
                    float tubeHeigthDistance = Random.Range(13f, 15f);
                    float coinProbability = Random.Range(0, 11);
                    Instantiate(worldItems[0], new Vector2(lastSpawnpos.x + distanceBetweenObstacles + obstacleToPlayerSpawnDistance, tubeHeigthPos), Quaternion.identity);
                    Instantiate(worldItems[0], new Vector2(lastSpawnpos.x + distanceBetweenObstacles + obstacleToPlayerSpawnDistance, tubeHeigthPos - tubeHeigthDistance), Quaternion.identity);
                    if(coinProbability <= 2) Instantiate(worldItems[1], new Vector2(lastSpawnpos.x + distanceBetweenObstacles + obstacleToPlayerSpawnDistance, tubeHeigthPos - (tubeHeigthDistance/2)), Quaternion.identity);
                    distanceBetweenObstacles = Random.Range(4, 6);
                    break;
                case "spikes":
                    int[] dirs = { -1, 1 };
                    int dir = dirs[Random.Range(0,2)];
                    float spikeHeigthPos = Random.Range(-4f, -8f);
                    GameObject spike = Instantiate(worldItems[2], new Vector2(lastSpawnpos.x + distanceBetweenObstacles + obstacleToPlayerSpawnDistance, spikeHeigthPos*dir), Quaternion.identity);
                    spike.transform.localScale= new Vector3(spike.transform.localScale.x, spike.transform.localScale.y*dir, spike.transform.localScale.z);
                    distanceBetweenObstacles = Random.Range(5, 8);

                    break;
                case "coins":
                    lastCoin = new Vector2(lastSpawnpos.x + distanceBetweenObstacles + obstacleToPlayerSpawnDistance, Mathf.Clamp( lastCoin.y + Random.Range(-2f, 2f), -4f,4f));
                    Instantiate(worldItems[1], lastCoin, Quaternion.identity);
                    changeZoneProb = Random.Range(0, 40);
                    distanceBetweenObstacles = Random.Range(1, 4);
                    break;
            }
            lastSpawnpos = player.transform.position;
            if (changeZoneProb <= 5)
            {
                zone = zones[Random.Range(0, zones.Length)];
            }
        }
        
    }
}