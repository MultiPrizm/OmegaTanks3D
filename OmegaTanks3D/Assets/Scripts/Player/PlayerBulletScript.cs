using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    [SerializeField] private float LifeTime = 1f;
    private void Start()
    {
        StartCoroutine(DIE());
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
