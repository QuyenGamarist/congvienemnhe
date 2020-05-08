using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SimpleSingleton<Player> {
    private Animator anim;

    public Transform head, gun, lineDirection;

	void Start () {
        anim = GetComponent<Animator>();
	}

    public void setWalking(bool isWalking)
    {
        anim.SetBool("Walk", isWalking);
    }

    public void setIdle()
    {
        head.rotation = Quaternion.Euler(Vector3.zero);
        gun.rotation = Quaternion.Euler(Vector3.zero);
        lineDirection.rotation = Quaternion.Euler(new Vector3(0, 0, -90f));
    }

	void Update () {
		
	}
}
