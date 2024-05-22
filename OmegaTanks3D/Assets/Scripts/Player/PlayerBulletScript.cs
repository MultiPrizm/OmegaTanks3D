using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    [SerializeField] private float LifeTime = 1f;
    [SerializeField] private float bulletForce;
    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.Euler(90f, transform.rotation.y, transform.rotation.z);
        // Calculate the velocity based on the bullet's rotation
        Vector3 velocity = transform.rotation * Vector3.forward * bulletForce;

        // Apply the calculated velocity to the Rigidbody
        rb.velocity = velocity;
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
