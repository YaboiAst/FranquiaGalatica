using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct Dialogue{
    public string name;
    public string text;
    
    public Sprite sprite;
    
}

[CreateAssetMenu (fileName = "Dialogo", menuName = "ScriptableObject/TalkScript", order = 1)]
public class DialogueData : ScriptableObject
{
    public List<Dialogue> talkScript;

}
