using System.Collections.Generic;
using UnityEngine;

public class OptimisationTriggerActivatePartsOfLevel : MonoBehaviour
{
    private List<OptimisationTriggerComponent> turnActive;
    private List<bool> states;
    private List<bool> isSetActiveSelf;
    public float lastItemAddTime = 0f;

    private void Awake()
    {
        turnActive = new List<OptimisationTriggerComponent>();
        states = new List<bool>();
        isSetActiveSelf = new List<bool>();
        Invoke(nameof(TurnOnColl), lastItemAddTime);

    }
    public void TurnOnColl()
    {
        foreach (OptimisationTriggerComponent item in turnActive)
        {
            foreach (var col in item.listOfColl)
            {
                if (col != null)
                    col.enabled = true;
            }
        }
    }
    public int AddToTurnActive(OptimisationTriggerComponent comp)
    {
        turnActive.Add(comp);
        states.Add(false);
        isSetActiveSelf.Add(comp.isSetActiveWhenStart);

        return turnActive.Count - 1;
    }

    public void TurnOfforOnAtIndex(int index, bool state)
    {
        if (index < 0 || index >= turnActive.Count)
            return;

        var entry = turnActive[index];
        GameObject[] list = entry.objectsTurnOn;

        if (list != null)
        {
            foreach (GameObject item in list)
            {
                if (item != null)
                    item.SetActive(state);
            }
        }

        if (!entry.backTracker)
        {
            entry.SetState(state);
            states[index] = state;
        }
    }

    public List<bool> GetStatesOfOptimization()
    {
        return states;
    }

    public List<bool> GetisSetActiveSelfOfOptimization()
    {
        return isSetActiveSelf;
    }
    public void ChangeSetActiveSelf(int index, bool newState) {
        isSetActiveSelf[index] = newState;
    }
}
