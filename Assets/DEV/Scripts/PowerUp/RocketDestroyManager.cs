using UnityEngine;

public class RocketDestroyManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("Laser"))
        {
            Destroy(other);
        }
    }
}
