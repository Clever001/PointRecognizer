using Emgu.CV;

namespace PointRecognizer.Filters;

public class MedianFilter : IFilter {
    public Mat ApplyFilter(Mat mat) {
        Mat result = new Mat();
        CvInvoke.MedianBlur(mat, result, 11);
        return result;
    }
}