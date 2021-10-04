namespace WJBPoker.Models
{
    public class Player
    {
        public Table PlayerTable;

        public bool IsVirtual { get; private set; } = false;
        public bool IsDealer { get; private set; } = false;
        public bool IsWinner { get; private set; } = false;
        public bool IsDealOut { get; private set; } = false;
        public int PLeft { get; private set; }
        public int PRight { get; private set; }

        public string PlayerName { get; private set; }
        public string PlayerImg { get; private set; }
        public int Stake { get; private set; } = 0;
        public Hand _hand { get; private set; }

        public Player(Table table, string newPlayerName, bool bvirtual = false)
        {
            PlayerTable = table;
            PlayerName = newPlayerName;
            IsVirtual = bvirtual;
            SitAtTable();
        }
                
        /// <summary>
        /// Method: SitAtTable()
        /// Description: This function is called by the Table
        ///     * Instantiate the player hand object
        ///     * Assign player positions at the table, relevant during betting
        /// 
        /// </summary>
        private void SitAtTable()
        {
            _hand = new Hand(PlayerTable);
            var playercount = PlayerTable.TablePlayers.Count;

            //  TODO:   Move this to table.NewGame when all players are seated
            // There will be a seated order, and each player will be aware of the player directly to the left or right of them.
            switch (playercount)
            {
                case 0: // Player 0: This is the PRIME PLAYER, so Player 1 will always be left of PRIME.
                        // TODO: Set PRight for Prime Player and PLeft for the last player seated after last player is seated.
                    PLeft = 1;
                    break;
                default:
                    PLeft = playercount + 1;
                    PRight = playercount - 1;
                    break;
            }
        }

        // TODO: Create a HandPool class interface?
        public HandPool GetBesthand()
        {
            return _hand.bestHandInPool;
        }

        public bool AnteUp(int anteAmount)
        {
            if (Stake >= anteAmount)
            {
                Stake -= anteAmount;
                return true;
            }
            else
            {
                //  TODO: Throw poverty notice if not able to ante.
                return false;
            }
        }

        public void Dealer()
        {
            IsDealer = true;
        }

        public void PassTheDeck()
        {
            IsDealer = false;
            //  TODO:   Make pass direction a game option, left for now
            PlayerTable.SetDealer(PLeft);
        }

        public bool MakeBet(int betAmount)
        {
            // TODO:    (ver 2.0) Add setting/condition that allows 'all-in' behavior if bet amount exceeds stakes,
            //          Setting stake to 0, or allowing for a 'house stake' with juice for the house when repaid, etc.
            if (Stake >= betAmount)
            {
                Stake -= betAmount;
                PlayerTable.AddToPot(betAmount);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void MakeWinner(int winningsAmount)
        {
            IsWinner = true;
            Stake += winningsAmount;
        }

        public void Show()
        {
            foreach (Card card in _hand._cardsInHand)
            {
                if (card.IsFaceDown) card.FlipCard();
            }
        }

        public void AddStake(int stakeAmout)
        {
            Stake += stakeAmout;
        }

        public void FinishDealing()
        {
            IsDealer = false;
        }

        public void DealOut()
        {
            IsDealOut = true;
        }

        public void DealBackIn()
        {
            IsDealOut = false;
        }

        public void SetPlayerImg(string imgString)
        {
            PlayerImg = imgString;
        }

        public void Muck()
        {
            _hand.Fold();
        }

        public bool hasFolded()
        {
            return _hand._hasFolded;
        }
    }
}
