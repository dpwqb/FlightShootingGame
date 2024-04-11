using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guided : MonoBehaviour
{
    public float RotSpeed;
    void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0) * Input.GetAxis("Mouse X") * RotSpeed);
    }
}
