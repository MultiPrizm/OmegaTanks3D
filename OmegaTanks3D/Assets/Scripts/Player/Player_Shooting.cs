using System.Collections;
using UnityEngine;

public class Player_Shooting : MonoBehaviour
{
    private float previousHorizontal;
    private float previousVertical;
    private float releaseThreshold = 0.5f;
    [SerializeField] private Joystick _joystick;
    private float rotateHorizontal = 0;
    [SerializeField] private GameObject bullet_prefab;
    [SerializeField] private GameObject bullet_spawn;

    [SerializeField] private float timeAfterShot = 0.25f;
    private Rigidbody _rb_bullet;
    private bool cunShoot = true;

    private AudioSource _audioSource;
    [SerializeField]private AudioClip shotClip;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        DetectJoystickRelease();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0) Time.timeScale = 1;
            if (Time.timeScale == 1) Time.timeScale = 0;
        }
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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(angle - 90, Vector3.down), 35f);
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

        }
        _audioSource.PlayOneShot(shotClip);
        GameObject CLoneBullet = Instantiate(bullet_prefab, bullet_spawn.transform.position, bullet_spawn.transform.rotation);

        //CLoneBullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletForce;
        cunShoot = false;
        StartCoroutine(Reload());
    }

    private void DetectJoystickRelease()
    {
        float currentHorizontal = _joystick.Horizontal;
        float currentVertical = _joystick.Vertical;

        // Проверка резкого изменения значений осей
        if ((Mathf.Abs(previousHorizontal - currentHorizontal) > releaseThreshold && currentHorizontal == 0) || (Mathf.Abs(previousVertical - currentVertical) > releaseThreshold && currentVertical == 0))
        {
            if(cunShoot == true)
                Shooting();
        }


        // Обновление предыдущих значений
        previousHorizontal = currentHorizontal;
        previousVertical = currentVertical;
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(timeAfterShot);
        cunShoot = true;
    }
}
