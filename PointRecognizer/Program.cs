﻿using Emgu.CV;

DirectoryInfo inputDir = new("D:\\code\\PointRecognizer\\PointRecognizer\\testPhotos");
DirectoryInfo outputDir = new("D:\\code\\PointRecognizer\\PointRecognizer\\result");

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