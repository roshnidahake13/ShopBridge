using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopBridge_WEBAPI.Controllers;
using ShopBridge_WEBAPI.Models;
using System;
using System.Net;
using System.Web.Http;
//using System.Web.Http;
using System.Web.Http.Results;

using Xunit;

namespace ShopBridge.Test
{
    public class UnitTestController
    {
        //private PostRepository repository;
        ShopBridgeDBContext context = new ShopBridgeDBContext(dbContextOptions);
        public static DbContextOptions<ShopBridgeDBContext> dbContextOptions { get; }
        public static string connectionString = @"Data Source=LAPTOP-O5G13I0U\SQLEXPRESS;Initial Catalog=ShopBridge;Integrated Security=True;MultipleActiveResultSets=true;";
        static UnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<ShopBridgeDBContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        [Obsolete]
        public UnitTestController()
        {
            var context = new ShopBridgeDBContext(dbContextOptions);
            DummyDataDBInitializer db = new DummyDataDBInitializer();
            db.Seed(context);
            //repository = new PostRepository(context);

        }

        [Fact]
        public async void Task_GetById_Return()
        {
            //Arrange  
            var controller = new InventoryController(context);
            var postId = 2;

            //Act  
            var data = await controller.getInventoryItem(postId);

            //Assert  
            var result = Assert.IsType<ActionResult<Inventory>>(data);
            Assert.Null(result.Result);
            Assert.Equal("Samsung A31", result.Value.item_Name);
            //Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void Task_Post_Return()
        {
            try
            {
                //Arrange  

                var controller = new InventoryController(context);
                Inventory objInventory = new Inventory();
                byte[] image = new byte[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                objInventory.item_Name = "OnePlus 8t";
                objInventory.item_Desc = "Mobile Phone";
                objInventory.itemAvailability = Convert.ToBoolean(1);
                objInventory.item_AddedOn = DateTime.Now;
                objInventory.item_Quantity = 5;
                objInventory.item_Image = image;


                // Act

                IActionResult actionResult = await controller.PostInventoryItem(objInventory);

                // Assert

                Microsoft.AspNetCore.Mvc.StatusCodeResult statusCode = Assert.IsType<Microsoft.AspNetCore.Mvc.StatusCodeResult>(actionResult);
                Assert.Equal(200, statusCode.StatusCode);




            }
            catch (Exception ex)
            {
                string exception = ex.Message;
            }

        }

        [Fact]
        public async void Task_PutById_Return()
        {
            //Arrange  
            var controller = new InventoryController(context);
            var postId = 2;
            Inventory objInventory = new Inventory();

            objInventory.item_Name = "OnePlus 8t";


            //Act  
            //var data = await controller.PutInventoryItem(postId,objInventory);
            IActionResult actionResult = await controller.PutInventoryItem(postId, objInventory);

            //Assert  
            Microsoft.AspNetCore.Mvc.StatusCodeResult statusCode = Assert.IsType<Microsoft.AspNetCore.Mvc.StatusCodeResult>(actionResult);
            Assert.Equal(200, statusCode.StatusCode);

        }

        [Fact]
        public async void Task_DeletetById_Return()
        {
            //Arrange  
            var controller = new InventoryController(context);
            var postId = 2;

            //Act  
            var data = await controller.DeleteInventoryItem(postId);

            //Assert  
            var result = Assert.IsType<ActionResult<Inventory>>(data);
            Assert.Null(result.Result);
            Assert.Equal("Samsung A31", result.Value.item_Name);
            //Assert.IsType<OkObjectResult>(data);
        }

        //[Fact]
        //public async void Task_Post_Return()
        //{
        //    try
        //    {
        //        //Arrange  

        //        var controller = new InventoryController(context);
        //        Inventory objInventory = new Inventory();
        //        byte[] image = new byte[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        //        objInventory.item_Name = "OnePlus 8t";
        //        objInventory.item_Desc = "Mobile Phone";
        //        objInventory.itemAvailability = Convert.ToBoolean(1);
        //        objInventory.item_AddedOn = DateTime.Now;
        //        objInventory.item_Quantity = 5;
        //        objInventory.item_Image = image;
        //        //var postId = 2;

        //        // Act
        //        IHttpActionResult actionResult = (IHttpActionResult)controller.PostInventoryItem(new Inventory 
        //        {   item_Name = "OnePlus 8t",
        //            item_Desc = "Mobile Phone",
        //            itemAvailability = Convert.ToBoolean(1),
        //            item_AddedOn = DateTime.Now,
        //            item_Quantity = 5,
        //            item_Image = image
        //        });
        //        var badResult =  actionResult as HttpStatusCodeResult;


        //        // Assert
        //        Assert.IsType((Type)actionResult, typeof(System.Web.Http.Results.OkResult));

        //        //Act  
        //        //var data = await controller.PostInventoryItem(objInventory);

        //        //Assert  
        //        //var result = Assert.IsType<ActionResult<Inventory>>(data);
        //        //Assert.Null(result.Result.ToString());
        //        //Assert.Equal(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, result.);
        //        actionResult.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        //        //Assert.IsType<OkObjectResult>(data);
        //        //Assert.IsType(action)


        //    }
        //    catch(Exception ex)
        //    {
        //        string exception = ex.Message;
        //    }

        //}

        private class PostRepository
        {
            private ShopBridgeDBContext context;

            public PostRepository(ShopBridgeDBContext context)
            {
                this.context = context;
            }
        }
    }
}
