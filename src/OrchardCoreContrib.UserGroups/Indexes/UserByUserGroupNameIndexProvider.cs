using Microsoft.AspNetCore.Identity;
using OrchardCore.Users.Models;
using YesSql.Indexes;

namespace OrchardCoreContrib.UserGroups.Indexes;

public class UserByUserGroupNameIndexProvider(ILookupNormalizer keyNormalizer) : IndexProvider<User>
{
    public override void Describe(DescribeContext<User> context)
    {
        context.For<UserByGroupNameIndex, string>()
            .Map(user => user.GetUserGroups().Select(group => new UserByGroupNameIndex
            {
                GroupName = keyNormalizer.NormalizeName(group),
                Count = 1
            }))
            .Group(index => index.GroupName)
            .Reduce(group => new UserByGroupNameIndex
            {
                GroupName = group.Key,
                Count = group.Sum(x => x.Count)
            })
            .Delete((index, map) =>
            {
                index.Count -= map.Sum(x => x.Count);

                return index.Count > 0
                    ? index
                    : null;
            });
    }
}
