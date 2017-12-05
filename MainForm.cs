using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace myGraf
{
    public partial class mForm1 : Form
    {
        System.Diagnostics.Stopwatch SW = new System.Diagnostics.Stopwatch();

        // Стэк, для записи путей, циклов. Которые потом будут выводиться.
        private Stack<sbyte> mSaveWay = new Stack<sbyte>();
        // В этом массиве будут храниться все узлы.
        private List<mNode> mGraf = new List<mNode>();
        // Матрица смежности
        private mMatrix2D<sbyte> mMatrix = new mMatrix2D<sbyte>();
        // Поле принимает значение 'True' если мы двигаем узел, 'False' если иначе.
        private bool mMoveNode;
        // После некоторых действий ( отобразить цвета, уровни и т.п. )
        // необходимо очистить значения цвета и уровня. Если поле = 'True' то
        // происходит очищение значений.
        private bool mNecessaryResetNode;
        // Если поле 'True' то граф неориентированный. Иначе - ориентированный.
        private bool NEORIENT;
        // Поле хранит значение, которое является номером последней нажатой вершины.
        private sbyte mNumberOfChosenNode;
        // Поле хранит значениие, которое является номером передвигаемой вершины.
        private sbyte mNumberOfMovingNode;
        // Массив, далее называемый "Очередь отмены" либо "Отмена", сюда 
        // будут заноситься пары значений, для отмены последнего действия.
        private List<KeyValuePair<sbyte, sbyte>> mCancelingQueue;
        // Метод, необходимый для раскраски графа. Для динамичного выбора цвета, в зависимости от индекса 'p_i'
        // Не додумался как кроме трех чисел в палитре RGB определять число в процессе окраски, поэтому сделал такой метод.
        private Pen mOrientPen;
        private Color mColor(byte p_i)
        {
            switch (p_i)
            {
                case 1: return Color.LightGray;
                case 2: return Color.Red;
                case 3: return Color.Blue;
                case 4: return Color.Brown;
                case 5: return Color.Aqua;
                case 6: return Color.Chocolate;
                case 7: return Color.Khaki;
                case 8: return Color.Orange;
                case 9: return Color.Coral;
                case 10: return Color.CornflowerBlue;
                case 11: return Color.Cyan;
                case 12: return Color.AliceBlue;
                case 13: return Color.Aquamarine;
                case 14: return Color.Beige;
                case 15: return Color.FloralWhite;
                case 16: return Color.Lime;
                case 17: return Color.Maroon;
                case 18: return Color.OldLace;
                case 19: return Color.PapayaWhip;
                case 20: return Color.Sienna;
            }
            return Color.Black;
        }
        public mForm1()
        {
            InitializeComponent();
            ChoiceTypeGraf CTG = new ChoiceTypeGraf();
            DialogResult DR = CTG.ShowDialog();
            if (DR == DialogResult.OK) NEORIENT = false;
            else NEORIENT = true;
            mCancelingQueue = new List<KeyValuePair<sbyte, sbyte>>(52);
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {            
            GraphicsPath hPath = new GraphicsPath();
            Point[] Points = { new Point(2, -15), new Point(-2, -15), new Point(0, -5) };
            hPath.AddPolygon(Points);
            CustomLineCap HookCap = new CustomLineCap(null, hPath);
            mOrientPen = new Pen(Color.Crimson, 2);
            mOrientPen.CustomEndCap = HookCap;
            Reset(NEORIENT);
        }
        private void butDraw_Click(object sender, EventArgs e)
        {
            if (!mnuPaintCreate.Checked)
            {
                mnuPaintCreate.Checked = true;
                mNumberOfMovingNode = -1;
                mGrafRegion.MouseDown += new MouseEventHandler(mGrafRegion_MouseDown);
                mGrafRegion.MouseUp += new MouseEventHandler(mGrafRegion_MouseUp);
                mGrafRegion.MouseMove += new MouseEventHandler(mGrafRegion_MouseMove);
                mGrafRegion.Cursor = new Cursor(Cursors.Cross.CopyHandle());
                mCursorPosition.Text = "Позиция курсора - X: 0, Y: 0.";
            }
            else
            {
                mnuPaintCreate.Checked = false;
                mGrafRegion.MouseDown -= new MouseEventHandler(mGrafRegion_MouseDown);
                mGrafRegion.MouseUp -= new MouseEventHandler(mGrafRegion_MouseUp);
                mGrafRegion.MouseMove -= new MouseEventHandler(mGrafRegion_MouseMove);
                mGrafRegion.Cursor = new Cursor(Cursors.Default.CopyHandle());
                mCursorPosition.Text = "";
            }
        }
        // Процедура, будет реагировать на нажатие клавиши мыши в области, предназначенной для отрисовки графа
        private void mGrafRegion_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Если при нажатии мы попали на какой либо узел, то выбираем его для перемещения.
                sbyte ResultOfFind = RegionOfWhichNode(e.X, e.Y);
                if (ResultOfFind >= 0)
                    mNumberOfMovingNode = ResultOfFind;
            }
        }
        // Процедура, для передвижения узла.
        private void mGrafRegion_MouseMove(object sender, MouseEventArgs e)
        {
            mCursorPosition.Text = String.Format("Позиция курсора - X: {0}, Y: {1}.", e.X, e.Y);
            if (mNumberOfMovingNode >= 0)
            {
                if ((e.Y < mGrafRegion.Height) && (e.Y > 0) && (e.X < mGrafRegion.Width) && (e.X > 0))
                {
                    mMoveNode = true;
                    mGraf[mNumberOfMovingNode].X = e.X - 5;
                    mGraf[mNumberOfMovingNode].Y = e.Y - 5;
                    mGrafRegion.Refresh();
                }
            }
        }
        // Именно эта процедура, предназначена для создания и удаления ребер, новых узлов.
        // Реагирует процедура на поднятие кнопки мыши.
        private void mGrafRegion_MouseUp(object sender, MouseEventArgs e)
        {
            // Определяем попали мы на какой-либо узел, или нет.
            sbyte ResultOfFind = RegionOfWhichNode(e.X, e.Y);
            // Если до этого передвигалась вершина, то просто прекращаем движение узла.
            if (mMoveNode)
            {
                mMoveNode = false;
            }
            else
            {
                // Если же попали на какой-то узел...
                if (ResultOfFind >= 0)
                {
                    // Если до этого уже был выбран какой-либо узел.
                    // То добавляем либо удаляем ребро, и добавляем действие в "Отмену".
                    if (mNumberOfChosenNode >= 0)
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            mGraf[mNumberOfChosenNode].NodeColor = Color.LightGray;
                            mNumberOfChosenNode = ResultOfFind;
                            mGraf[mNumberOfChosenNode].NodeColor = Color.LightGreen;
                        }
                        else
                        {
                            if (e.Button == MouseButtons.Right)
                            {
                                if ((mMatrix[mNumberOfChosenNode, ResultOfFind] == 1))
                                {
                                    mMatrix[mNumberOfChosenNode, ResultOfFind] = 0;
                                    if (NEORIENT) mMatrix[ResultOfFind, mNumberOfChosenNode] = 0;
                                }
                                else
                                {
                                    if (mNumberOfChosenNode != ResultOfFind)
                                    {
                                        mMatrix[mNumberOfChosenNode, ResultOfFind] = 1;
                                        if (NEORIENT) mMatrix[ResultOfFind, mNumberOfChosenNode] = 1;
                                    }
                                    else
                                    {
                                        mMatrix[ResultOfFind, ResultOfFind] = 1;
                                    }
                                }
                                AddToCancel(mNumberOfChosenNode, ResultOfFind);
                            }
                        }
                    }
                    else // Если до этого не было выбрано узла
                    {    // То выбираем узел.
                        if (e.Button == MouseButtons.Left)
                        {
                            mNumberOfChosenNode = ResultOfFind;
                            mGraf[mNumberOfChosenNode].NodeColor = Color.LightGreen;
                        }
                    }
                }
                else // Если попали на пустое место...
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (mGraf.Count < 126) // Если уже слишком много точек ( узлов ), то выведется предупреждение.
                        {
                            // Если слишком близко к границам, то выведется предупреждение.
                            if ((e.Y < mGrafRegion.Height - 20) && (e.X < mGrafRegion.Width - 20) && (e.X > 20) && (e.Y > 20))
                            {
                                // Если все впорядке, то начинаем создание нового узла
                                sbyte c_i = 0;
                                // Перебираем массив узлов, если цикл дошел до конца,
                                // то просто создаем новый узел.
                                for (; c_i < mGraf.Count; c_i++) if (mGraf[c_i].X == 0) break;
                                if (c_i == mGraf.Count)
                                {
                                    mGraf.Add(new mNode(e.X, e.Y));
                                    mMatrix.AddLineAndCollumn();
                                }
                                // если найдем такой, который был "удален", то есть убран из
                                // области видимости, то новую вершину( узел ), создаем с номером
                                // удаленной вершины.
                                else mGraf[c_i] = new mNode(e.X, e.Y);
                                AddToCancel(c_i, -1);
                                // Если до этого был выбран какой-либо узел, то снимаем с него выделение.
                                if ((mNumberOfChosenNode >= 0) && (mNumberOfChosenNode < mGraf.Count))
                                {
                                    mGraf[mNumberOfChosenNode].NodeColor = Color.LightGray;
                                    mNumberOfChosenNode = -1;
                                }
                            }
                            else labelResult.Text = "Ну не надо так близко к границам.";
                        }
                        else labelResult.Text = "Многовато точек будет.";
                    }
                    else
                    {
                        if (e.Button == MouseButtons.Right)
                        {
                            if (mNumberOfChosenNode >= 0)
                            {
                                mGraf[mNumberOfChosenNode].NodeColor = Color.LightGray;
                            }
                            mNumberOfChosenNode = -1;
                        }
                    }
                }
                mGrafRegion.Refresh();
            }
            mNumberOfMovingNode = -1;
        }
        // Метод, определяет, по координатами, попали ли мы на какой либо узел при клике мышью.
        private sbyte RegionOfWhichNode(int xCoord, int yCoord)
        {
            sbyte numberOfNode = 0;
            foreach (mNode mn in mGraf)
            {
                if ((xCoord < mn.X + 25) && (mn.X - 25 < xCoord) && (yCoord < mn.Y + 25) && (mn.Y - 25 < yCoord)) return numberOfNode;
                numberOfNode++;
            }
            return -1;
        }
        // Процедура, которая отрисовывает все узлы, ребра.
        private void mGrafRegion_Paint(object sender, PaintEventArgs e)
        {            
            // Сначала отрисовываем все петли, то есть ребра, у которых начало и конец совпадают.
            for (int i = 0; i < mMatrix.Capacity; i++)
                if (mMatrix[i, i] == 1) e.Graphics.DrawEllipse(new Pen(new SolidBrush(Color.Crimson), 2), mGraf[i].X, mGraf[i].Y, 50, 50);
            // Затем, если необходимо, очищаем значения цветов, уровней.
            if (mNecessaryResetNode == true)
                foreach (mNode MN in mGraf)
                {
                    MN.Level = -1;
                    MN.NodeColor = Color.LightGray;
                    mNecessaryResetNode = false;
                }
            // Отрисовываем все остальные ребра.
            for (int c_numberLine = 0; c_numberLine < mMatrix.Capacity; c_numberLine++)
                for (int c_numberRow = 0; c_numberRow < c_numberLine; c_numberRow++)
                    if (mMatrix[c_numberLine, c_numberRow] == 1)
                        e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Crimson), 2), mGraf[c_numberLine].X + 7, mGraf[c_numberLine].Y + 7, mGraf[c_numberRow].X + 7, mGraf[c_numberRow].Y + 7);
            // Если граф неориентированный, то должны отображаться стрелки, указывающие направление ребра.
            if (!NEORIENT)
            {
                for (int c_numberLine = 0; c_numberLine < mMatrix.Capacity; c_numberLine++)
                    for (int c_numberRow = 0; c_numberRow < mMatrix.Capacity; c_numberRow++)
                        if ((mMatrix[c_numberLine, c_numberRow] == 1))
                        {
                            int I_X = mGraf[c_numberLine].X, J_X = mGraf[c_numberRow].X;
                            int I_Y = mGraf[c_numberLine].Y, J_Y = mGraf[c_numberRow].Y;
                            e.Graphics.DrawLine(mOrientPen, I_X + 7, I_Y + 7, J_X + 7, J_Y + 7);
                        }
            }
            while (mSaveWay.Count > 1)
            {
                sbyte i_end = mSaveWay.Pop();
                sbyte i_start = mSaveWay.Peek();
                int i_end_X = mGraf[i_end].X, i_start_X = mGraf[i_start].X;
                int i_end_Y = mGraf[i_end].Y, i_start_Y = mGraf[i_start].Y;
                mOrientPen.Color = Color.Black;
                e.Graphics.DrawLine(mOrientPen, i_start_X + 7, i_start_Y + 7, i_end_X + 7, i_end_Y + 7);
                mOrientPen.Color = Color.Crimson;
            }
            // Уже затем отрисовываем все узлы.
            // Эта переменная дает понять методу, который отрисовывает узел, с каким номером его отображать.
            int i_chars = 0;
            foreach (mNode mn in mGraf)
            {
                mn.Draw(sender, e, i_chars);
                i_chars++;
            }
        }
        // Процедура отображает матрицу смежности.
        private void butMatrix(object sender, EventArgs e)
        {
            if ((mGraf.Capacity - NumberDeletedNode()) > 0) MessageBox.Show(mMatrix.ToString());
            else labelResult.Text = "Негоже работать с пустым графом!";
        }
        // Метод, возвращает массив всех цветов, каждого узла, с которыми связан узел под номером 'p_index'
        private List<Color> AroundColors(sbyte p_index)
        {
            List<Color> i_aroundColor = new List<Color>();
            for (int c_numberRow = 0; c_numberRow < mMatrix.Capacity; c_numberRow++)
            {
                if (mMatrix[p_index, c_numberRow] == 1) i_aroundColor.Add(mGraf[c_numberRow].NodeColor);
            }
            return i_aroundColor;
        }
        // Процедура, находит хроматическое число графа, и определяет цвет каждого узла.
        // Хроматическое число, это минимальное число красок, в которое можно окрасить граф.
        // Цветом узла, является некоторый цвет, причем у всех смежных узлов, цвет должен быть другим.
        // То есть у любых смежных вершин цвет должен быть разный.
        private void butFill_Click(object sender, EventArgs e)
        {
            if ((mGraf.Capacity - NumberDeletedNode()) > 0)
            {
                byte HromaticNumber = 0;
                foreach (mNode mn in mGraf)
                {
                    mn.NodeColor = Color.AliceBlue;
                }
                bool c_nonBreakCycle;
                for (byte c_numberLine = 0; c_numberLine < mMatrix.Capacity; c_numberLine++)
                {
                    uint c_startColor = 6202500;
                    uint c_intColor = 10000;
                    Color i_selectColor = Color.LightGray;
                    List<Color> i_aroundColor = AroundColors((sbyte)c_numberLine);
                    for (byte c_numberRow = 1; c_numberRow <= mMatrix.Capacity; c_numberRow++)
                    {
                        c_nonBreakCycle = true;
                        if (c_numberRow > HromaticNumber) HromaticNumber = c_numberRow;
                        i_selectColor = ColorFromIntWithoutAlpha((uint)(c_startColor + c_intColor * c_numberRow * Math.Pow(-1, c_numberRow)));
                        for (byte q = 0; q < i_aroundColor.Count; q++)
                        {
                            if (i_selectColor == i_aroundColor[q]) c_nonBreakCycle = false;
                        }
                        if (c_nonBreakCycle) break;
                    }
                    mGraf[c_numberLine].NodeColor = i_selectColor;
                }
                mGrafRegion.Refresh();
                labelResult.Text = String.Format("Хроматическое число графа = {0}", HromaticNumber.ToString());
                mNecessaryResetNode = true;
            }
            else labelResult.Text = "Негоже работать с пустым графом!";
        }
        // Процедура, определяет уровни вершин, относительно некоторой.
        // Уровень вершины 'M' относительно 'N', это минимальное число шагов, за которое можно достичь вершину 'M'        
        private void butLevelNode_Click(object sender, EventArgs e)
        {
            if ((mGraf.Capacity - NumberDeletedNode()) > 0)
            {
                if (mNecessaryResetNode)
                {
                    mNecessaryResetNode = false;
                    foreach (mNode MN in mGraf)
                        MN.Level = -1;
                }
                ChoiceNode NumberForm = new ChoiceNode((sbyte)mGraf.Count);
                NumberForm.ShowDialog();
                LevelAtNode((sbyte)(NumberForm.numericUpDown1.Value - 1));

            }
            else labelResult.Text = "Негоже работать с пустым графом!";
        }
        private void LevelAtNode(sbyte p_numberNode)
        {
            sbyte i_numberNode = p_numberNode;
            mGraf[p_numberNode].Level = 0;
            Queue<sbyte> i_findLink = new Queue<sbyte>();
            i_findLink.Enqueue(p_numberNode);
            while (i_findLink.Count > 0)
            {
                i_numberNode = i_findLink.Dequeue();
                for (sbyte c_numberRow = 0; c_numberRow < mMatrix.Capacity; c_numberRow++)
                {
                    if ((mMatrix[i_numberNode, c_numberRow] == 1) && (i_numberNode != c_numberRow) && (mGraf[i_numberNode].Level >= 0) && (mGraf[c_numberRow].Level < 0))
                    {
                        mGraf[c_numberRow].Level = (sbyte)(mGraf[i_numberNode].Level + 1);
                        i_findLink.Enqueue(c_numberRow);
                    }
                }
            }
            mGrafRegion.Refresh();
            mNecessaryResetNode = true;
        }
        private bool ComponentCoherence()
        {

            Queue<sbyte> i_findLink = new Queue<sbyte>();
            List<sbyte> i_allLink = new List<sbyte>(mGraf.Count);
            sbyte i_numberNode = 0;
            for (sbyte c_i = 0; c_i < mGraf.Count; c_i++)
                if (mGraf[c_i].X > 0)
                {
                    i_numberNode = c_i;
                    break;
                }
            i_findLink.Enqueue(i_numberNode);
            i_allLink.Add(i_numberNode);
            while (i_findLink.Count > 0)
            {
                i_numberNode = i_findLink.Dequeue();
                for (sbyte c_numberRow = 0; c_numberRow < mMatrix.Capacity; c_numberRow++)
                {
                    if ((mMatrix[i_numberNode, c_numberRow] == 1) && (i_numberNode != c_numberRow) && (!i_allLink.Contains(c_numberRow)))
                    {
                        i_findLink.Enqueue(c_numberRow);
                        i_allLink.Add(c_numberRow);
                    }
                }
            }
            sbyte i_numberDeleted = 0;
            foreach (mNode MN in mGraf)
                if (MN.X == 0) i_numberDeleted++;
            if ((i_allLink.Count + i_numberDeleted) == mMatrix.Capacity) return true;
            return false;
        }
        // Метод, возвращает матрицу связности ( смежности ) графа.
        // В матрице смежности элемент в строке 'i' и 'j'-ом столбце, равен единице, тогда
        // и только тогда, когда с помощью маршрута любой длины, можно достичь из 'i'-ого узла
        // 'j'-ый.
        private mMatrix2D<sbyte> ComponentCoherenceMatrix()
        {
            // Матрица, в которую будет записываться смежность.
            mMatrix2D<sbyte> i_resultMatrix = new mMatrix2D<sbyte>(mMatrix.Capacity);
            // Стэк, сюда будут заноситься все номера узлов, с которыми связаны ближайшие узлы.
            Stack<sbyte> i_findLink = new Stack<sbyte>();
            // Стэк, сюда будут заноситься все номера узлов, с которыми вообще связен узел.
            Stack<sbyte> i_resultLink = new Stack<sbyte>();
            sbyte c_numberNode;
            List<sbyte> i_numberDeletedNode = new List<sbyte>();
            for (sbyte c_i = 0; c_i < mGraf.Count; c_i++)
                if (mGraf[c_i].X < 1) i_numberDeletedNode.Add(c_i); 
            for (sbyte c_numberRepeat = 0; c_numberRepeat < mMatrix.Capacity; c_numberRepeat++)
            {
                if (i_numberDeletedNode.Contains(c_numberRepeat)) c_numberRepeat++;
                i_resultLink.Push(c_numberRepeat);
                i_findLink.Push(c_numberRepeat);
                while (i_findLink.Count > 0)
                {
                    c_numberNode = i_findLink.Pop();
                    for (sbyte c_numberRow = 0; c_numberRow < mMatrix.Capacity; c_numberRow++)
                    {
                        if ((mMatrix[c_numberNode, c_numberRow] == 1) && (c_numberNode != c_numberRow) && (!i_resultLink.Contains(c_numberRow)))
                        {
                            i_findLink.Push(c_numberRow);
                            i_resultLink.Push(c_numberRow);
                        }
                    }
                }
                while (i_resultLink.Count > 0)
                {
                    i_resultMatrix[c_numberRepeat, i_resultLink.Pop()] = 1;
                }
            }
            return i_resultMatrix;
        }
        private void butCompCoh_Click(object sender, EventArgs e)
        {
            if (ComponentCoherence()) labelResult.Text = "Граф связен";
            else labelResult.Text = "Граф не связен";
        }
        // Процедура, определяющая планарен ли граф.
        // Пусть 'N' - число узлов в графе, 'M' - число ребер, 
        // тогда граф планарен тогда и только тогда, когда:
        // M <= 3 * N - 6 
        private void butPlanar_Click(object sender, EventArgs e)
        {
            int NumberOfRib = 0;
            for (int i = 1; i < mMatrix.Capacity; i++)
                for (int j = 0; j < i; j++)
                    if (mMatrix[i, j] == 1) NumberOfRib++;
            if (NumberOfRib <= (3 * mGraf.Count - 6)) labelResult.Text = "Граф планарен.";
            else labelResult.Text = "Граф НЕ планарен.";
        }
        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void новыйГрафToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        // Процедура, создает обратный граф текущему.
        private void mnuFindReverse_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < mMatrix.Capacity; i++)
                for (int j = 0; j < mMatrix.Capacity; j++)
                {
                    if (mGraf[i].X > 0)
                    {
                        if (mMatrix[i, j] == 0) mMatrix[i, j] = 1;
                        else mMatrix[i, j] = 0;
                    }
                }
            mGrafRegion.Refresh();
        }
        // Процедура, отмена последнего действия.
        // Для экономии памяти, в отмену не заносятся такие действия как:
        // перемещение вершины, получение обратного графа.
        // Для определения нужного действия, используются пары значений:
        // Пусть 'N' и 'M' целочисленные значения больше либо равные нулю, тогда имеют место следующие обозначения в парах:
        // (N,M) - была изменена дуга от узла 'N' к 'M'
        // (-1,N) - была удалены вершина
        // (N,-1) - была создана новая вершина 
        private void mnuPaintCancel_Click(object sender, EventArgs e)
        {
            if (mCancelingQueue.Count > 0)
            {
                // Чтобы не возникло проблемы с тем что используются ссылочные типы, то создаем новую "пару".
                KeyValuePair<sbyte, sbyte> KVP = new KeyValuePair<sbyte, sbyte>(mCancelingQueue[mCancelingQueue.Count - 1].Key, mCancelingQueue[mCancelingQueue.Count - 1].Value);
                mCancelingQueue.RemoveAt(mCancelingQueue.Count - 1);
                if (KVP.Key >= 0)
                {
                    if (KVP.Value == -1)
                    {
                        DeleteNode(KVP.Key, false);
                    }
                    else
                    {
                        // Меняем значение наличия ребра на обратное.
                        if (mMatrix[KVP.Key, KVP.Value] == 0)
                        {
                            mMatrix[KVP.Key, KVP.Value] = 1;
                            if (NEORIENT) mMatrix[KVP.Value, KVP.Key] = 1;
                        }
                        else
                        {
                            mMatrix[KVP.Key, KVP.Value] = 0;
                            if (NEORIENT) mMatrix[KVP.Value, KVP.Key] = 0;
                        }
                    }
                }
                else
                {
                    Random i_random = new Random();
                    if (KVP.Value > mGraf.Count - 1)
                    {
                        // Последний удаленный узел был последним в массиве узлов. 
                        // Значит просто добавляем новый узел в конец.
                        mGraf.Add(new mNode(i_random.Next(10, 100), i_random.Next(10, 100)));
                        mMatrix.AddLineAndCollumn();
                    }
                    else
                    {
                        // Последний удаленный узел был НЕ последним в массиве узлов.
                        mGraf[KVP.Value] = new mNode(i_random.Next(10, 100), i_random.Next(10, 100)); ;
                    }
                }
                mGrafRegion.Refresh();
            }
        }
        // Процедура, для удаления выбранного узла.
        private void mnuPaintDelete_Click(object sender, EventArgs e)
        {
            if ((mGraf.Count - NumberDeletedNode()) > 0)
            {
                // Создаем форму, что выбрать, какой узел удалять.
                ChoiceNode i_numberForm = new ChoiceNode((sbyte)mGraf.Count);
                i_numberForm.ShowDialog();
                DeleteNode((sbyte)(i_numberForm.numericUpDown1.Value - 1), true);
                mGrafRegion.Refresh();
            }
            else labelResult.Text = "Негоже работать с пустым графом!";
        }
        private void DeleteNode(sbyte p_numberNode, bool p_addToCancel)
        {
            if (((mGraf.Count - NumberDeletedNode() ) > 0) && (p_numberNode < mGraf.Count))
            {
                    // И все связи с другими узлами убирать.
                    for (byte с_numberOfNode = 0; с_numberOfNode < mMatrix.Capacity; с_numberOfNode++)
                    {
                        if (NEORIENT)
                        {
                            if (mMatrix[p_numberNode, с_numberOfNode] == 1)
                            {
                                AddToCancel(p_numberNode, (sbyte)с_numberOfNode);
                                mMatrix[p_numberNode, с_numberOfNode] = 0;
                                mMatrix[с_numberOfNode, p_numberNode] = 0;
                            }
                        }
                        else
                        {
                            if (mMatrix[p_numberNode, с_numberOfNode] == 1)
                            {
                                AddToCancel(p_numberNode, (sbyte)с_numberOfNode);
                                mMatrix[p_numberNode, с_numberOfNode] = 0;
                            }
                            if (mMatrix[с_numberOfNode, p_numberNode] == 1)
                            {
                                AddToCancel((sbyte)с_numberOfNode, p_numberNode);
                                mMatrix[с_numberOfNode, p_numberNode] = 0;
                            }
                        }
                    }
                    // Если это не последний в массиве узел...
                    if (p_numberNode != mGraf.Count - 1)
                    {
                        // Дабы сэкономить память, будем не непосредственно удалять узел, а сдвигать его за 
                        // границу видимости.
                        mGraf[p_numberNode].X = 0;
                    }
                    else
                    {
                        mGraf.RemoveAt(mGraf.Count - 1);
                        mMatrix.DeleteLastLineAndCollumn();
                    }
                // В "Отмену" добавляем пару значений.
                if (p_addToCancel) AddToCancel(-1, p_numberNode);
            }
        }
        // Процедура, будет добавлять в "Массив отмены", специальную пару значений.
        private void AddToCancel(sbyte value1, sbyte value2)
        {
            // Для ускорения работы, и чтобы каждый раз не убирать из начала 1 пару значений, то 
            // при достижении границы массива, будет удаляться половина пар значений.
            if (mCancelingQueue.Count > 50) mCancelingQueue.RemoveRange(0, 25);
            mCancelingQueue.Add(new KeyValuePair<sbyte, sbyte>(value1, value2));
        }
        // Процедура, для поиска остова графа, корнем будет первый "неудаленный" узел.
        private void mnuFindOstov_Click(object sender, EventArgs e)
        {
            if ((mGraf.Count - NumberDeletedNode()) > 0)
            {
                if (ComponentCoherence())
                {
                    foreach (mNode MN in mGraf)
                    {
                        MN.Viewed = false;
                    }
                    sbyte c_numberNode = 0;
                    for (c_numberNode = 0; c_numberNode < mGraf.Count; c_numberNode++)
                        if (mGraf[c_numberNode].X > 0) break;
                    mMatrix2D<sbyte> innerMatrix = ConstructTree(c_numberNode);
                    ShowGraf SG = new ShowGraf(this.Height, this.Width, mGraf.ToList<mNode>(), innerMatrix);
                    SG.ShowDialog();
                }
                else labelResult.Text = "Граф не связен!";
            }
            else labelResult.Text = "Негоже работать с пустым графом.";
        }
        // Метод для построения дерева ( остова графа ), где корнем будет узел под номером 'p_NumberNode'.
        // Возвращает метод матрицу смежности, для дерева.
        private mMatrix2D<sbyte> ConstructTree(sbyte p_NumberNode)
        {
            // Для цикла, необходимо чтобы корень сразу стал считаться "просмотренным".
            mGraf[p_NumberNode].Viewed = true;
            // Итоговая матрица связности.
            mMatrix2D<sbyte> i_resultMatrix = new mMatrix2D<sbyte>(mMatrix.Capacity);
            // В этот стэк будут "пихаться" номера узлов, с которыми найдена связь.            
            Stack<sbyte> c_findsLink = new Stack<sbyte>();
            c_findsLink.Push(p_NumberNode);
            // В этой переменной будет храниться номер текущего просматриваемого узла.
            sbyte c_numberOfNode;
            // Цикл идет пока стэк не пустой, пустой он станет тогда,
            // когда больше не удастся найти связи между узлами.
            while (c_findsLink.Count > 0)
            {
                // Берем из стэка номер узла.
                c_numberOfNode = c_findsLink.Pop();
                for (sbyte c_numberColumns = 0; c_numberColumns < mMatrix.Capacity; c_numberColumns++)
                {
                    // Чтобы связь считалась подходящей для дерева, необходимо чтобы она была между просмотренным узлом
                    // и не просмотренным, если это так, ток эта связь ( ребро ) подходит.
                    if ((mMatrix[c_numberOfNode, c_numberColumns] == 1) && (c_numberOfNode != c_numberColumns) && (mGraf[c_numberOfNode].Viewed == true) && (mGraf[c_numberColumns].Viewed == false))
                    {
                        mGraf[c_numberColumns].Viewed = true;
                        // Помещаем в стэк номер узла, чтобы потом проверить с какими узлами связан он.
                        c_findsLink.Push(c_numberColumns);
                        i_resultMatrix[c_numberOfNode, c_numberColumns] = 1;
                        i_resultMatrix[c_numberColumns, c_numberOfNode] = 1;
                    }
                }
            }
            return i_resultMatrix.Clone();
        }
        // Процедура, в которой будет происходить поиск радиуса и диаметра графа.
        private void mnuFindRadius_Click(object sender, EventArgs e)
        {
            if ((mGraf.Count > 0) && ComponentCoherence())
            {
                List<sbyte> i_maximumEx = new List<sbyte>(mGraf.Count);
                sbyte i_maximumLevel;
                Queue<sbyte> i_findLink = new Queue<sbyte>();
                sbyte i_numberNode;
                for (sbyte c_numOfNode = 0; c_numOfNode < mGraf.Count; c_numOfNode++)
                {
                    i_maximumLevel = 0;
                    foreach (mNode MN in mGraf) MN.Level = -1;
                    mGraf[c_numOfNode].Level = 0;
                    i_findLink.Enqueue(c_numOfNode);
                    while (i_findLink.Count > 0)
                    {
                        i_numberNode = i_findLink.Dequeue();
                        for (sbyte c_numberRow = 0; c_numberRow < mMatrix.Capacity; c_numberRow++)
                        {
                            if ((mMatrix[i_numberNode, c_numberRow] == 1) && (i_numberNode != c_numberRow) && (mGraf[i_numberNode].Level >= 0) && (mGraf[c_numberRow].Level < 0))
                            {
                                mGraf[c_numberRow].Level = (sbyte)(mGraf[i_numberNode].Level + 1);
                                i_findLink.Enqueue(c_numberRow);
                                if (mGraf[c_numberRow].Level > i_maximumLevel) i_maximumLevel = mGraf[c_numberRow].Level;
                            }
                        }
                    }
                    i_maximumEx.Add(i_maximumLevel);
                }
                foreach (mNode MN in mGraf) MN.Level = -1;
                labelResult.Text = string.Format("Диаметр графа = '{0}', радиус = {1}.", i_maximumEx.Max<sbyte>(), i_maximumEx.Min<sbyte>());
                mNecessaryResetNode = true;
            }
            else labelResult.Text = "С таким графом нельзя работать.";
        }
        // Процедура, в которой будет проходить поиск эксцентриситета выбранного узла.
        // Эксцентриситет - минимальное количество шагов, необходимое чтобы дойти от одного узла,
        // до любого другого в графе.
        private void mnuFindEx_Click(object sender, EventArgs e)
        {
            if (ComponentCoherence())
            {
                if (mNecessaryResetNode)
                    foreach (mNode MN in mGraf)
                        MN.Level = -1;
                // Создаем новую форму, для выбора номера узла
                ChoiceNode i_numberForm = new ChoiceNode((sbyte)mGraf.Count);
                i_numberForm.ShowDialog();
                sbyte i_numberOfNode = (sbyte)(i_numberForm.numericUpDown1.Value - 1);
                sbyte i_maximumLevel = 0;
                mGraf[i_numberOfNode].Level = 0;
                for (byte c_numOfRepeat = 0; c_numOfRepeat <= i_numberOfNode; c_numOfRepeat++)
                {
                    for (byte c_numberOfLine = 0; c_numberOfLine < mMatrix.Capacity; c_numberOfLine++)
                    {
                        for (byte c_numberOfRow = 0; c_numberOfRow < mMatrix.Capacity; c_numberOfRow++)
                        {
                            if ((mMatrix[c_numberOfLine, c_numberOfRow] == 1) && (mGraf[c_numberOfLine].Level >= 0) && (mGraf[c_numberOfRow].Level < 0) && (c_numberOfLine != c_numberOfRow))
                            {
                                mGraf[c_numberOfRow].Level = (sbyte)(mGraf[c_numberOfLine].Level + 1);
                                if (mGraf[c_numberOfRow].Level > i_maximumLevel) i_maximumLevel = mGraf[c_numberOfRow].Level;
                            }
                        }
                    }
                }
                labelResult.Text =  String.Format("Экцсцентритет вершины '{0}' = {1}.", (i_numberOfNode + 1).ToString(), i_maximumLevel.ToString());
                mNecessaryResetNode = true;
            }
            else labelResult.Text = "С таким графом нельзя работать.";
        }
        private sbyte NumberDeletedNode()
        {
            sbyte i_result = 0;
            for (sbyte c_i = 0; c_i < mGraf.Count; c_i++)
                if (mGraf[c_i].X == 0) i_result++;
            return i_result;
        }
        // Процедура, в которой будет проходить поиск числа компонент связности графа.
        // Принцип: Пусть 'C' - число компонент связности. 'N' - число вершин в графе. 'M' - число ребер.
        // Тогда для любого "дерева" либо "леса" справедливо  -  'C' = 'N' - 'M'
        private void mnuFindComp_Click(object sender, EventArgs e)
        {

            if (mGraf.Count > 0)
            {
                labelResult.Text = String.Format("В графе {0} компонент(ы) связности.", ComponentCoherenceNumber(false).ToString());
            }
            else labelResult.Text = "Негоже работать с пустым графом!";
        }
        // Метод, определяющий число компонент связности.
        // Параметр определяет, учитываем мы отдельно стоящие точки или нет.
        // True - не учитываем, False - учитываем.
        private sbyte ComponentCoherenceNumber(bool p_withoutSeparatedNode)
        {
            foreach (mNode MN in mGraf)
                MN.Viewed = false;
            // Число всех вершин графа, не связных с любым другим узлом.
            int i_valueUnlinkedOrDeletedNode = 0;
            // Число всех вершин в графе.
            sbyte i_numberNode = 0;
            // Находим первую "неудаленную" вершину.
            for (i_numberNode = 0; i_numberNode < mGraf.Count; i_numberNode++)
                if (mGraf[i_numberNode].X > 0) break;
            if (p_withoutSeparatedNode)
            {
                for (sbyte c_numberLine = 0; c_numberLine < mMatrix.Capacity; c_numberLine++)
                {
                    sbyte i_summRib = 0;
                    for (sbyte c_numberRow = 0; c_numberRow < mMatrix.Capacity; c_numberRow++)
                        if ((mMatrix[c_numberLine, c_numberRow] == 1) && (c_numberRow != c_numberLine)) i_summRib++;
                    if (i_summRib == 0)
                    {
                        mGraf[c_numberLine].Viewed = true;
                        i_valueUnlinkedOrDeletedNode++;
                    }
                }
            }
            // Все удаленный вершины расцениваем как уже просмотренные.
            for (sbyte c_numberNode = 0; c_numberNode < mGraf.Count; c_numberNode++)
                if (mGraf[c_numberNode].X == 0) mGraf[c_numberNode].Viewed = true;
            while (true)
            {
                mMatrix2D<sbyte> c_innerMatrix = ConstructTree(i_numberNode);
                // Подсчитываем число всех ребер.
                for (int c_numberOfLine = 1; c_numberOfLine < c_innerMatrix.Capacity; c_numberOfLine++)
                    for (int c_numberOfRow = 0; c_numberOfRow < c_numberOfLine; c_numberOfRow++)
                        i_valueUnlinkedOrDeletedNode += c_innerMatrix[c_numberOfLine, c_numberOfRow];
                sbyte c_numberViewedNode;
                // Если будет найден хоть один узел, который будет не просмотренным, то построить дерево,
                // где корнем будет этот узел.
                for (c_numberViewedNode = 0; c_numberViewedNode < mGraf.Count; c_numberViewedNode++)
                    if (!mGraf[c_numberViewedNode].Viewed)
                    {
                        i_numberNode = c_numberViewedNode;
                        break;
                    }
                if ((c_numberViewedNode) == mGraf.Count) break;
                mNecessaryResetNode = true;
            }
            sbyte i_numberDeleted = 0;
            for (sbyte c_i = 0; c_i < mGraf.Count; c_i++)
                if (mGraf[c_i].X == 0) i_numberDeleted++;
            return (sbyte)(Math.Abs(mGraf.Count - i_numberDeleted - i_valueUnlinkedOrDeletedNode));
        }
        private void mnuFindMatrixLink_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ComponentCoherenceMatrix().ToString());
        }

        private void mnuFindMatrixContrLink_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ComponentCoherenceMatrix().Transpose().ToString());
        }
        // Процедура, определяющая является ли граф деревом.
        // Работает по двум принципам:
        // 1) В дереве, число ребер равно числу узлов минус 1.
        // 2) В дереве все вершины связаны.
        private void mnuTestTree_Click(object sender, EventArgs e)
        {
            int i_allLink = 0;
            int i_allValues = 0;
            mMatrix2D<sbyte> i_innerMatrix = ComponentCoherenceMatrix();
            for (sbyte c_numberLine = 1; c_numberLine < i_innerMatrix.Capacity; c_numberLine++)
                for (sbyte c_numberRow = 0; c_numberRow < c_numberLine; c_numberRow++)
                {
                    if (i_innerMatrix[c_numberLine, c_numberRow] == 1) i_allLink++;
                    i_allValues++;
                }
            int i_allRibs = 0;
            for (sbyte c_numberLine = 0; c_numberLine < mMatrix.Capacity; c_numberLine++)
                for (sbyte c_numberRow = 0; c_numberRow <= c_numberLine; c_numberRow++)
                    if (mMatrix[c_numberLine, c_numberRow] == 1) i_allRibs++;
            if ((i_allRibs == mMatrix.Capacity - 1) && (i_allLink == i_allValues)) labelResult.Text = "Граф является деревом.";
            else labelResult.Text = "Граф не является деревом.";
        }
        // Метод, преобразования числа в диапозоне от 0 до 16581375 в цвет.
        private Color ColorFromIntWithoutAlpha(UInt32 p_RGB)
        {
            byte R = (byte)(p_RGB / 65025);
            byte G = (byte)((p_RGB % 65025) / 255);
            byte B = (byte)((p_RGB % 65025) % 255);
            Color i_color = new Color();
            i_color = Color.FromArgb(R, G, B);
            return i_color;
        }
        private void mnuTestEiler_Click(object sender, EventArgs e)
        {
            if ((mMatrix.Capacity > 0))
            {
                if (ComponentCoherence() && EvenDegreeOfNode())
                {                    
                    Stack<sbyte> i_resultWay = new Stack<sbyte>();
                    mMatrix2D<sbyte> i_innerMatrix = mMatrix.Clone();
                    int i_summRib = 0;
                    for (sbyte c_numberLine = 1; c_numberLine < mMatrix.Capacity; c_numberLine++)
                        for (sbyte c_numberRow = 0; c_numberRow < c_numberLine; c_numberRow++)
                            if ((mMatrix[c_numberLine, c_numberRow] == 1) && (c_numberRow != c_numberLine)) i_summRib++;
                    i_resultWay.Push(0);
                    sbyte c_numberOfNode;
                    while (i_summRib > 0)
                    {
                        c_numberOfNode = i_resultWay.Peek();
                        for (sbyte c_numberRow = 0; c_numberRow < mMatrix.Capacity; c_numberRow++)
                        {
                            if ((mMatrix[c_numberOfNode, c_numberRow] == 1) &&
                                (c_numberOfNode != c_numberRow) &&
                                (((c_numberRow != 0) && (i_summRib > 1))
                                || ((c_numberRow == 0) && (i_summRib == 1))
                                || ((c_numberRow == 0) && (AroundColors(0).Count > 1))) &&
                                (!BridgeRib(c_numberOfNode, c_numberRow, true) && (i_summRib > 1)
                                || (BridgeRib(c_numberOfNode, c_numberRow, true) && (i_summRib == 1))))
                            {
                                mMatrix[c_numberOfNode, c_numberRow] = 0;
                                mMatrix[c_numberRow, c_numberOfNode] = 0;
                                i_resultWay.Push(c_numberRow);
                                i_summRib--;
                                break;
                            }
                        }
                    }
                    System.Text.StringBuilder i_result = new System.Text.StringBuilder(i_resultWay.Count*4+23);
                    i_result.AppendLine("Граф имеет Эйлеров цикл:");
                    mSaveWay = i_resultWay;
                    foreach (sbyte c_number in i_resultWay)
                    {
                        i_result.Append(String.Format("{0} <- ", (c_number + 1).ToString()));
                    }
                    i_result.Remove(i_result.Length - 4, 3);
                    mMatrix = i_innerMatrix;
                    mGrafRegion.Refresh();
                    labelResult.Text = i_result.ToString();
                }
                else labelResult.Text = "Граф не имеет эйлерова цикла.";
            }
            else labelResult.Text = "Негоже работать с пустым графом.";
        }
        // Проверяет, четная ли степень у всех вершин.
        private bool EvenDegreeOfNode()
        {
            byte SumInLine;
            for (sbyte c_numberLine = 0; c_numberLine < mMatrix.Capacity; c_numberLine++)
            {
                SumInLine = 0;
                for (sbyte c_numberColumn = 0; c_numberColumn < mMatrix.Capacity; c_numberColumn++)
                    if ((mMatrix[c_numberLine, c_numberColumn] == 1) && (c_numberColumn != c_numberLine)) SumInLine++;
                if ((SumInLine % 2) != 0) return false;
            }
            return true;
        }
        private void mnuTestGamilt_Click(object sender, EventArgs e)
        {
            if ((mGraf.Count - NumberDeletedNode()) > 0)
            {
                if (ComponentCoherence())
                {
                    bool i_findGamilt = false;
                    mSaveWay.Clear();
                    Stack<sbyte> i_ways = new Stack<sbyte>();
                    List<sbyte> i_numberDeletedNode = new List<sbyte>();
                    for (sbyte c_i = 0; c_i < mGraf.Count; c_i++)
                        if (mGraf[c_i].X < 1) i_numberDeletedNode.Add(c_i); 
                    foreach (mNode MN in mGraf) MN.Viewed = false;
                    for (sbyte c_numberNode = 0; c_numberNode < mGraf.Count; c_numberNode++)
                    {
                        if (mGraf[c_numberNode].X > 0)
                        {
                            mGraf[c_numberNode].Viewed = true;
                            i_ways.Push(c_numberNode);
                            break;
                        }
                    }
                    List<mMatrix2D<sbyte>> i_savingMatrix = new List<mMatrix2D<sbyte>>();                    
                    i_savingMatrix.Add(mMatrix.Clone()); 
                    for (sbyte c_numberNode = 0; c_numberNode < mMatrix.Capacity; c_numberNode++)
                        mMatrix[c_numberNode, c_numberNode] = 0;                    
                    sbyte i_numberNode;
                    sbyte i_numberNodeTwo;
                    while (i_ways.Count > 0)
                    {
                        i_savingMatrix.Add(mMatrix.Clone());
                        i_numberNode = i_ways.Peek();
                         for (sbyte c_numberColumn = 0; c_numberColumn < mMatrix.Capacity; c_numberColumn++)
                             if ((mMatrix[i_numberNode, c_numberColumn] == 1) && (!mGraf[c_numberColumn].Viewed || (i_ways.Count == (mGraf.Count - NumberDeletedNode()))))
                             {
                                 i_ways.Push(c_numberColumn);
                                 mGraf[c_numberColumn].Viewed = true;
                                 break;
                             }                       
                        i_numberNodeTwo = i_ways.Peek();
                        for (sbyte c_numberNode = 0; c_numberNode < mMatrix.Capacity; c_numberNode++)
                        {
                            if (i_numberNode != i_numberNodeTwo)
                            {
                                if (c_numberNode != i_numberNodeTwo)
                                    mMatrix[i_numberNode, c_numberNode] = 0;
                                if (c_numberNode != i_numberNode)
                                    mMatrix[c_numberNode, i_numberNodeTwo] = 0;
                            }
                        }               
                        for (sbyte c_numberNode = 0; c_numberNode < mMatrix.Capacity; c_numberNode++)
                        {
                            sbyte c_oneValueInLine = (sbyte)mMatrix.InLineOnlyOneValue(c_numberNode, 1);
                            if (c_oneValueInLine >= 0)
                            {
                                for (sbyte c_numberNodeInner = 0; c_numberNodeInner < mMatrix.Capacity; c_numberNodeInner++)
                                {
                                    if (c_numberNodeInner != c_numberNode)
                                        mMatrix[c_numberNodeInner, c_oneValueInLine] = 0;
                                }
                            }
                            sbyte c_oneValueInColumn = (sbyte)mMatrix.InColumnOnlyOneValue(c_numberNode, 1);
                            if (c_oneValueInColumn >= 0)
                            {
                                for (sbyte c_numberNodeInner = 0; c_numberNodeInner < mMatrix.Capacity; c_numberNodeInner++)
                                {
                                    if (c_numberNodeInner != c_numberNode)
                                        mMatrix[c_oneValueInColumn, c_numberNodeInner] = 0;
                                }
                            }
                        }
                        sbyte i_resultSummInLine = 0, i_resultSummInColumn = 0;
                        sbyte i_resultUnits = 0;
                        for (sbyte c_numberOne = 0; c_numberOne < mMatrix.Capacity; c_numberOne++)
                        {
                            if (i_numberDeletedNode.Contains(c_numberOne)) c_numberOne++;
                            i_resultSummInColumn = 0;
                            i_resultSummInLine = 0;
                            for (sbyte c_numberTwo = 0; c_numberTwo < mMatrix.Capacity; c_numberTwo++)
                            {
                                i_resultSummInColumn += mMatrix[c_numberTwo, c_numberOne];
                                i_resultSummInLine += mMatrix[c_numberOne, c_numberTwo];
                                i_resultUnits += mMatrix[c_numberOne, c_numberTwo];
                            }
                            if ((i_resultSummInLine == 0) || (i_resultSummInColumn == 0)) break;
                        }                    
                        bool i_nonCutGraf = ComponentCoherence();                                          
                        if ((i_resultSummInLine == 0) || (i_resultSummInColumn == 0) || !i_nonCutGraf)
                        {

                            if (i_numberNode != i_numberNodeTwo)
                            {
                                mGraf[i_ways.Pop()].Viewed = false;
                                mMatrix = i_savingMatrix[i_savingMatrix.Count - 1].Clone();
                                mMatrix[i_numberNode, i_numberNodeTwo] = 0;
                                i_savingMatrix.RemoveAt(i_savingMatrix.Count - 1);
                            }
                            else
                            {
                                mMatrix = i_savingMatrix[i_savingMatrix.Count - 2].Clone();
                                mGraf[i_ways.Pop()].Viewed = false;
                                if (i_ways.Count == 0) break;
                                mMatrix[i_ways.Peek(), i_numberNode] = 0;                          
                                i_savingMatrix.RemoveRange(i_savingMatrix.Count - 2, 2);
                            }
 
                        }
                        else
                        {
                            if ((i_resultUnits == ( mMatrix.Capacity  - NumberDeletedNode()) && i_nonCutGraf))
                            {
                                i_findGamilt = true;
                                break;
                            }
                        }
                    } // while (i_ways.Count > 0)
                    if (i_findGamilt)
                    {
                        mSaveWay.Push(i_ways.ElementAt<sbyte>(i_ways.Count - 1));                        
                        while (mSaveWay.Count != (mGraf.Count - NumberDeletedNode()))
                        {
                            for (sbyte c_numberColumn = 0; c_numberColumn < mMatrix.Capacity; c_numberColumn++)
                                if (mMatrix[mSaveWay.Peek(), c_numberColumn] == 1) mSaveWay.Push(c_numberColumn);
                        }
                        mSaveWay.Push(i_ways.ElementAt<sbyte>(i_ways.Count - 1));
                        System.Text.StringBuilder i_result = new System.Text.StringBuilder(mSaveWay.Count * 4 + 23);
                        i_result.AppendLine("Граф имеет Гамильтонов цикл:");
                        foreach (sbyte c_number in mSaveWay)
                        {
                            i_result.Append(String.Format("{0} <- ", (c_number + 1).ToString()));
                        }
                        i_result.Remove(i_result.Length - 4, 3);
                        labelResult.Text = i_result.ToString();                        
                    }
                    else
                    {
                        labelResult.Text = "Гамильтонова цикла в графе нет.";
                    }
                    mMatrix = i_savingMatrix[0].Clone();
                    i_savingMatrix.Clear();
                    mGrafRegion.Refresh();
                    mNecessaryResetNode = true;
                }
                else labelResult.Text = "Граф не связен, невозможно найти Гамильтонов цикл.";
            }
            else labelResult.Text = "Негоже работать с пустым графом.";
        }
        private bool BridgeRib(sbyte p_firstNode, sbyte p_secondNode, bool p_withoutSeparatedNode)
        {
            if (mMatrix[p_firstNode, p_secondNode] == 1)
            {
                sbyte i_firstCompCoh = ComponentCoherenceNumber(p_withoutSeparatedNode);
                mMatrix[p_firstNode, p_secondNode] = 0;
                mMatrix[p_secondNode, p_firstNode] = 0;
                sbyte i_secondCompCoh = ComponentCoherenceNumber(p_withoutSeparatedNode);
                mMatrix[p_firstNode, p_secondNode] = 1;
                mMatrix[p_secondNode, p_firstNode] = 1;
                if (i_firstCompCoh != i_secondCompCoh) return true;
                return false;
            }
            else return false;
        }
        private void mnuFindBridgeRib_Click(object sender, EventArgs e)
        {
            if ((mGraf.Count - NumberDeletedNode()) > 0)
            {
                ChoiceNode CN = new ChoiceNode("Выберите номера вершин, между\nкоторыми проверить ребро\nявляется ли оно мостом.", (sbyte)mGraf.Count);
                CN.ShowDialog();
                if (BridgeRib((sbyte)(CN.numericUpDown1.Value - 1), (sbyte)(CN.numericUpDown2.Value - 1), false)) MessageBox.Show("Да, это ребро является мостом.");
                else labelResult.Text = "Нет, это ребро не является мостом.";
            }
            else labelResult.Text = "Негоже работать с пустым графом.";
        }
        // Метод определяющий, является ли точка сочленяющей.
        private bool BridgeNode(sbyte p_numberNode)
        {
            // Сохраняем матрицу чтобы потом можно было ее восстановить.
            mMatrix2D<sbyte> i_timesMatrix = mMatrix.Clone();
            // 
            sbyte i_firstComp = ComponentCoherenceNumber(true);
            for (sbyte c_numberLine = 0; c_numberLine < mMatrix.Capacity; c_numberLine++)
                for (sbyte c_numberColumns = 0; c_numberColumns < mMatrix.Capacity; c_numberColumns++)
                {
                    mMatrix[c_numberLine, p_numberNode] = 0;
                    mMatrix[p_numberNode, c_numberLine] = 0;
                }
            int i_x = mGraf[p_numberNode].X;
            mGraf[p_numberNode].X = 0;
            if (i_firstComp != ComponentCoherenceNumber(false))
            {
                mGraf[p_numberNode].X = i_x;
                mMatrix = i_timesMatrix;
                return true;
            }
            mGraf[p_numberNode].X = i_x;
            mMatrix = i_timesMatrix;
            return false;
        }
        private void mnuFindTochkaSoch_Click(object sender, EventArgs e)
        {
            if (mGraf.Count > 0)
            {
                ChoiceNode CN = new ChoiceNode((sbyte)mGraf.Count);
                CN.ShowDialog();
                if (BridgeNode((sbyte)(CN.numericUpDown1.Value - 1))) labelResult.Text = "Да, эта точка является сочленяющей.";
                else labelResult.Text = "Нет, эта точка не сочленяющая.";
            }
            else MessageBox.Show("Негоже работать с пустым графом.");
        }
        private void сохранитьГрафToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult i_dialogResult = saveFileDialog1.ShowDialog();
                if (i_dialogResult == DialogResult.OK)
                {
                    StreamWriter i_streamWriter = new StreamWriter(saveFileDialog1.FileName);
                    i_streamWriter.WriteLine(NEORIENT);
                    i_streamWriter.WriteLine(mMatrix.Capacity);
                    i_streamWriter.WriteLine(mMatrix.ToString());
                    foreach (mNode c_node in mGraf)
                    {
                        i_streamWriter.WriteLine(c_node.X);
                        i_streamWriter.WriteLine(c_node.Y);
                    }
                    i_streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                labelResult.Text = "В процессе произошла ошибка, попробуйте пожалуйста еще раз.";
            }
        }
        private void Reset(bool p_neorient)
        {
            if (p_neorient)
            {
                foreach (Object c_menuItem in mnuFind.DropDownItems)
                {
                    if (c_menuItem.GetType() == typeof(ToolStripMenuItem))
                        ((ToolStripMenuItem)c_menuItem).Enabled = true;
                }
                foreach (Object c_menuItem in mnuTest.DropDownItems)
                {
                    if (c_menuItem.GetType() == typeof(ToolStripMenuItem))
                        ((ToolStripMenuItem)c_menuItem).Enabled = true;
                }
            }
            else
            {
                mnuTestEiler.Enabled = false;
                mnuFindHrom.Enabled = false;
                mnuFindOstov.Enabled = false;
                mnuFindRadius.Enabled = false;                
                mnuTestPlanar.Enabled = false;
                mnuTestTree.Enabled = false;
                mnuFindLevel.Enabled = false;
            }
            this.Focus();
            mNumberOfChosenNode = -1;
        }
        private void mnuFileLoad_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult i_dialogResult = openFileDialog1.ShowDialog();
                if (i_dialogResult == DialogResult.OK)
                {
                    LoadFile(openFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                labelResult.Text = "В процессе произошла ошибка, попробуйте пожалуйста еще раз.";
            }

        }
        public void LoadFile(string p_fileName)
        {
                StreamReader i_streamReader = new StreamReader(p_fileName);
                mGraf.Clear();
                mMatrix.Clear();
                if (i_streamReader.ReadLine() == "True")
                {
                    Reset(true);
                    NEORIENT = true;
                }
                else
                {
                    Reset(false);
                    NEORIENT = false;
                }
                mGraf = new List<mNode>(Int32.Parse(i_streamReader.ReadLine()));
                mMatrix = new mMatrix2D<sbyte>(mGraf.Capacity);
                for (sbyte c_numberLine = 0; c_numberLine < mMatrix.Capacity; c_numberLine++)
                {
                    string i_readString = i_streamReader.ReadLine();
                    for (sbyte c_numberRow = 0; c_numberRow < mMatrix.Capacity; c_numberRow++)
                        mMatrix[c_numberLine, c_numberRow] = SByte.Parse(i_readString[c_numberRow * 2].ToString());

                }
                for (sbyte c_numberNode = 0; c_numberNode < mMatrix.Capacity; c_numberNode++)
                {
                    mGraf.Add(new mNode(Int32.Parse(i_streamReader.ReadLine()), Int32.Parse(i_streamReader.ReadLine())));

                }
                i_streamReader.Dispose();
                mCancelingQueue.Clear();
                mGrafRegion.Refresh();          
        }
        private void mnuFindWay_Click(object sender, EventArgs e)
        {
            if ((mGraf.Count - NumberDeletedNode()) > 0)
            {
                ChoiceNode CN = new ChoiceNode("Выберите две вершины\nмежду которыми надо найти путь.", (sbyte)mGraf.Count);
                CN.ShowDialog();
                if (CN.numericUpDown1.Value != CN.numericUpDown2.Value)
                {
                    mSaveWay = CreateWayFromNtoM((sbyte)(CN.numericUpDown1.Value - 1), (sbyte)(CN.numericUpDown2.Value - 1));
                    sbyte[] i_show = new sbyte[mSaveWay.Count];
                    mSaveWay.CopyTo(i_show, 0);
                    if (mSaveWay.Count > 0)
                    {
                        System.Text.StringBuilder i_result = new System.Text.StringBuilder(20 + mSaveWay.Count * 4);
                        i_result.AppendLine("Путь найден:");
                        i_result.Append(i_show[0] + 1);
                        for (sbyte c_i = 1; c_i < i_show.Length; c_i++)
                            i_result.Append(" <- " + (i_show[c_i] + 1).ToString());
                        labelResult.Text = i_result.ToString();
                        mGrafRegion.Refresh();
                    }
                    else
                    {
                        labelResult.Text = String.Format("Пути между вершинами {0} и {1} нет.", CN.numericUpDown1.Value, CN.numericUpDown2.Value);
                    }
                }
                else
                {
                    labelResult.Text = "Начальная и конечные точки совпадают.";
                }
            }
            else labelResult.Text = "Негоже работать с пустым графом.";
        }
        // Метод, нахождения пути между двумя точками, возвращает Стэк, хранящий последавательно вершины, 
        // через которые проходи маршрут.
        private Stack<sbyte> CreateWayFromNtoM(sbyte p_nodeOne, sbyte p_nodeTwo)
        {
            LevelAtNode(p_nodeOne);
            sbyte i_minLength = mGraf[p_nodeTwo].Level;
            MessageBox.Show("min length = " + i_minLength.ToString());       
            // В этот стэк будут "пихаться" номера вершин, с которыми найдена связь.            
            Stack<sbyte> i_findsLink = new Stack<sbyte>();
            i_findsLink.Push(p_nodeTwo);
            // В этой переменной будет храниться номер текущей просматриваемой вершины.
            sbyte i_numberOfNode;
            sbyte c_numberColumns = 0;
            sbyte c_numberLastNode = 0;
            mMatrix2D<sbyte> i_saveMatrix = mMatrix.Clone();
            while (true)
            {
                i_numberOfNode = i_findsLink.Peek();
                for (c_numberColumns = 0; c_numberColumns < mMatrix.Capacity; c_numberColumns++)
                {
                    if (NEORIENT)
                    {
                        if ((mMatrix[i_numberOfNode, c_numberColumns] == 1) && (i_numberOfNode != c_numberColumns) && (mGraf[c_numberColumns].Level < mGraf[i_numberOfNode].Level))
                        {
                            i_findsLink.Push(c_numberColumns);
                            break;
                        }
                    }
                    else
                    {
                        if ((mMatrix[c_numberColumns, i_numberOfNode] == 1) && (i_numberOfNode != c_numberColumns) && (mGraf[c_numberColumns].Level <= mGraf[i_numberOfNode].Level))
                        {
                            i_findsLink.Push(c_numberColumns);
                            break;
                        }
                    }
                }
                if (c_numberColumns == mMatrix.Capacity)
                {
                    sbyte i_n = i_findsLink.Pop();
                    mMatrix[i_findsLink.Peek(), i_n] = 0;
                }
                if (mGraf[i_findsLink.Peek()].Level == 0) break;
            }
            mMatrix = i_saveMatrix;
            Stack<sbyte> i_returned = new Stack<sbyte>();
            while (i_findsLink.Count > 0)
            {
                i_returned.Push(i_findsLink.Pop());
            }
            return i_returned;
        }
    }
}
