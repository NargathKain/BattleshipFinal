using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;
using Image = System.Drawing.Image;

namespace Battleship2
{
    public class GameMechanics
    {             
        
        public const int picsize = 38;        
        //private int[,] playerGrid, aiGrid;        
        //private Dictionary<int,int> playerShips,aiShips;
        //private bool playerTurn; // True παικτης - False τοτε ai

        public int playerHits  , playerMiss , playerTurns ;
        public int aiHits , aiMiss , aiTurns ;
        public const int gridSize = 10;
        public int[,] playerGrid = new int[gridSize, gridSize];
        public int[,] aiGrid = new int[gridSize, gridSize];
        public bool[,] revealedCells = new bool[gridSize, gridSize];
        //int[,] aiGrid = shipPlacement.aiGrid;
        //ShipPlacement shipPlacement = new ShipPlacement();
        ShipPlacement shipPlacement;


        public GameMechanics(ShipPlacement shipPlacement)
        {
            this.shipPlacement = shipPlacement;
            this.playerGrid = shipPlacement.playerGrid;
            this.aiGrid = shipPlacement.aiGrid;
            this.revealedCells = shipPlacement.revealedCells;
        }


        #region Playerfire
        public void PlayerFire(PictureBox AIGridPicBox, string shotRow, string shotCol, Form form2)
        {
            //if (playerTurns ==0) { startMechanics(); }
            //int[,] aiGrid = shipPlacement.AiGrid;
            int locx = shipPlacement.FindLoc(shotCol);
            int locy = shipPlacement.FindLoc(shotRow);
            int x = shipPlacement.FindPos(shotCol);
            int y = shipPlacement.FindPos(shotRow);
            playerTurns++;
            switch (aiGrid[x, y])
            {
                case 0:
                    playerMiss++;
                    PlacePlayerHitORMiss(AIGridPicBox, locx, locy, Properties.Resources.miss1, playerTurns);
                    break;
                case 1:                 
                    playerHits++;
                    PlacePlayerHitORMiss(AIGridPicBox, locx, locy, Properties.Resources.hit1, playerTurns);
                    aiGrid[x, y] = 2;
                    if (playerHits == 13) { Ending(form2, true); }
                    break;
                default:                   
                    MessageBox.Show("ALREADY ATTACKED CELL");
                    break;
            }
            Console.WriteLine(" = - = PLAYER = - =");
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(playerGrid[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(" = - = - = - = - = - =");
            Console.WriteLine(" = - = opponent = - =");
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(aiGrid[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(" = - = - = - = - = - =");
            //int currShipLen = FindShipLength(locx, loc,coord, isHorizontal);      
            //if (CheckShipSunk(coord, isHorizontal, currShipLen)){MessageBox.Show("u sunk");}
        }

        

        private void PlacePlayerHitORMiss(PictureBox AIGridPicBox, int locX, int locY, Image shotImage, int playerTurns)
        {
            PictureBox[] hitORMissPlayer = new PictureBox[100];
            hitORMissPlayer[playerTurns] = new PictureBox()
            {
                Name = $"hitORMissPlayer{playerTurns}",
                Size = new Size(picsize, picsize),
                Location = new Point(locX, locY),
                Image = shotImage,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Parent = AIGridPicBox
            };
            AIGridPicBox.Controls.Add(hitORMissPlayer[playerTurns]);
            hitORMissPlayer[playerTurns].BringToFront();
        }               
        #endregion


        #region AIfire
        public void AIFire(PictureBox playerGridPicBox, Form form2)
        {
            aiTurns++;
            int posx, posy;
            randominder(out posx, out posy);
            if (revealedCells[posx, posy] == true)
            {
                if (checkAIFire(posx, posy))
                {
                    //int locx = FindLoc(posx.ToString());
                    //int locy = FindLoc(posy.ToString());
                    int locx = shipPlacement.FindLoc(posx.ToString());
                    int locy = shipPlacement.FindLoc(posy.ToString());
                    Console.WriteLine("attack at"+posx+","+posy);
                    revealedCells[posx, posy] = false;
                    switch (playerGrid[posx,posy])
                    {
                        case 0:
                            aiMiss++;
                            PlaceHitOrMissAi(playerGridPicBox, locx, locy, Properties.Resources.miss1, aiTurns);
                            break;
                        case 1:
                            aiHits++;
                            PlaceHitOrMissAi(playerGridPicBox, locx, locy, Properties.Resources.hit1, aiTurns);
                            playerGrid[posx, posy] = 2;
                            if (aiHits == 13) { Ending(form2, false); }
                            break;
                        default:
                            MessageBox.Show("ALREADY ATTACKED CELL");
                            break;
                    }
                }

            }
        }

        private static void randominder(out int posx, out int posy)
        {
            Random rand = new Random();
            posx = rand.Next(0, 10);
            posy = rand.Next(0, 10);
        }

        private bool checkAIFire(int posx, int posy)
        {
            try
            {
                if(posx > 10 || posy >10) return false;
                if(posx < 0 || posy < 0) return false;
                if (posx >=0 && posx<10 ) return true;
                if (posy >=0 && posy < 10) return true;

                if (!shipPlacement.revealedCells[posx, posy]) { return false; }

                if (playerGrid[posx, posy] == 0 || playerGrid[posx, posy] == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                randominder(out posx, out posy);
                return false;
            }
        }

        private void PlaceHitOrMissAi(PictureBox playerGridPicBox, int locX, int locY, Image shotImage, int aiTurns)
        {
            PictureBox[] hitOrMissAI = new PictureBox[100];
            hitOrMissAI[aiTurns] = new PictureBox()
            {
                Name = $"hitOrMissAI{aiTurns}",
                Size = new Size(picsize, picsize),
                Location = new Point(locX, locY),
                Image = shotImage,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Parent = playerGridPicBox
            };
            
            playerGridPicBox.Controls.Add(hitOrMissAI[aiTurns]);
            hitOrMissAI[aiTurns].BringToFront();
        }
        
        #endregion

        private void Ending(Form form2, bool ending)
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
                Size = new Size(250, 50)
                //Location = new Point(form2.ClientSize.Width ),
                
            };
            endwords.Location = new Point((form2.ClientSize.Width - endwords.Width) / 2, (form2.ClientSize.Height - endwords.Height) / 2);

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
        public int[] playerHMTurns = new int[100];
        public int[] aiHMTurns = new int[100];

        //public int[,] PlayerGrid => playerGrid;
        //public int[,] AiGrid => aiGrid;

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
            for(int i =0; i < 100; i++)
            {
                playerHMTurns[i] = i;
                aiHMTurns[i] = i;
            }
            CanPlaceShipAI();
        }

        public int FindLoc(string col)
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
            if (isHorizontal)
            {
                for (int k = col; k < col + shipSize; k++)
                {
                    if (playerTurn)
                    {
                        playerGrid[row, k] = 1;
                    }
                    else
                    {
                        aiGrid[row, k] = 1;
                    }
                }
            }
            else
            {
                for (int k = row; k < row + shipSize; k++)
                {
                    if (playerTurn)
                    {
                        playerGrid[k, col] = 1;
                    }
                    else
                    {
                        aiGrid[k, col] = 1;
                    }
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
            int posx = FindPos(row);
            int posy = FindPos(col);
            
            if (checkShipPosition(posx, posy, isHorizontal, shiplen))
            {
                PlaceShip(posx, posy, isHorizontal, shiplen, true);
                int locx = FindLoc(col);
                int locy = FindLoc(row);
                CreateShip(playerGridPicBox, locx, locy, isHorizontal, shiplen, image);
            }
            else
            {
                MessageBox.Show("Invalid ship placement");
            }

            Console.WriteLine(" = - = PLAYER = - =");
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(playerGrid[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(" = - = - = - = - = - =");
            Console.WriteLine(" = - = opponent = - =");
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(aiGrid[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(" = - = - = - = - = - =");
        }
        
        public int FindPos(string col)
        {
            int pos=0;

            if(int.TryParse(col, out pos))
            {
                return pos - 1;
            }
            else
            {
                switch (col.ToUpper())
                {
                    case "A":
                        pos = 0;
                        break;
                    case "B":
                        pos = 1;
                        break;
                    case "C":
                        pos = 2;
                        break;
                    case "D":
                        pos = 3;
                        break;
                    case "E":
                        pos = 4;
                        break;
                    case "F":
                        pos = 5;
                        break;
                    case "G":
                        pos = 6;
                        break;
                    case "H":
                        pos = 7;
                        break;
                    case "I":
                        pos = 8;
                        break;
                    case "J":
                        pos = 9;
                        break;
                }
                return pos;
            }
        }      

        

        private bool checkShipPosition(int row, int col, bool isHorizontal, int shiplen)
        {
            try
            {
                // Horizontal ships: Check columns (x-axis)
                if (isHorizontal && (col + shiplen > 9)) return false;

                // Vertical ships: Check rows (y-axis)
                if (!isHorizontal && (row + shiplen > 9)) return false;

                // Check each cell the ship would occupy
                for (int i = 0; i < shiplen; i++)
                {
                    int x = isHorizontal ? col + i : col; // Column index
                    int y = isHorizontal ? row : row + i; // Row index

                    if (playerGrid[y, x] != 0) return false; // [row, column]
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            
        }






        private void CreateShip(PictureBox playerGridPicBox, int locx, int locy, bool isHorizontal, int shiplen, Image image)
        {
            int sizeX = isHorizontal ? saiz * shiplen : saiz;
            int sizeY = isHorizontal ? saiz : saiz * shiplen;
            
            switch (shiplen)
            {
                case 2:
                    submarine(playerGridPicBox, locx, locy, shiplen, image, sizeX, sizeY);
                    break;
                case 3:
                    destroyer(playerGridPicBox, locx, locy, shiplen, image, sizeX, sizeY);
                    break;
                case 4:
                    bttl(playerGridPicBox, locx, locy, shiplen, image, sizeX, sizeY);
                    break;
                case 5:
                    carrier(playerGridPicBox,  locx,  locy, shiplen, image, sizeX, sizeY);
                    break;
            }
        }

        
        private static void submarine(PictureBox playerGridPicBox, int locx, int locy, int shiplen, Image image, int sizeX, int sizeY)
        {
            PictureBox submarine = new PictureBox()
            {
                Name = $"ship{shiplen}",
                Size = new Size(sizeX, sizeY),
                Location = new Point(locx, locy),
                Image = image,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Parent = playerGridPicBox
            };
            playerGridPicBox.Controls.Add(submarine);

        }
        private static void destroyer(PictureBox playerGridPicBox, int locx, int locy, int shiplen, Image image, int sizeX, int sizeY)
        {
            PictureBox destroyer = new PictureBox()
            {
                Name = $"ship{shiplen}",
                Size = new Size(sizeX, sizeY),
                Location = new Point(locx, locy),
                Image = image,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Parent = playerGridPicBox
            };
            playerGridPicBox.Controls.Add(destroyer);
        }
        private static void bttl(PictureBox playerGridPicBox, int locx, int locy, int shiplen, Image image, int sizeX, int sizeY)
        {
            PictureBox bttl = new PictureBox()
            {
                Name = $"ship{shiplen}",
                Size = new Size(sizeX, sizeY),
                Location = new Point(locx, locy),
                Image = image,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Parent = playerGridPicBox
            };
            playerGridPicBox.Controls.Add(bttl);
        }
        private static void carrier(PictureBox playerGridPicBox, int locx, int locy, int shiplen, Image image, int sizeX, int sizeY)
        {
            PictureBox carrier = new PictureBox()
            {
                Name = $"ship{shiplen}",
                Size = new Size(sizeX, sizeY),
                Location = new Point(locx, locy),
                Image = image,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Parent = playerGridPicBox
            };
            playerGridPicBox.Controls.Add(carrier);

        }

        #endregion




    }
}
