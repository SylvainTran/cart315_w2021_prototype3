using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using TMPro;
/** 
* Main : MonoBehaviour
*
* Persistent driver class for global components.
*/
public sealed class Main : MonoBehaviour
{
    /**
    * Character Factory
    * Generates characters on demand.
    */
    public sealed class CharacterFactory
    {
        // Generates a single cub by instantiating the prefab addressed in the GameAssetDatabase
        // Which is hashmapped to the Cub prefab.
        public static Character GenerateNewCharacter() 
        {
            string characterType = "";
            return (Character)GameObject.Instantiate(GameAssetsCharacters.GetAsset(characterType), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        }
    }
    // For UI
    public delegate void OnCharactersLoaded();
    public static event OnCharactersLoaded onCharactersLoaded;
    /**
     * Level Controller.
     * Checks current game state and
     * rolls out game subroutines accordingly.
     */
    public sealed class LevelController
    {
        public static void ApplyCurrentState()
        {
            switch(gameState)
            {
                case 0: break;
                case 1: // GAME 
                    break;
                case 2: // GAME
                    break;
                case 3: // END
                    break;
                default:
                    break;
            }
        }
    }

    public enum GAME_STATES { INTRO, NORMAL, END };
    public static int gameState = default;
    public float restartGameDelay = 3.0f;

    public static void LoadMain()
    {
        GameObject main = GameObject.Instantiate(Resources.Load("Initializer")) as GameObject;
        GameObject.DontDestroyOnLoad(main);
        gameState = (int) GAME_STATES.NORMAL;
        LevelController.ApplyCurrentState();
        onCharactersLoaded();
    }

    private void Start()
    {
        GameAssetsCharacters.InitGameAssetsCharacters();
        GameAssetsCharacters.LoadTable();
        LoadMain();
    }

    /**
    * Gets a ray to be used in raycasting.
    */
    private Ray GetRay()
    {
        // Move the mouseSelector to the cursor
        Vector3 inputMousePos = Input.mousePosition;
        inputMousePos.z = Camera.main.nearClipPlane * 14;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(inputMousePos);
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    /**
    * Raycast Cub characters by tag
    * and updates flags. TODO make these local in scope.
    */
    private RaycastHit RayCastCharacters()
    {
       RaycastHit hit;        
       if (Physics.Raycast(GetRay(), out hit)){
            {
                if(!hit.collider.gameObject.CompareTag("Character")) 
                {

                }
            }
        }
        return hit;
    }

    private void Update()
    {
     
    }

    private IEnumerator GameOverState()
    {
        if (gameState == (int)GAME_STATES.END)
        {
            yield return new WaitForSecondsRealtime(restartGameDelay);
            SceneManager.LoadScene("MainMenu");
        }
    }
}