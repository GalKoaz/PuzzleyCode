using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Cleans objectives scriptable objects flags at the beginning of the game,
   to avoid previous runs modifications.
 */
public class ResetObjectivesAttr : MonoBehaviour
{
    [SerializeField] private ObjectiveData[] ObjectiveDatas;

    // Start is called before the first frame update
    void Start()
    {
        foreach (ObjectiveData objectiveData in ObjectiveDatas)
        {
            objectiveData.isCompleted = false;
            objectiveData.isTriggered = false;
        }
    }
}