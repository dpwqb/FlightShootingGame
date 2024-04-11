using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//特效3秒后自动摧毁
public class DesBoom : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 3);
    }
}
