using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkIfWrongObj : RoomEntity
{
    public override void Choice(bool isCorrectChoice)
    {
        base.Choice(isCorrectChoice);
        {
            if(!isCorrectChoice)
            {
                StartCoroutine(ShrinkObject());
            }
        }
    }

    private IEnumerator ShrinkObject()
    {
        float duration = 1.0f; // Duration of the shrink effect
        float elapsed = 0.0f;
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * 0.01f; // Shrink to 10% of original size
        while (elapsed < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
        Destroy(this.gameObject);
    }
}
