using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goals
{
    public string name;
    public float value;

    public Goals (string Name, float Value)
    {
        name = Name;
        value = Value;
    }

    public float returnDiscontentment(float newVal)
    {
        return newVal * newVal;
    }
}
