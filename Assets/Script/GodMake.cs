using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GodMake : MonoBehaviour
{
    //开始游戏flag
    bool BeginFlag = false;
    //游戏难度
    public float Dfcty = 1;
    //光
    public GameObject Sun;
    //获取玩家飞船预制体
    public GameObject PlayerPrefab;
    //获取玩家飞船
    public GameObject Player;
    //生成的敌人数组
    public GameObject[] enemy;
    //生成物体的周期
    public float FileTimeCtrl = 0.5f;
    private float FileTime;
    //玩家存活时长
    public float GameTime;
    //分数编辑
    private int Score;
    public Text ScoreText;
    public Text GameOver;
    public Text Regame;
    public Text GamePlay;
    public Button BeginButton;
    public Button ExitButton;
    public Button PlayWayButton;
    public Button BackButton;
    public Button EasyButton;
    public Button DifficultButton;
    //剩余生命
    [Range(0, 6)]
    public int Life = 3;
    //Which bullet ？
    public int WhichBullet;
    //无敌时间
    float Invincible;
    //玩家Collider是否开启
    bool ColliderBool=true;
    void Start()
    {

    }
    void Update()
    {
        if (BeginFlag)
        {
            PlayerLife();
            ColliderCheck();
            if (Input.GetKey("escape")) Application.Quit();
        }
    }
    //加分机制
    public void AddScore(string DesTag)
    {
        if (DesTag == "EB") return;
        if (DesTag == "Finish")
        {
            Score += 1;
            ScoreText.text = "Score:" + Score.ToString();
            return;
        }
        if (DesTag == "ES")
        {
            Score += 5;
            ScoreText.text = "Score:" + Score.ToString();
            return;
        }
    }
    //玩家存在则创建敌人，不存在则减命重开
    void PlayerLife()
    {
        if (GameObject.Find("playerShip"))
        {
            //玩家存活时长
            GameTime += Time.deltaTime;
            if ((FileTime -= Time.deltaTime) < 0)
            {
                //创建陨石
                Instantiate(enemy[Random.Range(0, enemy.Length)], new Vector3(Random.Range(-5.0f, 5.0f), 0, 18.0f), Quaternion.identity);
                FileTime = FileTimeCtrl;
                //生成敌人的周期随时间递减
                if (Dfcty == 1) FileTimeCtrl = 10f / (GameTime / 3f + 20f) + 0.1f;
                else FileTimeCtrl = 10f / (GameTime / 3f + 20f);
            }
        }
        else
        {
            if (Life > 1)
            {
                Life--;
                LifeText();
                Resurrected();
            }
            else
            {
                //显示鼠标
                Cursor.lockState = CursorLockMode.None;
                Life = 0;
                GameOver.gameObject.SetActive(true);
                ExitButton.gameObject.SetActive(true);
                Regame.text = "按下R键重新开始\n存活时长："+ ((int)GameTime).ToString()+"秒";
                if (Input.GetButtonDown("Restart")) SceneManager.LoadScene(0);
            }
        }
    }
    //判断玩家是否开启碰撞器
    void ColliderCheck()
    {
        if (Invincible > 0)
        {
            Invincible -= Time.deltaTime;
        }
        else
        {
            if (!ColliderBool)
            {
                GameObject.Find("playerShip").GetComponent<Collider>().enabled = true;
                ColliderBool = true;
                GameObject.Find("playerShip").GetComponent<MovePlayer>().Invincible = false;
            }
        }
    }
    //复活时
    void Resurrected()
    {
        Instantiate(PlayerPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        GameObject.Find("playerShip(Clone)").GetComponent<Collider>().enabled = false;
        Invincible = 3.0f;
        ColliderBool = false;
        GameObject.Find("playerShip(Clone)").GetComponent<MovePlayer>().Invincible = true;
    }
    //子弹名称
    public string BulletName(int WB)
    {
        switch (WB)
        {
            case 0:
                return "\n武器：普通激光弹";
            case 1:
                return "\n武器：制导火箭弹";
            case 2:
                return "\n武器：能量小弹球";
            default:
                return "\n武器：NULL";
        }
    }
    //更新剩余生命文字
    public void LifeText()
    {
        Regame.text = "剩余生命";
        for (int i = 0; i < Life; i++) Regame.text += "♥";
        Regame.text += BulletName(WhichBullet);
    }
    //开始游戏
    public void Begin()
    {
        EasyButton.gameObject.SetActive(true);
        DifficultButton.gameObject.SetActive(true);
        PlayWayButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
        BeginButton.gameObject.SetActive(false);
    }
    //游戏玩法
    public void PlayWay()
    {
        PlayWayButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
        BeginButton.gameObject.SetActive(false);
        GamePlay.gameObject.SetActive(true);
        BackButton.gameObject.SetActive(true);
    }
    //返回目录
    public void Back()
    {
        PlayWayButton.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(true);
        BeginButton.gameObject.SetActive(true);
        GamePlay.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
    }
    //退出游戏
    public void Exit()
    {
        Application.Quit();
    }
    //简单模式
    public void Easy()
    {
        ScoreText.gameObject.SetActive(true);
        Player.gameObject.SetActive(true);
        LifeText();
        //隐藏并固定鼠标
        Cursor.lockState = CursorLockMode.Locked;
        BeginFlag = true;
        DifficultButton.gameObject.SetActive(false);
        EasyButton.gameObject.SetActive(false);
    }
    //困难模式
    public void Difficult()
    {
        ScoreText.gameObject.SetActive(true);
        Player.gameObject.SetActive(true);
        LifeText();
        //隐藏并固定鼠标
        Cursor.lockState = CursorLockMode.Locked;
        BeginFlag = true;
        Dfcty = 0.2f;
        Sun.gameObject.GetComponent<Light>().intensity = 0.5f;
        EasyButton.gameObject.SetActive(false);
        DifficultButton.gameObject.SetActive(false);
    }
}
