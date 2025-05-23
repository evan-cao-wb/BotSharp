using BotSharp.Abstraction.Conversations.Models;

namespace BotSharp.Plugin.MongoStorage.Models;

[BsonIgnoreExtraElements(Inherited = true)]
public class StateMongoElement
{
    public string Key { get; set; } = default!;
    public bool Versioning { get; set; }
    public bool Readonly { get; set; }
    public List<StateValueMongoElement> Values { get; set; } = [];

    public static StateMongoElement ToMongoElement(StateKeyValue state)
    {
        return new StateMongoElement
        {
            Key = state.Key,
            Versioning = state.Versioning,
            Readonly = state.Readonly,
            Values = state.Values?.Select(x => StateValueMongoElement.ToMongoElement(x))?.ToList() ?? []
        };
    }

    public static StateKeyValue ToDomainElement(StateMongoElement state)
    {
        return new StateKeyValue
        {
            Key = state.Key,
            Versioning = state.Versioning,
            Readonly = state.Readonly,
            Values = state.Values?.Select(x => StateValueMongoElement.ToDomainElement(x))?.ToList() ?? []
        };
    }
}

public class StateValueMongoElement
{
    public string Data { get; set; } = default!;
    public string? MessageId { get; set; }
    public bool Active { get; set; }
    public int ActiveRounds { get; set; }
    public string DataType { get; set; } = default!;
    public string Source { get; set; } = default!;

    public DateTime UpdateTime { get; set; }

    public static StateValueMongoElement ToMongoElement(StateValue element)
    {
        return new StateValueMongoElement
        {
            Data = element.Data,
            MessageId = element.MessageId,
            Active = element.Active,
            ActiveRounds = element.ActiveRounds,
            DataType = element.DataType,
            Source = element.Source,
            UpdateTime = element.UpdateTime
        };
    }

    public static StateValue ToDomainElement(StateValueMongoElement element)
    {
        return new StateValue
        {
            Data = element.Data,
            MessageId = element.MessageId,
            Active = element.Active,
            ActiveRounds = element.ActiveRounds,
            DataType= element.DataType,
            Source = element.Source,
            UpdateTime = element.UpdateTime
        };
    }
}