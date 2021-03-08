using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;

public class TriviaController : MonoBehaviour
{
    public static GameObject triviaCanvas;
    public static GameObject triviaQuestionPrefab;
    public static GameObject triviaQuestionChoiceAPrefab;
    public static GameObject triviaQuestionChoiceBPrefab;
    public static GameObject triviaQuestionChoiceCPrefab;
    public static GameObject triviaQuestionChoiceGroupPrefab;
    private static string[] triviaDb;
    private static string[] questions;
    private static string[] shuffledQuestions;
    private static string[] shuffledChoices;
    private static string[] shuffledAnswers;
    private static int shuffledQuestionsCurrentIndex;
    private static string[] _choices;
    private static string[] answers;

    private void Start()
    {
        triviaCanvas = GameObject.FindGameObjectWithTag("TriviaCanvas");
        LoadTriviaDatabase("triviadb.csv");
        InitializeTriviaArrays();
        ParseTriviaDatabase("Id,Answer,Question,Choices");
        string[][] shuffledResult = ShuffleQuestions();
        shuffledQuestions = shuffledResult[0];
        shuffledChoices = shuffledResult[1];
        shuffledAnswers = shuffledResult[2];
        LogInfo();
        // Setup UI
        triviaQuestionPrefab = GameObject.FindGameObjectWithTag("TriviaQuestionTextPrefab");
        triviaQuestionChoiceAPrefab = GameObject.FindGameObjectWithTag("TriviaQuestionChoiceAPrefab");
        triviaQuestionChoiceBPrefab = GameObject.FindGameObjectWithTag("TriviaQuestionChoiceBPrefab");
        triviaQuestionChoiceCPrefab = GameObject.FindGameObjectWithTag("TriviaQuestionChoiceCPrefab");
        triviaQuestionChoiceGroupPrefab = GameObject.FindGameObjectWithTag("TriviaQuestionChoiceGroupPrefab");
        // Start new round test
        StartNewTriviaRound();
    }

    /**
     * TODO Will be replaced by dict soon.
     */
    private static void InitializeTriviaArrays()
    {
        questions = new string[triviaDb.Length - 1];
        _choices = new string[triviaDb.Length - 1];
        answers = new string[triviaDb.Length - 1];
    }

    /**
     * To read the trivia questions, choices and answer,
     * we need to load the db from a file (csv for ease of design).
     * @args dbName : string name of file     
     */
    public static void LoadTriviaDatabase(string dbName)
    {
        string pathToDb = $"{Application.dataPath}/{dbName}";
        try
        {
            // Open the text file using a stream reader and the ",,," csv delimiter
            using (var sr = new StreamReader($"{pathToDb}"))
            {
                string db = sr.ReadToEnd();
                string[] csvLineDelimiter = { ",,," };
                triviaDb = db.Split(csvLineDelimiter, System.StringSplitOptions.RemoveEmptyEntries);
            }
        }
        catch (FileNotFoundException e)
        {
            Debug.LogError(e.Message);
        }
    }
    /**
     * To get the fields, 
     * we need to parse the string db
     * and put it into a dictionary for ease.
     * @args table : dictionary hash table
     * @args triviaDb : string[] the db
     */
    public static void ParseTriviaDatabase(string header)
    {
        int i = triviaDb[0].Equals(header) ? 1 : 0; // first row is the header in a .csv file
        for(; i < triviaDb.Length; i++)
        {
            string[] fields = triviaDb[i].Split(',');
            string id = fields[0].Trim(new char[] { '\r', '\n' });
            string choices = fields[3].Trim(new char[] { '\r', '\n' });
            string question = fields[2].Trim(new char[] { '\r', '\n', '"'}); // Remove the quotation marks from .csv auto export
            string answer = fields[1].Trim(new char[] { '\r', '\n' }); // A carriage return and line skip is actually there at the head
            questions[i-1] = question;
            _choices[i-1] = choices;
            answers[i-1] = answer;
        }
    }
    /**
    * To see the trivia quiz,
    * upon collision with a memory checkpoint
    * on the rollercoaster, we need to activate
    * the prefabs for the canvas and its elements (buttons and labels
    * fitting the question randomly picked) on the screen.
    */
    /**
    * To begin a new trivia quiz, we need to load
    * a new question from the shuffled questions.
    * We add to the counter after the question has been answered.
    */
    public static void StartNewTriviaRound()
    {
        string newQuestion = shuffledQuestions[shuffledQuestionsCurrentIndex];
        string newChoices = shuffledChoices[shuffledQuestionsCurrentIndex];
        string newAnswer = shuffledAnswers[shuffledQuestionsCurrentIndex];
        DisplayTriviaText(newQuestion, newChoices, newAnswer);
    }
    // Listener (if player is not dead, we put the cooldown on the trivia panel)
    public static void CooldownTriviaPanel()
    {
        ++shuffledQuestionsCurrentIndex;
        ShowUIPanel(false);
        SetTriviaQuestionTextPrefabAlpha(0f);
        DisplayTriviaChoicesGroupPrefab(false);
    }
    /**
    * To see the questions, we first shuffle
    * the questions order.
    */
    public static string[][] ShuffleQuestions()
    {
        if (questions == null)
        {
            Debug.LogError("Questions not initialized.");
            return null;
        }
        // copy
        string[] shuffledQuestions = (string[])questions.Clone();
        string[] shuffledChoices = (string[])_choices.Clone();
        string[] shuffledAnswers = (string[])answers.Clone();
        string[][] shuffledResult = new string[][] { shuffledQuestions, shuffledChoices, shuffledAnswers };

        for (int i = 0; i < shuffledQuestions.Length; i++)
        {
            int randIndex = UnityEngine.Random.Range(0, shuffledQuestions.Length - 1);
            // Swap
            //if (i == randIndex) continue;
            string temp = shuffledQuestions[i];
            shuffledQuestions[i] = shuffledQuestions[randIndex];
            shuffledQuestions[randIndex] = temp;

            temp = shuffledChoices[i];
            shuffledChoices[i] = shuffledChoices[randIndex];
            shuffledChoices[randIndex] = temp;

            temp = shuffledAnswers[i];
            shuffledAnswers[i] = shuffledAnswers[randIndex];
            shuffledAnswers[randIndex] = temp;
        }
        return shuffledResult;
    }
    /**
     * To show the trivia text, we need to call the required methods.
     */
    public static void DisplayTriviaText(string newQuestion, string newChoices, string answer)
    {
        ShowUIPanel(true);
        SetTriviaQuestionTextPrefabText(newQuestion);
        SetTriviaQuestionTextPrefabAlpha(255.0f);
        string[] splitChoices = new string[3];
        splitChoices[0] = newChoices.Split(';')[0].Split(':')[1];
        splitChoices[1] = newChoices.Split(';')[1].Split(':')[1];
        splitChoices[2] = newChoices.Split(';')[2].Split(':')[1];
        DisplayTriviaChoicesGroupPrefab(true);
        SetTriviaChoicesText(splitChoices);
    }
    /**
    * To begin the trivia mini-game,
    * we need to show the trivia panel.
    */
    public static void ShowUIPanel(bool enabled)
    {
        triviaCanvas.SetActive(enabled);
    }
    /*
    * To show the text, we need to set the alpha higher. 
    */
    public static void SetTriviaQuestionTextPrefabAlpha(float value)
    {
        triviaQuestionPrefab.GetComponent<TextMeshProUGUI>().alpha = value;
    }
    public static void SetTriviaQuestionTextPrefabText(string text)
    {
        triviaQuestionPrefab.GetComponent<TextMeshProUGUI>().text = text;
    }
    public static void SetTriviaChoicesText(string[] choice)
    {
        triviaQuestionChoiceAPrefab.GetComponentInChildren<TextMeshProUGUI>().text = choice[0];
        triviaQuestionChoiceBPrefab.GetComponentInChildren<TextMeshProUGUI>().text = choice[1];
        triviaQuestionChoiceCPrefab.GetComponentInChildren<TextMeshProUGUI>().text = choice[2];
    }
    public static void DisplayTriviaChoicesGroupPrefab(bool enabled)
    {
        triviaQuestionChoiceGroupPrefab.GetComponent<Canvas>().enabled = enabled;
    }
    /**
     *  Log content of db.
     */
    private static void LogInfo()
    {
        for (int i = 0; i < questions.Length; i++)
        {
            Debug.Log($"{questions[i]}");
            Debug.Log($"{_choices[i]}");
            Debug.Log($"{answers[i]}");
        }
    }
    /**
    * To see the trivia resources, we need to instantiate them.
    */
    public static GameObject InstantiateResource(string path)
    {
        return Instantiate(Resources.Load<GameObject>(path));
    }
}
