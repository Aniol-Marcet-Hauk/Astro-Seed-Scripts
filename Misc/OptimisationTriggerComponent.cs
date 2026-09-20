using UnityEngine;

public class OptimisationTriggerComponent : MonoBehaviour
{
    public GameObject[] objectsTurnOn;

    [SerializeField] private OptimisationTriggerComponent turnOff;
    [SerializeField] private OptimisationTriggerActivatePartsOfLevel parent;

    private int myArray = -1;
    private int turnOffArray = -1;
    private bool state = false;

    public bool isSetActiveWhenStart = true;
    public bool backTracker = false;
    public float timeBeforeAdd = 0.1f;
    public Collider2D[] listOfColl;
    private void Start()
    {
        Invoke(nameof(Register), timeBeforeAdd);
    }

    private void Register()
    {
        if (parent == null)
        {
            Debug.LogError($"Missing Parent on {gameObject.name}");
            return;
        }

        myArray = parent.AddToTurnActive(this);

        if (turnOff != null)
            turnOffArray = turnOff.GetMyArrayIndex();

        if (!isSetActiveWhenStart)
            gameObject.SetActive(false);
    }

    public int GetMyArrayIndex() => myArray;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!state && myArray != -1)
            TurnOnObjects();
    }

    public void TurnOnObjects()
    {
        Debug.Log(turnOffArray);
        if (turnOff != null)
            parent.TurnOfforOnAtIndex(turnOffArray, false);

        if (myArray != -1)
            parent.TurnOfforOnAtIndex(myArray, true);

        
    }

    public void SetState(bool set) => state = set;

    private void OnEnable()
    {
        if (myArray != -1)
            parent.ChangeSetActiveSelf(myArray, true);
    }
    private void OnDisable()
    {
        if (myArray != -1)
            parent.ChangeSetActiveSelf(myArray, false);
    }

}
