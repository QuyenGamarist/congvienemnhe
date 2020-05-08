using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLine : MonoBehaviour {
    private GameObject[] citys;
    // Use this for initialization
    void Start () {
        citys = GameObject.FindGameObjectsWithTag("City");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Tower")
        {
            MoveBackground.Instance.setMove(false);
            MoveWall.Instance.setMove(false);

            for (int i = 0; i < citys.Length; i++)
            {
                citys[i].GetComponent<ScrollCityBackground>().moveCity(false);
            }

            MoveTower.Instance.setMove(false);
            Player.Instance.setWalking(false);
            GameController.Instance.setShooting(false);
            MoveTower.Instance.setKinematic(true);
        }
    }
}
