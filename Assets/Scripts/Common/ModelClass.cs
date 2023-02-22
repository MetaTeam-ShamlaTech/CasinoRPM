using System.Collections.Generic;

public class PlayerCardsSet
{
    public string CardSuit { get; set; }
    public string CardValue { get; set; }
}

public class DealerCardsSet
{
    public string CardSuit { get; set; }
    public string CardValue { get; set; }
}

public class CardsSet
{
    public List<DealerCardsSet> DealerCards{ get; set; }
    public List<PlayerCardsSet> PlayerCards { get; set; }
}