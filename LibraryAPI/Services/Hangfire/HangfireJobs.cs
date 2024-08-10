using Hangfire;

namespace LibraryAPI.Services.Hangfire;

public static class HangfireJobs
{
    [Obsolete("Obsolete")]
    public static void ConfigureRecurringJobs(this IRecurringJobManager recurringJobs, IServiceProvider serviceProvider)
    {
        recurringJobs.AddOrUpdate(
            "change-book-status-daily",
            () => serviceProvider.GetService<UserBookService>()!.ChangeBookStatus(CancellationToken.None),
            "00 08 * * *",
            TimeZoneInfo.Local 
        );
    }
}