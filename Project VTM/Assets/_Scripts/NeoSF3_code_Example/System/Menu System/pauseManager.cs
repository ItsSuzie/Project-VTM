using UnityEngine;

public class pauseManager : MonoBehaviour
{
    #region variables
    public bool isPaused;
    public bool isInCutscene;

    #endregion

    #region public methods

    /// <summary>
    /// Pauses game
    /// </summary>
    public void executePause()
    {
        isPaused = true;
    }

    /// <summary>
    /// initiates cutscene
    /// </summary>
    public void executeCutscene()
    {
        isInCutscene = true;
    }

    /// <summary>
    /// Exits cutscene
    /// </summary>
    public void exitCutscene()
    {
        isInCutscene = false;
    }

    /// <summary>
    /// Exits pause
    /// </summary>
    public void exitPause()
    {
        isPaused = false;
    }

    #endregion
}
