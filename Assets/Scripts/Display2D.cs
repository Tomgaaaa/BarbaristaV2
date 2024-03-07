using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Display2D : MonoBehaviour
{
    public string vnTag => inkTag;

    [SerializeField] protected string inkTag;

    public SpriteRenderer mainImage { get; private set; }

    // Start is called before the first frame update
    float maxAlpha;

    protected virtual void Awake()
    {
        if (string.IsNullOrEmpty(inkTag))
            inkTag = gameObject.name;

        mainImage = GetComponent<SpriteRenderer>();
        mainImage.enabled = false;
        maxAlpha = mainImage.color.a;
    }

    public virtual void FadeIn(float time)
    {
        StartCoroutine(ChangeAlpha(0f, maxAlpha, time));
    }

    public virtual void FadeOut(float time)
    {
        StartCoroutine(ChangeAlpha(mainImage.color.a, 0f, time));
    }

    public virtual void FlipX()
    {
        mainImage.flipX = !mainImage.flipX;
    }

    public virtual void FlipY()
    {
        mainImage.flipY = !mainImage.flipY;
    }

    public virtual void Show()
    {
        mainImage.enabled = true;
    }

    public virtual void Hide()
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

    public void Move(Transform target, float duration)
    {
        Vector3 origin = transform.position;
        Vector3 destination = target.position;

        StartCoroutine(MoveToPosition(transform, origin, destination, duration));
    }

    public void Move(Vector3 destination, float duration)
    {
        Vector3 origin = transform.position;

        StartCoroutine(MoveToPosition(transform, origin, destination, duration));
    }

    static IEnumerator MoveToPosition(Transform t, Vector3 pos, Vector3 des, float duration)
    {
        float ellapsed = 0;

        while (ellapsed < duration)
        {
            ellapsed += Time.deltaTime;
            t.position = Vector3.Lerp(pos, des, ellapsed / duration);

            yield return null;
        }

        t.position = des;
    }
}
