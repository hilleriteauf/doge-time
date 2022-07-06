using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    //[SerializeField]
    //private Sprite[] digits;
    public ComboController combo;

    [SerializeField]
    public Sprite[] digits;

    [SerializeField]
    private Image[] characters;

    private int scoreAmount;

    private int numberOfDigitsInScoreAmount;

    // Start is called before the first frame update
    void Start()
    {
        digits = combo.digits;
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].sprite = digits[0];
        }

        scoreAmount = 0;
        //Simle.StonePickUp += AddScoreAndDisplayIt;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetScore(int NewScore)
    {
        scoreAmount = NewScore;

        int[] scoreAmountDigitsArray = GetDigitsArrayFromScoreAmount(scoreAmount);

        switch (scoreAmountDigitsArray.Length)
        {
            case 1:
                characters[0].sprite = digits[0];
                characters[1].sprite = digits[0];
                characters[2].sprite = digits[0];
                characters[3].sprite = digits[0];
                characters[4].sprite = digits[0];
                characters[5].sprite = digits[0];
                characters[6].sprite = digits[scoreAmountDigitsArray[0]];
                break;
            case 2:
                characters[0].sprite = digits[0];
                characters[1].sprite = digits[0];
                characters[2].sprite = digits[0];
                characters[3].sprite = digits[0];
                characters[4].sprite = digits[0];
                characters[5].sprite = digits[scoreAmountDigitsArray[0]];
                characters[6].sprite = digits[scoreAmountDigitsArray[1]];
                break;
            case 3:
                characters[0].sprite = digits[0];
                characters[1].sprite = digits[0];
                characters[2].sprite = digits[0];
                characters[3].sprite = digits[0];
                characters[4].sprite = digits[scoreAmountDigitsArray[0]];
                characters[5].sprite = digits[scoreAmountDigitsArray[1]];
                characters[6].sprite = digits[scoreAmountDigitsArray[2]];
                break;
            case 4:
                characters[0].sprite = digits[0];
                characters[1].sprite = digits[0];
                characters[2].sprite = digits[0];
                characters[3].sprite = digits[scoreAmountDigitsArray[0]];
                characters[4].sprite = digits[scoreAmountDigitsArray[1]];
                characters[5].sprite = digits[scoreAmountDigitsArray[2]];
                characters[6].sprite = digits[scoreAmountDigitsArray[3]];
                break;
            case 5:
                characters[0].sprite = digits[0];
                characters[1].sprite = digits[0];
                characters[2].sprite = digits[scoreAmountDigitsArray[0]];
                characters[3].sprite = digits[scoreAmountDigitsArray[1]];
                characters[4].sprite = digits[scoreAmountDigitsArray[2]];
                characters[5].sprite = digits[scoreAmountDigitsArray[3]];
                characters[6].sprite = digits[scoreAmountDigitsArray[4]];
                break;
            case 6:
                characters[0].sprite = digits[0];
                characters[1].sprite = digits[scoreAmountDigitsArray[0]];
                characters[2].sprite = digits[scoreAmountDigitsArray[1]];
                characters[3].sprite = digits[scoreAmountDigitsArray[2]];
                characters[4].sprite = digits[scoreAmountDigitsArray[3]];
                characters[5].sprite = digits[scoreAmountDigitsArray[4]];
                characters[6].sprite = digits[scoreAmountDigitsArray[5]];
                break;
            case 7:
                characters[0].sprite = digits[scoreAmountDigitsArray[0]];
                characters[1].sprite = digits[scoreAmountDigitsArray[1]];
                characters[2].sprite = digits[scoreAmountDigitsArray[2]];
                characters[3].sprite = digits[scoreAmountDigitsArray[3]];
                characters[4].sprite = digits[scoreAmountDigitsArray[4]];
                characters[5].sprite = digits[scoreAmountDigitsArray[5]];
                characters[6].sprite = digits[scoreAmountDigitsArray[6]];
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
        if (Input.GetKeyDown("e"))
        {
            AddScoreAndDisplayIt(1000);
        }
    }
    */


    // Update is called once per frame




}
