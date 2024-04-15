using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    #region enums
    public enum ENEMY_FOLLOW_TYPES
    {
        FOLLOW_PLAYER,
        RUN_AWAY_FROM_PLAYER,
        SlIDE_AWAY_FROM_PLAYER,
        TOKYO_DRIFT
    };

    #endregion

    #region Variables

    // Public Variables
    // ------------------------------------
    [Header("Combat Circle")]
    public Circle attackCircle;         // Circle for how calculating the attack range
                                        // Radius = attack range, Positon = circle + obj positon
    public Circle followCircle;
    public LayerMask collisionMask;
    public ENEMY_FOLLOW_TYPES followType;


    // private variables
    // component references
    private EnemyAttributes enemyAttributes;
    private Rigidbody2D rb2d;

    // timers
    private float attackTimer = 0;
    private bool timerStart = false;
    private bool canAttack = false;


    #endregion

    /// <summary>
    /// Unity start method
    /// 
    /// runs at initialization
    /// </summary>
    private void Start()
    {
        enemyAttributes = GetComponent<EnemyAttributes>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Unity update method
    /// 
    /// runs once per framce
    /// </summary>
    private void Update()
    {
        // initiates attack
        initiateAttack();
        followPlayer();
    }

    /// <summary>
    /// Unity fixed update
    /// 
    /// Runs every frame at a fixed iterval
    /// </summary>
    private void FixedUpdate()
    {
        followPlayer();
    }



    // Combat Methods
    // --------------------------------------------

    /// <summary>
    /// Sets timer to max value
    /// </summary>
    private void setTimer()
    {
        attackTimer = enemyAttributes.atkSpeed;
        canAttack = false;
        timerStart = true;
    }

    /// <summary>
    /// Decrements timer
    /// </summary>
    private void resetTimer()
    {
        attackTimer -= Time.deltaTime;
        Debug.Log("Attack in " + (int)attackTimer);

        // If timer runs out, the enemy can attack
        if (attackTimer <= 0)
        {
            canAttack = true;
            timerStart = false;
        }
    }

    /// <summary>
    /// manually restarts timer;
    /// </summary>
    private void restartTimer()
    {
        attackTimer = 0;
        canAttack = false;
        timerStart = false;
    }

    /// <summary>
    /// FOllows player within a circle cast
    /// </summary>
    private void followPlayer()
    {
        //create circle cast
        // get player position
        // go to positon every update frame
        // Creates circleCast at player
        Vector2 rayOrigin = (Vector2)transform.position + followCircle.Position;
        RaycastHit2D[] hit = Physics2D.CircleCastAll(rayOrigin, followCircle.Radius, Vector3.zero, 0, collisionMask);

        // Get closest player
        if (hit.Length > 0)
        {
            if (Vector2.Distance(hit[0].transform.position, transform.position) > 2)
            {
                if (followType == ENEMY_FOLLOW_TYPES.FOLLOW_PLAYER)
                {
                    Vector3 direction = hit[0].transform.position - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    rb2d.rotation = angle;
                    direction.Normalize();
                    rb2d.MovePosition(transform.position + (direction * enemyAttributes.enemyMoveSpeed * Time.fixedDeltaTime));
                }
                else if (followType == ENEMY_FOLLOW_TYPES.TOKYO_DRIFT)
                {
                    transform.LookAt(hit[0].transform);
                    Vector2 tempMove = transform.position - (hit[0].transform.position * enemyAttributes.enemyMoveSpeed * Time.fixedDeltaTime);
                    rb2d.MovePosition(tempMove);
                }
                else if (followType == ENEMY_FOLLOW_TYPES.SlIDE_AWAY_FROM_PLAYER)
                {
                    transform.LookAt(hit[0].transform);
                    Vector2 tempMove = transform.position + (hit[0].transform.position * enemyAttributes.enemyMoveSpeed * Time.fixedDeltaTime);
                    rb2d.MovePosition(tempMove);
                }
                else if (followType == ENEMY_FOLLOW_TYPES.RUN_AWAY_FROM_PLAYER)
                {
                    Vector3 direction = hit[0].transform.position - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    rb2d.rotation = angle;
                    direction.Normalize();
                    rb2d.MovePosition(transform.position - (direction * enemyAttributes.enemyMoveSpeed * Time.fixedDeltaTime));

                }

                // Returns rotation back to 0
                rb2d.rotation = 0;
            }
            
        }

    }

    /// <summary>
    /// Initiates attack
    /// </summary>
    private void initiateAttack()
    {
        // Creates circleCast at player
        Vector2 rayOrigin = (Vector2)transform.position + attackCircle.Position;
        RaycastHit2D[] hit = Physics2D.CircleCastAll(rayOrigin, attackCircle.Radius, Vector3.zero, 0, collisionMask);

        // if players found
        if (hit.Length > 0)
        {
            Debug.Log("Player found");
            // if timer not started, start timer
            if (!timerStart && !canAttack)
            {
                Debug.Log("Start delay attack timer");
                // sets atack delay timer
                setTimer();
            }
            // decrement timer
            else if (timerStart && !canAttack)
            {
                Debug.Log("Decrement delay attack timer");
                resetTimer();
            }
            // if can attack
            else if(!timerStart && canAttack)
            {
                Debug.Log("Attacking player");

                enemyAttributes.audioSource.PlayOneShot(enemyAttributes.attack);

                for (int i = 0; i < hit.Length; i++)
                {
                    restartTimer();
                    hit[i].transform.GetComponent<PlayerAttributesController>().takeDamage(enemyAttributes.atkStrength);
                }
            }
        }
    }


}
