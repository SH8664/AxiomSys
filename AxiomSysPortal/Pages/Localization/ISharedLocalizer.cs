namespace AxiomSysPortal.Pages.Localization
{
    public interface ISharedLocalizer
    {
        string this[string key] { get; }
    }
}
