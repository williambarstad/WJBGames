using System.Collections.Generic;

namespace AspNetCoreWebApplication.Models
{
    public class Table
    {
        public Game TableGame { get; private set; }
        public Deck TableDeck { get; private set; } = new Deck();

        public List<Player> TablePlayers { get; private set; } = new List<Player>();
        public List<Card> CommunityCards { get; private set; } = new List<Card>();
        public List<Card> Muck { get; private set; } = new List<Card>();

        public static int TableBetLimitMax { get; private set; } = 100;
        public static int TableBetLimitMin { get; private set; } = 25;
        public int TableDefaultStake { get; private set; } = 2000;
        public int TableDefaultAnte { get; private set; } = 25;
        private static readonly string[] Buddies = { "Karl", "Dave", "Byron", "Fred", "Navarre", "Spike" };
        public int TablePot { get; private set; } = 0;
        public int CurrentDealer { get; private set; } = 0;

        public enum ChipValues : int
        {
            WhiteChip = 25,
            RedChip = 50,
            BlueChip = 100
        }

        public Table()
        {
            // TODO DESIGN: Once the login and landing page are created,
            //  this will be modified to gather the security tokens from the front end 
            //  in the creation of the player object.

            //  PRIME PLAYER: Human is player 0
            TablePlayers.Add(new Player(this, "Bill", false));
            TablePlayers[0].SetPlayerImg("images\\bill.jpg");

            //  other virtual players
            for (int i = 0; i < Buddies.Length; i++)
            {
                try
                {
                    VirtualPlayer vp = new VirtualPlayer(this, Buddies[i]);
                    vp.SetPlayerImg("images\\" + Buddies[i].ToLower() + ".jpg");

                    TablePlayers.Add(vp);
                }
                catch (System.Exception)
                {
                    throw;
                }
            }

            // Set player stakes
            TablePlayers.ForEach(x => x.AddStake(TableDefaultStake));

            //  TODO: (enhancement) Deal out to first jack for dealer
            TablePlayers[CurrentDealer].Dealer();
        }

        public void StartGame()
        {
            //  Start new game
            TableGame = new Game(this);

            //  TODO:   Game Steps go here.
            TableGame.PreFlop();
            /*
            TableGame.TheFlop();
            TableGame.TheTurn();
            TableGame.TheRiver();
            TableGame.Showdown();
            */

        }

        public void AddCardToCommunity(Card card)
        {
            CommunityCards.Add(card);
        }

        public void AddCardToMuck(Card card)
        {
            Muck.Add(card);
        }

        public void SetDealer(int dealerInt = 0)
        {
            TablePlayers.ForEach(x => x.PassTheDeck());
            TablePlayers[dealerInt].Dealer();
        }

        public void MuckAndReset()
        {
            // Muck Player hands
            TablePlayers.ForEach(x => x.Muck());

            //  Clear board
            CommunityCards.Clear();
            Muck.Clear();
            TablePot = 0;

            //  Reset deck
            TableDeck.ResetDeck();

            //  Pass the deck
            TablePlayers[CurrentDealer].PassTheDeck();

        }

        public void AddToPot(int addToPot = 0)
        {
            TablePot += addToPot;
        }

    }
}
