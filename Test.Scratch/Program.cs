using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Framework.Extensibility;
using Test.Identity.Model;
using Test.Identity.Stores;
using Test.Framework;
using Test.Identity.Services;
using Test.Data;
using Test.Framework.DataAccess;
using Test.Framework.Configuration;
using Test.Entities.Entity.Songs;

namespace Test.Scratch
{
    class Program
    {
        static void Main(string[] args)
        {
            Framework.Framework.Initialize();
            var configuration = Container.Resolve<IWebConfiguration>();
            List<string> connectionStringNames = configuration.GetConnectionStringNames().ToList();
            ConnectionRegister.Register(connectionStringNames, SqlDbmsType.SqlServer);
            OrmRegister.Register(connectionStringNames, OrmType.Dapper, SqlDbmsType.SqlServer);
            DbRegister.Register(connectionStringNames, SqlDbmsType.SqlServer);
            Container.Register<IDataProvider, DataProvider>(ObjectLifeSpans.Singleton);
            DataRegister.Initialize();
            //EntityMap.Initialize();
            Container.Register<IUserService<IdentityUser>, UserService<IdentityUser>>();
            Container.Register<IRoleService, RoleService>();
            Container.Register<IUserClaimsService, UserClaimsService>();
            Container.Register<IUserLoginsService, UserLoginsService>();
            Container.Register<IUserRolesService, UserRolesService>();
            Container.Register<IUserStore<IdentityUser>, UserStore<IdentityUser>>();
            try
            {
                //var userStore = Container.Resolve<IUserStore<IdentityUser>>();
                //var output = userStore.FindByNameAsync("devner89@gmail.com");
                var dataProvider = Container.Resolve<IDataProvider>();
                var musicRepo = dataProvider.MusicRepository;
                var albumOne = new Album { ArtistName = "Maroon 5", Title="V" };
                var albumId = musicRepo.AddAlbum(albumOne);
                var songs = new List<Song> { 
                    new Song{ Title = "Maps", Length = "3:10", TrackNumber = 1 },
                    new Song{ Title = "Animals", Length = "3:51", TrackNumber = 2 },
                    new Song{ Title = "It Was Always You", Length = "4:00", TrackNumber = 3 },
                    new Song{ Title = "Unkiss Me", Length = "3:58", TrackNumber = 4},
                    new Song{ Title = "Sugar", Length = "3:55", TrackNumber = 5 },
                    new Song{ Title = "Leaving California", Length = "3:24", TrackNumber = 6 },
                    new Song{ Title = "In Your Pocket", Length = "3:39", TrackNumber = 7},
                    new Song{ Title = "New Love", Length = "3:16", TrackNumber = 8},
                    new Song{ Title = "Coming Back for You", Length = "3:47", TrackNumber = 9},
                    new Song{ Title = "Feelings", Length = "3:14", TrackNumber = 10},
                    new Song{ Title = "My Heart Is Open", Length = "3:58", TrackNumber = 11},
                    new Song{ Title = "Shoot Love", Length = "3:10", TrackNumber = 12},
                    new Song{ Title = "Sex and Candy", Length = "4:26", TrackNumber = 13},
                    new Song{ Title = "Lost Stars", Length = "4:28", TrackNumber = 14}
                };
                var songIds = musicRepo.AddSong(albumId, songs);

                var songToUpdate = songs[0];
                songToUpdate.Genre = "Pop";
                songToUpdate.Id = songIds[0];
                songToUpdate.AlbumId = albumId;

                var updateSuccess = musicRepo.UpdateSong(albumId, songToUpdate);

                foreach (var songId in songIds)
                {
                    var deleteSuccess = musicRepo.DeleteSong(songId, albumId);
                }

                var deleteSuccessAlbum = musicRepo.DeleteAlbum(albumId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
