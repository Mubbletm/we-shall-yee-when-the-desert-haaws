using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject target;
    public float intesity = .5f;
    public Vector2 boundingBox;

    public void FixedUpdate()
    {
        float adjustedIntensity = intensityCalculator(target.transform.position, boundingBox, intesity);
        Vector2 newPostion = Vector2.Lerp(transform.position, target.transform.position, adjustedIntensity);
        transform.position = new Vector3(newPostion.x, newPostion.y, transform.position.z);
    }

    public float intensityCalculator(Vector2 target, Vector2 boundingBox, float intensity)
    {
        // It's a quarter because both the x and y axis are divided, resulting in 4 pieces.
        Vector2 boundingQuarter = boundingBox / 2;
        Vector2 relativePosition = target - (Vector2) transform.position;

        float xModifier = (Mathf.Abs(relativePosition.x) / boundingQuarter.x);
        float yModifier = (Mathf.Abs(relativePosition.y) / boundingQuarter.y);

        return Mathf.Lerp(0, intensity, Mathf.Max(xModifier, yModifier));
    }
}
