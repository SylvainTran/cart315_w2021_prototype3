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
            RideRailcoaster.StartRide(GameObject.FindGameObjectWithTag("Player"));
            // Show twinkie trivia
            TriviaController.StartNewTriviaRound("twinkie");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
