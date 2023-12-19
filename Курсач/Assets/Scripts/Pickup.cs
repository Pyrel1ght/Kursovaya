using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    bool Inventory = false;
    public GameObject GunPrefab;
    private AudioSource sfx;
    public AudioClip pickupWeapon;
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
    private void Awake()
    {
        sfx = GetComponent<AudioSource>();
    }
    private void pickUp()
    {
        Inventory = true;
        Instantiate(GunPrefab, new Vector3(0.638f, -0.26f, 0.716f), Quaternion.identity);
        GameObject Gun = GameObject.Find("Gun(Clone)");
        Gun.transform.SetParent(Camera.transform, false);
        Gun.transform.localEulerAngles = new Vector3(0.393f, -73.481f, -3.819f);
        sfx.PlayOneShot(pickupWeapon, 0.5f);
    }
}
