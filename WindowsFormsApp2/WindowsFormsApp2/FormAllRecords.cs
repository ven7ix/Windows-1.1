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

        private void buttonNewRecord_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("", "");

            Person person1 = new Person(1, "aaaaa", DateTime.Now);
            Person person2 = new Person(2, "bbbbb", DateTime.Now);

            CustomCell person1Cell = addNewPersonInCell(person1);
            CustomCell person2Cell = addNewPersonInCell(person2);

            

            CustomCell testOpen = new CustomCell("test testOpen", true, null);
            CustomCell testOpenD1 = new CustomCell("test testOpenD1", true, testOpen);
            CustomCell testOpenD2 = new CustomCell("test testOpenD2", false, testOpen);
            CustomCell testOpenD1D1 = new CustomCell("test testOpenD1D1", false, testOpen);

            person1Cell._dependentCells.Add(testOpen);

            testOpen._dependentCells = new List<CustomCell>
            {
                testOpenD1,
                testOpenD2
            };

            testOpenD1._dependentCells = new List<CustomCell>
            {
                testOpenD1D1
            };
        }

        private CustomCell addNewPersonInCell(Person person)
        {
            CustomCell main = new CustomCell(person._name, true, null);
            CustomCell dependentCell1 = new CustomCell(person._cardNumber.ToString(), false, main);
            CustomCell dependentCell2 = new CustomCell(person._birthday.ToString(), false, main);

            main._dependentCells = new List<CustomCell>
            {
                dependentCell1,
                dependentCell2
            };

            displayedCells.Add(main);
            dataGridView1.Rows.Add(main._data);
            main._isShown = true;

            return main;
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