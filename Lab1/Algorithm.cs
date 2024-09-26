using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    
    public abstract class Algorithm
    {
        protected List<IterationData> iterationData;
        protected Stopwatch stopwatch;

        public Algorithm() 
        {
            iterationData = new List<IterationData>();
            stopwatch = new Stopwatch();
        }

        public abstract void MeasureIteration(int iterations);

        public void Start(int iterations)
        {
            iterationData.Clear();
            MeasureIteration(iterations);
        }

        public List<IterationData> GetIterationData()
        {
            return iterationData;
        }
    }

    public class IterationData
    {
        public int IterationNumber { get; set; }
        public double TimeSpent {  get; set; }
    }
}
