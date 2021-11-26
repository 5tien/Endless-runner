using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour // Sten
{
    [Header("Floor Settings")]

    [SerializeField] private List<GameObject> sections = new List<GameObject>();
    [SerializeField] private Transform beginPosition;
    [SerializeField] private Transform mapParent;

    [Tooltip("Minimal amount of floors at all time")]
    [Min(0)] [SerializeField] private int minFloor;


    [Header("Obstacle Settings Settings")]

    [SerializeField] private List<GameObject> obstacles = new List<GameObject>();
    [SerializeField] private Transform obstacleParent;
    
    [Tooltip("The chance of an obstacle spawning")]
    [Range(1, 100)] [SerializeField] private float spawnChance;

    private Pool floorPool;
    private Pool objectPool;

    private List<GameObject> placedSections = new List<GameObject>();
    private List<GameObject> placedObstacles = new List<GameObject>();

    private Vector3 currentPosition;

    void Awake()
    {
        if (beginPosition == null || sections.Count == 0)
            Destroy(this);
    }

    void Start()
    {
        floorPool = mapParent.GetComponent<Pool>();
        objectPool = obstacleParent.GetComponent<Pool>();

        currentPosition = beginPosition.position;

        for (int i = 0; i < minFloor; i++)
        {
            CreateFloor(Random.Range(0, sections.Count), Random.Range(4, 25));
        }

        StartCoroutine(CheckFloors());
        StartCoroutine(CheckObstacles());
    }

    void CreateObstacle(int _type, GameObject _floor)
    {
        GameObject newObstacle = objectPool.GetObject(_type, Vector3.zero, Quaternion.identity, null);

        newObstacle.transform.position = _floor.transform.position + new Vector3(0, 4.5f - newObstacle.GetComponent<Obstacle>().spawnPoint.position.y);
        newObstacle.transform.rotation = _floor.transform.rotation;

        placedObstacles.Add(newObstacle);
    }

    void CreateFloor(int _type, float _rotation)
    {
        GameObject newFloor = floorPool.GetObject(_type, Vector3.zero, Quaternion.identity, null);

        Floor floor = newFloor.GetComponent<Floor>();

        newFloor.transform.rotation = Quaternion.Euler(0, 0, -_rotation);
        newFloor.transform.position = currentPosition + (newFloor.transform.position - floor.End.position);

        currentPosition = floor.Begin.position;
        placedSections.Add(newFloor);

        if (Random.Range(0, 100) <= spawnChance - 1)
            CreateObstacle(Random.Range(0, obstacles.Count), newFloor);

        if (spawnChance < 30)
            spawnChance += 0.08f;
    }

    IEnumerator CheckObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            for (int i = 0; i < placedObstacles.Count; i++)
            {
                GameObject obstacle = placedObstacles[i];

                if (Camera.main.transform.position.x - 30 > obstacle.transform.position.x)
                {
                    placedObstacles.Remove(obstacle);
                    obstacle.GetComponent<PoolItem>().ReturnToPool();
                }
            }
        }
    }

    IEnumerator CheckFloors()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            for (int i = 0; i < placedSections.Count; i++)
            {
                GameObject floor = placedSections[i];

                if (Camera.main.transform.position.x - 30 > floor.GetComponent<Floor>().End.position.x)
                {
                    placedSections.Remove(floor);
                    floor.GetComponent<PoolItem>().ReturnToPool();
                }
            }

            if (placedSections.Count < minFloor)
                for (int i = 0; i < minFloor - placedSections.Count; i++)
                    CreateFloor(Random.Range(0, sections.Count), Random.Range(4, 25));
        }
    }
}
