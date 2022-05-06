using System.Collections.Generic;
using UnityEngine;
 
[CreateAssetMenu(menuName = "New Dialogue", fileName = "New Dialogue")]
public class Dialogue : ScriptableObject {
 
    [SerializeField] List<Dialogue> choices = new List<Dialogue>();
 
    [SerializeField] string optionName;
    [SerializeField] string dialogueText;
 
    public string OptionName {
        get {
            return optionName;
        }
    }
 
    public List<Dialogue> Choices {
        get {
            return choices;
        }
    }
 
    public string DialogueText {
        get {
            return dialogueText;
        }
    }
}