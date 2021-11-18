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

    private float yRotate = 0;

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

        Debug.DrawRay(this.transform.position, ray_point.position - this.transform.position, Color.green);

        for (int i = 0; i < hits.Length; i++)
            if (hits[i].transform.GetComponent<Floor>())
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
        else if (rigidbody.velocity.x < speed)
            rigidbody.AddForce(new Vector3(speed, 0));
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOnFloor() == true)
            rigidbody.AddForce(new Vector3(0, jumpPower * 10));
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            yRotate = yRotate + Time.deltaTime * (rotationSpeed / 5);
        else if (Input.GetKey(KeyCode.RightArrow))
            yRotate = yRotate - Time.deltaTime * (rotationSpeed / 5);

        if (IsOnFloor() == true)
        {
            yRotate = 0;
            return;
        }
        else if (yRotate >  1)
            yRotate =  1;
        else if (yRotate < -1)
            yRotate = -1f;


        rigidbody.angularVelocity = Vector3.zero;

        this.transform.Rotate(0, 0, yRotate * rotationSpeed);
    }
}
