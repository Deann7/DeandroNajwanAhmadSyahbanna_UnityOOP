using UnityEngine;

public class EnemyBoss : Enemy
{
    [Header("Movement")]
    [SerializeField] private float leftBound = -10f;
    [SerializeField] private float rightBound = 10f;
    [SerializeField] private float spawnInterval = 15f;
    [SerializeField] private float minSpawnY = 3f;
    [SerializeField] private float maxSpawnY = 5f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private int maxBosses = 1;

    [Header("Weapon System")]
    [SerializeField] private Weapon weaponPrefab;
    private Transform weaponMount;
    [SerializeField] private Vector3 weaponMountOffset = new Vector3(0.5f, 1f, -1f); // Offset from boss center

    private static int bossCount = 0;
    private bool movingLeft = true;
    private Weapon equippedWeapon;

    private void Awake()
    {
        CreateWeaponMount();
    }

    private void CreateWeaponMount()
    {
        weaponMount = transform.Find("WeaponMount");
        
        if (weaponMount == null)
        {
            GameObject mountObj = new GameObject("WeaponMount");
            weaponMount = mountObj.transform;
            weaponMount.SetParent(transform);
            weaponMount.localPosition = (Vector3)weaponMountOffset;
            weaponMount.localRotation = Quaternion.Euler(0, 0, 180);
        }
    }

    private void Start()
    {
        if (bossCount < maxBosses)
        {
            bossCount++;
            InvokeRepeating(nameof(SpawnBoss), 0f, spawnInterval);
            EquipWeapon();
        }
    }

    private void EquipWeapon()
    {
        if (weaponPrefab != null && weaponMount != null)
        {
            equippedWeapon = Instantiate(weaponPrefab, weaponMount.position, weaponMount.rotation, weaponMount);
            equippedWeapon.parentTransform = transform;
            equippedWeapon.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else if (weaponPrefab == null)
        {
            Debug.LogWarning("WeaponPrefab not assigned to EnemyBoss!");
        }
    }

    private void Update()
    {
        MoveEnemy();
        CheckScreenBounds();
    }

    private void SpawnBoss()
    {
        if (bossCount >= maxBosses)
            return;

        float spawnX = movingLeft ? rightBound : leftBound;
        float spawnY = Random.Range(minSpawnY, maxSpawnY);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        EnemyBoss newBoss = Instantiate(this, spawnPosition, Quaternion.identity);
        newBoss.movingLeft = !movingLeft;
    }

    private void MoveEnemy()
    {
        if (movingLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    private void CheckScreenBounds()
    {
        if (transform.position.x <= leftBound)
        {
            movingLeft = false;
        }
        else if (transform.position.x >= rightBound)
        {
            movingLeft = true;
        }
    }

    private void OnDestroy()
    {
        bossCount--;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 mountPosition = transform.position + (Vector3)weaponMountOffset;
        Gizmos.DrawWireSphere(mountPosition, 0.2f);
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(mountPosition, Vector3.down * 2f);
    }
}