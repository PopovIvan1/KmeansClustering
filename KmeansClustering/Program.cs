using KmeansClustering;

KmeansMethod kmeansMethod = new KmeansMethod();
kmeansMethod.GenerateRandomData();
var result = kmeansMethod.GetClustersCoordinates();