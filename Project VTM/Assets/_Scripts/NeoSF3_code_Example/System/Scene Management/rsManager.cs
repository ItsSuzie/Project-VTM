using UnityEngine;
using UnityEngine.SceneManagement;

public class rsManager : MonoBehaviour
{

    private GameObject playersGameObject;
    private SceneTransitionManager playersSceneManager;

    private void Start()
    {
        Debug.Log("Hello rsManager");
    }

    // assigns components
    public void assignComponents(GameObject playersGO, SceneTransitionManager playersSM)
    {
        playersGameObject = playersGO;
        playersSceneManager = playersSM;
    }

    public void loadScene(int id)
    {
        SceneManager.LoadScene(id);
        Destroy(playersGameObject);
    }
}
