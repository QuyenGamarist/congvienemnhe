using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : SimpleSingleton<BulletEnemy> {
    private Rigidbody2D mBody;
    private Vector3 direction;

    public GameObject deadParticle;

    private GameObject player;

    public void setDirection(Vector3 direction)
    {
        this.direction = direction;
    }

	void Start () {
        mBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.tag == "Player")
        {
            print("destroy");
            GameObject.Find("DieSound").GetComponent<AudioSource>().Play();
            Destroy(gameObject);
            GameObject obj = Instantiate(deadParticle, player.transform.position, player.transform.rotation);
            Destroy(player);
            Destroy(obj, obj.GetComponent<ParticleSystem>().duration);
            GameController.Instance.saveScore();
            GameController.Instance.saveCoin();
            GameController.Instance.GameOver();
        }
    }
}
