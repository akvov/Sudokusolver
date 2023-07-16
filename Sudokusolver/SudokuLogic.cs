using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudokusolver
{
    //множество статусов клетки - изначально_известна/заполнена/пуста/ошибка
    public enum CellStatus { known, written, empty, error };
    
    //struct Coordinates {byte Y;byte X;} //сделать нормальныечеловеческие координаты клетки

    //класс клетки судоку
    //у клетки есть координаты
    //есть столбец, ряд и квадрат в котором она находится
    //есть статус пусто/заполнено/известно изначально/ошибка
    //есть значение, которое записано в клетку (если клетка пустая, то значение равно нулю)
    //и набор вариантов цифр, которые можно вписать в клетку (если клетка не пустая, то набор null) 
    public class Cell
    {
        /**/ // вот эти вот хотелось бы сделать readonly, но в конструкторе их определить затруднительно
        public byte X;
        public byte Y;
        public Column CellCol;
        public Row CellRow;
        public Square CellSqre;

        CellStatus status; // пусто/известно/известно_сначала/ошибка
        byte Value; //если пустая - то тут ноль
        public SortedSet<byte> Variants; // если не пустая - то тут null

        //конструктор клетки
        //определяется значение value клетки и её статус
        public Cell(byte value)
        {
            Value = value;
            status = (value == 0) ? CellStatus.empty : CellStatus.known;
            Variants = (value == 0) ? new SortedSet<byte> { 1, 2, 3, 4, 5, 6, 7, 8, 9 } : null;
        }

        //некоторый костыль. по сути, это продолжение конструктора.
        //определить столбец/ряд/квадрат в конструкторе затруднительно
        //потому что в момент создания клеток они(столбцы/ряды/квадраты) еще не существуют
        //столбцы/ряды/квадраты создаются позже из уже созданных клеток
        //эта функция запускается после их создания
        //это мешает определить столбцы/ряды/квадраты как readonly
        //
        //возможно у этой проблемы есть некое изяшное решение, но я не сильно над этим задумывался
        public void SetContructs(Column col, Row row, Square sqre)
        {
            CellCol = col;
            CellRow = row;
            CellSqre = sqre;
            X = col.num;
            Y = row.num;
        }

        //функция, заполняющая клетку значением num
        //rdy показывает, является ли это действие частью решения(true) или заполнением условия(false) 
        //при заполнении клетки вызывается функция, корректирующая статусы в столбце/ряду/квадрате
        //также корректируются варианты во всех клетках столбца/ряда/квадрата
        public void WriteNumIntoCell(byte num, bool rdy = true)
        {
            if (IsEmpty())
            {
                CellCol.FoundNum(num);
                CellRow.FoundNum(num);
                CellSqre.FoundNum(num);
                Value = num;
                status = rdy ? CellStatus.written : CellStatus.known;
                Variants.Clear();
                ChangeCheck();
            }
        }

        //функция, стирающая значение из клетки
        //rdy показывает, является ли это действие исправлением ошибки при решении(true) или при заполнении условия(false)
        //при исправлении клетки вызывается функция, корректирующая статусы в столбце/ряду/квадрате
        //также корректируются варианты во всех клетках столбца/ряда/квадрата и в самой клетке
        public void Erase(bool rdy) 
        {
            if (status == CellStatus.written || (!rdy && status == CellStatus.known) )
            {
                CellCol.EraseNum(Value);
                CellRow.EraseNum(Value);
                CellSqre.EraseNum(Value);
                Value = 0;
                status = CellStatus.empty;
                Variants = new SortedSet<byte> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                CheckVariants();
                ChangeCheck();
            }
        }
        
        //пересчет вариантов в клетке
        //смотрим все цифры, найденные в столбце/ряду/квадрате
        //если находим цифру, которая есть в текущих вариантах, то убираем её из вариантов
        public void CheckVariants()  
        {
            if (status == CellStatus.empty)
                foreach (byte othersnum in CellCol.Found.Union(CellRow.Found.Union(CellSqre.Found)))
                    if (Variants.Contains(othersnum))
                        Variants.Remove(othersnum);
        }

        //пересчет вариантов во всех клетках столбца/ряда/квадрата
        public void ChangeCheck()
        { 
            foreach (Cell cell in CellCol.cells.Union(CellRow.cells))
                cell.CheckVariants();
            foreach (Cell cell in CellSqre.cells) // 
                cell.CheckVariants();
        }
        
        //возвращает true, если клетка пуста
        public bool IsEmpty()
        { return status == CellStatus.empty; }
        
        //возвращает true, если клетка изначально заполнена
        public bool IsKnown()
        { return status == CellStatus.known; }
        
        //возвращает true, если в клетке ошибка
        public bool IsError()
        { return status == CellStatus.error; }

        //возвращает true, если в клетку можно вписать num
        //если клетка пуста и в нее нельзя ничего вписать, меняет статус на ошибку
        public bool CanNumHere(byte num)
        {
            if (IsEmpty())
                if (Variants != null)
                    return Variants.Contains(num);
                else
                {
                    status = CellStatus.error;
                }
            else 
                return false;
            return false; 
        }

        //возвращает значение клетки
        public byte GetValue()
        { return Value; }
        
        //возвращает строку с информацией о клетке
        public string CellInfo()
        {
            string text = " Клетка " + (Y + 1) + ',' + (X + 1) +
                "\n Значение " + Value +
                "\n Статус " + status.ToString();
            if (IsEmpty())
            {
                text += "\n Варианты ";
                foreach (byte elem in Variants)
                    text += (elem.ToString() + ' ');
            }
            return text;
        }

    }

    // сравнивает клетки по количеству вариантов
    public class CompareCellByVariants : Comparer<Cell>
    {
        public override int Compare(Cell x, Cell y)
        {
            if (x == y) return 0;
            else if (x.Variants.Count >= y.Variants.Count) return 1;
            else return -1;
        }
    }

    //множество статусов конструкта - незаполнен/заполнен/ошибка
    public enum StructStatus { process, full, error };
    //public enum StructType { column, row, square };
    
    //классы строк, столбцов и квадратов являются наследниками абстрактного класса "конструкт"
    //конструкт содержит в себе логику, общую для всех наследников - строк/столбцов/квадратов, а именно:
    // 9 клеток
    // наборы найденных и ненайденных чисел
    // статус заполнено/незаполнено/ошибка
    // набор найденных чередований
    public abstract class Construct 
    { 
        public Cell[] cells;

        public SortedSet<byte> Missing; // каких цифр не хватает
        public SortedSet<byte> Found; // какие цифры есть

        //StructType type; //на будущее, когда я решусь удалить наследников
        protected StructStatus status; // заполнено/незаполнено/ошибка

        protected HashSet<Cell[]> alternations; //сет чередований в конструкте

        //добавление чередования.
        //если найденое чередование содержится в одном из имеющемся, большее удаляется  
        private void AddAlternation(Cell[] arr)
        {
            Cell[] alter = new Cell[arr.Length];
            int i;
            for (i = 0; i < arr.Length; i++)
                alter[i] = arr[i];
            HashSet<Cell[]> fordelete = new HashSet<Cell[]> { };
            bool addflag = true;
            foreach (Cell[] alters in alternations)
                if (alter.Length < alters.Length)
                    for (i = 0; i < alter.Length; i++)
                    {
                        if (alters.Contains(alter[i]))
                        {
                            fordelete.Add(alters);
                            break;
                        }
                    } //скобки не лишние, не удалять
                else
                    for (i = 0; i < alters.Length; i++)
                        if (alter.Contains(alters[i]))
                        {
                            addflag = false;
                            break;
                        }
            if (fordelete.Count() != 0)
                foreach (Cell[] del in fordelete)
                    alternations.Remove(del);
            if (addflag)
                alternations.Add(alter);
        }

        // проверка набора клеток на уже имеющееся чередование
        private bool IsAlreadyAlternation(Cell[] arr)
        {
            bool flag;
            bool found = false;
            foreach (Cell[] alter in alternations)
            {
                flag = true;
                if (alter.Length == arr.Length)
                {
                    for (int i = 0; i < arr.Length; i++)
                        if (flag && alter[i] != arr[i])
                        {
                            flag = false;
                            break;
                        }
                } //
                else
                    flag = false;

                if (flag)
                {
                    found = true;
                    break;
                }

            }
            return found;
        }

        //"ищем место для цифры num"
        //рассматривает набор клеток в конструкте, в которые можно вписать num
        //набор также проверяется на общую принадлежность к другому конструкту (т.к. это дает возможность удалить варианты)
        //возвращает true если где-либо изменились варианты
        public bool FindPlaceFor(byte num)
        {
            if (status == StructStatus.process && Missing.Contains(num))
            {
                HashSet<Cell> places = new HashSet<Cell>();
                foreach (Cell cell in cells)
                    if (cell.IsEmpty() && cell.CanNumHere(num))
                        places.Add(cell);
                if (places == null)
                { status = StructStatus.error; }
                
                return CheckForUniteConstruct(places, num);
            }
            else return false;

        }

        //проверка на общие конструкты в наборе клеток
        // по типу "смотрел как расположить цифру в столбе,
        // оказалось что она строго в том квадрате - 
        // значит, в квадрате больше не искать"
        //возвращает true если где-либо были изменены варианты (т.е. прогресс в решении)
        private bool CheckForUniteConstruct(HashSet<Cell> places, byte num)
        {
            bool res = false;
            if (places.Count == 1) // если у num только одно место в конструкте
                places.First().Variants = new SortedSet<byte> { num }; //то другие варианты не нужны

            if (places.Count >= 1 && places.Count <= 5)
            {
                bool colflag = true, rowflag = true, sqrflag = true;
                Column unitecol = places.First().CellCol;
                Row uniterow = places.First().CellRow;
                Square unitesqr = places.First().CellSqre;
                foreach (Cell cell in places)
                {
                    if (colflag == true && cell.CellCol != unitecol)
                        colflag = false;
                    if (rowflag == true && cell.CellRow != uniterow)
                        rowflag = false;
                    if (sqrflag == true && cell.CellSqre != unitesqr)
                        sqrflag = false;
                }
                if (colflag)
                    foreach (Cell cell in unitecol.cells)
                        if (!places.Contains(cell) && cell.IsEmpty() && cell.Variants.Contains(num))
                        {
                            res = true;
                            cell.Variants.Remove(num); 
                        }
                if (rowflag)
                    foreach (Cell cell in uniterow.cells)
                        if (!places.Contains(cell) && cell.IsEmpty() && cell.Variants.Contains(num))
                        {
                            res = true;
                            cell.Variants.Remove(num); 
                        }
                if (sqrflag)
                    foreach (Cell cell in unitesqr.cells)
                        if (!places.Contains(cell) && cell.IsEmpty() && cell.Variants.Contains(num))
                        {
                            res = true;
                            cell.Variants.Remove(num); 
                        }
            }
            return res;

        }

        //корректировка данных при нахождении цифры
        public void FoundNum(byte num)
        {
            Missing.Remove(num); // не хватает на одну меньше
            Found.Add(num);      // нашли на одну больше
            if (Found.Count() == 9)
                status = StructStatus.full;

        }

        //корректировка данных при удалении цифры
        public void EraseNum(byte num) 
        {
            if (status == StructStatus.full)
                status = StructStatus.process;
            foreach (Cell cell in cells)
                if (cell.IsEmpty())
                    cell.Variants.Add(num); //добавить вариант в остальные пустые клетки конструкта
            Missing.Add(num); // не хватает на одну больше
            Found.Remove(num);      // нашли на одну меньше
        }

        //проверка на заполненность
        public bool IsFull()
        { return status == StructStatus.full; }

        //проверка на ошибку
        public bool IsError()
        { return status == StructStatus.error; }

        //поиск чередований в конструкте
        //ПОЯСНЕНИЕ
        //если, например, мы видим в двух пустых клетках квадрата варианты 15,
        //значит эти клетки чередуются - если в одной 1, то в другой 5, и наоборот
        //тогда мы понимаем, что в остальных клетках квадрата не нужно искать ни 1 ни 5
        //и это работает не только с вариантами 15,
        //не только с квадратами,
        //и не только с ДВУМЯ числами
        //критерий чередования - количество различных вариантов в клетках равно количеству клеток
        //эта функция перебирает сочетания клеток в конструкте и проверяет, является ли оно чередованием
        //найденные чередования записываются в соответствующее поле конструкта
        //в процессе перебора сочетаний проводится в том числе проверка на уже имеющиеся чередования
        //и полученное чередование проверяется на общую принадлежность к другому конструкту
        //возвращает true если было найдено что-то новое
        public bool LookForAlternations()
        {
            bool res = false;
            if (status == StructStatus.process)
            {
                int misscount = Missing.Count; // сколько не хватает цифр в конструкте
                int i, j = 0;
                Cell[] emptycells = new Cell[misscount]; // клетки которые пустые
                for (i = 0; i < 9; i++) //смотрим все клетки
                    if (cells[i].IsEmpty()) //откладываем только пустые
                    {
                        emptycells[j] = cells[i];
                        j++;
                    }
                Cell[] alternation; // набор под чередование
                byte[,] combs; //варианты наборов
                int countcandidte; // сколько элементов в чередовании ищем (кандидаты в чередование)
                int numbcandidte; // номер кандитата в счетчике (который будет потом)
                int countcombs; //сколько вариантов наборов = cnk
                for (countcandidte = 1; countcandidte < misscount; countcandidte++)
                {
                    alternation = new Cell[countcandidte]; // массив под чередование
                    combs = combinations(misscount, countcandidte); //все наборы из н по к. нужно сделать по другому 
                    countcombs = cnk(misscount, countcandidte); // количество наборов
                    bool checkflag; //флаг, что каждая клетка по отдельности подходит по количеству вариантов 
                    for (i = 0; i < countcombs; i++) //пробуем все комбинации
                    {
                        checkflag = true; //пока не нашли ошибку, все правильно
                        for (numbcandidte = 0; numbcandidte < countcandidte; numbcandidte++) //собираем набор из клеток
                        {
                            if (emptycells[combs[i, numbcandidte]].Variants.Count <= countcandidte) // проверяем количество вариантов в клетках набора
                                alternation[numbcandidte] = emptycells[combs[i, numbcandidte]];
                            else
                            {
                                checkflag = false; // если хоть один в наборе не подходит, то весь набор не подходит
                                break; //переход к следующему набору
                            }
                        }

                        if (checkflag) //набор в котором каждая клетка имеет вариантов меньше количества клеток
                        {
                            if (IsAlreadyAlternation(alternation)) continue;

                            IEnumerable<byte> united = new SortedSet<byte> { }; //
                            foreach (Cell elem in alternation)
                                united = united.Union(elem.Variants);
                            if (united.Count() == countcandidte) //набор, прошедший все проверки
                            {
                                AddAlternation(alternation); //записываем чередование в конструкт
                                foreach (byte num in united)
                                    res|= CheckForUniteConstruct(alternation.ToHashSet(), num);
                            }
                            else
                            if (united.Count() > countcandidte) //набор излишний
                                continue;
                            else //ошибка типа: "впихнуть 3 цифры в 2 клетки"
                            { status = StructStatus.error; }
                        }
                        else //набор не подходит
                            continue; //переход к следующему набору
                    }
                }
            }
            return res;
        }

        //число сочетаний из n элементов по k штук (цэ из эн по ка)
        private int cnk(int n, int k)
        {
            int nf = 1, nkf = 1, kf = 1, mul = 1;
            
            //for (i = 1; i <= n; i++) nf *= i;
            //for (i = 1; i <= k; i++) kf *= i;
            //for (i = 1; i <= n - k; i++) nkf *= i;

            for (int i = 1; i <= n; i++)
            {
                mul *= i;
                nf = (i == n) ? mul : nf;
                kf = (i == k) ? mul : kf;
                nkf = (i == n - k) ? mul : nkf;
            }
            return nf / (kf * nkf);
        }

        //сочетания из n элементов по k штук
        //возвращает массив, по элементам этого массива потом(в другой функции) составляем наборы клеток
        //...идея мне не нравится, по-другому не думал как делать
        //через непомерно большое время я таки узнал о next_permutation() 
        //но идея все равно поганая, нужно лучше
        private byte[,] combinations(int n, int k)
        {
            int g, count = cnk(n, k);
            byte[,] res = new byte[count, k];
            byte i, j, m;
            byte[] comb = new byte[k];
            for (i = 0; i < k; i++)
            {
                res[0, i] = i;
                comb[i] = i;
            }
            for (i = 0; i < count - 1; i++)
            {
                if (comb[k - 1] < n - 1)
                    comb[k - 1]++;
                else
                {
                    m = (byte)(k - 2); // м - первая с конца цифра, которую надо менять
                    while (comb[m] == comb[m + 1] - 1)
                        m--;
                    comb[m]++;
                    for (g = m + 1; g < k; g++)
                        comb[g] = (byte)(comb[g - 1] + 1);
                }
                for (j = 0; j < k; j++)
                    res[i + 1, j] = comb[j];
            }
            return res;
        }

        //возвращает строку с информацией о конструкте
        public abstract string ShowInfo();
    }
    
    //классы столбцов/рядов/квадратов 
    //очень схожи. все наследники construct, одинаковые конструкторы
    //единственная разница - в выводе информации
    //...скорее всего излишнее, можно обойтись только конструктами, а информацию правильно вывести можно и по другому
    public class Column : Construct
    {
        public byte num;
        public Column(Cell[] cls, byte numb)
        {
            cells = new Cell[9];
            for (int i = 0; i < 9; i++)
                cells[i] = cls[i]; //по значению, не по ссылке
            num = numb;
            status = StructStatus.process;
            Missing = new SortedSet<byte> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Found = new SortedSet<byte> { };

            alternations = new HashSet<Cell[]> { };

            for (int i = 0; i < 9; i++)
                if (!cells[i].IsEmpty())
                    FoundNum(cells[i].GetValue());
        }

        public override string ShowInfo()
        {
            string text;
            text = " Столб " + (num + 1) +
                "\n Есть ";
            foreach (byte elem in Found)
                text += elem.ToString() + ' ';
            text += "\n Не хватает ";
            foreach (byte elem in Missing)
                text += elem.ToString() + ' ';
            text += "\n Статус " + status.ToString();
            return text;
        }
    }
    public class Row : Construct
    {
        public byte num;
        public Row(Cell[] cls, byte numb)
        {
            cells = new Cell[9];
            for (int i = 0; i < 9; i++)
                cells[i] = cls[i];

            num = numb;
            status = StructStatus.process;
            Missing = new SortedSet<byte> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Found = new SortedSet<byte> { };

            alternations = new HashSet<Cell[]> { };

            for (int i = 0; i < 9; i++)
                if (!cells[i].IsEmpty())
                    FoundNum( cells[i].GetValue() );
        }
        public override string ShowInfo()
        {
            string text;
            text = " Ряд " + (num + 1) +
                "\n Есть ";
            foreach (byte elem in Found)
                text += elem.ToString() + ' ';
            text += "\n Не хватает ";
            foreach (byte elem in Missing)
                text += elem.ToString() + ' ';
            text += "\n Статус " + status.ToString();
            return text;
        }
    }
    public class Square : Construct
    {
        public byte[] num;
        public Square(Cell[] cls, byte[] numb)
        {
            cells = new Cell[9];
            for (int i = 0; i < 9; i++)
                cells[i] = cls[i];
            num = numb;
            status = StructStatus.process;
            Missing = new SortedSet<byte> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Found = new SortedSet<byte> { };

            alternations = new HashSet<Cell[]> { };

            for (int i = 0; i < 9; i++)
                if (!cells[i].IsEmpty())
                    FoundNum(cells[i].GetValue());
        }
        public override string ShowInfo()
        {
            string text;
            text = " Квадрат " + (num[0] + 1) + ',' + (num[1] + 1) +
                "\n Есть ";
            foreach (byte elem in Found)
                text += elem.ToString() + ' ';
            text += "\n Не хватает ";
            foreach (byte elem in Missing)
                text += elem.ToString() + ' ';
            text += "\n Статус " + status.ToString();
            return text;
        }
    }

    //класс судоку
    // 9 столбцов, строк, квадратов
    //набор конструкторов (из матрицы, копирования; по умолчанию)
    //
    public class Sudoku
    {
        public Column[] cols;
        public Row[] rows;
        public Square[,] sqrs;

        public Sudoku(int[,] arr)
        {
            Cell[,] cells = new Cell[9, 9];
            Cell[] buff = new Cell[9];
            cols = new Column[9];
            rows = new Row[9];
            sqrs = new Square[3, 3];

            byte i, j;
            for (i = 0; i < 9; i++)
                for (j = 0; j < 9; j++)
                    cells[i, j] = new Cell((byte)arr[i, j]); //создаем клетки
            
            for (i = 0; i < 9; i++) //заполняем конструкты клетками
            {
                for (j = 0; j < 9; j++)
                    buff[j] = cells[j, i];
                cols[i] = new Column(buff, i);
                for (j = 0; j < 9; j++)
                    buff[j] = cells[i, j];
                rows[i] = new Row(buff, i);
                for (j = 0; j < 9; j++)
                    buff[j] = cells[(i / 3) * 3 + j / 3, (i % 3) * 3 + j % 3];
                sqrs[i / 3, i % 3] = new Square(buff, new byte[2] { (byte)(i / 3), (byte)(i % 3) });
            }

            for (i = 0; i < 9; i++)
                for (j = 0; j < 9; j++)
                    cells[i, j].SetContructs(cols[j], rows[i], sqrs[i / 3, j / 3]);
            
            for (i = 0; i < 9; i++)
                for (j = 0; j < 9; j++)
                    cells[i, j].CheckVariants();
        }
        public Sudoku(Sudoku other)
        {
            Cell[,] cells = new Cell[9, 9];
            Cell[] buff = new Cell[9];
            cols = new Column[9];
            rows = new Row[9];
            sqrs = new Square[3, 3];

            byte i, j;
            for (i = 0; i < 9; i++)
                for (j = 0; j < 9; j++)
                {
                    cells[i, j] = new Cell(other.rows[i].cells[j].GetValue());
                    cells[i, j].Variants = new SortedSet<byte> { };
                    if (other.rows[i].cells[j].Variants != null)
                        foreach (byte var in other.rows[i].cells[j].Variants)
                            cells[i, j].Variants.Add(var);
                }
            for (i = 0; i < 9; i++) //заполняем конструкты клетками
            {
                for (j = 0; j < 9; j++)
                    buff[j] = cells[j, i];
                cols[i] = new Column(buff, i);
                for (j = 0; j < 9; j++)
                    buff[j] = cells[i, j];
                rows[i] = new Row(buff, i);
                for (j = 0; j < 9; j++)
                    buff[j] = cells[(i / 3) * 3 + j / 3, (i % 3) * 3 + j % 3];
                sqrs[i / 3, i % 3] = new Square(buff, new byte[2] { (byte)(i / 3), (byte)(i % 3) });
            }
            for (i = 0; i < 9; i++)
                for (j = 0; j < 9; j++)
                    cells[i, j].SetContructs(cols[j], rows[i], sqrs[i / 3, j / 3]);
        }
        public Sudoku()
        {
            Cell[,] cells = new Cell[9, 9];
            Cell[] buff = new Cell[9];
            cols = new Column[9];
            rows = new Row[9];
            sqrs = new Square[3, 3];

            byte i, j;
            for (i = 0; i < 9; i++)
                for (j = 0; j < 9; j++)
                    cells[i, j] = new Cell(0); //создаем клетки
            for (i = 0; i < 9; i++) //заполняем конструкты клетками
            {
                for (j = 0; j < 9; j++)
                    buff[j] = cells[j, i];
                cols[i] = new Column(buff, i);
                for (j = 0; j < 9; j++)
                    buff[j] = cells[i, j];
                rows[i] = new Row(buff, i);
                for (j = 0; j < 9; j++)
                    buff[j] = cells[(i / 3) * 3 + j / 3, (i % 3) * 3 + j % 3];
                sqrs[i / 3, i % 3] = new Square(buff, new byte[2] { (byte)(i / 3), (byte)(i % 3) });
            }

            for (i = 0; i < 9; i++)
                for (j = 0; j < 9; j++)
                    cells[i, j].SetContructs(cols[j], rows[i], sqrs[i / 3, j / 3]);
        }

        //возвращает true если все решено
        public bool CheckForWin() 
        {
            bool flag = true;
            for (int i = 0; i < 9; i++)
                if (!rows[i].IsFull() || !cols[i].IsFull() || !sqrs[i / 3, i % 3].IsFull())
                    flag = false;
            return flag;
        }

        //проверка двух судоку на равенство - все клетки со всеми вариантами совпадают
        public bool CheckForEqual(Sudoku other) 
        {
            bool flag = true;
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (flag &&
                        rows[i].cells[j].Variants != null && other.rows[i].cells[j].Variants != null &&
                        rows[i].cells[j].Variants.Count != other.rows[i].cells[j].Variants.Count //достаточно бредово. надобы неравно но не робит
                        )
                    {
                        flag = false;
                        break;
                    }
            return flag;
        }

        //проверка на наличие ошибок в конструктах
        //возвращает false если ошибок нигде нет
        public bool CheckForError() 
        {
            bool res = false;
            byte i, j;
            for (i = 0; i < 9; i++)
            {
                for (j = 0; j < 9; j++)
                    if (!res && rows[i].cells[j].IsError())
                        res = true;
                if (rows[i].IsError() || cols[i].IsError() || sqrs[i / 3, i % 3].IsError())
                    res = true;
                if (res)
                    break;
            }
            return res;
        }

        //решение - нулевой уровень перебора
        //все решается одной логикой, ничего не нужно угадывать
        //в каждой строке/столбце/квадрате ищем чередования и места под каждую цифру
        //если где-то остается только один вариант, то вписываем его
        //flag обозначает наличие прогресса в решении
        // в принципе, этого хватает для решения большинства судоку
        public void Solve()
        {
            byte i, j;
            bool flag = true; 
            while (flag) 
            {
                flag = false;
                for (i = 0; i < 9; i++) 
                {
                    if (!cols[i].IsFull())
                    {
                        flag |= cols[i].LookForAlternations();
                        for (j = 1; j <= 9; j++)
                            flag |= cols[i].FindPlaceFor(j);
                    }
                    if (!rows[i].IsFull())
                    {
                        flag |= rows[i].LookForAlternations();
                        for (j = 1; j <= 9; j++)
                            flag |= rows[i].FindPlaceFor(j);
                    }
                    if (!sqrs[i / 3, i % 3].IsFull())
                    {
                        flag |= sqrs[i / 3, i % 3].LookForAlternations();
                        for (j = 1; j <= 9; j++)
                            flag |= sqrs[i / 3, i % 3].FindPlaceFor(j);
                    }
                }
                for (i = 0; i < 9; i++)
                    for (j = 0; j < 9; j++)
                        if (rows[i].cells[j].IsEmpty() && rows[i].cells[j].Variants.Count == 1)
                            rows[i].cells[j].WriteNumIntoCell(rows[i].cells[j].Variants.First());
            }
            
        }

        //решение - первый уровень перебора(т.е. нужно угадать одну цифру, и после этого все сойдется)
        //решаем судоку нулевым уровнем перебора, не получается, тогда выбираем пустую клетку, смотрим её варианты
        //копируем судоку (называю его альт), пишем в него какой-нибудь вариант, решаем до упора,
        //смотрим что получилось, делаем выводы, перебираем таким образом все варианты
        //какие могут быть выводы:
        //1) если оно решилось, то вставляем этот вариант в клетку
        //2) если вариант привел к ошибке, то удаляем вариант
        //3) если вышеперечисленное не произошло, то это ничего нам не дает
        // +
        //4) по логике, если все доступные в клетке варианты 
        //при решении дают одинаковый вывод в какой-то другой клетке,
        //то этот вывод надо записать
        //... но у меня пока не получилось отследить такую ситуацию и проверить работу кода
        public Sudoku[] HardSolve(Cell cell) 
        {
            //int i, j, k;
            int unicount = cell.Variants.Count;
            byte value;
            Sudoku[] Alt;
            int cellx = cell.X;
            int celly = cell.Y;
            bool equalflag = true;

            Alt = new Sudoku[unicount];

            HashSet<byte> fordelete = new HashSet<byte>(); //список на удаление - если удалять сразу, ломается нумерация

            for (int k = 0; k < unicount; k++)
            {
                Alt[k] = new Sudoku(this); //создали альт
                value = cell.Variants.ElementAt(k);
                Alt[k].rows[celly].cells[cellx].WriteNumIntoCell(value); //вставили вариант
                
                Alt[k].Solve();

                if (Alt[k].CheckForWin()) //1) мы его решили и это хорошо
                {
                    cell.WriteNumIntoCell(value);
                    Console.WriteLine("В какой-то вселенной оно решилось" + (celly + 1).ToString() + " " + (cellx + 1).ToString() + "  - " + value.ToString());
                    return null; //а зачем дальше решать что-то, если обычного solve хватит?
                }
                if (Alt[k].CheckForError()) //true => есть ошибка
                { //2) вариант ведет к ошибке => вариант неверный
                    fordelete.Add(value);
                    Console.WriteLine("ведет к ошибке " + (celly + 1).ToString() + " " + (cellx + 1).ToString() + "  - " + value.ToString());
                }
                //3) ничего. и тогда надо перебирать дальше
            }

            foreach (byte var in fordelete)
                rows[celly].cells[cellx].Variants.Remove(var);
            
            /**/ // идея то логичная, но схлопывает неправильно / хотя хз но проверить один хрен надо
            for (int i = 0; i < 9; i++) // хз нужно ли это вообще, но сама идея логичная
                for (int j = 0; j < 9; j++) //что ищем:
                {//рассматривамая клетка пустая, та же клетка в альтах заполнена одинаковой цыфирью
                    if (rows[i].cells[j].IsEmpty() && //в оригинале клетка пуста
                        Alt != null && //альты есть
                        Alt.Length > 0 && //альты есть 
                        !Alt[0].rows[i].cells[j].IsEmpty() ) // в первом альте клетка не пуста
                    {
                        for (int k = 1; k < unicount; k++) //если подставили все варианты в клетку
                            if (
                                Alt[k] != null && //альт есть
                                !Alt[k].CheckForError() && //альт не скатился в ошибку
                                (Alt[k].rows[i].cells[j].IsEmpty() || //в каком-то альте та же клетка пуста или
                                Alt[k].rows[i].cells[j].GetValue() != Alt[0].rows[i].cells[j].GetValue() )//не совпадает с первым альтом
                                )
                            {
                                equalflag = false;
                                break;
                            }
                    }
                    else
                        equalflag = false;

                    if (equalflag //&& 
                                  //AltUniverse.Length > 0
                        ) //и если воттакое нашли
                    { //ни разу не видел, чтобы это сработало
                        // затруднительно проверить правильность
                        Console.WriteLine("в клетке " + (celly + 1).ToString() + " " + (cellx + 1).ToString() + " мультивселенная схлопнулась " + (i + 1).ToString() + " " + (j + 1).ToString() + "=>" + Alt[0].rows[i].cells[j].GetValue());
                        rows[i].cells[j].WriteNumIntoCell(Alt[0].rows[i].cells[j].GetValue());
                        return null;
                    }
                }
            /**/
            return Alt;
        }

        //n-ый уровень перебора
        //в разработке
        public void MegaSolve()
        //отчаянный(но рабочий?) перебор в надежде найти хоть какое-то решение
        //переписать? есть идея лучше - рекурсия и всё такое
        {
            Console.WriteLine("Megasolve");
            //int i, j, k;
            int cellx, celly;
            byte value;
            bool equalflag;
            bool final = false;
            int countseq, seq;
            SortedSet<Cell> Emptys = SortedEmptyCells();
            SortedSet<Cell> Emptysbuff;
            Cell[] sequence; // последовательность клеток, которые нужно угадать
            /*Sudoku[] AltUniverse;*/
            Sudoku[] buff;
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (rows[i].cells[j].IsEmpty())
                        Emptys.Add(rows[i].cells[j]);

            for (countseq = 2; countseq < 3; countseq++) //верх потом поменять. хз на что, но по хорошему верха... не должно быть? это же и есть уровень перебора
            {
                sequence = new Cell[countseq];
                foreach (Cell cell in Emptys)
                {
                    sequence[0] = cell;
                    cellx = cell.X;
                    celly = cell.Y;
                    buff = HardSolve(cell); //набор альтов для клетки. вставили в клетку все варианты, смортим что из этого получается
                    //важно помнить, что hardsolve не решает полностью, просто делает выводы
                    for (seq = 1; seq < countseq; seq++)
                    {
                        foreach (Sudoku alt in buff)
                        {
                            alt.Solve();
                            Emptysbuff = alt.SortedEmptyCells(); //пустые клетки в альте
                            foreach (Cell altcell in Emptysbuff)
                            {
                                sequence[seq] = altcell;
                                if (!alt.CheckForError() && alt.HardSolve(altcell) == null)
                                {
                                    alt.Solve();
                                    if (alt.CheckForWin()) //
                                    {
                                        Console.WriteLine("АЧЁВСМЫСЛЕ!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                                        Console.WriteLine((celly + 1).ToString() + (cellx + 1).ToString() + " " + alt.rows[celly].cells[cellx].GetValue());
                                        Console.WriteLine((altcell.Y + 1).ToString() + (altcell.X + 1).ToString() + " " + alt.rows[altcell.Y].cells[altcell.X].GetValue());
                                        rows[celly].cells[cellx].WriteNumIntoCell(alt.rows[celly].cells[cellx].GetValue() );
                                        rows[altcell.Y].cells[altcell.X].WriteNumIntoCell(altcell.GetValue());
                                        final = true;
                                        break;
                                    }

                                }

                            }

                            if (final) break;

                        }
                        if (final) break;

                    }
                    if (final) break;
                }
                if (final) break;
            }

            Console.WriteLine("endmega");

        }

        //возвращает сет из клеток, сортированный по возрастанию количества вариантов
        private SortedSet<Cell> SortedEmptyCells()
        {
            Comparer<Cell> CountVars = new CompareCellByVariants();
            SortedSet<Cell> res = new SortedSet<Cell>(CountVars);
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (rows[i].cells[j].IsEmpty())
                        res.Add(rows[i].cells[j]);
            return res;
        }

        //тестовый консольный вывод. подлежит дальнейшему удалению
        public void ConsoleWrite() 
        {
            int i, j;
            for (i = 0; i < 9; i++)
            {
                for (j = 0; j < 9; j++)
                {
                    Console.Write(rows[i].cells[j].GetValue().ToString() + " " + (((j + 1) % 3 == 0) ? " " : ""));
                }
                Console.WriteLine((((i + 1) % 3 == 0) ? "\n" : ""));
            }
        }

    }
}