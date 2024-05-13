using System.Collections;
using UnityEngine;

public class Player_Shooting : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    private float rotateHorizontal = 0;
    private void Start()
    {
    }
    private void FixedUpdate()
    {
        if(_joystick.Horizontal != 0 || _joystick.Vertical != 0) { 
            float angle = Mathf.Atan2(_joystick.Vertical, _joystick.Horizontal) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.down);
            
        }
        else
        {
            Debug.Log(Quaternion.Euler(0, transform.parent.rotation.y - 90, 0));
            transform.rotation = transform.parent.rotation;
        }
    }
}
