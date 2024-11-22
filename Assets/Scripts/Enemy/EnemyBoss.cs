using UnityEngine;

public class EnemyBoss : Enemy
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    private Vector2 direction;

    [Header("Weapon Settings")]
    [SerializeField] private Weapon weaponPrefab;
    [SerializeField] private Vector3 weaponMountOffset = new Vector3(0.5f, 0.3f, 0f);

    private Transform weaponMount;
    private Weapon equippedWeapon;

    private void Awake()
    {
        CreateWeaponMount();
    }

    private void Start()
    {
        level = 3;
        PickRandomPositionAndDirection();
        EquipWeapon();
    }

    private void Update()
    {
        MoveEnemy();
        CheckViewportBounds();
    }

    private void CreateWeaponMount()
    {
        GameObject mountObj = new GameObject("WeaponMount");
        weaponMount = mountObj.transform;
        weaponMount.SetParent(transform);
        weaponMount.localPosition = weaponMountOffset;
    }

    private void EquipWeapon()
    {
        if (weaponPrefab != null && weaponMount != null)
        {
            equippedWeapon = Instantiate(weaponPrefab, weaponMount.position, weaponMount.rotation, weaponMount);
            equippedWeapon.parentTransform = transform;
            equippedWeapon.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            Debug.LogWarning("WeaponPrefab not assigned or weapon mount missing!");
        }
    }

    private void PickRandomPositionAndDirection()
    {
        Vector2 randomPosition;
        if (Random.Range(0, 2) == 0)
        {
            direction = Vector2.right;
            randomPosition = new Vector2(-0.1f, Random.Range(0.1f, 0.9f)); 
        }
        else
        {
            direction = Vector2.left;
            randomPosition = new Vector2(1.1f, Random.Range(0.1f, 0.9f)); 
        }

        transform.position = Camera.main.ViewportToWorldPoint(randomPosition) + new Vector3(0, 0, 10);
    }

    private void MoveEnemy()
    {
        transform.Translate(moveSpeed * Time.deltaTime * direction);
    }

    private void CheckViewportBounds()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        if (viewportPosition.x < -0.1f && direction == Vector2.left) 
        {
            PickRandomPositionAndDirection();
        }
        else if (viewportPosition.x > 1.1f && direction == Vector2.right) 
        {
            PickRandomPositionAndDirection();
        }
    }
}
