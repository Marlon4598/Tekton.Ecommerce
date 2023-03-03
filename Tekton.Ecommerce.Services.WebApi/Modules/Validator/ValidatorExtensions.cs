using Tekton.Ecommerce.Application.Validator;

namespace Tekton.Ecommerce.Services.WebApi.Modules.Validator
{
    public static class ValidatorExtensions
    {
        public static IServiceCollection AddValidator(this IServiceCollection services)
        {
            services.AddTransient<ProductDtoValidator>();
            return services;
        }
    }
}
