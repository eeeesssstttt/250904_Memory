using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CardBehavior : MonoBehaviour
{
    [SerializeField] private Vector3 scaleOnFocus = Vector3.one * 1.5f;
    [SerializeField] private float changeColorTime = 1f;
    private Vector3 normalScale;
    private Color color;
    private Color baseColor = Color.gray;
    public int IndexColor { get; private set; }
    public bool IsFaceUp { get; private set; } = false;
    // can use [SerializeField] to make property accessible in inspector.
    private CardManager manager;

    private void OnMouseEnter()
    {
        normalScale = transform.localScale;
        transform.localScale = scaleOnFocus;
    }

    private void OnMouseExit()
    {
        transform.localScale = normalScale;
    }

    private void OnMouseDown()
    // when mouse button pressed, OnMouseUp() means on release of mouse button.
    {
        manager.CardIsClicked(this);
        // sends info to CardManager so it can manage logical aspect.
    }

    public void Initialize(Color color, int indexColor, CardManager manager)
    {
        this.color = color;
        this.IndexColor = indexColor;
        this.manager = manager;

        ChangeColor(baseColor);
        IsFaceUp = false;
    }

    private void ChangeColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }

    public void FaceUp(float delay = 0f)
    {
        StartCoroutine(ChangeColorWithLerp(color, delay));
        IsFaceUp = true;
    }

    public void FaceDown(float delay = 0f)
    {
        StartCoroutine(ChangeColorWithLerp(baseColor, delay));
        IsFaceUp = false;
    }

    private IEnumerator ChangeColorWithLerp(Color color, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        float stopWatch = 0f;
        Color startColor = GetComponent<Renderer>().material.color;
        // if no [RequireComponent(typeof(<>))] must use TryGetComponent<>().
        while (stopWatch < changeColorTime)
        {
            stopWatch += Time.deltaTime;
            ChangeColor(Color.Lerp(startColor, color, stopWatch / changeColorTime));
            yield return new WaitForEndOfFrame();
            // Ensures that stopWatch is incremented once per frame.
            // yield return null; // also waits to end of frame, returns nothing = equivalent.
            // Should work without all this because in theory code runs once per frame but you never know.
        }
        ChangeColor(color);
    }
}
