using Domain.UseCases.AddAccount;
using Domain.UseCases.BlockedAccount;
using Domain.UseCases.GetAccountByUserId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controller responsável pelo gerenciamento de contas bancárias.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Construtor da controller de contas.
    /// </summary>
    /// <param name="mediator">
    /// Responsável pelo envio dos comandos e queries.
    /// </param>
    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cria uma nova conta bancária para o usuário informado.
    /// </summary>
    /// <param name="command">
    /// Dados necessários para criação da conta.
    /// </param>
    /// <returns>
    /// Retorna os dados da conta criada.
    /// </returns>
    /// <response code="200">
    /// Conta criada com sucesso.
    /// </response>
    /// <response code="400">
    /// Dados inválidos para criação da conta.
    /// </response>
    /// <response code="401">
    /// Usuário não autenticado.
    /// </response>
   // [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(AddAccountViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AddAccount([FromBody] AddAccountCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    /// <summary>
    /// Busca a conta bancária pelo identificador do usuário.
    /// </summary>
    /// <param name="userId">
    /// Identificador do usuário.
    /// </param>
    /// <returns>
    /// Retorna os dados da conta bancária.
    /// </returns>
    /// <response code="200">
    /// Conta encontrada com sucesso.
    /// </response>
    /// <response code="404">
    /// Conta não encontrada.
    /// </response>
    /// <response code="401">
    /// Usuário não autenticado.
    /// </response>
    //[Authorize]
    [HttpGet("user/{userId}")]
    [ProducesResponseType(typeof(GetAccountByUserIdViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetByUserId([FromRoute] Guid userId)
    {
        var result = await _mediator.Send(new GetAccountByUserIdQuery(userId));

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Atualiza o status da conta.
    /// </summary>
    [HttpPut("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromBody] BlockedAccountCommand command)
    {
        command.UserId = id;

        var result = await _mediator.Send(command);

        return Ok(result);
    }
}