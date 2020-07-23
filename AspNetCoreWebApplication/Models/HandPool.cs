using System;
using System.Collections.Generic;
using System.Linq;
using static AspNetCoreWebApplication.Models.Hand;

namespace AspNetCoreWebApplication.Models
{
    public class HandPool : IComparable<HandPool>
    {
        public List<Card> cardsInPool { get; private set; }
        protected Tuple<Card.CardRankValue, Card.CardRankValue, Card.CardRankValue, Card.CardRankValue, Card.CardRankValue> _handTuple;
        public PokerHands _poolHandRank { get; private set; } = PokerHands.Unknown;
        public int _poolScore { get; private set; } = 0;

        //  Constructor
        public HandPool(List<Card> pool)
        {
            SetPool(pool);
        }

        public void EvaluateHighestRank()
        {
            _poolHandRank = PokerHands.Nothing;
            _poolScore = 0;
            sortByCardRank();


            //  TWOPAIR and PAIR and HIGHCARD
            if (getGroupByRankCount(2) == 2)
            {
                _poolHandRank = PokerHands.TwoPair;
            }
            else
            {
                if (getGroupByRankCount(2) == 1)
                {
                    _poolHandRank = PokerHands.Pair;
                }
                else
                {
                    if (_handTuple.Item1 == Card.CardRankValue.AceHigh) _poolHandRank = PokerHands.HighCard;
                }
            }

            // THREEOFKIND 
            if (getGroupByRankCount(3) == 1)
            {
                _poolHandRank = PokerHands.ThreeOfKind;
            }

            // FOUROFKIND
            if (getGroupByRankCount(4) == 1)
            {
                _poolHandRank = PokerHands.FourOfKind;
            }

            // FULLHOUSE
            if (_poolHandRank == Hand.PokerHands.ThreeOfKind)
            {
                //  Get the rank values of the set and pair
                //Card.CardRankValue theSet = getRankbyGroupCount3();
                int theSet = getRankbyGroupCount3();
                int thePair = getRankbyGroupCount2();
                if (thePair != 0 && theSet != 0 && theSet != thePair)
                {
                    _poolHandRank = PokerHands.FullHouse;
                }
            }

            // FLUSH
            if (getGroupBySuitCount(5) == 1) _poolHandRank = PokerHands.Flush;

            //  STRAIGHT and STRAIGHT FLUSH and ROYAL FLUSH
            //      Check with Ace as high card and as low card
            if ((getGroupByRankCount(2) == 0) && (getGroupByRankCount(3) == 0) && (getGroupByRankCount(4) == 0))
            {
                if (((_handTuple.Item1 >= Card.CardRankValue.Five) && (_handTuple.Item1 - _handTuple.Item5 == 40)) || ((_handTuple.Item1 == Card.CardRankValue.AceHigh) && (_handTuple.Item2 == Card.CardRankValue.Five) && (_handTuple.Item2 - _handTuple.Item5 == 30)))
                {
                    if (_poolHandRank != PokerHands.Flush)
                    {
                        _poolHandRank = PokerHands.Straight;
                    }
                    else //  STRAIGHT FLUSH  
                    {
                        //  ROYAL FLUSH
                        if (_handTuple.Item1 == Card.CardRankValue.AceHigh && _handTuple.Item2 == Card.CardRankValue.King)
                        {
                            _poolHandRank = PokerHands.RoyalFlush;
                        }
                        else
                        {
                            _poolHandRank = PokerHands.StraightFlush;
                        }
                    }
                }
            }

            //  NOTHING
            if (_poolHandRank == PokerHands.Unknown) _poolHandRank = PokerHands.Nothing;

            // Calculate the poolScore and add hand rank Kickers
            switch (_poolHandRank)
            {
                case PokerHands.Unknown:
                    // TODO: Throw Error, this should not be unknown at this point
                    break;
                case PokerHands.Nothing:
                    _poolScore = cardsInPool.Sum(x => (int)x.CardRank);
                    break;
                case PokerHands.HighCard:
                    // HighCard bonus is not determined in the HandPool, but at table.showdown
                    break;
                case PokerHands.Pair:
                    // Pair Kicker: Add the rank of the pair card to the score
                    var kickerPair = (from card in cardsInPool group card by card.CardRank).Where(x => x.Count() == 2);
                    _poolScore = (int)PokerHands.Pair + (int)kickerPair.FirstOrDefault().Key;
                    break;
                case PokerHands.TwoPair:
                    // Two-Pair Kicker: Add the rank of the high pair card to the score
                    var kickerTwoPair = (from card in cardsInPool group card by card.CardRank).Where(x => x.Count() == 2).Max(x => x.Key);
                    _poolScore = (int)PokerHands.TwoPair + (int)kickerTwoPair;
                    break;
                case PokerHands.ThreeOfKind:
                    // ThreeOfKind Kicker: Add the rank of the set to the score
                    var kickerThreeOfKindPair = (from card in cardsInPool group card by card.CardRank).Where(a => a.Count() == 3).First().Key;
                    _poolScore = (int)PokerHands.ThreeOfKind + (int)kickerThreeOfKindPair;
                    break;
                case PokerHands.Straight:
                    // Straight Kicker: Add highest card rank
                    _poolScore = (int)PokerHands.Straight + (int)cardsInPool.Max(x => x.CardRank);
                    break;
                case PokerHands.Flush:
                    // Flush Kicker: Add highest card rank
                    _poolScore = (int)PokerHands.Flush + (int)cardsInPool.Max(x => x.CardRank);
                    break;
                case PokerHands.FullHouse:
                    // FullHouse Kicker: Add the rank of the set to the score
                    var kickerFullHouse = (from card in cardsInPool group card by card.CardRank).Where(a => a.Count() == 3).First().Key;
                    _poolScore = (int)PokerHands.FullHouse + (int)kickerFullHouse;
                    break;
                case PokerHands.FourOfKind:
                    // FourOfKind Kicker: Add the rank of the quad to the score
                    var kickerFourOfKind = (from card in cardsInPool group card by card.CardRank).Where(a => a.Count() == 4).First().Key;
                    _poolScore = (int)PokerHands.FourOfKind + (int)kickerFourOfKind;
                    break;
                case PokerHands.StraightFlush:
                    // StraightFlush Kicker: Add highest card rank
                    _poolScore = (int)PokerHands.Flush + (int)cardsInPool.Max(x => x.CardRank);
                    break;
                case PokerHands.RoyalFlush:
                    // Royal Flush ties will be chopped.
                    break;
                default:
                    break;
            }
        }

        public void SetPool(List<Card> pool)
        {
            // set and sort by rank
            cardsInPool = pool;
            sortByCardRank();

            //set _handTuple and evaluate
            _handTuple = new Tuple<Card.CardRankValue, Card.CardRankValue, Card.CardRankValue, Card.CardRankValue, Card.CardRankValue>(
                cardsInPool[0].CardRank, cardsInPool[1].CardRank, cardsInPool[2].CardRank, cardsInPool[3].CardRank, cardsInPool[4].CardRank);
            EvaluateHighestRank();
        }

        public List<Card> GetPool()
        {
            return cardsInPool;
        }

        private void sortByCardRank(bool desc = true)
        {
            //  NOTE:   Not only does this sort cardsInPool by the desired direction, 
            //          but also changes AceHigh to AceLow or vice-versa while sorted.

            if (desc)
            {

                foreach (Card card in cardsInPool)
                {
                    if (card.CardRank == Card.CardRankValue.AceLow) card.MakeAceHigh();
                }

                cardsInPool = cardsInPool.OrderByDescending(x => x.CardRank).ToList();

            }
            else  //  ascending
            {
                for (int i = 0; i < cardsInPool.Count; i++)
                {
                    if (cardsInPool[i].CardRank == Card.CardRankValue.AceHigh) cardsInPool[i].MakeAceLow();
                }

                cardsInPool = cardsInPool.OrderByDescending(x => x.CardRank).Reverse().ToList();

            }
        }

        private int getGroupByRankCount(int n)
        { return cardsInPool.GroupBy(c => c.CardRank).Count(g => g.Count() == n); }

        private int getGroupBySuitCount(int n)
        { return cardsInPool.GroupBy(c => c.Suit).Count(g => g.Count() == n); }

        private int getRankbyGroupCount2(int groupPos = 1)
        {
            // 1 = get the max pair (default)
            // 2 - get the min pair
            //  Return the rank of the desired pair, otherwise return 0.
            int pairCount = cardsInPool.GroupBy(c => c.CardRank).Count(g => g.Count() == 2);

            if (groupPos == 2)
            {

                return (pairCount == 2) ? (int)cardsInPool.GroupBy(c => c.CardRank).Last(g => g.Count() == 2).Key : 0;
            }
            else
            {
                return (pairCount >= 1) ? (int)cardsInPool.GroupBy(c => c.CardRank).First(g => g.Count() == 2).Key : 0;
            }
        }

        private int getRankbyGroupCount3()
        {
            //  Return the rank of the set, if one exists; otherwise return Unknown.
            int setCount = cardsInPool.GroupBy(c => c.CardRank).Count(g => g.Count() == 3);

            return (setCount == 1) ? (int)cardsInPool.GroupBy(c => c.CardRank).Last(g => g.Count() == 3).Key : 0;
        }


        int IComparable<HandPool>.CompareTo(HandPool other)
        {
            // Compare hand ranks, then compare scores, then individual cards
            if (this._poolHandRank == other._poolHandRank)
            {
                if (this._poolScore == other._poolScore)
                {
                    if ((int)this._handTuple.Item2 == (int)other._handTuple.Item2)
                    {
                        if ((int)this._handTuple.Item3 == (int)other._handTuple.Item3)
                        {
                            if ((int)this._handTuple.Item4 == (int)other._handTuple.Item4)
                            {
                                return other._handTuple.Item5.CompareTo(this._handTuple.Item5);
                            }
                            return other._handTuple.Item4.CompareTo(this._handTuple.Item4);
                        }
                        return other._handTuple.Item3.CompareTo(this._handTuple.Item3);
                    }
                    return other._handTuple.Item2.CompareTo(this._handTuple.Item2);
                }
                return other._poolScore.CompareTo(this._poolScore);
            }
            return other._poolHandRank.CompareTo(this._poolHandRank);
        }
    }
}
