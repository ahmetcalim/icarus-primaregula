
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tunnel")
        {
            Destroy(gameObject);
        }
    }
}
