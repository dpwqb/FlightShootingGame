using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    bool flag = true;
    //石头移速
    public float Speed;
    Vector3 Momentum;

    //爆炸特效
    public GameObject VFX;

    void Start()
    {
        Momentum = Random.insideUnitSphere;
        Speed -= GameObject.Find("God").GetComponent<GodMake>().GameTime / (20f * GameObject.Find("God").GetComponent<GodMake>().Dfcty);
    }
    void Update()
    {
        transform.Rotate(Momentum * Time.deltaTime * Random.Range(200.0f, 300.0f));
        XMove();
        //出范围即自我摧毁
        if (transform.position.z < -3.0f)
        {
            Destroy(gameObject);
            flag = false;
        }
    }
    //陨石撞毁对方功能
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
    //陨石移动功能
    void XMove()
    {
        //沿x轴移动运动
        transform.Translate(Vector3.forward * Time.deltaTime * Speed, Space.World);
    }
    //陨石爆炸特效
    private void OnDestroy()
    {
        if (flag) Instantiate(VFX, transform.position, Quaternion.identity);
    }
}
