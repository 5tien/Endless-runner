using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [Tooltip("Set this to true if you want to expand the pool if you run out of pooled objects.")]
    [SerializeField] private bool autoExpand = false;

    [Tooltip("The amount of new objects added when the pool runs out of objects.")]
    [SerializeField] private int expansionSize;

    [Tooltip("The prefab used for the Pool.")]
    //[SerializeField] public GameObject poolPrefab;
    [SerializeField] public List<GameObject> poolPrefabs = new List<GameObject>();

    public int poolSize = 10;

    //private Stack<PoolItem> objectPool;
    private List<Stack<PoolItem>> stack = new List<Stack<PoolItem>>();

    public void Start()
    {
        for (int i = 0; i < poolPrefabs.Count; i++)
        {
            stack.Add(new Stack<PoolItem>());
        }

        //objectPool = new Stack<PoolItem>();

        for (int i = 0; i < poolPrefabs.Count; i++)
        {
            Expand(poolSize, i);
        }

        //Expand(poolSize);

        if (expansionSize == 0)
            expansionSize = 1;
    }

    private void Expand(int size, int type)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject newObject = Instantiate(poolPrefabs[type], transform);

            PoolItem item = newObject.GetComponent<PoolItem>();

            item.pool = this;
            item.gameObject.SetActive(false);
            item.ID = type;

            stack[type].Push(item);
        }
    }

    public GameObject GetObject(int type, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (stack[type].Count == 0)
        {
            if (autoExpand)
                Expand(expansionSize, type);
            else
            {
                print($"{name}, Pool is empty.");
                return null;
            }
        }


        PoolItem item = stack[type].Pop();

        item.Init(position, rotation, parent != null ? parent : transform);
        item.gameObject.SetActive(true);

        return item.gameObject;
    }

    public void ReturnObject(PoolItem item)
    {
        if (item.gameObject.activeSelf == false)
            return;

        item.transform.parent = transform;
        item.gameObject.SetActive(false);

        stack[item.ID].Push(item);
    }
}
