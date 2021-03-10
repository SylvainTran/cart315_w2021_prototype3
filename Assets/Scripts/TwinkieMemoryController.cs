using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TwinkieMemoryController : MonoBehaviour
{
    public GameObject tutorialCanvas;
    private static int twinkieEaten = 0;
    /**
    * Gets a ray to be used in raycasting.
    */
    private Ray GetRay()
    {
        Debug.DrawRay(transform.position, Input.mousePosition, Color.red);
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    /**
    * Raycasts
    * and does stuff
    */
    private RaycastHit ApplyRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(GetRay(), out hit))
        {
            if (hit.collider.gameObject.CompareTag("Twinkie"))
            {
                tutorialCanvas.GetComponent<Canvas>().enabled = true;
                ++twinkieEaten;
                tutorialCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"You ate {twinkieEaten} twinkies.";
                Destroy(hit.collider.gameObject);
                if(twinkieEaten == 12)
                {
                    Main.playedTwinkieMemory = true;
                    SceneManager.LoadScene("RollercoasterThemePark");
                }
            }
        }
        return hit;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = ApplyRayCast();
        }
    }
}
