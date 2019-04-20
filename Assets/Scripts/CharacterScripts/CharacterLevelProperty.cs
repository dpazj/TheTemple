using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLevelProperty : MonoBehaviour {


    [SerializeField]
    private bool waterKills;
    [SerializeField]
    private bool tutorialBehaviour;


    WaterDeathBehaviour waterDeathBehaviour;
    TutorialBehaviour tb;
    private void Start()
    {
        if (waterKills)
        {
            waterDeathBehaviour = gameObject.AddComponent(typeof(WaterDeathBehaviour)) as WaterDeathBehaviour;
        }

        if (tutorialBehaviour)
        {
            tb = gameObject.AddComponent(typeof(TutorialBehaviour)) as TutorialBehaviour;
        }
    }
}
