using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    public Rigidbody rb;
    public float speed = 5f;
    public GameObject Player;
    public float jumpForce = 200f;
    public float Mass = 10f;
    bool IsGrounded = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        rb.AddForce(0, -1 * Time.deltaTime * Mass, 0, ForceMode.Acceleration);
        if (Input.GetKey("w"))
        {
            rb.AddForce(Player.transform.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(-Player.transform.right * speed * Time.deltaTime, ForceMode.VelocityChange);
        }
        if (Input.GetKey("d"))
        {
            rb.AddForce(Player.transform.right * speed * Time.deltaTime, ForceMode.VelocityChange);
        }
        if (Input.GetKey("s"))
        {
            rb.AddForce(-Player.transform.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.Space) && IsGrounded)
        {
            rb.AddForce(Player.transform.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
            IsGrounded = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
            IsGrounded = true;
        }
    }
}