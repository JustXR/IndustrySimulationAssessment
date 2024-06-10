using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObjects : MonoBehaviour
{
    public List<Transform> objectsReactingToBasses, objectsReactingToNB, objectsReactingToMiddles, objectsReactingToHighs;
    [SerializeField] float t = 0.1f;

    void FixedUpdate()
    {
        makeObjectsShakeScale();
    }

    void makeObjectsShakeScale()
    {
        foreach (Transform obj in objectsReactingToBasses)
        {
            float scaleValue = MusicManager.instance.getFrequenciesDiapason(0, 2, 10);
            Debug.Log("Bass Scale Value: " + scaleValue);
            obj.localScale = Vector3.Lerp(obj.localScale, new Vector3(scaleValue, scaleValue, scaleValue), t);
        }

        foreach (Transform obj in objectsReactingToNB)
        {
            float scaleValue = MusicManager.instance.getFrequenciesDiapason(2, 4, 100);
            Debug.Log("NB Scale Value: " + scaleValue);
            obj.localScale = Vector3.Lerp(obj.localScale, new Vector3(scaleValue, scaleValue, scaleValue), t);
        }

        foreach (Transform obj in objectsReactingToMiddles)
        {
            float scaleValue = MusicManager.instance.getFrequenciesDiapason(4, 6, 200);
            Debug.Log("Middle Scale Value: " + scaleValue);
            obj.localScale = Vector3.Lerp(obj.localScale, new Vector3(scaleValue, scaleValue, scaleValue), t);
        }

        foreach (Transform obj in objectsReactingToHighs)
        {
            float scaleValue = MusicManager.instance.getFrequenciesDiapason(6, 8, 1000);
            Debug.Log("High Scale Value: " + scaleValue);
            obj.localScale = Vector3.Lerp(obj.localScale, new Vector3(scaleValue, scaleValue, scaleValue), t);
        }
    }

}

