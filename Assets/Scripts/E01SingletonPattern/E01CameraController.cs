using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E01CameraController : MonoBehaviour
{
    [SerializeField] private float height;
    [SerializeField] private float frontOffset;
    [SerializeField] private Transform target;

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, height, target.position.z + frontOffset);
        }
    }
}
