using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Material material;

    private void Update()
    {
        transform.position = Vector3.forward * player.transform.position.z;
    }
}
