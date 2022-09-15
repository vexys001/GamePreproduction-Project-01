using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] ObjectsToSpawn;

    public Transform Point1;
    public Transform Point2;

    public float SpawnTimer;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= SpawnTimer)
        {
            SpawnRandomObject();
            _timer = 0;
        }
    }

    void SpawnRandomObject()
    {
        int rng = Mathf.FloorToInt(Random.Range(0, ObjectsToSpawn.Length));

        Vector3 pos = transform.position + new Vector3(0, 0, Random.Range(Point1.position.z, Point2.position.z));

        Instantiate(ObjectsToSpawn[rng], pos, Quaternion.identity);
    }
}
