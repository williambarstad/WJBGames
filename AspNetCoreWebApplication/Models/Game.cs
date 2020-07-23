using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreWebApplication.Models
{
    public class Game
    {
        public Table GameTable { get; private set; }
        public Deck GameDeck { get; private set; }
        public List<Player> GamePlayers { get; private set; }
        protected List<Card> CommunityCards;
        protected int Dealer;
        public string GameName { get; private set; }
        public List<Player> Winners { get; set; }

        //public Game(List<Player> players)
        public Game(Table table)
        {
            GameName = "Texas Hold 'Em";

            //  Create aliases
            GameTable = table;
            GamePlayers = GameTable.TablePlayers;
            GameDeck = GameTable.TableDeck;

            // TODO:    Deal first jack to win deal, asssign for now
            Dealer = GameTable.CurrentDealer;
        }

        public void PreFlop(int NumberOfCards = 2)
        {
            int numberOfPlayers = GamePlayers.Count;

            if (numberOfPlayers > 1)
            {
                for (int i = 0; i < NumberOfCards; i++)
                {
                    for (int j = 0; j < numberOfPlayers; j++)
                    {
                        GamePlayers[j]._hand.AddCard(GameDeck.Deal());
                    }
                }
            }
        }

        public void TheFlop(int _numofcards = 3, bool _burn = true)
        {
            //  Burn one.
            if (_burn) GameTable.AddCardToMuck(GameDeck.Deal(false));

            //  Turn three by default.
            for (int i = 0; i < _numofcards; i++)
            {
                GameTable.AddCardToCommunity(GameDeck.Deal(true));
            }

            //  Recalculate the hand standings. 
            RecalculateHands();
        }

        public void TheTurn(int _numofcards = 1, bool _burn = true)
        {
            //  Burn one.
            if (_burn) GameTable.AddCardToMuck(GameDeck.Deal(false));

            //  Turn three by default.
            for (int i = 0; i < _numofcards; i++)
            {
                GameTable.AddCardToCommunity(GameDeck.Deal(true));
            }

            //  Recalculate the hand standings. 
            RecalculateHands();
        }

        public void TheRiver(int _numofcards = 1, bool _burn = true)
        {
            //  Burn one.
            if (_burn) GameTable.AddCardToMuck(GameDeck.Deal(false));

            //  Turn three by default.
            for (int i = 0; i < _numofcards; i++)
            {
                GameTable.AddCardToCommunity(GameDeck.Deal(true));
            }

            //  Recalculate the hand standings. 
            RecalculateHands();
        }

        public void Bet()
        {
            // spin through players starting with player to the left
            Console.WriteLine("Betting round.");
        }

        public void Showdown()
        {
            // Flip cards face up
            GamePlayers.ForEach(x => x.Show());

            //  Get the best possible hand from every player and sort the list
            List<HandPool> _showHandPools = new List<HandPool>(GamePlayers.Select(x => x._hand.bestHandInPool).ToList());
            _showHandPools.Sort();

            Winners = GamePlayers.Where(y => y._hand.bestHandInPool._poolScore == _showHandPools[0]._poolScore).ToList();

            SettleUp();
        }

        private void SettleUp()
        {
            int winnings = 0;

            // TODO: Remove default table pot after betting has been has been builtd.
            if (GameTable.TablePot == 0) GameTable.AddToPot(500);

            winnings = (GameTable.TablePot / Winners.Count());
            Winners.ForEach(x => x.MakeWinner(winnings));

            //  Gather up player cards and reset.
            // TODO: Add back muck and reset after betting is added.
            // GameTable.MuckAndReset();
        }

        public void RecalculateHands()
        {
            GamePlayers.ForEach(x => x._hand.CalculateHandRank());
        }
    }
}
