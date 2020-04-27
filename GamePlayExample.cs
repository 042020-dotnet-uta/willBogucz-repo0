using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace RPS_Game
{
    static class GamePlay
    {
        private static Player p1 = new Player();
        private static Player p2 = new Player();
        private static Game game = new Game();
        private static Random rand = new Random(); // instantiates the Random Class
        private static string[] rps = { "Rock", "Paper", "Scissor" }; // Declare an string array to store Rock, Paper,and Scissors strings
        private static int p1Wins = 0; // win count for player 1
        private static int p2Wins = 0; // win count for player 2
        private static int ties = 0; // amount of tied rounds
        private static int rounds = 0; // number of rounds

        //get the users data
        public static void GetPlayersName()
        {
            Console.WriteLine("Enter Player1 Name: "); //prompts user to input player 1 name
            String player = Console.ReadLine(); //takes input from user and stores it as player 1 name
            p1.Name = player;

            Console.WriteLine("Enter Player2 Name: "); //prompts user to input player 1 name
            player = Console.ReadLine(); //takes input from user and stores it as player 1 name
            p2.Name = player;
        }

        public static void RunGame()
        {
            //create the game with players
            game = new Game(p1,p2);
            //game.p1 = p1;
            //game.p2 = p2;

            //run rounds till a player has 2 wins. 
            while (p1Wins < 2 && p2Wins < 2)
            {
                Round r1 = RunRound();
                game.Rounds.Add(r1);
            }

            AssignWinner();
        }

        /// <summary>
        /// this method runs one round
        /// </summary>
        private static Round RunRound()
        {
            //get random coices for each player
            int p1rand = GetRandomNum();
            int p2rand = GetRandomNum();

            //create a round to hold the data for this round
            Round oneRound = new Round();

            //record what each player chose in the round
            oneRound.p1Choice = rps[p1rand];
            oneRound.p2Choice = rps[p2rand];

            return DetermineRoundWinner(p1rand, p2rand, oneRound);

        }

        /// <summary>
        /// This method takes the choices of p1 and p2 and determins a winner
        /// It also updates all model fields according to the winner.
        /// </summary>
        /// <param name="p1rand"></param>
        /// <param name="p2rand"></param>
        /// <param name="oneRound"></param>
        /// <returns></returns>
        private static Round DetermineRoundWinner(int p1rand, int p2rand, Round oneRound)
        {
            int win = p1rand - p2rand + 2;//determine the winner

            switch (win)
            { //win is mostly unique varying with what each player picks
                case 0: //p1 rock p2 scissor p1 wins
                    p1Wins++;
                    oneRound.Winner = game.p1;
                    oneRound.pWinner = false;
                    break;
                case 1: // p1 lost since result is negative rock(0) - paper(1) or 1 - 2
                    p2Wins++;
                    oneRound.Winner = game.p2;//updating round
                    oneRound.pWinner = true;
                    break;
                case 2: // tie
                    ties++;
                    oneRound.Winner = null;
                    break;
                case 3:// p1 wins as 1 - 0 or 2 - 1;
                    p1Wins++;
                    oneRound.Winner = game.p1;
                    oneRound.pWinner = false;
                    break;
                case 4://p1 scissor p2 rock p2 wins
                    p2Wins++;
                    oneRound.Winner = game.p2;
                    oneRound.pWinner = true;
                    break;
                default:
                    break;
            }

            return oneRound;
        }

        private static int GetRandomNum()
        {
            return rand.Next(3);
        }


        //check the p1Wins and p2Wins to see who won.
        public static void AssignWinner()
        {
            if (p1Wins == 2)
            {
                game.Winner = game.p1;  //assign the winner to the winners spot in the game
                game.Winner.Wins++;     //increment the winners wins
                game.p2.Losses++;       //decrement the loosers losses
            }
            else if(p2Wins == 2)
            {
                game.Winner = game.p2;  //assign the winner to the winners spot in the game
                game.Winner.Wins++;     //increment the winners wins
                game.p1.Losses++;       //decrement the loosers losses
            }
            else
            {
                //just in case the wrong number of games is played
                Console.WriteLine("something happened and neither player won with 2 wins.");
            }
        }


        // Print the results bases on the objects 
        public static void PrintResults()
        {
            foreach (var round in game.Rounds)
            {
                int r = 1;
                if (round.Winner == null) 
                {
                    Console.WriteLine($"In round {r}, there was no winner because {game.p1.Name} chose {round.p1Choice} and {game.p2.Name} chose {round.p2Choice}.");
                }
                else if (round.Winner != null && round.pWinner == false)
                {
                    Console.WriteLine($"In round {r}, {round.Winner.Name} won by choosing {round.p1Choice} over {game.p2.Name}'s {round.p2Choice}.");
                }
                else
                {
                    Console.WriteLine($"In round {r}, {round.Winner.Name} won by choosing {round.p2Choice} over {game.p1.Name}'s {round.p1Choice}.");
                }
                r++;
            }

            Console.WriteLine($"Overall, {game.Winner.Name} won.");
        }
    }
}
