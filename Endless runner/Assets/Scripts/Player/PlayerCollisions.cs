using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    void Death()
    {
        print("ded");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Obstacle obstacle = collision.transform.GetComponent<Obstacle>();

        if (obstacle && obstacle.CausesDamage == true)
            Death();
    }
}
