using Application.ReadyQueue;
using Application.Runtime.Workers;
using Application.Scheduler;
using Domain.Graph;

namespace Application.Runtime.Session;

public sealed record ExecuteSession(
    ExecutionGraph Graph,
    IReadyQueue Queue,
    NodeScheduler Scheduler,
    IWorker[] Workers,
    ExecutionContext Context
);

