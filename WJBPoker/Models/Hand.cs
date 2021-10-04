using System.Collections.Generic;
using System.Linq;

namespace WJBPoker.Models
{
    public class Hand
    {
        private Table _table;
        public List<Card> _cardsInHand { get; private set; } = new List<Card>();
        protected List<HandPool> _handPool = new List<HandPool>();
        public HandPool bestHandInPool { get; private set; }
        public PokerHands _handRank = PokerHands.Unknown;
        public int _handScore { get; private set; } = 0;
        public bool _hasFolded { get; private set; } = false;

        public enum PokerHands : int
        {
            Unknown = -1,
            Nothing = 0,
            HighCard = 2000,
            Pair = 3000,
            TwoPair = 4000,
            ThreeOfKind = 5000,
            Straight = 6000,
            Flush = 7000,
            FullHouse = 8000,
            FourOfKind = 9000,
            StraightFlush = 10000,
            RoyalFlush = 20000
        }

        public Hand(Table table)
        {
            _table = table;
        }

        public void AddCard(Card newCard)
        {
            _cardsInHand.Add(newCard);
            if (_cardsInHand.Count > 1)
            {
                CalculateHandRank();
            }
        }

        public bool Discard(Card discardCard)
        {
            return _cardsInHand.Remove(discardCard);
        }

        public void Fold()
        {
            _cardsInHand.Clear();
            bestHandInPool = null;
            _handPool.Clear();
            _handRank = PokerHands.Unknown;
            _handScore = 0;
            _hasFolded = true;

        }

        public void CalculateHandRank()
        {
            //  Set the hand to nothing as default.
            _handRank = PokerHands.Nothing;
            _handScore = 0;
            var totCardsInRound = _cardsInHand.Count + _table.CommunityCards.Count;

            //  Populate _handPool with every possible combination of hands given the card count
            switch (totCardsInRound)
            {
                case 2: //  PreFlop, In-Pocket: This mainly is to help virtuals make better decisions
                    {
                        //Check for pair
                        if (_cardsInHand[0].CardRank == _cardsInHand[1].CardRank)
                        {
                            _handRank = PokerHands.Pair;
                            _handScore = (int)_handRank;
                        }
                        else
                        {
                            //  high Card
                            if (_cardsInHand.Where(x => x.CardRank.Equals(Card.CardRankValue.AceHigh)).Any())
                            {
                                _handRank = PokerHands.HighCard;
                                _handScore = (int)_handRank;
                            }
                        }
                        break;
                    }
                case 3: //  Flop
                    //  TODO: CalculateHandRank: Will build in 3- and 4-card analysis as the need arises
                    {
                        break;
                    }
                case 4: //  Flop
                    {
                        break;
                    }
                case 5: //  Flop, 5 cards: 1 hand added to pool
                    {
                        // Create a new handpool for analysis
                        List<Card> pool5 = new List<Card>();
                        pool5.AddRange(_cardsInHand);
                        pool5.AddRange(_table.CommunityCards);

                        HandPool _fiveHand = new HandPool(pool5);

                        //  Because it is the only possible hand, it wins
                        bestHandInPool = _fiveHand;
                        _handRank = bestHandInPool._poolHandRank;
                        _handScore = bestHandInPool._poolScore;

                        break;
                    }
                case 6: //  Turn, 6 cards: 5 hands added to pool
                    {
                        // Create a pool that will hold all cards in play for the hand, 
                        //  in-pocket and in the community
                        List<Card> pool6 = new List<Card>();
                        pool6.AddRange(_cardsInHand);
                        pool6.AddRange(_table.CommunityCards);

                        for (int i = 0; i < 6; i++)
                        {
                            List<Card> pool61 = new List<Card>();
                            pool61.AddRange(pool6);
                            pool61.RemoveAt(i);

                            HandPool _sixHand = new HandPool(pool61);

                            if (!_handPool.Contains(_sixHand))
                            {
                                _handPool.Add(_sixHand);
                            }
                        }

                        break;
                    }
                case 7: //  River, 7 cards: 10 hands added to pool
                    {
                        // Create a pool that will hold all cards in play for the hand, 
                        //  in-pocket and in the community
                        List<Card> pool7 = new List<Card>();
                        pool7.AddRange(_cardsInHand);
                        pool7.AddRange(_table.CommunityCards);

                        // XXX67
                        for (int i = 0; i < 5; i++)
                        {
                            List<Card> pool71 = new List<Card>();
                            pool71.AddRange(pool7);
                            if (i == 4)
                            {
                                // 2,3,4,6,7
                                pool71.RemoveAt(0);
                                pool71.RemoveAt(4);
                            }
                            else
                            {
                                // {x,x,3,4,5,6,7}, {1,x,x,4,5,6,7}...etc.
                                pool71.RemoveRange(i, 2);
                            }

                            HandPool _sevenOneHand = new HandPool(pool71);

                            if (!_handPool.Contains(_sevenOneHand))
                            {
                                _handPool.Add(_sevenOneHand);
                            }
                        }

                        // XXXX7
                        for (int i = 0; i < 5; i++)
                        {
                            List<Card> pool72 = new List<Card>();
                            pool72.AddRange(pool7);
                            pool72.RemoveAt(5); // 4th street has already been analyzed
                            pool72.RemoveAt(i);

                            HandPool _sevenTwoHand = new HandPool(pool72);

                            if (!_handPool.Contains(_sevenTwoHand))
                            {
                                _handPool.Add(_sevenTwoHand);
                            }
                        }

                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            // Find the best hand in the pool
            if (totCardsInRound > 5)
            {
                List<HandPool> ocean = _handPool.Where(x => x._poolScore == _handPool.Max(y => y._poolScore)).ToList();

                switch (ocean.Count())
                {
                    case 0: //  TODO:   Throw Error, there should be at least one best hand in the pool
                        break;
                    case 1:
                        bestHandInPool = new HandPool(ocean[0].GetPool());
                        break;
                    default:
                        _handPool.Sort();
                        bestHandInPool = new HandPool(_handPool[0].GetPool());
                        break;
                }
                _handRank = bestHandInPool._poolHandRank;
                _handScore = bestHandInPool._poolScore;
            }
        }
    }
}

