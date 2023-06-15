﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SliceOfItalyAPI.Data;
using SliceOfItalyAPI.Models;

namespace SliceOfItalyAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly SliceOfItalyContext _context;

    public CustomersController(SliceOfItalyContext context)
    {
        _context = context;
    }

    // GET: api/Customers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
        return await _context.Customer
            .ToListAsync();
    }

    // GET: api/Customers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomer(int id)
    {
        var customer = await _context.Customer
            .FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null)
        {
            return NotFound();
        }

        return customer;
    }

    // PUT: api/Customers/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustomer(int id, Customer customer)
    {
        if (id != customer.Id)
        {
            return BadRequest();
        }

        _context.Entry(customer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CustomerExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Customers
    [HttpPost]
    public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
    {
        _context.Customer?.Add(customer);
        await _context.SaveChangesAsync();

        return Ok(customer);
    }

    // DELETE: api/Customers/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Customer>> DeleteCustomer(int id)
    {
        var customer = await _context.Customer.FindAsync(id);
        if (customer == null)
        {
            return NotFound();
        }

        _context.Customer.Remove(customer);
        await _context.SaveChangesAsync();

        return customer;
    }

    private bool CustomerExists(int id)
    {
        return _context.Customer.Any(e => e.Id == id);
    }
}
