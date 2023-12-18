using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Durability : MonoBehaviour
{
    public int hp = 10;
    public int points = 1;
    GameObject PointText;

    private void Awake()
    {
        PointText = GameObject.Find("Points");
    }
    public void recieveDamage(int damage)
    {
        hp-=damage;
        if (hp <=0) { 
            Destroy(gameObject);
            PointText.GetComponent<PointSystem>().UpdatePoints(points);
        }
    }
}
