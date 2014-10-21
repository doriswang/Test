﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Test.WebApi.Models;
using Test.Framework.Extensions;

namespace Test.WebApi.Controllers
{
    public class SongController : BaseApiController
    {
        // GET: api/music/song/5
        public IHttpActionResult Get(int albumId)
        {
            if (albumId == 0)
                return BadRequest();

            var songs = songService.GetSong(albumId);

            if (!songs.IsNotNullOrEmpty())
                return InternalServerError();

            return Ok<IEnumerable<SongModel>>(songs);
        }

        // GET: api/music/song?albumId=5&id=2
        public IHttpActionResult Get(int albumId, int id)
        {
            if (albumId == 0 ||
                id == 0)
                return BadRequest();

            var song = songService.GetSong(albumId, id);

            if (song == null)
                return InternalServerError();

            return Ok<SongModel>(song);
        }

        // POST: api/music/song/5
        public IHttpActionResult Post(int albumId, [FromBody]SongModel songModel)
        {
            if (albumId == 0 ||
                songModel == null)
                return BadRequest();

            var result = songService.AddSong(albumId, songModel);

            if (result == 0)
                return InternalServerError();

            return Ok();
        }

        // PUT: api/music/song/5
        public IHttpActionResult Put(int albumId, [FromBody]SongModel songModel)
        {
            if (albumId == 0 ||
                songModel == null)
                return BadRequest();

            var result = songService.UpdateSong(albumId, songModel);

            if (!result)
                return InternalServerError();

            return Ok();
        }

        // DELETE: api/music/song/5
        public IHttpActionResult Delete(int albumId)
        {
            if (albumId == 0)
                return BadRequest();

            var result = songService.DeleteSong(albumId);

            if (!result)
                return InternalServerError();

            return Ok();
        }

        // DELETE: api/music/song?albumId=5&id=1
        public IHttpActionResult Delete(int albumId, int id)
        {
            if (albumId == 0 ||
                id == 0)
                return BadRequest();

            var result = songService.DeleteSong(albumId, id);

            if (!result)
                return InternalServerError();

            return Ok();
        }
    }
}