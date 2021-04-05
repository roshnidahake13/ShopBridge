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
        public async Task<IActionResult> getInventoryItemList()
        {
            string type_master = "";
            try
            {
                var list =await (from tm in _context.Inventories
                                 select tm).ToListAsync();

                if (list == null) {
                    return NotFound();
                }
                return Ok(list);

              

            }
            catch (Exception ex)
            {
                type_master = ex.Message;
                return BadRequest();
            }
           
        }
        
        
        [HttpGet]
        [Route("api/getInventoryItem")]
        public async Task<IActionResult> getInventoryItem(int? InventoryId)
        {
            if (InventoryId == null)
            {
                return BadRequest();
            }
            Inventory inventory_item = null;
            if (InventoryId != 0)
            {
                try
                {

                    inventory_item = await ((from inv in _context.Inventories
                                             where inv.item_Id == InventoryId
                                             select inv).SingleOrDefaultAsync());

                    //inventory_item = await _context.Inventories.FindAsync(InventoryId);

                    if (inventory_item == null)
                    {
                        return NotFound();
                    }

                }
                catch (Exception ex)
                {

                    string exception_msg = ex.Message;
                    return BadRequest();
                }
            }
            

            //return inventory_item; For unit testing purpose
            return Ok(inventory_item);
        }


        [HttpPost]
        [Route("api/PostInventoryItem")]
        
        public async Task<IActionResult> PostInventoryItem(Inventory obj_Inventory)
        {
            if (ModelState.IsValid) {
                try
                {

                    _context.Inventories.Add(obj_Inventory);
                   var inventory_id= await _context.SaveChangesAsync();
                    if (inventory_id > 0)
                    {
                        return Ok(inventory_id);
                    }
                    else {
                        return NotFound();
                    }

                }
                catch (Exception ex)
                {
                    string Exception = ex.Message;
                    return BadRequest();
                }
            }
           
            return BadRequest();
        }

        [Route("api/PutInventoryItem")]
        public async Task<IActionResult> PutInventoryItem(int id,Inventory obj_Inventory)
        {

            if (ModelState.IsValid) {

                obj_Inventory.item_Id = id;

                _context.Entry(obj_Inventory).State = EntityState.Modified; //Comment this line while Unit Testing

                try
                {
                    if (_context != null)
                    {
                        //Update that post
                      //  _context.Inventories.Update(obj_Inventory)

                        //Commit the transaction
                       var result= await _context.SaveChangesAsync();
                        return Ok();
                    }

                   
                    
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }

            }
            
            return BadRequest();
        }

        [Route("api/DeleteInventoryItem")]
        public async Task<IActionResult> DeleteInventoryItem(int? itemId)
        {
            if (itemId==null) {
                return BadRequest();
            }
            try
            {

                var inventory_item = await _context.Inventories.FindAsync(itemId);
                if (inventory_item == null)
                {
                    return NotFound();
                }

                _context.Inventories.Remove(inventory_item);
                await _context.SaveChangesAsync();

                return Ok();

            }
            catch (Exception ex)
            {
                string Exception = ex.Message;
                return BadRequest();
            }


            
        }

        private bool ItemExists(int id)
        {
            return _context.Inventories.Any(e => e.item_Id == id);
        }
    }
}
