using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> sections = new List<GameObject>();

    [SerializeField] private Transform beginPosition;

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

        for (int i = 0; i < 20; i++)
        {
            GameObject newFloor = Instantiate(sections[0], currentPosition, Quaternion.Euler(new Vector3(0, 0, -Random.Range(4, 15))));
            Floor floor = newFloor.GetComponent<Floor>();

            newFloor.transform.position = currentPosition + (newFloor.transform.position - floor.Begin.position);

            currentPosition = floor.End.position;
        }
    }
}
