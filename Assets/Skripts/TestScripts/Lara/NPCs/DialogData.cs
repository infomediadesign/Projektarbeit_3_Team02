using UnityEngine;

//Scriptable Object f�r den Dialog-Content

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog System/Dialog Data")]
public class DialogData : ScriptableObject
{
    [TextArea(3, 10)] //Zahlen sind min und max Zeilen, die im Inspector angezeigt werden -> anpassbar je Textl�nge
    public string[] dialogParts;  // Array f�r mehrere Textteile
    public bool hasMultipleParts => dialogParts.Length > 1;
}