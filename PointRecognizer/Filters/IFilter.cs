using Emgu.CV;

namespace PointRecognizer.Filters;

public interface IFilter {
    public Mat ApplyFilter(Mat mat);
}