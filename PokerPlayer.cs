namespace ShowdownGame
{
    public class PokerPlayer
    {
        public string Name;
        public string Cards;
        //private string[] Hand;

        public PokerPlayer(string name, string cards)
        {
            this.Name = name;
            this.Cards = cards;

            var hand = cards.Split(',');
        }
    }
}