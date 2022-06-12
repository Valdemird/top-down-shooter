using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private IEnumerator Shake(float duration, float magnitude) {
        Vector3 originalPos = transform.localPosition;
        float elapseTime = 0f;
        while (elapseTime < duration) {
            float xOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            float yOffset = Random.Range(-0.5f, 0.5f) * magnitude;

            transform.localPosition = new Vector3(xOffset, yOffset, transform.localPosition.z);
            elapseTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }

    public void StartSake() {
        StartCoroutine(Shake(.05f, .01f));
    }
}
