using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed = 30f;
    
    private void Update()
    {
        transform.Rotate(new Vector3(0,speed * Time.deltaTime,0));
    }
}
