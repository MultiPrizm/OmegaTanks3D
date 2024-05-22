using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    [SerializeField] private float LifeTime = 1f;
    [SerializeField] private float bulletForce;
    private void Start()
    {
        StartCoroutine(DIE());
        GetComponent<Rigidbody>().velocity = transform.up * bulletForce;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
    IEnumerator DIE()
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }
}
