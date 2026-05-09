using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics;

namespace Modul10_103022430013
{

    [ApiController]
    [Route("api/[controller]")]
    public class gameController : ControllerBase
    {
        private static List<Game> games = new List<Game>
        {
            new Game
            {
                Id = 1,
                Nama = "Valorant",
                Developer = "Riot Games",
                TahunRilis = 2020,
                Genre = "FPS",
                Rating = 8.5,
                Platform = new List<string> {"PC"},
                Mode = new List<string> {"Multiplayer"},
                IsOnline = true,
                Harga = 0
            },

            new Game
            {
                 Id = 2,
                 Nama = "GTA V",
                 Developer = "Rockstar Games",
                 TahunRilis = 2013,
                 Genre = "Open World",
                 Rating = 9.5,
                 Platform = new List<string> {"PC", "PS4", "PS5", "Xbox"},
                 Mode =  new List<string> {"Singleplayer", "Multiplayer" },
                 IsOnline = true,
                 Harga = 300000
            },

            new Game
            {
                Id = 3,
                Nama = "The Witcher 3",
                Developer = "CD Projekt Red",
                TahunRilis = 2015,
                Genre = "RPG",
                Rating = 9.7,
                Platform = new List<string>{"PC", "PS4", "PS5", "Xbox", "Switch" },
                Mode= new List<string> {"Singleplayer"},
                IsOnline = false,
                Harga = 250000
            }
        };

        [HttpGet]
        public List<Game> Get()
        {
            return games;
        }

        [HttpGet("{id}")]
        public ActionResult<Game> GetById(int id)
        {

            if (id <= 0 || id > games.Count)
            {

                return NotFound("Id game tidak vlid");
            }

            return games[id];
        }

        [HttpPost]
        public ActionResult Post([FromBody] Game game)
        {
            game.Id = games.Max(f => f.Id) + 1;
            games.Add(game);
            return Ok();
        }

        [HttpPut]
        public ActionResult Update(int id, [FromBody] Game updateGame)
        {
            if (id < 0 || id >= games.Count)
            {
                return NotFound("ID game tidak valid");
            }

            if (updateGame == null || string.IsNullOrEmpty(updateGame.Nama)
                || string.IsNullOrEmpty(updateGame.Developer) ||
                string.IsNullOrEmpty(updateGame.Genre) ||
                updateGame.TahunRilis <= 0 ||
                updateGame.Rating < 0 || updateGame.Rating > 10 ||
                updateGame.Platform == null || updateGame.Mode == null)
            {
                return BadRequest("Data tidak valid");
            }

            games[id-1].Nama = updateGame.Nama; ;
            games[id-1].Developer = updateGame.Developer;
            games[id-1].TahunRilis = updateGame.TahunRilis;
            games[id-1].Genre = updateGame.Genre;
            games[id-1].Rating = updateGame.Rating;
            games[id-1].Platform = updateGame.Platform;
            games[id-1].Mode = updateGame.Mode;
            games[id-1].IsOnline = updateGame.IsOnline;
            games[id-1].Harga = updateGame.Harga;

            return Ok("Genre berhasil diperbarui");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete (int id)
        {
            var game = games.FirstOrDefault(f => f.Id == id);

            if (game == null)
                return NotFound();
            
            games.Remove(game);
            return Ok();
        }

    }
}
      