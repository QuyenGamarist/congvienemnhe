using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveTower : SimpleSingleton<MoveTower> {
    public float moveSpeed;
    public float oldPositionX;
    public float minY, maxY;

    public Transform targetLine;
    private bool isMove = false;
    public bool isMaxY = false;

    // Use this for initialization
    void Start()
    {
        
    }

    public void setMove(bool isMove)
    {
        this.isMove = isMove;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            transform.Translate(new Vector2(-0.1f * Time.deltaTime * moveSpeed, 0));
        }else
        {
            if (!isMaxY)
            {
                float y = transform.position.y;
                y += 0.3f * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, y, transform.position.z);

                if(transform.position.y >= maxY)
                    isMaxY = true;
            }else
            {
                float y = transform.position.y;
                y -= 0.3f * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
                if (transform.position.y <= minY + 0.2f)
                    isMaxY = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //if(col.collider.tag == "Line")
        //{
        //    print("col");
        //    transform.position = new Vector3(oldPositionX, Random.Range(minY, maxY), 0);
        //    targetLine.position = new Vector3(Random.Range(1f, 2f), 0, 0);
        //}

        if(col.collider.tag == "Line")
        {
            Destroy(gameObject);
        }
    }

    public void setKinematic(bool isKinematic)
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = isKinematic;
    }
}
