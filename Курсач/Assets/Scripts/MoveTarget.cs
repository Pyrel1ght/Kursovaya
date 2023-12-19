using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    public float speed = 0.00001f;
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        transform.position += new Vector3(1*Time.deltaTime* (5+speed), 0, 0);
        if (speed < 5)
        {
            speed += 0.0001f;
        }
    }
    private void Update()
    {
        if (transform.position.x > 6) { 
            Destroy(gameObject);
        }
    }
}
