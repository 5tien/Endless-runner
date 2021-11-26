using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private bool DieFromFloor;

    void Death()
    {
        print("ded");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Obstacle obstacle = collision.transform.GetComponent<Obstacle>();
        Floor floor = collision.transform.GetComponent<Floor>();

        if ((obstacle && obstacle.CausesDamage) || (DieFromFloor && floor))
            GameManager.instance.Death();
    }
}
