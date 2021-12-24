using OrchardCore.AdminDashboard.Indexes;
using OrchardCore.Alias.Indexes;
using OrchardCore.AuditTrail.Indexes;
using OrchardCore.ContentLocalization.Records;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Deployment.Indexes;
using OrchardCore.Layers.Indexes;
using OrchardCore.Lists.Indexes;
using OrchardCore.OpenId.YesSql.Indexes;
using OrchardCore.PublishLater.Indexes;
using OrchardCore.Taxonomies.Indexing;
using OrchardCore.Users.Indexes;
using OrchardCore.Workflows.Indexes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YesSql;
using YesSql.Indexes;

namespace OrchardCoreContrib.Linq
{
    /// <summary>
    /// Represents a data context for Orchard Core index tables.
    /// </summary>
    public class OrchardCoreDataContext : IDisposable
    {
        private readonly IStore _store;

        /// <summary>
        /// Initializes a new instance of a <see cref="OrchardCoreDataContext"/> with store.
        /// </summary>
        /// <param name="store">The underlying store of the database.</param>
        public OrchardCoreDataContext(IStore store)
        {
            _store = store;
        }

        /// <summary>
        /// Gets a list of dashboards widgets metadata.
        /// </summary>
        public IEnumerable<DashboardPartIndex> Dashboards => QueryAsync<DashboardPartIndex>().Result;

        /// <summary>
        /// Gets a list of auditing events.
        /// </summary>
        public IEnumerable<AuditTrailEventIndex> AuditEvents => QueryAsync<AuditTrailEventIndex>().Result;

        /// <summary>
        /// Gets a list of deployment plans.
        /// </summary>
        public IEnumerable<DeploymentPlanIndex> DeploymentPlans => QueryAsync<DeploymentPlanIndex>().Result;

        /// <summary>
        /// Gets a list of layers metadata.
        /// </summary>
        public IEnumerable<LayerMetadataIndex> Layers => QueryAsync<LayerMetadataIndex>().Result;

        /// <summary>
        /// Gets a list of contained content items.
        /// </summary>
        public IEnumerable<ContainedPartIndex> Containers => QueryAsync<ContainedPartIndex>().Result;

        /// <summary>
        /// Gets a list of schedules content items.
        /// </summary>
        public IEnumerable<PublishLaterPartIndex> Publishes => QueryAsync<PublishLaterPartIndex>().Result;

        /// <summary>
        /// Gets a list of tags / categories of content items.
        /// </summary>
        public IEnumerable<TaxonomyIndex> Taxonomies => QueryAsync<TaxonomyIndex>().Result;

        /// <summary>
        /// Gets a list of workflows.
        /// </summary>
        public IEnumerable<WorkflowIndex> Workflows => QueryAsync<WorkflowIndex>().Result;

        /// <summary>
        /// Gets a list of workflow activities.
        /// </summary>
        public IEnumerable<WorkflowBlockingActivitiesIndex> WorkflowBlockingActivities => QueryAsync<WorkflowBlockingActivitiesIndex>().Result;

        /// <summary>
        /// Gets a list of workflow types.
        /// </summary>
        public IEnumerable<WorkflowTypeIndex> WorkflowTypes => QueryAsync<WorkflowTypeIndex>().Result;

        /// <summary>
        /// Gets a list of workflow types start activities.
        /// </summary>
        public IEnumerable<WorkflowTypeStartActivitiesIndex> WorkflowTypeStartActivities => QueryAsync<WorkflowTypeStartActivitiesIndex>().Result;

        /// <summary>
        /// Gets a list of content items route.
        /// </summary>
        public IEnumerable<AutoroutePartIndex> Routes => QueryAsync<AutoroutePartIndex>().Result;

        /// <summary>
        /// Gets a list of content item aliases.
        /// </summary>
        public IEnumerable<AliasPartIndex> Aliases => QueryAsync<AliasPartIndex>().Result;

        /// <summary>
        /// Gets a list of users.
        /// </summary>
        public IEnumerable<UserIndex> Users => QueryAsync<UserIndex>().Result;

        /// <summary>
        /// Gets a list of user login provider.
        /// </summary>
        public IEnumerable<UserByLoginInfoIndex> LoginProviders => QueryAsync<UserByLoginInfoIndex>().Result;

        /// <summary>
        /// Gets a list of user claims.
        /// </summary>
        public IEnumerable<UserByClaimIndex> UserClaims => QueryAsync<UserByClaimIndex>().Result;

        /// <summary>
        /// Gets a list of OpenID tokens.
        /// </summary>
        public IEnumerable<OpenIdTokenIndex> OpenIdTokens => QueryAsync<OpenIdTokenIndex>().Result;

        /// <summary>
        /// Gets a list of OpenID scopes.
        /// </summary>
        public IEnumerable<OpenIdScopeIndex> OpenIdScopes => QueryAsync<OpenIdScopeIndex>().Result;

        /// <summary>
        /// Gets a list of OpenID authorizations.
        /// </summary>
        public IEnumerable<OpenIdAuthorizationIndex> OpenIdAuthorizations => QueryAsync<OpenIdAuthorizationIndex>().Result;

        /// <summary>
        /// Gets a list of OpenID applications.
        /// </summary>
        public IEnumerable<OpenIdApplicationIndex> OpenIdApplications => QueryAsync<OpenIdApplicationIndex>().Result;

        /// <summary>
        /// Gets a list of content items.
        /// </summary>
        public IEnumerable<ContentItemIndex> ContentItems => QueryAsync<ContentItemIndex>().Result;

        /// <summary>
        /// Gets a list of localized content items.
        /// </summary>
        public IEnumerable<LocalizedContentItemIndex> LocalizedContentItems => QueryAsync<LocalizedContentItemIndex>().Result;

        private async Task<IEnumerable<TIndex>> QueryAsync<TIndex>() where TIndex : class, IIndex
        {
            IEnumerable<TIndex> result = null;
            using (var session = _store.CreateSession())
            {
                result = await session.QueryIndex<TIndex>().ListAsync();
            }

            return result;
        }

        public void Dispose() => _store.Dispose();
    }
}
