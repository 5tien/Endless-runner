using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float rotationSpeed;


    private Rigidbody2D rigidbody;

    private Transform ray_point;

    private float yRotate = 0;

    private void Start()
    {
        rigidbody = this.transform.GetComponent<Rigidbody2D>();
        ray_point = this.transform.Find("Ground Part");
    }


    void Update()
    {
        Move();
        Jump();
        RotatePlayer();

        speed += Time.deltaTime / 100;

        GameManager.instance.SetDistance((int)this.transform.position.x);
    }

    bool IsOnFloor()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(this.transform.position, ray_point.position - this.transform.position, 1);

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

            rigidbody.velocity = new Vector2(speed, oldVelocity.y);
        }
        else if (rigidbody.velocity.x < speed)
            rigidbody.AddForce(new Vector3(speed, 0));
    }

    IEnumerator Spin()
    {
        int used = 0;
        float rotation = 0;

        yield return new WaitForSeconds(0.1f);

        while (IsOnFloor() == false)
        {


            if (Input.GetKey(KeyCode.LeftArrow) && (used == 0 || used == 2))
            {
                used = 1;
                rotation = this.transform.rotation.z + 25;
            }

            if (Input.GetKey(KeyCode.RightArrow) && (used == 0 || used == 1))
            {
                used = 2;
                rotation = this.transform.rotation.z - 25;
            }
            
            if (used != 0 && this.transform.rotation.z > rotation && this.transform.rotation.z < rotation)
                print("DAMN");



            yield return new WaitForSeconds(0.1f);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOnFloor() == true)
        {
            rigidbody.AddForce(new Vector2(0, jumpPower * 10));
            StartCoroutine(Spin());
        }
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            yRotate = yRotate + Time.deltaTime * (rotationSpeed / 50);
        else if (Input.GetKey(KeyCode.RightArrow))
            yRotate = yRotate - Time.deltaTime * (rotationSpeed / 50);

        if (IsOnFloor() == true)
        {
            yRotate = 0;
            return;
        }
        else if (yRotate > 1)
            yRotate = 1;
        else if (yRotate < -1)
            yRotate = -1f;


        rigidbody.angularVelocity = 0;

        this.transform.Rotate(0, 0, yRotate * rotationSpeed * Time.deltaTime);
    }
}
