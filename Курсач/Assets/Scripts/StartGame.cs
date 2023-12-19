using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    public bool GameStarted = false;

    public void Interact()
    {
        GameStarted = true;
    }
}
