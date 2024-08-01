var publisher = new Publisher.Publisher(
    "C:\\main\\projects\\KSEAgent",
    "C:\\main\\builds",
    1780);

await publisher.PublishAsync(CancellationToken.None);