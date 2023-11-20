using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    float rotationX =0f;
    float rotationY =0f;
    public float sens = 10f;
    public GameObject Player;
    public GameObject Camera;

    // Update is called once per frame

    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * sens;
        rotationY += Input.GetAxis("Mouse Y") * sens * -1;
        rotationY = Mathf.Clamp(rotationY, -90,90);
        transform.position = Camera.transform.position;
        transform.rotation = Camera.transform.rotation;
        Player.transform.localEulerAngles = new Vector3(0, rotationX, 0);
        Camera.transform.localEulerAngles = new Vector3(rotationY, 0, 0);
    }
}
