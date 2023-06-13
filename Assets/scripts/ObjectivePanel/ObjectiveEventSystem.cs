using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveEventSystem : MonoBehaviour
{
    [Tooltip("Slot prefab to instantiate a new objective")]
    [SerializeField] private GameObject objectivePrefab;
    
    private void Start()
    {
        ObjectiveManager.Instance.OnObjectivesListChangedEvent += OnUpdateObjective;
    }

    void OnUpdateObjective()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        DrawObjectives();
    }

    public void DrawObjectives()
    {
        foreach (ObjectiveData objective in ObjectiveManager.Instance.allObjectives)
        {
            AddObjectiveUI(objective);
        }
    }

    void AddObjectiveUI(ObjectiveData objectiveData)
    {
        GameObject obj = Instantiate(objectivePrefab);
        obj.transform.SetParent(transform, false);
        
        ObjectiveElementUI objUI = obj.GetComponent<ObjectiveElementUI>();
        if (objUI)
        {
            objUI.Set(objectiveData);
        }
    }
}
