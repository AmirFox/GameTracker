using System.Collections.Generic;
using System.Linq;
using GameTracker.Controllers;
using GameTracker.Entities;
using GameTracker.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GameTracker.UnitTests.Controller
{
    public class GameControllerTests
    {
        private GameController _gameController;
        private readonly Mock<IGameRepository> _mockRepository;

        private readonly Game JUST_CAUSE = new Game
        {
            Id = 0,
            Title = "Just Cause",
            Platforms = new[] { "PC", "Xbox One" }
        };

        private readonly Game FINAL_FANTASY = new Game
        {
            Id = 1,
            Title = "Final Fantasy",
            Platforms = new[] { "PS4" }
        };

        private readonly Game TOMB_RAIDER = new Game
        {
            Id = 2,
            Title = "Tomb Raider",
            Platforms = new[] { "PC", "Xbox One", "PS4" }
        };

        public GameControllerTests()
        {
            _mockRepository = new Mock<IGameRepository>();
        }

        [Fact]
        public void GetAllGamesTest()
        {
            //arrange
            var expected = new List<Game>() { JUST_CAUSE, FINAL_FANTASY, TOMB_RAIDER };
            _mockRepository
                .Setup(r => r.GetAll())
                .Returns(expected);

            //act
            _gameController = new GameController(_mockRepository.Object);
            var response = _gameController.GetAll();

            //assert
            Assert.IsType<OkObjectResult>(response.Result);
            var result = ((OkObjectResult)response.Result).Value;
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetValidGameTest()
        {
            //arrange
            var games = new List<Game>() { JUST_CAUSE, FINAL_FANTASY, TOMB_RAIDER };
            var expected = FINAL_FANTASY;
            _mockRepository
                .Setup(r => r.Get(It.IsAny<int>()))
                .Returns<int>(id => games.FirstOrDefault(g => g.Id == id));

            //act
            _gameController = new GameController(_mockRepository.Object);
            var response = _gameController.Get(FINAL_FANTASY.Id);

            //assert
            Assert.IsType<OkObjectResult>(response.Result);
            var actual = ((OkObjectResult)response.Result).Value;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetInvalidGameTest()
        {
            //arrange
            var games = new List<Game>() { JUST_CAUSE, TOMB_RAIDER };
            _mockRepository
                .Setup(r => r.Get(It.IsAny<int>()))
                .Returns<int>(id => games.FirstOrDefault(g => g.Id == id));

            //act
            _gameController = new GameController(_mockRepository.Object);
            var response = _gameController.Get(FINAL_FANTASY.Id);

            //assert
            Assert.IsType<NoContentResult>(response.Result);
        }

        [Fact]
        public void AddValidGameTest()
        {
            //arrange
            var expected = TOMB_RAIDER;
            _mockRepository
                .Setup(r => r.Add(It.IsAny<Game>()))
                .Returns<Game>(g => g);

            //act
            _gameController = new GameController(_mockRepository.Object);
            var response = _gameController.Add(TOMB_RAIDER);

            //assert
            Assert.IsType<OkObjectResult>(response.Result);
            var actual = ((OkObjectResult)response.Result).Value;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DeleteValidGameTest()
        {
            //arrange
            var games = new List<Game>() { JUST_CAUSE, FINAL_FANTASY, TOMB_RAIDER };
            int expected = 1;
            _mockRepository
                .Setup(r => r.Delete(It.IsAny<int>()))
                .Returns<int>(id => games.Exists(g=>g.Id == id) ? 1 : 0);

            //act
            _gameController = new GameController(_mockRepository.Object);
            var response = _gameController.Delete(FINAL_FANTASY.Id);

            //assert
            Assert.IsType<OkObjectResult>(response.Result);
            var actual = ((OkObjectResult)response.Result).Value;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DeleteInvalidGameTest()
        {
            //arrange
            var games = new List<Game>() { JUST_CAUSE, TOMB_RAIDER };
            int expected = 0;
            _mockRepository
                .Setup(r => r.Delete(It.IsAny<int>()))
                .Returns<int>(id => games.Exists(g => g.Id == id) ? 1 : 0);

            //act
            _gameController = new GameController(_mockRepository.Object);
            var response = _gameController.Delete(FINAL_FANTASY.Id);

            //assert
            Assert.IsType<OkObjectResult>(response.Result);
            var actual = ((OkObjectResult)response.Result).Value;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Update()
        {
            //arrange
            var expected = JUST_CAUSE;
            _mockRepository
                .Setup(r => r.Update(It.IsAny<Game>()))
                .Returns<Game>(g => new Game()
                {
                    Id = g.Id,
                    Title = $"{g.Title} 2",
                    Platforms = g.Platforms
                });

            //act
            _gameController = new GameController(_mockRepository.Object);
            var response = _gameController.Update(JUST_CAUSE);

            //assert
            Assert.IsType<OkObjectResult>(response.Result);
            var result = ((OkObjectResult)response.Result).Value;
            var actual = (Game)result;
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Platforms, actual.Platforms);
            Assert.NotEqual(expected.Title, actual.Title);
        }
    }
}
