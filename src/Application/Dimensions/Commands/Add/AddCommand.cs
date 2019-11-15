using MediatR;

namespace Cemiyet.Application.Dimensions.Commands.Add
{
    public class AddCommand : IRequest
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }
}