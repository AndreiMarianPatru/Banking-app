using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace PaymentGateway.Application
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection RegisterBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddTransient<EnrollCustomerOperation>();
            //services.AddTransient<CreateAccountOperation>();
            //services.AddTransient<DepositMoneyOperation>();
            //services.AddTransient<WithdrawMoneyOperation>();
            //services.AddTransient<PurchaseProductOperation>();
            //services.AddTransient<CreateProductOperation>();

            services.AddSingleton<Data.PaymentDbContext>();

            // services.AddTransient<IValidator<Query>, Validator>();


            services.AddSingleton(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var options = new AccountOptions
                {
                    InitialBalance = config.GetValue("AccountOptions:InitialBalance", 0)
                };
                return options;
            });


            return services;
        }
    }
}