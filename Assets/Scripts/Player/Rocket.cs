using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : SimpleSingleton<Rocket> {
    public float speed;
    private Rigidbody2D mBody;

    private Vector3 direction;

    private GameObject enemy;
    private GameObject[] citys;

    public GameObject deadParticle, coinParticle, towerHolder;

	void Awake()
    {
        mBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        citys = GameObject.FindGameObjectsWithTag("City");
        towerHolder = GameObject.FindGameObjectWithTag("Tower");
    }

    public void setDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    // Update is called once per frame
    void Update () {
        var angle = Vector2.Angle(Vector2.right, mBody.velocity);
        angle = mBody.velocity.y <= 0 ? -angle : angle;
         transform.eulerAngles = new Vector3(0, 0, angle);
        //mBody.velocity = direction;
	}

    private void killed()
    {
        GameObject obj = Instantiate(deadParticle, enemy.transform.position, enemy.transform.rotation, towerHolder.transform);
        GameObject coin = Instantiate(coinParticle, enemy.transform.position, enemy.transform.rotation, towerHolder.transform);

        float t = obj.GetComponent<ParticleSystem>().duration;
        float tCoin = coin.GetComponent<ParticleSystem>().startLifetime;

        Destroy(obj, t);
        Destroy(coin, tCoin);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.tag == "Enemy")
        {
            GameObject.Find("KillSound").GetComponent<AudioSource>().Play();
            Destroy(gameObject);
            MoveBackground.Instance.setMove(true);
            MoveWall.Instance.setMove(true);
            
            for(int i = 0; i < citys.Length; i++)
            {
                citys[i].GetComponent<ScrollCityBackground>().moveCity(true);
            }

            MoveTower.Instance.setMove(true);
            Player.Instance.setWalking(true);
            Player.Instance.setIdle();
            MoveTower.Instance.setKinematic(false);
            Destroy(enemy);
            enemy.GetComponent<Enemy>().killed();
            GameController.Instance.addPoint(1);
            GameController.Instance.addCoin(3);
            GameController.Instance.showNotification("+1 PT");
        }

        if (col.collider.tag == "Head")
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
            GameController.Instance.addPoint(2);
            GameController.Instance.addCoin(6);
            GameController.Instance.showNotification("Headshot +2 PT");
        }

        if (col.collider.tag == "Road")
        {
            Destroy(gameObject);
            //GameController.Instance.rotateEnemyGun();
            //Enemy.Instance.rotateEnemyGun();
            //Enemy.Instance.setMissed(true);
            enemy.GetComponent<Enemy>().rotateEnemyGun();
            enemy.GetComponent<Enemy>().setMissed(true);
            GameController.Instance.showNotification("Missed");
        }

        if(col.collider.tag == "Tower")
        {
            print("touch tower");
            Destroy(gameObject);
            //Enemy.Instance.rotateEnemyGun();
            //Enemy.Instance.setMissed(true);

            enemy.GetComponent<Enemy>().rotateEnemyGun();
            enemy.GetComponent<Enemy>().setMissed(true);
            GameController.Instance.showNotification("Missed");
        }
    }

}
