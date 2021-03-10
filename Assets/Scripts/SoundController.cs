using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public GameObject player;
    public static AudioSource footstepSound;
    void OnEnable()
    {
        SimpleController_UsingPlayerInput.OnPlayerMoved += PlayFootstepSound;
        SimpleController_UsingPlayerInput.OnPlayerStopped += StopFootstepSound;
    }
    void OnDisable()
    {
        SimpleController_UsingPlayerInput.OnPlayerMoved -= PlayFootstepSound;
        SimpleController_UsingPlayerInput.OnPlayerStopped -= StopFootstepSound;
    }
    // Start is called before the first frame update
    void Start()
    {
        AudioSource theme = GetComponent<AudioSource>();
        if (!theme.isPlaying)
        {
            theme.Play();
        }
        player = GameObject.FindGameObjectWithTag("Player");
        footstepSound = player.GetComponent<AudioSource>();
    }

    public static void PlayFootstepSound()
    {
        if (!footstepSound.isPlaying)
        {
            footstepSound.Play();
        }
    }
    public static void StopFootstepSound()
    {
        if (footstepSound.isPlaying)
        {
            footstepSound.Stop();
        }
    }
}
