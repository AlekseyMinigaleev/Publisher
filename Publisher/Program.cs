var publisher = new Publisher.Publisher(
    "C:\\main\\projects\\learning\\AgentWith",
    "C:\\main\\builds",
    "AgentInst3");

await publisher.PublishAsync(CancellationToken.None);