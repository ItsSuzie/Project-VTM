using UnityEngine;

public class fadeSceneTransition : MonoBehaviour
{
    public SceneTransitionManager sceneTransitionManager;

    public void inititateLevelLoad()
    {
        sceneTransitionManager.transitionScene();
    }
    public void initLoadCheckers()
    {
        sceneTransitionManager.load();
    }
}
