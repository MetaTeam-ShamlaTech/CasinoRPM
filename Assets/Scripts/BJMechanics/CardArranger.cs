using System.Collections;
using UnityEngine;

public class CardArranger : MonoBehaviour
{
    private float progress, elapsedTime;
    private const float DURATION = 0.175f;

    public void ArrangeCards(float offset)
    {
        StartCoroutine(PositionLerp(transform.position, transform.position + (Vector3.right * offset)));
    }

    private IEnumerator PositionLerp(Vector3 startPos, Vector3 endPos)
    {
        progress = elapsedTime = 0;
        while (progress < 1)
        {
            elapsedTime += Time.deltaTime;
            progress = elapsedTime / DURATION;
            transform.position = Vector3.Lerp(startPos, endPos, progress);
            yield return null;
        }
        transform.position = endPos;
        yield return null;
    }
}
