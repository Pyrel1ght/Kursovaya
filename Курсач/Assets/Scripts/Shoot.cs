using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    RaycastHit hit;
    public GameObject Camera;
    private Animator animator;
    public GameObject Ammo;
    private AudioSource sfx;
    public AudioClip shoot,reload;
    public GameObject BulletSpawn;
    public ParticleSystem MuzzleFlash;
    public float TimeBetweenShots, range, reloadingTime;
    public int MagazineSize, BulletsLeft, damage;
    bool shooting, reloading = false, readyToShoot= true;
    public ParticleSystem ImpactSand,ImpactMetal;
    public TrailRenderer BulletTrail;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        sfx = GetComponent<AudioSource>();
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
        animator.SetTrigger("Reloading");
        sfx.PlayOneShot(reload, 1f);
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
        MuzzleFlash.Play();
        sfx.PlayOneShot(shoot, 0.5f);
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, range))
        {
            TrailRenderer trail = Instantiate(BulletTrail, BulletSpawn.transform.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));
            Debug.Log(hit.collider.name);
            if (hit.collider.CompareTag("Target"))
            {
                hit.collider.GetComponent<Durability>().recieveDamage(damage);
                Debug.Log(hit.collider.name);
            }
        }
        animator.SetTrigger("Shooting");
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
        while (timer < 1* (1/hit.distance))
        {
            trail.transform.position = Vector3.Lerp(Start, hit.point, timer);
            timer += Time.deltaTime / trail.time;
            yield return null;
        }
        trail.transform.position = hit.point;
        switch (hit.collider.tag)
        {
            case "Floor":
                Instantiate(ImpactSand, hit.point, Quaternion.LookRotation(hit.normal));
                break;
            case "Walls":
                Instantiate(ImpactMetal, hit.point, Quaternion.LookRotation(hit.normal));
                break;
        }
        Debug.Log(hit.distance);
        Destroy(trail.gameObject, trail.time);
    }
}
