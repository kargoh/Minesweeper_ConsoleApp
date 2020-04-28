using Milestone3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestone3
{
    /*
     * This is the Program Class. My name is Isaiah DeBenedetto and this is my own work. 
     */

    class Program
    {
        static void Main(string[] args)
        {
            int size = 0;
            while (size == 0) { 
                try
                {
                    Console.WriteLine("Choose a board size: ");
                    size = int.Parse(Console.ReadLine()) + 1;
                }
                catch
                {
                    Console.WriteLine("Please enter an appropriate board size");
                }
            }
            Board myBoard = new Board(size);
            int totalCells = myBoard.size * myBoard.size;

            // Prompt the user for difficulty and store it on a string 
            Console.WriteLine("Choose difficulty: (easy (10%), medium(30%), hard(50%))");
            String difficulty = Console.ReadLine(); // the user inputs incorrect information, easy will be selected by default

            //setup the board
            myBoard.setupLiveNeighbors(difficulty);

            // Prompt user to begin game with the while loop and give user the end condition to type "stop"
            Console.WriteLine("Type stop to stop!");
            String userInput = "";
            while (userInput != "stop")
            {
                int row = -1, column = -1;
                // Prompt the user size of board
                while (row == -1 && column == -1 && row <= (size - 1) && column <= (size - 1)) {
                    try
                    {
                        Console.WriteLine("Enter row!");
                        row = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter column!");
                        column = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Please enter a digit within the bounds of the board");
                    }
                }
                

                try
                {
                    // Set isVisited to true if user selects cell and also prompt the user to pick a different cell if the same cell is selected.
                    if (myBoard.theGrid[row, column].visited == false)
                    {
                        myBoard.floodFill(row, column);
                    }
                    else
                    {
                        Console.WriteLine("Please choose another cell.");
                    }
                }
                catch
                {
                    Console.WriteLine("Please enter a cell within bounds");
                }

                // Print board
                if (row != -1 && column != -1 && row !<= (size - 1) && column !<= (size - 1))
                myBoard.printBoard(false);

                try
                {
                    // End game if bomb was chosen, set the userInput to "stop" to end the game, and display end game board.
                    if (myBoard.theGrid[row, column].live == true)
                    {
                        userInput = "stop";
                        Console.WriteLine("GAME OVER. YOU LANDED ON A BOMB.");
                        myBoard.printBoard(true);
                    }
                }
                catch
                {
                    Console.WriteLine("Please enter a cell within bounds");
                }

                // End game by winning, to win you have the same amount of bombs as total cells - visited cells
                if (myBoard.bombCounter == (totalCells - myBoard.visitedCells))
                {
                    userInput = "stop";
                    Console.WriteLine("You Win!");
                    myBoard.printBoard(true);
                }

            }

            //end program
            Console.ReadLine();
        }
    }
}
