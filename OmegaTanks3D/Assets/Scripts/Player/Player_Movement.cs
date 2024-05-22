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
    private void Start()
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
    public Dictionary<string, float> GetStatistick()
    {
        Dictionary<string, float> StatistickBase = new Dictionary<string, float>();

        StatistickBase.Add("xPos", transform.position.x);
        StatistickBase.Add("zPos", transform.position.z);
        StatistickBase.Add("BaseYRot", transform.rotation.y);
        StatistickBase.Add("TowerYRot", Tower.transform.rotation.y);

        return StatistickBase;
    }
}
