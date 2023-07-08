using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float playerMoveSpeed = 0.01f;
    public Rigidbody rb;
    Vector3 Movement;
    float Rotation;

    // Update is called once per frame
    void Update()
    {
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");

        Rotation = Input.GetAxisRaw("Rotation");
    }

    private void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + Movement * playerMoveSpeed * Time.fixedDeltaTime);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + (Rotation * rotationSpeed)));

        //Rotation - Please don't ask me how this works.
        //Vector3 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 Direction = MousePosition - transform.position;
        //float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        //rb.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
