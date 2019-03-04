using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    public CanvasGroup group;
    public Page parentPage;
    public bool interactable
    {
        get { return group.interactable; }
        set
        {
            group.interactable = value;
        }
    }

    public void Awake()
    {
        group = GetComponent<CanvasGroup>();
    }

    protected virtual void OnPageEnter() { }
    protected virtual void OnPageExit() { }

    public void LeavePage(float duration, float exitScale, AnimationCurve curve)
    {
        OnPageExit();

        group.alpha = 1f;
        transform.localScale = Vector3.one;

        StartCoroutine(FadeAndScale(duration, 0f, exitScale, true, curve));
    }

    public void EnterPage(float duration, float startingScale, AnimationCurve curve)
    {
        OnPageEnter();

        group.alpha = 0f;
        transform.localScale = Vector3.one * startingScale;

        StartCoroutine(FadeAndScale(duration, 1f, 1f, false, curve));
    }

    private IEnumerator FadeAndScale(float duration, float targetAlpha, float targetScale, bool disable, AnimationCurve curve)
    {
        interactable = false;
        float remainingDuration = duration, alpha = group.alpha, scale = transform.localScale.x;
        
        while(remainingDuration > 0f)
        {
            float currentAlpha = Mathf.Lerp(alpha, targetAlpha, curve.Evaluate(1f - (remainingDuration / duration)));
            float currentScale = Mathf.Lerp(scale, targetScale, curve.Evaluate(1f - (remainingDuration / duration)));
            group.alpha = currentAlpha;
            transform.localScale = Vector3.one * currentScale;
            remainingDuration -= Time.deltaTime;
            yield return null;
        }
        interactable = true;
        group.alpha = targetAlpha;
        transform.localScale = Vector3.one * targetScale;

        gameObject.SetActive(!disable);
    }
}
