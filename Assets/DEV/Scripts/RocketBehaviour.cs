using System.Collections;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    RocketLauncher rocketLauncher;
    public Transform target;
    private bool launched;
    private void Start()
    {
        rocketLauncher = FindObjectOfType<RocketLauncher>();
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        yield return new WaitForSeconds(.01f);
        if (gameObject && transform && target)
        {
            if (!launched)
            {
                if (transform != null && target != null)
                {
                    GetComponent<Rigidbody>().velocity = Vector3.forward* Random.Range(PlayerPrefs.GetFloat("movementZMax") - 70f, PlayerPrefs.GetFloat("movementZMax") + 70f) * 3f;
                    StartCoroutine(Move());
                }
               
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObstacleSelected"))
        {
            if (!launched)
            {
                if (gameObject && transform && target)
                {
                    launched = true;
                    Instantiate(rocketLauncher.explosion, new Vector3(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f), transform.position.z + Random.Range(-1f, 1f)), Quaternion.identity);
                    Destroy(target.gameObject);
                    Destroy(gameObject);
                }
                else
                {
                    StartCoroutine(DestroySelf());
                }
                if (transform && gameObject && !target)
                {
                    StartCoroutine(DestroySelf());
                }
            }
        }
        else
        {
           
        }
    }
    IEnumerator Expo()
    {
        yield return new WaitForSeconds(0.05f);
    }
    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(.3f);
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
        
    }
}
