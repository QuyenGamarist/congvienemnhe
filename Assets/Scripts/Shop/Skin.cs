using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : SimpleSingleton<Skin> {
    public GameObject head, weapon, body, footL, footR;
    public GameObject skinObj;
    public int price = 0;

    private int whichWeapon = 1;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void getSkin()
    {
        if (whichWeapon == 1)
        {
            //head.GetComponent<SpriteRenderer>().sprite
            Transform skin = skinObj.transform.Find("Skin1");
            Sprite headSpr = skin.Find("Head").GetComponent<SpriteRenderer>().sprite;
            Sprite weaponSpr = skin.Find("Weapon").GetComponent<SpriteRenderer>().sprite;
            Sprite footLSpr = skin.Find("FootL").GetComponent<SpriteRenderer>().sprite;
            Sprite footRSpr = skin.Find("FootR").GetComponent<SpriteRenderer>().sprite;
            Sprite bodySpr = skin.Find("Body").GetComponent<SpriteRenderer>().sprite;

            head.GetComponent<SpriteRenderer>().sprite = headSpr;
            weapon.GetComponent<SpriteRenderer>().sprite = weaponSpr;
            footL.GetComponent<SpriteRenderer>().sprite = footLSpr;
            footR.GetComponent<SpriteRenderer>().sprite = footRSpr;
            body.GetComponent<SpriteRenderer>().sprite = bodySpr;
        }

        if (whichWeapon == 2)
        {
            Transform skin = skinObj.transform.Find("Skin2");
            Sprite headSpr = skin.Find("Head").GetComponent<SpriteRenderer>().sprite;
            Sprite weaponSpr = skin.Find("Weapon").GetComponent<SpriteRenderer>().sprite;
            Sprite footLSpr = skin.Find("FootL").GetComponent<SpriteRenderer>().sprite;
            Sprite footRSpr = skin.Find("FootR").GetComponent<SpriteRenderer>().sprite;
            Sprite bodySpr = skin.Find("Body").GetComponent<SpriteRenderer>().sprite;

            head.GetComponent<SpriteRenderer>().sprite = headSpr;
            weapon.GetComponent<SpriteRenderer>().sprite = weaponSpr;
            footL.GetComponent<SpriteRenderer>().sprite = footLSpr;
            footR.GetComponent<SpriteRenderer>().sprite = footRSpr;
            body.GetComponent<SpriteRenderer>().sprite = bodySpr;

            price = 400;
        }

        if(whichWeapon == 3)
        {
            Transform skin = skinObj.transform.Find("Skin3");
            Sprite headSpr = skin.Find("Head").GetComponent<SpriteRenderer>().sprite;
            Sprite weaponSpr = skin.Find("Weapon").GetComponent<SpriteRenderer>().sprite;
            Sprite footLSpr = skin.Find("FootL").GetComponent<SpriteRenderer>().sprite;
            Sprite footRSpr = skin.Find("FootR").GetComponent<SpriteRenderer>().sprite;
            Sprite bodySpr = skin.Find("Body").GetComponent<SpriteRenderer>().sprite;

            head.GetComponent<SpriteRenderer>().sprite = headSpr;
            weapon.GetComponent<SpriteRenderer>().sprite = weaponSpr;
            footL.GetComponent<SpriteRenderer>().sprite = footLSpr;
            footR.GetComponent<SpriteRenderer>().sprite = footRSpr;
            body.GetComponent<SpriteRenderer>().sprite = bodySpr;

            price = 100;
        }
    }

    public void changeSkin(int position)
    {
        this.whichWeapon = position;
        getSkin();
    }
}
