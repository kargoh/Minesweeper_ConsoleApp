using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestone3
{
    /*
     * This is the Board Class. My name is Isaiah DeBenedetto and this is my own work. 
     */
    class Board
    {
        // initiate default properties
        public int size { get; set; }
        public int visitedCells { get; set; } = 0;
        public Cell[,] theGrid { get; set; }
        public String difficulty { get; set; }

        public int bombCounter { get; set; } = 0;

        // default constructor with board size
        public Board(int size)
        {
            // creating the grid
            this.size = size;
            theGrid = new Cell[size, size];

            // nested for loop to fill each cell
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    theGrid[i, j] = new Cell(i, j);
                }
            }

        }
        // create live neighbor and difficulty method
        public void setupLiveNeighbors(String difficulty)
        {

            // prompt user to choose difficulty

            double percentageDifficulty = -1;
            switch (difficulty)
            {
                case "easy":
                    percentageDifficulty = 1;
                    break;
                case "medium":
                    percentageDifficulty = 3;
                    break;
                case "hard":
                    percentageDifficulty = 5;
                    break;
                    // if an improper input is given, the game will automatically start with easy difficulty
                default:
                    Console.WriteLine("Difficulty not found! Creating game with Easy difficulty");
                    percentageDifficulty = 1;
                    break;
            }

            Random rand = new Random();

            // setting up bombs based on difficulty and counting how many bombs were place
            
            for (int r = 0; r < this.size; r++)
            {
                for (int c = 0; c < this.size; c++)
                {
                    if (rand.Next(10) < percentageDifficulty)
                    {
                        this.theGrid[r, c].live = true;
                        bombCounter++;
                    }
                }
            }
        }

        // calculate bombs (based off of the 8 sides of each cell)
        public int calculateLiveNeighbors(Cell currentCell)
        {
            int liveNeighborsCounter = 0;
            try { if (theGrid[currentCell.row + 1, currentCell.column].live == true) { liveNeighborsCounter++; } } catch { }
            try { if (theGrid[currentCell.row - 1, currentCell.column].live == true) { liveNeighborsCounter++; } } catch { }
            try { if (theGrid[currentCell.row, currentCell.column + 1].live == true) { liveNeighborsCounter++; } } catch { }
            try { if (theGrid[currentCell.row, currentCell.column - 1].live == true) { liveNeighborsCounter++; } } catch { }
            try { if (theGrid[currentCell.row + 1, currentCell.column + 1].live == true) { liveNeighborsCounter++; } } catch { }
            try { if (theGrid[currentCell.row + 1, currentCell.column - 1].live == true) { liveNeighborsCounter++; } } catch { }
            try { if (theGrid[currentCell.row - 1, currentCell.column + 1].live == true) { liveNeighborsCounter++; } } catch { }
            try { if (theGrid[currentCell.row - 1, currentCell.column - 1].live == true) { liveNeighborsCounter++; } } catch { }
            return liveNeighborsCounter;

        }

        public void floodFill(int x, int y)
        {
            if (isInBounds(x, y) && !this.theGrid[x, y].live && !this.theGrid[x, y].visited)
            {
                this.theGrid[x, y].visited = true;
                visitedCells++;
                if (this.calculateLiveNeighbors(this.theGrid[x, y]) == 0)
                {
                    floodFill(x + 1, y);
                    floodFill(x - 1, y);
                    floodFill(x, y + 1);
                    floodFill(x, y - 1);
                }
            }
        }

        public Boolean isInBounds(int x, int y)
        {
            if (x >= 0 && x < this.size && y >= 0 && y < this.size)
            {
                return true;
            }
            else
            {
                return false;

            }

        }

        // print the board of the game
        public void printBoard(Boolean gameEnd)
        {
            Console.WriteLine("   ");
            Console.Write("     ");
            for (int i = 0; i < this.size; i++)
            {
                if (i < 10)
                {
                    Console.Write(i + "   ");
                }
                else
                {
                    Console.Write(i + "  ");
                }
            }

            Console.WriteLine();
            for (int i = 0; i < this.size; i++)

            {
                Console.Write("   ");
                for (int x = 0; x < this.size; x++)
                {
                    Console.Write("+---");
                }
                Console.Write("+");
                Console.WriteLine();
                if (i < 10)
                {
                    Console.Write(i + "  |");
                }
                else
                {
                    Console.Write(i + " |");
                }

                for (int j = 0; j < this.size; j++)
                {
                    Cell c = this.theGrid[i, j];
                    if (gameEnd == false)
                    {
                        if (c.visited == false)
                        {
                            Console.Write(" ? " + "|");
                        }
                        else
                        if (c.live == true)
                        {
                            Console.Write(" X " + "|");
                        }
                        else
                        {
                            if (this.calculateLiveNeighbors(c) > 0)
                            {
                                // when I was testing the game it was hard to see where the number was when I chose the cell so I decided to implement colors into the numbers. The switch statement just depends on what number is in the cell and if there is no number than a "`" symbol is placed with no color.
                                switch (this.calculateLiveNeighbors(c))
                                {
                                    case 1:
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        break;
                                    case 2:
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        break;
                                    case 3:
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        break;
                                    default:
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        break;
                                }

                                Console.Write(" " + this.calculateLiveNeighbors(c));
                                Console.ResetColor();
                                Console.Write(" |");
                            }
                            else 
                            {
                                Console.Write(" ` " + "|");
                            }
                        }
                    }
                    else
                    {
                        if (c.live == true)
                        {
                            Console.Write(" X " + "|");
                        }
                        else
                        {
                            if (this.calculateLiveNeighbors(c) > 0)
                            {
                                switch (this.calculateLiveNeighbors(c))
                                {
                                    case 1:
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        break;
                                    case 2:
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        break;
                                    case 3:
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        break;
                                    default:
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        break;
                                }

                                Console.Write(" " + this.calculateLiveNeighbors(c));
                                Console.ResetColor();
                                Console.Write(" |");
                            }
                            else
                            {
                                Console.Write(" ` " + "|");
                            }
                        }
                    }
                }
                Console.WriteLine();
            }
            Console.Write("   ");
            for (int x = 0; x < this.size; x++)
            {
                Console.Write("+---");
            }
            Console.Write("+\n");
        }
    }
}
