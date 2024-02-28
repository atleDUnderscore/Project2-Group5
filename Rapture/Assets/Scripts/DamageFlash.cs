using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float duration;

    private SpriteRenderer spriteRenderer;

    private Material originalMaterial;

    private Coroutine flashRoutine;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        originalMaterial = spriteRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator FlashRoutine()
    {
        //Swap to Flash material.
        spriteRenderer.material = flashMaterial;

        // Pause function for duration.
        yield return new WaitForSeconds(duration);

        // After pause, swap back to original material.
        spriteRenderer.material = originalMaterial;

        // Set the routine to null, signalling that it is finished.
        flashRoutine = null;
    }

    public void Flash()
    {
        // If the coroutine is not null, it is running.
        if(flashRoutine != null)
        {
            //Stop routine to prevent multi-flash bugs.
            StopCoroutine(flashRoutine);
        }

        //Start Coroutine and store reference.
        flashRoutine = StartCoroutine(FlashRoutine());
    }
}
