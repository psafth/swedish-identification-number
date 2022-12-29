namespace psafth.IdentificationNumber.Interfaces
{
    public interface IIdentificationNumber
    {
        string ToFormalString();
        bool Equals(IIdentificationNumber other);
        bool IsValid { get; }
    }
}
