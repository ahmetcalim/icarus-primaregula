using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject effect;

    private void OnEnable()
    {
        StartCoroutine(DestroyBullet());
    }
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.transform.name);
        if (collider.GetComponent<Obstacle>())
        {

            if (collider.transform.tag == "Obstacle")
            {

                if (collider.GetComponent<Obstacle>().health > 0)
                {
                    collider.GetComponent<Obstacle>().health--;
                }
                else
                {
                    Instantiate(effect, new Vector3(collider.transform.position.x + 2, collider.transform.position.y + 2, collider.transform.position.z - 2), collider.transform.rotation, collider.gameObject.transform.parent);
                    Destroy(collider.gameObject);
                    
                }

            }
            if (collider.CompareTag("LaserBeam"))
            {
                if (collider.GetComponent<Obstacle>().health > 0)
                {
                    collider.GetComponent<Obstacle>().health--;
                }
                else
                {
                    Instantiate(effect, new Vector3(collider.transform.position.x, collider.transform.position.y, collider.transform.position.z - 3f), Quaternion.identity);
                    Destroy(collider.GetComponent<LaserBeam>().parent);
                }

            }
            Destroy(this.gameObject);
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(8);
        Destroy(this.gameObject);
    }
}
