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

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("qt");
        for (int i = 0; i <= 2; i++)
        {
            characters[i].sprite = digits[0];
        }

        scoreAmount = 0;
        //Simle.StonePickUp += AddScoreAndDisplayIt;
    }


    public void AddScoreAndDisplayIt(int randomScoreValue)
    {
        scoreAmount += randomScoreValue;
        Debug.Log("+ " + randomScoreValue);


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

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            AddScoreAndDisplayIt(100);
        }
    }

  
    // Update is called once per frame




}
