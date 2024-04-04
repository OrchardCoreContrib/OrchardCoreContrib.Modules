using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Media.Settings;
using OrchardCore.Taxonomies.Settings;

namespace OrchardCoreContrib.IssueTracker;
public class Migrations : DataMigration
{
    IContentDefinitionManager _contentDefinitionManager;

    public Migrations(IContentDefinitionManager contentDefinitionManager)
    {
        _contentDefinitionManager = contentDefinitionManager;
    }

    public int Create()
    {
        // Issue Priority
        _contentDefinitionManager.AlterTypeDefinition("IssuePriority", type => type
            .DisplayedAs("Issue Priority")
            .WithPart("TitlePart", part => part
                .WithPosition("0")
            )
            .WithPart("IssuePriority", part => part
                .WithPosition("1")
            )
            .WithPart("AutoroutePart", part => part
                .WithPosition("2")
            )
        );

        // Issue Status
        _contentDefinitionManager.AlterTypeDefinition("IssueStatus", type => type
            .DisplayedAs("Issue Status")
            .WithPart("TitlePart", part => part
                .WithPosition("0")
            )
            .WithPart("IssueStatus", part => part
                .WithPosition("1")
            )
            .WithPart("AutoroutePart", part => part
                .WithPosition("2")
            )
        );

        // Issue Category
        _contentDefinitionManager.AlterTypeDefinition("IssueCategory", type => type
            .DisplayedAs("Issue Category")
            .WithPart("TitlePart", part => part
                .WithPosition("0")
            )
            .WithPart("IssueCategory", part => part
                .WithPosition("1")
            )
            .WithPart("AutoroutePart", part => part
                .WithPosition("2")
            )
        );

        _contentDefinitionManager.AlterPartDefinition("IssueCategory", part => part
            .WithField("ResponsibleUsers", field => field
                .OfType("UserPickerField")
                .WithDisplayName("Responsible Users")
                .WithPosition("0")
                .WithSettings(new UserPickerFieldSettings
                {
                    Required = true,
                    Multiple = true,
                    DisplayAllUsers = false,
                    DisplayedRoles = new[]
                    {
                        "Administrator",
                    },
                })
            )
        );

        // Issue
        _contentDefinitionManager.AlterTypeDefinition("Issue", type => type
            .DisplayedAs("Issue")
            .Creatable()
            .Listable()
            .Securable()
            .WithPart("Issue", part => part
                .WithPosition("2")
            )
            .WithPart("AutoroutePart", part => part
                .WithPosition("3")
            )
            .WithPart("TitlePart", part => part
                .WithPosition("0")
            )
            .WithPart("ContactInformationPart", part => part
                .WithPosition("1")
            )
        );

        _contentDefinitionManager.AlterPartDefinition("Issue", part => part
            .WithField("Description", field => field
                .OfType("MarkdownField")
                .WithDisplayName("Description")
                .WithEditor("Wysiwyg")
                .WithPosition("0")
            )
            .WithField("Files", field => field
                .OfType("MediaField")
                .WithDisplayName("Files")
                .WithEditor("Attached")
                .WithPosition("4")
            )
            .WithField("Category", field => field
                .OfType("TaxonomyField")
                .WithDisplayName("Category")
                .WithPosition("1")
                .WithSettings(new TaxonomyFieldSettings
                {
                    Required = true,
                    TaxonomyContentItemId = "4gj5zq9wf1zaet99mqab9gvw4g",
                    Unique = true,
                })
            )
            .WithField("Assignees", field => field
                .OfType("UserPickerField")
                .WithDisplayName("Assignees")
                .WithPosition("2")
                .WithSettings(new UserPickerFieldSettings
                {
                    Multiple = true,
                    DisplayAllUsers = false,
                    DisplayedRoles = new[]
                    {
                        "Administrator",
                    },
                })
            )
            .WithField("Priority", field => field
                .OfType("TaxonomyField")
                .WithDisplayName("Priority")
                .WithPosition("3")
                .WithSettings(new TaxonomyFieldSettings
                {
                    TaxonomyContentItemId = "4mwtqz7n7dqyr1hxnr7hcte148",
                })
            )
        );

        _contentDefinitionManager.AlterPartDefinition("ContactInformationPart", part => part
            .Attachable()
                .WithDescription("Contact Information")
            .WithField("Name", field => field
                .OfType("TextField")
                .WithDisplayName("Name")
                .WithPosition("0")
            )
            .WithField("Email", field => field
                .OfType("TextField")
                .WithDisplayName("Email")
                .WithEditor("Email")
                .WithPosition("2")
            )
            .WithField("Phone", field => field
                .OfType("TextField")
                .WithDisplayName("Phone")
                .WithEditor("Tel")
                .WithPosition("1")
            )
        );
        return 1;
    }
}
