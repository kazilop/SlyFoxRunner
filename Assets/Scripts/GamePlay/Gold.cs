using System;
using UnityEngine;

public class Gold : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PickupGold();
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
