using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QrCodeGenerator.Data;
using QrCodeGenerator.Models;
using QrCodeGenerator.Services;

namespace QrCodeGenerator.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly IContactService _contactService;
    private readonly IQrCodeService _qrCodeService;

    public ContactsController(IContactService contactService, IQrCodeService qrCodeService)
    {
        _contactService = contactService;
        _qrCodeService = qrCodeService;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
    {
        try
        {
            var contacts = await _contactService.GetContactsAsync();

            return Ok(contacts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContactById(int id)
    {
        try
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            return Ok(contact);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddContact([FromBody] Contact contactCard)
    {
        try
        {
            await _contactService.AddContactAsync(contactCard);
            return StatusCode(200);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpGet("edit/{id}")]
    [Authorize]
    public async Task<IActionResult> EditContact(int id)
    {
        try
        {
            var contact = await _contactService.EditContact(id);
            return Ok(contact);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateContact(int id, [FromBody] Contact contactCard)
    {
        try
        {
            await _contactService.UpdateContactAsync(id,contactCard);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("generateQrCode")]
    [Authorize]
    public IActionResult GenerateQrCode([FromBody] Contact contactCard)
    {
        try
        {
            byte[] qrCodeImage = _qrCodeService.GenerateQrCode(contactCard);
            return File(qrCodeImage, "image/png", "qrcode.png");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteContact(int id)
    {
        try
        {
            await _contactService.DeleteContactAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
