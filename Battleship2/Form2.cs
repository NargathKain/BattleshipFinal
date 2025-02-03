using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship2
{
    public partial class Form2 : Form
    {
        bool horizontalSubmarine, horizontalDestroyer, horizontalCorvette, horizontalCarrier; // T/F analoga 
        string destroyerRow, corvetteRow, carrierRow; // 1-10
        string destroyerCol, corvetteCol, carrierCol; // a-j
        private Timer timer;
        private int secondsElapsed=0;

        string name = null;

        private ShipPlacement shipPlacement;
        private GameMechanics paixnidi;


        public Form2()
        {
            InitializeComponent();
            MaximizeBox = false;
            CenterToScreen();
            shipPlacement = new ShipPlacement();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        

        #region enable_buttons
        private void submarineButton_Click(object sender, EventArgs e)
        {
            submarineColLabel.Visible = true;
            submarineRowLabel.Visible = true;
            submarineRowtextBox.Visible = true;
            submarineColtextBox.Visible = true;
            submarineHorizontalcheckBox.Visible = true;
            submarineVerticalcheckBox.Visible = true;
            submarineOKbutton.Visible = true;
        }

        private void destroyerButton_Click(object sender, EventArgs e)
        {
            destroyerColLabel.Visible = true;
            destroyerColtextBox.Visible = true;
            destroyerRowLabel.Visible = true;
            destroyerRowtextBox.Visible = true;
            destroyerHorizontalcheckBox.Visible = true;
            destroyerVerticalcheckBox.Visible = true;
            destroyerOKbutton.Visible = true;          
        }

        private void corvetteButton_Click(object sender, EventArgs e)
        {
            corvetteColLabel.Visible = true;
            corvetteColtextBox.Visible = true;
            corvetteRowLabel.Visible = true;
            corvetteRowtextBox.Visible = true;
            corvetteHorizontalcheckBox.Visible = true;
            corvetteVerticalcheckBox.Visible = true;
            corvetteOKbutton.Visible = true;
        }

        private void carrierButton_Click(object sender, EventArgs e)
        {
            carrierColLabel.Visible = true;
            carrierColtextBox.Visible = true;
            carrierRowLabel.Visible = true;
            carrierRowtextBox.Visible = true;
            carrierHorizontalcheckBox.Visible = true;
            carrierVerticalcheckBox.Visible = true;
            carrierOKbutton.Visible = true;           
        }
        #endregion
        
        #region PLAYERDEBUG
        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            name = nameTextBox.Text;
            NameCheck(name);            
        }

        private bool NameCheck(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                MessageBox.Show("Please enter a name");
                return false;
            }
            if (Name.Any(char.IsDigit))
            {
                MessageBox.Show("Please enter a valid name");
                return false;
            }            
            return true;                
        }

        
        #endregion

        #region placeSubmarine
        private void submarineRowtextBox_TextChanged(object sender, EventArgs e)
        {
            //submarineRow = submarineRowtextBox.Text;
            
        }

        private void submarineColtextBox_TextChanged(object sender, EventArgs e)
        {
            //submarineCol = submarineColtextBox.Text;
            
        }

        private void submarineVerticalcheckBox_CheckedChanged(object sender, EventArgs e)
        {            
            if (submarineVerticalcheckBox.Checked)
            {
                submarineHorizontalcheckBox.Checked = false;
                horizontalSubmarine = false;
            }

        }

        private void submarineHorizontalcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(submarineHorizontalcheckBox.Checked)
            {
                submarineVerticalcheckBox.Checked = false;
                horizontalSubmarine = true;
            }            
        }     
        
        private void submarineOKbutton_Click(object sender, EventArgs e)
        {
            if (!submarineHorizontalcheckBox.Checked && !submarineVerticalcheckBox.Checked)
            {
                MessageBox.Show("Please select ship orientation");
            }
            //ShipPlacement shipPlacement1 = new ShipPlacement();
            var image = horizontalSubmarine ? Properties.Resources.submarine : Properties.Resources.submarine_r;
            //ShipPlacement submarine1 = new ShipPlacement();
            //submarine1 = shipPlacement;
            shipPlacement.PlacePlayerShip(playerGridPicBox, submarineRowtextBox.Text, submarineColtextBox.Text, horizontalSubmarine, 2, image);
            
        }
        #endregion

        #region placeDestroyer
        private void destroyerRowtextBox_TextChanged(object sender, EventArgs e)
        {
            destroyerRow = destroyerRowtextBox.Text;
        }

        private void destroyerColtextBox_TextChanged(object sender, EventArgs e)
        {
            destroyerCol = destroyerColtextBox.Text;
        }

        private void destroyerVerticalcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (destroyerVerticalcheckBox.Checked)
            {
                destroyerHorizontalcheckBox.Checked = false;
                horizontalDestroyer = false;
            }
        }

        private void destroyerHorizontalcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (destroyerHorizontalcheckBox.Checked)
            {
                destroyerVerticalcheckBox.Checked = false;
                horizontalDestroyer = true;
            }
        }

        private void destroyerOKbutton_Click(object sender, EventArgs e)
        {
            if (!destroyerHorizontalcheckBox.Checked && !destroyerVerticalcheckBox.Checked)
            {
                MessageBox.Show("Please select ship orientation");
            }
            //ShipPlacement shipPlacement2 = new ShipPlacement();
            var image = horizontalDestroyer ? Properties.Resources.destroyership1 : Properties.Resources.destroyership_r;
            //ShipPlacement destroyer1 = new ShipPlacement();
            //destroyer1 = shipPlacement;
            shipPlacement.PlacePlayerShip(playerGridPicBox, destroyerRow, destroyerCol, horizontalDestroyer, 3, image);
        }

        #endregion

        #region placeCorvette

        private void corvetteVerticalcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (corvetteVerticalcheckBox.Checked)
            {
                corvetteHorizontalcheckBox.Checked = false;
                horizontalCorvette = false;
            }
        }

        private void corvetteHorizontalcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (corvetteHorizontalcheckBox.Checked)
            {
                corvetteVerticalcheckBox.Checked = false;
                horizontalCorvette = true;
            }
        }
        private void corvetteOKbutton_Click(object sender, EventArgs e)
        {
            if (!corvetteHorizontalcheckBox.Checked && !corvetteVerticalcheckBox.Checked)
            {
                MessageBox.Show("Please select ship orientation");
            }
            var image = horizontalCorvette ? Properties.Resources.btlship1 : Properties.Resources.btlship_r;
            shipPlacement.PlacePlayerShip(playerGridPicBox, corvetteRowtextBox.Text, corvetteColtextBox.Text, horizontalCorvette, 4, image);
        }

        #endregion

        #region placeCarrier
        private void carrierRowtextBox_TextChanged(object sender, EventArgs e)
        {
            carrierRow = carrierRowtextBox.Text;
        }

        

        private void carrierColtextBox_TextChanged(object sender, EventArgs e)
        {
            carrierCol = carrierColtextBox.Text;
        }

        

        private void carrierVerticalcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (carrierVerticalcheckBox.Checked)
            {
                carrierHorizontalcheckBox.Checked = false;
                horizontalCarrier = true;
            }
        }

        private void carrierHorizontalcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (carrierHorizontalcheckBox.Checked)
            {
                carrierVerticalcheckBox.Checked = false;
                horizontalCarrier = true;
            }
        }

        private void carrierOKbutton_Click(object sender, EventArgs e)
        {
            if (!carrierHorizontalcheckBox.Checked && !carrierVerticalcheckBox.Checked)
            {
                MessageBox.Show("Please select ship orientation");
            }
            var image = horizontalCarrier ? Properties.Resources.carriership1 : Properties.Resources.carriership_r;
            shipPlacement.PlacePlayerShip(playerGridPicBox, carrierRow, carrierCol, horizontalCarrier, 5, image);
        }
        #endregion

        private void battleButton_Click(object sender, EventArgs e)
        {
            foreach(Control ctrl in groupBox3.Controls)
            {
                ctrl.Enabled = false;
            }

            groupBox1.Visible = true;
            paixnidi = new GameMechanics(shipPlacement);

            timer1.Start();
            timer1.Tick += timer1_Tick;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            secondsElapsed++;
            if(timeTextBox != null)
            {
                timeTextBox.Text = secondsElapsed.ToString();
            }
        }

        private async void fireBttn_Click(object sender, EventArgs e)
        {
            paixnidi.PlayerFire(AIGridPicBox, shotRowTextBox.Text, shotColTextBox.Text, this.FindForm());
            turnTextBox.Text = paixnidi.aiTurns.ToString();
            oppHitsTextBox.Text = paixnidi.aiHits.ToString();
            plhitsTextBox.Text = paixnidi.playerHits.ToString();
            oppMissTextBox.Text = paixnidi.aiMiss.ToString();
            plMissTextBox.Text = paixnidi.playerMiss.ToString();
            await Task.Delay(2000);
            shotRowTextBox.Clear();
            shotColTextBox.Clear();
            paixnidi.AIFire(playerGridPicBox, this.FindForm());
        }

        



        private void startButton_Click(object sender, EventArgs e)
        {
            shipPlacement.GameStart();
        }








    }
}
