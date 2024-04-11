using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    //子弹射速
    public float Speed;
    void Start()
    {
        if (Speed < 0) Speed -= GameObject.Find("God").GetComponent<GodMake>().GameTime / (20f * GameObject.Find("God").GetComponent<GodMake>().Dfcty);
    }
    void Update()
    {
        XMove();
        //出范围即自我摧毁
        if (transform.position.z > 18.0f || transform.position.z < -3.0f || transform.position.x < -7 || transform.position.x > 7) Destroy(gameObject);
    }
    //子弹移动
    void XMove()
    {
        //火箭弹加速
        if (Speed > 0 && gameObject.name == "GuidedBullet(Clone)") Speed += (Time.deltaTime * 100);
            //沿x轴移动运动
            transform.Translate(Vector3.forward * Time.deltaTime * Speed);
    }
    //子弹击毁功能
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        if (gameObject.tag == "Player")
        {
            GameObject.Find("God").GetComponent<GodMake>().AddScore(other.gameObject.tag);
        }
        Destroy(gameObject);
    }
}
