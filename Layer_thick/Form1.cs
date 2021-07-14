using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Model;
using TSG = Tekla.Structures.Geometry3d;
using Tekla.Structures.Model.UI;
using System.Collections;


namespace ChangeLayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create a new Model object that represents the model you have opened in Tekla Structures.
            Model myModel = new Model();
            double layer_thick = 0;
            double layer_init_thick = 0;
            string layer_num_imput = maskedTextBox3.Text;
            string layer_num_name = "Thickness_L" + layer_num_imput;
            try
            {
                layer_thick = double.Parse(maskedTextBox1.Text);
                layer_init_thick = double.Parse(maskedTextBox2.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Error! Please enter a correct layer thickness.", "Oops!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Check if we have a Tekla Structures Model that you can connect to.
            if (myModel.GetConnectionStatus())
            {
                Tekla.Structures.Model.ModelObjectSelector Selector = myModel.GetModelObjectSelector();
                ModelObjectEnumerator Enum = Selector.GetAllObjectsWithType(ModelObject.ModelObjectEnum.COMPONENT);
                //ComponentInput ComponentInput = new ComponentInput();
                Change(Enum, layer_thick, layer_init_thick, layer_num_name);
            }
            myModel.CommitChanges();
        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
        private static void Change(ModelObjectEnumerator Enum, double thick, double init_thick, string numname)
        {
            foreach (Tekla.Structures.Model.Component obj in Enum)
            {
                if (obj.Name == "WallLayout")
                {
                    double Val = 0;
                    double newVal = thick;
                    bool tf = obj.GetAttribute(numname, ref Val);
                    if (Val == init_thick)
                    {
                        obj.SetUserProperty(numname, newVal);
                        obj.Modify();
                    }
                    // obj.SetAttribute("Cass_L2", newVal);

                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
