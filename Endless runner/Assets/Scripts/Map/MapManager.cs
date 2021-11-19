using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Floor Settings")]

    [SerializeField] private List<GameObject> sections = new List<GameObject>();
    [SerializeField] private Transform beginPosition;
    [SerializeField] private Transform mapParent;

    [Tooltip("Minimal amount of floors at all time")]
    [Min(20)] [SerializeField] private int minFloor;


    [Header("Obstacle Settings Settings")]

    [SerializeField] private List<GameObject> obstacles = new List<GameObject>();
    [SerializeField] private Transform obstacleParent;
    
    [Tooltip("The chance of an obstacle spawning")]
    [Range(1, 100)] [SerializeField] private float spawnChance;


    private List<GameObject> placedSections = new List<GameObject>();
    private List<GameObject> placedObstacles = new List<GameObject>();

    private Vector3 currentPosition;

    void Awake()
    {
        if (beginPosition == null || sections.Count == 0)
            Destroy(this);
    }

    void CreateObstacle(int _type, float _rotation, Vector3 _position)
    {
        GameObject newObstacle = Instantiate(obstacles[_type], obstacleParent);

        newObstacle.transform.rotation = Quaternion.Euler(0, 0, _rotation);
        newObstacle.transform.position = _position;

        placedObstacles.Add(newObstacle);
    }

    void CreateFloor(int _type, float _rotation)
    {
        GameObject newFloor = Instantiate(sections[_type], mapParent);
        Floor floor = newFloor.GetComponent<Floor>();

        newFloor.transform.rotation = Quaternion.Euler(0, 0, -_rotation);
        newFloor.transform.position = currentPosition + (newFloor.transform.position - floor.Begin.position);

        currentPosition = floor.End.position;
        placedSections.Add(newFloor);

        if (Random.Range(0, 100) <= spawnChance - 1)
            CreateObstacle(0, -_rotation, newFloor.transform.position + Vector3.up);

        if (spawnChance < 30)
            spawnChance += 0.1f;

        print(spawnChance);
    }

    IEnumerator CheckObstacles()
    {
        yield return new WaitForSeconds(1);

        for (int i = 0; i < placedObstacles.Count; i++)
        {
            GameObject obstacle = placedObstacles[i];

            if (Camera.main.transform.position.x - 25 > obstacle.transform.position.x)
            {
                placedObstacles.Remove(obstacle);
                Destroy(obstacle);
            }
        }

        StartCoroutine(CheckObstacles());
    }

    IEnumerator CheckFloors()
    {
        yield return new WaitForSeconds(1);

        for (int i = 0; i < placedSections.Count; i++)
        {
            GameObject floor = placedSections[i];

            if (Camera.main.transform.position.x - 25 > floor.GetComponent<Floor>().End.position.x)
            {
                placedSections.Remove(floor);
                Destroy(floor);
            }
        }

        if (placedSections.Count < minFloor)
            for (int i = 0; i < minFloor - placedSections.Count; i++)
                CreateFloor(Random.Range(0, sections.Count), Random.Range(4, 25));

        StartCoroutine(CheckFloors());
    }

    void Start()
    {
        currentPosition = beginPosition.position;

        for (int i = 0; i < minFloor; i++)
        {
            CreateFloor(Random.Range(0, sections.Count), Random.Range(4, 25));
        }

        StartCoroutine(CheckFloors());
        StartCoroutine(CheckObstacles());
    }
}
