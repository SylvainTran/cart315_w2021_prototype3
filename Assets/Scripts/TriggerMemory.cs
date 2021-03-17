using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerMemory : MonoBehaviour
{
    public GameObject RollercoasterController;
    public GameObject SoundController;
    public string nextScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RollercoasterController.GetComponent<AudioSource>().volume = 0.05f;
            SoundController.GetComponent<AudioSource>().volume = 0.05f;
            GetComponent<AudioSource>().Play();
            GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(WaitAndChangeScene(16.0f));
        }
    }

    private IEnumerator WaitAndChangeScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SimpleController_UsingPlayerInput.playerIsOnRollerCoaster = false;
        Debug.Log("Changing scene to " + nextScene);
        SceneManager.LoadScene(nextScene);
    }
}
