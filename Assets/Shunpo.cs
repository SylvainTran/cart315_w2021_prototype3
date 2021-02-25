using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* The SHUNPO aspect of the player's UNIT action moment-turn-based system.
* Take one step, enemy takes one step too (mirror?)
*/
public class Shunpo : MonoBehaviour
{
    public float velocity;
    public float acceleration = 2.0f;
    public float speed = 500f;

    void Update()
    {
        /**
        * Very intentional movement forward.
        * Movement in Kendo is akin to discrete math rather than continuous.
        * A sliding, slerp function and a re-center weight effect is needed.
        * Frame-dependent... time-dependent too?
        */
        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            Debug.Log("Moving one step forward towards the enemy, looking at the horizon and mountain");
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            // Consume one UNIT action moment-turn point?
            // Mirrored by enemy AI?
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)) {
            Debug.Log("Moving one step forward towards the enemy, looking at the horizon and mountain");
            this.transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Fumikashi ashi");
            this.transform.Translate(Vector3.forward * speed * acceleration * Time.deltaTime);   
            // Shake camera effect?
            // Thunder FX and sound         
        }
    }
}
