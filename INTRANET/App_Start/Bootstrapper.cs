using Autofac;
//using Autofac.Integration.WebApi;
using Autofac.Integration.Mvc;
//using DataTables.AspNet.Core;
using INTRANET.Data.Infrastructure;
using INTRANET.Data.Repository;
using INTRANET.Data.Repository.Interfaces;
using INTRANET.Service;
using INTRANET.Service.Interfaces;
using System.Reflection;
using System.Web.Mvc;

namespace INTRANET
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();

            //Configure AutoMapper
            //AutoMapperConfiguration.Configure();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerLifetimeScope();

            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            ////builder.RegisterType<AuctionHub>();

            ////repositories
            //builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            //builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AcademicYearRepository>().As<IAcademicYearRepository>().InstancePerLifetimeScope();

            builder.RegisterType<HrEmployeeRepository>().As<IHrEmployeeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HrEmployeeDocumentRepository>().As<IHrEmployeeDocumentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HrDepartmentRepository>().As<IHrDepartmentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HrPositionRepository>().As<IHrPositionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HrCvDetailRepository>().As<IHrCvDetailRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HrCvEductionRepository>().As<IHrCvEductionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HrCvRelativeRepository>().As<IHrCvRelativeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HrCvLaborRepository>().As<IHrCvLaborRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HrCvAwardRepository>().As<IHrCvAwardRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HrCvMembershipRepository>().As<IHrCvMembershipRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HrCvHintTextRepository>().As<IHrCvHintTextRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HrPositionRepository>().As<IHrPositionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HrDepartmentRepository>().As<IHrDepartmentRepository>().InstancePerLifetimeScope();

            ////servises
            builder.RegisterType<ActiveDirectoryService>().As<IActiveDirectoryService>().InstancePerLifetimeScope();
            //builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope().PropertiesAutowired();
            //builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<AcademicYearService>().As<IAcademicYearService>().InstancePerLifetimeScope();
            builder.RegisterType<HrEmployeeDocumentService>().As<IHrEmployeeDocumentService>().InstancePerLifetimeScope();

            builder.RegisterType<HrEmployeeService>().As<IHrEmployeeService>().InstancePerLifetimeScope();
            builder.RegisterType<HrEmployeeDocumentService>().As<IHrEmployeeDocumentService>().InstancePerLifetimeScope();
            //builder.RegisterType<IDataTablesRequest>().InstancePerLifetimeScope();
            builder.RegisterType<HrDepartmentService>().As<IHrDepartmentService>().InstancePerLifetimeScope();
            builder.RegisterType<HrPositionService>().As<IHrPositionService>().InstancePerLifetimeScope();
            builder.RegisterType<HrCvDetailService>().As<IHrCvDetailService>().InstancePerLifetimeScope();
            builder.RegisterType<HrCvEductionService>().As<IHrCvEductionService>().InstancePerLifetimeScope();
            builder.RegisterType<HrCvRelativeService>().As<IHrCvRelativeService>().InstancePerLifetimeScope();
            builder.RegisterType<HrCvLaborService>().As<IHrCvLaborService>().InstancePerLifetimeScope();
            builder.RegisterType<HrCvAwardService>().As<IHrCvAwardService>().InstancePerLifetimeScope();
            builder.RegisterType<HrCvMembershipService>().As<IHrCvMembershipService>().InstancePerLifetimeScope();
            builder.RegisterType<HrCvHintTextService>().As<IHrCvHintTextService>().InstancePerLifetimeScope();
            builder.RegisterType<HrPositionService>().As<IHrPositionService>().InstancePerLifetimeScope();
            builder.RegisterType<HrDepartmentRepository>().As<IHrDepartmentRepository>().InstancePerLifetimeScope();


            builder.RegisterFilterProvider();
            IContainer container = builder.Build();
            //DependencyResolver.SetResolver(new AutofacWebApiDependencyResolver(container));
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}