using System.Collections;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private float DirX, DirZ;
    [SerializeField] private float Speed = 1f;
    [SerializeField] private float RotationSpeed = 1f;
    [SerializeField] private Joystick _joystick; // Vector2

    
    
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
}
