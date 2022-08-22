using EntityFrameworkMVCNew.Controllers;
using EntityFrameworkMVCNew.Data;
using EntityFrameworkMVCNew.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace MVCCore.Tests
{
    public class MVCTest
    {
        CricketPlayersController CricketPlayersController;
        public DbContextOptions<EntityFrameworkMVCNewContext> dbContextOptions { get; }
        public string ConnectionString = "Data Source=EPINHYDW05C1\\MSSQLSERVER1;Initial Catalog=CricketDB;Integrated Security=True;";
        //CricketPlayer player;
        public MVCTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<EntityFrameworkMVCNewContext>()
                .UseSqlServer(ConnectionString)
                .Options;
            var context = new EntityFrameworkMVCNewContext(dbContextOptions);
            CricketPlayersController = new CricketPlayersController(context);
            //player = new CricketPlayer();
        }

        [Fact]
        public async void A01_GetAllCricketPlayers()
        {
            var result = await CricketPlayersController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<CricketPlayer>>(
                viewResult.ViewData.Model);
            Assert.Equal(8, model.Count);

            //Assert.Equal(8, 8);
        }

        [Fact]
        public async void A02_GetAllCricketPlayerById()
        {
            int Player_id = 1;
            var result = await CricketPlayersController.Details(Player_id);

            CricketPlayer cricketPlayer = new CricketPlayer()
            {
                Id = 1,
                Name = "Sachin Tendulkar",
                Age = 35,
            };

            var viewResult = Assert.IsType<ViewResult>(result);
            var GotPlayer =  Assert.IsAssignableFrom<CricketPlayer>(
                viewResult.ViewData.Model);
            //Assert.Equal(8, model.Count);

            Assert.Equal(cricketPlayer.Id, GotPlayer.Id);
            Assert.Equal(cricketPlayer.Name, GotPlayer.Name);
            Assert.Equal(cricketPlayer.Age, GotPlayer.Age);

            //Assert.Equal(8, 8);
        }

        [Fact]
        public async void A03_AddCricketPlayer()
        {
            CricketPlayer addCricketPlayer = new CricketPlayer()
            {
                Name = "Suryakumar Yadav",
                Age = 30,
            };
            //player.Id = await CricketPlayersController.GetMaxId() + 1;

            var result = await CricketPlayersController.Create(addCricketPlayer);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);


            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            //employeeService.Verify(x => x.AddEmployee(It.IsAny<Employee>()), Times.Once);
            //Assert.Equal(cricketPlayer.Id, GotPlayer.Id);

        }

        [Fact]
        public async void A04_UpdateCricketPlayer()
        {
            int updatePlayerId = await CricketPlayersController.GetMaxId();
            CricketPlayer updateCricketPlayer = new CricketPlayer()
            {
                Id = updatePlayerId,
                Name = "Suryakumar Yadav_Updated",
                Age = 32,
            };

            var result = await CricketPlayersController.Edit(updatePlayerId, updateCricketPlayer);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            //To Take Ref for other test cases
            //player = updateCricketPlayer;

            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);

        }

        [Fact]
        public async void A05_DeleteCricketPlayer()
        {
            //int deletePlayerId = player.Id;
            int deletePlayerId = await CricketPlayersController.GetMaxId();
            //CricketPlayer deleteCricketPlayer = player;

            var resultPlayer = await CricketPlayersController.Details(deletePlayerId);

            var viewResult = Assert.IsType<ViewResult>(resultPlayer);
            var deleteCricketPlayer = Assert.IsAssignableFrom<CricketPlayer>(
                viewResult.ViewData.Model);

            var result = await CricketPlayersController.Delete(deletePlayerId);

            var viewResult1 = Assert.IsType<ViewResult>(result);
            var GotPlayer = Assert.IsAssignableFrom<CricketPlayer>(
                viewResult1.ViewData.Model);

            //Assert
            Assert.Equal(deleteCricketPlayer.Name, GotPlayer.Name);
            Assert.Equal(deleteCricketPlayer.Age, GotPlayer.Age);

        }
    }
}
