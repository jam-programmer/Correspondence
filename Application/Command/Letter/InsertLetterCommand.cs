using Application.Common;
using Application.DataTransferObject;
using MediatR;

namespace Application.Command.Letter;

public sealed record InsertLetterCommand : IRequest<BaseResult>
{
    public LetterDto Letter { get; set; }
    public InsertLetterCommand(LetterDto Letter)
    {
        this.Letter = Letter;
    }
}
