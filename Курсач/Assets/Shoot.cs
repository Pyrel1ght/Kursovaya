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
    public GameObject BulletSpawn;
    public float TimeBetweenShots, range, reloadingTime;
    public int MagazineSize, BulletsLeft, damage;
    bool shooting, reloading = false, readyToShoot= true;
    //private ParticleSystem Impact, ShootingSystem;
    public TrailRenderer BulletTrail;
    private void Awake()
    {
        Camera = GameObject.Find("Main Camera");
        Ammo = GameObject.Find("Ammo");
        BulletSpawn = GameObject.Find("BulletSpawn");
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
                TrailRenderer trail = Instantiate(BulletTrail, BulletSpawn.transform.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail,hit));
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
   private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float timer = 0;
        Vector3 Start = trail.transform.position;
        while (timer < 1)
        {
            trail.transform.position = Vector3.Lerp(Start, hit.point, timer);
            timer += Time.deltaTime / trail.time;
            yield return null;
        }
        trail.transform.position = hit.point;
        //Instantiate(Impact, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(trail.gameObject, trail.time);
    }
}
