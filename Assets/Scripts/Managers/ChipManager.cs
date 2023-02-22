using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ChipManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private FiniteStateMachine finiteStateMachine;
    [SerializeField] private GameObject Chip;
    [SerializeField] private Transform BetPos;
    [SerializeField] private Texture2D[] textures;
    [SerializeField] private Color[] colors;
    [SerializeField] private TextMeshProUGUI currentBetTxt, balanceAmtTxt;

    private List<int> chipValueList = new();
    private List<GameObject> chipList = new();
    private MeshRenderer chipMeshRenderer;
    private GameObject chip;
    private Vector3 betPos;
    private int index, currentBet, balance;
    private readonly float YOffset = 0.0225f, XOffset = -0.0675f;

    public Action destroyChip, placeBet;
    public Action<int> addChip;
    #endregion Variables

    private void Awake()
    {
        placeBet += Bet;
        addChip += AddChipValue;
        destroyChip += ChipDestroyer;
        balance = 10000;
        currentBet = 0;
        currentBetTxt.text = "Current Bet : " + currentBet;
        balanceAmtTxt.text = "Balance : " + balance;
    }

    public List<int> GetChipValueList()
    { 
        return chipValueList;
    }
    private void AddChipValue(int chipValue)
    {
        currentBet += chipValue;
        currentBetTxt.text = "Current Bet : " + currentBet;
        chipValueList.Add(chipValue);
    }

    private void Bet()
    {
        if (chipValueList.Count <= 0)
            return;

        balance -= currentBet;
        currentBetTxt.text = "Current Bet : " + currentBet;
        balanceAmtTxt.text = "Balance : " + balance;
        chipValueList.Sort();
        chipValueList.Reverse();
        ChipCreator();
    }

    private void ChipCreator()
    {
        for(int i = 0; i < chipValueList.Count; i++)
        {
            IndexSetter(chipValueList[i]);
            betPos = new Vector3(BetPos.position.x + (i * XOffset), BetPos.position.y + (i * YOffset), BetPos.position.z);
            chip = Instantiate(Chip, betPos, Quaternion.Euler(0, UnityEngine.Random.Range(-60f, 30f), 0));
            chipMeshRenderer = chip.GetComponent<MeshRenderer>();
            chipMeshRenderer.materials[0].SetColor("_BaseColor", colors[index]);
            chipMeshRenderer.materials[0].SetColor("_EmissionColor", colors[index]);
            chipMeshRenderer.materials[2].SetColor("_BaseColor", colors[index]);
            chipMeshRenderer.materials[3].mainTexture = textures[index];
            chipList.Add(chip);
        }
    }

    private void ChipDestroyer()
    {
        currentBet = 0;
        currentBetTxt.text = "Current Bet : " + currentBet;
        chipValueList.Clear();
        if (chipList != null)
        {
            foreach (var chip in chipList)
                Destroy(chip);
            chipList.Clear();
        }
    }

    private void IndexSetter(int chipValue)
    {
        switch(chipValue)
        {
            case 1: index = 0; break;
            case 5: index = 1; break;
            case 25: index = 2; break;
            case 100: index = 3; break;
            case 500: index = 4; break;
        }
    }
}