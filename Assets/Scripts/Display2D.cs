using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Display2D : MonoBehaviour
{
    public SpriteRenderer mainImage { get; private set; }

    // Start is called before the first frame update
    float maxAlpha;

    protected virtual void Awake()
    {
        mainImage = GetComponent<SpriteRenderer>();
        mainImage.enabled = false;
        maxAlpha = mainImage.color.a;
    }

    public void FadeIn(float time)
    {
        StartCoroutine(ChangeAlpha(0f, maxAlpha, time));
    }

    public void FadeOut(float time)
    {
        StartCoroutine(ChangeAlpha(mainImage.color.a, 0f, time));
    }

    public void FlipX()
    {
        mainImage.flipX = !mainImage.flipX;
    }

    public void FlipY()
    {
        mainImage.flipY = !mainImage.flipY;
    }

    public void Show()
    {
        mainImage.enabled = true;
    }

    public void Hide()
    {
        mainImage.enabled = false;
    }

    private IEnumerator ChangeAlpha(float start, float destination, float duration)
    {
        duration = Mathf.Max(Mathf.Epsilon, duration);
        float ellapsed = 0;
        Color mainColor = mainImage.color;

        mainImage.enabled = true;

        while (ellapsed <= duration)
        {
            ellapsed += Time.deltaTime;

            mainImage.color = SetAlpha(mainColor, Mathf.Lerp(start, destination, ellapsed / duration));

            yield return null;
        }

        mainImage.color = SetAlpha(mainColor, destination);
        mainImage.enabled = destination > 0;

    }

    private Color SetAlpha(Color c, float a)
    {
        return new Color(c.r, c.g, c.b, a);
    }


}
