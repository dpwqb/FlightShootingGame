using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public float Speed;
    float x;

    void Start()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
        if(Random.Range(-1,1)==0)
        {
            x = -1;
            transform.position = new Vector3(-8, 0, 18);
        }
        else
        {
            x = 1;
            transform.position = new Vector3(8, 0, 18);
        }
    }
    void Update()
    {
        transform.Translate(new Vector3(x,0,1) * Time.deltaTime * Speed);
        if (transform.position.z < 4.0f) Destroy(gameObject);
    }
    //与玩家标签的物体相撞则＋♥
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && GameObject.Find("God").GetComponent<GodMake>().Life < 6)
        {
            GameObject.Find("God").GetComponent<GodMake>().Life++;
            GameObject.Find("God").GetComponent<GodMake>().Regame.text = "剩余生命";
            GameObject.Find("God").GetComponent<GodMake>().LifeText();
        }
        Destroy(gameObject);
    }
}
