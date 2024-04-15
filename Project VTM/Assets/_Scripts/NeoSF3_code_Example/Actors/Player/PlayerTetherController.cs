using UnityEngine;


/// <summary>
/// Tether 2 or more people based on set parameters
/// https://www.youtube.com/watch?v=r1Z8D8Y8k0U Look at this for reference
/// </summary>
public class PlayerTetherController : MonoBehaviour
{

    // Variables
    // -----------------------------------
    #region Variables
    // public variables
    //public PlayerInputController[] players;             // Lists all players
    public Transform otherPlayer;                       // Lists the other player
    public float playerMaxDistance = 20;                // The max distance between each player
    public bool atMax { get; private set; }             // Getter/setter? Sets/gets variable data here i suppose

    // Private variables
    //[SerializeField] private float playerDistanceTotal;          // Distance between both players


    #endregion

    // Private Methods
    // ------------------------------------
    #region Private Methods

    /// <summary>
    /// Unity update method
    /// 
    /// Updates every frame
    /// </summary>
    private void Update()
    {
        // Gets both player's locations, and their distance between the two
        //playerDistanceTotal = Vector2.Distance(players[0].transform.position, players[1].transform.position);

        // If max distance is greater than the current player distance, then stop
        // input away from the player


        float distance = (otherPlayer.position - transform.position).magnitude;
        //float distance = Vector2.Distance(otherPlayer.position, transform.position);
        atMax = distance >= playerMaxDistance;


        /*
        if (distance > playerMaxDistance)
        {
            Debug.Log("Player's are current at or above their max distance");
        }
        */

    }

    #endregion

    // Public Methods
    // ------------------------------------
    #region Public Methods

    /// <summary>
    /// CHecks players if they're closer
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public bool isCloser(Vector2 pos)
    {
        float distance = ((Vector2)otherPlayer.position - pos).magnitude;
        //float distance = Vector2.Distance(pos, otherPlayer.position);
        return distance < playerMaxDistance;
    }


    #endregion

    // Getters - setters
    // ------------------------------------
    #region getters/setters

    
    #endregion

}
