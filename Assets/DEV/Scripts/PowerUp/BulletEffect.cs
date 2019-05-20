using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DestroyBulletEffect());
    }

    IEnumerator DestroyBulletEffect()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
