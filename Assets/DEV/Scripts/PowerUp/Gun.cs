using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float bulletSpeed = 10;
    public Rigidbody bullet;

    public GameObject gunObj;
    public GameObject joyBooster;

    public void Fire()
    {
        if (BulletStorage.BulletAmount > 0)
        {
            BulletStorage.BulletAmount--;
            Rigidbody bulletClone = (Rigidbody)Instantiate(bullet, transform.position, Quaternion.Euler(90f, 0f, 0f));
            bulletClone.velocity = -transform.up * bulletSpeed;
        }
       
    }
    
}