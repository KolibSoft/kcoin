using System.Buffers.Text;
using System.Text;

namespace KolibSoft.KCoin.Core;

public struct KCoinAddress(ArraySegment<byte> utf8)
{

    public ArraySegment<byte> Data { get; } = utf8;

    public override string ToString()
    {
        var @string = Encoding.UTF8.GetString(Data);
        return @string;
    }

    public bool Validate()
    {
        var result = Verify(Data);
        return result;
    }

    public static bool Verify(ReadOnlySpan<byte> utf8)
    {
        var result = utf8.Length == 44 && Base64.IsValid(utf8);
        return result;
    }

    public static bool Verify(ReadOnlySpan<char> @string)
    {
        var result = @string.Length == 44 && Base64.IsValid(@string);
        return result;
    }

    public static KCoinAddress Parse(ReadOnlySpan<byte> utf8)
    {
        if (!Verify(utf8)) throw new FormatException("Invalid KCoin Address Format");
        var address = new KCoinAddress(utf8.ToArray());
        return address;
    }

    public static KCoinAddress Parse(ReadOnlySpan<char> @string)
    {
        if (!Verify(@string)) throw new FormatException("Invalid KCoin Address Format");
        var utf8 = new byte[Encoding.UTF8.GetByteCount(@string)];
        Encoding.UTF8.GetBytes(@string, utf8);
        var address = new KCoinAddress(utf8);
        return address;
    }

    public static readonly KCoinAddress None = Parse("00000000000000000000000000000000000000000000");

}