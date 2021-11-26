using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField] private int amount;
    void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.transform);

        if (collision.GetComponent<Player>())
            GameManager.instance.AddScore(amount);
    }
}
