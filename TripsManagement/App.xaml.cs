using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TripsManagement.Core.ApplicationServices.Common.Events;
using TripsManagement.Core.ApplicationServices.PipelineBehavior;
using TripsManagement.Core.ApplicationServices.Trips.Queries.GetAllTrips;
using TripsManagement.Core.DomainModels.Contracts;
using TripsManagement.Core.DomainModels.Contracts.Customers;
using TripsManagement.Core.DomainModels.Contracts.Trips;
using TripsManagement.Endpoint.WPF.Tools;
using TripsManagement.Endpoint.WPF.ViewModels;
using TripsManagement.Infra.DataAccess.Sql.Commands.Common;
using TripsManagement.Infra.DataAccess.Sql.Commands.Customers;
using TripsManagement.Infra.DataAccess.Sql.Commands.Trips;
using TripsManagement.Infra.DataAccess.Sql.Queries.Common;
using TripsManagement.Infra.DataAccess.Sql.Queries.Customers;
using TripsManagement.Infra.DataAccess.Sql.Queries.Trips;

namespace TripsManagement.Endpoint.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var services = CreateServiceProvider();

            var window = services.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddMediatR(typeof(GetAllTripsQuery));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddValidatorsFromAssembly(typeof(GetAllTripsQuery).Assembly);

            services.AddDbContext<TripsDbContext>(cfg =>
            {
                cfg.UseSqlServer("Data Source=.;Initial Catalog=TripsMngDev;Trusted_Connection=True;MultipleActiveResultSets=True;");
            });

            services.AddDbContext<TripsQueryDbContext>(cfg =>
            {
                cfg.UseSqlServer("Data Source=.;Initial Catalog=TripsMngDev;Trusted_Connection=True;MultipleActiveResultSets=True;");
            });

            services.AddScoped<ITripsUnitOfWork, TripsUnitOfWork>();

            services.AddScoped<ITripRepository, TripRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<ITripQueryRepository, TripQueryRepository>();
            services.AddScoped<ICustomerQueryRepository, CustomerQueryRepository>();
            services.AddScoped<ITripCustomerQueryRepository, TripCustomerQueryRepository>();

            services.AddScoped<IEventDispatcher, EventDispatcher>();

            services.AddTransient<MainWindow>();
            services.AddTransient<TripFormWindow>();
            services.AddTransient<CustomersWindow>();
            services.AddTransient<CustomerFormWindow>();
            services.AddTransient<TripCustomersWindow>();

            services.AddTransient<TripFormViewModel>();
            services.AddTransient<CustomerFormViewModel>();
            services.AddTransient<TripsViewModel>();
            services.AddTransient<CustomersViewModel>();
            services.AddTransient<TripCustomersViewModel>();

            services.AddSingleton<IWindowTools, WindowTools>();

            return services.BuildServiceProvider();
        }
    }
}


