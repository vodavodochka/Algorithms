//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Lab1
//{
//    public class PowerAlgorithm : AlgorithmWithSteps
//    {
//        public override void MeasureIteration(int exponent, int baseNumber)
//        {
//            int result = 1;
//            int steps = 0;
//            iterationData.Add(new IterationData { IterationNumber = 0, TimeSpent = steps });
//            for (int i = 0; i < exponent; i++)
//            {
//                iterationData.Add(new IterationData { IterationNumber = i + 1, TimeSpent = steps });
//                steps++;
//                iterationData.Add(new IterationData { IterationNumber = i + 1, TimeSpent = steps });
//                result *= baseNumber;
//                steps++;
//                iterationData.Add(new IterationData { IterationNumber = i + 1, TimeSpent = steps });
//                steps++;
//                iterationData.Add(new IterationData { IterationNumber = i + 1, TimeSpent = steps });
//            }
//        }
//    }

//}
