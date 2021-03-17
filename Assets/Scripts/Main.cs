using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/** 
* Main : MonoBehaviour
*
* Persistent driver class for global components.
*/
public sealed class Main : MonoBehaviour
{
    public static bool playedTwinkieMemory = false;
    public static bool answeredTwinkieTrivia = false;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}