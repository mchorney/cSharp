using System;

namespace basic13
{
    class Program
    {

        public static void OneToTwoFiftyFive()
        {
            for (int i = 1; i < 256; i++)
            {
                Console.WriteLine(i);
            }
        }

        public static void OneToTwoFiftyFiveOdds()
        {
            for (int i = 1; i < 256; i += 2)
            {
                Console.WriteLine(i);
            }
        }

        public static void Sum()
        {
            int sum = 0;
            for (int i = 1; i < 256; i++)
            {
                sum += i;
                Console.WriteLine("New number: " + i.ToString() + " Sum: " + sum.ToString());
            }
        }

        public static void IterateThroughArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }
        }

        public static void FindMax(int[] arr)
        {
            int max = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                }
            }
            Console.WriteLine(max);
        }

        public static void Average(int[] arr)
        {
            float count = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                count += arr[i];
            }
            float average = count / (float)arr.Length;
            Console.WriteLine(average);
        }

        public static void Odds()
        {
            int[] y = new int[128];
            int index = 0;
            int value = 1;
            while (value < 256)
            {
                y[index] = value;
                index++;
                value += 2;
            }
            foreach (int num in y)
            {
                Console.WriteLine(num);
            }
        }

        public static int GreaterThanY(int[] arr, int y)
        {
            int count = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > y)
                {
                    count++;
                }
            }
            Console.WriteLine(count);
            return count;
        }

        public static void SquareTheValues(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = arr[i] * arr[i];
            }
        }

        public static void NoNegatives(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < 0)
                {
                    arr[i] = 0;
                }
            }
        }

        public static void MinMaxAvg(int[] arr)
        {
            int sum = 0;
            int min = arr[0];
            int max = arr[0];
            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
                if (arr[i] > max)
                {
                    max = arr[i];
                }
                if (arr[i] < min)
                {
                    min = arr[i];
                }
            }
            int avg = sum / arr.Length;
            Console.WriteLine(min);
            Console.WriteLine(max);
            Console.WriteLine(avg);
        }

        public static void Shifting(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                arr[i] = arr[i + 1];
            }
            arr[arr.Length - 1] = 0;
        }

        public static void Dojo(object[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if ((int)arr[i] < 0)
                {
                    arr[i] = "Dojo";
                }
            }
        }



        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            OneToTwoFiftyFive();
            // OneToTwoFiftyFiveOdds();
            // Sum();
            // int[] testArray = {-3, -2, -1, 0, 1, 2, 3, 4, 5, 6};
            // IterateThroughArray(testArray);
            // FindMax(testArray);
            // Average(testArray);
            // Odds();
            // GreaterThanY(testArray, 3);
            // SquareTheValues(testArray);
            // NoNegatives(testArray);
            // MinMaxAvg(testArray);
            // Shifting(testArray);
            object[] testArray2 = { -3, -2, -1, 0, 1, 2, 3, 4, 5, 6 };
            Dojo(testArray2);
            Console.WriteLine(testArray2[2]);
        }
    }
}
