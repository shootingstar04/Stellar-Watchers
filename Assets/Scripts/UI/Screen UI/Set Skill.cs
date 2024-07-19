using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSkill : MonoBehaviour
{
    private List<GameObject> skillSlot = new List<GameObject>();


    [TextArea]
    public List<string> explanations = new List<string>();
    public List<string> names = new List<string>();
    public List<Sprite> images = new List<Sprite>();


    // Start is called before the first frame update
    void Awake()
    {
        explanations = SkillExplanation.skillExplanation.explanations;
        names = SkillExplanation.skillExplanation.explanations;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Apple(GameObject a) {
        Debug.Log(a.name);
    }
}
