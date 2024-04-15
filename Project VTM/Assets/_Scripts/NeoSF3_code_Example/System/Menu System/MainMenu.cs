using UnityEngine;
using Fungus;

public class MainMenu : MonoBehaviour
{
    // Variables
    #region Variables

    // Public VAriables
    // ---------------------------------
    public GameObject mainMenuUI;
    public GameObject playerUI;
    public bool playCutscene = true;

    // private variables
    // ---------------------------------
    // Component references
    private SceneTransitionManager sceneManager;
    private WorldMusicManager musicManager;
    private pauseManager pauseManager;
    private Flowchart cutscene;

    private bool isPaused = true;


    #endregion

    #region private methods


    private void Start()
    {
        // get components
        sceneManager = GetComponent<SceneTransitionManager>();
        musicManager = GetComponent<WorldMusicManager>();
        pauseManager = GetComponent<pauseManager>();
        cutscene = GameObject.FindGameObjectWithTag("cutscene").GetComponent<Flowchart>();
    }


    #endregion


    //Public methods
    #region public Methods


    /// <summary>
    /// Plyaer presses start game, hides main menu ui, activates game
    /// </summary>
    public void startNewGame()
    {
        // Deactivate main menu
        mainMenuUI.SetActive(false);
        playerUI.SetActive(true);

        // return control to the player
        isPaused = false;

        // Play curLevelMusic
        musicManager.playMusic(sceneManager.curSceneName);

        if (playCutscene)
        {
            // execute first cutscene
            cutscene.ExecuteBlock("OnPlay");

            // execute cutscene, prevent input
            pauseManager.executeCutscene();
        }
        
    }


    #endregion


    // Getters setters
    #region getters/setters


    public bool IsPaused
    {
        get { return isPaused; }
    }

    #endregion
}
