using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    public static AudioSystem instance;
    private void Awake()
    {
        instance = this;
    }


    //sound effect
    public AudioClip sfx_landing, sfx_cherry;
    //music
    public AudioClip music_tiktok;

    //sound object
    public GameObject soundObject;

    public void PlaySFX(string sfxName)
    {
        switch(sfxName)
        {
            case "landing":
                
                SoundObjectCreation(sfx_landing);
                break;
            case "cherry":
                SoundObjectCreation(sfx_cherry);

                break;
            default:
                break;
        }
    }

    void SoundObjectCreation(AudioClip clip)
    {
        //create SoundsObject gameobject
        GameObject newObject = Instantiate(soundObject,transform);
        //assign audioClip to its audiosource
        newObject.GetComponent<AudioSource>().clip = clip; 
        //play the audio
        newObject.GetComponent<AudioSource>().Play();   
    }

}
