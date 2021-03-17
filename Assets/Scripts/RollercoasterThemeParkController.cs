using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollercoasterThemeParkController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(Main.playedTwinkieMemory)
        {
            // Start off on the rollercoaster
            RideRailcoaster.StartRide(GameObject.FindGameObjectWithTag("Player"), true);
            // Show twinkie trivia
            TriviaController.StartNewTriviaRound("twinkie");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Main.answeredTwinkieTrivia)
        {
            RideRailcoaster.StartRide(GameObject.FindGameObjectWithTag("Player"), false);
        }
    }
}
