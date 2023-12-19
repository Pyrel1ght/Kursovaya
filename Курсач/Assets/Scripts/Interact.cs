using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}
public class Interact : MonoBehaviour
{
    public AudioClip button,invalidInteract;
    private AudioSource sfx;
    public Transform Source;
    public float range =2f;

    private void Awake()
    {
        sfx = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(Source.position,Source.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, range)) {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactable))
                {
                    sfx.PlayOneShot(button, 0.5f);
                    interactable.Interact();
                } else
                {
                    sfx.PlayOneShot(invalidInteract, 0.5f);
                }
            }
        }
    }
}
