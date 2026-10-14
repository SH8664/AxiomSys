namespace AxiomSysPortal.Localization
{
    public interface ISharedLocalizer
    {
        string this[string key] { get; }
    }
}
