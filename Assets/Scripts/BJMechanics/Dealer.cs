using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Dealer : MonoBehaviour
{
    #region Variables
    private enum WinCondition { Player, Dealer, Draw}
    [SerializeField] Transform DeckPos;
    [SerializeField] private GameObject Card;
    [SerializeField] private PlayerCards playerCards;
    [SerializeField] private DealerCards dealerCards;
    [SerializeField] private GameObject gameBtnPanel;
    [SerializeField] private TextMeshProUGUI PopupText;
    private FiniteStateMachine finiteStateMachine;
    private WinCondition winCondition;
    public CardsSet cardsSet;
    private int gameValue = 0;
    private string data;
    #endregion Variables

    public void Init(FiniteStateMachine FiniteStateMachine)
    {
        finiteStateMachine = FiniteStateMachine;
        gameBtnPanel.SetActive(false);
        PopupText.text = "";

        data = GameUtilities.GetFileData(Application.streamingAssetsPath + "\\" + gameValue, "CardSet.json");

        cardsSet = GameUtilities.DeserializeObject<CardsSet>(data);
        playerCards.cardsSet = cardsSet.PlayerCards;
        dealerCards.cardsSet = cardsSet.DealerCards;
        dealerCards.stood = true;

        if(gameValue == 0)
            winCondition= WinCondition.Player;
        if (gameValue == 1)
            winCondition = WinCondition.Dealer;
        if (gameValue == 2)
            winCondition = WinCondition.Draw;

        gameValue++;
        if (gameValue > 2)
            gameValue = 0;

        StartCoroutine(FirstServe());
    }

    public void Hit()
    {
        gameBtnPanel.SetActive(false);
        StartCoroutine(PlayerHitCoroutine());
    }

    private IEnumerator PlayerHitCoroutine()
    {
        yield return StartCoroutine(playerCards.DealPlayerCards(DeckPos, Card));
        StopCoroutine(playerCards.DealPlayerCards(DeckPos, Card));
        gameBtnPanel.SetActive(true);
        yield return null;
    }

    private IEnumerator DealerHitCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        while (dealerCards.cardCount < dealerCards.cardsSet.Count)
        {
            yield return StartCoroutine(dealerCards.DealDealerCards(DeckPos, Card));
            StopCoroutine(dealerCards.DealDealerCards(DeckPos, Card));
            gameBtnPanel.SetActive(true);
        }
        StartCoroutine(CheckWin());
        yield return null;
    }

    public void Stand()
    {
        dealerCards.stood = true;
        dealerCards.RevealCard();
        StartCoroutine(CheckWin());
    }

    private IEnumerator CheckWin()
    {
        yield return new WaitForSeconds(1f);
        if (winCondition == WinCondition.Player)
        {
            if (int.Parse(dealerCards.cardValueTxt.text) >= int.Parse(playerCards.cardValueTxt.text) && int.Parse(dealerCards.cardValueTxt.text) < 21)
                StartCoroutine(DealerHitCoroutine());
            else
                StartCoroutine(Restore());
        }
        else if (winCondition == WinCondition.Dealer)
        {
            if (int.Parse(dealerCards.cardValueTxt.text) <= int.Parse(playerCards.cardValueTxt.text) && int.Parse(playerCards.cardValueTxt.text) < 21)
                StartCoroutine(DealerHitCoroutine());
            else
                StartCoroutine(Restore());
        }
        else
        {
            if (int.Parse(dealerCards.cardValueTxt.text) < int.Parse(playerCards.cardValueTxt.text))
                StartCoroutine(DealerHitCoroutine());
            else if (int.Parse(dealerCards.cardValueTxt.text) == int.Parse(playerCards.cardValueTxt.text))
                StartCoroutine(Restore());
        }
        yield return null;
    }

    private IEnumerator FirstServe()
    {
        for(int i = 0; i < 4; i++)
        {
            if (i > 1)
                dealerCards.stood = false;
            if(i % 2 != 1)
                yield return StartCoroutine(playerCards.DealPlayerCards(DeckPos, Card));
            else
                yield return StartCoroutine(dealerCards.DealDealerCards(DeckPos, Card));
            yield return new WaitForSeconds(1f);
        }
        gameBtnPanel.SetActive(true);
        yield return null;
    }

    private IEnumerator Restore()
    {
        gameBtnPanel.SetActive(false);
        if (int.Parse(playerCards.cardValueTxt.text) > 21 || int.Parse(dealerCards.cardValueTxt.text) > 21)
        {
            if (int.Parse(playerCards.cardValueTxt.text) > 21)
                PopupText.text = "Player Bust!";
            else if (int.Parse(dealerCards.cardValueTxt.text) > 21)
                PopupText.text = "Dealer Bust!";
        }
        else
        {
            if (winCondition == WinCondition.Player)
                PopupText.text = "Player Wins!";
            else if (winCondition == WinCondition.Dealer)
                PopupText.text = "Dealer Wins!";
            else
                PopupText.text = "PUSH!";
        }

        yield return new WaitForSeconds(1.5f);
        playerCards.RestoreCards();
        dealerCards.RestoreCards();
        yield return new WaitForSeconds(0.5f);
        PopupText.text = "";
        finiteStateMachine.ChangeState(finiteStateMachine.PostGameState);
        finiteStateMachine.chipManager.destroyChip?.Invoke();
        yield return null;
    }
}