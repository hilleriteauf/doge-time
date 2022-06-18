using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combo : MonoBehaviour
{
    [SerializeField]
    public Sprite[] digits;

    [SerializeField]
    private Image[] characters;

    private int scoreAmount;

    private int numberOfDigitsInScoreAmount;

    
    public GameObject gradin;
    public GameObject publicSprite;
    private SpriteRenderer spriteR;
    public Sprite[] sprites;
    

    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i <= 2; i++)
        {
            characters[i].sprite = digits[0];
        }

        scoreAmount = 0;
        //Simle.StonePickUp += AddScoreAndDisplayIt;
    }


    public void AddComboAndDisplayIt(int randomScoreValue)
    {
        scoreAmount = randomScoreValue;
        //Debug.Log("+ " + randomScoreValue);

        spriteR = gradin.GetComponent<SpriteRenderer>();

        if (scoreAmount > 0)
        {
            publicSprite.transform.Translate(Vector3.up*30 * Time.deltaTime);
        }

        if (scoreAmount < 50) {
            spriteR.sprite = sprites[0];
        }
        if (scoreAmount >= 50 && scoreAmount < 100)
        {
            spriteR.sprite = sprites[1];
        }
        if (scoreAmount >= 100 && scoreAmount < 150)
        {
            spriteR.sprite = sprites[2];
        }
        if (scoreAmount >= 150)
        {
            spriteR.sprite = sprites[3];
        }
        
       

        int[] scoreAmountDigitsArray = GetDigitsArrayFromScoreAmount(scoreAmount);

        switch (scoreAmountDigitsArray.Length)
        {
            case 1:
                characters[0].sprite = digits[0];
                characters[1].sprite = digits[0];
                characters[2].sprite = digits[0];
                characters[3].sprite = digits[scoreAmountDigitsArray[0]];
                break;
            case 2:
                characters[0].sprite = digits[0];
                characters[1].sprite = digits[0];
                characters[2].sprite = digits[scoreAmountDigitsArray[0]];
                characters[3].sprite = digits[scoreAmountDigitsArray[1]];
                break;
            case 3:
                characters[0].sprite = digits[0];
                characters[1].sprite = digits[scoreAmountDigitsArray[0]];
                characters[2].sprite = digits[scoreAmountDigitsArray[1]];
                characters[3].sprite = digits[scoreAmountDigitsArray[2]];
                break;
            case 4:
                characters[0].sprite = digits[scoreAmountDigitsArray[0]];
                characters[1].sprite = digits[scoreAmountDigitsArray[1]];
                characters[2].sprite = digits[scoreAmountDigitsArray[2]];
                characters[3].sprite = digits[scoreAmountDigitsArray[3]];
                break;
        }

    }

    private int[] GetDigitsArrayFromScoreAmount(int scoreAmount)
    {
        List<int> listOfInts = new List<int>();
        while (scoreAmount > 0)
        {
            listOfInts.Add(scoreAmount % 10);
            scoreAmount = scoreAmount / 10;
        }
        listOfInts.Reverse();
        return listOfInts.ToArray();
    }

    /*
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            AddScoreAndDisplayIt(10);
        }
    }
    */
    

  
    // Update is called once per frame




}
