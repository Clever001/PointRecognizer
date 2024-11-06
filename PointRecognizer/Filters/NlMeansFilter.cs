using Emgu.CV;

namespace PointRecognizer.Filters;

public class NlMeansFilter : IFilter {
    public Mat ApplyFilter(Mat mat) {
        Mat result = new();
        CvInvoke.FastNlMeansDenoising(mat, result);
        return result;
    }
}