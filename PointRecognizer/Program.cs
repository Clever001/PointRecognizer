using System.Diagnostics;
using PointRecognizer;
using PointRecognizer.Filters;

Stopwatch sw = Stopwatch.StartNew();

// ----- Настройка конфигурации ----- //

// Расположение основной папки
string basePath = @"C:\Users\cakev\RiderProjects\PointRecognizer\PointRecognizer\";

// Фильтры, которые будут применены к изображению
IFilter[] filters = [
    new MedianFilter(),
//    new NlMeansFilter(),
//    new GaussianBlurFilter(),
//    new BilateralFilter(),
];

// ----- Настройка конфигурации ----- //

MatContainer container = new(
    basePath + "testPhotos",
    basePath + "result", 
    filters);

container.InitFromDirectory()
    .ApplyFilters()
    .WriteToDirectory();

sw.Stop();
Console.WriteLine($"Всего было потрачено времени: {sw.Elapsed}");
