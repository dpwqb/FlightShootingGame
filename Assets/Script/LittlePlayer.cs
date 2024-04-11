using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittlePlayer : MonoBehaviour
{
    //开火功能
    public GameObject bullet;
    private float FileTime;
    void Update()
    {
        FireBullet(transform.position);
    }
    //小飞船创建子弹功能
    void FireBullet(Vector3 xz)
    {
        if ((FileTime -= Time.deltaTime) < 0 && Input.GetMouseButton(0))
        {
            //创建子弹
            Instantiate(bullet, xz + new Vector3(0, 0, 0.6f), Quaternion.identity);
            FileTime = GameObject.Find("playerShip").GetComponent<MovePlayer>().FileTimeCtrl;
        }
    }
}
