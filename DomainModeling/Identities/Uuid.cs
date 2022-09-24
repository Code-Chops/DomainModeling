using System.Text.RegularExpressions;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

/// <summary>
/// A 32-digit UUID without hyphens.
/// </summary>
[GenerateStringValueObject(addParameterlessConstructor: true)]
public partial record struct Uuid : IConvertible
{
    private static readonly Regex ValidationRegex = new("^[0-9A-F]{32}$", RegexOptions.Compiled);

    public void Validate()
    {
        if (!ValidationRegex.IsMatch(this.Value))
            throw new ArgumentException($"Invalid GUID '{this.Value}' provided.");
    }

    public Uuid()
    {
        this.Value = Guid.NewGuid().ToString("N").ToUpper();
    }

    public Uuid(Guid uuid)
    {
        this.Value = uuid.ToString("N").ToUpper();
    }

    public TypeCode GetTypeCode() => this.Value.GetTypeCode();
    object IConvertible.ToType(Type type, IFormatProvider? provider) => ((IConvertible)this.Value).ToType(type, provider);
    public string ToString(IFormatProvider? provider) => this.Value.ToString(provider);
    bool IConvertible.ToBoolean(IFormatProvider? provider) => ((IConvertible)this.Value).ToBoolean(provider);
    char IConvertible.ToChar(IFormatProvider? provider) => ((IConvertible)this.Value).ToChar(provider);
    sbyte IConvertible.ToSByte(IFormatProvider? provider) => ((IConvertible)this.Value).ToSByte(provider);
    byte IConvertible.ToByte(IFormatProvider? provider) => ((IConvertible)this.Value).ToByte(provider);
    short IConvertible.ToInt16(IFormatProvider? provider) => ((IConvertible)this.Value).ToInt16(provider);
    ushort IConvertible.ToUInt16(IFormatProvider? provider) => ((IConvertible)this.Value).ToUInt16(provider);
    int IConvertible.ToInt32(IFormatProvider? provider) => ((IConvertible)this.Value).ToInt32(provider);
    uint IConvertible.ToUInt32(IFormatProvider? provider) => ((IConvertible)this.Value).ToUInt32(provider);
    long IConvertible.ToInt64(IFormatProvider? provider) => ((IConvertible)this.Value).ToInt64(provider);
    ulong IConvertible.ToUInt64(IFormatProvider? provider) => ((IConvertible)this.Value).ToUInt64(provider);
    float IConvertible.ToSingle(IFormatProvider? provider) => ((IConvertible)this.Value).ToSingle(provider);
    double IConvertible.ToDouble(IFormatProvider? provider) => ((IConvertible)this.Value).ToDouble(provider);
    decimal IConvertible.ToDecimal(IFormatProvider? provider) => ((IConvertible)this.Value).ToDecimal(provider);
    DateTime IConvertible.ToDateTime(IFormatProvider? provider) => ((IConvertible)this.Value).ToDateTime(provider);
}