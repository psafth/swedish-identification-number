namespace psafth.IdentificationNumber.Interfaces
{
    public interface IIdentificationNumber<T>
    {
        T ParseFromString(string value);
        string ToFormalString();
        bool IsValid { get; }
    }
}
