using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TriviaController : MonoBehaviour
{
    public static GameObject triviaCanvas;
    public static GameObject choiceButton;
    public static GameObject choiceLabel;
    private static string[] triviaDb;
    private static string[] questions;
    private static string[] _choices;
    private static string[] answers;

    private void Start()
    {
        triviaCanvas = Instantiate(Resources.Load<GameObject>("UI/TriviaCanvas"));
        triviaCanvas.SetActive(false);
        LoadTriviaDatabase("triviadb.csv");
        SetupTriviaQuestionsAndChoices();
        ParseTriviaDatabase();
        LogInfo();
        //choiceButton = Instantiate(Resources.Load<GameObject>("ChoicePrefab"));
        //choiceLabel = Instantiate(Resources.Load<GameObject>("ChoiceLabel"));
    }

    private static void LogInfo()
    {
        for (int i = 0; i < questions.Length; i++)
        {
            Debug.Log($"{questions[i]}");
            Debug.Log($"{_choices[i]}");
            Debug.Log($"{answers[i]}");
        }
    }

    private static void SetupTriviaQuestionsAndChoices()
    {
        questions = new string[triviaDb.Length - 1];
        _choices = new string[triviaDb.Length - 1];
        answers = new string[triviaDb.Length - 1];
    }

    /**
    * To begin the trivia mini-game,
    * we need to show the trivia panel.
    */
    public static void ShowUIPanel(bool enabled)
    {
        triviaCanvas.SetActive(enabled);
    }
    /**
     * To read the trivia questions, choices and answer,
     * we need to load the db from a file (csv for ease of design).
     * @args dbName : string name of file     
     */
    public static void LoadTriviaDatabase(string dbName)
    {
        // Get the file
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
    public static void ParseTriviaDatabase()
    {
        int i = triviaDb[0].Equals("Id,Answer,Question,Choices") ? 1 : 0; // first row is the header in a csv file
        for(; i < triviaDb.Length; i++)
        {
            string[] fields = triviaDb[i].Split(',');
            string id = fields[0].Trim(new char[] { '\r', '\n' });
            string choices = fields[3].Trim(new char[] { '\r', '\n' });
            string question = fields[2].Trim(new char[] { '\r', '\n' });
            string answer = fields[1].Trim(new char[] { '\r', '\n' });
            questions[i-1] = question;
            _choices[i-1] = choices;
            answers[i-1] = answer;
        }
    }
}
