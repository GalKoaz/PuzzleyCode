using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance; // Singelton

    // List of all objectives
    public List<ObjectiveData> allObjectives; // dynamically list that the room objects are added to

    public List<ObjectiveData> roomObjectives;

    // Current active objective
    public ObjectiveData currentObjective;

    public event Action OnObjectiveEndEvent; // event that is invoked whenever we finish current objective

    // Event to notify subscribers when the inventory changes
    public event Action OnObjectivesListChangedEvent;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Keep the inventory system object persistent across scenes
        DontDestroyOnLoad(gameObject);
    }

    public void CompleteObjective(int objectiveID)
    {
        ObjectiveData objective = GetObjectiveByID(objectiveID);
        if (objective != null)
        {
            objective.isCompleted = true;
            OnObjectivesListChangedEvent?.Invoke();

            // Check if there is a next objective
            ObjectiveData nextObjective = GetNextObjective();
            if (nextObjective != null)
            {
                // Progress to the next objective
                currentObjective = nextObjective;
                allObjectives.Add(currentObjective);
                OnObjectivesListChangedEvent?.Invoke();

                // Perform any necessary actions for the new objective (e.g., UI updates)
                // OnObjectiveEndEvent?.Invoke();

                // Trigger objective-specific behavior (e.g., opening a door, spawning enemies, etc.)

                // Update UI or perform any other necessary actions
            }
            else
            {
                // All objectives are completed
                Debug.Log("All objectives completed!");
            }
        }
        else
        {
            Debug.LogWarning("Objective ID not found: " + objectiveID);
        }
    }

    private ObjectiveData GetObjectiveByID(int objectiveID)
    {
        foreach (ObjectiveData objective in allObjectives)
        {
            if (objective.objectiveID == objectiveID)
                return objective;
        }

        return null;
    }

    private ObjectiveData GetNextObjective()
    {
        int nextID = currentObjective.nextObjectiveID;
        if (nextID == -1)
        {
            return null;
        }

        // Find the next objective in the list
        foreach (ObjectiveData objective in roomObjectives)
        {
            if (objective.objectiveID == nextID)
            {
                Debug.Log("GOOD NEXT ID: " + nextID);
                return objective;
            }
        }

        return null; // No next objective found
    }

    public void SetCurrObjective(ObjectiveData objectiveData)
    {
        currentObjective = objectiveData;
    }

    public void Add(ObjectiveData objectiveData)
    {
        allObjectives.Add(objectiveData);
        OnObjectivesListChangedEvent?.Invoke();
    }

    private void Update()
    {
        // Debug.Log("current objective: " + currentObjective.isTriggered);
        if (currentObjective && currentObjective.isTriggered)
        {
            Debug.Log("Objective Manager: triggered!");
            CompleteObjective(currentObjective.objectiveID);
        }
    }
}