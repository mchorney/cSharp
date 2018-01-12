using System;
using System.Collections.Generic;

namespace puzzles
{
    class Program
    {
        public static int[] randomArray(){
            int[] arr = new int[10];
            Random rand = new Random();
            int min = 0;
            int max = 0;
            int sum = 0;
            for (int i=0; i<arr.Length; i++){
                arr[i] = rand.Next(5,25);
                if (i==0){
                    min = arr[i];
                }
                if (arr[i]>max){
                    max = arr[i];
                }
                if (arr[i]<min){
                    min = arr[i];
                }
                sum += arr[i];
            }
            Console.WriteLine("Max: {0}, Min: {1}, Avg: {2}", max, min, sum/arr.Length);
            return arr;
        }

        public static string tossCoin(){
            Console.WriteLine("Tossing a Coin!");
            Random rand = new Random();
            int result = rand.Next(2);
            if (result == 0){
                Console.WriteLine("Heads");
                return "Heads";
            } else {
                Console.WriteLine("Tails");
                return "Tails";
            }
        }

        public static double tossMultipleCoins(int num){
            double heads = 0;
            double tails = 0;
            for (int i=0; i<num; i++){
                string result = tossCoin();
                if (result == "Heads"){
                    heads += 1;
                } else {
                    tails += 1;
                }
            }
            return heads/tails;
        }

        public static string[] names(){
            string[] arr = {"Todd", "Tiffany", "Charlie", "Geneva", "Sydney"};
            List<string> namesList = new List<string>();
            Random rand = new Random();
            for (int i=0; i<arr.Length; i++){
                int j = rand.Next(i, arr.Length);
                string temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
            foreach (string name in arr){
                Console.WriteLine(name);
                if (name.Length > 5){
                    namesList.Add(name);
                }
            }
            return namesList.ToArray();
        }

        static void Main(string[] args)
        {
            int[] arr1 = randomArray();
            foreach (int num in arr1){
                Console.WriteLine(num);
            }

            tossCoin();
            double ratio = tossMultipleCoins(10);
            Console.WriteLine(ratio);

            string[] arr2 = names();
            Console.WriteLine("Only names with length greater than 5");
            foreach (string name in arr2){
                Console.WriteLine(name);
            }
        }
    }
}
