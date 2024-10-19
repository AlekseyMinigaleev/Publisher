var a = Directory.GetCurrentDirectory();

var publisher = new Publisher.Publisher(
    "D:\\Work\\K-Soft\\projects\\WorkProjects\\KSEObjectModel",
    "D:\\Work\\K-Soft\\projects\\LearningProjects\\Publisher\\TestBuilds",
    "OM_10K_TEGOV");

await publisher.PublishAsync(CancellationToken.None);