using System.Text;

namespace myGraf
{
    // Класс, для хранения матрицы. 
    public class mMatrix2D<T>
    {
        // Хранит объем массива.
        private int capacity;
        public int Capacity
        {
            get { return capacity; }
        }
        // Поле, хранящее все значения массива.
        private T[,] values;
        public mMatrix2D()
        {
            capacity = 0;
        }
        public mMatrix2D(int capacity)
        {
            this.capacity = capacity;
            values = new T[this.capacity, this.capacity];
        }
        public void AddLineAndCollumn()
        {
            capacity++;
            T[,] timeMassiv = new T[capacity, capacity];
            for (int i = 0; i < capacity - 1; i++)
            {
                for (int j = 0; j < capacity - 1; j++)
                {
                    timeMassiv[i, j] = values[i, j];
                }
            }
            values = timeMassiv;
        }
        public void DeleteLastLineAndCollumn()
        {
            capacity--;
            T[,] timeMassiv = new T[capacity, capacity];
            for (int i = 0; i < capacity; i++)
            {
                for (int j = 0; j < capacity; j++)
                {
                    timeMassiv[i, j] = values[i, j];
                }
            }
            values = timeMassiv;
        }
        // Для оператора [,]
        public T this[int rows, int cols]
        {
            get { return values[rows, cols]; }
            set { values[rows, cols] = value; }
        }
        public override string ToString()
        {
            StringBuilder ReturnString = new StringBuilder(capacity * capacity + capacity + 1);
            for (int i = 0; i < capacity; i++)
            {
                for (int j = 0; j < capacity; j++)
                {
                    ReturnString.AppendFormat("{0} ", values[i, j]);
                }
                if (i < (capacity - 1)) ReturnString.AppendLine();
            }
            return ReturnString.ToString();
        }        
        public mMatrix2D<T> Clone()
        {
            mMatrix2D<T> rets = new mMatrix2D<T>(capacity);
            for (byte i = 0; i < capacity; i++)
                for (byte j = 0; j < capacity; j++)
                    rets[i, j] = values[i, j];
            return rets;
        }
        public void Clear()
        {
            for (int i = 0; i < capacity; i++)
                for (int j = 0; j < capacity; j++)
                    values[i, j] = default(T);
        }
        // Транспонирование матрицы.
        public mMatrix2D<T> Transpose()
        {
            mMatrix2D<T> matrix = new mMatrix2D<T>(capacity);
            for (int i = 0; i < capacity; i++) matrix[i, i] = this[i, i];
            for (int i = 0; i < capacity; i++)
                for (int j = 0; j < i; j++)
                {
                    matrix[i, j] = this[j, i];
                    matrix[j, i] = this[i, j];
                }
            return matrix;
        }
        public int InLineOnlyOneValue(int p_numberLine,T p_value)
        {
            int i_retNumber = -1;
            for (int c_numberColumn = 0; c_numberColumn < capacity; c_numberColumn++)
            {
                if (values[p_numberLine, c_numberColumn].Equals(p_value))
                    if (i_retNumber < 0) i_retNumber = c_numberColumn;
                    else return -1;
            }
            return i_retNumber;
        }
        public int InColumnOnlyOneValue(int p_numberColumn, T p_value)
        {
            int i_retNumber = -1;
            for (int c_numberLine = 0; c_numberLine < capacity; c_numberLine++)
            {                
                if (values[c_numberLine, p_numberColumn].Equals(p_value))
                    if (i_retNumber < 0) i_retNumber = c_numberLine;
                    else return -1;
            }
            return i_retNumber;
        }
    }
}
