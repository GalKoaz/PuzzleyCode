using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objective info")]
public class ObjectiveData : ScriptableObject
{
    public int objectiveID;
    public int nextObjectiveID;
    public string objectiveName;
    public string objectiveDescription;
    public bool isCompleted;
    public bool isTriggered;
}