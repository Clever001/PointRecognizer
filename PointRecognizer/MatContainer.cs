using System.Diagnostics;
using Emgu.CV;
using PointRecognizer.Filters;

namespace PointRecognizer;

public class MatContainer : List<Mat> {
    private readonly DirectoryInfo _inputDir;
    private readonly DirectoryInfo _outputDir;
    public List<IFilter> Filters { get; init; }

    private SortedDictionary<string, List<string>> names = new();

    public MatContainer(string inputDirPath, string outputDirPath, IEnumerable<IFilter> filters) {
        ArgumentNullException.ThrowIfNull(inputDirPath);
        ArgumentNullException.ThrowIfNull(outputDirPath);
        ArgumentNullException.ThrowIfNull(filters);
        if (!filters.Any()) throw new ArgumentException("Should be at least one filter", nameof(filters));

        _inputDir = new(inputDirPath);
        _outputDir = new(outputDirPath);
        if (!_inputDir.Exists) throw new DirectoryNotFoundException();
        if (!_outputDir.Exists) throw new DirectoryNotFoundException();
        
        Filters = [..filters];
    }

    public MatContainer InitFromDirectory() {
        Clear();
        names.Clear();

        Stopwatch sw = Stopwatch.StartNew();
        Console.WriteLine("Initializing MatContainer");
        
        foreach (var dir in _inputDir.GetDirectories()) {
            DirectoryInfo photosDir = dir.EnumerateDirectories().First(x => x.Name == "opencv_images");
            names[dir.Name] = new(photosDir.EnumerateFiles().Select(x => x.Name));
            
            foreach (var file in photosDir.EnumerateFiles()) {
                Mat sourceMat = CvInvoke.Imread(file.FullName);
                Add(sourceMat);
            }
        }
        
        sw.Stop();
        Console.WriteLine($"Took {sw.Elapsed} to load {Count} images");
        return this;
    }

    public MatContainer ApplyFilters() {
        Stopwatch sw = Stopwatch.StartNew();
        Console.WriteLine("Applying filters");
        if (Count == 0) throw new InvalidOperationException("MatContainer has no elements");

        for (int i = 0; i < Count; i++) {
            Filters.ForEach(filter => { this[i] = filter.ApplyFilter(this[i]);} );
            // Console.WriteLine($"Image {i + 1}/{Count} is filtered");
        }
        sw.Stop();
        Console.WriteLine($"Took {sw.Elapsed} to Filter {Count} images");
        return this;
    }

    public MatContainer WriteToDirectory() {
        if (Count == 0) throw new InvalidOperationException("MatContainer has no elements");
        
        Stopwatch sw = Stopwatch.StartNew();
        Console.WriteLine("Writing to output directory");
        if (_outputDir.EnumerateDirectories().Any()) {
            _outputDir.Delete(true);
            _outputDir.Create();
        }
        using var enumerator = GetEnumerator();
        enumerator.MoveNext();
        
        foreach (var kvp in names) {
            DirectoryInfo resultDir = _outputDir.CreateSubdirectory(kvp.Key);

            foreach (var file in kvp.Value) {
                CvInvoke.Imwrite(resultDir.FullName + "\\" + file, enumerator.Current);
                enumerator.MoveNext();
                // Console.WriteLine($"Image {file} is written to output directory");
            }
        }
        
        sw.Stop();
        Console.WriteLine($"Took {sw.Elapsed} to write {Count} images");
        return this;
    }
}