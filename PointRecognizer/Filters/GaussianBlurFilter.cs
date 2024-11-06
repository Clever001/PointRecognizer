using System.Drawing;
using Emgu.CV;

namespace PointRecognizer.Filters;

public class GaussianBlurFilter : IFilter {
    public Mat ApplyFilter(Mat mat) {
        Mat result = new();
        CvInvoke.GaussianBlur(mat, result, new Size(11, 11), 1.5);
        return result;
    }
}