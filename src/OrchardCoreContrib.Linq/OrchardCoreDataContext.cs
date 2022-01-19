using LinqToDB;
using LinqToDB.Data;
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
using YesSql;

namespace OrchardCoreContrib.Linq
{
    /// <summary>
    /// Represents a data context for Orchard Core index tables.
    /// </summary>
    public class OrchardCoreDataContext : DataContextBase, IDisposable
    {
        private readonly IStore _store;

        /// <summary>
        /// Initializes a new instance of a <see cref="OrchardCoreDataContext"/> with store.
        /// </summary>
        /// <param name="store">The underlying store of the database.</param>
        public OrchardCoreDataContext(IStore store)
        {
            _store = store;

            Connection = OrchardCoreDataConnectionFactory.Create(_store);
        }

        /// <summary>
        /// Gets a list of dashboards widgets metadata.
        /// </summary>
        public ITable<DashboardPartIndex> Dashboards => GetTable<DashboardPartIndex>();

        /// <summary>
        /// Gets a list of auditing events.
        /// </summary>
        public ITable<AuditTrailEventIndex> AuditEvents => GetTable<AuditTrailEventIndex>();

        /// <summary>
        /// Gets a list of deployment plans.
        /// </summary>
        public ITable<DeploymentPlanIndex> DeploymentPlans => GetTable<DeploymentPlanIndex>();

        /// <summary>
        /// Gets a list of layers metadata.
        /// </summary>
        public ITable<LayerMetadataIndex> Layers => GetTable<LayerMetadataIndex>();

        /// <summary>
        /// Gets a list of contained content items.
        /// </summary>
        public ITable<ContainedPartIndex> Containers => GetTable<ContainedPartIndex>();

        /// <summary>
        /// Gets a list of schedules content items.
        /// </summary>
        public ITable<PublishLaterPartIndex> Publishes => GetTable<PublishLaterPartIndex>();

        /// <summary>
        /// Gets a list of tags / categories of content items.
        /// </summary>
        public ITable<TaxonomyIndex> Taxonomies => GetTable<TaxonomyIndex>();

        /// <summary>
        /// Gets a list of workflows.
        /// </summary>
        public ITable<WorkflowIndex> Workflows => GetTable<WorkflowIndex>();

        /// <summary>
        /// Gets a list of workflow activities.
        /// </summary>
        public ITable<WorkflowBlockingActivitiesIndex> WorkflowBlockingActivities => GetTable<WorkflowBlockingActivitiesIndex>();

        /// <summary>
        /// Gets a list of workflow types.
        /// </summary>
        public ITable<WorkflowTypeIndex> WorkflowTypes => GetTable<WorkflowTypeIndex>();

        /// <summary>
        /// Gets a list of workflow types start activities.
        /// </summary>
        public ITable<WorkflowTypeStartActivitiesIndex> WorkflowTypeStartActivities => GetTable<WorkflowTypeStartActivitiesIndex>();

        /// <summary>
        /// Gets a list of content items route.
        /// </summary>
        public ITable<AutoroutePartIndex> Routes => GetTable<AutoroutePartIndex>();

        /// <summary>
        /// Gets a list of content item aliases.
        /// </summary>
        public ITable<AliasPartIndex> Aliases => GetTable<AliasPartIndex>();

        /// <summary>
        /// Gets a list of users.
        /// </summary>
        public ITable<UserIndex> Users => GetTable<UserIndex>();

        /// <summary>
        /// Gets a list of user login provider.
        /// </summary>
        public ITable<UserByLoginInfoIndex> LoginProviders => GetTable<UserByLoginInfoIndex>();

        /// <summary>
        /// Gets a list of user claims.
        /// </summary>
        public ITable<UserByClaimIndex> UserClaims => GetTable<UserByClaimIndex>();

        /// <summary>
        /// Gets a list of OpenID tokens.
        /// </summary>
        public ITable<OpenIdTokenIndex> OpenIdTokens => GetTable<OpenIdTokenIndex>();

        /// <summary>
        /// Gets a list of OpenID scopes.
        /// </summary>
        public ITable<OpenIdScopeIndex> OpenIdScopes => GetTable<OpenIdScopeIndex>();

        /// <summary>
        /// Gets a list of OpenID authorizations.
        /// </summary>
        public ITable<OpenIdAuthorizationIndex> OpenIdAuthorizations => GetTable<OpenIdAuthorizationIndex>();

        /// <summary>
        /// Gets a list of OpenID applications.
        /// </summary>
        public ITable<OpenIdApplicationIndex> OpenIdApplications => GetTable<OpenIdApplicationIndex>();

        /// <summary>
        /// Gets a list of content items.
        /// </summary>
        public ITable<ContentItemIndex> ContentItems => GetTable<ContentItemIndex>();

        /// <summary>
        /// Gets a list of localized content items.
        /// </summary>
        public ITable<LocalizedContentItemIndex> LocalizedContentItems => GetTable<LocalizedContentItemIndex>();

        /// <inheritdoc/>
        public void Dispose() => _store.Dispose();
    }
}
