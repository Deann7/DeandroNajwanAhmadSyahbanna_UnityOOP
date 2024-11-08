using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
   [SerializeField] private Weapon weaponHolder;
   private Weapon weapon;

   void Awake() 
   {
       if (weaponHolder != null)
       {
           weapon = Instantiate(weaponHolder, transform.position, transform.rotation);
           weapon.gameObject.SetActive(false);
       }
       else
       {
           Debug.LogWarning("No weapon holder assigned");
       }
   }

   void Start()
   {
       if (weapon != null)
       {
           SetWeaponVisibility(false);
       }
   }

   void OnTriggerEnter2D(Collider2D other)
   {
       if (other.CompareTag("Player"))
       {
           Debug.Log("Weapon pickup triggered");

           if (weapon != null)
           {
               Weapon existingWeapon = other.GetComponentInChildren<Weapon>();
               
               if (existingWeapon == null)
               {
                   Weapon spawnedWeapon = Instantiate(weapon, other.transform.position, Quaternion.identity);
                   spawnedWeapon.transform.SetParent(other.transform);
                   spawnedWeapon.transform.localPosition = new Vector3(0, 0, 1);
                   spawnedWeapon.gameObject.SetActive(true);
               }
               else
               {
                   existingWeapon.gameObject.SetActive(false); 
               }
           }
           else
           {
               Debug.LogWarning("No weapon to pickup");
           }
       }
   }

   void SetWeaponVisibility(bool visible)
   {
       if (weapon != null)
       {
           weapon.gameObject.SetActive(visible);
       }
       else
       {
           Debug.LogWarning("No weapon assigned");
       }
   }
}