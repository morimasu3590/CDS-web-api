using BizCover.Application;
using BizCover.Repository.Cars;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizCover.WebAPI
{
    [ApiController]
    [Route("Api/Cars")]
    public class CarController : ControllerBase
    {
        private readonly IGetCarListQuery getCarListQuery;
        private readonly IGetCarQuery getCarQuery;
        private readonly IUpdateCarCommand updateCarCommand;
        private readonly ICreateCarCommand createCarCommand;
        private readonly ICalcCarDiscountCmd calcCarDiscountCmd;

        public CarController(
            IGetCarListQuery getCarListQuery
            ,IGetCarQuery getCarQuery
            ,IUpdateCarCommand updateCarCommand
            ,ICreateCarCommand createCarCommand
            ,ICalcCarDiscountCmd calcCarDiscountCmd)
        {
     
            this.getCarListQuery = getCarListQuery;
            this.getCarQuery = getCarQuery;
            this.updateCarCommand = updateCarCommand;
            this.createCarCommand = createCarCommand;
            this.calcCarDiscountCmd = calcCarDiscountCmd;
        }

        // GET /Item
        [HttpGet]
        public async Task<IEnumerable<CarItemModel>> GetCars()
        {
            return await getCarListQuery.Execute();
        }

        //GET /Cars/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CarItemModel>> GetCar(int id)
        {
            try
            {
                return await getCarQuery.Execute(id);
            }
            catch(ArgumentNullException)
            {
                return NotFound();
            }
        }

       [HttpGet("{id}")]
        public async Task<ActionResult<decimal>> GetDiscounts(int[] carIds)
        {
            if(carIds == null || carIds.Length <= 0)
            {
                return BadRequest();
            }

            decimal discount = await calcCarDiscountCmd.Execute(carIds);
            return Ok(
                new { discount }
            );
        }

        [HttpPost]
        public async Task<ActionResult<Car>> AddCar(CreateCarModel createCarModel)
        {
            var result = await createCarCommand.Execute(createCarModel);

            return CreatedAtAction(nameof(GetCar), new { id = result.Item1 }, result.Item2);

        }

        // POST /Cars/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCar(int id, UpdateCarModel updateCarModel)
        {
            try
            {
                await updateCarCommand.Execute(id, updateCarModel);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            return NoContent();
            
        }


    }
}
