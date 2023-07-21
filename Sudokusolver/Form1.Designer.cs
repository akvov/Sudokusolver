
namespace Sudokusolver
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SudokuGrid = new System.Windows.Forms.DataGridView();
            this.col1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestButton = new System.Windows.Forms.Button();
            this.CellBox = new System.Windows.Forms.GroupBox();
            this.EraseNumber = new System.Windows.Forms.Button();
            this.WriteNumber = new System.Windows.Forms.Button();
            this.EraseCellVariants = new System.Windows.Forms.Button();
            this.WriteCellVariant = new System.Windows.Forms.Button();
            this.cell_label = new System.Windows.Forms.Label();
            this.ColumnBox = new System.Windows.Forms.GroupBox();
            this.AlterCol = new System.Windows.Forms.Button();
            this.LookCol = new System.Windows.Forms.Button();
            this.column_label = new System.Windows.Forms.Label();
            this.RowBox = new System.Windows.Forms.GroupBox();
            this.AlterRow = new System.Windows.Forms.Button();
            this.LookRow = new System.Windows.Forms.Button();
            this.row_label = new System.Windows.Forms.Label();
            this.SquareBox = new System.Windows.Forms.GroupBox();
            this.AlterSqre = new System.Windows.Forms.Button();
            this.LookSqre = new System.Windows.Forms.Button();
            this.square_label = new System.Windows.Forms.Label();
            this.ezsolve = new System.Windows.Forms.Button();
            this.restartbutton = new System.Windows.Forms.Button();
            this.HelpBox = new System.Windows.Forms.GroupBox();
            this.HardSolveButton = new System.Windows.Forms.Button();
            this.SolveButton = new System.Windows.Forms.Button();
            this.AutoVariants = new System.Windows.Forms.CheckBox();
            this.NumBox = new System.Windows.Forms.GroupBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.readybutton = new System.Windows.Forms.Button();
            this.resetbutton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SudokuGrid)).BeginInit();
            this.CellBox.SuspendLayout();
            this.ColumnBox.SuspendLayout();
            this.RowBox.SuspendLayout();
            this.SquareBox.SuspendLayout();
            this.HelpBox.SuspendLayout();
            this.NumBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SudokuGrid);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(445, 425);
            this.panel1.TabIndex = 0;
            // 
            // SudokuGrid
            // 
            this.SudokuGrid.AllowUserToAddRows = false;
            this.SudokuGrid.AllowUserToDeleteRows = false;
            this.SudokuGrid.AllowUserToResizeColumns = false;
            this.SudokuGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = " ";
            this.SudokuGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.SudokuGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SudokuGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.SudokuGrid.ColumnHeadersHeight = 45;
            this.SudokuGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.SudokuGrid.ColumnHeadersVisible = false;
            this.SudokuGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col1,
            this.col2,
            this.col3,
            this.col4,
            this.col5,
            this.col6,
            this.col7,
            this.col8,
            this.col9});
            this.SudokuGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.SudokuGrid.Location = new System.Drawing.Point(0, 0);
            this.SudokuGrid.MaximumSize = new System.Drawing.Size(405, 405);
            this.SudokuGrid.MinimumSize = new System.Drawing.Size(405, 405);
            this.SudokuGrid.MultiSelect = false;
            this.SudokuGrid.Name = "SudokuGrid";
            this.SudokuGrid.RowHeadersVisible = false;
            this.SudokuGrid.RowHeadersWidth = 45;
            this.SudokuGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.SudokuGrid.RowTemplate.Height = 45;
            this.SudokuGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SudokuGrid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.SudokuGrid.ShowEditingIcon = false;
            this.SudokuGrid.Size = new System.Drawing.Size(405, 405);
            this.SudokuGrid.TabIndex = 0;
            this.SudokuGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SudokuGrid_CellClick);
            this.SudokuGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.SudokuGrid_Paint);
            // 
            // col1
            // 
            this.col1.HeaderText = "";
            this.col1.Name = "col1";
            this.col1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // col2
            // 
            this.col2.HeaderText = "";
            this.col2.Name = "col2";
            // 
            // col3
            // 
            this.col3.HeaderText = "";
            this.col3.Name = "col3";
            // 
            // col4
            // 
            this.col4.HeaderText = "";
            this.col4.Name = "col4";
            // 
            // col5
            // 
            this.col5.HeaderText = "";
            this.col5.Name = "col5";
            // 
            // col6
            // 
            this.col6.HeaderText = "";
            this.col6.Name = "col6";
            // 
            // col7
            // 
            this.col7.HeaderText = "";
            this.col7.Name = "col7";
            // 
            // col8
            // 
            this.col8.HeaderText = "";
            this.col8.Name = "col8";
            // 
            // col9
            // 
            this.col9.HeaderText = "";
            this.col9.Name = "col9";
            // 
            // TestButton
            // 
            this.TestButton.Location = new System.Drawing.Point(13, 12);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(120, 23);
            this.TestButton.TabIndex = 1;
            this.TestButton.Text = "тест";
            this.TestButton.UseVisualStyleBackColor = true;
            this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // CellBox
            // 
            this.CellBox.Controls.Add(this.EraseNumber);
            this.CellBox.Controls.Add(this.WriteNumber);
            this.CellBox.Controls.Add(this.EraseCellVariants);
            this.CellBox.Controls.Add(this.WriteCellVariant);
            this.CellBox.Controls.Add(this.cell_label);
            this.CellBox.Location = new System.Drawing.Point(464, 82);
            this.CellBox.Name = "CellBox";
            this.CellBox.Size = new System.Drawing.Size(308, 98);
            this.CellBox.TabIndex = 2;
            this.CellBox.TabStop = false;
            this.CellBox.Text = "клетка";
            // 
            // EraseNumber
            // 
            this.EraseNumber.Location = new System.Drawing.Point(166, 52);
            this.EraseNumber.Name = "EraseNumber";
            this.EraseNumber.Size = new System.Drawing.Size(65, 40);
            this.EraseNumber.TabIndex = 10;
            this.EraseNumber.Text = "Стереть цифру";
            this.EraseNumber.UseVisualStyleBackColor = true;
            this.EraseNumber.Click += new System.EventHandler(this.EraseNumber_Click);
            // 
            // WriteNumber
            // 
            this.WriteNumber.Location = new System.Drawing.Point(166, 9);
            this.WriteNumber.Name = "WriteNumber";
            this.WriteNumber.Size = new System.Drawing.Size(65, 40);
            this.WriteNumber.TabIndex = 9;
            this.WriteNumber.Text = "Вписать цифру";
            this.WriteNumber.UseVisualStyleBackColor = true;
            this.WriteNumber.Click += new System.EventHandler(this.WriteNumber_Click);
            // 
            // EraseCellVariants
            // 
            this.EraseCellVariants.Location = new System.Drawing.Point(237, 52);
            this.EraseCellVariants.Name = "EraseCellVariants";
            this.EraseCellVariants.Size = new System.Drawing.Size(65, 40);
            this.EraseCellVariants.TabIndex = 8;
            this.EraseCellVariants.Text = "Стереть вариант";
            this.EraseCellVariants.UseVisualStyleBackColor = true;
            this.EraseCellVariants.Click += new System.EventHandler(this.EraseCellVariants_Click);
            // 
            // WriteCellVariant
            // 
            this.WriteCellVariant.Location = new System.Drawing.Point(237, 9);
            this.WriteCellVariant.Name = "WriteCellVariant";
            this.WriteCellVariant.Size = new System.Drawing.Size(65, 40);
            this.WriteCellVariant.TabIndex = 7;
            this.WriteCellVariant.Text = "Вписать вариант";
            this.WriteCellVariant.UseVisualStyleBackColor = true;
            this.WriteCellVariant.Click += new System.EventHandler(this.WriteCellVariant_Click);
            // 
            // cell_label
            // 
            this.cell_label.AutoSize = true;
            this.cell_label.Location = new System.Drawing.Point(10, 19);
            this.cell_label.Name = "cell_label";
            this.cell_label.Size = new System.Drawing.Size(13, 13);
            this.cell_label.TabIndex = 6;
            this.cell_label.Text = "_";
            // 
            // ColumnBox
            // 
            this.ColumnBox.Controls.Add(this.AlterCol);
            this.ColumnBox.Controls.Add(this.LookCol);
            this.ColumnBox.Controls.Add(this.column_label);
            this.ColumnBox.Location = new System.Drawing.Point(464, 186);
            this.ColumnBox.Name = "ColumnBox";
            this.ColumnBox.Size = new System.Drawing.Size(308, 80);
            this.ColumnBox.TabIndex = 3;
            this.ColumnBox.TabStop = false;
            this.ColumnBox.Text = "столб";
            // 
            // AlterCol
            // 
            this.AlterCol.Location = new System.Drawing.Point(215, 46);
            this.AlterCol.Name = "AlterCol";
            this.AlterCol.Size = new System.Drawing.Size(87, 34);
            this.AlterCol.TabIndex = 9;
            this.AlterCol.Text = "Искать чередования";
            this.AlterCol.UseVisualStyleBackColor = true;
            this.AlterCol.Click += new System.EventHandler(this.AlterCol_Click);
            // 
            // LookCol
            // 
            this.LookCol.Location = new System.Drawing.Point(215, 9);
            this.LookCol.Name = "LookCol";
            this.LookCol.Size = new System.Drawing.Size(87, 34);
            this.LookCol.TabIndex = 8;
            this.LookCol.Text = "Искать место для цифры";
            this.LookCol.UseVisualStyleBackColor = true;
            this.LookCol.Click += new System.EventHandler(this.LookCol_Click);
            // 
            // column_label
            // 
            this.column_label.AutoSize = true;
            this.column_label.Location = new System.Drawing.Point(10, 20);
            this.column_label.Name = "column_label";
            this.column_label.Size = new System.Drawing.Size(13, 13);
            this.column_label.TabIndex = 7;
            this.column_label.Text = "_";
            // 
            // RowBox
            // 
            this.RowBox.Controls.Add(this.AlterRow);
            this.RowBox.Controls.Add(this.LookRow);
            this.RowBox.Controls.Add(this.row_label);
            this.RowBox.Location = new System.Drawing.Point(464, 272);
            this.RowBox.Name = "RowBox";
            this.RowBox.Size = new System.Drawing.Size(308, 80);
            this.RowBox.TabIndex = 4;
            this.RowBox.TabStop = false;
            this.RowBox.Text = "ряд";
            // 
            // AlterRow
            // 
            this.AlterRow.Location = new System.Drawing.Point(215, 46);
            this.AlterRow.Name = "AlterRow";
            this.AlterRow.Size = new System.Drawing.Size(87, 34);
            this.AlterRow.TabIndex = 10;
            this.AlterRow.Text = "Искать чередования";
            this.AlterRow.UseVisualStyleBackColor = true;
            this.AlterRow.Click += new System.EventHandler(this.AlterRow_Click);
            // 
            // LookRow
            // 
            this.LookRow.Location = new System.Drawing.Point(215, 9);
            this.LookRow.Name = "LookRow";
            this.LookRow.Size = new System.Drawing.Size(87, 34);
            this.LookRow.TabIndex = 9;
            this.LookRow.Text = "Искать место для цифры";
            this.LookRow.UseVisualStyleBackColor = true;
            this.LookRow.Click += new System.EventHandler(this.LookRow_Click);
            // 
            // row_label
            // 
            this.row_label.AutoSize = true;
            this.row_label.Location = new System.Drawing.Point(10, 19);
            this.row_label.Name = "row_label";
            this.row_label.Size = new System.Drawing.Size(13, 13);
            this.row_label.TabIndex = 8;
            this.row_label.Text = "_";
            // 
            // SquareBox
            // 
            this.SquareBox.Controls.Add(this.AlterSqre);
            this.SquareBox.Controls.Add(this.LookSqre);
            this.SquareBox.Controls.Add(this.square_label);
            this.SquareBox.Location = new System.Drawing.Point(464, 358);
            this.SquareBox.Name = "SquareBox";
            this.SquareBox.Size = new System.Drawing.Size(308, 80);
            this.SquareBox.TabIndex = 4;
            this.SquareBox.TabStop = false;
            this.SquareBox.Text = "квадрат";
            // 
            // AlterSqre
            // 
            this.AlterSqre.Location = new System.Drawing.Point(215, 42);
            this.AlterSqre.Name = "AlterSqre";
            this.AlterSqre.Size = new System.Drawing.Size(87, 34);
            this.AlterSqre.TabIndex = 12;
            this.AlterSqre.Text = "Искать чередования";
            this.AlterSqre.UseVisualStyleBackColor = true;
            this.AlterSqre.Click += new System.EventHandler(this.AlterSqre_Click);
            // 
            // LookSqre
            // 
            this.LookSqre.Location = new System.Drawing.Point(215, 6);
            this.LookSqre.Name = "LookSqre";
            this.LookSqre.Size = new System.Drawing.Size(87, 34);
            this.LookSqre.TabIndex = 11;
            this.LookSqre.Text = "Искать место для цифры";
            this.LookSqre.UseVisualStyleBackColor = true;
            this.LookSqre.Click += new System.EventHandler(this.LookSqre_Click);
            // 
            // square_label
            // 
            this.square_label.AutoSize = true;
            this.square_label.Location = new System.Drawing.Point(10, 16);
            this.square_label.Name = "square_label";
            this.square_label.Size = new System.Drawing.Size(13, 13);
            this.square_label.TabIndex = 9;
            this.square_label.Text = "_";
            // 
            // ezsolve
            // 
            this.ezsolve.Location = new System.Drawing.Point(179, 12);
            this.ezsolve.Name = "ezsolve";
            this.ezsolve.Size = new System.Drawing.Size(75, 76);
            this.ezsolve.TabIndex = 5;
            this.ezsolve.Text = "господи пусть все само разрулится";
            this.ezsolve.UseVisualStyleBackColor = true;
            this.ezsolve.Click += new System.EventHandler(this.ezsolve_Click);
            // 
            // restartbutton
            // 
            this.restartbutton.Location = new System.Drawing.Point(13, 41);
            this.restartbutton.Name = "restartbutton";
            this.restartbutton.Size = new System.Drawing.Size(120, 23);
            this.restartbutton.TabIndex = 6;
            this.restartbutton.Text = "начать заново";
            this.restartbutton.UseVisualStyleBackColor = true;
            this.restartbutton.Click += new System.EventHandler(this.restartbutton_Click);
            // 
            // HelpBox
            // 
            this.HelpBox.Controls.Add(this.HardSolveButton);
            this.HelpBox.Controls.Add(this.SolveButton);
            this.HelpBox.Controls.Add(this.AutoVariants);
            this.HelpBox.Controls.Add(this.ezsolve);
            this.HelpBox.Location = new System.Drawing.Point(778, 330);
            this.HelpBox.Name = "HelpBox";
            this.HelpBox.Size = new System.Drawing.Size(267, 108);
            this.HelpBox.TabIndex = 7;
            this.HelpBox.TabStop = false;
            this.HelpBox.Text = "Авторешение";
            // 
            // HardSolveButton
            // 
            this.HardSolveButton.Location = new System.Drawing.Point(87, 45);
            this.HardSolveButton.Name = "HardSolveButton";
            this.HardSolveButton.Size = new System.Drawing.Size(75, 43);
            this.HardSolveButton.TabIndex = 8;
            this.HardSolveButton.Text = "1-й уровень перебора";
            this.HardSolveButton.UseVisualStyleBackColor = true;
            this.HardSolveButton.Click += new System.EventHandler(this.HardSolveButton_Click);
            // 
            // SolveButton
            // 
            this.SolveButton.Location = new System.Drawing.Point(6, 45);
            this.SolveButton.Name = "SolveButton";
            this.SolveButton.Size = new System.Drawing.Size(75, 43);
            this.SolveButton.TabIndex = 7;
            this.SolveButton.Text = "0-й уровень перебора";
            this.SolveButton.UseVisualStyleBackColor = true;
            this.SolveButton.Click += new System.EventHandler(this.SolveButton_Click);
            // 
            // AutoVariants
            // 
            this.AutoVariants.AutoSize = true;
            this.AutoVariants.Location = new System.Drawing.Point(6, 19);
            this.AutoVariants.Name = "AutoVariants";
            this.AutoVariants.Size = new System.Drawing.Size(162, 17);
            this.AutoVariants.TabIndex = 6;
            this.AutoVariants.Text = "Автоматические варианты";
            this.AutoVariants.UseVisualStyleBackColor = true;
            this.AutoVariants.CheckedChanged += new System.EventHandler(this.AutoVariants_CheckedChanged);
            // 
            // NumBox
            // 
            this.NumBox.Controls.Add(this.Cancel);
            this.NumBox.Location = new System.Drawing.Point(778, 13);
            this.NumBox.Name = "NumBox";
            this.NumBox.Size = new System.Drawing.Size(254, 301);
            this.NumBox.TabIndex = 8;
            this.NumBox.TabStop = false;
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(6, 265);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(242, 30);
            this.Cancel.TabIndex = 0;
            this.Cancel.Text = "отмена";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.readybutton);
            this.groupBox1.Controls.Add(this.resetbutton);
            this.groupBox1.Controls.Add(this.restartbutton);
            this.groupBox1.Controls.Add(this.TestButton);
            this.groupBox1.Location = new System.Drawing.Point(464, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 73);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // readybutton
            // 
            this.readybutton.Location = new System.Drawing.Point(156, 41);
            this.readybutton.Name = "readybutton";
            this.readybutton.Size = new System.Drawing.Size(120, 23);
            this.readybutton.TabIndex = 8;
            this.readybutton.Text = "готово";
            this.readybutton.UseVisualStyleBackColor = true;
            this.readybutton.Click += new System.EventHandler(this.readybutton_Click);
            // 
            // resetbutton
            // 
            this.resetbutton.Location = new System.Drawing.Point(156, 12);
            this.resetbutton.Name = "resetbutton";
            this.resetbutton.Size = new System.Drawing.Size(120, 23);
            this.resetbutton.TabIndex = 7;
            this.resetbutton.Text = "новый";
            this.resetbutton.UseVisualStyleBackColor = true;
            this.resetbutton.Click += new System.EventHandler(this.resetbutton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.NumBox);
            this.Controls.Add(this.HelpBox);
            this.Controls.Add(this.SquareBox);
            this.Controls.Add(this.RowBox);
            this.Controls.Add(this.ColumnBox);
            this.Controls.Add(this.CellBox);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SudokuGrid)).EndInit();
            this.CellBox.ResumeLayout(false);
            this.CellBox.PerformLayout();
            this.ColumnBox.ResumeLayout(false);
            this.ColumnBox.PerformLayout();
            this.RowBox.ResumeLayout(false);
            this.RowBox.PerformLayout();
            this.SquareBox.ResumeLayout(false);
            this.SquareBox.PerformLayout();
            this.HelpBox.ResumeLayout(false);
            this.HelpBox.PerformLayout();
            this.NumBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView SudokuGrid;
        private System.Windows.Forms.Button TestButton;
        private System.Windows.Forms.GroupBox CellBox;
        private System.Windows.Forms.GroupBox ColumnBox;
        private System.Windows.Forms.GroupBox RowBox;
        private System.Windows.Forms.GroupBox SquareBox;
        private System.Windows.Forms.Label cell_label;
        private System.Windows.Forms.Label column_label;
        private System.Windows.Forms.Label row_label;
        private System.Windows.Forms.Label square_label;
        private System.Windows.Forms.DataGridViewTextBoxColumn col1;
        private System.Windows.Forms.DataGridViewTextBoxColumn col2;
        private System.Windows.Forms.DataGridViewTextBoxColumn col3;
        private System.Windows.Forms.DataGridViewTextBoxColumn col4;
        private System.Windows.Forms.DataGridViewTextBoxColumn col5;
        private System.Windows.Forms.DataGridViewTextBoxColumn col6;
        private System.Windows.Forms.DataGridViewTextBoxColumn col7;
        private System.Windows.Forms.DataGridViewTextBoxColumn col8;
        private System.Windows.Forms.DataGridViewTextBoxColumn col9;
        private System.Windows.Forms.Button ezsolve;
        private System.Windows.Forms.Button restartbutton;
        private System.Windows.Forms.GroupBox HelpBox;
        private System.Windows.Forms.Button EraseCellVariants;
        private System.Windows.Forms.Button WriteCellVariant;
        private System.Windows.Forms.Button AlterCol;
        private System.Windows.Forms.Button LookCol;
        private System.Windows.Forms.Button AlterRow;
        private System.Windows.Forms.Button LookRow;
        private System.Windows.Forms.Button AlterSqre;
        private System.Windows.Forms.Button LookSqre;
        private System.Windows.Forms.Button EraseNumber;
        private System.Windows.Forms.Button WriteNumber;
        private System.Windows.Forms.GroupBox NumBox;
        private System.Windows.Forms.CheckBox AutoVariants;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button resetbutton;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button readybutton;
        private System.Windows.Forms.Button SolveButton;
        private System.Windows.Forms.Button HardSolveButton;
    }
}

