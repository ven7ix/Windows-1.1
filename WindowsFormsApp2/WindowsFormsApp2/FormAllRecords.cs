using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp2
{
    public partial class FormAllRecords : Form
    {
        public FormAllRecords()
        {
            InitializeComponent();
        }

        List<CustomCell> people = new List<CustomCell>();

        List<CustomCell> displayedCells = new List<CustomCell>();
        List<CustomCell> dependentCells = new List<CustomCell>();

        private void buttonNewRecord_Click(object sender, EventArgs e)
        {
            
            dataGridView1.Columns.Add("", "");

            CustomCell name = new CustomCell("test name", true, null, null);
            CustomCell card = new CustomCell("test card", false, null, name);
            CustomCell birthday = new CustomCell("test birthday", false, null, name);
            CustomCell testOpen = new CustomCell("test testOpen", true, null, name);
            CustomCell testOpenD1 = new CustomCell("test testOpenD1", false, null, testOpen);
            CustomCell testOpenD2 = new CustomCell("test testOpenD2", false, null, testOpen);

            CustomCell name2 = new CustomCell("test name 2", false, null, null);

            

            dependentCells.Add(card);
            dependentCells.Add(birthday);
            dependentCells.Add(testOpen);
            name._dependentCells = new List<CustomCell>(dependentCells);
            name._isShown = true;
            dependentCells.Clear();


            dependentCells.Add(testOpenD1);
            dependentCells.Add(testOpenD2);
            testOpen._dependentCells = new List<CustomCell>(dependentCells);
            dependentCells.Clear();

            displayedCells.Add(name);
            dataGridView1.Rows.Add(name._data);

            displayedCells.Add(name2);
            dataGridView1.Rows.Add(name2._data);

            /*people.Add(new CustomCell(new Person(11111, "dddd", DateTime.Now), true, 2));
            people.Add(new CustomCell(new Person(22222, "xxxx", DateTime.Now), true, 2));

            foreach (CustomCell p in people)
            {
                dataGridView1.Rows.Add(p._person._name);
            }*/

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1_OpenCellHandler(dataGridView1.SelectedRows[0].Index);

            dataGridView1.Refresh();
        }

        private void dataGridView1_OpenCellHandler(int selected)
        {
            CustomCell cell = displayedCells[selected];

            if (!cell._canBeOpened) return;

            if (!cell._isOpen)
            {
                foreach (CustomCell c in cell._dependentCells)
                {
                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(dataGridView1);

                    newRow.Cells[0].Value = c._data;
                    dataGridView1.Rows.Insert(selected + 1, newRow);
                    displayedCells.Insert(selected + 1, c);
                    c._isShown = true;
                }
                cell._isOpen = true;
            }
            else
            {
                CheckAllDependentCells(cell, selected);
                cell._isOpen = false;
            }
        }

        private void CheckAllDependentCells(CustomCell cell, int selected)
        {
            var queue = new Queue<CustomCell>();
            queue.Enqueue(cell);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                //body

                if (current._isShown && displayedCells.IndexOf(current) != selected)
                {
                    dataGridView1.Rows.RemoveAt(selected + 1);
                    displayedCells.RemoveAt(selected + 1);
                    current._isShown = false;
                }
                

                //end

                if (current._dependentCells == null) continue;
                foreach (CustomCell c in current._dependentCells)
                {
                    queue.Enqueue(c);
                }
            }
        }

        /*int position = dataGridView1.SelectedRows[0].Index + 1;
            int selected = dataGridView1.SelectedRows[0].Index;
            


            string str = dataGridView1.Rows[selected].Cells[0].Value.ToString();
            if (str.Split(' ')[0] == "")
            {
                return;
            }

            if (!people[selected]._isOpen)
            {
                DataGridViewRow newRow1 = new DataGridViewRow();
                newRow1.CreateCells(dataGridView1);

                newRow1.Cells[0].Value = " :. ";

                DataGridViewRow newRow2 = new DataGridViewRow();
                newRow2.CreateCells(dataGridView1);
                newRow2.Cells[0].Value = " :. ";

                dataGridView1.Rows.Insert(position, newRow1);
                dataGridView1.Rows.Insert(position + 1, newRow2);

                people[selected]._isOpen = true;
            }
            else
            {

                for (int i = 0; i < 2; i++)
                {
                    dataGridView1.Rows.RemoveAt(position);
                }
                dataGridView1.Refresh();
                people[selected]._isOpen = false;
            }*/
    }
}