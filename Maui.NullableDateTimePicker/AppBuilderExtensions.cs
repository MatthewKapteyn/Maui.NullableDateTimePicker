using CommunityToolkit.Maui;
using FFImageLoading.Maui;
namespace Maui.NullableDateTimePicker
{
    public static class AppBuilderExtensions
    {
        public static MauiAppBuilder ConfigureNullableDateTimePicker(this MauiAppBuilder builder)
        {
            builder.UseFFImageLoading();
#if DEBUG
            builder.UseMauiCommunityToolkit();
#else
            builder.UseMauiCommunityToolkit(options =>
                 {
                     options.SetShouldSuppressExceptionsInConverters(true);
                     options.SetShouldSuppressExceptionsInBehaviors(true);
                     options.SetShouldSuppressExceptionsInAnimations(true);
                 });
#endif
            return builder;
        }
        
    }
}
