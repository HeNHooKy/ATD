using System;

namespace SegmentTree
{

    /*
     * Использование дерева отрезков:
     * - Реализация любых ассоциативных операций (сумма, произведение, поиск минимума или максимума)
     * на заданном ряде чисел за логарифмическое время
     * 
     */
    class SegmentTree
    {
        int N;
        int Lenght;
        double[] data;

        public SegmentTree(double[] array) 
        {
            this.N = array.Length;
            Lenght = (1 << ((int)Math.Log2(N - 1) + 1)) * 2;
            data = new double[Lenght];

            for(int i = 0; i < Lenght; i++)
            {
                data[i] = 0;
            }

            for (int i = 0; i < N; i++)
            {
                data[Lenght / 2 + i] = array[i];
            }
            
            for(int i = Lenght /2 - 1; i > 0; i--)
            {
                data[i] = data[i * 2] + data[i * 2 + 1];
            }
        }

        public void UpdateElement(int num, double value)
        {
            if (num > Lenght / 2 || num < 0)
            {
                throw new IndexOutOfRangeException($"{num} > {Lenght / 2} || {num} < 0");
            }

            num += Lenght / 2;
            double dif = value - data[num];

            while(num > 0)
            {
                data[num] += dif;
                num /= 2;
            }
        }

        public double GetSum(int L, int R)
        {
            if(L > N || R > N || L < 0 || R < 0)
            {
                throw new IndexOutOfRangeException($"{L} > {N} || {R} > {N} || {L} < 0 || {R} < 0");
            }

            L += Lenght / 2; R += Lenght / 2;

            double sum = 0;

            while (L <= R)
            {
                if (L % 2 != 0) sum += data[L];
                if (R % 2 == 0) sum += data[R];
                L = (L + 1) / 2;
                R = (R - 1) / 2;
            }

            return sum;
        }

        public void Print()
        {
            Print(1, 0);
        }

        private void Print(int n, int h)
        {
            if (n >= Lenght)
            {
                return;
            }

            

            Print(n * 2, h + 1);
            for (int i = 0; i < h; i++)
            {
                Console.Write("   ");
            }
            Console.WriteLine(data[n]);
            Print(n * 2 + 1, h + 1);

            
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //дерево из 16 элементов
            var array = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            var tree = new SegmentTree(array);

            Console.WriteLine("sum: " + tree.GetSum(0, 15));
            //изменение элемента в дереве
            tree.UpdateElement(2, 5);
            //Вывод суммы от 0-го до 15-го элементов
            Console.WriteLine("sum: " + tree.GetSum(0, 15));
            Console.WriteLine("1-15 tree:");
            tree.Print();

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("random tree:");
            //дерево из n элементов, заполненного случайно
            var rand = new Random();
            var randArray = new double[rand.Next(100, 1000)];
            for(int i = 0; i < randArray.Length; i++)
            {
                randArray[i] = rand.Next(100);
            }

            var randTree = new SegmentTree(randArray);

            randTree.Print();

            Console.WriteLine(randTree.GetSum(0, randArray.Length - 1));
        }
    }
}
