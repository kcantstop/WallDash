using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFlicker : MonoBehaviour
{
    [SerializeField] private float flickerInterval = 0.1f;
    [SerializeField] private float flickerDuration = 1.5f;

    public void StartFlicker()
    {
        StartCoroutine(FlickerRoutine());
    }

    private IEnumerator FlickerRoutine()
    {
        
        List<SpriteRenderer> renderers = new List<SpriteRenderer>();
        renderers.Add(GetComponent<SpriteRenderer>());

        TrailDelay[] allSegments = FindObjectsByType<TrailDelay>(FindObjectsSortMode.None);
        foreach (TrailDelay segment in allSegments)
        {
            if (segment.OwnerName == gameObject.name)
            {
                renderers.Add(segment.GetComponent<SpriteRenderer>());
            }
        }

        float elapsed = 0f;
        bool visible = false;

        while (elapsed < flickerDuration)
        {
            foreach (SpriteRenderer r in renderers)
            {
                if (r != null) r.enabled = visible;
            }
            visible = !visible;

            yield return new WaitForSeconds(flickerInterval);
            elapsed += flickerInterval;
        }
    }
}