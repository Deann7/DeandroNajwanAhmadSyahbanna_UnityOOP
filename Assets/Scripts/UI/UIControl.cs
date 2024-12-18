using UnityEngine;
using UnityEngine.UIElements;

public class UIControl : MonoBehaviour
{
    public CombatManager combatManager;
    public GameObject playerObject;
    private HealthComponent playerHealthComponent;

    private VisualElement root;
    private Label healthLabel;
    private Label pointsLabel;
    private Label waveLabel;
    private Label totalEnemiesLabel;

    public int totalPoints = 0;

    void Start()
    {
        FindRequiredComponents();
        SetupUIElements();
    }

    void Update()
    {
        if (root != null)
        {
            UpdateUIElements();
        }
    }

    void FindRequiredComponents()
    {
        
        try 
        {
            combatManager = FindObjectOfType<CombatManager>();
            playerObject = GameObject.FindGameObjectWithTag("Player");
            
            if (playerObject != null)
            {
                playerHealthComponent = playerObject.GetComponent<HealthComponent>();
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error finding required components: {e.Message}");
        }
    }

    void SetupUIElements()
    {
        try 
        {

            var uiDocument = GetComponent<UIDocument>();
            if (uiDocument != null)
            {
                root = uiDocument.rootVisualElement;
                
                healthLabel = root?.Q<Label>("Health");
                pointsLabel = root?.Q<Label>("Point");
                waveLabel = root?.Q<Label>("Wave");
                totalEnemiesLabel = root?.Q<Label>("TotalEnemies");
            }
            else 
            {
                Debug.LogError("UIDocument component not found!");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error setting up UI elements: {e.Message}");
        }
    }

    public void UpdateUIElements()
    {
        try 
        {
            if (playerHealthComponent != null && healthLabel != null)
            {
                healthLabel.text = $"Health: {playerHealthComponent.health}";
            }

            if (pointsLabel != null)
            {
                pointsLabel.text = $"Points: {totalPoints}";
            }

      
            if (combatManager != null && waveLabel != null)
            {
                waveLabel.text = $"Wave: {combatManager.waveNumber}";
            }

            if (combatManager != null && totalEnemiesLabel != null)
            {
                totalEnemiesLabel.text = $"Enemies Left: {combatManager.totalEnemies}";
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error updating UI elements: {e.Message}");
        }
    }
}