using ComputerAPIs.Data;
using ComputerAPIs.DTOs;
using ComputerAPIs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ComputerAPIs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComputerController : ControllerBase
    {
        private IConfiguration config;
        private ComputerDbContext context;
        public ComputerController(IConfiguration config)
        {
            this.config = config;
            context = new ComputerDbContext(config);
        }
        [HttpGet("GetComputers")]
        public IEnumerable<Computer> GetComputers()
        {
            IEnumerable<Computer> computers = context.Computers.ToList();
            return computers;
        }

        [HttpGet("GetComputer")]
        public Computer GetComputer(int computerId)
        {
            Computer computer = context.Computers.Where(c => c.ComputerId == computerId).FirstOrDefault();
            if (computer == null)
            {
                throw new ArgumentException("Provided id is not valid");
            }

            return computer;
        }

        [HttpPost("AddComputer")]
        public IActionResult AddComputer(ComputerDTO computerDTO)
        {

            Computer computer = new();
            computer.Model = computerDTO.Model;
            computer.Graphics = computerDTO.Graphics;
            computer.Memory = computerDTO.Memory;
            computer.Price = computerDTO.Price;
            computer.Processor = computerDTO.Processor;

            context.Computers.Add(computer);
            if (context.SaveChanges() == 0)
            {
                throw new ArgumentException("Computer not added");
            }
            return Ok();
        }

        [HttpPut("UpdateComputer")]
        public IActionResult UpdateComputer(int computerId, ComputerDTO computerDTO)
        {

            Computer computer = context.Computers.Where(id => id.ComputerId == computerId).FirstOrDefault();

            if (computer == null)
            {
                throw new ArgumentException("Provided id is not valid");
            }

            computer.Model = computerDTO.Model;
            computer.Graphics = computerDTO.Graphics;
            computer.Memory = computerDTO.Memory;
            computer.Price = computerDTO.Price;
            computer.Processor = computerDTO.Processor;

            if (context.SaveChanges() == 0)
            {
                throw new ArgumentException("No changed saved");
            }

            return Ok();
        }

        [HttpDelete("DeleteComputer")]
        public IActionResult DeleteComputer(int computerId)
        {
            Computer computer = context.Computers.Where(c => c.ComputerId == computerId).FirstOrDefault();
            if (computer == null)
            {
                throw new ArgumentException("Computer with this ID not found");
            }
            context.Computers.Remove(computer);

            if (context.SaveChanges() == 0)
            {
                throw new InvalidOperationException("No changed saved");
            }

            return Ok();
        }
    }
}
