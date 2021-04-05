using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopBridge_WEBAPI.Controllers;
using ShopBridge_WEBAPI.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
//using System.Web.Http;
using System.Web.Http.Results;

using Xunit;

namespace ShopBridge.Test
{
    public class UnitTestController
    {
        
       
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

        #region Get By Id  

        [Fact]
        public async void Task_GetPostById_Return_OkResult()
        {
            //Arrange  
            var controller = new InventoryController(context);
            var postId = 2;

            //Act  
            var data = await controller.getInventoryItem(postId);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void Task_GetPostById_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new InventoryController(context);
            var postId = 7;

            //Act  
            var data = await controller.getInventoryItem(postId);

            //Assert  
            Assert.IsType<Microsoft.AspNetCore.Mvc.NotFoundResult>(data);
        }

        [Fact]
        public async void Task_GetPostById_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new InventoryController(context);
            int? postId = null;

            //Act  
            var data = await controller.getInventoryItem(postId);

            //Assert  
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestResult>(data);
        }

        [Fact]
        public async void Task_GetPostById_MatchResult()
        {
            //Arrange  
            var controller = new InventoryController(context);
            int? postId = 2;

            //Act  
            var data = await controller.getInventoryItem(postId);

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var post = okResult.Value.Should().BeAssignableTo<Inventory>().Subject;

           
            Assert.Equal("Samsung A31", post.item_Name);
            Assert.Equal("Mobile Phone", post.item_Desc);
        }

        #endregion

        #region Get All  

        [Fact]
        public async void Task_GetPosts_Return_OkResult()
        {
            //Arrange  
            var controller = new InventoryController(context);

            //Act  
            var data = await controller.getInventoryItemList();

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void Task_GetPosts_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new InventoryController(context);

            //Act  
            var data = controller.getInventoryItemList();
            data = null;

            if (data != null)
                //Assert  
                Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestResult>(data);
        }

        [Fact]
        public async void Task_GetPosts_MatchResult()
        {
            //Arrange  
            var controller = new InventoryController(context);

            //Act  
            var data = await controller.getInventoryItemList();

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var post = okResult.Value.Should().BeAssignableTo<List<Inventory>>().Subject;


           
            Assert.Equal("Samsung A20", post[0].item_Name);
            Assert.Equal("Mobile Phone", post[0].item_Desc);

            Assert.Equal("Samsung A31", post[1].item_Name);
            Assert.Equal("Mobile Phone", post[1].item_Desc);
        }

        #endregion

        #region Add New Blog  

        [Fact]
        public async void Task_Add_ValidData_Return_OkResult()
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
            //Act  
            var data = await controller.PostInventoryItem(objInventory);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void Task_Add_InvalidData_Return_BadRequest()
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
            //Act   
            controller.ModelState.AddModelError("item_Name", "Item Name is Required");
            var data = await controller.PostInventoryItem(objInventory);

            //Assert  
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestResult>(data);
        }

        [Fact]
        public async void Task_Add_ValidData_MatchResult()
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

            //Act  
            var data = await controller.PostInventoryItem(objInventory);

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
           

           Assert.Equal(1, okResult.Value);
        }

        #endregion

        #region Update Existing Blog  

        [Fact]
        public async void Task_Update_ValidData_Return_OkResult()
        {
            //Arrange  
            var controller = new InventoryController(context);
            var postId = 2;

            //Act  
            var existingPost = await controller.getInventoryItem(postId);
            var okResult = existingPost.Should().BeOfType<OkObjectResult>().Subject;
            var result = okResult.Value.Should().BeAssignableTo<Inventory>().Subject;

            Inventory objInventory = new Inventory();

            
            byte[] image = new byte[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            objInventory.item_Name = "OnePlus 8t Updated";
            objInventory.item_Desc = result.item_Desc;
            objInventory.itemAvailability = result.itemAvailability;
            objInventory.item_AddedOn = result.item_AddedOn;
            objInventory.item_Quantity = result.item_Quantity;
            objInventory.item_Image = result.item_Image;

            var updatedData = await controller.PutInventoryItem(postId, objInventory);

            //Assert  
            Assert.IsType<Microsoft.AspNetCore.Mvc.OkResult>(updatedData);
        }

        [Fact]
        public async void Task_Update_InvalidData_Return_BadRequest()
        {
            //Arrange  
            var controller = new InventoryController(context);
            var postId = 2;

            //Act  
            var existingPost = await controller.getInventoryItem(postId);
            var okResult = existingPost.Should().BeOfType<OkObjectResult>().Subject;
            var result = okResult.Value.Should().BeAssignableTo<Inventory>().Subject;

            Inventory objInventory = new Inventory();

            

            byte[] image = new byte[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            objInventory.item_Name = null;
            objInventory.item_Desc = result.item_Desc;
            objInventory.itemAvailability = result.itemAvailability;
            objInventory.item_AddedOn = result.item_AddedOn;
            objInventory.item_Quantity = result.item_Quantity;
            objInventory.item_Image = result.item_Image;

            controller.ModelState.AddModelError("item_Name", "Item Name is Required");
            var updatedData = await controller.PutInventoryItem(postId, objInventory);


            //Assert  
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestResult>(updatedData);
        }

       

        #endregion

        #region Delete Post  

        [Fact]
        public async void Task_Delete_Post_Return_OkResult()
        {
            //Arrange  
            var controller = new InventoryController(context);
            var postId = 2;

            //Act  
            var data = await controller.DeleteInventoryItem(postId);

            //Assert  
            Assert.IsType<Microsoft.AspNetCore.Mvc.OkResult>(data);
        }

       

        [Fact]
        public async void Task_Delete_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new InventoryController(context);
            int? postId = null;

            //Act  
            var data = await controller.DeleteInventoryItem(postId);

            //Assert  
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestResult>(data);
        }

        #endregion

    }
}
