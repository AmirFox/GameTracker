using System;
using System.Collections.Generic;
using System.Linq;
using GameTracker.Contexts;
using GameTracker.Entities;
using GameTracker.Repositories.Interfaces;

namespace GameTracker.Repositories.Implementations
{
    public class GameRepository : IGameRepository
    {
        public GameRepository()
        {
        }

        public Game Add(Game game)
        {
            using var context = new GameTrackerContext();
            try
            {
                context.Add(game);
                context.SaveChanges();
                return game;
            }
            catch (Exception e)
            {
                Console.Write(e);
                return null;
            }
        }

        public int Delete(int id)
        {
            using var context = new GameTrackerContext();
            try
            {
                var game = context
                    .Games
                    .FirstOrDefault(x => x.Id == id);

                if (game != null)
                {
                    context.Remove(game);
                    context.SaveChanges();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                return 0;
            }
        }

        public Game Get(int id)
        {
            using var context = new GameTrackerContext();
            try
            {
                var game = context
                    .Games
                    .FirstOrDefault(x => x.Id == id);

                if (game != null)
                {
                    return game;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                return null;
            }
        }

        public IEnumerable<Game> GetAll()
        {
            using var context = new GameTrackerContext();
            return context?.Games?.ToList();
        }

        public Game Update(Game game)
        {
            using var context = new GameTrackerContext();
            try
            {
                context.Update(game);
                context.SaveChanges();
                return game;
            }
            catch (Exception e)
            {
                Console.Write(e);
                return null;
            }
        }
    }
}
