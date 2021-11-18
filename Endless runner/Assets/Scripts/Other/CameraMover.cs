using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 offset;

    void Update()
    {
        if (target == null)
            return;

        this.transform.position = new Vector3(0, 0, -10) + target.transform.position + offset;       
    }
}
