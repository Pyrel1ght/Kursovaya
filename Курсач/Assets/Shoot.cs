using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    RaycastHit hit;
    public GameObject Camera;
    public GameObject Ammo;
    public float TimeBetweenShots, range, reloadingTime;
    public int MagazineSize, BulletsLeft, damage;
    bool shooting, reloading = false, readyToShoot= true;
    private void Awake()
    {
        Camera = GameObject.Find("Main Camera");
        Ammo = GameObject.Find("Ammo");
        Ammo.GetComponent<TextMeshProUGUI>().text = BulletsLeft.ToString();

    }
    private void Update()
    {
        inputSystem();
    }
    private void inputSystem()
    {
        shooting = Input.GetKeyDown(KeyCode.Mouse0);
        if (Input.GetKeyDown(KeyCode.R) && BulletsLeft < MagazineSize && !reloading) Reload();

        if (readyToShoot&&shooting&&!reloading && BulletsLeft > 0)
        {
            ShootGun();
        }
    }
    private void Reload()
    {
        reloading= true;
        Invoke("ReloadFinished", reloadingTime);
    }
    private void ReloadFinished()
    {
        BulletsLeft = MagazineSize;
        reloading = false;
        Ammo.GetComponent<TextMeshProUGUI>().text = BulletsLeft.ToString();
        Debug.Log("ReloadFinished");
    }
    private void ShootGun()
    {
        readyToShoot = false;

        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, range))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.CompareTag("Target"))
            {
                hit.collider.GetComponent<Durability>().recieveDamage(damage);
                Debug.Log(hit.collider.name);
            }
        }
        BulletsLeft--;
        Ammo.GetComponent<TextMeshProUGUI>().text = BulletsLeft.ToString();
        Invoke("ResetShot", TimeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot=true;
    }
   
}
