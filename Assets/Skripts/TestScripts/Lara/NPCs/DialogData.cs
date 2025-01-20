using UnityEngine;

//Scriptable Object für den Dialog-Content

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog System/Dialog Data")]
public class DialogData : ScriptableObject
{
    [TextArea(3, 10)] //Zahlen sind min und max Zeilen, die im Inspector angezeigt werden -> anpassbar je Textlänge
    public string[] dialogParts;  // Array für mehrere Textteile
    public bool hasMultipleParts => dialogParts.Length > 1;
}