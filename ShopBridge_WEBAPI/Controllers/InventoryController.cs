using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
//using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShopBridge_WEBAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopBridge_WEBAPI.Controllers
{
    //[Route("api/[controller]")]
    
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly ShopBridgeDBContext _context;

        //IPostRepository postRepository;
       
        public InventoryController(ShopBridgeDBContext context)
        {
            _context = context;
        }


        
        [HttpGet]
        [Route("api/getInventoryItemList")]
        public async Task<ActionResult<IEnumerable<Inventory>>> getInventoryItemList()
        {
            string type_master = "";
            try
            {
                var list =await (from tm in _context.Inventories
                                 select tm).ToListAsync();
                return list;

                //type_master = JsonConvert.SerializeObject(list);

            }
            catch (Exception ex)
            {
                type_master = ex.Message;
                return BadRequest();
            }
            return Ok(type_master);
        }
        
        
        [HttpGet]
        [Route("api/getInventoryItem")]
        public async Task<ActionResult<Inventory>> getInventoryItem(int InventoryId)
        {
           
            Inventory inventory_item = null;
            if (InventoryId != 0)
            {
                try
                {

                     inventory_item = await ((from inv in _context.Inventories
                                           where inv.item_Id == InventoryId
                                           select inv).SingleOrDefaultAsync()); // Getting product based on InventoryId from Database

                    //inventory_item = await _context.Inventories.FindAsync(InventoryId);

                    if ( inventory_item == null) {
                        return NotFound();
                    }

                }
                catch (Exception ex)
                {
                    
                    string exception_msg = ex.Message;
                    return BadRequest();
                }
            }

          
            return Ok(inventory_item);
            //return inventory_item; For unit testing purpose
        }


        [HttpPost]
        [Route("api/PostInventoryItem")]
        
        public async Task<IActionResult> PostInventoryItem(Inventory obj_Inventory)
        {
            try
            {

                _context.Inventories.Add(obj_Inventory);
                await _context.SaveChangesAsync(); // Inserting product to database

            }
            catch (Exception ex)
            {
                string Exception = ex.Message;
                return BadRequest();
            }

            //return Content(HttpStatusCode.Created, obj_Inventory);
            return StatusCode(200);
        }

        [Route("api/PutInventoryItem")]
        public async Task<IActionResult> PutInventoryItem(int id,Inventory obj_Inventory)
        {


            obj_Inventory.item_Id = id;

            _context.Entry(obj_Inventory).State = EntityState.Modified; // Updating product in databsae based on id

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            
            return StatusCode(200);
        

            //return NoContent();
        }

        [Route("api/DeleteInventoryItem")]
        public async Task<ActionResult<Inventory>> DeleteInventoryItem(int itemId)
        {

            try
            {

                var inventory_item = await _context.Inventories.FindAsync(itemId); // Finding and Deleting product from database
                if (inventory_item == null)
                {
                    return NotFound();
                }

                _context.Inventories.Remove(inventory_item);
                await _context.SaveChangesAsync();

                return inventory_item;

            }
            catch (Exception ex)
            {
                string Exception = ex.Message;
                return BadRequest();
            }


            return Ok();
        }

        private bool ItemExists(int id)
        {
            return _context.Inventories.Any(e => e.item_Id == id);
        }
    }
}
