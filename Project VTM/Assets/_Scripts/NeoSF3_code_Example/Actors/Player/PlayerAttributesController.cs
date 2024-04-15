// using Com.LuisPedroFonseca.ProCamera2D;
using TMPro;
using UnityEngine;

public class PlayerAttributesController : MonoBehaviour
{

    // Variables
    #region Variables
    // Public Variables
    // --------------------------------
    [Header("Player Attributes")]
    public int playerHealth;            // Current player health
    public int playerHealthMax;         // Max player health
    public float invulnerabilityTimer;  // How long the player is invulnerable after hit
    public float staggerTimer;          // How long the player is staggered
    public float playerRespawnTimer;    // How long before the player can respawn after death
    public AudioClip playerTakingDamage;
    public AudioClip playerDeathSound;

    [Header("UI Attributes")]
    public SimpleHealthBar P1_Health_Bar;
    public GameObject P1_healthTextHolder;
    public TextMeshProUGUI P1_curHPText;
    public TextMeshProUGUI P1_MaxHPText;
    public TextMeshProUGUI P1_Respawn_Text;
    public TextMeshProUGUI p1_strText;
    public SimpleHealthBar P2_Health_Bar;
    public GameObject P2_healthTextHolder;
    public TextMeshProUGUI P2_curHPText;
    public TextMeshProUGUI P2_maxHPText;
    public TextMeshProUGUI P2_Respawn_Text;
    public TextMeshProUGUI p2_strText;

    [Header("Player Combat Values")]
    public int AttackStrength;          // Player attack strength

    [Header("Player Partner Attributes")]
    public PlayerAttributesController partner;
    [HideInInspector] public AudioSource audioSource;
    // [HideInInspector] public ProCamera2D cam;

    [Header("Pickups")]
    public LayerMask pickupsCollisionMask;


    // Private Variables
    // ---------------------------------
    // Component references
    private BoxCollider2D box2d;

    // Defies player number
    private int player_number;

    // invul timer
    private float invulTimer = 0;       // Timer to decrement invul
    private bool isInvul = false;
    private float staggeredTimer = 0;
    private bool isStaggered = false;
    private float respawnTimer = 0;
    private bool isPlayerDead = false;

    #endregion


    // Private Methods
    // ------------------------------------
    #region private Methods

    private void Start()
    {
        // get rb2d
        box2d = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        
        // Get the camera
        // cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ProCamera2D>();

        // Ensures health bars are properly updated
        updateHealthBars();

        // Set respawn text
        if (player_number == 0)
        {
            P1_Respawn_Text.gameObject.SetActive(false);
        }
        else
        {
            P2_Respawn_Text.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Unity update method
    /// 
    /// Runs once every frame
    /// </summary>
    private void Update()
    {
        // Resets invl timer
        if (invulTimer >= 0 && isInvul) resetInvul();
        if (staggeredTimer >= 0 && isStaggered) resetStagger();
        if (respawnTimer >= 0 && isPlayerDead) resetRespawn();

        // Check for item pickups
        pickupItem();

    }

    // Item PIckup Methods
    // -----------------------------------------

    private void pickupItem()
    {
        // Get current vector2 location of where the attack will start
        Vector2 rayOrigin = (Vector2)transform.position + new Vector2(box2d.size.x / 2, box2d.size.y / 2);

        // Create raycast box for checking if enemy has been hit
        RaycastHit2D hit = Physics2D.BoxCast(rayOrigin, box2d.size, 0, Vector2.zero, 0, pickupsCollisionMask);

        if (hit)
        {
            // Get pickup
            itemDrop pickup = hit.transform.GetComponent<itemDrop>();

            // add on new stats
            playerHealth += pickup.restoreHealthAmount;
            playerHealthMax += pickup.permanentHealthIncreaseAmount;
            AttackStrength += pickup.permanentAttackStrengthIncreaseAmount;

            // Updates health bars
            updateHealthBars();
            

            // Destroy object
            Destroy(pickup.gameObject);
        }
    }

    // stagger invul states
    // ------------------------------------------

    /// <summary>
    /// Set invul state
    /// </summary>
    private void setInvul()
    {
        isInvul = true;
        invulTimer = invulnerabilityTimer;
    }

    /// <summary>
    /// Sets stagger state
    /// </summary>
    private void setStagger()
    {
        staggeredTimer = staggerTimer;
        isStaggered = true;
    }

    /// <summary>
    /// Sets player to dead, starts respawn timer
    /// </summary>
    private void setRespawn()
    {
        respawnTimer = playerRespawnTimer;
        isPlayerDead = true;

        // Set respawn text
        if (player_number == 0)
        {
            P1_Respawn_Text.gameObject.SetActive(true);
        }
        else
        {
            P2_Respawn_Text.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Timer decrements to reset the invulnerability timer
    /// </summary>
    private void resetInvul()
    {
        // decrement timer
        invulTimer -= Time.deltaTime;

        // Set invul to false when timer runs
        if (invulTimer <= 0)
        {
            isInvul = false;
        }
    }

    /// <summary>
    /// Timer decrements to reset the stagger
    /// </summary>
    private void resetStagger()
    {
        // decrement timer
        staggeredTimer -= Time.deltaTime;
        

        // set stagger  to false is timer runs out
        if (staggeredTimer <= 0)
        {
            isStaggered = false;
        }
    }

    /// <summary>
    /// Timer decrements to allow for respawning
    /// </summary>
    private void resetRespawn()
    {
        respawnTimer -= Time.deltaTime;
        int textTimer = (int)respawnTimer;

        // Set respawn text
        if (player_number == 0)
        {
            P1_Respawn_Text.text = "Respawning in " + textTimer.ToString() + "...";
        }
        else
        {
            P2_Respawn_Text.text = "Respawning in " + textTimer.ToString() + "...";
        }

        if (respawnTimer <= 0)
        {
            respawn();
        }
        Debug.Log("Respawning in " + (int)respawnTimer);
    }

    #endregion

    // public Methods
    // ------------------------------------
    #region public Methods

    /// <summary>
    /// Player takes damage 
    /// </summary>
    /// <param name="damageToTake"></param>
    public void takeDamage(int damageToTake)
    {
        if (!isInvul && !isPlayerDead)
        {
            // player invulnerable
            setInvul();

            // player staggered
            setStagger();

            // Take damage
            playerHealth -= damageToTake;

            // Update UI
            updateHealthBars();

            // Disable player if dead
            if (playerHealth <= 0)
            {
                death();
                setRespawn();
            }
            else
            {
                audioSource.PlayOneShot(playerTakingDamage);
            }
        }
    }

    /// <summary>
    /// Restores health based on an item the player picks up
    /// </summary>
    /// <param name="healthToRestore"></param>
    public void restoreHealth(int healthToRestore)
    {
        // if player picks up health restore item and is low enough health to restore it
        if (playerHealth < playerHealthMax)
        {
            // destroy pickup, restore health
            // Player can overheal a little bit
            playerHealth += healthToRestore;
        }
    }

    /// <summary>
    /// Sets called if player dies
    /// </summary>
    public void death()
    {
        isPlayerDead = true;
        // Kill player
        Debug.Log("player died, respawning player");

        // play death sound
        audioSource.PlayOneShot(playerDeathSound);

        // Remove player from camera
        // cam.RemoveCameraTarget(this.transform);

        if (player_number == 0)
        {
            P1_healthTextHolder.SetActive(false);
        }
        else
        {
            P2_healthTextHolder.SetActive(false);
        }
    }

    /// <summary>
    /// respawns player when they've died
    /// </summary>
    public void respawn()
    {
        Debug.Log("Respawning Player now");
        // Return player object from dead
        //gameObject.SetActive(true);
        isPlayerDead = false;

        // restore health
        playerHealth = playerHealthMax;
        if (player_number == 0)
        {
            P1_healthTextHolder.SetActive(true);
        }
        else
        {
            P2_healthTextHolder.SetActive(true);
        }
        updateHealthBars();

        // Set respawn text
        if (player_number == 0)
        {
            P1_Respawn_Text.gameObject.SetActive(false);
        }
        else
        {
            P2_Respawn_Text.gameObject.SetActive(false);
        }

        // set invulnerable
        setInvul();

        // move player next to partner
        transform.position = partner.transform.position + Vector3.up;

        // add player to camera
        // cam.AddCameraTarget(this.transform);
    }

    /// <summary>
    /// Updates the player's health bars when called
    /// </summary>
    public void updateHealthBars()
    {
        // Updates health bars
        if (player_number == 0)
        {
            P1_Health_Bar.UpdateBar(playerHealth, playerHealthMax);
            P1_curHPText.text = playerHealth.ToString();
            P1_MaxHPText.text = playerHealthMax.ToString();
            p1_strText.text = AttackStrength.ToString();
        }
        else
        {
            P2_Health_Bar.UpdateBar(playerHealth, playerHealthMax);
            P2_curHPText.text = playerHealth.ToString();
            P2_maxHPText.text = playerHealthMax.ToString();
            p2_strText.text = AttackStrength.ToString();
        }
    }


    #endregion


    #region getters/setters

    /// <summary>
    /// Returns if player is invulnerable
    /// </summary>
    public bool playerInvulnerable
    {
        get { return isInvul; }
    }

    /// <summary>
    /// Returns if player is staggered
    /// </summary>
    public bool playerStaggered
    {
        get { return isStaggered; }
    }

    /// <summary>
    /// Returns if is dead
    /// </summary>
    public bool PlayerDied
    {
        get { return isPlayerDead; }
    }

    public int currentPlayerNumber
    {
        set { player_number = value; }
    }
    #endregion
}
