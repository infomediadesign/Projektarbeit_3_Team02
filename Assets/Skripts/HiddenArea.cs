using System.Collections;
using UnityEngine;

public class HiddenArea : MonoBehaviour
{
    public float fadeDuration = 1f;
    SpriteRenderer spriteRenderer;
    Color hiddenColor;
    Coroutine currentCoroutine;

    private bool hasDiscovered = false; //checks if hidden area was already discovered by the player


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hiddenColor = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(currentCoroutine != null)        //new
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(FadeSprite(true));
            //SoundManager.Instance.PlaySound2D("HiddenArea1");

            //Play sound based on wether the area was discovered already
            if (!hasDiscovered)
            {
                SoundManager.Instance.PlaySound2D("HiddenArea1");
                hasDiscovered = true;
            }
            else
            {
                SoundManager.Instance.PlaySound2D("HiddenArea2");
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(currentCoroutine != null)        //stop existing fade coroutine
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(FadeSprite(false));
        }
    }

   private IEnumerator FadeSprite(bool fadeOut)
   {
        Color startColor = spriteRenderer.color;
        Color targetColor = fadeOut ? new Color(hiddenColor.r, hiddenColor.g, hiddenColor.b, 0f) : hiddenColor;     //0f transparent color
        float timeFading = 0f;

        while(timeFading < fadeDuration)
        {
            spriteRenderer.color = Color.Lerp(startColor, targetColor, timeFading / fadeDuration);
            timeFading += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = targetColor;
   }
}
