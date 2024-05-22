using System.Collections;
using UnityEngine;

public class Player_Shooting : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    private float rotateHorizontal = 0;
    [SerializeField] private GameObject bullet_prefab;
    [SerializeField] private GameObject bullet_spawn;

    [SerializeField] private float timeAfterShot = 0.25f;
    private Rigidbody _rb_bullet;
    private bool cunShoot = true;
    private void Start()
    {

    }
    private void Update()
    {
        Shooting();
    }
    private void FixedUpdate()
    {
        RotateTower();
    }
    private void RotateTower()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            float angle = Mathf.Atan2(_joystick.Vertical, _joystick.Horizontal) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(angle - 90, Vector3.down), 20f);
        }
        else
        {
            // Debug.Log(Quaternion.Euler(0, transform.parent.rotation.y, 0));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.parent.rotation, 10f);
        }
    }
    private void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.F) && cunShoot == true)
        {

            GameObject CLoneBullet = Instantiate(bullet_prefab, transform.position, transform.rotation);

            //CLoneBullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletForce;
            cunShoot = false;
            StartCoroutine(Reload());
        }
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(timeAfterShot);
        cunShoot = true;
    }
}
