using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTournament
{
    class Player7 : Player
    {
        private enum States { Evaluate, Check, Bet, Fold, RaiseCall }

        #region Variables
        List<PlayerAction> actions;
        Card[] hand;
        Dictionary<int, int[]> bettingRangeTable;
        Dictionary<int, int> maxRaisesTable;
        Dictionary<int, int> cardsToDiscardTable;
        Card highCard;
        int rank;
        int safety;
        int maxBet;
        States stateRound1, stateRound2;
        #endregion

        public Player7(int idNum, string name, int money) : base(idNum, name, money)
        {
            bettingRangeTable = new Dictionary<int, int[]>();
            maxRaisesTable = new Dictionary<int, int>();
            cardsToDiscardTable = new Dictionary<int, int>();
            rank = 0;
            safety = 0;
            maxBet = 0;
            stateRound1 = 0;
            stateRound2 = 0;
        }

        #region Custom Methods
        private void AnalyzeHand()
        {
            rank = Evaluate.RateAHand(this.Hand, out highCard);
            // May implement more later if necessary
        }

        private PlayerAction BTCheck(States currentState)
        {
            if(rank >= 2) // May change to 3 if one pair isn't good enough?
            {
                // First player
                if (!this.Dealer)
                    return BTBet(currentState);
                // Second player
                else
                    return BTRaiseCall(currentState);
            }
            else
            {
                // First player
                if (!this.Dealer)
                {
                    // FIGURE OUT WHAT STATE WE'RE GOING TO GO TO!!!!!!!! //
                    return new PlayerAction(this.Name, "Bet1", "check", 0);
                }
                // Second player but first checked
                else if (actions[actions.Count].ActionName == "check")
                {
                    stateRound1 = States.Evaluate; //Go back since the round is gonna end
                    return new PlayerAction(this.Name, "Bet1", "check", 0);
                }
                // Second player and first didn't check
                else
                {
                    //Check to see if you should fold, or if you can keep playing
                    if (ShouldFold())
                    {
                        stateRound1 = States.Evaluate; //Go back since the round is gonna end
                        return new PlayerAction(this.Name, "Bet1", "fold", 0);
                    }
                    else
                    {
                        currentState = States.RaiseCall;
                        BTRaiseCall(currentState);
                    }
                }
            }

            // DON'T FORGET TO UPDATE STATE
            return new PlayerAction(this.Name, "<STUFF>", "<OTHER STUFF>", -1);
        }

        private PlayerAction BTBet(States currentState)
        {
            // CHECK TO SEE IF YOU SHOULD FOLD

            // DON'T FORGET TO UPDATE STATE
            return new PlayerAction(this.Name, "<STUFF>", "<OTHER STUFF>", -1);
        }

        private PlayerAction BTRaiseCall(States currentState)
        {
            // DON'T FORGET TO UPDATE STATE
            return new PlayerAction(this.Name, "<STUFF>", "<OTHER STUFF>", -1);
        }

        private bool ShouldFold()
        {
            // Do this!
            // If you have rank of 1, all 4 suits, or distant numbers
            return false;
        }

        private void CalculateSafetyAndMaxBet()
        {
            // Do stuff!
        }
        #endregion

        #region Abstract Overrides
        public override PlayerAction BettingRound1(List<PlayerAction> actions, Card[] hand)
        {
            this.actions = actions;
            this.hand = hand;
            stateRound2 = States.Evaluate;
            while (true) //Professor, I'm sorry
            {
                switch (stateRound1)
                {
                    case States.Evaluate:
                        AnalyzeHand();
                        CalculateSafetyAndMaxBet();
                        stateRound1 = States.Check;
                        break;
                    case States.Check:
                        BTCheck(stateRound1);
                        break;
                    case States.Bet:
                        BTBet(stateRound1);
                        break;
                    case States.RaiseCall:
                        BTRaiseCall(stateRound1);
                        break;
                }
            }
        }

        public override PlayerAction BettingRound2(List<PlayerAction> actions, Card[] hand)
        {
            stateRound1 = States.Evaluate;
            while (true)
            {
                switch (stateRound1)
                {
                    case States.Evaluate:
                        AnalyzeHand();
                        CalculateSafetyAndMaxBet();
                        stateRound2 = States.Bet;
                        break;
                    case States.Bet:
                        BTBet(stateRound2);
                        break;
                    case States.RaiseCall:
                        BTRaiseCall(stateRound2);
                        break;
                }
            }
        }

        public override PlayerAction Draw(Card[] hand)
        {
            // Consider high card / 1 pair case
            // Consult table to find cards to discard
            // Return basically nothing if none of those go through
            return new PlayerAction(this.Name, "<STUFF>", "<OTHER STUFF>", -1);
        }
        #endregion
    }
}
