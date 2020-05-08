using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameController : SimpleSingleton<GameController> {
    public float zGun = 0f;
    public float zLine = -90f;
    public float speedRotate = 2f;
    private float angleEnemyGun = 0f;

    public int whichWeapon = 1;

    public int currentCoin;
    public Text txtCoin;

    private bool isReady = false;
    public GameObject panelMenu;
    public Text txtBestScoreMenu;

    public GameObject panelPoint, panelNotification;
    public Text txtPoint, txtNotification;
    private int numPoint = 0;

    public GameObject panelGameOver;
    public Text txtScore, txtBestScore;

    public Transform gun, head, body, footL, footR, lineDirection, point, player;
    public Transform gunEnemy, pointEnemy;

    [SerializeField]
    private GameObject rocket, laser;
    [SerializeField]
    private GameObject bulletEnemy;
    [SerializeField]
    private GameObject skinObj;

    private Vector3 rotateGun, rotateHead = Vector3.zero;

    public bool isHole = false;
    private bool isShoot = false;
    private bool isEnemyShoot = false;
    private bool isMissed = false;
    private bool isShoot3Bullet = false;

    private int numShoot = 0;

	public void setShooting(bool isShoot)
    {
        this.isShoot = isShoot;
    }

    public void setMissed(bool isMissed)
    {
        this.isMissed = isMissed;
    }

    public void setShoot3Bullet(bool isShoot3Bullet)
    {
        this.isShoot3Bullet = isShoot3Bullet;
    }

    void Awake()
    {
        //PlayerPrefs.DeleteKey("hasSkin");
        //PlayerPrefs.DeleteKey("skin");
        //PlayerPrefs.DeleteKey("coin");
        //PlayerPrefs.Save();
        if (!PlayerPrefs.HasKey("score"))
        {
            PlayerPrefs.SetInt("score", 0);
            PlayerPrefs.Save();
        }

        if (!PlayerPrefs.HasKey("coin"))
        {
            PlayerPrefs.SetInt("coin", 500);
            PlayerPrefs.Save();
        }

        if (!PlayerPrefs.HasKey("hasSkin"))
        {
            PlayerPrefs.SetString("hasSkin", "0");
            PlayerPrefs.Save();
        }

        if (!PlayerPrefs.HasKey("skin"))
        {
            PlayerPrefs.SetInt("skin", 1);
            PlayerPrefs.Save();
        }else
        {
            whichWeapon = PlayerPrefs.GetInt("skin");
        }

        currentCoin = PlayerPrefs.GetInt("coin");
        txtCoin.text = currentCoin.ToString();
    }

    void Start()
    {
        getSkin();
        getBestScoreMenu();
    }

	void Update () {
        if (!isReady)
        {
            Time.timeScale = 0f;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isReady)
            {
                if (!isShoot)
                {
                    isHole = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!isReady)
            {
                Time.timeScale = 1f;
                panelMenu.SetActive(false);
                panelPoint.SetActive(true);
                isReady = true;
            }

            if (isHole)
            {
                isShoot = true;
                var direction = point.position - gun.position;
                if (whichWeapon == 1)
                {
                    GameObject.Find("ShootSound").GetComponent<AudioSource>().Play();

                    GameObject obj = Instantiate(rocket, point.position, Quaternion.identity);

                    obj.GetComponent<Rocket>().setDirection(direction);
                    obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zGun));
                    obj.GetComponent<Rigidbody2D>().AddForce(direction * 13f, ForceMode2D.Impulse);
                    isHole = false;
                }

                if(whichWeapon == 2)
                {
                    GameObject.Find("LaserSound").GetComponent<AudioSource>().Play();
                    GameObject obj = Instantiate(laser, point.position, Quaternion.identity);
                    obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zGun));
                    //obj.GetComponent<Laser>().Shoot(direction);   
                    isHole = false;
                }

                if(whichWeapon == 3)
                {
                    //rotateGun = new Vector3(0, 0, zGun);
                    //gun.rotation = Quaternion.Euler(rotateGun);
                    //direction = point.position - gun.position;
                    //zGun = zGun - 5f;

                    //GameObject obj = Instantiate(laser, point.position, Quaternion.identity);
                    //obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zGun));
                    if (!isShoot3Bullet)
                    {
                        Gun.Instance.setRotate(true);
                        Gun.Instance.setZGun(zGun);
                        Gun.Instance.Shoot();
                        isShoot3Bullet = true;
                    }
                    //isHole = false;
                }

                //isHole = false;
            }   
        }

        if (!isShoot)
        {
            if (isHole)
            {
                if (zGun < 90)
                {
                    if (whichWeapon == 3)
                    {
                        lineDirection.gameObject.SetActive(false);
                    }
                    else
                    {
                        lineDirection.gameObject.SetActive(true);
                    }
                    zGun += (speedRotate);
                    zLine = zGun - 90f;

                    rotateGun = new Vector3(0, 0, zGun);
                    gun.rotation = Quaternion.Euler(rotateGun);

                    lineDirection.position = gun.position;
                    lineDirection.rotation = Quaternion.Euler(new Vector3(0, 0, zLine));

                    if (zGun < 15)
                    {
                        rotateHead = new Vector3(0, 0, zGun);
                        head.rotation = Quaternion.Euler(rotateHead);
                    }
                }
            }
        }

        if (isShoot)
        {
            if (!isHole)
            {
                rotateGun = Vector3.zero;
                rotateHead = Vector3.zero;
                lineDirection.gameObject.SetActive(false);
                zGun = 0f;
            }
        }
    }

    public void addPoint(int point)
    {
        numPoint+= point;
        txtPoint.text = numPoint.ToString();
    }

    public void addCoin(int coin)
    {
        currentCoin+= coin;
        txtCoin.text = currentCoin.ToString();
    }

    public void saveCoin()
    {
        PlayerPrefs.SetInt("coin", currentCoin);
        PlayerPrefs.Save();
    }

    public void GameOver()
    {
        panelPoint.SetActive(false);
        panelGameOver.SetActive(true);
        txtScore.text = numPoint.ToString();
        txtBestScore.text = "BEST " + PlayerPrefs.GetInt("score").ToString();

        gameObject.GetComponent<AudioSource>().Stop();
        GameObject.Find("GameOverSound").GetComponent<AudioSource>().Play();
    }

    public void Replay()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void saveScore()
    {
        int bestScore = PlayerPrefs.GetInt("score");

        if (numPoint > bestScore)
        {
            PlayerPrefs.SetInt("score", numPoint);
            PlayerPrefs.Save();
        }
    }

    public void showNotification(string text)
    {
        panelNotification.SetActive(true);
        txtNotification.text = text;
        StartCoroutine(hideNotification());
    }

    IEnumerator hideNotification()
    {
        yield return new WaitForSeconds(1f);
        panelNotification.SetActive(false);
    }

    private void getSkin()
    {
        if(whichWeapon == 1)
        {
            //head.GetComponent<SpriteRenderer>().sprite
            Transform skin = skinObj.transform.Find("Skin1");
            Sprite headSpr = skin.Find("Head").GetComponent<SpriteRenderer>().sprite;
            Sprite weaponSpr = skin.Find("Weapon").GetComponent<SpriteRenderer>().sprite;
            Sprite footLSpr = skin.Find("FootL").GetComponent<SpriteRenderer>().sprite;
            Sprite footRSpr = skin.Find("FootR").GetComponent<SpriteRenderer>().sprite;
            Sprite bodySpr = skin.Find("Body").GetComponent<SpriteRenderer>().sprite;

            head.GetComponent<SpriteRenderer>().sprite = headSpr;
            gun.GetComponent<SpriteRenderer>().sprite = weaponSpr;
            footL.GetComponent<SpriteRenderer>().sprite = footLSpr;
            footR.GetComponent<SpriteRenderer>().sprite = footRSpr;
            body.GetComponent<SpriteRenderer>().sprite = bodySpr;
        }

        if(whichWeapon == 2)
        {
            Transform skin = skinObj.transform.Find("Skin2");
            Sprite headSpr = skin.Find("Head").GetComponent<SpriteRenderer>().sprite;
            Sprite weaponSpr = skin.Find("Weapon").GetComponent<SpriteRenderer>().sprite;
            Sprite footLSpr = skin.Find("FootL").GetComponent<SpriteRenderer>().sprite;
            Sprite footRSpr = skin.Find("FootR").GetComponent<SpriteRenderer>().sprite;
            Sprite bodySpr = skin.Find("Body").GetComponent<SpriteRenderer>().sprite;

            head.GetComponent<SpriteRenderer>().sprite = headSpr;
            gun.GetComponent<SpriteRenderer>().sprite = weaponSpr;
            footL.GetComponent<SpriteRenderer>().sprite = footLSpr;
            footR.GetComponent<SpriteRenderer>().sprite = footRSpr;
            body.GetComponent<SpriteRenderer>().sprite = bodySpr;
        }

        if (whichWeapon == 3)
        {
            Transform skin = skinObj.transform.Find("Skin3");
            Sprite headSpr = skin.Find("Head").GetComponent<SpriteRenderer>().sprite;
            Sprite weaponSpr = skin.Find("Weapon").GetComponent<SpriteRenderer>().sprite;
            Sprite footLSpr = skin.Find("FootL").GetComponent<SpriteRenderer>().sprite;
            Sprite footRSpr = skin.Find("FootR").GetComponent<SpriteRenderer>().sprite;
            Sprite bodySpr = skin.Find("Body").GetComponent<SpriteRenderer>().sprite;

            head.GetComponent<SpriteRenderer>().sprite = headSpr;
            gun.GetComponent<SpriteRenderer>().sprite = weaponSpr;
            footL.GetComponent<SpriteRenderer>().sprite = footLSpr;
            footR.GetComponent<SpriteRenderer>().sprite = footRSpr;
            body.GetComponent<SpriteRenderer>().sprite = bodySpr;
        }
    }

    private void getBestScoreMenu()
    {
        int bestScore = PlayerPrefs.GetInt("score");
        txtBestScoreMenu.text = "Best " + bestScore.ToString();
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }
}
