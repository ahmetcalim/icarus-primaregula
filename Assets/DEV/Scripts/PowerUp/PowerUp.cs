
using UnityEngine;
public class PowerUp : MonoBehaviour
{
    public enum PowerUpType {PHASE, ROCKET, BULLET_TIME}
    public PowerUpType powerUpType;
    public Sprite sprite;
    public string tagName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("Laser"))
        {
            Destroy(gameObject);
        }
    }
}
