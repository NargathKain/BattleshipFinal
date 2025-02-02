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
        bool horizontalSubmarine, horizontalDestroyer, horizontalBttl, horizontalCarrier; // T/F analoga 
        string destroyerRow, bttlRow, carrierRow; // 1-10
        string destroyerCol, bttlCol, carrierCol; // a-j
        string shotRow, shotCol;      

        string name = null;
        ShipPlacement shipPlacement = new ShipPlacement();
        GameMechanics paixnidi = new GameMechanics();            

        public Form2()
        {
            InitializeComponent();
            MaximizeBox = false;
            CenterToScreen();                   
                        
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

        private void bttlshipButton_Click(object sender, EventArgs e)
        {
            bttlshipColLabel.Visible = true;
            bttlshipColtextBox.Visible = true;
            bttlshipRowLabel.Visible = true;
            bttlshipRowtextBox.Visible = true;
            bttlshipHorizontalcheckBox.Visible = true;
            bttlshipVerticalcheckBox.Visible = true;
            bttlshipOKbutton.Visible = true;
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
            //NameCheck(name);            
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

        private void playerGridPicBox_MouseMove(object sender, MouseEventArgs e)
        {
            label4.Text = string.Format("{0},{1}",e.Location.X,e.Location.Y);
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
            
            var image = horizontalSubmarine ? Properties.Resources.submarine : Properties.Resources.submarine_r;
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
            
            var image = horizontalDestroyer ? Properties.Resources.destroyership1 : Properties.Resources.destroyership_r;
            shipPlacement.PlacePlayerShip(playerGridPicBox, destroyerRow, destroyerCol, horizontalDestroyer, 3, image);
        }

        #endregion

        #region placeBattleship
        private void bttlshipRowtextBox_TextChanged(object sender, EventArgs e)
        {
            bttlRow = bttlshipRowLabel.Text;
        }

        private void bttlshipColtextBox_TextChanged(object sender, EventArgs e)
        {
            bttlCol = bttlshipColLabel.Text;
        }

        private void bttlshipVerticalcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (bttlshipVerticalcheckBox.Checked)
            {
                bttlshipHorizontalcheckBox.Checked = false;
                horizontalBttl = false;
            }
        }

        private void bttlshipHorizontalcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (bttlshipHorizontalcheckBox.Checked)
            {
                bttlshipVerticalcheckBox.Checked = false;
                horizontalBttl = true;
            }
        }

        private void bttlshipOKbutton_Click(object sender, EventArgs e)
        {
            if (!bttlshipHorizontalcheckBox.Checked && !bttlshipVerticalcheckBox.Checked)
            {
                MessageBox.Show("Please select ship orientation");
            }
            
            var image = horizontalBttl ? Properties.Resources.btlship1 : Properties.Resources.btlship_r;
            shipPlacement.PlacePlayerShip(playerGridPicBox, bttlRow, bttlCol, horizontalBttl, 4, image);

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
            //ShipPlacement shipPlacement = new ShipPlacement();
            //shipPlacement.StartAi();                        
            foreach(Control ctrl in groupBox3.Controls)
            {
                ctrl.Enabled = false;
            }
        }

        private void fireBttn_Click(object sender, EventArgs e)
        {            
            //GameMechanics paixnidi = new GameMechanics();            
            paixnidi.PlayerFire(AIGridPicBox,shotRow,shotCol);
            paixnidi.AIFire(playerGridPicBox);
        }

        private void shotRowTextBox_TextChanged(object sender, EventArgs e)
        {
            shotRow = shotRowTextBox.Text;
        }

        private void shotColTextBox_TextChanged(object sender, EventArgs e)
        {
            shotCol = shotColTextBox.Text;
        }



        private void startButton_Click(object sender, EventArgs e)
        {
            shipPlacement.GameStart();
        }
    }
}
