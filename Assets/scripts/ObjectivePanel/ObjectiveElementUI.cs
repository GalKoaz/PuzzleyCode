using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveElementUI : MonoBehaviour
{
    [SerializeField] private Text objectiveDescriptionText;
    [SerializeField] private GameObject checkV;
    [SerializeField] private GameObject checkX;

    // NOTE: for now it's only updates the picture
    public void Set(ObjectiveData objective)
    {
        if (objective.isCompleted)
        {
            checkV.SetActive(true);
            checkX.SetActive(false);
        }
        else
        {
            checkX.SetActive(true);
            checkV.SetActive(false);
        }

        objectiveDescriptionText.text = objective.objectiveDescription;
    }
}