using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using WebApiExercitii.Models;

namespace WebApiExercitii.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        public static readonly List<Store> _stores = new List<Store>
        {
            new Store
            {
                Id = Guid.NewGuid(),
                Name = "Store 1",
                Country = "Country 1",
                City = "City 1",
                MonthlyIncome = 20,
                OwnerName = "Owner 1",
                ActiveSince = DateTime.Now

            },

            new Store
            {
                Id = Guid.NewGuid(),
                Name = "Store 2",
                Country = "Country 2",
                City =  "City 2",
                MonthlyIncome = 2,
                OwnerName = "Owner 1",
                ActiveSince = DateTime.Now

            }
        };

        [HttpGet]
        public Store[] GetAllStores()
        {
            return _stores.ToArray();
        }

        [HttpPost]
        public IActionResult CreateStore([FromBody]Store store)
        {
            if (store == null)
            {
                return BadRequest("Store is null");
            }
            foreach(var existingStore in _stores)
            {
                if (store.Id ==  existingStore.Id)
                {
                    return BadRequest("Store with the same id already exists");
                }
            }
            _stores.Add(store);
            return Ok(store);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStore([FromBody] Guid id)
        {

            foreach (var existingStore in _stores)
            {
                if (id == existingStore.Id)
                {
                    _stores.Remove(existingStore);
                    return Ok();
                }
            }
            return NotFound("Store not found");
        }

        [HttpPut("transfer-ownership/{storeId}")]
        public IActionResult TransferOwnership(Guid storeId, [FromBody] string name)
        {

            foreach (var existingStore in _stores)
            {
                if (storeId == existingStore.Id)
                {
                    existingStore.OwnerName = name;
                    return Ok();
                }
            }
            return NotFound("Store not found");
        }

        [HttpGet("filter-by-keyword")]
        public IActionResult FilterByKeyword(string keyword)
        {
            List<Store> res = new();
            foreach (var existingStore in _stores)
            {
                if (existingStore.Name.Contains(keyword))
                    res.Add(existingStore);
            }

            return Ok(res.ToArray());
        }

        [HttpGet("get-by-country")]
        public IActionResult GetByCountry(string keyword)
        {
            List<Store> res = new();
            foreach (var existingStore in _stores)
            {
                if (existingStore.Country.Contains(keyword) )
                    res.Add(existingStore);
            }

            return Ok(res.ToArray());
        }

        [HttpGet("get-by-city")]
        public IActionResult GetByCity(string keyword)
        {
            List<Store> res = new();
            foreach (var existingStore in _stores)
            {
                if (existingStore.City.Contains(keyword))
                    res.Add(existingStore);
            }

            return Ok(res.ToArray());
        }

        [HttpGet("get-by-sorted-income")]
        public IActionResult GetBySortedIncome()
        {
            return Ok(_stores.OrderBy(store => store.MonthlyIncome).ToArray());
        }


        [HttpGet("get-oldest-store")]
        public IActionResult GetOldestStore()
        {

            if(_stores.Count > 0)
            {
                Store oldest = _stores[0];
                foreach(var store in _stores)
                {
                    if (store.ActiveSince <  oldest.ActiveSince)
                        oldest = store;
                }
                return Ok(oldest);
            }
            return BadRequest("There are no stores.");
        }
    }




}
