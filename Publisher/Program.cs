using Publisher;

var publisher = new Publisher.Publisher(
    DirectoryPathConstatns.ALL_PROJECT_SOLUTION,
    DirectoryPathConstatns.BUILDS
    );

await publisher.PublishAsync(CancellationToken.None);