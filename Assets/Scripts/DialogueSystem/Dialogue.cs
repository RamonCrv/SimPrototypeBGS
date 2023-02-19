using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Dialogue", menuName ="ScriptableObjects/Dialogue")]
public class Dialogue : ScriptableObject
{
    public List<Sentence> sentences = new List<Sentence>();    

}

[System.Serializable]
public class Sentence{
    public string name;
    [TextArea(5, 15)] public string sentence;
    
}
