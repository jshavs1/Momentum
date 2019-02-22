using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    public CanvasGroup group;
    public Page parentPage;

    public void Awake()
    {
        group = GetComponent<CanvasGroup>();
    }

    public void LeavePage(float duration, float exitScale)
    {
        group.alpha = 1f;
        transform.localScale = Vector3.one;

        StartCoroutine(FadeAndScale(duration, 0f, exitScale, true));
    }

    public void EnterPage(float duration, float startingScale)
    {
        enabled = true;
        group.alpha = 0f;
        transform.localScale = Vector3.one * startingScale;

        StartCoroutine(FadeAndScale(duration, 1f, 1f, false));
    }

    private IEnumerator FadeAndScale(float duration, float targetAlpha, float targetScale, bool disable)
    {
        float remainingDuration = duration, alpha = group.alpha, scale = transform.localScale.x;
        
        while(remainingDuration > 0f)
        {
            float currentAlpha = Mathf.Lerp(targetAlpha, alpha, remainingDuration / duration);
            float currentScale = Mathf.Lerp(targetScale, scale, remainingDuration / duration);
            group.alpha = currentAlpha;
            transform.localScale = Vector3.one * currentScale;
            remainingDuration -= Time.deltaTime;
            yield return null;
        }
        group.alpha = targetAlpha;
        transform.localScale = Vector3.one * targetScale;

        gameObject.SetActive(!disable);
    }
}
