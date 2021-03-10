using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class RideRailcoaster : MonoBehaviour
{
    public GameObject narrativeCanvas;

    // Ride rollercoaster
    // Begin at first waypoint of Bezier curve
    public static void StartRide(Collider other)
    {
        other.gameObject.GetComponent<PathFollower>().enabled = true;
        SoundController.StopFootstepSound();
        SimpleController_UsingPlayerInput.playerIsOnRollerCoaster = true;
    }
    public static void StartRide(GameObject player)
    {
        player.gameObject.GetComponent<PathFollower>().enabled = true;
        SoundController.StopFootstepSound();
        SimpleController_UsingPlayerInput.playerIsOnRollerCoaster = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartRide(other);
            GetComponent<AudioSource>().Play();
            narrativeCanvas.GetComponent<Canvas>().enabled = true;
            StartCoroutine(FadeNarrativeCanvas(5.0f));
        }
    }

    private IEnumerator FadeNarrativeCanvas(float delay)
    {
        yield return new WaitForSeconds(delay);
        narrativeCanvas.GetComponent<Canvas>().enabled = false;
    }
}
