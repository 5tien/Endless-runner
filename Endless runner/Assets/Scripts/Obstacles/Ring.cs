using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
            print("nice man, punten erbij");
    }
}
