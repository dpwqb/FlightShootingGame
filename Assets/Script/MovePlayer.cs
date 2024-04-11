using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    //�ƶ��ٶ�
    public float MoveSpeed;
    //�ӵ�����
    public GameObject[] bullet;
    //��������
    public float FileTimeCtrl = 0.5f;
    private float FileTime;
    //��ը��Ч
    public GameObject VFX;
    //�Ƿ��޵�
    public bool Invincible = false;
    //������Ⱦ���Ƿ��
    bool MeshRendererBool = true;
    float BlingTime = 0.1f;
    //С�ɴ�
    public GameObject LittleShip;
    void Start()
    {
        gameObject.name = "playerShip";
    }
    void Update()
    {
        PlayerMove();
        FireBullet(transform.position);
        bling(Invincible);
        Summon();
        ChangeBullet();
    }
    //�����ӵ�
    void FireBullet(Vector3 xz)
    {
        if ((FileTime -= Time.deltaTime) < 0 && Input.GetMouseButton(0))
        {
            //�����ӵ�
            Instantiate(bullet[GameObject.Find("God").GetComponent<GodMake>().WhichBullet], xz + new Vector3(0, 0, 1.5f), Quaternion.identity);
            GetComponent<AudioSource>().Play();
            if (GameObject.Find("God").GetComponent<GodMake>().WhichBullet != 0) FileTime = 1.2f * FileTimeCtrl;
            else FileTime = FileTimeCtrl;
        }
    }
    //����ƶ�
    void PlayerMove()
    {
        //player�ƶ�
        float MoveH = Input.GetAxis("Horizontal");
        float MoveV = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(MoveH, 0, MoveV) * Time.deltaTime * MoveSpeed);

        //�޶�player��Χ
        if (transform.position.x < -5.0f) transform.position = new Vector3(-5.0f, 0, transform.position.z);
        if (transform.position.x > 5.0f) transform.position = new Vector3(5.0f, 0, transform.position.z);
        if (transform.position.z < -2.0f) transform.position = new Vector3(transform.position.x, 0, -2.0f);
        if (transform.position.z > 16.0f) transform.position = new Vector3(transform.position.x, 0, 16.0f);
    }
    //��ը��Ч
    private void OnDestroy()
    {
        Instantiate(VFX, transform.position, Quaternion.identity);
    }
    //�޵�ʱ��˸
    void bling(bool a)
    {
        if (a)
        {
            if (BlingTime > 0)
            {
                BlingTime -= Time.deltaTime;
            }
            else
            {
                BlingTime = 0.1f;
                if (MeshRendererBool)
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    MeshRendererBool = false;
                }
                else
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = true;
                    MeshRendererBool = true;
                }
            }
        }
        else
        {
            if (!MeshRendererBool)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                MeshRendererBool = true;
            }
        }
    }
    //�ٻ�С�ɴ�
    void Summon()
    {
        if (Input.GetMouseButtonDown(1) && GameObject.Find("God").GetComponent<GodMake>().Life > 4 && !GameObject.Find("littleShip(Clone)"))
        {
            GameObject.Find("God").GetComponent<GodMake>().Life -= 4;
            GameObject.Find("God").GetComponent<GodMake>().LifeText();
            Instantiate(LittleShip, transform.position + new Vector3(-1f, 0f, 0f), Quaternion.identity).transform.parent = GameObject.Find("playerShip").transform;
            Instantiate(LittleShip, transform.position + new Vector3(1f, 0f, 0f), Quaternion.identity).transform.parent = GameObject.Find("playerShip").transform;
        }
    }
    //����ѡ��ͬ�ӵ�
    void ChangeBullet()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (GameObject.Find("God").GetComponent<GodMake>().WhichBullet >= bullet.Length-1) GameObject.Find("God").GetComponent<GodMake>().WhichBullet = 0;
            else GameObject.Find("God").GetComponent<GodMake>().WhichBullet++;
            GameObject.Find("God").GetComponent<GodMake>().LifeText();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (GameObject.Find("God").GetComponent<GodMake>().WhichBullet <= 0) GameObject.Find("God").GetComponent<GodMake>().WhichBullet = bullet.Length-1;
            else GameObject.Find("God").GetComponent<GodMake>().WhichBullet--;
            GameObject.Find("God").GetComponent<GodMake>().LifeText();
        }
    }
}
