using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.WebApi.Models;

namespace Test.WebApi.Services
{
    public interface IMusicService
    {
        RequestResult<MusicModel> GetMusic();
    }
}
