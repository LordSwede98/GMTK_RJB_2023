using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerMoveSpeed = 0.01f;
    public Rigidbody rb;

    Vector3 Movement;
    Vector3 Rotation;

    // Update is called once per frame
    void Update()
    {
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Movement * playerMoveSpeed * Time.fixedDeltaTime);

        Vector3 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 Direction = MousePosition - transform.position;
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        rb.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        
        
        //Vector3 euler = transform.rotation.eulerAngles;
        //rb.MoveRotation(Quaternion.Euler(new Vector3(Input.mousePosition.x, -90, 90)));
    }
}
