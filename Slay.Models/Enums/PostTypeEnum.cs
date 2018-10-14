namespace Slay.Models.Enums
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1602:EnumerationItemsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public enum PostTypeEnum
    {
        None = 0,

        Image = 1,

        Images = 2,

        Text = 3,

        Quote = 4,

        Video = 5,

        YouTube = 6
    }
}