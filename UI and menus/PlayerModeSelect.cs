
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class PlayerModeSelect : MonoBehaviour,

 
    IPointerClickHandler,
    ISubmitHandler
{

    public int mode = 0;
    public TMP_Text text,text2;
    public Color colorNorm, colorSelected;
    public GameObject turnOn;
    private bool selected = false;
    private InputManager input;
    [Space]
    public Color Color1Player;
    private void Start()
    {
        input = InputManager.instance;
    }
    public void OnSubmit(BaseEventData eventData)
    {
        Select();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Select();
    }

    void Select()
    {
       
       
        input.playerMode = mode;
        if(mode == 0)
        {
            GameManager.gameManager.colorLeft= Color1Player;
            GameManager.gameManager.colorRight = Color1Player;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(selected && input.playerMode != mode)
        {
            selected = false;
            text.color = colorNorm;
            text2.color = colorNorm;
            turnOn.SetActive(false);
        }
        else if(!selected && input.playerMode == mode)
        {
            selected = true;
            text.color = colorSelected;
            text2.color = colorSelected;
            turnOn.SetActive(true);
        }
    }
}
