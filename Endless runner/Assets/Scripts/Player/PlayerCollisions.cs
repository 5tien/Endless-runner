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
        Obstacle obstacle = other.transform.GetComponent<Obstacle>();

        if (obstacle && obstacle.CausesDamage == true)
            Death();
    }

}
