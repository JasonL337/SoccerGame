using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoosePower : MonoBehaviour
{
    public List<string> ability = new List<string>();
    int abilityNum = 0;
    public GameObject self;
    public GameObject panel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChooseAbility()
    {
        if (abilityNum == 0)
        {
            ability.Add(EventSystem.current.currentSelectedGameObject.name);
            abilityNum += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ability.Count.Equals(1))
        {
            self.SetActive(false);
            panel.SetActive(false);
        }
    }
}
