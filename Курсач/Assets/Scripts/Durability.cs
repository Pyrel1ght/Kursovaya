using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Durability : MonoBehaviour
{
    public int hp = 10;
    public int points = 1;
    public AudioClip hit;
    private AudioSource sfx;
    GameObject PointText;
    private Animator Animator;

    private void Awake()
    {
        sfx = GetComponent<AudioSource>();
        Animator = GetComponent<Animator>();
        PointText = GameObject.Find("Points");
    }
    public void recieveDamage(int damage)
    {
        hp-=damage;
        if (hp <=0) {
            Animator.SetTrigger("Hit");
            sfx.PlayOneShot(hit, 0.5f);
            PointText.GetComponent<PointSystem>().UpdatePoints(points);
        }
    }
}
