using MagicVillaAPI.Data;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MagicVillaAPI.Controllers;

[ApiController]
[Route("api/VillaAPI")]
public class VillaAPIController : ControllerBase
{
    private readonly ILogger<VillaAPIController> _logger;
    private readonly AppDbContext _db;

    public VillaAPIController(ILogger<VillaAPIController> logger, AppDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas()
    {
        _logger.LogInformation("Get All Villas");
        return Ok(_db.Villas.ToList());
    }
    [HttpGet("{id:int}", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    /*    [ProducesResponseType(200, Type = typeof(VillaDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]*/
    public ActionResult<VillaDTO> GetVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        var villa = _db.Villas.FirstOrDefault(x => x.Id == id);
        if (villa == null)
        {
            return NotFound();
        }
        _logger.LogInformation($"Get One Villa {id}");
        return Ok(villa);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<VillaCreateDTO> CreateVilla([FromBody] VillaCreateDTO villaDTO)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(villaDTO);
        }
        if (villaDTO == null)
        {
            return BadRequest(villaDTO);
        }
        Villa villa = new Villa()
        {
            Name = villaDTO.Name,
        };
        _db.Villas.Add(villa);
        _db.SaveChanges();

        return CreatedAtRoute("GetVilla", new {id = villa.Id }, villa);
    }



    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<VillaDTO> DeleteVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        var villa = _db.Villas.FirstOrDefault(x => x.Id == id);
        if (villa == null)
        {
            return NotFound();
        }
        _db.Villas.Remove(villa);
        _db.SaveChanges(true);
        return NoContent();
    }

    [HttpPut("{id:int}", Name = "UpdateVilla")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult UpdateVilla(int id, [FromBody]VillaUpdateDTO villaDTO)
    {
        if(villaDTO == null || id != villaDTO.Id)
        {
            return BadRequest();
        }
        var villa = _db.Villas.AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (villa == null)
        {
            return NotFound();
        }
        Villa newVilla = new Villa()
        {
            Id = villaDTO.Id,
            Name = villaDTO.Name,
        };
        _db.Villas.Update(newVilla);
        _db.SaveChanges();

        return NoContent();
    }

    [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchVillaDTO)
    {
        if (patchVillaDTO == null || id == 0)
        {
            return BadRequest();
        }
        var villa = _db.Villas.AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (villa == null)
        {
            return BadRequest();
        }

        VillaUpdateDTO newVillaDTO = new VillaUpdateDTO()
        {
            Id = villa.Id,
            Name = villa.Name,
        };

        patchVillaDTO.ApplyTo(newVillaDTO, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Villa newVilla = new Villa()
        {
            Id = newVillaDTO.Id,
            Name = newVillaDTO.Name,
        };
        _db.Villas.Update(newVilla);
        _db.SaveChanges();

        return NoContent();
    }
}
