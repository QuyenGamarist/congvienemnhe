using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {
    public Transform targetLine;
    public GameObject tower;

    public float oldPositionX;
    public float minY, maxY;
    public float minXTargetLine, maxXTargetLine;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.tag == "Tower")
        {
            targetLine.position = new Vector3(Random.Range(minXTargetLine, maxXTargetLine), 0, 0);
            GameObject obj = Instantiate(tower, new Vector3(4f, Random.Range(minY, maxY), 0), Quaternion.identity);
            obj.GetComponent<MoveTower>().setMove(true);
        }
    }
}
