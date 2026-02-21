using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class WordGame : MonoBehaviour
{
    // VARIABLES

    public string correctWord;

    public string[] words;

    public string lettersGuessed = "";

    public int attemptsRemaining = 3;

    // UI REFERENCES

    public TMP_Text wordText;

    public TMP_Text attemptsText;

    public TMP_InputField guessInput;

    public Button submitButton;

    public Button resetButton;

    void Start()
    {
        LoadWords();
        ResetGame();

        // Connect buttons using code
        submitButton.onClick.AddListener(SubmitGuess);
        resetButton.onClick.AddListener(ResetGame);
    }


    // LOAD WORDS FROM FILE
    public void LoadWords()
    {
        TextAsset file = Resources.Load<TextAsset>("wordlist");

        words = file.text.Split('\n');
    }



    // CHOOSE RANDOM WORD
    public void ChooseWord()
    {
        int randomIndex = Random.Range(0, words.Length);

        correctWord = words[randomIndex].Trim().ToUpper();
    }



    // RESET GAME
    public void ResetGame()
    {
        ChooseWord();

        attemptsRemaining = 3;

        lettersGuessed = "";

        guessInput.text = "";

        UpdateText();
    }



    // SUBMIT GUESS BUTTON
    public void SubmitGuess()
    {
        string guess = guessInput.text.ToUpper();

        // must be exactly 1 character
        if (guess.Length != 1)
        {
            guessInput.text = "";
            return;
        }

        string letter = guess;

        // prevent duplicate guesses
        if (lettersGuessed.Contains(letter))
        {
            guessInput.text = "";
            return;
        }

        lettersGuessed += letter;

        // WRONG GUESS?
        if (!correctWord.Contains(letter))
        {
            attemptsRemaining--;
        }

        guessInput.text = "";

        UpdateText();
    }



    // UPDATE SCREEN TEXT
    public void UpdateText()
    {
        string displayWord = "";

        bool allLettersFound = true;

        foreach (char letter in correctWord)
        {
            if (lettersGuessed.Contains(letter.ToString()))
            {
                displayWord += letter;
            }
            else
            {
                displayWord += "-";
                allLettersFound = false;
            }
        }

        wordText.text = displayWord;

        attemptsText.text =
        "Attempts Remaining: "
        + attemptsRemaining
        + "\nGuessed Letters: "
        + lettersGuessed;



        // WIN
        if (allLettersFound)
        {
            attemptsText.text += "\nYOU WIN!";
        }

        // LOSE
        if (attemptsRemaining <= 0)
        {
            wordText.text = correctWord;

            attemptsText.text += "\nYOU LOSE!";
        }
    }
}
