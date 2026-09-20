using System.Collections;
using UnityEngine;

public class disappear : MonoBehaviour
{
    public float TimeStart;
    public float Times;
    public GameObject Trans;
    public float A;
    public float buffer = 0.7f;
    private void Start()
    {
        StartCoroutine("app");
    }
    IEnumerator app()
    {
        yield return new WaitForSeconds(TimeStart);
        while (true)
        {
            Trans.SetActive(false);
            yield return new WaitForSeconds(Times);
            Trans.SetActive(true);
            yield return new WaitForSeconds(Times+buffer);
            
        }

        //-(A-1)*0.3f
    }
}
