using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboController : MonoBehaviour
{
    [SerializeField]
    public Sprite[] digits;

    [SerializeField]
    private Image[] characters;

    private int comboAmout;

    private int numberOfDigitsInComboAmount;

    
    public GameObject PublicSprite;
    public GameObject AmongUsGradin;
    private SpriteRenderer AmongUsGradinSpriteRenderer;
    public Sprite[] AmongUsGradinSprites = new Sprite[4];
    

    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].sprite = digits[0];
        }

        comboAmout = 0;
        //Simle.StonePickUp += AddScoreAndDisplayIt;
    }

    public void MakePublicDanse()
    {
        PublicSprite.transform.Translate(Vector3.up*30 * Time.deltaTime);
    }

    public void SetCombo(int newCombo)
    {
        comboAmout = newCombo;
        Debug.Log("newCombo: " + newCombo);

        AmongUsGradinSpriteRenderer = AmongUsGradin.GetComponent<SpriteRenderer>();

        if (comboAmout < 50) {
            AmongUsGradinSpriteRenderer.sprite = AmongUsGradinSprites[0];
        }
        if (comboAmout >= 50 && comboAmout < 100)
        {
            AmongUsGradinSpriteRenderer.sprite = AmongUsGradinSprites[1];
        }
        if (comboAmout >= 100 && comboAmout < 150)
        {
            AmongUsGradinSpriteRenderer.sprite = AmongUsGradinSprites[2];
        }
        if (comboAmout >= 150)
        {
            AmongUsGradinSpriteRenderer.sprite = AmongUsGradinSprites[3];
        }
        
       

        int[] scoreAmountDigitsArray = GetDigitsArrayFromScoreAmount(comboAmout);

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
