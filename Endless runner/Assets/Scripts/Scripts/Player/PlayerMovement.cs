using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jump_power;


    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = this.transform.GetComponent<Rigidbody>();
    }


    void Update()
    {
        Jump();
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddForce(new Vector3(0, 50));
        }
    }
}
