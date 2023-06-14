using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveObj : MonoBehaviour
{
    public ObjectiveData objectiveData;

    public void OnObjectiveBegin()
    {
        ObjectiveManager.Instance.Add(objectiveData);
    }
}