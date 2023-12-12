using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Heart : MonoBehaviour
{
    [SerializeField] private float _lerpDuration;

    private Image _image;
    private Coroutine _filling;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.fillAmount = 1;
    }

    private void OnDestroy()
    {
        StopFilling();
    }

    public void ToFill()
    {
        StopFilling();
        _filling = StartCoroutine(Filling(0, 1, _lerpDuration, Fill));
    }

    public void ToEmpty()
    {
        StopFilling();
        _filling = StartCoroutine(Filling(1, 0, _lerpDuration, Destroy));
    }

    private void StopFilling()
    {
        if (_filling != null)
            StopCoroutine(_filling);
    }

    private void Destroy(float value)
    {
        _image.fillAmount = value;
        Destroy(gameObject);
    }

    private void Fill(float value)
    {
        _image.fillAmount = value;
    }

    private IEnumerator Filling(float startValue, float endValue, float duration,
        UnityAction<float> lerpingEnd)
    {
        var waitForFixedUpdate = new WaitForFixedUpdate();
        float elapsed = 0;
        float nextValue;

        while (elapsed < duration)
        {
            nextValue = Mathf.Lerp(startValue, endValue, elapsed / duration);
            _image.fillAmount = nextValue;
            elapsed += Time.fixedDeltaTime;
            yield return waitForFixedUpdate;
        }

        lerpingEnd.Invoke(endValue);
    }
}