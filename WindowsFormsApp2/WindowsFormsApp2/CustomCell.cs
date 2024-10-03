using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    internal class CustomCell
    {
        public string _data { get; set; }
        public bool _isOpen { get; set; }
        public List<CustomCell> _dependentCells { get; set; }
        public CustomCell _parent { get; set; }
        public bool _isShown { get; set; }

        //я не знаю правильно ли это но оно работает
        private static List<CustomCell> displayedCells = new List<CustomCell>();

        public CustomCell(string data, CustomCell parent)
        {
            _data = data;
            _isOpen = false;
            _dependentCells = new List<CustomCell>();
            _parent = parent;
            _isShown = false;
        }

        public static void AddNewCellOnTop(DataGridView dataGridView, string data)
        {
            CustomCell cell = new CustomCell(data, null);
            displayedCells.Add(cell);
            dataGridView.Rows.Add(cell._data);
            cell._isShown = true;
        }

        public static void NewDependence(int selected, string data)
        {
            CustomCell cell = displayedCells[selected];
            CustomCell newCell = new CustomCell(data, cell);
            cell._dependentCells.Add(newCell);
        }

        public static void NewDependence(string data)
        {
            CustomCell cell = displayedCells[displayedCells.Count - 1];
            CustomCell newCell = new CustomCell(data, cell);
            cell._dependentCells.Add(newCell);
        }

        public static void DeleteDependence(int selected, DataGridView dataGridView)
        {
            CustomCell cell = displayedCells[selected];

            Queue<CustomCell> queue = new Queue<CustomCell>();
            queue.Enqueue(cell);

            while (queue.Count > 0)
            {
                CustomCell current = queue.Dequeue();

                //body
                if (current._isShown && displayedCells.IndexOf(current) != selected)
                {
                    dataGridView.Rows.RemoveAt(selected + 1);
                    displayedCells.RemoveAt(selected + 1);
                    current._isShown = false;
                }
                //end

                if (current._dependentCells == null) continue;
                foreach (CustomCell c in current._dependentCells) queue.Enqueue(c);
            }

            dataGridView.Rows.RemoveAt(selected);
            displayedCells.RemoveAt(selected);
            cell._parent?._dependentCells.Remove(cell);
        }

        public static void OpenCell(int selected, DataGridView dataGridView)
        {
            CustomCell cell = displayedCells[selected];

            if (cell._dependentCells.Count == 0) return;

            //открытие и закрытие ячейки (попеременно)
            if (!cell._isOpen)
            {
                foreach (CustomCell c in cell._dependentCells)
                {
                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(dataGridView);

                    //для показа на каком уровне находится ячейка, наверное можно сделать лучше
                    int countParents = 0;
                    CustomCell parents = c;
                    while (parents._parent != null)
                    {
                        parents = parents._parent;
                        countParents++;
                    }
                    for (int i = 0; i < countParents; i++)
                    {
                        //не работает \t
                        newRow.Cells[0].Value += "    ";
                    }
                    //end

                    newRow.Cells[0].Value += c._data;
                    dataGridView.Rows.Insert(selected + 1, newRow);
                    displayedCells.Insert(selected + 1, c);
                    c._isShown = true;
                }
                cell._isOpen = true;
            }
            else
            {
                //обход в ширину
                Queue<CustomCell> queue = new Queue<CustomCell>();
                queue.Enqueue(cell);
                while (queue.Count > 0)
                {
                    CustomCell current = queue.Dequeue();

                    //закрытие ячейки
                    if (current._isShown && displayedCells.IndexOf(current) != selected)
                    {
                        dataGridView.Rows.RemoveAt(selected + 1);
                        displayedCells.RemoveAt(selected + 1);
                        current._isShown = false;
                    }
                    //end

                    if (current._dependentCells == null) continue;
                    foreach (CustomCell c in current._dependentCells) queue.Enqueue(c);
                }
                cell._isOpen = false;
            }
        }

        public static void SaveCellData(int selected, DataGridView dataGridView)
        {
            CustomCell cell = displayedCells[selected];

            if (cell._parent != null)
            {
                int cellIndex = cell._parent._dependentCells.IndexOf(cell);
                cell._parent._dependentCells[cellIndex]._data = dataGridView.Rows[selected].Cells[0].Value.ToString();
            }
            else
            {
                cell._data = dataGridView.Rows[selected].Cells[0].Value.ToString();
            }
        }
    }
}
