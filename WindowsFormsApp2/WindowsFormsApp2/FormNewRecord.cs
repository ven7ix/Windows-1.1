using System;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class FormNewRecord : Form
    {
        FormAllRecords formAllRecords;

        public FormNewRecord(FormAllRecords owner)
        {
            formAllRecords = owner;
            InitializeComponent();
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {

        }
    }
}
