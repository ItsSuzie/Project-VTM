using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class musicDictionary
{
    public string key;
    public AudioClip value;
}

public class WorldMusicManager : MonoBehaviour
{
    // Variables
    // ---------------------------------
    #region Variables

    // Public variables
    // ---------------------------------
    public List<musicDictionary> BGThemes;

    // Private variables
    // ---------------------------------
    
    // Component reference
    private AudioSource audioSource;

    #endregion

    // Private methods
    // ---------------------------------
    #region private methods

    private void Start()
    {
        // get audio source
        audioSource = GetComponent<AudioSource>();

        // Play main menu
        playMusic("Main Menu");
    }


    #endregion


    // plays music based on level string
    public void playMusic(string levelName)
    {
        // Play mainMenu
        AudioClip audio = null;
        for (int i = 0; i < BGThemes.Count; ++i)
        {
            if (BGThemes[i].key == levelName)
            {
                audio = BGThemes[i].value;
                break;
            }
        }

        audioSource.clip = audio;
        audioSource.loop = true;
        audioSource.Play();
    }


}
