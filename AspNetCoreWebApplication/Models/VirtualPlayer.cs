namespace AspNetCoreWebApplication.Models
{
    public class VirtualPlayer : Player
    {
        public static double ChanceOfBettingBadly = 0.00;
        public static double ChanceOfBluffing = 0.00;
        public static double ChangeOfFolding = 0.00;


        public VirtualPlayer(Table table, string newPlayerName) : base(table, newPlayerName, true)
        {

            ChanceOfBettingBadly = 75.00;
            ChanceOfBluffing = 50.00;
            ChangeOfFolding = 25.00;

        }

        //  TODO:   All of theese "Likely" functions need to be researched and tested. THESE ARE ONLY STUBS.

        public bool LikelyToBetBadly(double rank)
        {
            double thold = 50.00;

            return (rank <= thold) ? false : true;
        }


        public bool LikelyToBluff(double rank)
        {
            double thold = 50.00;

            return (rank <= thold) ? false : true;
        }

        public bool LikelyToFold(double rank)
        {
            double thold = 50.00;

            return (rank <= thold) ? false : true;
        }
    }
}
