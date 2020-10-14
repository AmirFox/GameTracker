using System;
using System.Collections.Generic;
using GameTracker.Entities;

namespace GameTracker.Repositories.Interfaces
{
    public interface IGameRepository
    {
        IEnumerable<Game> GetAll();
        Game Get(int id);
        Game Add(Game game);
        int Delete(int id);
        Game Update(Game game);
    }
}
