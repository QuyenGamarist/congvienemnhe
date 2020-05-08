using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : SimpleSingleton<Enemy> {
    public GameObject gunEnemy, pointEnemy;
    public Transform player;

    private float angleEnemyGun = 0f;

    private bool isEnemyShoot = false;
    private bool isMissed = false;

    [SerializeField]
    private GameObject bulletEnemy;

    [SerializeField]
    private GameObject burst;

    public GameObject deadParticle, coinParticle, towerHolder;

    public void setPlayer(Transform player)
    {
        this.player = player;
    }

    public void setMissed(bool isMissed)
    {
        this.isMissed = isMissed;
    }
    
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //towerHolder = GameObject.FindGameObjectWithTag("Tower");
    }

	void Update () {
        if (isMissed)
        {
            if (!isEnemyShoot)
            {
                enemyBulletShoot();
            }
        }
    }

    public void rotateEnemyGun()
    {
        float ratio = Mathf.Atan2(player.position.y - gunEnemy.transform.position.y, player.position.x - gunEnemy.transform.position.x);
        angleEnemyGun = 180 + (180 / Mathf.PI * ratio);

        gunEnemy.transform.DORotateQuaternion(Quaternion.Euler(new Vector3(0, 0, angleEnemyGun)), 2f);
    }

    private void enemyBulletShoot()
    {
        
        if (gunEnemy.transform.rotation.eulerAngles.z >= angleEnemyGun - 0.01f)
        {
            print("shoot");
            GameObject.Find("EnemyShootSound").GetComponent<AudioSource>().Play();
            isEnemyShoot = true;

            GameObject bullet = Instantiate(bulletEnemy, pointEnemy.transform.position, Quaternion.identity);

            var direction = player.position - pointEnemy.transform.position;

            burst.GetComponent<ParticleSystem>().Play();

            bullet.GetComponent<Rigidbody2D>().AddForce(direction * 5f, ForceMode2D.Impulse);
            isMissed = false;
        }
    }

    public void killed()
    {
        GameObject obj = Instantiate(deadParticle, transform.position, transform.rotation, towerHolder.transform);
        GameObject coin = Instantiate(coinParticle, transform.position, transform.rotation, towerHolder.transform);

        float t = obj.GetComponent<ParticleSystem>().duration;
        float tCoin = coin.GetComponent<ParticleSystem>().startLifetime;

        Destroy(obj, t);
        Destroy(coin, tCoin);
    }
}
