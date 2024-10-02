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
        public FormAllRecords()
        {
            InitializeComponent();

            //перенести в отдельный метод
            dataGridView1.Columns.Add("", "");

            Person person1 = new Person(1, "aaaaa", DateTime.Now);
            Person person2 = new Person(2, "bbbbb", DateTime.Now);

            //убрать присваивание 
            CustomCell person1Cell = AddNewPersonInCell(person1);
            CustomCell person2Cell = AddNewPersonInCell(person2);


            dataGridView2.Columns.Add("", "");
            dataGridView2.Rows.Add("tesst");
        }

        List<CustomCell> people = new List<CustomCell>();
        List<CustomCell> displayedCells = new List<CustomCell>();
        List<CustomCell> allCells = new List<CustomCell>();

        private CustomCell AddNewPersonInCell(Person person)
        {
            CustomCell main = new CustomCell(person._name, true, null);
            CustomCell dependentCell1 = new CustomCell(person._cardNumber.ToString(), false, main);
            CustomCell dependentCell2 = new CustomCell(person._birthday.ToString(), false, main);

            //надо для запоминания изменения информации
            allCells.Add(main);
            allCells.Add(dependentCell1);
            allCells.Add(dependentCell2);

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

        private void ButtonAddCell_Click(object sender, EventArgs e)
        {
            CustomCell cell = new CustomCell("no data", false, null);
            allCells.Add(cell);
            displayedCells.Add(cell);
            dataGridView1.Rows.Add(cell._data);
            dataGridView1.Refresh();
        }

        private void NewDependence(CustomCell cell)
        {
            if (!cell._canBeOpened)
            {
                cell._dependentCells = new List<CustomCell>();
                cell._canBeOpened = true;
            }
            
            CustomCell testOpen = new CustomCell("test testOpen", false, cell);
            allCells.Add(cell);

            //закомментированное надо было для теста вложенности, сейчас вложенность работает сама
            /*CustomCell testOpenD1 = new CustomCell("test testOpenD1", true, testOpen);
            CustomCell testOpenD2 = new CustomCell("test testOpenD2", false, testOpen);
            CustomCell testOpenD1D1 = new CustomCell("test testOpenD1D1", false, testOpen);

            testOpen._dependentCells = new List<CustomCell>
            {
                testOpenD1,
                testOpenD2
            };

            testOpenD1._dependentCells = new List<CustomCell>
            {
                testOpenD1D1
            };*/

            cell._dependentCells.Add(testOpen);
            dataGridView1.Refresh();
        }

        private void ButtonNewDependence_Click(object sender, EventArgs e)
        {
            NewDependence(displayedCells[dataGridView1.CurrentCell.RowIndex]);
            dataGridView1.Refresh();
        }

        private void DeleteDependence(int selected)
        {
            CustomCell cell = displayedCells[selected];

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

            cell._canBeOpened = false;
            cell._dependentCells = null;
            // не работает, доделать
            dataGridView1.Rows.RemoveAt(selected);
            displayedCells.RemoveAt(selected);
            //ломается т.к. у testOpen нет родитеся, хотя он в список детей у aaaaa
            //вроде починил, но подумать как избавиться от parent

            cell._parent?._dependentCells.Remove(cell);
        }

        private void ButtonDeleteDependence_Click(object sender, EventArgs e)
        {
            DeleteDependence(dataGridView1.CurrentCell.RowIndex);
            dataGridView1.Refresh();
        }

        private void DataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView1_OpenCellHandler(dataGridView1.SelectedRows[0].Index);
            dataGridView1.Refresh();
        }

        private void DataGridView1_OpenCellHandler(int selected)
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

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int selected = dataGridView1.CurrentCell.RowIndex;
            CustomCell cell = displayedCells[selected];
            int cellIndex = cell._parent._dependentCells.IndexOf(cell);
            cell._parent._dependentCells[cellIndex]._data = dataGridView1.Rows[selected].Cells[0].Value.ToString();
        }
    }
}