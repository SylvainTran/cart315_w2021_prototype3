using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public bool showedButtonsYet = false;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ShowStartButton");
    }
    public IEnumerator ShowStartButton()
    {
        yield return new WaitForSeconds(45.0f);
        canvas.GetComponent<Canvas>().enabled = true;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("RollercoasterThemePark");
    }
}
