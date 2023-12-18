using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    private int startingPoints = 0;
    // Start is called before the first frame update
    public void UpdatePoints(int points)
    {
        startingPoints += points;
        gameObject.GetComponent<TextMeshProUGUI>().text = startingPoints.ToString();
    }
}
