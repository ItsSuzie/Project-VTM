using UnityEngine;
/// <summary>
/// Instance manager
/// 
/// Ensures that there is only 1 instance of this class.
/// Note: It's only for 1 instance of this specific class. If you want to create instance manager for
/// more than this class, create the other instance managers for the other classes for other objects
/// </summary>
public class PlayerInstanceManager : MonoBehaviour {

    public GameObject lumi;
    public bool destroyGameObjectUponLoad = false;

    private static PlayerInstanceManager instance;        // Static reference to the instance

    private void Awake()
    {
        Debug.Log("Instance of " + this.gameObject.name + " Loaded");

        // If the instance is not set
        if (instance == null)
        {
            // Set current instance
            instance = this;

            // Ensure we don't destroy this instance upon loading a scene
            DontDestroyOnLoad(gameObject);

            Debug.Log("Instance of " + this.gameObject.name + " Set");
        }

        // If the instance is not set to the original
        //  There is another instance of this object
        else if (instance != this)
        {
            // Destroy the duplicate
            Debug.Log("Destroying Instance of " + this.gameObject.name + ". Already exists");
            Destroy(gameObject);
        }
    }

    
    public GameObject GetPlayer
    {
        get { return lumi; }
    }

}
