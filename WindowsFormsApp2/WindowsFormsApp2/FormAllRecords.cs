using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp2
{
    public partial class FormAllRecords : Form
    {
        List<Person> people = new List<Person>();

        public FormAllRecords()
        {
            InitializeComponent();

            //создание начальной таблицы
            dataGridView1.Columns.Add("1", "");
            //откллючение сортировки столбца
            dataGridView1.Columns["1"].SortMode = DataGridViewColumnSortMode.NotSortable;

            Person person1 = new Person(11111, "aaaaa", DateTime.Now);
            people.Add(person1);
            AddNewPersonInCell(person1);

            Person person2 = new Person(22222, "bbbbb", DateTime.Now);
            people.Add(person2);
            AddNewPersonInCell(person2);
        }

        private void buttonNewRecord_Click(object sender, EventArgs e)
        {
            //чисто для теста
            Person person = new Person(66666, "hhhhh", DateTime.Now);
            people.Add(person);
            AddNewPersonInCell(person);
        }

        private void AddNewPersonInCell(Person person)
        {
            CustomCell.AddNewCellOnTop(dataGridView1, person._name);
            CustomCell.NewDependence(person._cardNumber.ToString());
            CustomCell.NewDependence(person._birthday.ToString());
        }

        private void ButtonAddCell_Click(object sender, EventArgs e)
        {
            CustomCell.AddNewCellOnTop(dataGridView1, "nothig");
            dataGridView1.Refresh();
        }

        private void ButtonNewDependence_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null) return;
            CustomCell.NewDependence(dataGridView1.CurrentCell.RowIndex, "nothig");
            //не работает рефреш
            dataGridView1.Refresh();
        }

        private void ButtonDeleteDependence_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null) return;
            CustomCell.DeleteDependence(dataGridView1.CurrentCell.RowIndex, dataGridView1);
            dataGridView1.Refresh();
        }

        private void DataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            CustomCell.OpenCell(dataGridView1.CurrentCell.RowIndex, dataGridView1);
            dataGridView1.Refresh();
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CustomCell.SaveCellData(dataGridView1.CurrentCell.RowIndex, dataGridView1);
        }
    }
}