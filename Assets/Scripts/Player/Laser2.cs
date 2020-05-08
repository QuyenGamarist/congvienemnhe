using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser2 : MonoBehaviour {
    private Rigidbody2D mBody;
    public float speed = 10;

    private GameObject[] citys;
    private GameObject enemy;
    private float x = 0;
    private bool isShoot = false;

    private int index = 0;
    private Vector3 direction;

    private bool isHit = false;

    public void setHit(bool isHit)
    {
        this.isHit = isHit;
    }

    public void setIndex(int index)
    {
        this.index = index;
    }

    public void setDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    // Use this for initialization
    void Start()
    {
        citys = GameObject.FindGameObjectsWithTag("City");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        GameObject.Find("GatlingGunSound").GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot(direction);
    }

    public void Shoot(Vector3 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction * speed;
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            GameObject.Find("KillSound").GetComponent<AudioSource>().Play();
            Destroy(gameObject);
            if (index >= 5)
            {
                MoveBackground.Instance.setMove(true);
                MoveWall.Instance.setMove(true);

                for (int i = 0; i < citys.Length; i++)
                {
                    citys[i].GetComponent<ScrollCityBackground>().moveCity(true);
                }

                MoveTower.Instance.setMove(true);
                Player.Instance.setWalking(true);
                Player.Instance.setIdle();
                MoveTower.Instance.setKinematic(false);
            }
            Destroy(enemy);
            enemy.GetComponent<Enemy>().killed();
            if (!isShoot)
            {
                GameController.Instance.addPoint(1);
                GameController.Instance.addCoin(3);
                GameController.Instance.showNotification("+1 PT");
                isShoot = true;
                Gun.Instance.setShoot(true);
                print(Gun.Instance.getShoot());
            }
            
        }

        if (col.tag == "Head")
        {
            GameObject.Find("KillSound").GetComponent<AudioSource>().Play();
            Destroy(gameObject);
            if (index >= 5)
            {
                MoveBackground.Instance.setMove(true);
                MoveWall.Instance.setMove(true);

                for (int i = 0; i < citys.Length; i++)
                {
                    citys[i].GetComponent<ScrollCityBackground>().moveCity(true);
                }

                MoveTower.Instance.setMove(true);
                Player.Instance.setWalking(true);
                Player.Instance.setIdle();
                MoveTower.Instance.setKinematic(false);
            }
            Destroy(enemy);
            enemy.GetComponent<Enemy>().killed();
            if (!isShoot)
            {
                GameController.Instance.addPoint(2);
                GameController.Instance.addCoin(6);
                GameController.Instance.showNotification("Headshot +2 PT");
                isShoot = true;
                Gun.Instance.setShoot(true);
                //print(Gun.Instance.getShoot());
            }
            
        }

        if (col.tag == "Road")
        {
            Destroy(gameObject);
            if (index >= 5)
            {
                //print(Gun.Instance.getShoot());
                if (!Gun.Instance.getShoot() && enemy != null)
                {
                    enemy.GetComponent<Enemy>().rotateEnemyGun();
                    enemy.GetComponent<Enemy>().setMissed(true);
                    GameController.Instance.showNotification("Missed");
                    
                    Gun.Instance.setShoot(false);
                }else
                {
                    MoveBackground.Instance.setMove(true);
                    MoveWall.Instance.setMove(true);

                    for (int i = 0; i < citys.Length; i++)
                    {
                        citys[i].GetComponent<ScrollCityBackground>().moveCity(true);
                    }

                    MoveTower.Instance.setMove(true);
                    Player.Instance.setWalking(true);
                    Player.Instance.setIdle();
                    MoveTower.Instance.setKinematic(false);
                }
            }
        }
    }
}
