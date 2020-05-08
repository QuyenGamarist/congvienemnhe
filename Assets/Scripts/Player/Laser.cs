using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Laser : MonoBehaviour {
    private Rigidbody2D mBody;
    public float speed = 10;

    private GameObject[] citys;
    private GameObject enemy;
    private float x = 0;
    private bool isShoot = false;
    // Use this for initialization
    void Start () {
        citys = GameObject.FindGameObjectsWithTag("City");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }
	
	// Update is called once per frame
	void Update () {
        Shoot(Vector3.zero);
	}

    public void Shoot(Vector3 direction)
    {
        //GetComponent<Rigidbody2D>().velocity = direction * speed;
        x += 0.5f;
        transform.localScale = new Vector3(x, 0.5f, 1);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy")
        {
            GameObject.Find("KillSound").GetComponent<AudioSource>().Play();
            Destroy(gameObject);
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
            Destroy(enemy);
            enemy.GetComponent<Enemy>().killed();
            if (!isShoot)
            {
                GameController.Instance.addPoint(1);
                GameController.Instance.addCoin(3);
                GameController.Instance.showNotification("+1 PT");
                isShoot = true;
            }
        }

        if (col.tag == "Head")
        {
            GameObject.Find("KillSound").GetComponent<AudioSource>().Play();
            Destroy(gameObject);
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
            Destroy(enemy);
            enemy.GetComponent<Enemy>().killed();
            if (!isShoot)
            {
                GameController.Instance.addPoint(2);
                GameController.Instance.addCoin(6);
                GameController.Instance.showNotification("Headshot +2 PT");
                isShoot = true;
            }        
        }

        if (col.tag == "Road")
        {
            GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
            //Destroy(gameObject);
            enemy.GetComponent<Enemy>().rotateEnemyGun();
            enemy.GetComponent<Enemy>().setMissed(true);
            GameController.Instance.showNotification("Missed");
        }
    }
}
