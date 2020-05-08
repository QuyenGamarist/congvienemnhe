using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gun : SimpleSingleton<Gun> {
    private bool isRotate = false;
    private int i = 0;
    private float zGun = 0;

    public Transform point;
    public GameObject laser;

    public bool isShoot = false;

    public void setShoot(bool isShoot)
    {
        this.isShoot = isShoot;
    }

    public bool getShoot()
    {
        return this.isShoot;
    }

	// Use this for initialization
	void Start () {
        
	}

    public void setZGun(float zGun)
    {
        this.zGun = zGun;
    }

    public void setRotate(bool isRotate)
    {
        this.isRotate = isRotate;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void Shoot()
    {
        if (isRotate)
        {
            DOVirtual.DelayedCall(0.01f, () => abc());
            //StartCoroutine(Shoots());
        }
    }

    void abc()
    {
        i++;
        var direction = point.position - transform.position;
        print(direction);
        zGun = zGun - 0.5f;
        var rotateGun = new Vector3(0, 0, zGun);
        transform.rotation = Quaternion.Euler(rotateGun);

        GameObject obj = Instantiate(laser, point.position, Quaternion.identity);
        obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zGun));
        obj.GetComponent<Laser2>().setIndex(i);
        obj.GetComponent<Laser2>().setDirection(direction);

        if (i < 5)
        {
            DOVirtual.DelayedCall(0.05f, () => abc());
        }
        else
        {
            isShoot = false;
            isRotate = false;
            GameController.Instance.isHole = false;
            GameController.Instance.setShoot3Bullet(false);
            i = 0;

        }
    }

    IEnumerator Shoots()
    {
        yield return new WaitForSeconds(0.1f);
        i++;
        var direction = point.position - transform.position;
        zGun = zGun - 1f;
        var rotateGun = new Vector3(0, 0, zGun);
        transform.rotation = Quaternion.Euler(rotateGun);

        GameObject obj = Instantiate(laser, point.position, Quaternion.identity);
        obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zGun));
        obj.GetComponent<Laser2>().setIndex(i);
        obj.GetComponent<Laser2>().setDirection(direction);
        
        //if (isShoot)
        //{
        //obj.GetComponent<Laser2>().setHit(true);
        //isShoot = false;
        //}else
        //{
        //if(i == 5)
        //{
        //obj.GetComponent<Laser2>().setHit(true);
        //isShoot = false;
        //}
        //}


        if (i < 5)
        {         
            StartCoroutine(Shoots());
        }else
        {
            print(isShoot);
            isShoot = false;
            isRotate = false;
            GameController.Instance.isHole = false;
            GameController.Instance.setShoot3Bullet(false);
            //zGun = 0;
            i = 0;
            
        }
    }
}
