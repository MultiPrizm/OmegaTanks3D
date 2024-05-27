using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private float DirX, DirZ;
    [SerializeField] private float Speed = 1f;
    [SerializeField] private float RotationSpeed = 1f;
    [SerializeField] private Joystick _joystick; // Vector2
    [Header("Tower")]
    [SerializeField] private GameObject Tower;

    private Rigidbody _Rb;
    [Header("Sounds")]
    [SerializeField] private AudioClip tank_idle;
    [SerializeField] private AudioClip tankDead;
    private AudioSource _audioSorse;
    private void Awake()
    {
        _audioSorse = GetComponent<AudioSource>();
        _Rb = GetComponent<Rigidbody>();
        _audioSorse.clip = tank_idle;
        _audioSorse.Play();
    }
    private void Update()
    {
        DirX = _joystick.Horizontal * Speed; 
        DirZ = _joystick.Vertical * Speed; 
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        _Rb.velocity = new Vector3(DirX, 0, DirZ);

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_Rb.velocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);
            if (_audioSorse.pitch <= Mathf.Max(Mathf.Abs(_joystick.Vertical), Mathf.Abs(_joystick.Horizontal)) + 1f) _audioSorse.pitch += 1f * Time.deltaTime;
            if (_audioSorse.pitch > Mathf.Max(Mathf.Abs(_joystick.Vertical), Mathf.Abs(_joystick.Horizontal)) + 1f && _audioSorse.pitch >= 1f) _audioSorse.pitch -= 1f * Time.deltaTime;
        }
        else
        {
            if (_audioSorse.pitch >= 1f) _audioSorse.pitch -= 1f * Time.deltaTime;
        }
    }
    public Templates.REQUES_UPDATEPLAYER_level3 GetStatistick()
    {
        Templates.REQUES_UPDATEPLAYER_level3 StatistickBase = new Templates.REQUES_UPDATEPLAYER_level3();

        StatistickBase.xPos = transform.position.x;
        StatistickBase.zPos = transform.position.z;
        StatistickBase.BaseYRot = transform.rotation.y;
        StatistickBase.TowerYRot = Tower.transform.rotation.y;
        StatistickBase.xVel = _Rb.velocity.x;
        StatistickBase.zVel = _Rb.velocity.z;

        return StatistickBase;
    }
}
