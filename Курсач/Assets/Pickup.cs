using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    bool Inventory = false;
    public GameObject GunPrefab;
    public GameObject Player;
    public GameObject Camera;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon" && !Inventory)
        {
            Destroy(other.gameObject);
            pickUp();
            
        }
    }
    private void pickUp()
    {
        Inventory = true;
        Instantiate(GunPrefab, new Vector3(0.767f, -0.409f, 0.5f), Quaternion.identity);
        GameObject Gun = GameObject.Find("Gun(Clone)");
        Gun.transform.SetParent(Camera.transform, false);
        Gun.transform.localEulerAngles = new Vector3(-10f, -20f, 0f);
    }
}
