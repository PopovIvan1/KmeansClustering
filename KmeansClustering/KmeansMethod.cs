namespace KmeansClustering
{
    public class KmeansMethod
    {
        private int clustersCount = 0;
        private List<double[]> rawData = new List<double[]>();
        private List<double[]> clustersCoordinates = new List<double[]>();
        private int[] clustersNumbers = new int[] { };

        public void SetMethodParams(int clustersCount, List<double[]> rawData)
        {
            this.clustersCount = clustersCount;
            this.rawData = rawData;
        }

        public List<double[]> GetClustersCoordinates()
        {
            if (clustersCount > rawData.Count()) return new List<double[]>();
            startClustering();
            return clustersCoordinates;
        }

        public void GenerateRandomData()
        {
            clustersCount = 4;
            Random rnd = new Random();
            for (int i = 0; i < rnd.Next(40, 100); i++)
            {
                rawData.Add(new double[] 
                { 100 * rnd.NextDouble(), 100 * rnd.NextDouble(), 100 * rnd.NextDouble() });
            }
        }

        private void startClustering()
        {
            initClusters();
            int[] newClusters;
            bool isClustersChanged = true;
            while (isClustersChanged)
            {
                newClusters = setNewClusters();
                isClustersChanged = !Enumerable.SequenceEqual(newClusters, clustersNumbers);
                Array.Copy(newClusters, clustersNumbers, newClusters.Length);
            }
        }

        private int[] setNewClusters()
        {
            List<int> newClusters = new List<int>();
            foreach (var data in rawData)
            {
                newClusters.Add(getClusterNumber(data));
            }
            setClustersCenters(newClusters.ToArray());
            return newClusters.ToArray();
        }

        private void initClusters()
        {
            rawData.Take(clustersCount).ToList()
                .ForEach(coordinates => clustersCoordinates.Add(coordinates));
            clustersNumbers = new int[rawData.Count()];
        }

        private double distance(double[] a, double[] b)
        {
            double result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                result += Math.Pow(a[i] - b[i], 2);
            }
            return Math.Sqrt(result);
        }

        private int getClusterNumber(double[] element)
        {
            double minDistance = double.MaxValue;
            int clusterNumber = 0;
            for (int i = 0; i < clustersCount; i++)
            {
                double currentDistance = distance(element, clustersCoordinates[i]);
                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    clusterNumber = i;
                }
            }
            return clusterNumber;
        }

        private void setClustersCenters(int[] currentClustersNumbers)
        {
            for (int i = 0; i < clustersCount; i++)
            {
                List<double[]> currentClusterElements = new List<double[]>();
                for (int j = 0; j < currentClustersNumbers.Length; j++)
                {
                    if (currentClustersNumbers[j] == i)
                    {
                        currentClusterElements.Add(rawData[j]);
                    }
                }
                clustersCoordinates[i] = Enumerable.Range(0, rawData[0].Length)
                    .Select(index => currentClusterElements.Select(e => e[index]).Average()).ToArray();
            }
        }
    }
}
