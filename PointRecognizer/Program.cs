using Emgu.CV;

DirectoryInfo inputDir = new("C:\\Users\\cakev\\RiderProjects\\PointRecognizer\\PointRecognizer\\testPhotos");
DirectoryInfo outputDir = new("C:\\Users\\cakev\\RiderProjects\\PointRecognizer\\PointRecognizer\\result");

foreach (var dir in inputDir.GetDirectories()) {
    Console.WriteLine($"Обработка папки {dir.Name}");
    DirectoryInfo photosDir = (from d in dir.EnumerateDirectories()
        where d.Name == "opencv_images"
        select d).First();
    
    DirectoryInfo resultDir = outputDir.CreateSubdirectory(dir.Name);
    
    foreach (var file in photosDir.GetFiles()) {
        Mat sourceMat = CvInvoke.Imread(file.FullName);
        if (sourceMat is null || sourceMat.IsEmpty) throw new ArgumentException("Неверно распознано изображение.");

        Mat dstMat = new();
        CvInvoke.MedianBlur(sourceMat, dstMat, 9);
        CvInvoke.Imwrite(resultDir.FullName + "\\" + file.Name, dstMat);
        Console.WriteLine($"Изображение обработано и сохранено как {resultDir.FullName + "\\" + file.Name}");
    }
}

/*
foreach (var file in inputDir.GetFiles()) {
    Console.WriteLine($"Обработка файла \"{file}\".");
    Mat src = CvInvoke.Imread(file.FullName, Emgu.CV.CvEnum.ImreadModes.Color);
    if (src == null || src.IsEmpty) {
        Console.WriteLine($"Не удалось загрузить изображение \"{file}\".");
        continue;
    }
    
    // Создание выходного изображения
    Mat dst = new Mat();

    // Применение медианного фильтра
    int kernelSize = 9; // Размер ядра фильтра (нечетное число, например, 3, 5, 7)
    CvInvoke.MedianBlur(src, dst, kernelSize);

    // Сохранение обработанного изображения
    CvInvoke.Imwrite(outputDir.FullName + "\\RE_" + file.Name, dst);

    Console.WriteLine($"Изображение обработано и сохранено как {outputDir.FullName + "\\RE_" + file.Name}");
}
*/