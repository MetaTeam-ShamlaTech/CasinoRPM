using UnityEngine;

public class CardSetter : MonoBehaviour
{
    #region Variables
    [SerializeField] private Texture2D[] CardSuits;
    [SerializeField] private Texture2D[] CardValues;
    private Material cardMaterial;
    #endregion Variables

    public int SetCardTextures(MeshRenderer cardMeshRenderer, string CardSuit, string CardValue)
    {
        cardMaterial = cardMeshRenderer.material;
        switch(CardSuit)
        {
            case "C": cardMaterial.SetTexture("_CardSuit", CardSuits[0]);
                cardMaterial.SetInt("_RedColor", 0); break;
            case "D": cardMaterial.SetTexture("_CardSuit", CardSuits[1]);
                cardMaterial.SetInt("_RedColor", 1); break;
            case "H": cardMaterial.SetTexture("_CardSuit", CardSuits[2]);
                cardMaterial.SetInt("_RedColor", 1); break;
            case "S": cardMaterial.SetTexture("_CardSuit", CardSuits[3]);
                cardMaterial.SetInt("_RedColor", 0); break;
        }
        switch(CardValue)
        {
            case "A": cardMaterial.SetTexture("_CardValue", CardValues[0]); return 11;
            case "2": cardMaterial.SetTexture("_CardValue", CardValues[1]); return 2;
            case "3": cardMaterial.SetTexture("_CardValue", CardValues[2]); return 3;
            case "4": cardMaterial.SetTexture("_CardValue", CardValues[3]); return 4;
            case "5": cardMaterial.SetTexture("_CardValue", CardValues[4]); return 5;
            case "6": cardMaterial.SetTexture("_CardValue", CardValues[5]); return 6;
            case "7": cardMaterial.SetTexture("_CardValue", CardValues[6]); return 7;
            case "8": cardMaterial.SetTexture("_CardValue", CardValues[7]); return 8;
            case "9": cardMaterial.SetTexture("_CardValue", CardValues[8]); return 9;
            case "10": cardMaterial.SetTexture("_CardValue", CardValues[9]); return 10;
            case "J": cardMaterial.SetTexture("_CardValue", CardValues[10]); return 10;
            case "Q": cardMaterial.SetTexture("_CardValue", CardValues[11]); return 10;
            case "K": cardMaterial.SetTexture("_CardValue", CardValues[12]); return 10;
        }
        return 0;
    }
}