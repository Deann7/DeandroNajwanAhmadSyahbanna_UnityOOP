using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(HitboxComponent))]
public class Invincibility : MonoBehaviour
{
    [SerializeField] private int blinkingCount = 7;
    [SerializeField] private float blinkInterval = 0.1f;
    [SerializeField] private Material blinkMaterial; 
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    public bool isInvincible = false;

    private void Awake()
    {
        // Mengambil komponen SpriteRenderer dan menyimpan material aslinya
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    public void StartInvincibility()
    {
        if (!isInvincible)
        {
            StartCoroutine(InvincibilityBlink());
        }
    }
    private IEnumerator InvincibilityBlink()
    {

        for (int i = 0; i < blinkingCount; i++)
        {
            spriteRenderer.material = blinkMaterial;
            yield return new WaitForSeconds(blinkInterval);

            spriteRenderer.material = originalMaterial;
            yield return new WaitForSeconds(blinkInterval);
        }

        isInvincible = false; // Invincibility selesai
    }
}
