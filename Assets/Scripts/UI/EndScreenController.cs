using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    public TextMeshProUGUI CorrectNotesText;
    public TextMeshProUGUI IncorrectNotesText;
    public TextMeshProUGUI MissedNotesText;
    public TextMeshProUGUI MaximumComboText;
    public TextMeshProUGUI SpeedText;
    public TextMeshProUGUI ScoreText;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Display(int correctNotes, int incorrectNotes, int missedNotes, int maximumCombo, float speed, int score)
    {
        gameObject.SetActive(true);
        SetValue(CorrectNotesText, correctNotes.ToString());
        SetValue(IncorrectNotesText, incorrectNotes.ToString());
        SetValue(MissedNotesText, missedNotes.ToString());
        SetValue(MaximumComboText, maximumCombo.ToString());
        SetValue(SpeedText, speed.ToString());
        SetValue(ScoreText, score.ToString());
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void SetValue(TextMeshProUGUI text, string value)
    {
        text.SetText(text.text.Split(":")[0] + ": " + value);
    }
}
