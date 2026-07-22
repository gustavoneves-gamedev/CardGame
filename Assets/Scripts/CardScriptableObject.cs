using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card", order = 1)]
public class CardScriptableObject : ScriptableObject
{
    public string cardName;

    [TextArea]
    public string actionDescription, cardLore;

    public int attackPower, currentHealth, manaCost;

    public Sprite characterSprite, bgSprite;
}
