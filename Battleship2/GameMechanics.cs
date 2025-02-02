using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Image = System.Drawing.Image;

namespace Battleship2
{
    public class GameMechanics : ShipPlacement
    {             
        
        public const int picsize = 38;        
        //private int[,] playerGrid, aiGrid;        
        //private Dictionary<int,int> playerShips,aiShips;
        //private bool playerTurn; // True παικτης - False τοτε ai

        public int playerHits, playerMiss, playerTurns;
        public int aiHits, aiMiss, aiTurns;             
                
        private int findShotLoc(string col)
        {
            int[] locations = { 7, 46, 85, 124, 162, 201, 239, 280, 316, 353 };

            if (int.TryParse(col, out int num) && num >= 1 && num <= 10)
            {
                return locations[num - 1];
            }
            else
            {
                return locations[findShotPos(col) - 1];
            }
        }
        private int findShotPos(string col)
        {
            int pos;

            if (int.TryParse(col, out pos)) return pos;

            switch (col.ToUpper())
            {
                case "A":
                    pos = 1;
                    break;
                case "B":
                    pos = 2;
                    break;
                case "C":
                    pos = 3;
                    break;
                case "D":
                    pos = 4;
                    break;
                case "E":
                    pos = 5;
                    break;
                case "F":
                    pos = 6;
                    break;
                case "G":
                    pos = 7;
                    break;
                case "H":
                    pos = 8;
                    break;
                case "I":
                    pos = 9;
                    break;
                case "J":
                    pos = 10;
                    break;
            }
            return pos;
        }

        #region Playerfire
        public void PlayerFire(PictureBox AIGridPicBox, string shotRow, string shotCol)
        {
            int locx = findShotLoc(shotRow);
            int locy = findShotLoc(shotCol);
            int x = findShotPos(shotRow);
            int y = findShotPos(shotCol);
            int coord = aiGrid[x, y];
            playerTurns++;
            if (playerHits == 14) { Ending(true); }          
            switch (coord)
            {
                case 0:
                    playerMiss++;
                    PlacePlayerHitORMiss(AIGridPicBox, locx, locy, Properties.Resources.miss1, playerTurns);
                    break;
                case 1:
                    aiGrid[locx, locy] = 2;                    
                    playerHits++;
                    PlacePlayerHitORMiss(AIGridPicBox, locx, locy, Properties.Resources.hit1, playerTurns);
                    break;
                case 2:                    
                    MessageBox.Show("ALREADY ATTACKED CELL");
                    break;
            }
            //int currShipLen = FindShipLength(locx, loc,coord, isHorizontal);      
            //if (CheckShipSunk(coord, isHorizontal, currShipLen)){MessageBox.Show("u sunk");}
        }

        private void PlacePlayerHitORMiss(PictureBox AIGridPicBox, int locX, int locY, Image shotImage, int playerTurns)
        {
            PictureBox hitORMissPlayer = new PictureBox()
            {
                Name = $"hitORMissPlayer{playerTurns}",
                Size = new Size(picsize, picsize),
                Location = new Point(locX, locY),
                Image = shotImage,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Parent = AIGridPicBox
            };
            AIGridPicBox.Controls.Add(hitORMissPlayer);
        }               
        #endregion


        #region AIfire
        public void AIFire(PictureBox playerGridPicBox)
        {
            Random rand = new Random();            
            aiTurns++;
            bool turn = true;
            if (aiHits == 14) { Ending(false); }
            while (turn)
            {
                int posx = rand.Next(0, 11);
                int posy = rand.Next(0, 11);                
                if (revealedCells[posx, posy])
                {                    
                    int locx = findShotLoc(posx.ToString());
                    int locy = findShotLoc(posy.ToString());
                    revealedCells[posx, posy] = false;
                    turn = false;
                    if (playerGrid[posx, posy] == 0)
                    {
                        aiMiss++;                        
                        PlaceHitOrMissAi(playerGridPicBox, locx, locy, Properties.Resources.miss1, aiTurns);
                    }
                    else if (playerGrid[posx, posy] == 1)
                    {
                        aiHits++;
                        PlaceHitOrMissAi(playerGridPicBox, locx, locy, Properties.Resources.hit1, aiTurns);
                    }                 
                }                                                                   
            }                        
        }

        private void PlaceHitOrMissAi(PictureBox playerGridPicBox, int locX, int locY, Image shotImage, int aiTurns)
        {
            PictureBox HitOrMissAI = new PictureBox()
            {
                Name = $"hitOrMissAI{aiTurns}",
                Size = new Size(picsize, picsize),
                Location = new Point(locX, locY),
                Image = shotImage,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Parent = playerGridPicBox
            };
            playerGridPicBox.Controls.Add(playerGridPicBox);
        }
        
        #endregion

        

        private int FindShipLength(int posx, int posy, int coord, bool isHorizontal)
        {
            int length = 0;
            if (isHorizontal)
            {

                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        if (aiGrid[x, y] == coord)
                        {
                            length++;
                        }

                    }
                }
            }
            else
            {

            }


            return length;
        }

        private bool CheckShipSunk(int coord, bool isHorizontal, int currShipLen)
        {
            int lenx = isHorizontal ? currShipLen : 1;
            int leny = isHorizontal ? 1 : currShipLen;                        
            int sum=0;

            for (int x = 0; x < lenx; x++)
            {
                for (int y = 0; y< leny; y++)
                {                    
                    if (aiGrid[x,y]== coord)
                    {                        
                        sum++;
                    }

                }
            }
            if (sum == currShipLen) { return true; } else { return false; }                        
        }
              

        private void Ending(bool ending)
        {            
            var endImage = ending ? Properties.Resources.victory : Properties.Resources.defeat;

            PictureBox endBox = new PictureBox()
            {
                Size = new Size(1339, 715),
                Location = new Point(0, 0),
                Image = endImage,
                SizeMode = PictureBoxSizeMode.StretchImage,
            };

            string endtext = ending ? "Victory" : "Defeat";

            TextBox endwords = new TextBox()
            {
                Text = endtext,
                Font = new Font("Arial", 24, FontStyle.Bold),
                ForeColor = Color.Blue,
                TextAlign = HorizontalAlignment.Center,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                Size = new Size(250, 50),
                Location = new Point()
            };

            Form2 form2 = new Form2();
            form2.Controls.Add(endBox);
            endBox.BringToFront();
            form2.Controls.Add(endwords);
            endwords.BringToFront();
        }        


        

    }

    public class ShipPlacement
    {        
        public int saiz = 40;
        public int diff = 80;

        public const int gridSize = 10;
        public int[,] playerGrid = new int[gridSize, gridSize];
        public int[] shipSize = { 2, 3, 4, 5 };
        public int[,] aiGrid = new int[gridSize, gridSize];        
        public bool[,] revealedCells = new bool[gridSize, gridSize];
        public bool[,] placeAIRevealedCells = new bool[gridSize, gridSize];

        public void GameStart()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    playerGrid[i, j] = 0;
                    aiGrid[i, j] = 0;
                    revealedCells[i, j] = true;
                    placeAIRevealedCells[i, j] = true;
                }
            }
            CanPlaceShipAI();
        }

        private int FindLoc(string col)
        {
            int loc = 0;
            int[] locations = { 7, 46, 85, 124, 162, 201, 239, 280, 316, 353 };
            if (int.TryParse(col, out int num) && (num >0 && num <= 10))
            {
                return locations[num - 1];
            }
            else
            {
                switch (col.ToUpper())
                {
                    case "A":
                        loc = locations[0];
                        break;
                    case "B":
                        loc = locations[1];
                        break;
                    case "C":
                        loc = locations[2];
                        break;
                    case "D":
                        loc = locations[3];
                        break;
                    case "E":
                        loc = locations[4];
                        break;
                    case "F":
                        loc = locations[5];
                        break;
                    case "G":
                        loc = locations[6];
                        break;
                    case "H":
                        loc = locations[7];
                        break;
                    case "I":
                        loc = locations[8];
                        break;
                    case "J":
                        loc = locations[9];
                        break;
                }
                return loc;

            }

        }


        
        
        
        

        private void PlaceShip(int row, int col, bool isHorizontal, int shipSize, bool playerTurn)
        {
            int x,y;
            for (int k=0; k<shipSize; k++)
            {
                x = isHorizontal ? row : row + k;
                y = isHorizontal ? col + k : col;

                if (playerTurn)
                {
                    playerGrid[x, y] = 1;
                }
                else
                {
                    aiGrid[x, y] = 1;

                }
            }                      
        }

        #region AIShipPlacement
        
        private void CanPlaceShipAI()
        {
            foreach (int Ship in shipSize)
            {
                int currShipSize = Ship;
                bool placed = true;
                while (placed)
                {
                    int row, col;
                    bool orientation;
                    Randomizer(out row, out col, out orientation);
                    if (placeAIRevealedCells[row, col] == true)
                    {
                        if (CheckAIPos(row, col, orientation, currShipSize))
                        {
                            PlaceShip(row, col, orientation, currShipSize, false);
                            placed = false;
                            placeAIRevealedCells[row, col] = false;
                        }
                    }

                }
            }
        }

        private static void Randomizer(out int row, out int col, out bool orientation)
        {
            Random random = new Random();
            row = random.Next(0, 10);
            col = random.Next(0, 10);
            orientation = random.Next(2) == 0;
        }

        private bool CheckAIPos(int row, int col, bool isHorizontal, int currShipSize)
        {        
            try
            {
                if (isHorizontal && (row + currShipSize > 10)) return false;
                if (!isHorizontal && (col + currShipSize > 10)) return false;

                if (isHorizontal)
                {
                    for (int i = 0; i < currShipSize; i++)
                    {
                        if (aiGrid[row, col + i] != 0) return false;
                    }

                }
                else
                {
                    for (int i = 0; i < currShipSize; i++)
                    {
                        if (aiGrid[row + i, col] != 0) return false;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Randomizer(out row, out col, out isHorizontal);
                return false;
            }
        }
        #endregion

        #region PlayerPositionNPlacement
        
        public void PlacePlayerShip(PictureBox playerGridPicBox, string row, string col, bool isHorizontal,int shiplen, Image image)
        {            
            for (int i = 0; i < 10; i++)
            {
                for (int j =0 ; j < 10; j++)
                {
                    Console.Write(playerGrid[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(" = - = - = - = - = - =");

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(aiGrid[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(" = - = - = - = - = - =");

            if (!int.TryParse(row, out int posy) || posy < 1 || posy > 10)
            {
                MessageBox.Show("Invalid row input. Please enter a value between 1 and 10.");
                return;
            }
            posy -= 1;
            int posx = col.IndexOfAny("ABCDEFGHIJ".ToCharArray());
            if (posx == -1)
            {
                MessageBox.Show("Invalid column input. Please enter a letter between A and J.");
                return;
            }
            
            PlaceShip(posx, posy, isHorizontal, shiplen, true);
            int locx = FindLoc(col);
            int locy = FindLoc(row);
            CreateShip(playerGridPicBox, locx, locy, isHorizontal, shiplen, image);
            
        }

        private bool checkShipPosition(int row, int col, bool isHorizontal, int shiplen)
        {
            bool placed = true;

            // Check if the ship fits horizontally
            if (isHorizontal)
            {
                if (col + shiplen > 10) // Check if the ship goes out of bounds on the right
                {
                    placed = false;
                }
                else
                {
                    for (int i = 0; i < shiplen; i++) // Iterate up to shiplen
                    {
                        if (playerGrid[row, col + i] != 0) // Check if there's already a ship
                        {
                            placed = false;
                            break; // No need to check further if the position is blocked
                        }
                    }
                }
            }
            // Check if the ship fits vertically
            else
            {
                if (row + shiplen > 10) // Check if the ship goes out of bounds at the bottom
                {
                    placed = false;
                }
                else
                {
                    for (int i = 0; i < shiplen; i++) // Iterate up to shiplen
                    {
                        if (playerGrid[row + i, col] != 0) // Check if there's already a ship
                        {
                            placed = false;
                            break; // No need to check further if the position is blocked
                        }
                    }
                }
            }

            return placed;
        }






        private void CreateShip(PictureBox playerGridPicBox, int locx, int locy, bool isHorizontal, int shiplen, Image image)
        {
            int sizeX = isHorizontal ? saiz * shiplen : saiz;
            int sizeY = isHorizontal ? saiz : saiz * shiplen;

            PictureBox ship = new PictureBox()
            {
                Name = $"ship{shiplen}",
                Size = new Size(sizeX, sizeY),
                Location = new Point(locx, locy),
                Image = image,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Parent = playerGridPicBox
            };
            playerGridPicBox.Controls.Add(ship);
        }

        #endregion




    }
}
