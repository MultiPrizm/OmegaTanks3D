using System.Collections;
using UnityEngine;

public class Player_Shooting : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    private float rotateHorizontal = 0;
    [SerializeField] private GameObject bullet_prefab;
    [SerializeField] private GameObject bullet_spawn;
    [SerializeField] private float bulletForce;
    [SerializeField] private float timeAfterShot = 0.25f;
    private Rigidbody _rb_bullet;
    private bool cunShoot = true;
    private void Start()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && cunShoot == true)
        {
            int layerMask = 1 << 8;
            layerMask = ~layerMask;

            /*
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10f, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10f, Color.white);
                Debug.Log("Did not Hit");
            }
            */
            GameObject CLoneBullet = Instantiate(bullet_prefab, bullet_spawn.transform.position, bullet_prefab.transform.rotation);
            CLoneBullet.transform.rotation = Quaternion.Euler(bullet_prefab.transform.position.x, transform.position.y, bullet_prefab.transform.position.z);
            //CLoneBullet.GetComponent<Rigidbody>().AddForce(Vector3.forward * bulletForce,ForceMode.Impulse);
            //CLoneBullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
            
            CLoneBullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletForce;
            cunShoot = false;
            StartCoroutine(Reload());
        }
    }
    private void FixedUpdate()
    {


        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0) { 
            float angle = Mathf.Atan2(_joystick.Vertical, _joystick.Horizontal) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.down);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(angle-90, Vector3.down), 20f);
        }
        else
        {
            Debug.Log(Quaternion.Euler(0, transform.parent.rotation.y, 0));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.parent.rotation, 10f);
        }
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(timeAfterShot);
        cunShoot = true;
    }
}
