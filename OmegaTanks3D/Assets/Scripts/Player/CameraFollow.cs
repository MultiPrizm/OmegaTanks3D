using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private Transform FollowPlayer;
    [SerializeField] private Vector3 moveSettings = new Vector3(0,0,0);
    private void FixedUpdate()
    {
        if (FollowPlayer != null)
        {
            Vector3 Target = new Vector3(FollowPlayer.position.x + moveSettings.x, transform.position.y, FollowPlayer.position.z + moveSettings.z);
            transform.position = Vector3.Lerp(transform.position, Target, followSpeed * Time.deltaTime);
        }
    }
}
