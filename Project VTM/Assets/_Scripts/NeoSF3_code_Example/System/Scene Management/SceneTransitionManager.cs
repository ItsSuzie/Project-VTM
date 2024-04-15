using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{

    // Variables
    // --------------------------------
    #region Variables
    // Public Variables
    

    [Header("Scene Manager")]
    public Animator fadeAnim;
    public PlayerInputManager[] players;
    public string curSceneName;

    // Priavate variables
    private TransitionSetter nextScene;
    private WorldMusicManager musicManager;
    private string nextSceneToTransition;
    private bool levelSet = false;
    private GameObject TransitionCheckerGO;
    [SerializeField] private SceneTransitionChecker[] transitionChecker;

    public float WaitForSeconds { get; private set; }

    // private variables
    private bool dontRunMusic = true;


    #endregion

    #region private Methods

    /// <summary>
    /// Unity start methods
    /// 
    /// Runs at initialization
    /// </summary>
    private void Start()
    {
        players = transform.GetComponentsInChildren<PlayerInputManager>();
        musicManager = GetComponent<WorldMusicManager>();
    }   

    private void getTransitionCheckers()
    {
        TransitionCheckerGO = GameObject.FindGameObjectWithTag("SceneTransitionHolder");
        transitionChecker = TransitionCheckerGO.GetComponentsInChildren<SceneTransitionChecker>();
    }

    /// <summary>
    /// Unity update method
    /// 
    /// Runs once every frame
    /// </summary>
    private void Update()
    {
        if (!levelSet)
        {
            for (int i = 0; i < transitionChecker.Length; i++)
            {
                nextScene = transitionChecker[i].hasHitPlayer();
                // If player has been hit, change level
                if (transitionChecker[i].hasHitPlayer() != null)
                {
                    // Initiate level change
                    levelSet = true;
                    nextSceneToTransition = nextScene.nextScene;
                    initLevelChange();
                }
            }
        }
        
        
    }



    /// Level change
    /// // --------------------------

    private void initLevelChange()
    {
        /*
         * Initiate fade to black anim
         * fade to black anim ends, triggers load scene
         * loads scene
         */
        fadeAnim.SetTrigger("SwitchScene");
    }

    #endregion


    #region public Methods

    /// <summary>
    /// WHen called, it transitions to the next scene stated in the nextScenToLoad
    /// </summary>
    public void transitionScene()
    {
        levelSet = false;
        for (int i = 0; i < players.Length; ++i)
        {
            players[i].transform.position = nextScene.location + new Vector2(i * 2, 0);
        }
        transitionChecker = new SceneTransitionChecker[0];
        TransitionCheckerGO = null;
        StartCoroutine(transitionAsync());
        
    }

    private IEnumerator transitionAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextSceneToTransition);
        operation.allowSceneActivation = false;
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (progress >= 1.0f)
            {
                operation.allowSceneActivation = true;
                fadeAnim.SetTrigger("IntoGame");
            }
            yield return null;
        }
    }

    /// <summary>
    /// Loads next scene
    /// </summary>
    public void load()
    {
        getTransitionCheckers();
        curSceneName = SceneManager.GetActiveScene().name;
        if (dontRunMusic)
        {
            dontRunMusic = false;
        }
        else
        {
            musicManager.playMusic(curSceneName);
        }

    }

    public void restartGame(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    #endregion

}

