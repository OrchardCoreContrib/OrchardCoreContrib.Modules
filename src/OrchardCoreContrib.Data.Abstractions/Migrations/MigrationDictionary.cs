using System.Collections;

namespace OrchardCoreContrib.Data.Migrations;

/// <summary>
/// Represents a dictionary of migrations.
/// </summary>
public class MigrationDictionary : IEnumerable<MigrationDictionaryRecord>
{
    private readonly Dictionary<string, List<MigrationDictionaryRecord>> _modulesMigrations;

    public MigrationDictionary()
    {
        _modulesMigrations = [];
    }

    /// <summary>
    /// Gets the module ids.
    /// </summary>
    public ICollection<string> ModuleIds => _modulesMigrations.Keys;

    /// <summary>
    /// Gets the migrations.
    /// </summary>
    public ICollection<MigrationDictionaryRecord> Migrations => _modulesMigrations.Values
        .SelectMany(m => m)
        .ToList();

    /// <summary>
    /// Gets the migrations for the specified module id.
    /// </summary>
    /// <param name="moduleId">The module identifier.</param>
    public List<MigrationDictionaryRecord> this[string moduleId]
    {
        get
        {
            _modulesMigrations.TryGetValue(moduleId, out List<MigrationDictionaryRecord> migrations);

            return migrations;
        }
    }

    /// <summary>
    /// Adds a migration.
    /// </summary>
    /// <param name="moduleId">The module identifier.</param>
    /// <param name="migration">The migration record.</param>
    public void Add(string moduleId, MigrationDictionaryRecord migration)
    {
        if (!_modulesMigrations.TryGetValue(moduleId, out List<MigrationDictionaryRecord> value))
        {
            value = ([]);
            _modulesMigrations.Add(moduleId, value);
        }

        value.Add(migration);
    }

    /// <inheritdoc/>
    public IEnumerator<MigrationDictionaryRecord> GetEnumerator()
    {
        foreach (var value in _modulesMigrations.Values)
        {
            foreach (var record in value.OrderBy(r => r.Id))
            {
                yield return record;
            }
        }
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
