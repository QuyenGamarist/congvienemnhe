using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour {

    public RectTransform panel;
    public Button[] skin;
    public RectTransform center;

    public Text txtCoin, txtPrice;
    public Button btnSelect, btnPrice;

    private float[] distance;
    private bool dragging = false;
    private int btnDistance;
    private int minButtonNum;
    private float lerpSpeed = 5f;

    public int StartButton = 1;
    private bool sendMessage = false;
    private bool targetNearestButton = false;

    private int coin;
    private List<int> listSkinHas = new List<int>();

    void Start()
    {

        int btnLength = skin.Length;
        distance = new float[btnLength];
        btnDistance = (int)Mathf.Abs(skin[1].GetComponent<RectTransform>().anchoredPosition.x - skin[0].GetComponent<RectTransform>().anchoredPosition.x);

        //panel.anchoredPosition = new Vector2((StartButton - 1) * -330, 0f);

        minButtonNum = PlayerPrefs.GetInt("skin") - 1;
        changeSkin(minButtonNum);
        checkHasSkin(minButtonNum);

        coin = PlayerPrefs.GetInt("coin");
        txtCoin.text = coin.ToString();
    }

    void Update()
    {
        for(int i = 0; i < skin.Length; i++)
        {
            distance[i] = Mathf.Abs(center.transform.position.x - skin[i].transform.position.x);
        }

        if (targetNearestButton)
        {
            float minDistance = Mathf.Min(distance);

            for (int i = 0; i < skin.Length; i++)
            {
                if (minDistance == distance[i])
                {
                    minButtonNum = i;
                }
            }
        }

        if (!dragging)
        {
            LerpToSkin(minButtonNum * -btnDistance);
        }
    }

    void LerpToSkin(int position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * lerpSpeed);

        if(Mathf.Abs(position - newX) < 3f)
        {
            newX = position;
        }

        if(Mathf.Abs(newX) >= Mathf.Abs(position) - 1 && Mathf.Abs(newX) <= Mathf.Abs(position) + 1 && !sendMessage)
        {
            sendMessage = true;
            //changeSkin(minButtonNum);
        }

        Vector2 newPos = new Vector2(newX, panel.anchoredPosition.y);

        panel.anchoredPosition = newPos;
    }

    public void StartDrag()
    {
        sendMessage = false;
        lerpSpeed = 5f;
        dragging = true;
        targetNearestButton = true;
    }

    public void EndDrag()
    {
        dragging = false;
    }

    private void changeSkin(int position)
    {
        Skin.Instance.changeSkin(position + 1);
    }

    public void GoToButton(int position)
    {
        targetNearestButton = false;
        minButtonNum = position;
        changeSkin(position);
        txtPrice.text = Skin.Instance.price.ToString(); 

        if (coin >= Skin.Instance.price)
        {
            btnPrice.interactable = true;
        }else
        {
            btnPrice.interactable = false;
        }
        checkHasSkin(position);
    }

    private void checkHasSkin(int position)
    {
        listSkinHas = new List<int>();
        string hasSkinStr = PlayerPrefs.GetString("hasSkin");
        string[] skins = hasSkinStr.Split(',');

        for (int i = 0; i < skins.Length; i++)
        {
            print(int.Parse(skins[i]));
            listSkinHas.Add(int.Parse(skins[i]));
        }

        for (int i = 0; i < listSkinHas.Count; i++)
        {
            if (listSkinHas[i] == position)
            {
                btnSelect.gameObject.SetActive(true);
                btnPrice.gameObject.SetActive(false);
                return;
            }
            else
            {
                btnSelect.gameObject.SetActive(false);
                btnPrice.gameObject.SetActive(true);
            }
        }
    }

    public void ChooseSkin()
    {
        //GameController.Instance.whichWeapon = minButtonNum;
        PlayerPrefs.SetInt("skin", minButtonNum + 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GamePlay");
    }

    public void BuySkin()
    {
        string s = "";
        for(int i = 0; i < listSkinHas.Count; i++)
        {
            s = s + listSkinHas[i] + ",";
        }
        print(s);
        if (coin >= Skin.Instance.price)
        {
            s = s + minButtonNum;
            
            coin = coin - Skin.Instance.price;
            txtCoin.text = coin.ToString();

            PlayerPrefs.SetInt("coin", coin);
            PlayerPrefs.SetString("hasSkin", s);
            PlayerPrefs.Save();

            checkHasSkin(minButtonNum);
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
