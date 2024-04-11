using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//地面移动功能
public class LandMove : MonoBehaviour
{
    public Transform other;
    public float Speed;
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed, Space.World);
    }
    private void LateUpdate()
    {
        if (transform.position.z <= -13.0f) transform.position = other.position + new Vector3(0.0f, 0.0f, 20.0f);
    }
}
