using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShip : MonoBehaviour
{
    bool flag = true;
    //敌人射速
    public float Speed;
    //开火功能
    public GameObject EnemyBullet;
    public float FileTimeCtrl = 0.5f;
    private float FileTime;
    //追击功能
    static float LRSpeed = 5;
    //爆炸特效
    public GameObject VFX;
    void Start()
    {
        Speed -= GameObject.Find("God").GetComponent<GodMake>().GameTime / (20f * GameObject.Find("God").GetComponent<GodMake>().Dfcty);
    }
    void Update()
    {
        XMove();
        //出范围即自我摧毁
        if (transform.position.z < -3.0f)
        {
            Destroy(gameObject);
            flag = false;
        }
        FireBullet();
    }
    //敌方飞船撞毁对方功能
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
    //敌方飞船移动功能
    void XMove()
    {
        //移动
        if (GameObject.Find("playerShip"))
        {
            if (GameObject.Find("playerShip").GetComponent<Transform>().position.x > transform.position.x) transform.Translate(new Vector3(1 * Time.deltaTime * LRSpeed, 0, 1 * Time.deltaTime * Speed));
            else if (GameObject.Find("playerShip").GetComponent<Transform>().position.x < transform.position.x) transform.Translate(new Vector3(1 * Time.deltaTime * -LRSpeed, 0, 1 * Time.deltaTime * Speed));
            else transform.Translate(Vector3.forward * Time.deltaTime * Speed);
        }
        else transform.Translate(Vector3.forward * Time.deltaTime * Speed);
    }
    //发射子弹功能
    void FireBullet()
    {
        if ((FileTime -= Time.deltaTime) < 0)
        {
            //创建子弹
            Instantiate(EnemyBullet, transform.position + new Vector3(0, 0, -1.5f), Quaternion.identity);
            GetComponent<AudioSource>().Play();
            FileTime = FileTimeCtrl;
        }
    }
    //敌方飞船爆炸特效
    private void OnDestroy()
    {
        if (flag) Instantiate(VFX, transform.position, Quaternion.identity);
    }
}
