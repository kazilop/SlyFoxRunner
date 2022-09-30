using System;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class Gold : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioSource;
    [SerializeField] private AudioClip clip;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PickupGold();
            audioSource.PlayOneShot(clip);
        }
    }

    private void PickupGold()
    {
        anim?.SetTrigger("Pickup");
        GameStats.Instance.CollectGold();
    }

    public void OnShowChunk()
    {
        anim?.SetTrigger("Idle");
    }
}
