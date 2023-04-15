namespace OrchardCoreContrib.Data.Migrations.Tests;

public class MigrationRunnerTests
{
    
    //private static ISession GetSession()
    //{
    //    var records = new List<object>();
    //    var session = new Mock<ISession>();
    //    session
    //        .Setup(s => s.Save(It.IsAny<object>(), It.IsAny<bool>(), It.IsAny<string>()))
    //        .Callback<object, bool, string>((obj, _, _) =>
    //        {
    //            records.RemoveAll(r => r.GetType() == obj.GetType());
    //            records.Add(obj);
    //        });
    //    session
    //        .Setup(s => s.GetAsync<MigrationsHistory>(It.IsAny<long[]>(), It.IsAny<string>()))
    //        .Returns<long[], string>((ids, _) => Task.FromResult(records.OfType<MigrationsHistory>()));
    //    session
    //        .Setup(s => s.Query(It.IsAny<string>()))
    //        .Returns<string>(_ =>
    //        {
    //            var query = new Mock<IQuery>();
    //            query
    //                .Setup(q => q.For<MigrationsHistory>(It.IsAny<bool>()))
    //                .Returns<bool>(_ =>
    //                {
    //                    var queryOfT = new Mock<IQuery<MigrationsHistory>>();
    //                    queryOfT
    //                        .Setup(q => q.FirstOrDefaultAsync())
    //                        .Returns(() => Task.FromResult(default(MigrationsHistory)));

    //                    return queryOfT.Object;
    //                });

    //            return query.Object;
    //        });
    //    session
    //        .Setup(s => s.CancelAsync())
    //        .Returns(async () =>
    //        {
    //            records.Clear();

    //            await Task.CompletedTask;
    //        });

    //    return session.Object;
    //}
}
