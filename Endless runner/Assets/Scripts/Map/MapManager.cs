using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> sections = new List<GameObject>();

    [SerializeField] private Transform beginPosition;
    [SerializeField] private Transform mapParent;

    private List<GameObject> placedSections = new List<GameObject>();

    private Vector3 currentPosition;

    void Awake()
    {
        if (beginPosition == null || sections.Count == 0)
            Destroy(this);
    }


    void CreateFloor(int _type, float _rotation)
    {
        GameObject newFloor = Instantiate(sections[_type], mapParent);
        Floor floor = newFloor.GetComponent<Floor>();

        newFloor.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -_rotation));
        newFloor.transform.position = currentPosition + (newFloor.transform.position - floor.Begin.position);

        currentPosition = floor.End.position;
        placedSections.Add(newFloor);
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

        if (placedSections.Count < 20)
            for (int i = 0; i < 20 - placedSections.Count; i++)
                CreateFloor(Random.Range(0, sections.Count), Random.Range(4, 25));

        StartCoroutine(CheckFloors());
    }

    void Start()
    {
        currentPosition = beginPosition.position;

        for (int i = 0; i < 20; i++)
        {
            CreateFloor(Random.Range(0, sections.Count), Random.Range(4, 25));
        }

        StartCoroutine(CheckFloors());
    }
}
