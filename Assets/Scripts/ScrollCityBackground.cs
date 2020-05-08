using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCityBackground : MonoBehaviour {
    private Rigidbody2D mBody;
    public float speed;

	// Use this for initialization
	void Start () {
        mBody = GetComponent<Rigidbody2D>();
	}

    public void moveCity(bool isMove)
    {
        if (isMove)
        {
            mBody.velocity = new Vector2(-speed, 0);
        }else
        {
            mBody.velocity = new Vector2(0, 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
