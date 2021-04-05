using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public string name;
    public List<Goals> goals;

    public Action (string Name)
    {
        name = Name;
        goals = new List<Goals>();
    }

    public float getChange(Goals goal)
    {
        foreach (Goals targetgoals in goals)
        {
            if (targetgoals.name == goal.name)
            {
                return targetgoals.value;
            }
        }
        return 0f;
    }
}
