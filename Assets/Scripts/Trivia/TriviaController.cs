using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriviaController : MonoBehaviour
{
    public static GameObject triviaCanvas;
    public GameObject choiceButton;
    public GameObject choiceLabel;

    private void Start()
    {
        triviaCanvas = Instantiate(Resources.Load<GameObject>("UI/TriviaCanvas"));
        triviaCanvas.SetActive(true);
        DontDestroyOnLoad(this);
    }
    /**
     * To begin the trivia mini-game,
     * we need to show the trivia panel.
     */
    public static void ShowTriviaPanel(bool enabled)
    {
        triviaCanvas.SetActive(enabled);
    }
}
