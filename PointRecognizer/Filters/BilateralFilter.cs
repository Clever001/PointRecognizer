using Emgu.CV;

namespace PointRecognizer.Filters;

public class BilateralFilter :IFilter {
    public Mat ApplyFilter(Mat mat) {
        Mat result = new();
        CvInvoke.BilateralFilter(mat, result, 11, 75, 75);
        return result;
    }
}