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
            List<Store> res = new List<Store>();
            foreach (var existingStore in _stores)
            {
                if (existingStore.Name.Contains(keyword))
                    res.Add(existingStore);
            }

            return Ok(res.ToArray());
        }

        [HttpGet("get-by-country-city")]

        public IActionResult GetByCountryOrCity(string keyword)
        {
            List<Store> res = new List<Store>();
            foreach (var existingStore in _stores)
            {
                if (existingStore.Country == keyword || existingStore.City == keyword)
                    res.Add(existingStore);
            }

            return Ok(res.ToArray());
        }

        [HttpGet("get-by-sorted-income")]

        public IActionResult GetBySortedIncome()
        {
            List<Store> sorted = new List<Store>(_stores);



            if (_stores.Count > 0)
            {
                for (int i = 0; i < sorted.Count; i++)
                {
                    var store = sorted[i];
                    
                    for (int j = 0; j < sorted.Count; j++)
                    {
                        var other = sorted[j];
                        
                        if (store.MonthlyIncome < other.MonthlyIncome)
                            (sorted[i], sorted[j]) = (sorted[j], sorted[i]);
                    }

                }
            }
            return Ok(sorted.ToArray());
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
