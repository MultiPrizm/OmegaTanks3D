using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REmotePlayerController : MonoBehaviour
{
    [SerializeField] private GameObject Tower;
    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void TowerRotation(float y)
    {
        transform.rotation = Quaternion.Euler(Tower.transform.rotation.x, y, Tower.transform.rotation.z);
    }
    public void BodyRotation(float y)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, y, transform.rotation.z);
    }
    public void MovePosition(float x, float z, float xVel, float zVel)
    {
        transform.position = new Vector3(x, transform.position.y, z);
        _rb.velocity = new Vector3(xVel, 0, zVel);
    }

}
