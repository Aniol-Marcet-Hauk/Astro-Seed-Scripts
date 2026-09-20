using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class TextControlScript : MonoBehaviour
{
    private TMP_Text text;
    private string textS;
    public float wait;
    public bool doDots;
    public bool isNumber;
    public int skipAfterCutscene = 0;
    private void Start()
    {
        if(skipAfterCutscene > 0 && GameManager.gameManager.cutscene >= skipAfterCutscene)
        {
            Destroy(gameObject);
            return;
        }
        text = transform.GetComponent<TMP_Text>();
        textS = text.text;
        text.text = "";

        if (isNumber)
        {
            StartCoroutine(CountToNumber(textS));
            return;
        }

        List<string> listText = new List<string>();
        listText.AddRange(textS.Select(c => c.ToString()));

        StartCoroutine(Jasthliwa(listText));

    }

    IEnumerator Jasthliwa(List<string> listText)
    {
        textS = "";
        for (int i = 0; i < listText.Count; i++)
        {
            yield return new WaitForSeconds(Random.Range(.02f, .07f));
            text.text += listText[i];
            textS += listText[i];
            SoundManager.instance?.LetterSound();
        }
        if(!doDots)
        {
            yield break;
        }
        float theLastTime = Time.time + wait;
        while(Time.time< theLastTime)
        {
            text.text = textS;
            for (int i = 0; i < 2; i++)
            {
                yield return new WaitForSeconds(.26f);
                text.text += ".";
            }

            yield return new WaitForSeconds(.4f);


        }
        Destroy(gameObject);
    }

    IEnumerator CountToNumber(string original)
    {
       
        int target;
        if (!int.TryParse(original, out target))
        {

            List<string> listText = new List<string>();
            listText.AddRange(original.Select(c => c.ToString()));
            yield return StartCoroutine(Jasthliwa(listText));
            yield break;
        }

        int digits = Mathf.Max(1, original.Length);


        float duration = (wait > 0f) ? wait : 1f;
        float elapsed = 0f;
        int lastValue = -1;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
       
            float eased = Mathf.SmoothStep(0f, 1f, t);
            int value = Mathf.RoundToInt(Mathf.Lerp(0, target, eased));
            if (value != lastValue)
            {
                text.text = value.ToString().PadLeft(digits, '0');
                SoundManager.instance?.LetterSound();
                lastValue = value;
            }
            yield return null;
        }

     
        text.text = target.ToString().PadLeft(digits, '0');

        if (!doDots)
        {
            yield break;
        }

        float theLastTime = Time.time + wait;
        string finalText = text.text;
        while (Time.time < theLastTime)
        {
            text.text = finalText;
            for (int i = 0; i < 2; i++)
            {
                yield return new WaitForSeconds(.26f);
                text.text += ".";
            }

            yield return new WaitForSeconds(.4f);
        }

        Destroy(gameObject);
    }
}
