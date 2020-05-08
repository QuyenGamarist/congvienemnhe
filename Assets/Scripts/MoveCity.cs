using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCity : SimpleSingleton<MoveCity> {

    public float moveSpeed;
    public float moveRange;

    private Vector3 oldPosition;

    private bool isMove = false;

	// Use this for initialization
	void Start () {
        oldPosition = transform.position;
	}

    public void setMove(bool isMove)
    {
        this.isMove = isMove;
    }
	
	// Update is called once per frame
	void Update () {
        //if (isMove)
        //{
            transform.Translate(new Vector2(-0.1f * Time.deltaTime * moveSpeed, 0));

            if (Vector3.Distance(oldPosition, transform.position) > moveRange)
            {
                transform.position = oldPosition;
            }
        //}
	}
}
