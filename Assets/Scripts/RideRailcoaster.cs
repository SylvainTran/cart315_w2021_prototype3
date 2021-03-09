using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class RideRailcoaster : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colliding with" + other.gameObject);
        if (other.CompareTag("Player"))
        {
            // Ride rollercoaster
            // Begin at first waypoint of Bezier curve
            other.gameObject.GetComponent<PathFollower>().enabled = true;
        }
    }
}
