using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestone3
{
    /*
     * This is the Cell Class. My name is Isaiah DeBenedetto and this is my own work. 
     */
    class Cell
    {
        // Initiate properties
        public int row { get; set; } = -1;
        public int column { get; set; } = -1;
        public Boolean visited { get; set; } = false;
        public Boolean live { get; set; } = false;
        public int liveNeighbors { get; set; } = 0;

        // Cell constructor
        public Cell(int x, int y)
        {
            row = x;
            column = y;
        }
    }


}
