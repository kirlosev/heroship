using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Transform Variable", menuName = "L/Vars/Transform Variable")]
public class TransformVariable : ScriptableObject
{
    public Transform value;
}