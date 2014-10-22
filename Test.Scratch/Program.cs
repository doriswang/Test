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
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using Test.Framework.Extensions;
using Test.Scratch.Xml;

namespace Test.Scratch
{
    class Program
    {
        static void Main(string[] args)
        {
            Framework.Framework.Initialize();

            XmlMusicRepository repo = new XmlMusicRepository();

            List<Album> albums = new List<Album>();
            List<Song> songs = new List<Song>();

            albums.Add(new Album { Id = 1, Title = "Prism", ArtistName = "Katy Perry" });
            albums.Add(new Album { Id = 2, Title = "Teenage Dream", ArtistName = "Katy Perry" });

            songs.Add(new Song { Id = 1, AlbumId = 1, Title = "Roar", Genre = "Pop", Length = "3:44", TrackNumber = 1, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 2, AlbumId = 1, Title = "Legendary Lovers", Genre = "Pop", Length = "3:44", TrackNumber = 2, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 3, AlbumId = 1, Title = "Birthday", Genre = "Pop", Length = "3:35", TrackNumber = 3, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 4, AlbumId = 1, Title = "Walking on Air", Genre = "Pop", Length = "3:42", TrackNumber = 4, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 5, AlbumId = 1, Title = "Unconditionally", Genre = "Pop", Length = "3:49", TrackNumber = 5, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 6, AlbumId = 1, Title = "Dark Horse", Genre = "Pop", Length = "3:36", TrackNumber = 6, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 7, AlbumId = 1, Title = "This Is How We Do", Genre = "Pop", Length = "3:24", TrackNumber = 7, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 8, AlbumId = 1, Title = "International Smile", Genre = "Pop", Length = "3:48", TrackNumber = 8, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 9, AlbumId = 1, Title = "Ghost", Genre = "Pop", Length = "3:23", TrackNumber = 9, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 10, AlbumId = 1, Title = "Love Me", Genre = "Pop", Length = "3:53", TrackNumber = 10, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 11, AlbumId = 1, Title = "This Moment", Genre = "Pop", Length = "3:47", TrackNumber = 11, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 12, AlbumId = 1, Title = "Double Rainbow", Genre = "Pop", Length = "3:52", TrackNumber = 12, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 13, AlbumId = 1, Title = "By The Grace Of God", Genre = "Pop", Length = "4:27", TrackNumber = 13, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 14, AlbumId = 1, Title = "Spiritual", Genre = "Pop", Length = "4:36", TrackNumber = 14, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 15, AlbumId = 1, Title = "It Takes Two", Genre = "Pop", Length = "3:55", TrackNumber = 15, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 16, AlbumId = 1, Title = "Choose Your Battles", Genre = "Pop", Length = "4:27", TrackNumber = 16, DateAdded = DateTime.Now, DateModified = DateTime.Now });

            songs.Add(new Song { Id = 1, AlbumId = 2, Title = "Teenage Dream", Genre = "Pop", Length = "3:48", TrackNumber = 1, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 2, AlbumId = 2, Title = "Last Friday Night (T.G.I.F)", Genre = "Pop", Length = "3:51", TrackNumber = 2, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 3, AlbumId = 2, Title = "California Gurls", Genre = "Pop", Length = "3:55", TrackNumber = 3, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 4, AlbumId = 2, Title = "Firework", Genre = "Pop", Length = "3:48", TrackNumber = 4, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 5, AlbumId = 2, Title = "Peacock", Genre = "Pop", Length = "3:52", TrackNumber = 5, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 6, AlbumId = 2, Title = "Circle The Drain", Genre = "Pop", Length = "4:33", TrackNumber = 6, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 7, AlbumId = 2, Title = "The One That Got Away", Genre = "Pop", Length = "3:47", TrackNumber = 7, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 8, AlbumId = 2, Title = "E.T.", Genre = "Pop", Length = "3:26", TrackNumber = 8, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 9, AlbumId = 2, Title = "Who Am I Living For?", Genre = "Pop", Length = "4:09", TrackNumber = 9, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 10, AlbumId = 2, Title = "Pearl", Genre = "Pop", Length = "4:08", TrackNumber = 10, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 11, AlbumId = 2, Title = "Hummingbird Heartbeat", Genre = "Pop", Length = "3:32", TrackNumber = 11, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 12, AlbumId = 2, Title = "Not Like The Movies", Genre = "Pop", Length = "4:01", TrackNumber = 12, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 13, AlbumId = 2, Title = "The One That Got Away - Acoustic", Genre = "Pop", Length = "4:19", TrackNumber = 13, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 14, AlbumId = 2, Title = "Park Of Me", Genre = "Pop", Length = "3:36", TrackNumber = 14, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 15, AlbumId = 2, Title = "Wide Awake", Genre = "Pop", Length = "3:41", TrackNumber = 15, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 16, AlbumId = 2, Title = "Dressin' Up", Genre = "Pop", Length = "3:44", TrackNumber = 16, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 17, AlbumId = 2, Title = "E.T. 2", Genre = "Pop", Length = "3:50", TrackNumber = 17, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 18, AlbumId = 2, Title = "Last Friday Night (T.G.I.F) 2", Genre = "Pop", Length = "3:59", TrackNumber = 18, DateAdded = DateTime.Now, DateModified = DateTime.Now });
            songs.Add(new Song { Id = 19, AlbumId = 2, Title = "Tommie Sunshine's Megasix Smash-Up", Genre = "Pop", Length = "7:03", TrackNumber = 19, DateAdded = DateTime.Now, DateModified = DateTime.Now });


            //var result = 0;
            //foreach (var album in albums)
            //{
            //    result += repo.AddAlbum(album);
            //}

            //var songResult = 0;
            //foreach (var song in songs)
            //{
            //    songResult += repo.AddSong(song.AlbumId, song);
            //}

            //var xmlAlbum1 = repo.GetAlbum(1);
            //var xmlAlbum2 = repo.GetAlbum(2);

            //var xmlSong1 = repo.GetSong(1);
            //var xmlSong2 = repo.GetSong(2);

            var newAlbum = new Album { Id = 3, Title = "Summa", ArtistName = "Sura" };
            var newSong = new Song { Id = 1, TrackNumber = 1, AlbumId = 3, Title = "Summa Song", Genre = "Tamil", Length = "3:55", DateAdded = DateTime.Now, DateModified = DateTime.Now  };

            var newAlbumUpdated = new Album { Id = 3, Title = "Gumma", ArtistName = "Sura" };
            var newSongUpdated = new Song { Id = 1, TrackNumber = 1, AlbumId = 3, Title = "Gumma Song", Genre = "Tamil", Length = "3:55", DateAdded = DateTime.Now, DateModified = DateTime.Now  };

            var testAlbum = new Album();
            var testSong = new Song();

            repo.AddAlbum(newAlbum);
            repo.AddSong(newAlbum, newSong);

            testAlbum = repo.GetAlbum(3);
            testSong = repo.GetSong(3, 1);

            repo.UpdateAlbum(newAlbumUpdated);
            repo.UpdateSong(newAlbumUpdated, newSongUpdated);

            testAlbum = repo.GetAlbum(3);
            testSong = repo.GetSong(3, 1);

            repo.DeleteAlbum(3);
            repo.DeleteSong(3);

            repo.AddAlbum(newAlbum);
            repo.AddSong(newAlbum, newSong);

            testAlbum = repo.GetAlbum(3);
            testSong = repo.GetSong(3, 1);

            repo.DeleteAlbum(newAlbum);
            repo.DeleteSong(3, 1);

            repo.AddAlbum(newAlbum);
            repo.AddSong(newAlbum, newSong);

            testAlbum = repo.GetAlbum(3);
            testSong = repo.GetSong(3, 1);

            repo.DeleteAlbum(newAlbum);
            repo.DeleteSong(3, newSong);
        }



        private static void CustomDALCheck()
        {
            try
            {
                var userStore = Container.Resolve<IUserStore<IdentityUser>>();
                var output = userStore.FindByNameAsync("devner89@gmail.com");
                var dataProvider = Container.Resolve<IDataProvider>();
                var musicRepo = dataProvider.MusicRepository;
                var albumOne = new Album { ArtistName = "Maroon 5", Title = "V" };
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

        private static void CustomDataAccessLayerInitialize()
        {
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
        }
    }
}
