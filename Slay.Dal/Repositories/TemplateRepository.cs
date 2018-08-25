namespace Slay.Dal.Repositories
{
    using Slay.DalContracts.Repositories;
    using Slay.Models.Entities;

    public sealed class TemplateRepository : RepositoryBase<TemplateEntity>, ITemplateRepository
    {
        public TemplateRepository() 
            : base("Slay", "Templates")
        {

        }
    }
}