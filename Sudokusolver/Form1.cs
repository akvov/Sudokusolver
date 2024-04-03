using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sudokusolver
{
    enum Moves {nothing, writenum, writevar, erasevar, lookforplace};
    //структура для хранения контекста - какие галочки включены, какая клетка выделена, какое действие ожидается
    //сейчас так лежит, решил объединить в структуру. осталось переписать обращения
    struct context 
    {
        Moves WaitingFor;
        bool ready;
        bool ShowVariants;
        Construct waiting;
        Cell curcell;
        Column curcol;
        int colnum;
        Row currow;
        int rownum;
        Square cursqre;
        int sqrex, sqrey;
    }
    public partial class Form1 : Form
    {
        Sudoku sudoku; //решаемое судоку
        Sudoku startsudoku; //изначальная копия судоку для перезапуска
        Label[,] vars; //надписи для отображения вариантов
        SortedSet<byte>[,] uservars; //матрица сетов для хранения пользовательских вариантов
        Label[] colvars, rowvars; //надписи для отображения оставшихся цифр в столбцах и рядах
        Button[] numbuttons; //массив кнопок для цифр
        
        //множество действий для цифр - ничего/написать цифру/написать вариант/стереть вариант/поиск цифры в конструкте
        
        //перенести в контекст, переписать все обращения
        /**/
        Moves WaitingFor; //текущее ожидаемое действие.
        ////waitingfor == writenum => нажатие цифры приведет к написанию нажатой цифры в выделенную клетку

        bool ready; // заполнено ли
        bool ShowVariants; // включены ли автоварианты
        
        Construct waiting; // то что ждет взаимодействия
        // пример - выделили клетку, нажали "искать место для цифры" в столбце
        //пока цифра не выбрана, столбец будет ожидающим взаимодействия

        //выделенная клетка:
        Cell curcell; //какая клетка

        Column curcol; //в каком столбце
        int colnum = 0; //номер столбца

        Row currow; //в каком ряду
        int rownum = 0; //номер ряда

        Square cursqre; //в каком квадрате
        int sqrex = 0, sqrey = 0; // 0-2
        /**/

        //шрифт для написания известных цифр
        static Font bold = new Font(FontFamily.GenericSerif, 15.0F, FontStyle.Bold);
        //шрифт для написания заполненных цифр
        static Font italic = new Font(FontFamily.GenericSerif, 15.0F, FontStyle.Italic);
        public Form1()
        {
            InitializeComponent();

            tests.EnableTests(); //заполнение списка тестов

            sudoku = new Sudoku();
            DataGridViewCell[] cells = new DataGridViewTextBoxCell[9]; //ряд ячеек для таблицы
            vars = new Label[9, 9];
            uservars = new SortedSet<byte>[9,9];
            colvars = new Label[9];
            rowvars = new Label[9];
            numbuttons = new Button[9];
            Size labelsize = new Size(40, 13); //размер надписи
            Size buttonsize = new Size(80, 80); //размер кнопки
            Font numfont = new Font(FontFamily.GenericSerif, 20.0F, FontStyle.Bold); //шрифт для кнопок
            int i, j;
            
            for (i = 0; i < 9; i++) //создание кнопок для табло с цифрами
            {
                numbuttons[i] = new Button(); 
                numbuttons[i].Size = buttonsize; 
                numbuttons[i].Location = new Point(5 + 83 * (i % 3), 12 + 83 * (i / 3)); 
                numbuttons[i].Font = numfont; 
                numbuttons[i].Text = (i + 1).ToString(); //есть костыль, текст важен
                numbuttons[i].Enabled = false;
                numbuttons[i].Click += ClickedNumber; //костыль внутри clickednumber - кнопка считывается по тексту
                NumBox.Controls.Add(numbuttons[i]);
            }

            for (i = 0; i < 9; i++) //заполнение таблицы
            {
                for (j = 0; j < 9; j++) //создание и оформление ряда ячеек
                { 
                    cells[j] = new DataGridViewTextBoxCell();
                    cells[j].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    uservars[i, j] = new SortedSet<byte> { };
                    vars[i, j] = new Label();
                    vars[i, j].Text = "";
                    vars[i, j].Location = new Point(i * 45 + 2, j * 45 + 2);
                    vars[i, j].Size = labelsize;
                    vars[i, j].BackColor = Color.White;
                    panel1.Controls.Add(vars[i, j]);
                    vars[i, j].BringToFront();

                }
                //оформление надписей столбцов
                colvars[i] = new Label();
                colvars[i].Text = "";
                colvars[i].Location = new Point(i * 45 + 2, 405 + 2);
                colvars[i].Size = labelsize;
                colvars[i].BackColor = Color.Transparent;
                panel1.Controls.Add(colvars[i]);
                colvars[i].BringToFront();

                //оформление надписей рядов
                rowvars[i] = new Label();
                rowvars[i].Text = "";
                rowvars[i].Location = new Point(405 + 2, i * 45 + 2);
                rowvars[i].Size = labelsize;
                rowvars[i].BackColor = Color.Transparent;
                panel1.Controls.Add(rowvars[i]);
                rowvars[i].BringToFront();

                //добавление ряда ячеек в таблицу
                DataGridViewRow row = new DataGridViewRow();
                row.Cells.AddRange(cells);
                row.MinimumHeight = 45; //чтобы ячейки таблицы были квадратными. выглядит странно, но нашел только так
                SudokuGrid.Rows.Add(row);
            }
            curcell = sudoku.rows[rownum].cells[colnum];

            curcol = sudoku.cols[colnum];
            currow = sudoku.rows[rownum];
            cursqre = sudoku.sqrs[sqrey, sqrex];

            colnum = rownum = sqrey = sqrex = 0;
            cell_label.Text = curcell.CellInfo();
            column_label.Text = curcol.ShowInfo();
            row_label.Text = currow.ShowInfo();
            square_label.Text = cursqre.ShowInfo();

            LookCol.Enabled = false;
            AlterCol.Enabled = false;
            LookRow.Enabled = false;
            AlterRow.Enabled = false;
            LookSqre.Enabled = false;
            AlterSqre.Enabled = false;

            ready = false;
            ReadyMode();
            PrintToGrid();
            /**/
        }

        //нажатие на кнопку "тест"
        //создается судоку, случайно выбранное из списка тестов
        private void TestButton_Click(object sender, EventArgs e)
        { 
            sudoku = new Sudoku(tests.PickRandomTest() );

            curcell = sudoku.rows[rownum].cells[colnum];

            curcol = sudoku.cols[colnum];
            currow = sudoku.rows[rownum];
            cursqre = sudoku.sqrs[sqrey, sqrex];
            startsudoku = new Sudoku(sudoku);
            ready = true;
            ReadyMode();
            PrintToGrid();
        }

        //переключить режим готовности
        // ready==0 => условие еще не заполнено, отключить действия для решения
        // ready!=0 => заполнение условия завершено, включить действия для решения
        private void ReadyMode()
        {
            ColumnBox.Enabled = ready;
            RowBox.Enabled    = ready;
            SquareBox.Enabled = ready;
            HelpBox.Enabled = ready;
            readybutton.Enabled = !ready;
            AutoVariants.Checked = false;
        }

        
        //рисует решетку в таблице (визуальное разделение на квадраты)
        private void SudokuGrid_Paint(object sender, PaintEventArgs e)
        { //...как нормально нарисовать решетку в таблице? НИКАК
            //имею в виду, думал, что это можно сделать как-то в настройках таблицы, или вроде того, но нет(или я просто не нашел)
            //вместо этого рисую решетку поверх таблицы
            Pen pen = new Pen(Color.Black, 3);
            for (int i = 0; i < 4; i++)
            {
                e.Graphics.DrawLine(pen, i * 135, 0, i * 135, 405);
                e.Graphics.DrawLine(pen, 0, i * 135, 405, i * 135);
            }
        }
        
        //нажатие на ячейку таблицы
        //обновляются данные выделенной ячейки и выводится информация о клетке, столбце, ряде, квадрате
        //если ожидался выбор цифры, то он отменяется
        private void SudokuGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            colnum = e.ColumnIndex;
            rownum = e.RowIndex;
            sqrex = colnum / 3;
            sqrey = rownum / 3;

            curcell = sudoku.rows[rownum].cells[colnum];
            cell_label.Text = curcell.CellInfo();

            curcol = sudoku.cols[colnum];
            column_label.Text = curcol.ShowInfo();

            currow = sudoku.rows[rownum];
            row_label.Text = currow.ShowInfo();

            cursqre = sudoku.sqrs[sqrey, sqrex];
            square_label.Text = cursqre.ShowInfo();

            if (WaitingFor != Moves.nothing)
            {
                WaitingFor = Moves.nothing;
                EnableNumbers(null);
            }
        }

        //включить на табло цифры из set
        //если set==null выключить все цифры
        private void EnableNumbers(IEnumerable<byte> set)
        {
            if (set!=null)
                for (int i = 0; i < 9; i++)
                    numbuttons[i].Enabled = (set.Contains((byte)(i + 1))) ? true : false;
            else
                for (int i = 0; i < 9; i++)
                    numbuttons[i].Enabled = false;
        }
        
        //отслеживает нажатия на табло и запускает ранее выбранное действие с нажатой цифрой
        //обработанное судоку выводится на экран
        private void ClickedNumber(object sender, EventArgs e)
        {
            byte value;
            Button clicked = (Button)sender;

            value = byte.Parse(clicked.Text); // КОСТЫЛЬ!
            //...так делать нельзя. правильно передавать данные нужно через EventArgs

            if (WaitingFor == Moves.lookforplace)
                waiting.FindPlaceFor(value);
            else
            if (WaitingFor == Moves.writenum)
                curcell.WriteNumIntoCell(value, ready);
            else
            if (WaitingFor == Moves.writevar)
                uservars[curcell.X, curcell.Y].Add(value);
            else
            if (WaitingFor == Moves.erasevar)
                uservars[curcell.X, curcell.Y].Remove(value);
            WaitingFor = Moves.nothing;
            PrintToGrid();
            EnableNumbers(null);
            if (sudoku.CheckForWin())
                    Victory();
        }

        //нажатие на "найти место для цифры" в столбце
        //переход в состояние ожидания цифры для поиска и включение возможных для операции цифр
        private void LookCol_Click(object sender, EventArgs e)
        {
            WaitingFor = Moves.lookforplace;
            waiting = curcol;
            EnableNumbers(waiting.Missing);
        }
        //нажатие на "поиск чередований" в столбце
        private void AlterCol_Click(object sender, EventArgs e)
        {
            curcol.LookForAlternations();
            PrintToGrid();
        }

        //нажатие на "найти место для цифры" в ряду
        //переход в состояние ожидания цифры для поиска и включение возможных для операции цифр
        private void LookRow_Click(object sender, EventArgs e)
        {
            WaitingFor = Moves.lookforplace;
            waiting = currow;
            EnableNumbers(waiting.Missing);
        }

        //нажатие на "поиск чередований" в ряду
        private void AlterRow_Click(object sender, EventArgs e)
        {
            currow.LookForAlternations();
            PrintToGrid();
        }

        //нажатие на "найти место для цифры" в квадрате
        //переход в состояние ожидания цифры для поиска и включение возможных для операции цифр
        private void LookSqre_Click(object sender, EventArgs e)
        {
            WaitingFor = Moves.lookforplace;
            waiting = cursqre;
            EnableNumbers(waiting.Missing);
        }

        //нажатие на "поиск чередований" в ряду
        private void AlterSqre_Click(object sender, EventArgs e)
        {
            cursqre.LookForAlternations();
            PrintToGrid();
        }

        //нажатие на "вписать цифру"
        private void WriteNumber_Click(object sender, EventArgs e)
        {
            if (curcell.IsEmpty())
            {
                WaitingFor = Moves.writenum;
                EnableNumbers(curcell.Variants);
            }
        }
       
        //нажатие на "стереть цифру"
        private void EraseNumber_Click(object sender, EventArgs e)
        {
            curcell.Erase(ready);
            PrintToGrid();
        }
        
        //нажатие на "вписать вариант"
        private void WriteCellVariant_Click(object sender, EventArgs e)
        {
            if (curcell.IsEmpty())
            {
                WaitingFor = Moves.writevar;
                SortedSet<byte> full = new SortedSet<byte> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                EnableNumbers(full.Except(uservars[curcell.X, curcell.Y])  );
            }
        }
        
        //нажатие на "стереть вариант"
        private void EraseCellVariants_Click(object sender, EventArgs e)
        {
            WaitingFor = Moves.erasevar;
            EnableNumbers(uservars[curcell.X, curcell.Y]);
        }
        
        //нажатие на "отмена"
        private void Cancel_Click(object sender, EventArgs e)
        {
            WaitingFor = Moves.nothing;
            EnableNumbers(null);
        }

        //переключение галочки "Автоматические варианты"
        //

        private void AutoVariants_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = AutoVariants.Checked;
            ShowVariants = flag;
            for (int i = 0; i < 81; i++)
                uservars[i / 9, i % 9].Clear();

            LookCol.Enabled = flag;
            AlterCol.Enabled = flag;
            LookRow.Enabled = flag;
            AlterRow.Enabled = flag;
            LookSqre.Enabled = flag;
            AlterSqre.Enabled = flag;

            AutoVariants.Enabled = !flag; 
            WriteCellVariant.Enabled = !flag;
            EraseCellVariants.Enabled = !flag;
            PrintToGrid();
        }

        //нажатие на кнопку "готово"
        private void readybutton_Click(object sender, EventArgs e)
        {
            ready = true;
            for (int i = 0; i < 81; i++) 
                uservars[i / 9, i % 9].Clear();
            startsudoku = new Sudoku(sudoku);
            ReadyMode();
        }
        
        // нажатие на "новый"
        // отключение готовности - доступно заполнение условия
        private void resetbutton_Click(object sender, EventArgs e)
        {
            sudoku = new Sudoku();
            for (int i = 0; i < 81; i++)
                uservars[i / 9, i % 9].Clear();
            ready = false;
            ReadyMode();
            PrintToGrid();
        }

        // нажатие на "начать заново"
        private void restartbutton_Click(object sender, EventArgs e)
        {
            sudoku = new Sudoku(startsudoku);
            for (int i = 0; i < 81; i++)
                uservars[i / 9, i % 9].Clear();
            ready = true;
            ReadyMode();
            PrintToGrid(); 
        }

        //выводит сообщение о победе
        private void Victory()
        {
            var message = "УРА ПОБЕДА";
            var head = "Задача решена";
            MessageBox.Show(message, head, MessageBoxButtons.OK);
        }

        // нажатие на "0-й уровень перебора"
        private void SolveButton_Click(object sender, EventArgs e)
        {
            sudoku.Solve();

            PrintToGrid();

            if (sudoku.CheckForWin())
                Victory();
        }

        // нажатие на "1-й уровень перебора"
        private void HardSolveButton_Click(object sender, EventArgs e)
        {
            sudoku.HardSolve();

            PrintToGrid();

            if (sudoku.CheckForWin())
                Victory();    
        }

        //не должно быть, случайно добавил
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //вывод судоку в таблицу на экран 
        //флаг - есть что переделать
        public void PrintToGrid()
        {
            DataGridViewCell gridcell; //ячейка в таблице
            Cell cell; // клетка в судоку
            string symb;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    gridcell = SudokuGrid.Rows[i].Cells[j];
                    cell = sudoku.rows[i].cells[j];

                    symb = (cell.GetValue() == 0) ? "" : cell.GetValue().ToString();
                    gridcell.Value = symb;

                    if (cell.IsKnown()) //изначально известная клетка
                        gridcell.Style.Font = bold;
                    else //пустая или заполненная клетка
                        gridcell.Style.Font = italic;
                    
                    string text = "";
                    if (ShowVariants && cell.IsEmpty() && cell.Variants.Count <= 4) //если включены автоварианты
                    {

                            foreach (byte num in cell.Variants)
                                text += num.ToString();
                            vars[j, i].Text = text;
 
                    }
                    else //выключены автоварианты
                    {
                        foreach (byte num in uservars[j, i] )
                            text += num.ToString();
                        vars[j, i].Text = text;
                    }
                }

                if (!sudoku.cols[i].IsFull() && sudoku.cols[i].Missing.Count <= 5)
                { //если в столбе/строке осталось <=5 цифр, то показать их рядом
                    string text = "";
                    foreach (byte num in sudoku.cols[i].Missing)
                        text += num.ToString();
                    colvars[i].Text = text;
                }
                else
                    colvars[i].Text = "";

                //повторный код? подумать как объединить в метод
                if (!sudoku.rows[i].IsFull() && sudoku.rows[i].Missing.Count <= 5)
                {
                    string text = "";
                    foreach (byte num in sudoku.rows[i].Missing)
                        text += num.ToString();
                    rowvars[i].Text = text;
                }
                else
                    rowvars[i].Text = "";
            }
        }
        
        //волшебная кнопка-решалка
        //переписать полностью, когда будет готов n-ый уровень перебора ( MegaSolve() )
        private void ezsolve_Click(object sender, EventArgs e)
        {
            sudoku.MegaSolve();
            PrintToGrid();

            if (sudoku.CheckForWin())
                Victory();
        }
    }
    
}
