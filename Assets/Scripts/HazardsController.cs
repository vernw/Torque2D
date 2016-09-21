using UnityEngine;
using System.Collections;

public class HazardsController : MonoBehaviour
{
    public static HazardsController instance = null;

    public GameObject blackHolePrefab;
    public GameObject wormholePrefab;
    public GameObject gravityFieldPrefab;
    public GameObject depthChargePrefab;
    public GameObject powerUpPrefab;
    public GameObject meteorShowerPrefab;

    /*** Event Toggles ***/
    public bool blackHoleActive;
    public bool wormholeActive;
    public bool gravityFieldActive;
    public bool depthChargeActive;
    public bool powerUpActive;
    public bool meteorShowerActive;

    public float hazardBuffer;
    public float maxX, minX;
    public float maxY, minY;

    void Awake()
    {
        // Ensures singleton status of HazardsController
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        hazardBuffer = 10f;
        maxX = 9f;
        minX = -9f;
        maxY = 3f;
        minY = -3f;

        SpawnEvents();
    }

    void SpawnEvents()
    {
        if (blackHoleActive)
        {
            // StartCoroutine(SpawnBH());
        }
        if (wormholeActive)
        {
            StartCoroutine(SpawnWH());
        }
        if (gravityFieldActive)
        {
            StartCoroutine(SpawnGF());
        }
        if (depthChargeActive)
        {
            StartCoroutine(SpawnDC());
        }
        if (powerUpActive)
        {
            StartCoroutine(SpawnPU());
        }
        if (meteorShowerActive)
        {
            StartCoroutine(SpawnMS());
        }
    }

    // Black hole spawner
    IEnumerator SpawnBH()
    {
        yield return new WaitForSeconds(hazardBuffer + Random.Range(0f, 16f));

        Vector3 pos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);

        GameObject blackHole = Instantiate(blackHolePrefab, pos, Quaternion.identity) as GameObject;
        StartCoroutine(SpawnBH());
    }

    // Wormhole spawner
    IEnumerator SpawnWH()
    {
        yield return new WaitForSeconds(hazardBuffer + Random.Range(0f, 16f));

        Vector3 pos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);

        GameObject wormhole = Instantiate(wormholePrefab, pos, Quaternion.identity) as GameObject;
        StartCoroutine(SpawnWH());
    }

    // Gravity field spawner
    IEnumerator SpawnGF()
    {
        yield return new WaitForSeconds(hazardBuffer + Random.Range(0f, 16f));

        Vector3 pos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);

        GameObject gravityField = Instantiate(gravityFieldPrefab, pos, Quaternion.identity) as GameObject;
        StartCoroutine(SpawnGF());
    }

    // Depth charge spawner
    IEnumerator SpawnDC()
    {
        yield return new WaitForSeconds(hazardBuffer + Random.Range(0f, 16f));

        Vector3 pos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);

        GameObject depthCharge = Instantiate(depthChargePrefab, pos, Quaternion.identity) as GameObject;
        StartCoroutine(SpawnDC());
    }

    // Powerup spawner
    IEnumerator SpawnPU()
    {
        yield return new WaitForSeconds(hazardBuffer + Random.Range(0f, 16f));

        Vector3 pos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);

        GameObject powerUp = Instantiate(powerUpPrefab, pos, Quaternion.identity) as GameObject;
        StartCoroutine(SpawnPU());
    }

    // Meteor spawner
    IEnumerator SpawnMS()
    {
        yield return new WaitForSeconds(hazardBuffer + Random.Range(0f, 1f));

        Vector3 pos1 = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
        Vector3 pos2 = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
        Vector3 pos3 = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);

        GameObject meteorShower1 = Instantiate(meteorShowerPrefab, pos1, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(hazardBuffer + Random.Range(0f, 0.4f));
        GameObject meteorShower2 = Instantiate(meteorShowerPrefab, pos2, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(hazardBuffer + Random.Range(0f, 0.4f));
        GameObject meteorShower3 = Instantiate(meteorShowerPrefab, pos3, Quaternion.identity) as GameObject;
        StartCoroutine(SpawnMS());
    }
}
