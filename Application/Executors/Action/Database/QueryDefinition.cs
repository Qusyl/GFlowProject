

namespace Application.Executors.Action.Database
{

/// <summary>
/// Класс не подлежит использованию
/// </summary>

    public abstract record QueryDefinition
    {
        public abstract OperationType Operation { get; }

        public string TableName { get; init; }

    }
}