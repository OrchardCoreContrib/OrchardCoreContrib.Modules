using System.Collections;

namespace OrchardCoreContrib.Data.Migrations;

public class MigrationDictionary : IEnumerable<MigrationDictionaryRecord>
{
    private readonly Dictionary<string, List<MigrationDictionaryRecord>> _modulesMigrations;

    public MigrationDictionary()
    {
        _modulesMigrations = new();
    }

    public ICollection<string> ModuleIds => _modulesMigrations.Keys;

    public ICollection<MigrationDictionaryRecord> Migrations => _modulesMigrations.Values
        .SelectMany(m => m)
        .ToList();

    public List<MigrationDictionaryRecord> this[string moduleId]
    {
        get
        {
            _modulesMigrations.TryGetValue(moduleId, out List<MigrationDictionaryRecord> migrations);

            return migrations;
        }
    }

    public void Add(string moduleId, MigrationDictionaryRecord migration)
    {
        if (!_modulesMigrations.ContainsKey(moduleId))
        {
            _modulesMigrations.Add(moduleId, new List<MigrationDictionaryRecord>());
        }

        _modulesMigrations[moduleId].Add(migration);
    }

    public IEnumerable<MigrationDictionaryRecord> GetMigrations(string moduleId)
    {
        _modulesMigrations.TryGetValue(moduleId, out List<MigrationDictionaryRecord> migrations);

        return migrations;
    }

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

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
