using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTriggerSystem : MonoBehaviour
{
    // public static ObjectiveTriggerSystem instance; // Singleton instance
    //
    // private Dictionary<string, Event> objectiveTriggers; // Dictionary to store objective triggers and their corresponding actions
    //
    // private void Awake()
    // {
    //     // Set up the singleton instance
    //     if (instance == null)
    //     {
    //         instance = this;
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //         return;
    //     }
    //
    //     // Initialize the dictionary
    //     objectiveTriggers = new Dictionary<string, Action>();
    // }
    //
    // public void RegisterObjectiveTrigger(string objectiveID, Action onCompleteAction)
    // {
    //     // Register the objective trigger and its corresponding action
    //     objectiveTriggers[objectiveID] = onCompleteAction;
    // }
    //
    // public void TriggerObjectiveCompletion(string objectiveID)
    // {
    //     // Check if the objective trigger is registered
    //     if (objectiveTriggers.ContainsKey(objectiveID))
    //     {
    //         // Invoke the corresponding action
    //         objectiveTriggers[objectiveID]?.Invoke();
    //     }
    //     else
    //     {
    //         Debug.LogWarning("Objective trigger not found: " + objectiveID);
    //     }
    }
