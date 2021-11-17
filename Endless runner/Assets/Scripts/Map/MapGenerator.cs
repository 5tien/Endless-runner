using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> sections = new List<GameObject>();
    [SerializeField] private Transform beginPosition;

    private List<GameObject> placedSections = new List<GameObject>();

    private Vector3 currentPosition;

    void Awake()
    {
        if (beginPosition == null || sections.Count == 0)
        {
            print("No Begin Position Given | No Sections Added");
            Destroy(this);
        }
    }

    void Start()
    {
        currentPosition = beginPosition.position;

        for (int i = 0; i < 100; i++)
        {
            GameObject floorType = sections[Random.Range(0, sections.Count )];

            GameObject newFloor = Instantiate(floorType, currentPosition, Quaternion.Euler(new Vector3(0, 0, -Random.Range(4, 25))));

            Floor floor = newFloor.GetComponent<Floor>();

            newFloor.transform.position = currentPosition + (newFloor.transform.position - floor.Begin.position);

            currentPosition = floor.End.position;

            placedSections.Add(newFloor);
        }
    }
}
