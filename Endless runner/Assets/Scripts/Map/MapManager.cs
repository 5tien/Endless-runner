using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> sections = new List<GameObject>();
    [SerializeField] private Transform beginPosition;

    private List<GameObject> placedSections = new List<GameObject>();

    private Vector3 currentPosition;

    void Awake()
    {
        if (beginPosition == null || sections.Count == 0)
            Destroy(this);
    }


    void CreateFloor(int _type, float _rotation)
    {
        GameObject newFloor = Instantiate(sections[_type], currentPosition, Quaternion.Euler(new Vector3(0, 0, -_rotation)));
        Floor floor = newFloor.GetComponent<Floor>();

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

            if (!floor.GetComponent<Renderer>().isVisible && Camera.main.transform.position.x - 25 > floor.GetComponent<Floor>().End.position.x)
            {
                placedSections.Remove(floor);
                Destroy(floor);
            }
        }

        StartCoroutine(CheckFloors());
    }

    void Start()
    {
        currentPosition = beginPosition.position;

        for (int i = 0; i < 100; i++)
        {
            CreateFloor(Random.Range(0, sections.Count), Random.Range(4, 25));
        }

        StartCoroutine(CheckFloors());
    }
}
