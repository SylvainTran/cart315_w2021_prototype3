using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RollercoasterThemeParkController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SimpleController_UsingPlayerInput.playerIsOnRollerCoaster = false;
        if (SceneManager.GetActiveScene().name.Equals("RollercoasterThemePark") && Main.playedTwinkieMemory)
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

    }
}
