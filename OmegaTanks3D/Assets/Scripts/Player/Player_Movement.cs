using System.Collections;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private float DirX, DirZ;
    [SerializeField] private float Speed = 1f;
    [SerializeField] private Joystick _joystick;
    private Rigidbody _Rb;
    private void Start()
    {
        _Rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        DirX = _joystick.Horizontal * Speed;
        DirZ = _joystick.Vertical * Speed;
    }
    private void FixedUpdate()
    {
        _Rb.velocity = new Vector3(DirX,0, DirZ);

        if(_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(_Rb.velocity);
        }
    }
}
