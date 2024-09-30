//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Lab1
//{
//    internal class RecursivePower : AlgorithmWithSteps
//    {
//        public override void MeasureIteration(int exponent)
//        {
//            Stopwatch stopwatch = new Stopwatch();
//            stopwatch.Start();

//            PowerRecursive(2, exponent);

//            stopwatch.Stop();
//        }

//        private int PowerRecursive(int baseNumber, int exponent)
//        {
//            steps++;  
//            if (exponent == 0) return 1;

//            int result = PowerRecursive(baseNumber, exponent - 1);
//            iterationData.Add(new IterationData
//            {
//                IterationNumber = exponent,
//                TimeSpent = steps  
//            });

//            return baseNumber * result;
//        }
//    }
//}
