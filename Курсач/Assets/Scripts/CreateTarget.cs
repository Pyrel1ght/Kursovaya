using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTarget : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public float timeBetween;
    private float tempTime;
    private void Awake()
    {
        tempTime = timeBetween;
    }
    private void create()
    {
        if (timeBetween <= 0)
        {
            Instantiate(target,gameObject.transform.position,Quaternion.identity);
            timeBetween = tempTime;
            if (tempTime > 0.5f)
            {
                tempTime -= 0.0001f;
            }
        }
        
    }
    private void FixedUpdate()
    {
        create();
        timeBetween -= Time.deltaTime;
    }

}

