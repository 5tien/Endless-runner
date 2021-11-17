using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float rotationSpeed;


    private Rigidbody rigidbody;

    private Transform ray_point;

    private void Start()
    {
        rigidbody = this.transform.GetComponent<Rigidbody>();
        ray_point = this.transform.Find("Ground Part");
    }


    void Update()
    {
        Move();
        Jump();
        RotatePlayer();
    }

    bool IsOnFloor()
    {
        RaycastHit[] hits = Physics.RaycastAll(this.transform.position, ray_point.position - this.transform.position, 1);

        for (int i = 0; i < hits.Length; i++)
            if (hits[i].transform.CompareTag("Floor"))
                return true;

        return false;
    }

    void Move()
    {
        if (rigidbody.velocity.x > speed)
        {
            Vector3 oldVelocity = rigidbody.velocity;

            rigidbody.velocity = new Vector3(speed, oldVelocity.y, oldVelocity.z);
        }
        else
            rigidbody.AddForce(new Vector3(speed, 0));
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOnFloor() == true)
            rigidbody.AddForce(new Vector3(0, jumpPower * 10));
    }

    void RotatePlayer()
    {
        if (IsOnFloor() == false)
            if (Input.GetKey(KeyCode.LeftArrow))
                this.transform.Rotate(new Vector3(0, 0, 1 * rotationSpeed));
            else if (Input.GetKey(KeyCode.RightArrow))
                this.transform.Rotate(new Vector3(0, 0, -1 * rotationSpeed));
    }
}
