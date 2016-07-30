using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Portfolio.Contracts.Repositories;
using Portfolio.Models;
using Portfolio.DAL.Repositories;
using Portfolio.DAL.Data;
using Portfolio.DAL.Repositories.Collections;

namespace Portfolio.WebUI.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            //container.RegisterType<DataContext>(new PerRequestLifetimeManager());

            container.RegisterType<IRepositoryBase<User>, UserRepository>();
            container.RegisterType<IRepositoryBase<Role>, RoleRepository>();

            container.RegisterType<UserRepositoryCollection>();
            container.RegisterType<PortfolioRepositoryCollection>();

            container.RegisterType<IRepositoryBase<Category>, CategoryRepository>();
            container.RegisterType<IRepositoryBase<Framework>, FrameworkRepository>();
            container.RegisterType<IRepositoryBase<Platform>, PlatformRepository>();
            container.RegisterType<IRepositoryBase<ProgrammingLanguage>, ProgrammingLanguageRepository>();
            container.RegisterType<IRepositoryBase<Tag>, TagRepository>();
            container.RegisterType<IRepositoryBase<PortfolioEntry>, PortfolioEntryRepository>();

        }
    }
}
