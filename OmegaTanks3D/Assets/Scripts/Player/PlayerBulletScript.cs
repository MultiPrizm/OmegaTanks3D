using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    [SerializeField] private float LifeTime = 1f;
    [SerializeField] private float bulletForce;
    [SerializeField] private ParticleSystem ExplosionParticles;
    [SerializeField] private AudioSource ExplosionAudio;
    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        //transform.rotation = Quaternion.Euler(90f, transform.rotation.y, transform.rotation.z);
        // Calculate the velocity based on the bullet's rotation
        Vector3 velocity = transform.rotation * Vector3.forward * bulletForce;

        // Apply the calculated velocity to the Rigidbody
        rb.velocity = velocity;
        StartCoroutine(DIELifeTime());
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            DIE();
        }
    }
    private void DIE()
    {
        ExplosionParticles.Play();
        ExplosionAudio.Play();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        Destroy(gameObject, 2);
    }
    IEnumerator DIELifeTime()
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }
}
