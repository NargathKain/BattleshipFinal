using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship2
{
    public class GameMechanics
    {
        

        public int fire;



    }

    public class ShipPlacement
    {
        public int shipsizeSub = 2;
        public int shipsizeDestr = 3;
        public int shipsizeBttl = 4;
        public int shipsizeCar = 5;

        public int saiz = 40;
        public int diff = 80;

        public const int gridSize = 10;
        public int[,] grid = new int[gridSize, gridSize];

        private int findRow(string row)
        {
            int posf1x = 0;
            switch (row)
            {
                case "1":
                    posf1x = 7;
                    break;
                case "2":
                    posf1x = 46;
                    break;
                case "3":
                    posf1x = 85;
                    break;
                case "4":
                    posf1x = 124;
                    break;
                case "5":
                    posf1x = 162;
                    break;
                case "6":
                    posf1x = 201;
                    break;
                case "7":
                    posf1x = 239;
                    break;
                case "8":
                    posf1x = 280;
                    break;
                case "9":
                    posf1x = 316;
                    break;
                case "10":
                    posf1x = 353;
                    break;
                default:
                    Console.WriteLine("please enter column");
                    break;
            }
            return posf1x;

        }


        private int findCol(string col)
        {
            int posf1y = 0;
            switch (col.ToUpper())
            {
                case "A":
                    posf1y = 7;
                    break;
                case "B":
                    posf1y = 46;
                    break;
                case "C":
                    posf1y = 85;
                    break;
                case "D":
                    posf1y = 124;
                    break;
                case "E":
                    posf1y = 162;
                    break;
                case "F":
                    posf1y = 201;
                    break;
                case "G":
                    posf1y = 239;
                    break;
                case "H":
                    posf1y = 280;
                    break;
                case "I":
                    posf1y = 316;
                    break;
                case "J":
                    posf1y = 353;
                    break;
                default:
                    Console.WriteLine("please enter row");
                    break;
            }
            return posf1y;

        }
        
        public void CreateSubmarine(PictureBox playerGridPicBox, string row, string col, bool isHorizontal)
        {
            int sizeX = isHorizontal ? saiz*2 : saiz;
            int sizeY = isHorizontal ? saiz : saiz * 2;
            var image = isHorizontal ? Properties.Resources.submarine : Properties.Resources.submarine_r;
            
            PictureBox submarine1 = new PictureBox()
            {
                Size = new Size(sizeX, sizeY),
                Location = new Point(findCol(col),findRow(row)),
                Image = image,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Parent = playerGridPicBox
            };
            playerGridPicBox.Controls.Add(submarine1);
            
            findShip(row, col, isHorizontal, shipsizeSub);
        }


        public void CreateDestroyer()
        {

        }

        public void CreateBttlship()
        {

        }

        public void CreateCarrier()
        {

        }
                

        

        private void CanPlaceShipAI()
        {

        }


        private int findColPos(string col)
        {
            int posy=0;
            switch (col.ToUpper())
            {
                case "A":
                    posy = 1;
                    break;
                case "B":
                    posy = 2;
                    break;
                case "C":
                    posy = 3;
                    break;
                case "D":
                    posy = 4;
                    break;
                case "E":
                    posy = 5;
                    break;
                case "F":
                    posy = 6;
                    break;
                case "G":
                    posy = 7;
                    break;
                case "H":
                    posy = 8;
                    break;
                case "I":
                    posy = 9;
                    break;
                case "J":
                    posy = 10;
                    break;
                default:
                    Console.WriteLine("please enter row");
                    break;
            }
            return posy;
        }

        private int findRowPos(string row)
        {
            int posx = 0;
            switch (row)
            {
                case "1":
                    posx = 1;
                    break;
                case "2":
                    posx = 2;
                    break;
                case "3":
                    posx = 3;
                    break;
                case "4":
                    posx = 4;
                    break;
                case "5":
                    posx = 5;
                    break;
                case "6":
                    posx = 6;
                    break;
                case "7":
                    posx = 7;
                    break;
                case "8":
                    posx = 8;
                    break;
                case "9":
                    posx = 9;
                    break;
                case "10":
                    posx = 10;
                    break;
                default:
                    Console.WriteLine("please enter column");
                    break;
            }
            return posx;

        }
        private void findShip(string row, string col, bool isHorizontal, int shipsize)
        {
            int pixelX = findCol(col);
            int pixelY = findRow(row);

            int startX = GetGridIndex(pixelX);
            int startY = GetGridIndex(pixelY);

            int posx = findRowPos(row);
            int posy = findColPos(col);

            int rowIndex;
            int colIndex;
            
            for (int i =0; i < 10; i++)
            {
                for (int j=0; j<10; j++)
                {                    
                    if (isHorizontal)
                    {


                        rowIndex = 1;

                    }
                    else
                    {
                        colIndex = 1;
                    }
                }
            }        
                       

        }

        private int GetGridIndex(int PixelPosition)
        {
            int offset = 7;
            int cellsize = 80;
            return (PixelPosition - offset) / cellsize;
        }


        public void GameStart()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j =0; j<10; j++)
                {
                    grid[i, j] = 0;
                }                
            }

        }

        private void GameEnd()
        {

        }



        







    }
}
