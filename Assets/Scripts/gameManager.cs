using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class gameManager : MonoBehaviour
{
    public Text timeTxt;
    public GameObject endTxt;
    public GameObject card;
    float time;
    public static gameManager I;
    public GameObject firstCard;
    public GameObject secondCard;
    int cardsLeft = 0;
    public AudioClip match;
    public AudioSource audioSource;

    void Awake()
    {
        I = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        for (int i = 0; i < 16; i++)
        {
            
            GameObject newCard = Instantiate(card);
            //newCard�� cards�� �Ű���!
            newCard.transform.parent = GameObject.Find("cards").transform;
            // (0,1,2,3) ,(0+4,1+4,2+4,3+4), (0+8,1+8,2+8,3+8), (0+12,1+12,2+12,3+12)

            float x = (i / 4) * 1.4f - 2.1f;
            float y = (i % 4) * 1.4f - 3.0f;
            newCard.transform.position = new Vector3(x, y, 0);

            string rtanName = "rtan" + rtans[i].ToString();
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);

        }
        cardsLeft = GameObject.Find("cards").transform.childCount;


    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time > 30.0f)
        {
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
        }

    }
    
    public void isMatched()
    {
       
        // ���⼭ �����ΰ� �Ǵ���
        // FirstCard �� secondCard ������
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if(firstCardImage == secondCardImage)
        {
            audioSource.PlayOneShot(match);
            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();
            cardsLeft -= 2;

            // if(cardsLeft == 2)
            // cardsLeft = GameObject.Find("cards").transform.childCount;������
            // ������ ī�尡 ������� ���� ������ ��!�� �ߴ� ���� �߻�
            // int cardsLeft = 0; �߰�
            // cardsLeft = GameObject.Find("cards").transform.childCount; �� ī�� �����ɶ��� �̵���Ŵ
            // if(cardsLeft == 0) ���� ���� �� �ذ�
            
            if (cardsLeft == 0)
            {
                // �����Ű��!!
                // Time.timeScale = 0f;
                //endTxt.SetActive(true);
                Invoke("GameEnd", 1f);
            }
        }
        else
        {
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();
        }

        firstCard = null;
        secondCard = null;
    }

    void GameEnd()
    {
        Time.timeScale = 0f;
        endTxt.SetActive(true);
    }

}
