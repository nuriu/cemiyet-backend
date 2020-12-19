using System;
using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Commands.Publishers
{
    public class DeleteOneCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteOneCommandValidator : AbstractValidator<DeleteOneCommand>
    {
        public DeleteOneCommandValidator()
        {
            RuleFor(doc => doc.Id).NotEmpty();
        }
    }
}
