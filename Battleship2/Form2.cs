using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship2
{
    public partial class Form2 : Form
    {
        bool horizontalSubmarine; // T/F analoga 
        string submarineRow; // 1-10
        string submarineCol; // a-j

        // μεταβλητες για επιλογη θεσης
        int mouseCellX = -1;
        int mouseCellY = -1;

        // παρων επιλεγμενο πλοιο
        int currentShip;

        // μεταβλητη για περιστροφη του πλοιου
        bool shipRotation;

        // πινακας για ποια πλοια εχουν παραταχθει
        bool[] shipDeployed = new bool[4];

        string Name = null;



        public Form2()
        {
            InitializeComponent();
            MaximizeBox = false;
            CenterToScreen();
            ShipPlacement shipPlacement = new ShipPlacement();
            shipPlacement.GameStart();

            
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void BoardPictureBoxClick(object sender, EventArgs e)
        {

        }

        private void BoardPicBoxMouseMove(object sender, MouseEventArgs e)
        {

        }

        private void BoardPicBoxPaint(object sender, PaintEventArgs e)
        {

        }
        #region buttons
        private void submarineButton_Click(object sender, EventArgs e)
        {
            if (NameCheck(Name))
            {
                submarineColLabel.Visible = true;
                submarineRowLabel.Visible = true;
                submarineRowtextBox.Visible = true;
                submarineColtextBox.Visible = true;
                submarineHorizontalcheckBox.Visible = true;
                submarineVerticalcheckBox.Visible = true;
                submarineOKbutton.Visible = true;
            }
            else
            {
                MessageBox.Show("Please enter a valid name");
            }
        }

        private void destroyerButton_Click(object sender, EventArgs e)
        {
            if (NameCheck(Name))
            {
                destroyerColLabel.Visible = true;
                destroyerColtextBox.Visible = true;
                destroyerRowLabel.Visible = true;
                destroyerRowtextBox.Visible = true;
                destroyerHorizontalcheckBox.Visible = true;
                destroyerVerticalcheckBox.Visible = true;
                destroyerOKbutton.Visible = true;
            }
            else
            {
                MessageBox.Show("Please enter a valid name");
            }
            
        }

        private void bttlshipButton_Click(object sender, EventArgs e)
        {
            if (NameCheck(Name))
            {
                bttlshipColLabel.Visible = true;
                bttlshipColtextBox.Visible = true;
                bttlshipRowLabel.Visible = true;
                bttlshipRowtextBox.Visible = true;
                bttlshipHorizontalcheckBox.Visible = true;
                bttlshipVerticalcheckBox.Visible = true;
                bttlshipOKbutton.Visible = true;
            }
            else
            {
                MessageBox.Show("Please enter a valid name");
            }
            
        }

        private void carrierButton_Click(object sender, EventArgs e)
        {
            
            if(NameCheck(Name))
            {                
                carrierColLabel.Visible = true;
                carrierColtextBox.Visible = true;
                carrierRowLabel.Visible = true;
                carrierRowtextBox.Visible = true;
                carrierHorizontalcheckBox.Visible = true;
                carrierVerticalcheckBox.Visible = true;
                carrierOKbutton.Visible = true;
            }else
            {
                MessageBox.Show("Please enter a valid name");
            }
                        
        }
        #endregion
        
        #region PLAYER
        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            Name = nameTextBox.Text;
            NameCheck(Name);            
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

        #region submarineControls
        private void submarineRowtextBox_TextChanged(object sender, EventArgs e)
        {
            submarineRow = submarineRowtextBox.Text;
            
        }

        private void submarineColtextBox_TextChanged(object sender, EventArgs e)
        {
            submarineCol = submarineColtextBox.Text;
            
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
            ShipPlacement shipPlacement = new ShipPlacement();
            shipPlacement.CreateSubmarine(playerGridPicBox, submarineRow, submarineCol, horizontalSubmarine);

        }
        #endregion





        private void battleButton_Click(object sender, EventArgs e)
        {

        }

        private void fireBttn_Click(object sender, EventArgs e)
        {

        }
    }
}
