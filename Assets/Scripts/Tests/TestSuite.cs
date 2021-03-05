using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class TestSuite : MonoBehaviour
{
    // Trivia quiz mini-game tests
    string[] db = { "Id,Answer,Question,Choices", "1,C, 'How many twinkies did you eat on the morning of September 11th, 2001?',A:20;B:3;C:12" };
    [SetUp]
    public void Setup()
    {
        Debug.Log(db[1]);
    }
    [UnityTest]
    public IEnumerator TriviaCanvasExists()
    {
        GameObject triviaCanvas = Instantiate(Resources.Load<GameObject>("UI/TriviaCanvas"));
        triviaCanvas.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Assert.True(triviaCanvas.gameObject.activeSelf);
    }
    [UnityTest]
    public IEnumerator TestReadCSVDbCorrectly()
    {
        yield return new WaitForSeconds(0.1f);
        ReadCSVDbCorrectly();
    }
    public string[] ReadCSVDbCorrectly()
    {
        // Get the file
        string pathToDb = $"{Application.dataPath}/triviadb.csv";
        string db;
        string[] csvLineDelimiter = { ",,," };
        string[] lines;
        try
        {
            // Open the text file using a stream reader and the ",,," csv delimiter
            using (var sr = new StreamReader($"{pathToDb}"))
            {
               db = sr.ReadToEnd();
               lines = db.Split(csvLineDelimiter, System.StringSplitOptions.RemoveEmptyEntries);
               foreach (string line in lines)
               {
                   Debug.Log(line);
                   Assert.IsNotNull(line);
               }
               return lines;
            }
        }
        catch (FileNotFoundException e)
        {
            Debug.LogError(e.Message);
        }
        return null;
    }
    [UnityTest]
    public IEnumerator TestParseDb()
    {
        yield return new WaitForSeconds(0.1f);
        Dictionary<string, string[]> triviaDbTableTest = new Dictionary<string, string[]>();
        Dictionary<string, string[]> t = ParseTriviaDatabase(triviaDbTableTest, ReadCSVDbCorrectly());
        string[] _t;
        t.TryGetValue("1", out _t);
        if(_t.Length > 0)
        {
            string testLine = _t[2];
            Debug.Log(testLine);
            StringAssert.AreEqualIgnoringCase("How many twinkies did you eat on the morning of September 11th, 2001?", testLine);
        } else
        {
            Debug.Log("T is not what you think it is");
        }
        
    }

    public Dictionary<string, string[]> ParseTriviaDatabase(Dictionary<string, string[]> table, string[] triviaDb)
    {
        int i = triviaDb[0].Equals("Id,Answer,Question,Choices") ? 1 : 0; // first row is the header in a csv file
        for (; i < triviaDb.Length; i++)
        {
            string[] fields = triviaDb[i].Split(',');
            string choices = fields[3];
            string question = fields[2];
            string answer = fields[1];
            string id = fields[0];
            Debug.Log($"{id}");
            table.Add(id, fields);
        }
        return table;
    }
}
