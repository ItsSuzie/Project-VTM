using UnityEngine;

public class RestartUponDeath : MonoBehaviour
{
    #region variables
    public PlayerAttributesController[] players;

    // private variables
    private int curFrameCount = 0;
    private SceneTransitionManager sceneManager;
    private rsManager rsmanager;
    private bool restartRan = false;
    private bool myStart = true;
    #endregion

    #region private variables

    private void Start()
    {


    }

    private void Update()
    {
        if (myStart && curFrameCount >= 20)
        {
            myStart = false;
            sceneManager = GetComponent<SceneTransitionManager>();
            rsmanager = GameObject.FindGameObjectWithTag("rsManager").GetComponent<rsManager>();
        }
        // Every 20 frames, check if players dead
        if (curFrameCount >= 20 && !myStart)
        {
            curFrameCount = 0;
            // If both players died, restart scene
            if ((players[0].PlayerDied && players[1].PlayerDied) == true && !restartRan)
            {
                restartRan = true;
                Invoke("RestartGame", 1.0f);
            }
        }

        ++curFrameCount;
    }

    private void RestartGame()
    {
        rsmanager.assignComponents(this.gameObject, sceneManager);
        rsmanager.loadScene(0);
    }


    #endregion

}
