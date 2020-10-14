using System;
using System.Collections.Generic;
using GameTracker.Entities;
using GameTracker.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace GameTracker.Controllers
{
    /// <summary>
    /// Controller representing all /game endpoints
    /// </summary>
    [Route("api/games")]
    [ApiController]
    public class GameController : Controller
    {
        private readonly IGameRepository _gameRepository;

        public GameController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        // GET: /api/games/List
        [HttpGet]
        [Route("list")]
        public ActionResult<IEnumerable<Game>> GetAll()
        {
            var games = _gameRepository.GetAll();
            return Ok(games);
        }

        [HttpGet]
        [Route("game/{id}")]
        public ActionResult<Game> Get(int id)
        {
            try
            {
                var result = _gameRepository.Get(id);
                if (result == null)
                    return NoContent();
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.Write(e);
                return BadRequest();
            }
        }

        //POST: /api/games/add
        [HttpPost]
        [Route("add")]
        public ActionResult<Game> Add([FromBody]Game gameParam)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var game = _gameRepository.Add(gameParam);
                    return Ok(game);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception e)
            {
                Console.Write(e);
                return BadRequest();
            }
        }

        //DELETE: api/games/delete
        [HttpDelete]
        [Route("delete/{id}")]
        public ActionResult<int> Delete(int id)
        {
            try
            {
                var result = _gameRepository.Delete(id);
                return Ok(result);
            }
            catch(Exception e)
            {
                Console.Write(e);
                return BadRequest();
            }
        }

        //PUT: api/games/update
        [HttpPut]
        [Route("update")]
        public ActionResult<Game> Update([FromBody]Game gameParam)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var game = _gameRepository.Update(gameParam);
                    return Ok(game);
                }
                else
                {

                    return BadRequest();
                }
            }
            catch(Exception e)
            {
                Console.Write(e);
                return BadRequest();
            }
        }
    }
}
