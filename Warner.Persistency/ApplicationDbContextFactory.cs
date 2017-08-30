using System;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Warner.Persistency
{
    /// <summary>
    /// Factory for "dotnet ef" migrations.
    /// Hardcoded connection string not to pull it from config.
    /// </summary>
    public class ApplicationDbContextFactory :
        IDbContextFactory<ApplicationDataContext>
    {
        private const string ProductionDatabase =
            "Server=tcp:warnerdb.database.windows.net,1433;Initial Catalog=Warner;Persist Security Info=False;User ID=ashukanov;Password=l337_it_be;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        private const string DevelopmentLocalDatabase =
            "Data Source=.;Initial Catalog=warner;Integrated Security=True;MultipleActiveResultSets=True";

        public ApplicationDataContext Create(
            DbContextFactoryOptions options)
        {
            return new ApplicationDataContext(DevelopmentLocalDatabase);
        }
    }
}
