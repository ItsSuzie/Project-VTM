using UnityEngine;
using TMPro;

[System.Serializable]
public class itemDrops
{
    public itemDrop itemDrop;
    public float itemDropRate;
}

public class EnemyAttributes : MonoBehaviour
{

    // Variables
    // ------------------------------
    #region Variables

    // Public Variables
    public int curHealth;           // The current enemy health
    public int maxHealth;           // The max enemy health
    public int atkStrength;         // How strong the enemy is 
    public float atkSpeed;          // How fast the enemy can attack per second
    public int physDefense;         // How much damage the enemy will resist
    public float enemyMoveSpeed;

    [Header("World space UI")]
    public GameObject uiPrefab;
    public Vector3 uiPrefabSpawnLoc;
    public RectTransform healthCanvasParent;

    private SimpleHealthBar enemyHealthBar;
    private TextMeshProUGUI curHP;
    private TextMeshProUGUI maxHP;
    private RectTransform uiTransform;
    public GameObject uiPrefabClone;

    [Header("Sounds")]
    public AudioClip attack;
    public AudioClip takingDamage;
    public AudioClip death;

    [Header("Item Drops")]
    public itemDrops[] itemDrops;

    [HideInInspector] public AudioSource audioSource;

    // private variables
    // component references
    private SpriteRenderer sprite;
    private BoxCollider2D box2d;

    #endregion

    // Private methods
    // ------------------------------
    #region private methods

    /// <summary>
    /// Unity start method
    /// 
    /// RUns at initialization
    /// </summary>
    private void Start()
    {
        // Sets health value
        curHealth = maxHealth;

        // component
        audioSource = GetComponent<AudioSource>();
        box2d = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();


        healthCanvasParent = GameObject.FindGameObjectWithTag("enemyUIHealthParent").GetComponent<RectTransform>();
        // create ui
        // -------------------------------------------------------------------------------------
        // Gets, instantiates the UI prefab at above the actor and sets the parent to the world space canvas
        uiPrefabClone = Instantiate(uiPrefab, transform.position, Quaternion.identity);
        uiTransform = uiPrefabClone.GetComponent<RectTransform>();
        uiPrefabClone.transform.SetParent(healthCanvasParent, false);
        uiTransform.position = uiPrefabSpawnLoc + transform.position;

        // Sets the respective component references for the UI 
        enemyHealthBar = uiPrefabClone.transform.GetChild(0).GetChild(0).GetComponent<SimpleHealthBar>();
        curHP = uiPrefabClone.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        maxHP = uiPrefabClone.transform.GetChild(0).GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>();
        updateEnemyUI();

    }

    private void Update()
    {
        uiTransform.position = transform.position + uiPrefabSpawnLoc;
    }

    private void updateEnemyUI()
    {
        enemyHealthBar.UpdateBar(curHealth, maxHealth);
        curHP.text = curHealth.ToString();
        maxHP.text = maxHealth.ToString();  
    }
    #endregion

    #region public Methods

    public void takeDamage(int damageToDeal)
    {

        // Calculate how much damage the enemy will take
        // If damage to deal is greater than the defense, then subtact def
        // from the damage to deal and apply that to the curHealth.
        if (damageToDeal >= physDefense)
        {
            Debug.Log(this.name + " is taking " + (damageToDeal - physDefense).ToString() + " damage");
            curHealth -= (damageToDeal - physDefense);
        }
        // Otherwise, enemy will take scratch damage
        // Or just 1 damage
        else
        {
            Debug.Log(this.name + " is taking 1 damage");
            curHealth -= 1;
        }

        updateEnemyUI();

        // Check for enemy knockout
        if (curHealth <= 0)
        {
            // randomly drop item
            itemDrops newItem = null;
            newItem = itemDrops[Random.Range(0, itemDrops.Length)];
            if (newItem != null && Random.Range(0, 100) < newItem.itemDropRate)
            {
                // Drop item
                // Instantiate item at this enemy's feet
                Instantiate(newItem.itemDrop.gameObject, transform.position, Quaternion.identity);
            }
            audioSource.PlayOneShot(death);
            box2d.enabled = false;
            sprite.enabled = false;


            Invoke("enemyDeath", 1.0f);
        }
        else
        {
            audioSource.PlayOneShot(takingDamage);
        }

    }


    public void enemyDeath()
    {
        // Destroy this object
        Destroy(uiPrefabClone);
        Destroy(this.gameObject);
    }

    #endregion
}
