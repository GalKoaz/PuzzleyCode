using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/*
   Class starts the first objective in the room.
 */
public class BeginRoomObjective : MonoBehaviour
{
    [SerializeField] private GameObject objective;

    // Start is called before the first frame update
    void Start()
    {
        ObjectiveData currObjective = objective.GetComponent<ObjectiveObj>().objectiveData;
        ObjectiveManager.Instance.allObjectives.Clear();
        ObjectiveManager.Instance.SetCurrObjective(currObjective);
        ObjectiveManager.Instance.roomObjectives = gameObject.GetComponentsInChildren<ObjectiveObj>()
            .Select(objData => objData.objectiveData)
            .ToList();
        objective.GetComponent<ObjectiveObj>().OnObjectiveBegin();
    }
}