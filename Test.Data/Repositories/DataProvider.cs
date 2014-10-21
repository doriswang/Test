using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data.Repositories;
using Test.Framework;
using Test.Framework.DataAccess;
using Test.Framework.Extensibility;

namespace Test.Data
{
    public class DataProvider : IDataProvider
    {
        private IAuthenticationRepository _AuthenticationRepository;
        private IUserRepository _UserRepository;
        private IUserRolesRepository _UserRolesRepository;
        private IUserLoginsRepository _UserLoginsRepository;
        private IUserClaimsRepository _UserClaimsRepository;
        private IRoleRepository _RoleRepository;
        private IMusicRepository _MusicRepository;

        public IAuthenticationRepository AuthenticationRepository
        {
            get
            {
                if (_AuthenticationRepository == null)
                {
                    _AuthenticationRepository = RepositoryFactory.Instance.Get<IAuthenticationRepository>(new object[] { 
                        Container.Resolve<IDatabase>(CONSTANTS.AuthenticationConnectionStringName)
                    });
                }
                return _AuthenticationRepository;
            }
        }

        public IUserRepository UserRepository 
        {
            get
            {
                if (_UserRepository == null)
                {
                    _UserRepository = RepositoryFactory.Instance.Get<IUserRepository>(new object[] { 
                        Container.Resolve<IDatabase>(CONSTANTS.AuthenticationConnectionStringName)
                    });
                }
                return _UserRepository;
            }
        }

        public IUserRolesRepository UserRolesRepository
        {
            get
            {
                if (_UserRolesRepository == null)
                {
                    _UserRolesRepository = RepositoryFactory.Instance.Get<IUserRolesRepository>(new object[] { 
                        Container.Resolve<IDatabase>(CONSTANTS.AuthenticationConnectionStringName)
                    });
                }
                return _UserRolesRepository;
            }
        }

        public IUserLoginsRepository UserLoginsRepository
        {
            get
            {
                if (_UserLoginsRepository == null)
                {
                    _UserLoginsRepository = RepositoryFactory.Instance.Get<IUserLoginsRepository>(new object[] { 
                        Container.Resolve<IDatabase>(CONSTANTS.AuthenticationConnectionStringName)
                    });
                }
                return _UserLoginsRepository;
            }
        }

        public IUserClaimsRepository UserClaimsRepository
        {
            get
            {
                if (_UserClaimsRepository == null)
                {
                    _UserClaimsRepository = RepositoryFactory.Instance.Get<IUserClaimsRepository>(new object[] { 
                        Container.Resolve<IDatabase>(CONSTANTS.AuthenticationConnectionStringName)
                    });
                }
                return _UserClaimsRepository;
            }
        }

        public IRoleRepository RoleRepository
        {
            get
            {
                if (_RoleRepository == null)
                {
                    _RoleRepository = RepositoryFactory.Instance.Get<IRoleRepository>(new object[] { 
                        Container.Resolve<IDatabase>(CONSTANTS.AuthenticationConnectionStringName)
                    });
                }
                return _RoleRepository;
            }
        }

        public IMusicRepository MusicRepository
        {
            get
            {
                if (_MusicRepository == null)
                {
                    _MusicRepository = RepositoryFactory.Instance.Get<IMusicRepository>(new object[] { 
                        Container.Resolve<IDatabase>(CONSTANTS.SongsConnectionStringName)
                    });
                }
                return _MusicRepository;
            }
        }

        public IAuthenticationRepository GetAuthenticationRepository(string connectionString)
        {
            return RepositoryFactory.Instance.Get<IAuthenticationRepository>(new object[] { Container.Resolve<IDatabase>(connectionString) });
        }
    }
}
