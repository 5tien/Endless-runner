using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField] private int amount;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
            GameManager.instance.AddScore(amount);
    }
}
