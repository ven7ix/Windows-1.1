namespace WindowsFormsApp2
{
    partial class FormAllRecords
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonNewRecord = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonNewDependence = new System.Windows.Forms.Button();
            this.buttonDeleteDependence = new System.Windows.Forms.Button();
            this.buttonAddCell = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonNewRecord
            // 
            this.buttonNewRecord.Location = new System.Drawing.Point(87, 286);
            this.buttonNewRecord.Name = "buttonNewRecord";
            this.buttonNewRecord.Size = new System.Drawing.Size(242, 115);
            this.buttonNewRecord.TabIndex = 0;
            this.buttonNewRecord.Text = "add";
            this.buttonNewRecord.UseVisualStyleBackColor = true;
            this.buttonNewRecord.Click += new System.EventHandler(this.buttonNewRecord_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(89, 60);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(240, 220);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellEndEdit);
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView1_RowHeaderMouseClick);
            // 
            // buttonNewDependence
            // 
            this.buttonNewDependence.Location = new System.Drawing.Point(335, 286);
            this.buttonNewDependence.Name = "buttonNewDependence";
            this.buttonNewDependence.Size = new System.Drawing.Size(242, 115);
            this.buttonNewDependence.TabIndex = 2;
            this.buttonNewDependence.Text = "add new cell inside";
            this.buttonNewDependence.UseVisualStyleBackColor = true;
            this.buttonNewDependence.Click += new System.EventHandler(this.ButtonNewDependence_Click);
            // 
            // buttonDeleteDependence
            // 
            this.buttonDeleteDependence.Location = new System.Drawing.Point(583, 286);
            this.buttonDeleteDependence.Name = "buttonDeleteDependence";
            this.buttonDeleteDependence.Size = new System.Drawing.Size(150, 115);
            this.buttonDeleteDependence.TabIndex = 3;
            this.buttonDeleteDependence.Text = "delete dependence";
            this.buttonDeleteDependence.UseVisualStyleBackColor = true;
            this.buttonDeleteDependence.Click += new System.EventHandler(this.ButtonDeleteDependence_Click);
            // 
            // buttonAddCell
            // 
            this.buttonAddCell.Location = new System.Drawing.Point(335, 60);
            this.buttonAddCell.Name = "buttonAddCell";
            this.buttonAddCell.Size = new System.Drawing.Size(242, 115);
            this.buttonAddCell.TabIndex = 4;
            this.buttonAddCell.Text = "add cell into main";
            this.buttonAddCell.UseVisualStyleBackColor = true;
            this.buttonAddCell.Click += new System.EventHandler(this.ButtonAddCell_Click);
            // 
            // FormAllRecords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonAddCell);
            this.Controls.Add(this.buttonDeleteDependence);
            this.Controls.Add(this.buttonNewDependence);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonNewRecord);
            this.Name = "FormAllRecords";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonNewRecord;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonNewDependence;
        private System.Windows.Forms.Button buttonDeleteDependence;
        private System.Windows.Forms.Button buttonAddCell;
    }
}

