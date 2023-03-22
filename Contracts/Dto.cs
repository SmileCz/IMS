using ProtoBuf;

namespace Contracts;

[ProtoContract]
public class Dto<TValue>
{
    [ProtoMember(1)] public TValue Value { get; set; }

    public Dto()
    {
    }

    public Dto(TValue value)
    {
        Value = value;
    }
}

public static class Dto
{
    public static Dto<TValue> FromValue<TValue>(TValue value)
    {
        return new Dto<TValue>(value);
    }
}