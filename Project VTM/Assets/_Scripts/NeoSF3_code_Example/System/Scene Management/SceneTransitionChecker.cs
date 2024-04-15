using UnityEngine;

public class SceneTransitionChecker : MonoBehaviour
{

    #region Variables
    [Header("Rect hit")]
    public Rect sceneRect;
    public Vector2 sceneDirection;
    public float sceneDistance;
    public LayerMask collisionMask;
    
    [Header("Transition location")]
    public string nextSceneToLoad;
    public Vector2 locToPlacePlayerNextScene;
    #endregion

    /// <summary>
    /// Checks to see if has been hit
    /// </summary>
    /// <returns></returns>
    public TransitionSetter hasHitPlayer()
    {
        TransitionSetter transition = new TransitionSetter();
        Vector2 rayOrigin = (Vector2)transform.position + sceneRect.center;
        RaycastHit2D hit = Physics2D.BoxCast(rayOrigin, sceneRect.size, 0, sceneDirection, sceneDistance, collisionMask);
        if (hit)
        {
            transition.nextScene = nextSceneToLoad;
            transition.location = locToPlacePlayerNextScene;
            return transition;
        }
        else
            return null;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS((Vector2)this.transform.position + sceneRect.center, this.transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector2.zero, sceneRect.size);
        Gizmos.matrix = Matrix4x4.TRS((Vector2)this.transform.position + sceneRect.center + (sceneDirection.normalized * sceneDistance), this.transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector2.zero, sceneRect.size);
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS((Vector2)this.transform.position + sceneRect.center, Quaternion.identity, Vector3.one);
        Gizmos.DrawLine(Vector2.zero, sceneDirection.normalized * sceneDistance);

    }
}
