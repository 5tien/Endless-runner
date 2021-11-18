using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    void Death()
    {
        print("ded");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Floor>() || other.transform.GetComponent<Obstacle>())
            Death();
    }

}
