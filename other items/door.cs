using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class door : MonoBehaviour
{
    [SerializeField] private Transform doorHole;

    [SerializeField] private GameObject[] objectsUnderneath,objectsTurnOff;

    private void Start()
    {
        foreach (GameObject obj in objectsUnderneath)
        {if(obj.activeSelf == true)
            {
                obj.SetActive(false);
            }
           
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "items" && collision.transform.GetComponent<Onetriggerisequal>() != null)
        {
            collision.enabled = false;
            collision.GetComponent<Rigidbody2D>().simulated = false;
            
            StartCoroutine(KeyInHole(collision.transform));
        }



    }
    IEnumerator KeyInHole(Transform _key)
    {
        
       

      
        _key.DOMove(doorHole.position, .2f);
    
        _key.DOLocalRotate(new Vector3(0,0,-transform.localEulerAngles.z), 1f);
        yield return new WaitForSeconds(2f);
        //MAYBE ADD SCREEN SHAKE HERE
        
        _key.gameObject.SetActive(false);
        transform.GetComponent<Animator>().Play("DoorOpen");
       
        transform.DOScaleX(0,.25f).SetEase(Ease.InBack);
        transform.DOLocalMove(transform.localPosition -1.2f*transform.right, .25f).SetEase(Ease.InBack);
        Invoke("SetActiveFalse", .3f);
    }
    public void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }
    public void SetUnderneathTrue()
    {
        foreach (GameObject obj in objectsUnderneath)
        {
            Debug.Log(obj.name);
            obj.SetActive(true);
        }
        foreach(GameObject obj in objectsTurnOff)
        {
            obj.SetActive(false);
        }
    }

}
