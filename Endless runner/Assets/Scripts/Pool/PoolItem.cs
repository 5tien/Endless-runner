using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItem : MonoBehaviour
{
    private Pool myPool;
    public Pool pool { set { myPool = value; } }
    public int ID;

    public void Init(Vector3 position, Quaternion rotation, Transform parent)
    {
        transform.position = position;
        transform.rotation = rotation;
        transform.parent = parent;

        Activate();
    }

    protected virtual void Activate()
    {
        //
    }
    protected virtual void Deactivate()
    {
        //
    }

    public void ReturnToPool()
    {
        Deactivate();

        myPool.ReturnObject(this);
    }
}
