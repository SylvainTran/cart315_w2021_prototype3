using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuite : MonoBehaviour
{
    // Trivia quiz mini-game tests
    [SetUp]
    public void Setup()
    {

    }
    [UnityTest]
    public IEnumerator TriviaCanvasExists()
    {
        GameObject triviaCanvas = Instantiate(Resources.Load<GameObject>("UI/TriviaCanvas"));
        triviaCanvas.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Assert.True(triviaCanvas.gameObject.activeSelf);
    }
}
